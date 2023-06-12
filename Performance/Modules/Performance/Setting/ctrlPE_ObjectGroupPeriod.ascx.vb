Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI

Public Class ctrlPE_ObjectGroupPeriod
    Inherits Common.CommonView

#Region "Property"

    Public Property SelectedItemCanNotSchedule As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCanNotSchedule") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCanNotSchedule") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCanNotSchedule")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCanNotSchedule") = value
        End Set
    End Property

    Public Property SelectedItemCanSchedule As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCanSchedule") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCanSchedule") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCanSchedule")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCanSchedule") = value
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

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgCanNotSchedule.AllowCustomPaging = True
            rgCanNotSchedule.SetFilter()
            rgCanSchedule.AllowCustomPaging = True
            rgCanSchedule.SetFilter()
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            Using rep As New PerformanceRepository
                Dim dtData = rep.GetPeriodList(True)
                FillRadCombobox(cboPeriod, dtData, "NAME", "ID")
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Refresh)
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        If Not IsPostBack Then
            CurrentState = CommonMessage.STATE_NORMAL
        End If
    End Sub

    Public Overrides Sub UpdateControlState()

        Using rep As New PerformanceRepository
            Select Case CurrentState
                Case CommonMessage.STATE_NEW

                    Dim lst As New List(Of ObjectGroupPeriodDTO)
                    For Each idSelected In SelectedItemCanNotSchedule
                        Dim obj As New ObjectGroupPeriodDTO
                        obj.OBJECT_GROUP_ID = idSelected
                        obj.PERIOD_ID = cboPeriod.SelectedValue
                        lst.Add(obj)
                    Next

                    If rep.InsertObjectGroupByPeriod(lst) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        SelectedItemCanNotSchedule = Nothing
                        SelectedItemCanSchedule = Nothing
                        rgCanNotSchedule.Rebind()
                        rgCanSchedule.Rebind()
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_DELETE
                    If rep.DeleteObjectGroupByPeriod(SelectedItemCanSchedule) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        SelectedItemCanNotSchedule = Nothing
                        SelectedItemCanSchedule = Nothing
                        rgCanNotSchedule.Rebind()
                        rgCanSchedule.Rebind()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select

        End Using
    End Sub

#End Region

