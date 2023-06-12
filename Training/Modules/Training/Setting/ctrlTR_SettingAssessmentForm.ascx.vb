Imports Common
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_SettingAssessmentForm
    Inherits CommonView

    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Public Property SelectedItemCriGroupFormNotForm As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCriGroupFormNotForm") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCriGroupFormNotForm") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCriGroupFormNotForm")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCriGroupFormNotForm") = value
        End Set
    End Property

    Public Property SelectedItemCriGroupForm As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCriGroupForm") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCriGroupForm") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCriGroupForm")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCriGroupForm") = value
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
            rgCriGroupFormNotForm.PageSize = Common.Common.DefaultPageSize
            rgCriGroupFormNotForm.AllowCustomPaging = True
            rgCriGroupForm.PageSize = Common.Common.DefaultPageSize
            rgCriGroupForm.AllowCustomPaging = True
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
                dtData = rep.GetTrAssFormList(False)
                FillRadCombobox(cboAssForm, dtData, "NAME", "ID")
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

                    Dim lst As New List(Of SettingAssFormDTO)
                    For Each criID In SelectedItemCriGroupFormNotForm
                        Dim obj As New SettingAssFormDTO
                        obj.TR_CRITERIA_GROUP_ID = criID
                        obj.TR_ASSESSMENT_FORM_ID = Decimal.Parse(cboAssForm.SelectedValue)
                        lst.Add(obj)
                    Next

                    If rep.InsertSettingAssForm(lst) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        SelectedItemCriGroupFormNotForm = Nothing
                        SelectedItemCriGroupForm = Nothing
                        rgCriGroupFormNotForm.Rebind()
                        rgCriGroupForm.Rebind()
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_NEW & "ALL"

                    Dim lst As New List(Of SettingAssFormDTO)
                    For Each item As GridDataItem In rgCriGroupFormNotForm.Items
                        Dim obj As New SettingAssFormDTO
                        obj.TR_CRITERIA_GROUP_ID = item.GetDataKeyValue("TR_CRITERIA_GROUP_ID")
                        obj.TR_ASSESSMENT_FORM_ID = Decimal.Parse(cboAssForm.SelectedValue)
                        lst.Add(obj)
                    Next

                    If rep.InsertSettingAssForm(lst) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        SelectedItemCriGroupFormNotForm = Nothing
                        SelectedItemCriGroupForm = Nothing
                        rgCriGroupFormNotForm.Rebind()
                        rgCriGroupForm.Rebind()
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_DELETE
                    If rep.DeleteSettingAssForm(SelectedItemCriGroupForm) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        SelectedItemCriGroupFormNotForm = Nothing
                        SelectedItemCriGroupForm = Nothing
                        rgCriGroupFormNotForm.Rebind()
                        rgCriGroupForm.Rebind()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_DELETE & "ALL"
                    Dim lst As New List(Of Decimal)
                    For Each item As GridDataItem In rgCriGroupFormNotForm.Items
                        lst.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteSettingAssForm(lst) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        SelectedItemCriGroupFormNotForm = Nothing
                        SelectedItemCriGroupForm = Nothing
                        rgCriGroupFormNotForm.Rebind()
                        rgCriGroupForm.Rebind()
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

    Private Sub rgCriGroupFormNotForm_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCriGroupFormNotForm.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim id = datarow.GetDataKeyValue("TR_CRITERIA_GROUP_ID")
            If SelectedItemCriGroupFormNotForm IsNot Nothing AndAlso SelectedItemCriGroupFormNotForm.Contains(id) Then
                datarow.Selected = True
            End If
        End If
    End Sub

    Private Sub rgCriGroupFormNotForm_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCriGroupFormNotForm.PageIndexChanged
        GetCanNotScheduleSelected()
    End Sub

    Private Sub rgCriGroupFormNotForm_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgCriGroupFormNotForm.PageSizeChanged
        GetCanNotScheduleSelected()
    End Sub

    Private Sub rgCriGroupFormNotForm_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles rgCriGroupFormNotForm.SortCommand
        GetCanNotScheduleSelected()
    End Sub

    Private Sub rgCriGroupForm_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCriGroupForm.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim id = datarow.GetDataKeyValue("ID")
            If SelectedItemCriGroupForm IsNot Nothing AndAlso SelectedItemCriGroupForm.Contains(id) Then
                datarow.Selected = True
            End If
        End If
    End Sub

    Private Sub rgCriGroupForm_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCriGroupForm.PageIndexChanged
        GetCanScheduleSelected()
    End Sub

    Private Sub rgCriGroupForm_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgCriGroupForm.PageSizeChanged
        GetCanScheduleSelected()
    End Sub

    Private Sub rgCriGroupForm_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles rgCriGroupForm.SortCommand
        GetCanScheduleSelected()
    End Sub

    Private Sub rgCriGroupFormNotForm_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCriGroupFormNotForm.NeedDataSource
        CreateDataFilterCanNotSchedule()
    End Sub

    Private Sub rgCriGroupForm_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCriGroupForm.NeedDataSource
        CreateDataFilterStudent()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If cboAssForm.SelectedValue = "" Then
            ShowMessage(Translate("Bạn phải chọn Mẫu biểu đánh giá"), NotifyType.Warning)
            Exit Sub
        End If
        GetCanScheduleSelected()
        ctrlMessageBox.MessageText = "Bạn có chắc chắn xóa " & SelectedItemCriGroupForm.Count & " nhóm tiêu chí?"
        ctrlMessageBox.ActionName = "DELETE"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()

    End Sub

    Private Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        If cboAssForm.SelectedValue = "" Then
            ShowMessage(Translate("Bạn phải chọn Mẫu biểu đánh giá"), NotifyType.Warning)
            Exit Sub
        End If
        GetCanNotScheduleSelected()
        ctrlMessageBox.MessageText = "Bạn có chắc chắn chuyển " & SelectedItemCriGroupFormNotForm.Count & " nhóm tiêu chí vào nhóm?"
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

    Private Sub cboCriteriaGroup_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboAssForm.SelectedIndexChanged
        Try
            rgCriGroupForm.Rebind()
            rgCriGroupFormNotForm.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    Private Sub CreateDataFilterStudent()

        Try
            Dim rep As New TrainingRepository
            Dim _filter As New SettingAssFormDTO
            If cboAssForm.SelectedValue <> "" Then
                _filter.TR_ASSESSMENT_FORM_ID = Decimal.Parse(cboAssForm.SelectedValue)
            End If
            Dim Sorts As String = rgCriGroupForm.MasterTableView.SortExpressions.GetSortString()
            Dim MaximumRows As Decimal = 0
            If Sorts IsNot Nothing Then
                rgCriGroupForm.DataSource = rep.GetCriteriaGroupByFormID(_filter, rgCriGroupForm.CurrentPageIndex, _
                                                                rgCriGroupForm.PageSize, _
                                                                MaximumRows, _
                                                                Sorts)
            Else

                rgCriGroupForm.DataSource = rep.GetCriteriaGroupByFormID(_filter, rgCriGroupForm.CurrentPageIndex, _
                                                                rgCriGroupForm.PageSize, _
                                                                MaximumRows)
            End If
            rgCriGroupFormNotForm.VirtualItemCount = MaximumRows
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub CreateDataFilterCanNotSchedule()

        Try
            Dim rep As New TrainingRepository
            Dim _filter As New SettingAssFormDTO
            If cboAssForm.SelectedValue <> "" Then
                _filter.TR_ASSESSMENT_FORM_ID = Decimal.Parse(cboAssForm.SelectedValue)
            End If
            Dim Sorts As String = rgCriGroupFormNotForm.MasterTableView.SortExpressions.GetSortString()
            Dim MaximumRows As Decimal = 0
            If Sorts IsNot Nothing Then
                rgCriGroupFormNotForm.DataSource = rep.GetCriteriaGroupNotByFormID(_filter, rgCriGroupFormNotForm.CurrentPageIndex, _
                                                            rgCriGroupFormNotForm.PageSize, _
                                                            MaximumRows, _
                                                            Sorts)
            Else
                rgCriGroupFormNotForm.DataSource = rep.GetCriteriaGroupNotByFormID(_filter, rgCriGroupFormNotForm.CurrentPageIndex, _
                                                            rgCriGroupFormNotForm.PageSize, _
                                                            MaximumRows)
            End If
            rgCriGroupFormNotForm.VirtualItemCount = MaximumRows

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub GetCanNotScheduleSelected()
        For Each dr As Telerik.Web.UI.GridDataItem In rgCriGroupFormNotForm.Items
            Dim id As String = dr.GetDataKeyValue("TR_CRITERIA_GROUP_ID")
            If dr.Selected Then
                If Not SelectedItemCriGroupFormNotForm.Contains(id) Then SelectedItemCriGroupFormNotForm.Add(id)
            Else
                If SelectedItemCriGroupFormNotForm.Contains(id) Then SelectedItemCriGroupFormNotForm.Remove(id)
            End If
        Next
    End Sub

    Private Sub GetCanScheduleSelected()
        For Each dr As Telerik.Web.UI.GridDataItem In rgCriGroupForm.Items
            Dim id As String = dr.GetDataKeyValue("ID")
            If dr.Selected Then
                If Not SelectedItemCriGroupForm.Contains(id) Then SelectedItemCriGroupForm.Add(id)
            Else
                If SelectedItemCriGroupForm.Contains(id) Then SelectedItemCriGroupForm.Remove(id)
            End If
        Next
    End Sub

#End Region

End Class