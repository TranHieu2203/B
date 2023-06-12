Imports Common
Imports Common.CommonBusiness
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_Salary_Detention
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Payroll/Module/Payroll/Business/" + Me.GetType().Name.ToString()
    Dim log As New UserLog
#Region "Properties"
    Public Property SalaryDetention As List(Of PA_Salary_DetentionDTO)
        Get
            Return ViewState(Me.ID & "_SalaryDetention_DTO")
        End Get
        Set(ByVal value As List(Of PA_Salary_DetentionDTO))
            ViewState(Me.ID & "_SalaryDetention_DTO") = value
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
        If Not Page.IsPostBack Then
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Dim startTime As DateTime = DateTime.UtcNow
            Try
                ctrlOrganization.AutoPostBack = True
                ctrlOrganization.LoadDataAfterLoaded = True
                ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
                ctrlOrganization.CheckBoxes = TreeNodeTypes.None

                Refresh()
                _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
                DisplayException(Me.ViewName, Me.ID, ex)
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Khởi tạo, load control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            SetGridFilter(rgSalaryDetention)
            rgSalaryDetention.AllowCustomPaging = True
            rgSalaryDetention.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Khởi tạo load control, menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Export, ToolbarItem.Sync, ToolbarItem.Delete)
            MainToolBar.Items(3).Text = Translate("Cập nhật")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load data to combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository
        Try
            Dim lsData As New DataTable
            Dim lsData1 As New DataTable
            lsData = rep.GetPeriod(1)
            lsData1 = rep.GetPeriod(-1)

            FillRadCombobox(cboYear1, lsData1, "ID", "ID", )
            FillRadCombobox(cboYear2, lsData1, "ID", "ID", True)
            FillRadCombobox(cboYear3, lsData1, "ID", "ID", True)

            FillRadCombobox(cboPayMonthSearch, lsData, "PERIOD_T", "ID", True)
            FillRadCombobox(cboPeriodT, lsData, "PERIOD_T", "ID", True)
            FillRadCombobox(cboPayMonth, lsData, "PERIOD_T", "ID", True)

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Reload page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgSalaryDetention.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgSalaryDetention.CurrentPageIndex = 0
                        rgSalaryDetention.MasterTableView.SortExpressions.Clear()
                        rgSalaryDetention.Rebind()
                    Case "Cancel"
                        rgSalaryDetention.MasterTableView.ClearSelectedItems()
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

#End Region