#Region "Event"

    Private Sub tbarMain_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_REFRESH
                    rgCanNotSchedule.Rebind()
                    rgCanSchedule.Rebind()
                    SelectedItemCanNotSchedule = Nothing
                    SelectedItemCanSchedule = Nothing
            End Select
        Catch ex As Exception
            ShowMessage(ex.Message, Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub rgCanNotSchedule_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCanNotSchedule.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim id = datarow.GetDataKeyValue("OBJECT_GROUP_ID")
            If SelectedItemCanNotSchedule IsNot Nothing AndAlso SelectedItemCanNotSchedule.Contains(id) Then
                datarow.Selected = True
            End If
        End If
    End Sub

    Private Sub rgCanNotSchedule_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCanNotSchedule.PageIndexChanged
        GetCanNotScheduleSelected()
    End Sub

    Private Sub rgCanNotSchedule_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgCanNotSchedule.PageSizeChanged
        GetCanNotScheduleSelected()
    End Sub

    Private Sub rgCanNotSchedule_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles rgCanNotSchedule.SortCommand
        GetCanNotScheduleSelected()
    End Sub

    Private Sub rgCanSchedule_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCanSchedule.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim id = datarow.GetDataKeyValue("ID")
            If SelectedItemCanSchedule IsNot Nothing AndAlso SelectedItemCanSchedule.Contains(id) Then
                datarow.Selected = True
            End If
        End If
    End Sub

    Private Sub rgCanSchedule_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCanSchedule.PageIndexChanged
        GetCanScheduleSelected()
    End Sub

    Private Sub rgCanSchedule_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgCanSchedule.PageSizeChanged
        GetCanScheduleSelected()
    End Sub

    Private Sub rgCanSchedule_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles rgCanSchedule.SortCommand
        GetCanScheduleSelected()
    End Sub

    Private Sub rgCanNotSchedule_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCanNotSchedule.NeedDataSource
        CreateDataFilterCanNotSchedule()
    End Sub

    Private Sub rgCanSchedule_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCanSchedule.NeedDataSource
        CreateDataFilterStudent()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Page.IsValid Then
            GetCanScheduleSelected()
            ctrlMessageBox.MessageText = "Bạn có chắc chắn xóa " & SelectedItemCanSchedule.Count & " Nhóm đối tượng khỏi Kỳ đánh giá?"
            ctrlMessageBox.ActionName = "DELETE"
            ctrlMessageBox.DataBind()
            ctrlMessageBox.Show()
        End If
    End Sub

    Private Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        If Page.IsValid Then
            GetCanNotScheduleSelected()
            ctrlMessageBox.MessageText = "Bạn có chắc chắn chuyển " & SelectedItemCanNotSchedule.Count & " Nhóm đối tượng vào Kỳ đánh giá?"
            ctrlMessageBox.ActionName = "INSERT"
            ctrlMessageBox.DataBind()
            ctrlMessageBox.Show()
        End If
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = "INSERT" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_NEW
                UpdateControlState()
            End If
            If e.ActionName = "DELETE" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cboPeriod_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriod.SelectedIndexChanged
        rgCanNotSchedule.Rebind()
        rgCanSchedule.Rebind()
        SelectedItemCanNotSchedule = Nothing
        SelectedItemCanSchedule = Nothing
    End Sub
#End Region

#Region "Custom"

    Private Sub CreateDataFilterStudent()

        Try
            Using rep As New PerformanceRepository
                Dim _filter As New ObjectGroupPeriodDTO
                If cboPeriod.SelectedValue = "" Then
                    rgCanSchedule.VirtualItemCount = 0
                    rgCanSchedule.DataSource = New List(Of ObjectGroupPeriodDTO)
                    Exit Sub
                End If
                SetValueObjectByRadGrid(rgCanSchedule, _filter)
                _filter.PERIOD_ID = cboPeriod.SelectedValue
                Dim Sorts As String = rgCanSchedule.MasterTableView.SortExpressions.GetSortString()
                Dim MaximumRows As Decimal = 0
                Dim lstData As List(Of ObjectGroupPeriodDTO)
                If Sorts IsNot Nothing Then
                    lstData = rep.GetObjectGroupByPeriodID(_filter,
                                                           rgCanSchedule.CurrentPageIndex, _
                                                           rgCanSchedule.PageSize, _
                                                           MaximumRows, _
                                                           Sorts)
                Else

                    lstData = rep.GetObjectGroupByPeriodID(_filter,
                                                           rgCanSchedule.CurrentPageIndex, _
                                                           rgCanSchedule.PageSize, _
                                                           MaximumRows)
                End If
                rgCanSchedule.VirtualItemCount = MaximumRows
                rgCanSchedule.DataSource = lstData

            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub CreateDataFilterCanNotSchedule()

        Try
            Using rep As New PerformanceRepository
                Dim _filter As New ObjectGroupPeriodDTO
                If cboPeriod.SelectedValue = "" Then
                    rgCanNotSchedule.VirtualItemCount = 0
                    rgCanNotSchedule.DataSource = New List(Of ObjectGroupPeriodDTO)
                    Exit Sub
                End If
                SetValueObjectByRadGrid(rgCanNotSchedule, _filter)
                _filter.PERIOD_ID = cboPeriod.SelectedValue
                Dim Sorts As String = rgCanNotSchedule.MasterTableView.SortExpressions.GetSortString()
                Dim MaximumRows As Decimal = 0
                Dim lstData As List(Of ObjectGroupPeriodDTO)
                If Sorts IsNot Nothing Then
                    lstData = rep.GetObjectGroupNotByPeriodID(_filter,
                                                              rgCanNotSchedule.CurrentPageIndex, _
                                                              rgCanNotSchedule.PageSize, _
                                                              MaximumRows, _
                                                              Sorts)
                Else
                    lstData = rep.GetObjectGroupNotByPeriodID(_filter,
                                                              rgCanNotSchedule.CurrentPageIndex, _
                                                              rgCanNotSchedule.PageSize, _
                                                              MaximumRows)
                End If
                rgCanNotSchedule.VirtualItemCount = MaximumRows
                rgCanNotSchedule.DataSource = lstData

            End Using

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub GetCanNotScheduleSelected()
        For Each dr As Telerik.Web.UI.GridDataItem In rgCanNotSchedule.Items
            Dim id As String = dr.GetDataKeyValue("OBJECT_GROUP_ID")
            If dr.Selected Then
                If Not SelectedItemCanNotSchedule.Contains(id) Then SelectedItemCanNotSchedule.Add(id)
            Else
                If SelectedItemCanNotSchedule.Contains(id) Then SelectedItemCanNotSchedule.Remove(id)
            End If
        Next
    End Sub

    Private Sub GetCanScheduleSelected()
        For Each dr As Telerik.Web.UI.GridDataItem In rgCanSchedule.Items
            Dim id As String = dr.GetDataKeyValue("ID")
            If dr.Selected Then
                If Not SelectedItemCanSchedule.Contains(id) Then SelectedItemCanSchedule.Add(id)
            Else
                If SelectedItemCanSchedule.Contains(id) Then SelectedItemCanSchedule.Remove(id)
            End If
        Next
    End Sub

#End Region

End Class