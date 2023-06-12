Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI
Public Class ctrlMngInfoEvalTarget
    Inherits Common.CommonView
#Region "Property"

    Property DeleteKpiAssessment As KPI_ASSESSMENT_DTO
        Get
            Return ViewState(Me.ID & "_DeleteKpiAssessment")
        End Get
        Set(ByVal value As KPI_ASSESSMENT_DTO)
            ViewState(Me.ID & "_DeleteKpiAssessment") = value
        End Set
    End Property

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <summary>
    ''' Khởi tạo, load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Refresh()
            UpdateControlState()
            rgMngInfoEvalTarget.SetFilter()
            rgMngInfoEvalTarget.AllowCustomPaging = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo, load control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Load data cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim dtData As DataTable
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Using rep As New PerformanceRepository

            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo, load menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMngInfoEvalTarget
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Calculate, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Export,
                                       ToolbarItem.Sync,
                                       ToolbarItem.Next,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(5), RadToolBarButton).Text = Translate("DS chi tiết")
            CType(MainToolBar.Items(6), RadToolBarButton).Text = Translate("Cập nhật tỉ trọng")
            CType(MainToolBar.Items(6), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(2), RadToolBarButton).ImageUrl
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Update trạng thái page, trạng thái control, process event xóa dữ liệu
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New PerformanceRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            UpdateCotrolEnabled(False)
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    setButtonbarStatus(False)

                    UpdateCotrolEnabled(True)
                Case CommonMessage.STATE_DELETE
                    Dim objID As New List(Of Decimal)
                    For Each item In rgMngInfoEvalTarget.MasterTableView.GetSelectedItems
                        objID.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteKpiAssessment(objID) Then
                        DeleteKpiAssessment = Nothing
                        IDSelect = Nothing
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                    setButtonbarStatus(False)

            End Select
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub GetDataToCombo()
        Dim reps As New PerformanceStoreProcedure
        Dim rep As New PerformanceRepository
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
        dtTable = rep.GetOtherList("PROCESS_STATUS", "vi-VN", True)
        FillRadCombobox(cboStatus, dtTable, "NAME", "ID")
    End Sub
    ''' <summary>
    ''' Reset lại page theo trạng thái
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                GetDataToCombo()
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMngInfoEvalTarget.Rebind()
                        SelectedItemDataGridByKey(rgMngInfoEvalTarget, IDSelect, , rgMngInfoEvalTarget.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMngInfoEvalTarget.CurrentPageIndex = 0
                        rgMngInfoEvalTarget.MasterTableView.SortExpressions.Clear()
                        rgMngInfoEvalTarget.Rebind()
                        SelectedItemDataGridByKey(rgMngInfoEvalTarget, IDSelect)
                    Case "Cancel"
                        rgMngInfoEvalTarget.MasterTableView.ClearSelectedItems()
                End Select
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <summary>
    ''' Event click item trên menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_CANCEL
                    setButtonbarStatus(False)
                    For Each item As GridDataItem In rgMngInfoEvalTarget.MasterTableView.Items
                        Dim txtmark_offical As RadNumericTextBox = DirectCast(item("RATIO").FindControl("txtRATIO"), RadNumericTextBox)
                        txtmark_offical.Enabled = False
                    Next

                Case CommonMessage.TOOLBARITEM_SAVE
                    setButtonbarStatus(False)
                    Dim list As New List(Of KPI_ASSESSMENT_DTO)
                    Dim list_emp As New List(Of Decimal)

                    For Each item As GridDataItem In rgMngInfoEvalTarget.MasterTableView.Items
                        Dim obj As New KPI_ASSESSMENT_DTO
                        obj.ID = item.GetDataKeyValue("ID")
                        obj.RATIO = If(IsNumeric(TryCast(item.FindControl("txtRATIO"), RadNumericTextBox).Value), TryCast(item.FindControl("txtRATIO"), RadNumericTextBox).Value, Nothing)
                        list.Add(obj)

                        list_emp.Add(CDec(Val(item.GetDataKeyValue("EMPLOYEE"))))
                    Next

                    'list_emp = list_emp.Where(Function(x) list_emp.Where(Function(y) x = y).Count() > 1).ToList
                    list_emp = list_emp.Distinct.ToList
                    For Each Item In list_emp
                        Dim sum = 0
                        For Each item1 As GridDataItem In rgMngInfoEvalTarget.MasterTableView.Items
                            If Item = item1.GetDataKeyValue("EMPLOYEE") Then
                                sum = sum + If(IsNumeric(TryCast(item1.FindControl("txtRATIO"), RadNumericTextBox).Value), TryCast(item1.FindControl("txtRATIO"), RadNumericTextBox).Value, 0)
                            End If
                        Next
                        If sum <> 100 Then
                            ShowMessage("Tỷ trọng các mục tiêu đánh giá của từng NV phải bằng 100", NotifyType.Warning)
                            setButtonbarStatus(True)
                            For Each item2 As GridDataItem In rgMngInfoEvalTarget.MasterTableView.Items
                                Dim txtmark_offical As RadNumericTextBox = DirectCast(item2("RATIO").FindControl("txtRATIO"), RadNumericTextBox)
                                txtmark_offical.Enabled = True
                            Next
                            Exit Sub
                        End If
                    Next


                    Dim rep As New PerformanceRepository
                    If rep.SaveChangeRatio(list) Then
                        setButtonbarStatus(False)
                        For Each item As GridDataItem In rgMngInfoEvalTarget.MasterTableView.Items
                            Dim txtmark_offical As RadNumericTextBox = DirectCast(item("RATIO").FindControl("txtRATIO"), RadNumericTextBox)
                            txtmark_offical.Enabled = False
                        Next
                        ShowMessage("Cập nhật tỉ trọng thành công", NotifyType.Success)
                    End If
                Case CommonMessage.TOOLBARITEM_NEXT

                    setButtonbarStatus(True)
                    For Each item As GridDataItem In rgMngInfoEvalTarget.MasterTableView.Items
                        Dim txtmark_offical As RadNumericTextBox = DirectCast(item("RATIO").FindControl("txtRATIO"), RadNumericTextBox)
                        txtmark_offical.Enabled = True
                    Next

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgMngInfoEvalTarget.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARTIEM_CALCULATE
                    Dim rep As New PerformanceRepository
                    Dim _param = New ParamDTO With {.ORG_ID = ctrlOrg.CurrentValue,
                                                    .IS_DISSOLVE = ctrlOrg.IsDissolve,
                                                    .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)}
                    rep.CalKPI(_param)
                    Refresh("UpdateView")

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMngInfoEvalTarget.ExportExcel(Server, Response, dtData, "KPI_ASSESSMENT")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using
            End Select
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Sub setButtonbarStatus(ByVal isShow As Boolean)
        CType(MainToolBar.Items(7), RadToolBarButton).Enabled = isShow
        CType(MainToolBar.Items(8), RadToolBarButton).Enabled = isShow
        For i = 0 To 6
            CType(MainToolBar.Items(i), RadToolBarButton).Enabled = Not isShow
        Next
    End Sub
    ''' <summary>
    ''' Event Yes/No trên popup Message Hỏi xóa dữ liệu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event click button tìm kiếm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgMngInfoEvalTarget.CurrentPageIndex = 0
            rgMngInfoEvalTarget.MasterTableView.SortExpressions.Clear()
            rgMngInfoEvalTarget.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event Click item Node trên TreeView-Sơ đồ tổ chức
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgMngInfoEvalTarget.CurrentPageIndex = 0
            rgMngInfoEvalTarget.MasterTableView.SortExpressions.Clear()
            rgMngInfoEvalTarget.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Reload, load datasource cho grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMngInfoEvalTarget.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgMngInfoEvalTarget.ItemDataBound
        My.Application.ChangeCulture("en-US")
        'Dim startTime As DateTime = DateTime.UtcNow
        'Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Try
        '    If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
        '        Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
        '        datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
        '    End If
        '    _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing)
        'Catch ex As Exception
        '    _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex.ToString)
        'End Try

    End Sub

