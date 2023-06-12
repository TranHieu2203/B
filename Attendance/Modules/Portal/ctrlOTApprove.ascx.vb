Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports Telerik.Web.UI

Public Class ctrlOTApprove
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Property unit As String

    Property RegistrationList As List(Of AT_OT_REGISTRATIONDTO)
        Get
            Return ViewState(Me.ID & "_OT_REGISTRATIONDTOS")
        End Get
        Set(ByVal value As List(Of AT_OT_REGISTRATIONDTO))
            ViewState(Me.ID & "_OT_REGISTRATIONDTOS") = value
        End Set
    End Property

    Property OtRegistrationTotal As Int32
        Get
            Return ViewState(Me.ID & "_OtRegistrationTotal")
        End Get
        Set(ByVal value As Int32)
            ViewState(Me.ID & "_OtRegistrationTotal") = value
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

    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_APPROVE
                    Dim lstApp As New List(Of AT_OT_REGISTRATIONDTO)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        Dim dto As New AT_OT_REGISTRATIONDTO
                        dto.ID = item.GetDataKeyValue("ID")
                        dto.STATUS = PortalStatus.ApprovedByLM
                        dto.ID_REGGROUP = item.GetDataKeyValue("ID_REGGROUP")
                        dto.REGIST_DATE = item.GetDataKeyValue("REGIST_DATE")
                        dto.EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
                        dto.REASON = ""
                        lstApp.Add(dto)
                    Next
                    If Not rep.ApproveOtRegistration(lstApp, LogHelper.CurrentUser.EMPLOYEE_ID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    End If
                    rgMain.Rebind()
            End Select
        Catch ex As Exception

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

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        'rgMain.SetFilter()
        SetFilter(rgMain)
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Approve, ToolbarItem.Reject, ToolbarItem.Export)
            CType(MainToolBar.Items(0), RadToolBarButton).Text = Translate("Phê duyệt bởi QLTT")
            CType(MainToolBar.Items(1), RadToolBarButton).Text = Translate("Không phê duyệt (QLTT)")
            CType(MainToolBar.Items(2), RadToolBarButton).Text = Translate("Xuất Excel")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim dtData As DataTable
        Using rep As New AttendanceRepository
            dtData = rep.GetOtherList("PROCESS_STATUS", True)
            If dtData IsNot Nothing Then
                Dim data = dtData.AsEnumerable().Where(Function(f) Not f.Field(Of Decimal?)("ID").HasValue _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.WaitingForApproval).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.ApprovedByLM).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.UnApprovedByLM).ToString()).CopyToDataTable()
                FillRadCombobox(cboStatus, data, "NAME", "ID")
                cboStatus.SelectedValue = PortalStatus.WaitingForApproval
            End If

            rdRegDateFrom.SelectedDate = New DateTime(DateTime.Now.Year, 1, 1)
            rdRegDateTo.SelectedDate = New DateTime(DateTime.Now.Year, 1, 1).AddMonths(12).AddDays(-1)
        End Using
    End Sub

#End Region