#Region "Event"
    ''' <summary>
    ''' Event SeletedNodeChanged sơ đồ tổ chức
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Try
            rgSalaryDetention.CurrentPageIndex = 0
            rgSalaryDetention.MasterTableView.SortExpressions.Clear()
            rgSalaryDetention.Rebind()
        Catch ex As Exception

        End Try

    End Sub
    ''' <summary>
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository
        Try

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_DELETE
                    If rgSalaryDetention.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgSalaryDetention.ExportExcel(Server, Response, dtDatas, "DataSalaryDetention")
                        End If
                    End Using
                Case TOOLBARITEM_SYNC
                    Dim lstSalaryDetention As New List(Of PA_Salary_DetentionDTO)
                    Try
                        For Each row As GridDataItem In rgSalaryDetention.SelectedItems
                            Dim salaryDetention As New PA_Salary_DetentionDTO
                            salaryDetention.NOTE = row("NOTE").Text
                            salaryDetention.PAY_MONTH = Convert.ToDecimal(row("PAY_MONTH").Text)
                            salaryDetention.ID = row.GetDataKeyValue("ID")
                            salaryDetention.EMPLOYEE_ID = row.GetDataKeyValue("EMPLOYEE_ID")
                            salaryDetention.PERIOD_ID = row.GetDataKeyValue("PERIOD_ID")
                            salaryDetention.IS_DETENTION = row.GetDataKeyValue("IS_DETENTION")
                            lstSalaryDetention.Add(salaryDetention)
                        Next
                        rep.MODIFY_PA_SALARY_DETENTION(lstSalaryDetention)
                        _myLog.WriteLog(_myLog._info, _classPath, method,
                                                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
                    Catch ex As Exception
                        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
                        DisplayException(Me.ViewName, Me.ID, ex)
                    End Try
                    rep.Dispose()
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Xử lý sự kiện command khi click button yes/no
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim lstID As New List(Of Decimal)
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New PayrollRepository
                For Each row As GridDataItem In rgSalaryDetention.SelectedItems
                    lstID.Add(row.GetDataKeyValue("ID"))
                Next

                If rep.DELETE_PA_SALARY_DETENTION(lstID) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Refresh("UpdateView")
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    Exit Sub
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Reload grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgSalaryDetention_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgSalaryDetention.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            CreateDataFilter(False)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
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
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository
        Dim filter As New PA_Salary_DetentionDTO
        Dim MaximumRows As Integer
        Try
            rgSalaryDetention.CurrentPageIndex = 0
            rgSalaryDetention.MasterTableView.SortExpressions.Clear()
            rgSalaryDetention.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub btnQuickFulfill_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnQuickFulfill.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            For Each row As GridDataItem In rgSalaryDetention.SelectedItems
                row("NOTE").Text = txtNote.Text
                row("PAY_MONTH").Text = cboPayMonthSearch.SelectedValue
                row("PAY_MONTH_NAME").Text = cboPayMonthSearch.Text
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    ''' <summary>
    ''' Load data to grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository
        Dim obj As New PA_Salary_DetentionDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgSalaryDetention, obj)
            Dim Sorts As String = rgSalaryDetention.MasterTableView.SortExpressions.GetSortString()
            If ctrlOrganization.CurrentValue IsNot Nothing Then
                obj.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            Else
                rgSalaryDetention.DataSource = New List(Of PA_Salary_DetentionDTO)
                Exit Function
            End If

            Dim _param = New Payroll.PayrollBusiness.ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                                    .IS_DISSOLVE = ctrlOrganization.IsDissolve}

            If cboPayMonth.SelectedValue <> "" Then
                obj.PAY_MONTH = cboPayMonth.SelectedValue
            End If
            If cboPeriodT.SelectedValue <> "" Then
                obj.PERIOD_ID = cboPeriodT.SelectedValue
            End If

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GET_PA_SALARY_DETENTION(obj, _param, Sorts).ToTable()
                Else
                    Return rep.GET_PA_SALARY_DETENTION(obj, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.SalaryDetention = rep.GET_PA_SALARY_DETENTION(obj, rgSalaryDetention.CurrentPageIndex, rgSalaryDetention.PageSize, MaximumRows, _param, Sorts)
                Else
                    Me.SalaryDetention = rep.GET_PA_SALARY_DETENTION(obj, rgSalaryDetention.CurrentPageIndex, rgSalaryDetention.PageSize, MaximumRows, _param)
                End If

                rgSalaryDetention.VirtualItemCount = MaximumRows
                rgSalaryDetention.DataSource = Me.SalaryDetention
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
#End Region
    Private Sub cboYear1_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear1.SelectedIndexChanged
        Dim rep As New PayrollRepository
        Dim lsData As New DataTable
        Dim value As Decimal
        If cboYear1.SelectedValue <> "" Then
            value = CDec(cboYear1.SelectedValue)
        Else
            Exit Sub
        End If
        lsData = rep.GetPeriod(value)
        FillRadCombobox(cboPeriodT, lsData, "PERIOD_T", "ID", False)
    End Sub

    Private Sub cboYear2_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear2.SelectedIndexChanged
        Dim rep As New PayrollRepository
        Dim lsData As New DataTable
        Dim value As Decimal
        If cboYear2.SelectedValue <> "" Then
            value = CDec(cboYear2.SelectedValue)
        Else
            Exit Sub
        End If
        lsData = rep.GetPeriod(value)
        FillRadCombobox(cboPayMonth, lsData, "PERIOD_T", "ID", False)
    End Sub

    Private Sub cboYear3_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear3.SelectedIndexChanged
        Dim rep As New PayrollRepository
        Dim lsData As New DataTable
        Dim value As Decimal
        If cboYear3.SelectedValue <> "" Then
            value = CDec(cboYear3.SelectedValue)
        Else
            Exit Sub
        End If
        lsData = rep.GetPeriod(value)
        FillRadCombobox(cboPayMonthSearch, lsData, "PERIOD_T", "ID", False)
    End Sub

End Class