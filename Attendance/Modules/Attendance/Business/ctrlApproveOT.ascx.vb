Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlApproveOT
    Inherits Common.CommonView
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String

#Region "Properties"
    Private Property REGISTER_OT As List(Of AT_OT_REGISTRATIONDTO)
        Get
            Return ViewState(Me.ID & "_REGISTER_OT")
        End Get
        Set(ByVal value As List(Of AT_OT_REGISTRATIONDTO))
            ViewState(Me.ID & "_REGISTER_OT") = value
        End Set
    End Property
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
    Property dtDataImportEmployee As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportEmployee")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportEmployee") = value
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
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            SetGridFilter(rgDeclaresOT)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgDeclaresOT.AllowCustomPaging = True

            rgDeclaresOT.ClientSettings.EnablePostBackOnRowClick = False
            ctrlUpload1.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload1.MaxFileInput = 1
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Approve, ToolbarItem.Reject)
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
                    rdtungay.SelectedDate = periodid.START_DATE
                    rdDenngay.SelectedDate = periodid.END_DATE
                Else
                    cboPeriod.SelectedIndex = lsData.Count - 1
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            Dim rep As New AttendanceRepository
            Select Case CurrentState
                Case CommonMessage.STATE_APPROVE
                    Dim lstDeletes As New List(Of AT_REGISTER_OTDTO)

                    For Each i As GridDataItem In rgDeclaresOT.SelectedItems
                        If i.Edit Then
                            Dim o As New AT_REGISTER_OTDTO
                            o.ID = i.GetDataKeyValue("ID")
                            o.APPROVE_HOUR = CType(i("APPROVE_HOUR").Controls(0), RadNumericTextBox).Value
                            lstDeletes.Add(o)
                        End If
                    Next
                    If rep.ApproveRegisterOT(lstDeletes, 1) Then
                        Refresh("UpdateView")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_REJECT
                    Dim lstDeletes As New List(Of AT_REGISTER_OTDTO)

                    For Each i As GridDataItem In rgDeclaresOT.SelectedItems
                        If i.Edit Then
                            Dim o As New AT_REGISTER_OTDTO
                            o.ID = i.GetDataKeyValue("ID")
                            o.APPROVE_HOUR = 0
                            lstDeletes.Add(o)
                        End If
                    Next
                    If rep.ApproveRegisterOT(lstDeletes, 0) Then
                        Refresh("UpdateView")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            UpdateToolbarState()
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
        Me.PERIOD = dtData
        FillRadCombobox(cboPeriod, dtData, "PERIOD_NAME", "PERIOD_ID", True)
        If dtData.Count > 0 Then
            Dim periodid = (From d In dtData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
            If periodid IsNot Nothing Then
                cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                rdtungay.SelectedDate = periodid.START_DATE
                rdDenngay.SelectedDate = periodid.END_DATE
                rgDeclaresOT.Rebind()
                For Each i As GridDataItem In rgDeclaresOT.Items
                    If i.GetDataKeyValue("APPROVE_ID") IsNot Nothing AndAlso
                        (i.GetDataKeyValue("APPROVE_ID") = 1 Or
                         i.GetDataKeyValue("APPROVE_ID") = 2) Then
                        i.Edit = False
                        Continue For
                    End If
                    i.Edit = True
                Next
                rgDeclaresOT.Rebind()
            Else
                cboPeriod.SelectedIndex = dtData.Count - 1
                Dim per = (From c In dtData Where c.PERIOD_ID = cboPeriod.SelectedValue).FirstOrDefault
                rdtungay.SelectedDate = per.START_DATE
                rdDenngay.SelectedDate = per.END_DATE
            End If
        End If
    End Sub

    Private Sub cboPeriod_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        period.YEAR = Decimal.Parse(cboYear.SelectedValue)
        period.ORG_ID = 46
        If cboPeriod.SelectedValue <> "" Then
            Dim Lstperiod = rep.LOAD_PERIODBylinq(period)

            Dim p = (From o In Lstperiod Where o.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)).FirstOrDefault
            rdtungay.SelectedDate = p.START_DATE
            rdDenngay.SelectedDate = p.END_DATE

        End If
        rgDeclaresOT.Rebind()
        For Each i As GridDataItem In rgDeclaresOT.Items
            If i.GetDataKeyValue("APPROVE_ID") IsNot Nothing AndAlso
                (i.GetDataKeyValue("APPROVE_ID") = 1 Or
                 i.GetDataKeyValue("APPROVE_ID") = 2) Then
                Continue For
            End If
            i.Edit = True
        Next
        rgDeclaresOT.Rebind()
        ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceBusinessClient
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                rgDeclaresOT.Rebind()
                For Each i As GridDataItem In rgDeclaresOT.Items
                    If i.GetDataKeyValue("APPROVE_ID") IsNot Nothing AndAlso
                        (i.GetDataKeyValue("APPROVE_ID") = 1 Or
                         i.GetDataKeyValue("APPROVE_ID") = 2) Then
                        Continue For
                    End If
                    i.Edit = True
                Next
                rgDeclaresOT.Rebind()
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDeclaresOT.Rebind()
                        For Each i As GridDataItem In rgDeclaresOT.Items
                            If i.GetDataKeyValue("APPROVE_ID") IsNot Nothing AndAlso
                                (i.GetDataKeyValue("APPROVE_ID") = 1 Or
                                 i.GetDataKeyValue("APPROVE_ID") = 2) Then
                                i.Edit = False
                                Continue For
                            End If
                            i.Edit = True
                        Next
                        rgDeclaresOT.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, , rgSignWork.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDeclaresOT.CurrentPageIndex = 0
                        rgDeclaresOT.MasterTableView.SortExpressions.Clear()
                        rgDeclaresOT.Rebind()
                        For Each i As GridDataItem In rgDeclaresOT.Items
                            If i.GetDataKeyValue("APPROVE_ID") IsNot Nothing AndAlso
                                (i.GetDataKeyValue("APPROVE_ID") = 1 Or
                                 i.GetDataKeyValue("APPROVE_ID") = 2) Then
                                i.Edit = False
                                Continue For
                            End If
                            i.Edit = True
                        Next
                        rgDeclaresOT.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, )
                    Case "Cancel"
                        rgDeclaresOT.MasterTableView.ClearSelectedItems()
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
        rgDeclaresOT.CurrentPageIndex = 0
        rgDeclaresOT.Rebind()
        For Each i As GridDataItem In rgDeclaresOT.Items
            If i.GetDataKeyValue("APPROVE_ID") IsNot Nothing AndAlso
                (i.GetDataKeyValue("APPROVE_ID") = 1 Or
                 i.GetDataKeyValue("APPROVE_ID") = 2) Then
                i.Edit = False
                Continue For
            End If
            i.Edit = True
        Next
        rgDeclaresOT.Rebind()
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim rep As New AttendanceRepository
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue), _
                                            .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve}

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_APPROVE
                    If rgDeclaresOT.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_APPROVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case TOOLBARITEM_REJECT
                    If rgDeclaresOT.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_REJECT)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_REJECT
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                    rgDeclaresOT.Rebind()
                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgDeclaresOT.ExportExcel(Server, Response, dtDatas, "DataEntitlement")
                        End If
                    End Using
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_APPROVE
                UpdateControlState()
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_REJECT And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_REJECT
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_OT_REGISTRATIONDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgDeclaresOT, obj)
            Dim _param = New ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue, _
                                           .IS_DISSOLVE = ctrlOrganization.IsDissolve,
                                           .IS_FULL = True}
            Dim Sorts As String = rgDeclaresOT.MasterTableView.SortExpressions.GetSortString()

            If rdtungay.SelectedDate.HasValue Then
                obj.REGIST_DATE_FROM = rdtungay.SelectedDate
            End If
            If rdDenngay.SelectedDate.HasValue Then
                obj.REGIST_DATE_TO = rdDenngay.SelectedDate
            End If
            'If chkChecknghiViec.Checked Then
            '    obj.IS_TERMINATE = True
            'Else
            '    obj.IS_TERMINATE = False
            'End If
            'obj.IS_NB = False
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.REGISTER_OT = rep.GetRegisterOT(obj, _param, MaximumRows,
                                                       rgDeclaresOT.CurrentPageIndex,
                                                       rgDeclaresOT.PageSize, "APPROVE_ID")
                Else
                    Me.REGISTER_OT = rep.GetRegisterOT(obj, _param, MaximumRows, rgDeclaresOT.CurrentPageIndex, rgDeclaresOT.PageSize)
                End If
            Else
                Return rep.GetRegisterOT(obj, _param).ToTable
            End If

            rgDeclaresOT.VirtualItemCount = MaximumRows
            rgDeclaresOT.DataSource = Me.REGISTER_OT
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDeclaresOT.NeedDataSource
        Try
            CreateDataFilter(False)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            rgDeclaresOT.Rebind()
            For Each i As GridDataItem In rgDeclaresOT.Items
                If i.GetDataKeyValue("APPROVE_ID") IsNot Nothing AndAlso
                    (i.GetDataKeyValue("APPROVE_ID") = 1 Or
                     i.GetDataKeyValue("APPROVE_ID") = 2) Then
                    i.Edit = False
                    Continue For
                End If
                i.Edit = True
            Next
            rgDeclaresOT.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Try
            Dim url = e.Argument
            If (url.Contains("reload=1")) Then
                rgDeclaresOT.CurrentPageIndex = 0
                rgDeclaresOT.Rebind()
                For Each i As GridDataItem In rgDeclaresOT.Items
                    If i.GetDataKeyValue("APPROVE_ID") IsNot Nothing AndAlso
                        (i.GetDataKeyValue("APPROVE_ID") = 1 Or
                         i.GetDataKeyValue("APPROVE_ID") = 2) Then
                        i.Edit = False
                        Continue For
                    End If
                    i.Edit = True
                Next
                rgDeclaresOT.Rebind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgDeclaresOT_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgDeclaresOT.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
        End If

        If e.Item.Edit Then
            Dim edit = CType(e.Item, GridEditableItem)
            Dim rntxt As RadNumericTextBox
            rntxt = CType(edit("APPROVE_HOUR").Controls(0), RadNumericTextBox)
            rntxt.Width = Unit.Percentage(100)
            rntxt.MinValue = 0
            rntxt.MaxValue = 0
            If edit.GetDataKeyValue("HOUR") IsNot Nothing Then
                rntxt.MaxValue = edit.GetDataKeyValue("HOUR")
                rntxt.Value = edit.GetDataKeyValue("HOUR")
            End If
        End If
    End Sub


#End Region

#Region "Custom"

    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region
End Class