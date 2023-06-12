Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlPE_PortalHTCHAssessmentDetail
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Performance\Modules\Portal" + Me.GetType().Name.ToString()
#Region "Property"
    Property dtPeriod As DataTable
        Get
            Return ViewState(Me.ID & "_dtPeriod")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtPeriod") = value
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
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim rep As New PerformanceRepository
                    Dim objID As New List(Of Decimal)
                    For Each item In rgMngInfoEvalTarget.MasterTableView.GetSelectedItems
                        objID.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteHTCHAssessment(objID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMngInfoEvalTarget.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                    CurrentState = CommonMessage.STATE_NORMAL
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
            BuildToolbar(Me.MainToolBar, ToolbarItem.Calculate,
                                         ToolbarItem.ExportTemplate,
                                         ToolbarItem.Import,
                                         ToolbarItem.Export)
            MainToolBar.Items(0).Text = Translate("Tổng hợp")
            MainToolBar.Items(1).Text = Translate("Xuất file mẫu")
            MainToolBar.Items(2).Text = Translate("Nhập file mẫu")
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
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgMngInfoEvalTarget.ExportExcel(Server, Response, dtDatas, "HTCH Assessment Detail")
                        End If
                    End Using

                Case CommonMessage.TOOLBARTIEM_CALCULATE
                    'If ctrlOrganization.CheckedValues.Count = 0 Then
                    '    ShowMessage(Translate("Chưa chọn phòng ban !"), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    'If cboPeriod.SelectedValue = "" Then
                    '    ShowMessage(Translate("Chưa chọn kỳ đánh giá !"), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    'Dim lstOrg = ctrlOrganization.CheckedValueKeys
                    'Dim rep As New PerformanceRepository
                    'If rep.CAL_HTCH_ASSESSMENT(lstOrg, cboYear.SelectedValue, cboPeriod.SelectedValue) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    '    rgMngInfoEvalTarget.Rebind()
                    'Else
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    '    Exit Sub
                    'End If

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
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
    End Sub

    Protected Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        ClearControlValue(rdFromDate, rdToDate)
        cboPeriod.Items.Clear()
        If cboYear.SelectedValue <> "" Then
            Dim rep As New PerformanceRepository
            dtPeriod = rep.GetPeriodHTCHByYear(cboYear.SelectedValue)
            FillRadCombobox(cboPeriod, dtPeriod, "NAME", "ID", True)
            If cboPeriod.SelectedValue <> "" Then
                Dim xRow = (From p In dtPeriod Where Not IsDBNull(p("ID")) AndAlso p("ID") = cboPeriod.SelectedValue).FirstOrDefault
                If Not IsNothing(xRow) Then
                    If Not IsDBNull(xRow("START_DATE")) Then
                        rdFromDate.SelectedDate = CDate(xRow("START_DATE"))
                    End If
                    If Not IsDBNull(xRow("END_DATE")) Then
                        rdToDate.SelectedDate = CDate(xRow("END_DATE"))
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim rep As New PerformanceRepository
        ClearControlValue(rdToDate, rdFromDate)
        If cboPeriod.SelectedValue <> "" Then
            Dim xRow = (From p In dtPeriod Where Not IsDBNull(p("ID")) AndAlso p("ID") = cboPeriod.SelectedValue).FirstOrDefault
            If Not IsNothing(xRow) Then
                If Not IsDBNull(xRow("START_DATE")) Then
                    rdFromDate.SelectedDate = CDate(xRow("START_DATE"))
                End If
                If Not IsDBNull(xRow("END_DATE")) Then
                    rdToDate.SelectedDate = CDate(xRow("END_DATE"))
                End If
            End If

        End If
    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New PE_HTCH_ASSESSMENT_DTL_DTO
        Dim rep As New PerformanceRepository
        Dim bCheck As Boolean = False
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim lstOrg = ctrlOrganization.CheckedValueKeys
            SetValueObjectByRadGrid(rgMngInfoEvalTarget, _filter)
            If cboYear.Text <> "" Then
                _filter.YEAR = cboYear.SelectedValue
            End If
            If cboPeriod.Text <> "" Then
                _filter.PERIOD_ID = cboPeriod.SelectedValue
            End If
            If cboStatus.SelectedValue <> "" Then
                _filter.STATUS_ID = cboStatus.SelectedValue
            End If
            Dim MaximumRows As Integer
            Dim Sorts As String = rgMngInfoEvalTarget.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetHTCHAssessmentDetail(_filter, lstOrg, Sorts).ToTable()
                Else
                    Return rep.GetHTCHAssessmentDetail(_filter, lstOrg).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    rgMngInfoEvalTarget.DataSource = rep.GetHTCHAssessment_Detail(_filter, lstOrg, rgMngInfoEvalTarget.CurrentPageIndex, rgMngInfoEvalTarget.PageSize, MaximumRows, Sorts)
                Else
                    rgMngInfoEvalTarget.DataSource = rep.GetHTCHAssessment_Detail(_filter, lstOrg, rgMngInfoEvalTarget.CurrentPageIndex, rgMngInfoEvalTarget.PageSize, MaximumRows)
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
        Dim dtTable As New DataTable
        dtTable = rep.GetYearHTCH(True)
        FillRadCombobox(cboYear, dtTable, "YEAR", "YEAR")
        cboYear.SelectedValue = Date.Now.Year
        If cboYear.SelectedValue <> "" Then
            dtPeriod = rep.GetPeriodHTCHByYear(cboYear.SelectedValue)
            FillRadCombobox(cboPeriod, dtPeriod, "NAME", "ID", True)
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
            cboStatus.SelectedValue = PortalStatus.WaitingForApproval
        End If
    End Sub
#End Region

End Class