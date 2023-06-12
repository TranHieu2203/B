Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlHU_CompetencyStandard
    Inherits Common.CommonView

#Region "Property"

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
            rgMain.SetFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        rgMain.AllowCustomPaging = True
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete, ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL

                Dim titleid = Decimal.Parse(Request.Params("titleid"))
                cboTitle.SelectedValue = titleid

            Else

                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileRepository
        Dim _filter As New CompetencyStandardDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If cboTitle.SelectedValue <> "" Then
                _filter.TITLE_ID = cboTitle.SelectedValue
            End If
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetCompetencyStandard(_filter, Sorts).ToTable()
                Else
                    Return rep.GetCompetencyStandard(_filter).ToTable()
                End If
            Else
                Dim CompetencyStandards As List(Of CompetencyStandardDTO)
                If Sorts IsNot Nothing Then
                    CompetencyStandards = rep.GetCompetencyStandard(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    CompetencyStandards = rep.GetCompetencyStandard(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If

                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = CompetencyStandards
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, cboCompetencyGroup, cboCompetency, cboLevelNumber)
                    EnableControlAll(False, cboTitle)
                    cboCompetencyGroup.AutoPostBack = True


                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    EnableControlAll(False, cboCompetencyGroup, cboCompetency, cboLevelNumber)
                    EnableControlAll(False, cboTitle)
                    cboCompetencyGroup.AutoPostBack = False


                Case CommonMessage.STATE_EDIT

                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, cboLevelNumber)
                    EnableControlAll(False, cboTitle, cboCompetencyGroup, cboCompetency)
                    cboCompetencyGroup.AutoPostBack = True


                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteCompetencyStandard(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_EFFECT_NOT_DELETE), NotifyType.Error)
                        UpdateControlState()
                    End If

            End Select
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try

            Dim dtData As DataTable
            Using rep As New ProfileRepository
                dtData = rep.GetHU_CompetencyGroupList(True)
                FillRadCombobox(cboCompetencyGroup, dtData, "NAME", "ID")
                dtData = rep.GetTitleList(True)
                FillRadCombobox(cboTitle, dtData, "NAME", "ID")
            End Using

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("COMPETENCY_ID;COMPETENCY_NAME", cboCompetency)
            dic.Add("COMPETENCY_GROUP_ID", cboCompetencyGroup)
            dic.Add("LEVEL_NUMBER", cboLevelNumber)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objCompetencyStandard As New CompetencyStandardDTO
        Dim rep As New ProfileRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    If cboTitle.SelectedValue = "" Then
                        ShowMessage("Bạn phải chọn chức danh", NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_NEW
                    cboCompetency.Items.Clear()
                    ClearControlValue(cboCompetencyGroup, cboCompetency, cboLevelNumber)
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgMain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    cboCompetencyGroup_SelectedIndexChanged(Nothing, Nothing)
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE

                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item("ID").Text))
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "CompetencyStandard")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objCompetencyStandard.COMPETENCY_GROUP_ID = cboCompetencyGroup.SelectedValue
                        objCompetencyStandard.COMPETENCY_ID = cboCompetency.SelectedValue
                        objCompetencyStandard.LEVEL_NUMBER = cboLevelNumber.SelectedValue
                        objCompetencyStandard.TITLE_ID = cboTitle.SelectedValue
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertCompetencyStandard(objCompetencyStandard, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objCompetencyStandard.ID = rgMain.SelectedValue
                                If rep.ModifyCompetencyStandard(objCompetencyStandard, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objCompetencyStandard.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    cboCompetency.Items.Clear()
                    ClearControlValue(cboCompetencyGroup, cboCompetency, cboLevelNumber)
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_ACTIVE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DEACTIVE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cboCompetencyGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompetencyGroup.SelectedIndexChanged
        Dim groupID As Decimal = 0
        Dim dtData
        Try
            If cboCompetencyGroup.SelectedValue <> "" Then
                groupID = cboCompetencyGroup.SelectedValue
            End If
            Using rep As New ProfileRepository
                dtData = rep.GetHU_CompetencyList(groupID)
                FillRadCombobox(cboCompetency, dtData, "NAME", "ID")
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboTitle_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTitle.SelectedIndexChanged
        Try
            rgMain.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
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