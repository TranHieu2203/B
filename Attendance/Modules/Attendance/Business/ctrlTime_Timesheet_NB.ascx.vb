Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Public Class ctrlTime_Timesheet_NB
    Inherits Common.CommonView
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
#Region "Property"

    Private Property PERIOD As List(Of AT_PERIODDTO)
        Get
            Return ViewState(Me.ID & "_PERIOD")
        End Get
        Set(ByVal value As List(Of AT_PERIODDTO))
            ViewState(Me.ID & "_PERIOD") = value
        End Set
    End Property

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlOrganization.AutoPostBack = True
            ctrlOrganization.LoadDataAfterLoaded = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckBoxes = TreeNodeTypes.None
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            SetGridFilter(rgData)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgData.AllowCustomPaging = True
            rgData.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Calculate, ToolbarItem.Export)
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim lsData As List(Of AT_PERIODDTO)
            Dim rep As New AttendanceRepository
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
            Dim period As New AT_PERIODDTO
            period.ORG_ID = 1
            period.YEAR = Date.Now.Year
            lsData = rep.LOAD_PERIODBylinq(period)
            Me.PERIOD = lsData
            FillRadCombobox(cboPeriod, lsData, "PERIOD_NAME", "PERIOD_ID", True)
            If lsData.Count > 0 Then
                Dim periodid = (From d In lsData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                Else
                    cboPeriod.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        If Not IsPostBack Then
            ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
        End If
        rgData.CurrentPageIndex = 0
        rgData.Rebind()
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim rep As New AttendanceRepository
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue), _
                                            .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve}
            ' kiem tra ky cong da dong chua?

            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case TOOLBARTIEM_CALCULATE
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện"), NotifyType.Error)
                        Exit Sub
                    End If
                    Dim lsEmployee As New List(Of Decimal?)
                    If rep.Cal_TimeTImesheet_NB(_param, Decimal.Parse(cboPeriod.SelectedValue),
                                                   Decimal.Parse(ctrlOrganization.CurrentValue), lsEmployee) Then
                        Refresh("UpdateView")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                        Exit Sub
                    End If

                    UpdateToolbarState()
                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtDatas, "TimeSheet_NB")
                        End If
                    End Using
            End Select
            ' 
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False)
        Dim rep As New AttendanceRepository
        Dim obj As New AT_TIME_TIMESHEET_NBDTO
        Dim startdate As Date '= CType("01/01/2016", Date)
        Dim enddate As Date '= CType("31/01/2016", Date)
        Try
            If Not String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                Dim objperiod As New AT_PERIODDTO
                objperiod.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)
                obj.PERIOD_ID = objperiod.PERIOD_ID
                Dim ddate = rep.LOAD_PERIODByID(objperiod)
                startdate = ddate.START_DATE
                enddate = ddate.END_DATE
            Else
                ShowMessage(Translate("Kỳ công chưa được chọn"), Utilities.NotifyType.Warning)
                Exit Function
            End If
            For i = 1 To 31
                If startdate <= enddate Then
                    rgData.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM")
                    rgData.MasterTableView.GetColumn("D" & i).Visible = True
                    rgData.MasterTableView.GetColumn("D" & i).HeaderStyle.CssClass = "Removecss3"
                    If startdate.DayOfWeek = DayOfWeek.Sunday Then
                        rgData.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> CN"
                        rgData.MasterTableView.GetColumn("D" & i).HeaderStyle.CssClass = "css3"
                    ElseIf startdate.DayOfWeek = DayOfWeek.Monday Then
                        rgData.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T2"
                    ElseIf startdate.DayOfWeek = DayOfWeek.Tuesday Then
                        rgData.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T3"
                    ElseIf startdate.DayOfWeek = DayOfWeek.Wednesday Then
                        rgData.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T4"
                    ElseIf startdate.DayOfWeek = DayOfWeek.Thursday Then
                        rgData.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T5"
                    ElseIf startdate.DayOfWeek = DayOfWeek.Friday Then
                        rgData.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T6"
                    ElseIf startdate.DayOfWeek = DayOfWeek.Saturday Then
                        rgData.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T7"
                    End If
                    startdate = startdate.AddDays(1)
                Else
                    rgData.MasterTableView.GetColumn("D" & i).Visible = False
                End If
            Next

            Dim MaximumRows As Integer = 0
            SetValueObjectByRadGrid(rgData, obj)
            Dim _param = New ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue, _
                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve,
                                            .IS_FULL = True}
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            obj.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)
            obj.PAGE_INDEX = rgData.CurrentPageIndex
            obj.PAGE_SIZE = rgData.PageSize
            obj.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            If obj.PAGE_INDEX = 0 Then
                obj.PAGE_INDEX = 1
            End If
            Dim ds As DataSet
            ds = rep.GetSummaryNB(obj)
            If Not isFull Then
                If ds IsNot Nothing Then
                    Dim tableCct = ds.Tables(0)
                    rgData.VirtualItemCount = Decimal.Parse(ds.Tables(1).Rows(0)("TOTAL"))
                    rgData.DataSource = tableCct
                Else
                    rgData.DataSource = New DataTable
                End If
            Else
                Return ds.Tables(0)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim dtData As List(Of AT_PERIODDTO)
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        period.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
        period.YEAR = Decimal.Parse(cboYear.SelectedValue)
        dtData = rep.LOAD_PERIODBylinq(period)
        cboPeriod.ClearSelection()
        FillRadCombobox(cboPeriod, dtData, "PERIOD_NAME", "PERIOD_ID", True)
        If dtData.Count > 0 Then
            Dim periodid = (From d In dtData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
            If periodid IsNot Nothing Then
                cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                rgData.Rebind()
            Else
                cboPeriod.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub cboPeriod_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged
        rgData.Rebind()
        ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter(False)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgData.PageIndexChanged
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Try
            Dim url = e.Argument
            If (url.Contains("reload=1")) Then
                rgData.CurrentPageIndex = 0
                rgData.Rebind()
                If rgData.Items IsNot Nothing AndAlso rgData.Items.Count > 0 Then
                    rgData.Items(0).Selected = True
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
        End If
    End Sub
#End Region

#Region "CUSTOM"

    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_CHECK

            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region
End Class