Imports Common
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_SettingCriteriaGroup
    Inherits CommonView

    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Public Property SelectedItemCriNotGroup As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCriNotGroup") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCriNotGroup") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCriNotGroup")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCriNotGroup") = value
        End Set
    End Property

    Public Property SelectedItemCriGroup As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCriGroup") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCriGroup") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCriGroup")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCriGroup") = value
        End Set
    End Property

    '0 - normal
    '1 - Employee
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
            rgCriNotGroup.PageSize = Common.Common.DefaultPageSize
            rgCriNotGroup.AllowCustomPaging = True
            rgCriGroup.PageSize = Common.Common.DefaultPageSize
            rgCriGroup.AllowCustomPaging = True
            UpdateControlState()
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dtData
            Using rep As New TrainingRepository
                dtData = rep.GetTrCriteriaGroupList(False)
                FillRadCombobox(cboCriteriaGroup, dtData, "NAME", "ID")
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            tbarMain.Visible = False
        Catch ex As Exception
            Throw ex
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

    Public Overrides Sub UpdateControlState()
        Using rep As New TrainingRepository
            Select Case CurrentState
                Case CommonMessage.STATE_NEW

                    Dim lst As New List(Of SettingCriteriaGroupDTO)
                    For Each criID In SelectedItemCriNotGroup
                        Dim obj As New SettingCriteriaGroupDTO
                        obj.TR_CRITERIA_ID = criID
                        obj.TR_CRITERIA_GROUP_ID = Decimal.Parse(cboCriteriaGroup.SelectedValue)
                        lst.Add(obj)
                    Next

                    If rep.InsertSettingCriteriaGroup(lst) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        SelectedItemCriNotGroup = Nothing
                        SelectedItemCriGroup = Nothing
                        rgCriNotGroup.Rebind()
                        rgCriGroup.Rebind()
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_NEW & "ALL"

                    Dim lst As New List(Of SettingCriteriaGroupDTO)
                    For Each item As GridDataItem In rgCriNotGroup.Items
                        Dim obj As New SettingCriteriaGroupDTO
                        obj.TR_CRITERIA_ID = item.GetDataKeyValue("TR_CRITERIA_ID")
                        obj.TR_CRITERIA_GROUP_ID = Decimal.Parse(cboCriteriaGroup.SelectedValue)
                        lst.Add(obj)
                    Next

                    If rep.InsertSettingCriteriaGroup(lst) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        SelectedItemCriNotGroup = Nothing
                        SelectedItemCriGroup = Nothing
                        rgCriNotGroup.Rebind()
                        rgCriGroup.Rebind()
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_DELETE
                    If rep.DeleteSettingCriteriaGroup(SelectedItemCriGroup) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        SelectedItemCriNotGroup = Nothing
                        SelectedItemCriGroup = Nothing
                        rgCriNotGroup.Rebind()
                        rgCriGroup.Rebind()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_DELETE & "ALL"
                    Dim lst As New List(Of Decimal)
                    For Each item As GridDataItem In rgCriNotGroup.Items
                        lst.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteSettingCriteriaGroup(lst) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        SelectedItemCriNotGroup = Nothing
                        SelectedItemCriGroup = Nothing
                        rgCriNotGroup.Rebind()
                        rgCriGroup.Rebind()
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

    Private Sub tbarMain_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles tbarMain.ButtonClick
        Dim rep As New TrainingRepository
        Dim _error As Integer = 0
        Dim sType As Object = Nothing
        Try

        Catch ex As Exception
            ShowMessage(ex.Message, Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub rgCriNotGroup_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCriNotGroup.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim id = datarow.GetDataKeyValue("TR_CRITERIA_ID")
            If SelectedItemCriNotGroup IsNot Nothing AndAlso SelectedItemCriNotGroup.Contains(id) Then
                datarow.Selected = True
            End If
        End If
    End Sub

    Private Sub rgCriNotGroup_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCriNotGroup.PageIndexChanged
        GetCanNotScheduleSelected()
    End Sub

    Private Sub rgCriNotGroup_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgCriNotGroup.PageSizeChanged
        GetCanNotScheduleSelected()
    End Sub

    Private Sub rgCriNotGroup_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles rgCriNotGroup.SortCommand
        GetCanNotScheduleSelected()
    End Sub

    Private Sub rgCriGroup_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCriGroup.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim id = datarow.GetDataKeyValue("ID")
            If SelectedItemCriGroup IsNot Nothing AndAlso SelectedItemCriGroup.Contains(id) Then
                datarow.Selected = True
            End If
        End If
    End Sub

    Private Sub rgCriGroup_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCriGroup.PageIndexChanged
        GetCanScheduleSelected()
    End Sub

    Private Sub rgCriGroup_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgCriGroup.PageSizeChanged
        GetCanScheduleSelected()
    End Sub

    Private Sub rgCriGroup_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles rgCriGroup.SortCommand
        GetCanScheduleSelected()
    End Sub

    Private Sub rgCriNotGroup_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCriNotGroup.NeedDataSource
        CreateDataFilterCanNotSchedule()
    End Sub

    Private Sub rgCriGroup_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCriGroup.NeedDataSource
        CreateDataFilterStudent()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If cboCriteriaGroup.SelectedValue = "" Then
            ShowMessage(Translate("Bạn phải chọn Nhóm tiêu chí đánh giá"), NotifyType.Warning)
            Exit Sub
        End If
        GetCanScheduleSelected()
        ctrlMessageBox.MessageText = "Bạn có chắc chắn xóa " & SelectedItemCriGroup.Count & " tiêu chí?"
        ctrlMessageBox.ActionName = "DELETE"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()

    End Sub

    Private Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        If cboCriteriaGroup.SelectedValue = "" Then
            ShowMessage(Translate("Bạn phải chọn Nhóm tiêu chí đánh giá"), NotifyType.Warning)
            Exit Sub
        End If
        GetCanNotScheduleSelected()
        ctrlMessageBox.MessageText = "Bạn có chắc chắn chuyển " & SelectedItemCriNotGroup.Count & " tiêu chí vào nhóm?"
        ctrlMessageBox.ActionName = "INSERT"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = "INSERT" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_NEW
                UpdateControlState()
            End If
            If e.ActionName = "INSERTALL" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_NEW & "ALL"
                UpdateControlState()
            End If
            If e.ActionName = "DELETE" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            If e.ActionName = "DELETEALL" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE & "ALL"
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cboCriteriaGroup_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCriteriaGroup.SelectedIndexChanged
        Try
            rgCriGroup.Rebind()
            rgCriNotGroup.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    Private Sub CreateDataFilterStudent()

        Try
            Dim rep As New TrainingRepository
            Dim _filter As New SettingCriteriaGroupDTO
            If cboCriteriaGroup.SelectedValue <> "" Then
                _filter.TR_CRITERIA_GROUP_ID = Decimal.Parse(cboCriteriaGroup.SelectedValue)
            End If
            Dim Sorts As String = rgCriGroup.MasterTableView.SortExpressions.GetSortString()
            Dim MaximumRows As Decimal = 0
            If Sorts IsNot Nothing Then
                rgCriGroup.DataSource = rep.GetCriteriaByGroupID(_filter, rgCriGroup.CurrentPageIndex, _
                                                                rgCriGroup.PageSize, _
                                                                MaximumRows, _
                                                                Sorts)
            Else

                rgCriGroup.DataSource = rep.GetCriteriaByGroupID(_filter, rgCriGroup.CurrentPageIndex, _
                                                                rgCriGroup.PageSize, _
                                                                MaximumRows)
            End If
            rgCriNotGroup.VirtualItemCount = MaximumRows
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub CreateDataFilterCanNotSchedule()

        Try
            Dim rep As New TrainingRepository
            Dim _filter As New SettingCriteriaGroupDTO
            If cboCriteriaGroup.SelectedValue <> "" Then
                _filter.TR_CRITERIA_GROUP_ID = Decimal.Parse(cboCriteriaGroup.SelectedValue)
            End If
            Dim Sorts As String = rgCriNotGroup.MasterTableView.SortExpressions.GetSortString()
            Dim MaximumRows As Decimal = 0
            If Sorts IsNot Nothing Then
                rgCriNotGroup.DataSource = rep.GetCriteriaNotByGroupID(_filter, rgCriNotGroup.CurrentPageIndex, _
                                                            rgCriNotGroup.PageSize, _
                                                            MaximumRows, _
                                                            Sorts)
            Else
                rgCriNotGroup.DataSource = rep.GetCriteriaNotByGroupID(_filter, rgCriNotGroup.CurrentPageIndex, _
                                                            rgCriNotGroup.PageSize, _
                                                            MaximumRows)
            End If
            rgCriNotGroup.VirtualItemCount = MaximumRows

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub GetCanNotScheduleSelected()
        For Each dr As Telerik.Web.UI.GridDataItem In rgCriNotGroup.Items
            Dim id As String = dr.GetDataKeyValue("TR_CRITERIA_ID")
            If dr.Selected Then
                If Not SelectedItemCriNotGroup.Contains(id) Then SelectedItemCriNotGroup.Add(id)
            Else
                If SelectedItemCriNotGroup.Contains(id) Then SelectedItemCriNotGroup.Remove(id)
            End If
        Next
    End Sub

    Private Sub GetCanScheduleSelected()
        For Each dr As Telerik.Web.UI.GridDataItem In rgCriGroup.Items
            Dim id As String = dr.GetDataKeyValue("ID")
            If dr.Selected Then
                If Not SelectedItemCriGroup.Contains(id) Then SelectedItemCriGroup.Add(id)
            Else
                If SelectedItemCriGroup.Contains(id) Then SelectedItemCriGroup.Remove(id)
            End If
        Next
    End Sub

#End Region

End Class