Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Common
Imports Telerik.Web.UI
Imports Aspose.Words

Public Class ctrlSeniorityMonthly
    Inherits Common.CommonView

#Region "Property"

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            UpdateControlState()
            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrg.CheckBoxes = TreeNodeTypes.None
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarTerminates
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Calculate,
                                       ToolbarItem.Export)
            MainToolBar.Items(0).Text = Translate("Tổng hợp")

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim rep As New PayrollRepository
        Dim objPeriod As List(Of ATPeriodDTO)
        Try
            cboPeriod.ClearSelection()
            objPeriod = rep.GetPeriodbyYear(cboYear.SelectedValue)
            cboPeriod.DataSource = objPeriod
            cboPeriod.DataValueField = "ID"
            cboPeriod.DataTextField = "PERIOD_NAME"
            cboPeriod.DataBind()
            Dim lst = (From s In objPeriod Where s.MONTH = Date.Now.Month).FirstOrDefault
            If Not lst Is Nothing Then
                cboPeriod.SelectedValue = lst.ID
            Else
                cboPeriod.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARTIEM_CALCULATE
                    Dim _param = New PA_ParamDTO With {.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue), _
                                                    .ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                                    .STATUS = 1,
                                                    .IS_DISSOLVE = ctrlOrg.IsDissolve}
                    Using rep As New PayrollRepository
                        If rep.IS_PERIOD_COLEXSTATUS(_param) Then
                            ShowMessage(Translate("Tồn tại kỳ công đang mở. Thao tác thực hiện không thành công"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    Using rep As New PayrollRepository
                        Dim _filter = New PASeniorityMonthlyDTO With {.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue), _
                                                                     .ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                                                     .IS_DISSOLVE = ctrlOrg.IsDissolve}
                        If rep.CalSeniorityMonthly(_filter) Then
                            ShowMessage("Tổng hợp thưởng thâm niên thành công.", NotifyType.Success)
                            rgData.Rebind()
                        Else
                            ShowMessage("Tổng hợp thưởng thâm niên không thành công.", NotifyType.Warning)
                        End If

                    End Using

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtDatas, "SeniorityMonthly")
                        End If
                    End Using

            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand

        Try
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rgData_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        If Not IsPostBack Then
            If cboPeriod.SelectedValue <> "" Then
                ctrlOrg.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.PA)
            End If
        End If
        Dim _param = New PA_ParamDTO With {.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue), _
                                        .ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                        .STATUS = 0,
                                        .IS_DISSOLVE = ctrlOrg.IsDissolve}

        Using rep As New PayrollRepository
            If rep.IS_PERIOD_COLEXSTATUS(_param) Then
                _param.STATUS = 1
                If rep.IS_PERIODSTATUS(_param) Then
                    MainToolBar.Items(0).Enabled = True
                Else
                    MainToolBar.Items(0).Enabled = False
                End If
            Else
                MainToolBar.Items(0).Enabled = False
            End If
        End Using

        rgData.Rebind()
    End Sub

    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriod.SelectedIndexChanged
        If cboPeriod.SelectedValue <> "" Then
            ctrlOrg.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.PA)

            Dim _param = New PA_ParamDTO With {.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue), _
                                            .ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .STATUS = 0,
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

            Using rep As New PayrollRepository
                If rep.IS_PERIOD_COLEXSTATUS(_param) Then
                    _param.STATUS = 1
                    If rep.IS_PERIODSTATUS(_param) Then
                        MainToolBar.Items(0).Enabled = True
                    Else
                        MainToolBar.Items(0).Enabled = False
                    End If
                Else
                    MainToolBar.Items(0).Enabled = False
                End If
            End Using
        End If
        rgData.Rebind()
    End Sub

#End Region

#Region "Custom"

    Protected Sub GetDataCombo()
        Dim rep As New PayrollRepository
        Dim objPeriod As List(Of ATPeriodDTO)
        Dim id As Integer = 0
        Try
            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2015 To Date.Now.Year + 1
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year
            objPeriod = rep.GetPeriodbyYear(cboYear.SelectedValue)
            cboPeriod.DataSource = objPeriod
            cboPeriod.DataValueField = "ID"
            cboPeriod.DataTextField = "PERIOD_NAME"
            cboPeriod.DataBind()
            Dim lst = (From s In objPeriod Where s.MONTH = Date.Now.Month).FirstOrDefault
            If Not lst Is Nothing Then
                cboPeriod.SelectedValue = lst.ID
            Else
                cboPeriod.SelectedIndex = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New PayrollRepository
        Dim bCheck As Boolean = False
        Dim total As Integer = 0
        Dim _filter As New PASeniorityMonthlyDTO
        Try
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            SetValueObjectByRadGrid(rgData, _filter)
            _filter.ORG_ID = Utilities.ObjToDecima(ctrlOrg.CurrentValue)
            _filter.PERIOD_ID = Utilities.ObjToDecima(cboPeriod.SelectedValue)
            _filter.IS_DISSOLVE = ctrlOrg.IsDissolve
            If Not isFull Then

                Dim lst As List(Of PASeniorityMonthlyDTO)
                If Sorts <> "" Then
                    lst = rep.GetSeniorityMonthly(_filter,
                                                         rgData.MasterTableView.CurrentPageIndex,
                                                         rgData.MasterTableView.PageSize, total, Sorts)
                Else

                    lst = rep.GetSeniorityMonthly(_filter,
                                                         rgData.MasterTableView.CurrentPageIndex,
                                                         rgData.MasterTableView.PageSize, total)

                End If
                rgData.VirtualItemCount = total
                rgData.DataSource = lst
            Else
                If Sorts <> "" Then
                    Return rep.GetSeniorityMonthly(_filter, Sorts).ToTable
                Else
                    Return rep.GetSeniorityMonthly(_filter).ToTable
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class