#Region "Event"

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            rgMain.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtDatas, "Approve OT Request (LM)")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_APPROVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim listDataCheck As New List(Of AT_PROCESS_DTO)
                    Dim datacheck As AT_PROCESS_DTO
                    'Kiểm tra các điều kiện trước khi xóa
                    For Each dr As Telerik.Web.UI.GridDataItem In rgMain.SelectedItems
                        If dr.GetDataKeyValue("STATUS") <> PortalStatus.WaitingForApproval Then
                            ShowMessage(Translate("Thao tác này chỉ thực hiện với giờ làm thêm đang chờ phê duyệt, vui lòng chọn đơn khác"), NotifyType.Warning)
                            Exit Sub
                        End If
                        datacheck = New AT_PROCESS_DTO With {
                            .EMPLOYEE_ID = dr.GetDataKeyValue("EMPLOYEE_ID"),
                            .FROM_DATE = dr.GetDataKeyValue("REGIST_DATE"),
                            .FULL_NAME = dr.GetDataKeyValue("FULLNAME")
                        }
                        listDataCheck.Add(datacheck)
                    Next

                    Dim itemError As New AT_PROCESS_DTO
                    Using rep As New AttendanceRepository
                        Dim checkResult = rep.CheckTimeSheetApproveVerify(listDataCheck, "LEAVE", itemError)
                        If Not checkResult Then
                            ShowMessage(String.Format(Translate("TimeSheet of {0} in {1} has been approved"), itemError.FULL_NAME, itemError.FROM_DATE.Value.Month & "/" & itemError.FROM_DATE.Value.Year), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    ctrlMessageBox.MessageText = Translate("Bạn có muốn phê duyệt dữ liệu này?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_REJECT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim listDataCheck As New List(Of AT_PROCESS_DTO)
                    Dim datacheck As AT_PROCESS_DTO
                    'Kiểm tra các điều kiện trước khi xóa
                    For Each dr As Telerik.Web.UI.GridDataItem In rgMain.SelectedItems
                        If dr.GetDataKeyValue("STATUS") <> PortalStatus.WaitingForApproval Then
                            ShowMessage(Translate("Thao tác này chỉ thực hiện với giờ làm thêm đang chờ phê duyệt, vui lòng chọn đơn khác"), NotifyType.Warning)
                            Exit Sub
                        End If
                        datacheck = New AT_PROCESS_DTO With {
                            .EMPLOYEE_ID = dr.GetDataKeyValue("EMPLOYEE_ID"),
                            .FROM_DATE = dr.GetDataKeyValue("REGIST_DATE"),
                            .FULL_NAME = dr.GetDataKeyValue("FULLNAME")
                        }
                        listDataCheck.Add(datacheck)
                    Next

                    Dim itemError As New AT_PROCESS_DTO
                    Using rep As New AttendanceRepository
                        Dim checkResult = rep.CheckTimeSheetApproveVerify(listDataCheck, "LEAVE", itemError)
                        If Not checkResult Then
                            ShowMessage(String.Format(Translate("TimeSheet of {0} in {1} has been approved"), itemError.FULL_NAME, itemError.FROM_DATE.Value.Month & "/" & itemError.FROM_DATE.Value.Year), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    ctrlCommon_Reject.Show()
            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_APPROVE
            UpdateControlState()
        End If
    End Sub

    Protected WithEvents ctrlCommon_Reject As ctrlCommon_Reject
    Private Sub ctrlCommon_Reject_ButtonCommand(ByVal sender As Object, ByVal e As CommandSaveEventArgs) Handles ctrlCommon_Reject.ButtonCommand
        Try
            Dim rep As New AttendanceRepository
            Dim strComment As String = e.Comment

            Dim lstApp As New List(Of AT_OT_REGISTRATIONDTO)
            For idx = 0 To rgMain.SelectedItems.Count - 1
                Dim item As GridDataItem = rgMain.SelectedItems(idx)
                Dim dto As New AT_OT_REGISTRATIONDTO
                dto.ID = item.GetDataKeyValue("ID")
                dto.STATUS = PortalStatus.UnApprovedByLM
                dto.REASON = strComment
                dto.ID_REGGROUP = item.GetDataKeyValue("ID_REGGROUP")
                dto.REGIST_DATE = item.GetDataKeyValue("REGIST_DATE")
                dto.EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
                lstApp.Add(dto)
            Next
            If Not rep.ApproveOtRegistration(lstApp, LogHelper.CurrentUser.EMPLOYEE_ID) Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            End If

            rgMain.Rebind()
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim _filter As New AT_OT_REGISTRATIONDTO
        Try
            _filter.EMPLOYEE_ID = EmployeeID
            _filter.P_MANAGER_ID = LogHelper.CurrentUser.EMPLOYEE_ID
            If rdRegDateFrom.SelectedDate.HasValue Then
                _filter.REGIST_DATE_FROM = rdRegDateFrom.SelectedDate
            End If
            If rdRegDateTo.SelectedDate.HasValue Then
                _filter.REGIST_DATE_TO = rdRegDateTo.SelectedDate
            End If
            If Not String.IsNullOrEmpty(cboStatus.SelectedValue) Then
                _filter.STATUS = cboStatus.SelectedValue
            End If
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetOtRegistration(_filter, Integer.MaxValue, 0, Integer.MaxValue, Sorts).ToTable()
                Else
                    Return rep.GetOtRegistration(_filter, Integer.MaxValue, 0, Integer.MaxValue).ToTable
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.RegistrationList = rep.GetOtRegistration(_filter, Me.OtRegistrationTotal, rgMain.CurrentPageIndex, rgMain.PageSize, Sorts)
                Else
                    Me.RegistrationList = rep.GetOtRegistration(_filter, Me.OtRegistrationTotal, rgMain.CurrentPageIndex, rgMain.PageSize)
                End If
            End If

            rgMain.VirtualItemCount = Me.OtRegistrationTotal
            rgMain.DataSource = Me.RegistrationList

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class