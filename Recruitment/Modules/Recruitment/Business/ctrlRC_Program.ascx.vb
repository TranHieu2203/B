Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_Program
    Inherits Common.CommonView
    Protected WithEvents ProgramView As ViewBase
    Private store As New RecruitmentStoreProcedure()

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
            rgData.PageSize = 50
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        'Try
        '    Dim dtData As DataTable
        '    Using rep As New RecruitmentRepository
        '        dtData = rep.GetOtherList("RC_PROGRAM_STATUS", True)
        '        FillRadCombobox(cboStatus, dtData, "NAME", "ID")
        '    End Using
        'Catch ex As Exception
        '    DisplayException(Me.ViewName, Me.ID, ex)
        'End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            'Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Print)
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Export)
            CType(MainToolBar.Items(0), RadToolBarButton).Text = "Cập nhật tuyển dụng"
            CType(MainToolBar.Items(1), RadToolBarButton).Text = "Xuất Excel"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New RecruitmentRepository
        Try
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                GetDataToCombo()
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
        Try
            Dim dtData As DataTable
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData1 As DataTable
                    Using xls As New ExcelCommon
                        dtData1 = CreateDataFilter(True)
                        If dtData1.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData1, "RC")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = RecruitmentCommon.RC_PLAN_REG_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_APPROVE
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = RecruitmentCommon.RC_PLAN_REG_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_REJECT
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = RecruitmentCommon.RC_PLAN_REG_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_REJECT)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_REJECT
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_PRINT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ' Kiểm tra + lấy thông tin trong database
                    Dim dataItem = TryCast(rgData.SelectedItems(0), GridDataItem)
                    If dataItem IsNot Nothing And dataItem("STAGE_ID") IsNot Nothing Then
                        Using rep As New RecruitmentRepository
                            dtData = rep.XuatToTrinh(Decimal.Parse(dataItem("STAGE_ID").Text))
                            If dtData.Rows.Count = 0 Then
                                ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
                                Exit Sub
                            End If
                        End Using

                        Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder") & "\\Recruitment"
                        ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath), "BM04_TT Ke hoach TD theo dot.doc"),
                                              "BM04_TT Ke hoach TD " & cboRecPeriod.SelectedItem.Text & ".doc",
                                              dtData,
                                              Response)
                    Else
                        ShowMessage(Translate("Chương trình tuyển dụng này chưa cập nhật thông tin đợt tuyển dụng. <br/> Vui lòng cập nhật thông tin đợt tuyển dụng!"), NotifyType.Warning)
                        Exit Sub
                    End If

            End Select

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

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged

        Try
            cboRecPeriod.Text = String.Empty
            Dim tab As DataTable = store.STAGE_GetList(Decimal.Parse(ctrlOrg.CurrentValue), 0)
            FillRadCombobox(cboRecPeriod, tab, "TITLE", "ID")
            cboRecPeriod.Items.Insert(0, New RadComboBoxItem("-- Tất cả -- ", 0))
            cboRecPeriod.SelectedIndex = 0


            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
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

    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        rgData.Rebind()
    End Sub
#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New ProgramDTO
        Dim rep As New RecruitmentRepository
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgData.DataSource = New List(Of ProgramDTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), _
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve}
            _filter.FROM_DATE = rdFromDate.SelectedDate
            _filter.TO_DATE = rdToDate.SelectedDate
            If cboRecPeriod.SelectedValue <> "" AndAlso cboRecPeriod.SelectedValue <> 0 Then
                _filter.STAGE_ID = Decimal.Parse(cboRecPeriod.SelectedValue)
            End If

            Select Case cboRecType.SelectedValue
                Case "REC_TYPE1"
                    _filter.IS_IN_PLAN = True
                Case "REC_TYPE2"
                    _filter.IS_IN_PLAN = False
            End Select

            'If cboRecType.SelectedValue <> "" Then
            '    _filter.RECRUIT_TYPE_ID = cboRecType.SelectedValue
            'End If
            If cboStatus.SelectedValue <> "" Then
                _filter.STATUS_ID = cboStatus.SelectedValue
            End If
            _filter.PERSON_PT_RC = store.GET_EMP_BY_USER(UserLogHelper.CurrentLogUser.ID)
            If store.CHECK_EXIST_SE_CONFIG("PERSONRE_TCTD") = -1 Then
                _filter.IS_INSERT_PRO = True
            Else
                _filter.IS_INSERT_PRO = False
            End If
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of ProgramDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetProgram(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
            Else
                lstData = rep.GetProgram(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
            End If

            'Dim lst = (From p In lstData).ToList

            'If cboRecType.SelectedIndex <> 0 Then
            '    lst = (From p In lst Where p.IS_IN_PLAN = cboRecType.SelectedValue).ToList
            'End If

            'If rdFromDate.SelectedDate IsNot Nothing Then
            '    lst = (From p In lst Where p.SEND_DATE >= rdFromDate.SelectedDate).ToList
            'End If

            'If rdToDate.SelectedDate IsNot Nothing Then
            '    lst = (From p In lst Where p.SEND_DATE <= rdToDate.SelectedDate).ToList
            'End If

            'If cboRecPeriod.SelectedIndex <> 0 Then
            '    lst = (From p In lst Where p.STAGE_ID = cboRecPeriod.SelectedValue).ToList
            'End If

            'If cboStatus.SelectedValue <> String.Empty Then
            '    lst = (From p In lst Where p.STATUS_ID = cboStatus.SelectedValue).ToList
            'End If
            rgData.DataSource = Nothing

            rgData.DataSource = lstData.ToList
            rgData.VirtualItemCount = MaximumRows
            Return lstData.ToTable()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub GetDataToCombo()
        Dim comboBoxDataDto As New ComboBoxDataDTO
        Using rep As New RecruitmentRepository
            ''Load danh sách Loại tuyển dụng
            'comboBoxDataDto.GET_RECRUITMENT_TYPE = True
            'rep.GetComboList(comboBoxDataDto)
            'FillRadCombobox(cboRecType, comboBoxDataDto.LIST_RECRUITMENT_TYPE, "NAME_VN", "CODE", True)

            'Load danh sách Trạng thái
            Dim dtData As DataTable
            dtData = rep.GetOtherList("RC_PROGRAM_STATUS", True)
            FillRadCombobox(cboStatus, dtData, "NAME", "ID")
        End Using


        'Dim tab As DataTable = store.STAGE_GetList(Decimal.Parse(ctrlOrg.CurrentValue), 0)
        'FillRadCombobox(cboRecPeriod, tab, "ORGANIZATIONNAME", "ID")
        'cboRecPeriod.SelectedIndex = 0
    End Sub

#End Region

End Class