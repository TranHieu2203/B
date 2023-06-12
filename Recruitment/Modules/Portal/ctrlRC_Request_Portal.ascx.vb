Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlRC_Request_Portal
    Inherits Common.CommonView
    Protected WithEvents RequestView As ViewBase
    Private store As New RecruitmentStoreProcedure
    Dim dsDataComper As New DataTable
    Dim _myLog As New MyLog
    Dim pathLog As String = _myLog._pathLog
    Public Property EmployeeID As Decimal
    Public Overrides Property MustAuthorize As Boolean = True

#Region "Property"
    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property
    Property IS_INSERT_PROGRAM As Boolean
        Get
            Return ViewState(Me.ID & "_IS_INSERT_PROGRAM")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IS_INSERT_PROGRAM") = value
        End Set
    End Property

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("ORG_ID", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("TITLE_ID", GetType(String))
                dt.Columns.Add("Work_Location_name", GetType(String))
                dt.Columns.Add("Work_Location_ID", GetType(String))
                dt.Columns.Add("SEND_DATE", GetType(String))
                dt.Columns.Add("EXPECTED_JOIN_DATE", GetType(String))
                dt.Columns.Add("LABOR_TYPE_NAME", GetType(String))
                dt.Columns.Add("LABOR_TYPE_ID", GetType(String))
                dt.Columns.Add("RC_RECRUIT_PROPERTY_NAME", GetType(String))
                dt.Columns.Add("RC_RECRUIT_PROPERTY", GetType(String))
                dt.Columns.Add("RECRUIT_REASON_NAME", GetType(String))
                dt.Columns.Add("RECRUIT_REASON_ID", GetType(String))
                dt.Columns.Add("RECRUIT_NUMBER", GetType(String))
                dt.Columns.Add("IS_OVER_LIMIT_NAME", GetType(String))
                dt.Columns.Add("IS_OVER_LIMIT", GetType(String))
                dt.Columns.Add("IS_SUPPORT_NAME", GetType(String))
                dt.Columns.Add("IS_SUPPORT", GetType(String))
                dt.Columns.Add("RECRUIT_REASON", GetType(String))
                dt.Columns.Add("LEARNING_LEVEL_NAME", GetType(String))
                dt.Columns.Add("LEARNING_LEVEL_ID", GetType(String))
                dt.Columns.Add("AGE_FROM", GetType(String))
                dt.Columns.Add("AGE_TO", GetType(String))
                dt.Columns.Add("QUALIFICATION_NAME", GetType(String))
                dt.Columns.Add("QUALIFICATION", GetType(String))
                dt.Columns.Add("LANGUAGE_NAME", GetType(String))
                dt.Columns.Add("LANGUAGE", GetType(String))
                dt.Columns.Add("LANGUAGELEVEL_NAME", GetType(String))
                dt.Columns.Add("LANGUAGELEVEL", GetType(String))
                dt.Columns.Add("LANGUAGESCORES", GetType(String))
                dt.Columns.Add("FOREIGN_ABILITY", GetType(String))
                dt.Columns.Add("EXPERIENCE_NUMBER", GetType(String))
                dt.Columns.Add("GENDER_PRIORITY_NAME", GetType(String))
                dt.Columns.Add("GENDER_PRIORITY", GetType(String))
                dt.Columns.Add("COMPUTER_LEVEL_NAME", GetType(String))
                dt.Columns.Add("COMPUTER_LEVEL", GetType(String))
                dt.Columns.Add("COMPUTER_APP_LEVEL", GetType(String))
                dt.Columns.Add("DESCRIPTION", GetType(String))
                dt.Columns.Add("MAINTASK", GetType(String))
                'dt.Columns.Add("SPECIALSKILLS_NAME", GetType(String))
                'dt.Columns.Add("SPECIALSKILLS", GetType(String))
                dt.Columns.Add("REQUEST_EXPERIENCE", GetType(String))
                dt.Columns.Add("REQUEST_OTHER", GetType(String))
                dt.Columns.Add("STATUS_NAME", GetType(String))
                dt.Columns.Add("STATUS_ID", GetType(String))
                'dt.Columns.Add("REMARK", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
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
            Dim dtData As DataTable
            Using rep As New RecruitmentRepository
                dtData = rep.GetOtherList("PROCESS_STATUS", True)
                FillRadCombobox(cboStatus1, dtData, "NAME", "ID")
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Seperator, ToolbarItem.Submit, ToolbarItem.Export, ToolbarItem.Create, ToolbarItem.Seperator, ToolbarItem.Delete)
            CType(MainToolBar.Items(3), RadToolBarButton).Text = "Gửi Phê duyệt"
            CType(MainToolBar.Items(4), RadToolBarButton).Text = "Xuất Excel"
            CType(MainToolBar.Items(5), RadToolBarButton).Text = "Sao chép"
            CType(MainToolBar.Items(5), RadToolBarButton).CommandName = "COPY_REQUEST"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New RecruitmentRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    'rgData.Rebind()
                Case "ACTION_DELETED"
                    Dim lstDeletes As New List(Of Decimal)
                    Dim flag = False
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                        If item.GetDataKeyValue("IS_APPROVED_CODE") <> "R" Then
                            flag = True
                        End If
                    Next
                    If flag = True Then
                        ShowMessage(Translate("Yêu cầu tuyển dụng đã phát sinh dữ liệu liên quan, không được xóa"), NotifyType.Error)
                        Exit Sub
                    End If
                    If rep.DeleteRequestPortal(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case "SUBMIT"
                    Dim lstApp As New List(Of RequestPortalDTO)
                    Dim strId As String
                    Dim countSuccess As Integer
                    'Dim lstSubMits As String
                    'For Each idx As GridDataItem In rgData.SelectedItems
                    '    lstSubMits += idx.GetDataKeyValue("ID").ToString + ","
                    'Next

                    'If lstSubMits.TrimEnd(",") <> "" AndAlso store.UPDATE_APPROVED_REQUEST_PORTAL(lstSubMits.TrimEnd(",")) > 0 Then
                    '    Refresh("UpdateView")
                    '    UpdateControlState()
                    'Else
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    '    UpdateControlState()
                    'End If
                    If rgData.SelectedItems.Count = 1 Then

                        For Each dr As GridDataItem In rgData.SelectedItems
                            strId = dr.GetDataKeyValue("ID").ToString + ","
                        Next

                        strId = strId.Remove(strId.LastIndexOf(",")).ToString

                        Dim outNumber As Decimal

                        Try
                            outNumber = rep.PRI_PROCESS_APP(EmployeeID, 0, "RECRUITMENT", 0, 0, 0, strId, "")
                        Catch ex As Exception
                            ShowMessage(ex.ToString, NotifyType.Error)
                        End Try

                        If outNumber = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        ElseIf outNumber = 1 Then
                            ShowMessage(Translate("Quy trình phê duyệt chưa được thiết lập"), NotifyType.Success)
                        ElseIf outNumber = 2 Then
                            ShowMessage(Translate("Thao tác xảy ra lỗi,bạn kiểm tra lại quy trình"), NotifyType.Error)
                        ElseIf outNumber = 3 Then
                            ShowMessage(Translate("Nhân viên chưa có thiết lập nhóm chức danh"), NotifyType.Error)
                        End If
                        Dim Is_Exits As Boolean = True
                        If outNumber <> 0 Or outNumber <> 1 Or outNumber <> 2 Then
                            Dim dt = store.GET_APP_ALL_TEMPLATES()
                            If dt IsNot Nothing Then
                                For Each item In dt.Rows
                                    If Decimal.Parse(item("ID")) = outNumber Then
                                        Is_Exits = False
                                        ShowMessage(Translate("Không tồn tại cấp phê duyệt theo template phê duyệt (" + item("TEMPLATE_NAME") + " )"), NotifyType.Warning)
                                        Exit For
                                    End If
                                Next
                            End If
                        End If
                        If Is_Exits Then
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                        End If
                    ElseIf rgData.SelectedItems.Count > 1 Then
                        For Each dr As GridDataItem In rgData.SelectedItems
                            strId = ""
                            strId = dr.GetDataKeyValue("ID").ToString + ","
                            strId = strId.Remove(strId.LastIndexOf(",")).ToString

                            Dim outNumber As Decimal

                            Try
                                outNumber = rep.PRI_PROCESS_APP(EmployeeID, 0, "RECRUITMENT", 0, 0, 0, strId, "")
                            Catch ex As Exception
                                ShowMessage(ex.ToString, NotifyType.Error)
                            End Try

                            Dim Is_Exits As Boolean = True
                            If outNumber <> 0 Or outNumber <> 1 Or outNumber <> 2 Then
                                Dim dt = store.GET_APP_ALL_TEMPLATES()
                                If dt IsNot Nothing Then
                                    For Each item In dt.Rows
                                        If Decimal.Parse(item("ID")) = outNumber Then
                                            Is_Exits = False
                                            'ShowMessage(Translate("Không tồn tại cấp phê duyệt theo template phê duyệt (" + item("TEMPLATE_NAME") + " )"), NotifyType.Warning)
                                            Exit For
                                        End If
                                    Next
                                End If
                            End If
                            If Is_Exits Then
                                'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                countSuccess += 1
                            End If
                        Next
                        If countSuccess > 0 Then
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                        End If
                    End If
            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            If Not IsPostBack Then
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
        Try

            Select Case CType(e.Item, RadToolBarButton).CommandName
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
                        If item.GetDataKeyValue("STATUS_ID") = 1 Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If item.GetDataKeyValue("STATUS_ID") = 2 Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái không phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    CurrentState = CommonMessage.STATE_DELETE
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_SUBMIT
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("IS_APPROVED_CODE") <> "" AndAlso item.GetDataKeyValue("IS_APPROVED_CODE").ToString.Trim.ToUpper = "W" Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng chờ phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    CurrentState = "SUBMIT"
                    ctrlMessageBox.MessageText = "Bạn có muốn gửi duyệt"
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_SUBMIT
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "Đăng ký yêu cầu tuyển dụng Portal")
                            Exit Sub
                        End If
                    End Using
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New RecruitmentRepository
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = "ACTION_DELETED"

                UpdateControlState()

            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_SUBMIT And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged

        Try
            rgData.CurrentPageIndex = 0
            hidOrg.Value = ctrlOrg.CurrentValue
            hidOrgName.Value = ctrlOrg.CurrentText
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
            rwPopup.VisibleOnPageLoad = False
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

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New RequestPortalDTO
        Dim rep As New RecruitmentRepository
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgData.DataSource = New List(Of RequestPortalDTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), _
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve}
            _filter.FROM_DATE = rdFromDate.SelectedDate
            _filter.TO_DATE = rdToDate.SelectedDate
            If cboStatus1.SelectedValue <> "" Then
                _filter.IS_APPROVED = Decimal.Parse(cboStatus1.SelectedValue)
            End If
            _filter.REQUIRER = EmployeeID
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of RequestPortalDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetRequestPortal(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
            Else
                lstData = rep.GetRequestPortal(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
            Return lstData.ToTable
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        rgData.CurrentPageIndex = 0
        rgData.MasterTableView.SortExpressions.Clear()
        rgData.Rebind()
    End Sub
#End Region
End Class