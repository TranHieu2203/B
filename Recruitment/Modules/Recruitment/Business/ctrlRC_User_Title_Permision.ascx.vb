Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_User_Title_Permision
    Inherits Common.CommonView
    Protected WithEvents ProgramExamsDtlView As ViewBase

#Region "Property"

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
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ID", hidID)
            Utilities.OnClientRowSelectedChanged(rgData, dic)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub GetDataCombo()
        Try

            Dim rep As New RecruitmentStoreProcedure
            Dim dtData = rep.GET_SE_USER_BY_IS_RC(True)
            FillRadCombobox(cboUser, dtData, "USERNAME", "ID")

            Dim cb_data As New List(Of OtherListDTO)
            cb_data = rep.GetComboList("HU_TITLE_GROUP")
            If cb_data.Count > 0 Then
                FillDropDownList(cboGroupTitle, cb_data, "NAME_VN", "ID", Common.Common.SystemLanguage, False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rgData_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgData.SelectedIndexChanged
        Try
            If rgData.SelectedItems.Count > 0 Then
                cboGroupTitle.ClearCheckedItems()
                cboUser.ClearValue()
                Dim slItem As GridDataItem
                slItem = rgData.SelectedItems(0)
                If IsNumeric(slItem.GetDataKeyValue("ID")) Then
                    hidID.Value = slItem.GetDataKeyValue("ID").ToString
                    cboUser.SelectedValue = slItem.GetDataKeyValue("USER_ID").ToString
                    'cboGroupTitle.SelectedValue = slItem.GetDataKeyValue("GROUP_TITLE_ID").ToString
                    cboGroupTitle.FindItemByValue(slItem.GetDataKeyValue("GROUP_TITLE_ID").ToString).Checked = True
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Delete)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New RecruitmentRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    cboUser.Enabled = False
                    cboGroupTitle.Enabled = False

                    EnabledGridNotPostback(rgData, True)
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    cboUser.Enabled = True
                    cboGroupTitle.Enabled = True
                    EnabledGridNotPostback(rgData, False)
                Case CommonMessage.STATE_DELETE
                    'Dim objDelete As RC_USER_TITLE_PERMISIONDTO
                    'Dim item As GridDataItem = rgData.SelectedItems(0)
                    'objDelete = New RC_USER_TITLE_PERMISIONDTO With {.ID = item.GetDataKeyValue("ID")}
                    'If rep.DeleteUSER_TITLE_PERMISION(objDelete) Then
                    '    Refresh("UpdateView")
                    '    UpdateControlState()
                    'Else
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    '    UpdateControlState()
                    'End If

                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim objDelete As RC_USER_TITLE_PERMISIONDTO
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        objDelete = New RC_USER_TITLE_PERMISIONDTO With {.ID = item.GetDataKeyValue("ID")}
                        rep.DeleteUSER_TITLE_PERMISION(objDelete)
                    Next
                    Refresh("UpdateView")
                    UpdateControlState()
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            If Not IsPostBack Then
                'hidProgramID.Value = Request.Params("PROGRAM_ID")
                'Dim obj = rep.GetProgramByID(New ProgramDTO With {.ID = Decimal.Parse(hidProgramID.Value)})
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
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

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New RecruitmentRepository

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE

                    CurrentState = CommonMessage.STATE_NEW
                    'txtName.Text = ""
                    'rntxtExamsOrder.Value = rep.Get_EXAMS_ORDER(hidProgramID.Value)

                    ClearControlValue(hidID, cboGroupTitle, cboUser)
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        For Each item In cboGroupTitle.CheckedItems
                            Dim obj As New RC_USER_TITLE_PERMISIONDTO
                            obj.USER_ID = cboUser.SelectedValue
                            obj.GROUP_TITLE_ID = item.Value
                            rep.UpdateUSER_TITLE_PERMISION(obj)

                        Next
                        Refresh("UpdateView")
                        'If rep.UpdateProgramExams(obj) Then
                        '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        '    CurrentState = CommonMessage.STATE_NORMAL
                        '    Refresh("UpdateView")
                        'Else
                        '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        'End If
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE
                    'If Decimal.Parse(rntxtExamsOrder.Value) < (Decimal.Parse(rep.Get_EXAMS_ORDER(hidProgramID.Value)) - 1) Then
                    '    ShowMessage(Translate("Bạn phải xóa vòng phỏng vấn cuối cùng trước"), Utilities.NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    ''For Each items As GridDataItem In rgData.SelectedItems
                    ''    Dim STATUS_CODE As String = String.Empty
                    ''    STATUS_CODE = items.GetDataKeyValue("STATUS_CODE").ToString
                    ''Next
                    'Dim item As GridDataItem = rgData.SelectedItems(0)
                    'Dim COUNT_SCAN = CDec(item.GetDataKeyValue("COUNT_SCAN").ToString)
                    'If COUNT_SCAN > 0 Then
                    '    ShowMessage(Translate("Vòng phỏng vấn được lên lịch phỏng vấn cho ứng viên không được xóa."), Utilities.NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
            End Select
            rep.Dispose()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New RC_USER_TITLE_PERMISIONDTO
        Dim rep As New RecruitmentRepository
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            '_filter.RC_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of RC_USER_TITLE_PERMISIONDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GETUSER_TITLE_PERMISION(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
            Else
                lstData = rep.GETUSER_TITLE_PERMISION(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
            End If
            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region



End Class