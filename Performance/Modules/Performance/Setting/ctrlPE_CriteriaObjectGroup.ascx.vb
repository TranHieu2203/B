Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI

Public Class ctrlPE_CriteriaObjectGroup
    Inherits Common.CommonView

#Region "Property"

    Public Property ItemCanNotSchedule As List(Of CriteriaObjectGroupDTO)
        Get
            If ViewState(Me.ID & "_ItemCanNotSchedule") Is Nothing Then
                ViewState(Me.ID & "_ItemCanNotSchedule") = New List(Of CriteriaObjectGroupDTO)
            End If
            Return ViewState(Me.ID & "_ItemCanNotSchedule")
        End Get
        Set(ByVal value As List(Of CriteriaObjectGroupDTO))
            ViewState(Me.ID & "_ItemCanNotSchedule") = value
        End Set
    End Property

    Public Property ItemCanSchedule As List(Of CriteriaObjectGroupDTO)
        Get
            If ViewState(Me.ID & "ItemCanSchedule") Is Nothing Then
                ViewState(Me.ID & "ItemCanSchedule") = New List(Of CriteriaObjectGroupDTO)
            End If
            Return ViewState(Me.ID & "ItemCanSchedule")
        End Get
        Set(ByVal value As List(Of CriteriaObjectGroupDTO))
            ViewState(Me.ID & "ItemCanSchedule") = value
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
                Dim dtData = rep.GetObjectGroupList(True)
                FillRadCombobox(cboObjectGroup, dtData, "NAME", "ID")
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Refresh)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
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
                'Case CommonMessage.STATE_NEW
                '    Dim lst As New List(Of CriteriaObjectGroupDTO)
                '    For Each idSelected In ItemCanNotSchedule
                '        Dim obj As New CriteriaObjectGroupDTO
                '        obj.OBJECT_GROUP_ID = cboObjectGroup.SelectedValue
                '        obj.CRITERIA_ID = idSelected.CRITERIA_ID
                '        obj.CRITERIA_CODE = idSelected.CRITERIA_CODE
                '        obj.CRITERIA_NAME = idSelected.CRITERIA_NAME
                '        obj.EXPENSE = idSelected.EXPENSE
                '        obj.AMONG = idSelected.AMONG
                '        obj.FROM_DATE = idSelected.FROM_DATE
                '        obj.TO_DATE = idSelected.TO_DATE
                '        lst.Add(obj)
                '    Next

                '    If rep.InsertCriteriaByObjectGroup(lst) Then
                '        CurrentState = CommonMessage.STATE_NORMAL
                '        ItemCanNotSchedule = Nothing
                '        ItemCanSchedule = Nothing
                '        rgCanNotSchedule.Rebind()
                '        rgCanSchedule.Rebind()
                '        UpdateControlState()
                '    Else
                '        CurrentState = CommonMessage.STATE_NORMAL
                '        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                '        UpdateControlState()
                '    End If
                Case CommonMessage.STATE_DELETE
                    Dim lstSelectItemDelete As New List(Of Decimal)
                    For Each item In ItemCanSchedule
                        lstSelectItemDelete.Add(item.ID)
                    Next
                    ItemCanSchedule.Clear()
                    If rep.DeleteCriteriaByObjectGroup(lstSelectItemDelete) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        ItemCanNotSchedule = Nothing
                        ItemCanSchedule = Nothing
                        rgCanNotSchedule.Rebind()
                        rgCanSchedule.Rebind()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            ChangeToolbarState()
        End Using
    End Sub

#End Region

