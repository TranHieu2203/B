Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlRC_Request_Approve_Portal
    Inherits Common.CommonView
    Protected WithEvents RequestView As ViewBase
    Private store As New RecruitmentStoreProcedure
    Dim dsDataComper As New DataTable
    Dim _myLog As New MyLog
    Dim pathLog As String = _myLog._pathLog
    Protected WithEvents ctrlCommon_Reject As ctrlCommon_Reject
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
                If dtData IsNot Nothing Then
                    Dim data = dtData.AsEnumerable().Where(Function(f) Not f.Field(Of Decimal?)("ID").HasValue _
                                                            Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.WaitingForApproval).ToString() _
                                                            Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.ApprovedByLM).ToString() _
                                                            Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.UnApprovedByLM).ToString()).CopyToDataTable()
                    FillRadCombobox(cboStatus, data, "NAME", "ID")
                    cboStatus.SelectedValue = 0
                End If
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Approve, ToolbarItem.Reject, ToolbarItem.Seperator, ToolbarItem.Export)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).Text = Translate("Phê duyệt")
            CType(Me.MainToolBar.Items(1), RadToolBarButton).Text = Translate("Không phê duyệt")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New RecruitmentRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    rgData.Rebind()
                Case "ACTIVE"
                    'Dim store As New AttendanceStoreProcedure
                    Dim ID_REGGROUP As Integer
                    Dim IS_SUCCESS As Integer
                    Dim EMP_ID As Integer
                    For Each dr As GridDataItem In rgData.SelectedItems
                        ID_REGGROUP = dr.GetDataKeyValue("ID")
                        EMP_ID = dr.GetDataKeyValue("REQUIRER")
                        Dim result = rep.PRI_PROCESS(LogHelper.CurrentUser.EMPLOYEE_ID, EMP_ID, 0, 1, "RECRUITMENT", "", ID_REGGROUP)

                        If result = 0 Then
                            IS_SUCCESS += 1
                        Else
                            IS_SUCCESS -= 1
                        End If
                    Next

                    If IS_SUCCESS >= 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Refresh("UpdateView")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            If Request.Headers("NoSearch") IsNot Nothing Then
                RadPane3.Visible = False
            End If
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
                Case CommonMessage.TOOLBARITEM_APPROVE
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = "447" Then
                            ShowMessage(Translate("Chỉ được thao tác với đơn chờ phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    CurrentState = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.MessageText = "Bạn có muốn phê duyệt"
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_REJECT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = "447" Then
                            ShowMessage(Translate("Chỉ được thao tác với đơn chờ phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    CurrentState = CommonMessage.TOOLBARITEM_REJECT
                    ctrlCommon_Reject.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "Phê duyệt yêu cầu tuyển dụng Portal")
                            Exit Sub
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
                CurrentState = "ACTIVE"
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged

        Try
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
            _filter.ORG_ID = 1
            'If ctrlOrg.CurrentValue IsNot Nothing Then
            '    _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            'Else
            '    rgData.DataSource = New List(Of RequestPortalDTO)
            '    Exit Function
            'End If
            Dim _param = New ParamDTO With {.ORG_ID = 1,
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve}
            If Request.Headers("NoSearch") IsNot Nothing Then
                _filter.IS_APPROVED = 0
            Else
                If IsNumeric(cboStatus.SelectedValue) Then
                    _filter.IS_APPROVED = Decimal.Parse(cboStatus.SelectedValue)
                Else
                    _filter.IS_APPROVED = 0
                End If
                _filter.FROM_DATE = rdFromDate.SelectedDate
                _filter.TO_DATE = rdToDate.SelectedDate
            End If

            _filter.EMPLOYEE_APPROVED = LogHelper.CurrentUser.EMPLOYEE_ID
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of RequestPortalDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetRequestPortal_Approve(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
            Else
                lstData = rep.GetRequestPortal_Approve(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
            End If

            rgData.VirtualItemCount = MaximumRows
            If lstData Is Nothing Then
                rgData.DataSource = New DataTable
            Else
                rgData.DataSource = lstData
            End If

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
    Private Sub ctrlCommon_Reject_ButtonCommand(ByVal sender As Object, ByVal e As CommandSaveEventArgs) Handles ctrlCommon_Reject.ButtonCommand
        Dim rep As New RecruitmentRepository
        Try
            Dim strComment As String = e.Comment
            Dim ID_REGGROUP As Integer
            Dim IS_SUCCESS As Integer
            Dim EMP_ID As Integer
            For Each dr As GridDataItem In rgData.SelectedItems
                ID_REGGROUP = dr.GetDataKeyValue("ID")
                EMP_ID = dr.GetDataKeyValue("REQUIRER")
                Dim result = rep.PRI_PROCESS(LogHelper.CurrentUser.EMPLOYEE_ID, EMP_ID, 0, 2, "RECRUITMENT", strComment, ID_REGGROUP)

                If result = 0 Then
                    IS_SUCCESS += 1
                Else
                    IS_SUCCESS -= 1
                End If
            Next

            If IS_SUCCESS >= 1 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                Refresh("UpdateView")
            Else
                CurrentState = CommonMessage.STATE_NORMAL
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
            End If
            'rgData.Rebind()
            'UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

End Class