Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlPortalMngInfoEvalTarget
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = True
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Performance\Modules\Portal" + Me.GetType().Name.ToString()
#Region "Property"

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
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim rep As New PerformanceRepository
                    Dim objID As New List(Of Decimal)
                    For Each item In rgMngInfoEvalTarget.MasterTableView.GetSelectedItems
                        objID.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteKpiAssessment(objID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMngInfoEvalTarget.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
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
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        rgMngInfoEvalTarget.AllowCustomPaging = True
        rgMngInfoEvalTarget.PageSize = Common.Common.DefaultPageSize
        SetFilter(rgMngInfoEvalTarget)
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Try
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                         ToolbarItem.Edit,
                                         ToolbarItem.Delete,
                                         ToolbarItem.Export)

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("SUBMIT_KPI",
                                                                  ToolbarIcons.Submit,
                                                                  ToolbarAuthorize.Special1,
                                                                  "Gửi duyệt KPI"))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_SUBMIT,
                                                                    ToolbarIcons.Submit,
                                                                    ToolbarAuthorize.Special4,
                                                                    "Gửi duyệt KQĐG"))
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        GetDataToCombo()
    End Sub

#End Region

#Region "Event"
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMngInfoEvalTarget.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            rgMngInfoEvalTarget.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    Dim lstStatus As New List(Of Decimal)({2}) '3,4?
                    For idx = 0 To rgMngInfoEvalTarget.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMngInfoEvalTarget.SelectedItems(idx)
                        'If item.GetDataKeyValue("IS_CONFIRM") = PortalStatus.ApprovedByLM And item.GetDataKeyValue("IS_CONFIRM") = PortalStatus.WaitingForApproval Then
                        '    ShowMessage(Translate("Thao tác này chỉ áp dụng đối với trạng thái Chưa gửi duyệt, Không phê duyệt, Hủy đơn. Vui lòng chọn dòng khác."), NotifyType.Error)
                        '    Exit Sub
                        'End If
                        If Not lstStatus.Contains(item.GetDataKeyValue("IS_CONFIRM")) Then
                            ShowMessage(Translate("Trạng thái bản ghi không cho phép xóa."), NotifyType.Warning)
                            Exit Sub
                        ElseIf Not lstStatus.Contains(item.GetDataKeyValue("STATUS_ID")) Then
                            ShowMessage(Translate("Trạng thái bản ghi không cho phép xóa."), NotifyType.Warning)
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
                            rgMngInfoEvalTarget.ExportExcel(Server, Response, dtDatas, "Performance Records")
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_SUBMIT
                    Dim rep As New PerformanceRepository
                    For Each item As GridDataItem In rgMngInfoEvalTarget.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") <> PortalStatus.Saved Then 'KIỂM TRA TRẠNG THÁI ĐƠN
                            ShowMessage(Translate("Chỉ gửi đơn ở trạng thái Chưa gửi duyệt. Vui lòng thử lại !"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If item.GetDataKeyValue("IS_CONFIRM") <> PortalStatus.ApprovedByLM Then 'KIỂM TRA TRẠNG THÁI ĐƠN
                            ShowMessage(Translate("Không thể gửi phê duyệt KQĐG khi KPI chưa được phê duyệt !"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If rep.ValidateSubmit(item.GetDataKeyValue("ID"), "SEND_APP") Then
                            ShowMessage(Translate("Vui lòng nhập kết quả đánh giá trước khi gửi duyệt"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn gửi phê duyệt?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_SUBMIT
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case "SUBMIT_KPI"

                    For Each item As GridDataItem In rgMngInfoEvalTarget.SelectedItems
                        If item.GetDataKeyValue("IS_CONFIRM") <> PortalStatus.Saved Then 'KIỂM TRA TRẠNG THÁI ĐƠN
                            ShowMessage(Translate("Chỉ gửi đơn ở trạng thái Chưa gửi duyệt. Vui lòng thử lại !"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn gửi phê duyệt?")
                    ctrlMessageBox.ActionName = "SUBMIT_KPI"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_SUBMIT And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            Dim rep As New PerformanceRepository
            Dim store As New PerformanceStoreProcedure
            Dim strId As String
            Dim countSuccess As Integer
            Dim employeeID = LogHelper.CurrentUser.EMPLOYEE_ID
            If rgMngInfoEvalTarget.SelectedItems.Count = 1 Then

                Dim sItem As GridDataItem = rgMngInfoEvalTarget.SelectedItems(0)
                strId = sItem.GetDataKeyValue("ID").ToString


                Dim outNumber As Decimal

                Try
                    outNumber = rep.PRI_PROCESS_APP(employeeID, sItem.GetDataKeyValue("PE_PERIOD_ID"), "PERFORMANCE", 0, 0, 0, strId, "")
                Catch ex As Exception
                    ShowMessage(ex.ToString, NotifyType.Error)
                End Try

                If outNumber = 0 Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                ElseIf outNumber = 1 Then
                    ShowMessage(Translate("Quy trình phê duyệt chưa được thiết lập"), NotifyType.Warning)
                ElseIf outNumber = 2 Then
                    ShowMessage(Translate("Thao tác xảy ra lỗi,bạn kiểm tra lại quy trình"), NotifyType.Error)
                ElseIf outNumber = 3 Then
                    ShowMessage(Translate("Nhân viên chưa có thiết lập nhóm chức danh"), NotifyType.Error)
                End If
                Dim Is_Exits As Boolean = True
                If outNumber <> 0 Or outNumber <> 1 Or outNumber <> 2 Then
                    Dim dt = store.GET_APP_ALL_TEMPLATES()
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
                    Refresh("UpdateView")
                    UpdateControlState()
                    rgMngInfoEvalTarget.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                End If
            ElseIf rgMngInfoEvalTarget.SelectedItems.Count > 1 Then
                For Each dr As GridDataItem In rgMngInfoEvalTarget.SelectedItems
                    strId = ""
                    strId = dr.GetDataKeyValue("ID").ToString + ","
                    strId = strId.Remove(strId.LastIndexOf(",")).ToString
                    Dim period = CDec(dr.GetDataKeyValue("PE_PERIOD_ID"))
                    Dim outNumber As Decimal

                    Try
                        outNumber = rep.PRI_PROCESS_APP(employeeID, period, "PERFORMANCE", 0, 0, 0, strId, "")
                    Catch ex As Exception
                        ShowMessage(ex.ToString, NotifyType.Error)
                    End Try

                    Dim Is_Exits As Boolean = True
                    If outNumber <> 0 Or outNumber <> 1 Or outNumber <> 2 Then
                        Dim dt = store.GET_APP_ALL_TEMPLATES()
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
                    If Is_Exits Then
                        'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        countSuccess += 1
                    End If
                Next
                If countSuccess > 0 Then
                    Refresh("UpdateView")
                    UpdateControlState()


                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                End If
            End If
        ElseIf e.ActionName = "SUBMIT_KPI" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            Dim rep As New PerformanceRepository
            Dim store As New PerformanceStoreProcedure
            Dim strId As String
            Dim countSuccess As Integer
            Dim employeeID = LogHelper.CurrentUser.EMPLOYEE_ID
            If rgMngInfoEvalTarget.SelectedItems.Count = 1 Then

                Dim sItem As GridDataItem = rgMngInfoEvalTarget.SelectedItems(0)
                strId = sItem.GetDataKeyValue("ID").ToString


                Dim outNumber As Decimal

                Try
                    outNumber = rep.PRI_PROCESS_APP(employeeID, sItem.GetDataKeyValue("PE_PERIOD_ID"), "PERFORMANCE_KPI", 0, 0, 0, strId, "")
                Catch ex As Exception
                    ShowMessage(ex.ToString, NotifyType.Error)
                End Try

                If outNumber = 0 Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                ElseIf outNumber = 1 Then
                    ShowMessage(Translate("Quy trình phê duyệt chưa được thiết lập"), NotifyType.Warning)
                ElseIf outNumber = 2 Then
                    ShowMessage(Translate("Thao tác xảy ra lỗi,bạn kiểm tra lại quy trình"), NotifyType.Error)
                ElseIf outNumber = 3 Then
                    ShowMessage(Translate("Nhân viên chưa có thiết lập nhóm chức danh"), NotifyType.Error)
                End If
                Dim Is_Exits As Boolean = True
                If outNumber <> 0 Or outNumber <> 1 Or outNumber <> 2 Then
                    Dim dt = store.GET_APP_ALL_TEMPLATES()
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
                    Refresh("UpdateView")
                    UpdateControlState()
                    rgMngInfoEvalTarget.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                End If
            ElseIf rgMngInfoEvalTarget.SelectedItems.Count > 1 Then
                For Each dr As GridDataItem In rgMngInfoEvalTarget.SelectedItems
                    strId = ""
                    strId = dr.GetDataKeyValue("ID").ToString + ","
                    strId = strId.Remove(strId.LastIndexOf(",")).ToString
                    Dim period = CDec(dr.GetDataKeyValue("PE_PERIOD_ID"))
                    Dim outNumber As Decimal

                    Try
                        outNumber = rep.PRI_PROCESS_APP(employeeID, period, "PERFORMANCE_KPI", 0, 0, 0, strId, "")
                    Catch ex As Exception
                        ShowMessage(ex.ToString, NotifyType.Error)
                    End Try

                    Dim Is_Exits As Boolean = True
                    If outNumber <> 0 Or outNumber <> 1 Or outNumber <> 2 Then
                        Dim dt = store.GET_APP_ALL_TEMPLATES()
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
                    If Is_Exits Then
                        'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        countSuccess += 1
                    End If
                Next
                If countSuccess > 0 Then
                    Refresh("UpdateView")
                    UpdateControlState()


                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                End If
            End If
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
    End Sub

    Protected Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        If cboYear.SelectedValue <> "" Then
            Dim rep As New PerformanceRepository
            FillRadCombobox(cboPeriod, rep.GetPeriodByYear(cboYear.SelectedValue), "NAME", "ID", True)
            If cboPeriod.SelectedValue <> "" Then
                Dim table As DataTable
                table = rep.GetDateByPeriod(cboPeriod.SelectedValue)
                rdFromDate.SelectedDate = If(table.Rows(0)(0).ToString() = "", Nothing, table.Rows(0)(0))
                rdToDate.SelectedDate = If(table.Rows(0)(1).ToString() = "", Nothing, table.Rows(0)(1))
            Else
                rdFromDate.SelectedDate = Nothing
                rdToDate.SelectedDate = Nothing
            End If
        Else
            cboPeriod.Text = ""
            cboPeriod.ClearSelection()
            rdFromDate.SelectedDate = Nothing
            rdToDate.SelectedDate = Nothing
        End If
    End Sub

    Protected Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim rep As New PerformanceRepository
        If cboPeriod.SelectedValue <> "" Then
            Dim table As DataTable
            table = rep.GetDateByPeriod(cboPeriod.SelectedValue)
            rdFromDate.SelectedDate = If(table.Rows(0)(0).ToString() = "", Nothing, table.Rows(0)(0))
            rdToDate.SelectedDate = If(table.Rows(0)(1).ToString() = "", Nothing, table.Rows(0)(1))
        Else
            rdFromDate.SelectedDate = Nothing
            rdToDate.SelectedDate = Nothing
        End If
    End Sub

    Protected Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgMngInfoEvalTarget.SelectedIndexChanged
        Try
            Dim count1 As Decimal = 0
            Dim count2 As Decimal = 0
            For Each item As GridDataItem In rgMngInfoEvalTarget.SelectedItems
                If item.GetDataKeyValue("IS_CONFIRM") = 3 AndAlso item.GetDataKeyValue("STATUS_ID") = 3 Then
                    count1 += 1
                ElseIf item.GetDataKeyValue("IS_CONFIRM") = 1 AndAlso item.GetDataKeyValue("STATUS_ID") = 3 Then
                    count2 += 1
                End If
            Next

            If count1 = rgMngInfoEvalTarget.SelectedItems.Count Then
                CType(MainToolBar.Items(4), RadToolBarButton).Enabled = True
                CType(MainToolBar.Items(5), RadToolBarButton).Enabled = False
            ElseIf count2 = rgMngInfoEvalTarget.SelectedItems.Count Then
                CType(MainToolBar.Items(4), RadToolBarButton).Enabled = False
                CType(MainToolBar.Items(5), RadToolBarButton).Enabled = True
            Else
                CType(MainToolBar.Items(4), RadToolBarButton).Enabled = False
                CType(MainToolBar.Items(5), RadToolBarButton).Enabled = False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New KPI_ASSESSMENT_DTO
        Dim rep As New PerformanceRepository
        Dim bCheck As Boolean = False
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            SetValueObjectByRadGrid(rgMngInfoEvalTarget, _filter)
            If cboYear.Text <> "" Then
                _filter.YEAR = cboYear.SelectedValue
            End If
            If cboPeriod.SelectedValue <> "" Then
                _filter.PE_PERIOD_ID = cboPeriod.SelectedValue
            End If
            If cboStatus.SelectedValue <> "" Then
                _filter.STATUS_ID = cboStatus.SelectedValue
            End If
            If cboKPIStatus.SelectedValue <> "" Then
                _filter.IS_CONFIRM = cboKPIStatus.SelectedValue
            End If
            _filter.EMPLOYEE = LogHelper.CurrentUser.EMPLOYEE_ID
            Dim MaximumRows As Integer
            Dim Sorts As String = rgMngInfoEvalTarget.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetPortalAssessment(_filter, Sorts).ToTable()
                Else
                    Return rep.GetPortalAssessment(_filter).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    rgMngInfoEvalTarget.DataSource = rep.GetPortalAssessment(_filter, rgMngInfoEvalTarget.CurrentPageIndex, rgMngInfoEvalTarget.PageSize, MaximumRows, Sorts)
                Else
                    rgMngInfoEvalTarget.DataSource = rep.GetPortalAssessment(_filter, rgMngInfoEvalTarget.CurrentPageIndex, rgMngInfoEvalTarget.PageSize, MaximumRows)
                End If

                rgMngInfoEvalTarget.VirtualItemCount = MaximumRows
            End If
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub GetDataToCombo()
        Dim rep As New PerformanceRepository
        Dim reps As New PerformanceStoreProcedure
        Dim dtTable As New DataTable
        dtTable = reps.GetYear(False)
        FillRadCombobox(cboYear, dtTable, "YEAR", "ID")
        cboYear.SelectedValue = Date.Now.Year
        If cboYear.SelectedValue <> "" Then
            FillRadCombobox(cboPeriod, rep.GetPeriodByYear(cboYear.SelectedValue), "NAME", "ID", True)
            If cboPeriod.SelectedValue <> "" Then
                Dim table As DataTable
                table = rep.GetDateByPeriod(cboPeriod.SelectedValue)
                rdFromDate.SelectedDate = If(table.Rows(0)(0).ToString() = "", Nothing, table.Rows(0)(0))
                rdToDate.SelectedDate = If(table.Rows(0)(1).ToString() = "", Nothing, table.Rows(0)(1))
            Else
                rdFromDate.SelectedDate = Nothing
                rdToDate.SelectedDate = Nothing
            End If
        Else
            cboPeriod.Text = ""
            cboPeriod.ClearSelection()
            rdFromDate.SelectedDate = Nothing
            rdToDate.SelectedDate = Nothing
        End If
        Dim dtData = rep.GetOtherList("PROCESS_STATUS", Common.Common.SystemLanguage.Name, True)
        If dtData IsNot Nothing Then
            Dim data = dtData.AsEnumerable().Where(Function(f) Not f.Field(Of Decimal?)("ID").HasValue _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.WaitingForApproval).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.ApprovedByLM).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.UnApprovedByLM).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.Saved).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.Cancel).ToString()).CopyToDataTable()
            FillRadCombobox(cboStatus, data, "NAME", "ID")
            ''cboStatus.SelectedValue = PortalStatus.WaitingForApproval
            FillRadCombobox(cboKPIStatus, data, "NAME", "ID")
            'cboKPIStatus.SelectedValue = PortalStatus.WaitingForApproval
        End If
    End Sub
#End Region

End Class