#Region "Event"

    Private Sub tbarMain_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim rep As New PerformanceRepository
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_REFRESH
                    rgCanNotSchedule.Rebind()
                    rgCanSchedule.Rebind()
                    ItemCanNotSchedule = Nothing
                    ItemCanSchedule = Nothing
                Case CommonMessage.TOOLBARITEM_EDIT
                    Dim isSchedule As Boolean = False
                    For Each item As GridDataItem In rgCanSchedule.MasterTableView.Items
                        If item.Selected Then
                            isSchedule = True
                            item.Edit = True
                        End If
                    Next
                    If Not isSchedule Then
                        ShowMessage(Translate("Chưa chọn tiêu chí để cập nhật"), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    ItemCanSchedule.Clear()
                    rgCanSchedule.MasterTableView.Rebind()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    For Each item As GridDataItem In rgCanSchedule.MasterTableView.Items
                        item.Edit = False
                    Next
                    ItemCanSchedule.Clear()
                    rgCanSchedule.MasterTableView.Rebind()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim lst As New List(Of CriteriaObjectGroupDTO)
                    For Each item As GridDataItem In rgCanSchedule.EditItems
                        If item.Edit = True Then
                            Dim edit = CType(item, GridEditableItem)
                            Dim txtEXPENSE As TextBox = CType(edit("EXPENSE").Controls(0), TextBox)
                            Dim txtAMONG As RadNumericTextBox = CType(edit("AMONG").Controls(0), RadNumericTextBox)
                            Dim rdFROM_DATE As RadDatePicker = CType(edit("FROM_DATE").Controls(0), RadDatePicker)
                            Dim rdTO_DATE As RadDatePicker = CType(edit("TO_DATE").Controls(0), RadDatePicker)

                            Dim obj As New CriteriaObjectGroupDTO
                            With obj
                                .ID = item.GetDataKeyValue("ID")
                                .CRITERIA_ID = item.GetDataKeyValue("CRITERIA_ID")
                                .OBJECT_GROUP_ID = cboObjectGroup.SelectedValue
                                .EXPENSE = txtEXPENSE.Text
                                .AMONG = Convert.ToDecimal(txtAMONG.Text)
                                .FROM_DATE = rdFROM_DATE.SelectedDate
                                .TO_DATE = rdTO_DATE.SelectedDate
                            End With
                            lst.Add(obj)
                        End If
                    Next
                    'Validate truong du lieu nhap vao
                    If Not ValidateItemValueObjDTO(lst) Then
                        ShowMessage(Translate("Tiêu chí đánh giá chưa nhập đầy đủ thông tin"), NotifyType.Warning)
                        Exit Sub
                    End If
                    ItemCanSchedule.Clear()
                    If rep.InsertCriteriaByObjectGroup(lst) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        For Each item As GridDataItem In rgCanSchedule.MasterTableView.Items
                            item.Edit = False
                        Next
                        rgCanSchedule.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    End If
            End Select
            UpdateControlState()
        Catch ex As Exception
            ShowMessage(ex.Message, Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub rgCanNotSchedule_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCanNotSchedule.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim id = datarow.GetDataKeyValue("CRITERIA_ID")
            Dim code As String = datarow.GetDataKeyValue("CRITERIA_CODE")
            Dim name As String = datarow.GetDataKeyValue("CRITERIA_NAME")
            Dim objadd As New CriteriaObjectGroupDTO
            objadd.CRITERIA_ID = id
            objadd.CRITERIA_CODE = code
            objadd.CRITERIA_NAME = name
            'If ItemCanNotSchedule IsNot Nothing AndAlso ItemCanNotSchedule.Contains(objadd) Then
            '    datarow.Selected = True
            'End If
            Dim var = ItemCanNotSchedule.Where(Function(f) f.CRITERIA_ID = objadd.CRITERIA_ID).ToList
            If var.Count > 0 Then
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
            Dim code As String = datarow.GetDataKeyValue("CRITERIA_CODE")
            Dim name As String = datarow.GetDataKeyValue("CRITERIA_NAME")
            Dim objadd As New CriteriaObjectGroupDTO
            objadd.ID = id
            objadd.CRITERIA_CODE = code
            objadd.CRITERIA_NAME = name
            'If ItemCanSchedule IsNot Nothing AndAlso ItemCanSchedule.Contains(objadd) Then
            '    datarow.Selected = True
            'End If
            Dim var = ItemCanSchedule.Where(Function(f) f.ID = objadd.ID).ToList
            If var.Count > 0 Then
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
            ctrlMessageBox.MessageText = "Bạn có chắc chắn xóa " & ItemCanSchedule.Count & " Tiêu chí khỏi Nhóm đối tượng?"
            ctrlMessageBox.ActionName = "DELETE"
            ctrlMessageBox.DataBind()
            ctrlMessageBox.Show()
        End If
    End Sub

    Private Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        If Page.IsValid Then
            GetCanNotScheduleSelected()
            ItemCanSchedule.Clear()
            'ctrlMessageBox.MessageText = "Bạn có chắc chắn chuyển " & SelectedItemCanNotSchedule.Count & " Tiêu chí vào Nhóm đối tượng?"
            'ctrlMessageBox.ActionName = "INSERT"
            'ctrlMessageBox.DataBind()
            'ctrlMessageBox.Show()
            ' Lay du lieu gan sang luoi

            ' Dim lst As New List(Of CriteriaObjectGroupDTO)
            For Each idSelected In ItemCanNotSchedule
                Dim obj As New CriteriaObjectGroupDTO
                obj.OBJECT_GROUP_ID = cboObjectGroup.SelectedValue
                obj.CRITERIA_CODE = idSelected.CRITERIA_CODE
                obj.CRITERIA_NAME = idSelected.CRITERIA_NAME
                obj.CRITERIA_ID = idSelected.CRITERIA_ID
                Dim var = ItemCanSchedule.Where(Function(f) f.CRITERIA_ID = obj.CRITERIA_ID And f.OBJECT_GROUP_ID = obj.OBJECT_GROUP_ID).ToList
                If var.Count <= 0 Or ItemCanSchedule.Count = 0 Then
                    ItemCanSchedule.Add(obj)
                End If
            Next
            ItemCanNotSchedule = Nothing
            'ItemCanSchedule = Nothing
            rgCanNotSchedule.Rebind()
            rgCanSchedule.Rebind()
            'rgCanSchedule.VirtualItemCount = MaximumRows
            'rgCanSchedule.DataSource = ItemCanSchedule
            EditValueItem()
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

    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboObjectGroup.SelectedIndexChanged
        ItemCanSchedule.Clear()
        rgCanNotSchedule.Rebind()
        rgCanSchedule.Rebind()
        ItemCanNotSchedule = Nothing
        ItemCanSchedule = Nothing
    End Sub
#End Region

#Region "Custom"

    Private Sub CreateDataFilterStudent()

        Try
            Using rep As New PerformanceRepository
                Dim _filter As New CriteriaObjectGroupDTO
                If cboObjectGroup.SelectedValue = "" Then
                    rgCanSchedule.VirtualItemCount = 0
                    rgCanSchedule.DataSource = New List(Of CriteriaObjectGroupDTO)
                    Exit Sub
                End If
                SetValueObjectByRadGrid(rgCanSchedule, _filter)

                If cboObjectGroup.SelectedValue <> "" Then
                    _filter.OBJECT_GROUP_ID = cboObjectGroup.SelectedValue
                End If
                Dim Sorts As String = rgCanSchedule.MasterTableView.SortExpressions.GetSortString()
                Dim MaximumRows As Decimal = 0
                Dim lstData As List(Of CriteriaObjectGroupDTO)
                If Sorts IsNot Nothing Then
                    lstData = rep.GetCriteriaByObjectGroupID(_filter,
                                                           rgCanSchedule.CurrentPageIndex, _
                                                           rgCanSchedule.PageSize, _
                                                           MaximumRows, _
                                                           Sorts)
                Else

                    lstData = rep.GetCriteriaByObjectGroupID(_filter,
                                                           rgCanSchedule.CurrentPageIndex, _
                                                           rgCanSchedule.PageSize, _
                                                           MaximumRows)
                End If
                'luu lai data cua luoi
                'ItemCanSchedule.Clear()
                ItemCanSchedule.AddRange(lstData)
                rgCanSchedule.VirtualItemCount = MaximumRows
                rgCanSchedule.DataSource = ItemCanSchedule
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub CreateDataFilterCanNotSchedule()

        Try
            Using rep As New PerformanceRepository
                Dim _filter As New CriteriaObjectGroupDTO
                If cboObjectGroup.SelectedValue = "" Then
                    rgCanNotSchedule.VirtualItemCount = 0
                    rgCanNotSchedule.DataSource = New List(Of CriteriaObjectGroupDTO)
                    Exit Sub
                End If
                SetValueObjectByRadGrid(rgCanNotSchedule, _filter)
                If cboObjectGroup.SelectedValue <> "" Then
                    _filter.OBJECT_GROUP_ID = cboObjectGroup.SelectedValue
                End If
                Dim Sorts As String = rgCanNotSchedule.MasterTableView.SortExpressions.GetSortString()
                Dim MaximumRows As Decimal = 0
                Dim lstData As List(Of CriteriaObjectGroupDTO)
                If Sorts IsNot Nothing Then
                    lstData = rep.GetCriteriaNotByObjectGroupID(_filter,
                                                              rgCanNotSchedule.CurrentPageIndex, _
                                                              rgCanNotSchedule.PageSize, _
                                                              MaximumRows, _
                                                              Sorts)
                Else
                    lstData = rep.GetCriteriaNotByObjectGroupID(_filter,
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
            Dim id As String = dr.GetDataKeyValue("CRITERIA_ID")
            Dim code As String = dr.GetDataKeyValue("CRITERIA_CODE")
            Dim name As String = dr.GetDataKeyValue("CRITERIA_NAME")
            Dim objadd As New CriteriaObjectGroupDTO
            objadd.CRITERIA_ID = id
            objadd.CRITERIA_CODE = code
            objadd.CRITERIA_NAME = name
            Dim var = ItemCanNotSchedule.Where(Function(f) f.CRITERIA_ID = objadd.CRITERIA_ID).ToList
            If dr.Selected Then
                'If Not ItemCanNotSchedule.Contains(objadd) Then
                '    ItemCanNotSchedule.Add(objadd)
                'End If
                'Dim var = From p In ItemCanNotSchedule.Where(Function(f) f.CRITERIA_ID = objadd.CRITERIA_ID)
                'Select New CriteriaObjectGroupDTO With {
                '                 .CRITERIA_ID = p.CRITERIA_ID}
                If var.Count <= 0 Or ItemCanNotSchedule.Count = 0 Then
                    ItemCanNotSchedule.Add(objadd)
                End If
            Else
                'For Each item As CriteriaObjectGroupDTO In ItemCanNotSchedule
                '    If item.ID = id Then
                '        ItemCanNotSchedule.Remove(item)
                '    End If
                'Next
                If var IsNot Nothing Then
                    ItemCanNotSchedule.Remove(objadd)
                End If
            End If
        Next
    End Sub

    Private Sub GetCanScheduleSelected()
        Try
            ItemCanSchedule.Clear()
            For Each dr As Telerik.Web.UI.GridDataItem In rgCanSchedule.Items
                Dim id As String = dr.GetDataKeyValue("ID")
                Dim code As String = dr.GetDataKeyValue("CRITERIA_CODE")
                Dim name As String = dr.GetDataKeyValue("CRITERIA_NAME")
                Dim objadd As New CriteriaObjectGroupDTO
                objadd.ID = id
                objadd.CRITERIA_CODE = code
                objadd.CRITERIA_NAME = name
                'Dim var = ItemCanSchedule.Where(Function(f) f.ID = objadd.ID).ToList
                'If dr.Selected Then
                '    If var.Count <= 0 Or ItemCanSchedule.Count = 0 Then
                '        ItemCanSchedule.Add(objadd)
                '    End If
                '    'If Not ItemCanSchedule.Contains(objadd) Then ItemCanSchedule.Add(objadd)
                'Else
                '    If var IsNot Nothing Then
                '        ItemCanSchedule.Remove(objadd)
                '    End If
                '    'If ItemCanSchedule.Contains(objadd) Then ItemCanSchedule.Remove(objadd)
                'End If
                If dr.Selected Then
                    ItemCanSchedule.Add(objadd)
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub

    Private Sub EditValueItem()
        Try
            'Dim isSchedule As Boolean = False
            For Each item As GridDataItem In rgCanSchedule.MasterTableView.Items
                'If item.Selected Then
                'isSchedule = True
                item.Edit = True
                'End If
            Next
            'If Not isSchedule Then
            '    ShowMessage(Translate("Chưa chọn tiêu chí để cập nhật"), NotifyType.Warning)
            '    Exit Sub
            'End If
            CurrentState = CommonMessage.STATE_EDIT
            rgCanSchedule.MasterTableView.Rebind()
            ChangeToolbarState()
        Catch ex As Exception

        End Try

    End Sub

    Private Function ValidateItemValueObjDTO(ByVal lstObjDTO As List(Of CriteriaObjectGroupDTO)) As Boolean
        Dim result As Boolean = True
        Try
            For Each item In lstObjDTO
                If item.CRITERIA_ID Is Nothing Or item.OBJECT_GROUP_ID Is Nothing Or item.EXPENSE = "" Or item.AMONG Is Nothing Or item.FROM_DATE Is Nothing Or item.TO_DATE Is Nothing Then
                    result = False
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
        Return result
    End Function


#End Region

End Class