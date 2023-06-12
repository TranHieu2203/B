Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI
Public Class ctrlPE_EmployeesRecommend
    Inherits Common.CommonView
    Private psp As New CommonRepository
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
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
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
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Calculate,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Export, ToolbarItem.SendMail)

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("CAL_RESULT",
                                                                  ToolbarIcons.Calculator,
                                                                  ToolbarAuthorize.Special1,
                                                                  "Tổng hợp KQ"))
            MainToolBar.Items(0).Text = Translate("Tổng hợp DS")

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

                    UpdateCotrolEnabled(True)
                Case CommonMessage.STATE_DELETE
                    Dim objID As New List(Of Decimal)
                    For Each item In rgData.MasterTableView.GetSelectedItems
                        objID.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteEmployeeHTCHPeriod(objID) Then
                        Refresh("UpdateView")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub GetDataToCombo()
        Dim reps As New PerformanceStoreProcedure
        Dim dtTable As New DataTable
        dtTable = reps.GetYear2(False)
        FillRadCombobox(cboYear, dtTable, "YEAR", "ID")
        cboYear.SelectedValue = Date.Now.Year
        If cboYear.SelectedValue <> "" Then
            Dim rep As New PerformanceRepository
            FillRadCombobox(cboPeriod, rep.GetPeriodByYear2(cboYear.SelectedValue), "NAME", "ID", True)
            If cboPeriod.SelectedValue <> "" Then
                Dim table As DataTable
                table = rep.GetDateByPeriod2(cboPeriod.SelectedValue)
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
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect, , rgData.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect)
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
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
        Dim store As New PerformanceStoreProcedure
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARTIEM_CALCULATE
                    Dim rep As New PerformanceRepository
                    Dim _org_id = ctrlOrg.CheckedValueKeys()
                    If _org_id.Count = 0 Then
                        ShowMessage(Translate("Chưa chọn phòng ban"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If cboPeriod.SelectedValue = "" Then
                        ShowMessage(Translate("Chưa chọn kỳ đánh giá"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rep.CAL_EMPLOYEEHTCH_PERIOD(_org_id, cboPeriod.SelectedValue) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If

                Case "CAL_RESULT"
                    Dim rep As New PerformanceRepository
                    Dim _org_id = ctrlOrg.CheckedValueKeys()
                    If _org_id.Count = 0 Then
                        ShowMessage(Translate("Chưa chọn phòng ban"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If cboPeriod.SelectedValue = "" Then
                        ShowMessage(Translate("Chưa chọn kỳ đánh giá"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rep.CAL_EMP_RECOMEND_RESULT(_org_id, cboPeriod.SelectedValue) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "Employee_Period")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_SENDMAIL
                    Dim defaultFrom As String = ""
                    Dim titleMail As String = ""
                    Dim dataMail As DataTable
                    Dim dtValues As DataTable
                    Dim dtValuesDetail As DataTable

                    Dim config As Dictionary(Of String, String)
                    config = psp.GetConfig(0)
                    defaultFrom = If(config.ContainsKey("MailFrom"), config("MailFrom"), "")
                    If defaultFrom = "" Then
                        ShowMessage(Translate("Gửi mail thất bại"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    dataMail = store.GET_MAIL_TEMPLATE("HTCH_THONGBAO_DG", "Performance")
                    If dataMail.Rows.Count = 0 Then
                        ShowMessage(Translate("Mẫu mail có mã 'HTCH_THONGBAO_DG' không tồn tại"), NotifyType.Warning)
                        Exit Sub
                    End If
                    titleMail = dataMail.Rows(0)("TITLE").ToString
                    Dim err As Boolean = True
                    Dim lstManager As New List(Of Decimal)
                    For Each itemSelect As GridDataItem In rgData.SelectedItems
                        Dim manager_id As Decimal = Decimal.Parse(itemSelect.GetDataKeyValue("DIRECT_MANAGER"))
                        If Not lstManager.Contains(manager_id) Then
                            lstManager.Add(manager_id)
                        End If
                    Next
                    For Each mangerId As Decimal In lstManager
                        Dim lstEmp = ""
                        Dim period_id As Decimal
                        Dim mail As String
                        Dim send_mail As Boolean
                        For Each itemSelect As GridDataItem In rgData.SelectedItems
                            Dim manager_Id As Decimal = Decimal.Parse(itemSelect.GetDataKeyValue("DIRECT_MANAGER"))
                            If manager_Id = mangerId Then
                                period_id = Decimal.Parse(itemSelect.GetDataKeyValue("PE_PERIOD_ID"))
                                mail = If(itemSelect.GetDataKeyValue("EMAIL") Is Nothing, String.Empty, itemSelect.GetDataKeyValue("EMAIL").ToString)
                                send_mail = itemSelect.GetDataKeyValue("SEND_MAIL")
                                If mail = "" Then
                                    ShowMessage(Translate("Tồn tại QLTT chưa có email"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                If (Not String.IsNullOrEmpty(mail)) And (Not send_mail) Then
                                    If lstEmp = "" Then
                                        lstEmp = itemSelect.GetDataKeyValue("EMPLOYEE_ID").ToString
                                    Else
                                        lstEmp = lstEmp + ", " + itemSelect.GetDataKeyValue("EMPLOYEE_ID").ToString
                                    End If
                                End If
                            End If
                        Next
                        If (Not String.IsNullOrEmpty(mail)) And (Not send_mail) Then
                            Dim body As String = dataMail.Rows(0)("CONTENT").ToString
                            dtValues = store.GET_MAIL_INFORMATION_OF_PE_EMPLOYEEHTCH_PERIOD(lstEmp, mangerId, period_id)
                            body = Replace(body, "MANAGER_NAME", dtValues.Rows(0)("MANAGER_NAME").ToString())
                            body = Replace(body, "NHANVIEN_LIST", dtValues.Rows(0)("NHANVIEN_LIST").ToString())
                            body = Replace(body, "YEAR", dtValues.Rows(0)("YEAR").ToString())
                            body = Replace(body, "PERIOD_NAME", dtValues.Rows(0)("PERIOD_NAME").ToString())
                            body = Replace(body, "START_DATE", DateTime.Parse(dtValues.Rows(0)("START_DATE").ToString()).ToString("dd/MM/yyyy"))
                            body = Replace(body, "END_DATE", DateTime.Parse(dtValues.Rows(0)("END_DATE").ToString()).ToString("dd/MM/yyyy"))
                            If psp.InsertMail(defaultFrom, mail, titleMail, body, "", "", "", True) Then
                                store.UPDATE_STATUS_SEND_MAIL_HTCH(lstEmp, period_id, 1)
                            End If
                        End If
                    Next
                    rgData.Rebind()
            End Select
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
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
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
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
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

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
            Utilities.EnabledGrid(rgData, Not bCheck, False)
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
        Dim _filter As New EmployeePeriodDTO
        Dim rep As New PerformanceRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim lstOrg = ctrlOrg.CheckedValueKeys

            SetValueObjectByRadGrid(rgData, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            If cboPeriod.SelectedValue <> "" Then
                _filter.PE_PERIOD_ID = cboPeriod.SelectedValue
            End If
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetEmployeePeriodHCTH(_filter, 0, Integer.MaxValue, 0, lstOrg, Sorts).ToTable
                Else
                    Return rep.GetEmployeePeriodHCTH(_filter, 0, Integer.MaxValue, 0, lstOrg).ToTable
                End If
            Else
                Dim EmpList As New List(Of EmployeePeriodDTO)
                If Sorts IsNot Nothing Then
                    EmpList = rep.GetEmployeePeriodHCTH(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, lstOrg, Sorts)
                Else
                    EmpList = rep.GetEmployeePeriodHCTH(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, lstOrg)
                End If
                rgData.VirtualItemCount = MaximumRows
                rgData.DataSource = EmpList
            End If
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Protected Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        If cboYear.SelectedValue <> "" Then
            Dim rep As New PerformanceRepository
            FillRadCombobox(cboPeriod, rep.GetPeriodByYear2(cboYear.SelectedValue), "NAME", "ID", True)
            If cboPeriod.SelectedValue <> "" Then
                Dim table As DataTable
                table = rep.GetDateByPeriod2(cboPeriod.SelectedValue)
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
            table = rep.GetDateByPeriod2(cboPeriod.SelectedValue)
            rdFromDate.SelectedDate = If(table.Rows(0)(0).ToString() = "", Nothing, table.Rows(0)(0))
            rdToDate.SelectedDate = If(table.Rows(0)(1).ToString() = "", Nothing, table.Rows(0)(1))
        Else
            rdFromDate.SelectedDate = Nothing
            rdToDate.SelectedDate = Nothing
        End If
    End Sub

#End Region

End Class