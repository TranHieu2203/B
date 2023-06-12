Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports Telerik.Web.UI

Public Class ctrlOTRegistration
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False
    Dim psp As New AttendanceStoreProcedure
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
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteOtRegistration(lstDeletes) Then
                        Refresh("UpdateView")
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_APPROVE
                    Dim lstApp As New List(Of AT_OT_REGISTRATIONDTO)
                    Dim Is_Exits As Boolean = True
                    If rgMain.SelectedItems.Count = 1 Then
                        Dim idx As GridDataItem = DirectCast(rgMain.SelectedItems(0), GridDataItem)
                        Dim dto As New AT_OT_REGISTRATIONDTO
                        dto.ID = idx.GetDataKeyValue("ID")
                        dto.REGIST_DATE = idx.GetDataKeyValue("REGIST_DATE")
                        'Kiem tra ky cong da dong hay chua
                        Dim periodid = rep.GetperiodID_2(idx.GetDataKeyValue("EMPLOYEE_ID"), dto.REGIST_DATE)
                        If periodid = 0 Then
                            'ShowMessage(Translate("Kiểm tra lại kì công"), NotifyType.Warning)
                            'Exit Sub
                        ElseIf periodid = -1 Then
                            ShowMessage(Translate("Đối tượng nhân viên chưa được khởi tạo chu kỳ chấm công"), NotifyType.Warning)
                            Exit Sub
                        ElseIf periodid = -2 Then
                            ShowMessage(Translate("Nhân viên chưa được thiết lập đối tượng nhân viên"), NotifyType.Warning)
                            Exit Sub
                        End If
                        'Dim checkKicong = rep.CHECK_PERIOD_CLOSE(periodid)
                        'If checkKicong = 0 Then
                        '    ShowMessage(Translate("Kì công đã đóng. Vui lòng kiểm tra lại!"), NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        dto.PERIOD_ID = periodid
                        dto.STATUS = PortalStatus.WaitingForApproval
                        dto.EMPLOYEE_ID = idx.GetDataKeyValue("EMPLOYEE_ID")
                        dto.REASON = ""
                        lstApp.Add(dto)
                        Dim outNumber = rep.SendApproveOtRegistration(lstApp)
                        If outNumber = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        ElseIf outNumber = 1 Then
                            ShowMessage(Translate("Quy trình phê duyệt chưa được thiết lập"), NotifyType.Success)
                        ElseIf outNumber = 2 Then
                            ShowMessage(Translate("Thao tác xảy ra lỗi,bạn kiểm tra lại quy trình"), NotifyType.Error)
                        ElseIf outNumber = 3 Then
                            ShowMessage(Translate("Nhân viên chưa có thiết lập nhóm chức danh"), NotifyType.Error)
                        End If
                        If outNumber <> 0 Or outNumber <> 1 Or outNumber <> 2 Then
                            Dim dt = psp.GET_APP_ALL_TEMPLATES()
                            If dt IsNot Nothing Then
                                For Each item In dt.Rows
                                    If Decimal.Parse(item("ID")) = outNumber Then
                                        Is_Exits = False
                                        ShowMessage(Translate("Không tồn tại cấp phê duyệt theo template phê duyệt (" + item("TEMPLATE_NAME") + " )"), NotifyType.Warning)
                                        Exit For
                                    End If
                                Next
                            End If
                        End If

                        If Is_Exits Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        End If
                    ElseIf rgMain.SelectedItems.Count > 1 Then
                        Dim countSuccess As Integer
                        For Each idx As GridDataItem In rgMain.SelectedItems
                            Dim dto As New AT_OT_REGISTRATIONDTO
                            lstApp = New List(Of AT_OT_REGISTRATIONDTO)
                            dto.ID = idx.GetDataKeyValue("ID")
                            dto.REGIST_DATE = idx.GetDataKeyValue("REGIST_DATE")
                            'Kiem tra ky cong da dong hay chua
                            Dim periodid = rep.GetperiodID_2(idx.GetDataKeyValue("EMPLOYEE_ID"), dto.REGIST_DATE)
                            If periodid = 0 Then
                                'ShowMessage(Translate("Kiểm tra lại kì công"), NotifyType.Warning)
                                'Exit Sub
                            ElseIf periodid = -1 Then
                                ShowMessage(Translate("Đối tượng nhân viên chưa được khởi tạo chu kỳ chấm công"), NotifyType.Warning)
                                Exit Sub
                            ElseIf periodid = -2 Then
                                ShowMessage(Translate("Nhân viên chưa được thiết lập đối tượng nhân viên"), NotifyType.Warning)
                                Exit Sub
                            End If
                            'Dim checkKicong = rep.CHECK_PERIOD_CLOSE(periodid)
                            'If checkKicong = 0 Then
                            '    ShowMessage(Translate("Kì công đã đóng. Vui lòng kiểm tra lại!"), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            dto.PERIOD_ID = periodid
                            dto.STATUS = PortalStatus.WaitingForApproval
                            dto.EMPLOYEE_ID = idx.GetDataKeyValue("EMPLOYEE_ID")
                            dto.REASON = ""
                            lstApp.Add(dto)
                            Dim outNumber = rep.SendApproveOtRegistration(lstApp)
                            If outNumber <> 0 Or outNumber <> 1 Or outNumber <> 2 Then
                                Dim dt = psp.GET_APP_ALL_TEMPLATES()
                                If dt IsNot Nothing Then
                                    For Each item In dt.Rows
                                        If Decimal.Parse(item("ID")) = outNumber Then
                                            Is_Exits = False
                                            'ShowMessage(Translate("Không tồn tại cấp phê duyệt theo template phê duyệt (" + item("TEMPLATE_NAME") + " )"), NotifyType.Warning)
                                            Exit For
                                        End If
                                    Next
                                End If
                            End If
                            If Is_Exits And outNumber = 0 Then
                                'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                countSuccess += 1
                            End If
                        Next

                        If countSuccess > 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgMain.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    End If

                    'If Not rep.SendApproveOtRegistration(lstApp) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    'Else
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    'End If
            End Select
            rep.Dispose()
        Catch ex As Exception

        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then

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
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Seperator, _
                         ToolbarItem.Submit, ToolbarItem.Export, ToolbarItem.Seperator, ToolbarItem.Delete)
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
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.UnApprovedByLM).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.Saved).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.Cancel).ToString()).CopyToDataTable()
                FillRadCombobox(cboStatus, data, "NAME", "ID")

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
                Case CommonMessage.TOOLBARITEM_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        If item.GetDataKeyValue("STATUS") <> PortalStatus.Saved And item.GetDataKeyValue("STATUS") <> PortalStatus.UnApprovedByLM And item.GetDataKeyValue("STATUS") <> PortalStatus.Cancel Then
                            ShowMessage(Translate("Thao tác này chỉ áp dụng đối với trạng thái Chưa gửi duyệt, Không phê duyệt, Hủy đơn. Vui lòng chọn dòng khác."), NotifyType.Error)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtDatas, "Overtime Record")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SUBMIT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    'Dim listDataCheck As New List(Of AT_PROCESS_DTO)
                    'Dim datacheck As AT_PROCESS_DTO
                    ''Kiểm tra các điều kiện trước khi xóa
                    'For Each dr As Telerik.Web.UI.GridDataItem In rgMain.SelectedItems
                    '    If dr.GetDataKeyValue("STATUS") <> PortalStatus.Saved And dr.GetDataKeyValue("STATUS") <> PortalStatus.UnApprovedByLM Then
                    '        ShowMessage(String.Format(Translate("Trạng thái {0} không thể gửi duyệt. Vui lòng chọn dòng khác."), dr.GetDataKeyValue("STATUS_NAME")), NotifyType.Warning)
                    '        Exit Sub
                    '    End If
                    '    datacheck = New AT_PROCESS_DTO With {
                    '        .EMPLOYEE_ID = dr.GetDataKeyValue("EMPLOYEE_ID"),
                    '        .FROM_DATE = dr.GetDataKeyValue("REGIST_DATE"),
                    '        .FULL_NAME = dr.GetDataKeyValue("FULLNAME")
                    '    }
                    '    listDataCheck.Add(datacheck)
                    'Next

                    'Dim itemError As New AT_PROCESS_DTO
                    'Using rep As New AttendanceRepository
                    '    Dim checkResult = rep.CheckTimeSheetApproveVerify(listDataCheck, "LEAVE", itemError)
                    '    If Not checkResult Then
                    '        ShowMessage(String.Format(Translate("Thời gian biểu của {0} trong tháng {1} đã được phê duyệt."), itemError.FULL_NAME, itemError.FROM_DATE.Value.Month & "/" & itemError.FROM_DATE.Value.Year), NotifyType.Warning)
                    '        Exit Sub
                    '    End If
                    'End Using

                    Dim _item As GridDataItem
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        _item = rgMain.SelectedItems(idx)
                        If _item.GetDataKeyValue("STATUS") <> PortalStatus.Saved Then 'KIỂM TRA TRẠNG THÁI ĐƠN
                            ShowMessage(Translate("Chỉ gửi đơn ở trạng thái Chưa gửi duyệt. Vui lòng thử lại !"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate("Gửi duyệt. Bạn có chắc chắn gửi duyệt?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_SUBMIT
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_SUBMIT And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_APPROVE
            UpdateControlState()
        End If

        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
        rgMain.Rebind()
    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim _filter As New AT_OT_REGISTRATIONDTO
        Dim dtData As New DataTable
        Try
            Dim MaximumRows As Integer
            _filter.EMPLOYEE_ID = EmployeeID

            If rdRegDateFrom.SelectedDate.HasValue Then
                _filter.REGIST_DATE_FROM = rdRegDateFrom.SelectedDate
            End If
            If rdRegDateTo.SelectedDate.HasValue Then
                _filter.REGIST_DATE_TO = rdRegDateTo.SelectedDate
            End If
            If Not String.IsNullOrEmpty(cboStatus.SelectedValue) Then
                _filter.STATUS = cboStatus.SelectedValue
            End If
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    dtData = rep.GetPortalOtRegData(_filter, MaximumRows, rgMain.CurrentPageIndex, rgMain.PageSize, "EMPLOYEE_CODE, REGIST_DATE desc")
                Else
                    dtData = rep.GetPortalOtRegData(_filter, MaximumRows, rgMain.CurrentPageIndex, rgMain.PageSize)
                End If
            Else
                Return rep.GetPortalOtRegData(_filter)
            End If
            If Not IsPostBack Then
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
        For Each column As DataColumn In dt.Columns
            If column.ColumnName = "CBSTATUS" Then
                rColCheck = New GridClientSelectColumn()
                rgMain.MasterTableView.Columns.Add(rColCheck)
                rColCheck.HeaderStyle.Width = 30
                rColCheck.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            End If
            If Not column.ColumnName.Contains("ID") And column.ColumnName <> "STATUS" And column.ColumnName <> "EMPLOYEE_CODE" And column.ColumnName <> "FULLNAME" And
                    column.ColumnName <> "DEPARTMENT_NAME" And column.ColumnName <> "TITLE_NAME" And
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
                If column.ColumnName = "STT" OrElse column.ColumnName.ToUpper = "MODIFIED_BY" OrElse column.ColumnName = "WORK_STATUS" _
                    OrElse column.ColumnName = "IS_DELETED" OrElse column.ColumnName = "STT" OrElse column.ColumnName.ToUpper = "MODIFIED_DATE" OrElse column.ColumnName.ToUpper = "MODIFIED_NAME" Then
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
                If column.ColumnName = "CREATED_DATE" OrElse column.ColumnName.ToUpper = "MODIFIED_DATE" Then
                    rColDate.Visible = False
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