#End Region

#Region "Custom"
    ''' <summary>
    ''' Update trạng thái của control
    ''' </summary>
    ''' <param name="bCheck"></param>
    ''' <remarks></remarks>
    Private Sub UpdateCotrolEnabled(ByVal bCheck As Boolean)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ctrlOrg.Enabled = Not bCheck
            Utilities.EnabledGrid(rgMngInfoEvalTarget, Not bCheck, False)
            EnableRadDatePicker(rdFromDate, Not bCheck)
            EnableRadDatePicker(rdToDate, Not bCheck)

            btnSearch.Enabled = Not bCheck
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Load data from DB to grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New KPI_ASSESSMENT_DTO
        Dim rep As New PerformanceRepository
        Dim bCheck As Boolean = False
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgMngInfoEvalTarget.DataSource = New List(Of KPI_ASSESSMENT_DTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                          .IS_DISSOLVE = ctrlOrg.IsDissolve}
            SetValueObjectByRadGrid(rgMngInfoEvalTarget, _filter)
            If cboYear.Text <> "" Then
                _filter.YEAR = cboYear.SelectedValue
            End If
            If cboStatus.Text <> "" Then
                _filter.STATUS_ID = cboStatus.SelectedValue
            End If
            If cboPeriod.Text <> " " Then
                _filter.PE_PERIOD_ID = cboPeriod.SelectedValue
            End If

            _filter.MULTI_CRITERIA = chkMultiCriteria.Checked

            Dim MaximumRows As Integer
            Dim Sorts As String = rgMngInfoEvalTarget.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetKpiAssessment(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetKpiAssessment(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    rgMngInfoEvalTarget.DataSource = rep.GetKpiAssessment(_filter, rgMngInfoEvalTarget.CurrentPageIndex,
                                                     rgMngInfoEvalTarget.PageSize, MaximumRows, _param, Sorts)
                Else
                    rgMngInfoEvalTarget.DataSource = rep.GetKpiAssessment(_filter, rgMngInfoEvalTarget.CurrentPageIndex,
                                                     rgMngInfoEvalTarget.PageSize, MaximumRows, _param)
                End If

                rgMngInfoEvalTarget.VirtualItemCount = MaximumRows
            End If
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

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

End Class