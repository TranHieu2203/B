Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPortalApproveEvalTarget
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Protected WithEvents ctrlCommon_Reject As ctrlCommon_Reject
    Public Overrides Property MustAuthorize As Boolean = True
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Performance\Modules\Portal" + Me.GetType().Name.ToString()
    Dim rep As New PerformanceRepository
    Private psp As New CommonRepository
#Region "Property"
    Public Property EmployeeID As Decimal
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
        Dim rep As New PerformanceRepository
        Try
            Select Case CurrentState
                Case "ACTIVE"
                    For Each dr As GridDataItem In rgMngInfoEvalTarget.SelectedItems
                        'If Not rep.APPROVE_EVALUATE_TARGET(ID_REGGROUP, EmployeeID, EMP_ID, 1, String.Empty) Then
                        '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        '    rgMngInfoEvalTarget.Rebind()
                        '    CurrentState = CommonMessage.STATE_NORMAL
                        '    Exit Sub
                        'End If
                        Dim result = rep.PRI_PROCESS(LogHelper.CurrentUser.EMPLOYEE_ID, dr.GetDataKeyValue("EMPLOYEE"), dr.GetDataKeyValue("PE_PERIOD_ID"), 1, "PERFORMANCE", "", dr.GetDataKeyValue("ID"))
                    Next
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgMngInfoEvalTarget.Rebind()
                    CurrentState = CommonMessage.STATE_NORMAL

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
            BuildToolbar(Me.MainToolBar, ToolbarItem.View, ToolbarItem.Approve, ToolbarItem.Reject, ToolbarItem.Export, ToolbarItem.Sync)
            CType(MainToolBar.Items(0), RadToolBarButton).Text = "Xem"
            CType(MainToolBar.Items(4), RadToolBarButton).Text = "DS Chi tiết"
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
                Case CommonMessage.TOOLBARITEM_APPROVE
                    If rgMngInfoEvalTarget.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For idx = 0 To rgMngInfoEvalTarget.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMngInfoEvalTarget.SelectedItems(idx)
                        Dim status_id As Decimal = If(item.GetDataKeyValue("STATUS_ID") Is Nothing, 0, Decimal.Parse(item.GetDataKeyValue("STATUS_ID").ToString))
                        If status_id <> 0 Then
                            ShowMessage(Translate("Chỉ thực hiện phê duyệt trên các đơn đang trạng thái chờ phê duyệt"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If rep.ValidateSubmit(item.GetDataKeyValue("ID"), "APPROVE") Then
                            ShowMessage(Translate("Vui lòng đánh giá kết quả"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    CurrentState = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.MessageText = "Bạn có muốn phê duyệt"
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_REJECT
                    If rgMngInfoEvalTarget.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For idx = 0 To rgMngInfoEvalTarget.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMngInfoEvalTarget.SelectedItems(idx)
                        Dim status_id As Decimal = If(item.GetDataKeyValue("STATUS_ID") Is Nothing, 0, Decimal.Parse(item.GetDataKeyValue("STATUS_ID").ToString))
                        If status_id <> 0 Then
                            ShowMessage(Translate("Chỉ thực hiện không phê duyệt trên các đơn đang trạng thái chờ phê duyệt"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If rep.ValidateSubmit(item.GetDataKeyValue("ID"), "APPROVE") Then
                            ShowMessage(Translate("Vui lòng đánh giá kết quả"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlCommon_Reject.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgMngInfoEvalTarget.ExportExcel(Server, Response, dtDatas, "APPROVE_EVALUATE_TARGET")
                        End If
                    End Using
            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = "ACTIVE"
            UpdateControlState()
        End If
    End Sub

    Private Sub ctrlCommon_Reject_ButtonCommand(ByVal sender As Object, ByVal e As CommandSaveEventArgs) Handles ctrlCommon_Reject.ButtonCommand
        Try
            Dim strComment As String = e.Comment
            'Dim ID_REGGROUP As Integer
            'Dim EMP_ID As Integer
            For Each dr As GridDataItem In rgMngInfoEvalTarget.SelectedItems
                'ID_REGGROUP = dr.GetDataKeyValue("ID")
                'EMP_ID = dr.GetDataKeyValue("EMPLOYEE")
                'If Not rep.APPROVE_EVALUATE_TARGET(ID_REGGROUP, EmployeeID, EMP_ID, 2, strComment) Then
                '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                '    rgMngInfoEvalTarget.Rebind()
                '    CurrentState = CommonMessage.STATE_NORMAL
                '    Exit Sub
                'End If

                Dim result = rep.PRI_PROCESS(LogHelper.CurrentUser.EMPLOYEE_ID, dr.GetDataKeyValue("EMPLOYEE"), dr.GetDataKeyValue("PE_PERIOD_ID"), 2, "PERFORMANCE", strComment, dr.GetDataKeyValue("ID"))
            Next
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            rgMngInfoEvalTarget.Rebind()
            CurrentState = CommonMessage.STATE_NORMAL

        Catch ex As Exception

        End Try
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

            If Request.Headers("NoSearch") IsNot Nothing Then
                _filter.STATUS_ID = 0
            Else
                If cboStatus.SelectedValue <> "" Then
                    _filter.STATUS_ID = cboStatus.SelectedValue
                End If
                If cboYear.SelectedValue <> "" Then
                    _filter.YEAR = cboYear.SelectedValue
                End If
                If cboPeriod.SelectedValue <> "" Then
                    _filter.PE_PERIOD_ID = cboPeriod.SelectedValue
                End If
            End If

            _filter.EMPLOYEE = LogHelper.CurrentUser.EMPLOYEE_ID
            Dim MaximumRows As Integer
            Dim Sorts As String = rgMngInfoEvalTarget.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.Get_Portal_Approve_Evaluate_Target(_filter, Sorts).ToTable()
                Else
                    Return rep.Get_Portal_Approve_Evaluate_Target(_filter).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    rgMngInfoEvalTarget.DataSource = rep.Get_Portal_Approve_Evaluate_Target(_filter, rgMngInfoEvalTarget.CurrentPageIndex, rgMngInfoEvalTarget.PageSize, MaximumRows, Sorts)
                Else
                    rgMngInfoEvalTarget.DataSource = rep.Get_Portal_Approve_Evaluate_Target(_filter, rgMngInfoEvalTarget.CurrentPageIndex, rgMngInfoEvalTarget.PageSize, MaximumRows)
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
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.UnApprovedByLM).ToString()).CopyToDataTable()
            FillRadCombobox(cboStatus, data, "NAME", "ID")
            cboStatus.SelectedValue = PortalStatus.WaitingForApproval
        End If
    End Sub
#End Region
End Class