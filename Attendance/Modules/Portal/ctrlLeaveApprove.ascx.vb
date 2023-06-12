Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlLeaveApprove
    Inherits Common.CommonView
#Region "Property"
    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Overrides Property MustAuthorize As Boolean = False

    Public ReadOnly Property ProcessApprove As String
        Get
            Return ATConstant.GSIGNCODE_LEAVE
        End Get
    End Property
    Public ReadOnly Property CurrentUser As UserDTO
        Get
            Return LogHelper.CurrentUser
        End Get
    End Property

    Protected Property ListManual As List(Of AT_TIME_MANUALDTO)
        Get
            Return PageViewState(Me.ID & "_ListManual")
        End Get
        Set(ByVal value As List(Of AT_TIME_MANUALDTO))
            PageViewState(Me.ID & "_ListManual") = value
        End Set
    End Property

    Public ReadOnly Property PageId As String
        Get
            ' Lấy Page ID của form đăng ký
            Return Me.ID
        End Get
    End Property
    Property ListComboData As Attendance.AttendanceBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Attendance.AttendanceBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
#End Region

#Region "View"
    Property GridData As List(Of AttendanceBusiness.AT_PORTAL_REG_DTO)
        Get
            Return ViewState(Me.ID & "_GridData")
        End Get
        Set(ByVal value As List(Of AttendanceBusiness.AT_PORTAL_REG_DTO))
            ViewState(Me.ID & "_GridData") = value
        End Set
    End Property

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        rgWaiting.SetFilter()
        rgWaiting.AllowCustomPaging = True
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        If Not IsPostBack Then
            Refresh()
        End If
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            Dim db As New AttendanceRepository

            Dim filter As New AttendanceBusiness.ATRegSearchDTO With {
                .EmployeeIdName = txtEmployee.Text,
                .FromDate = rdFromDateSearch.SelectedDate,
                .ToDate = rdToDateSearch.SelectedDate,
                .Status = 0
            }

            If cboLeaveTypeSearch.SelectedIndex <> -1 Then
                filter.SignId = Decimal.Parse(cboLeaveTypeSearch.SelectedValue)
            End If

            Dim listApprove = db.GetListWaitingForApprove(EmployeeID, ProcessApprove, filter)
            For Each item As AT_PORTAL_REG_DTO In listApprove
                item.LINK_POPUP = "POPUP('/Default.aspx?mid=Attendance&fid=ctrlLeaveHistory&noscroll=1&EmpId=" & item.ID_EMPLOYEE & "&Date=" & Date.Parse(item.FROM_DATE).ToShortDateString() & "')"
            Next
            GridData = listApprove
            grvApproved.Rebind()
            grvDenied.Rebind()
            rgWaiting.Rebind()
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

    Public Overrides Sub BindData()
        LoadComboData()
        rdFromDateSearch.SelectedDate = Date.Now.FirstDateOfMonth()
        rdToDateSearch.SelectedDate = Date.Now.LastDateOfMonth()
    End Sub

#End Region

#Region "Events"
    Private Sub grvApproved_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grvApproved.NeedDataSource
        Try
            Dim listApproved = GridData.Where(Function(p) p.STATUS = 2).ToList

            grvApproved.DataSource = listApproved
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub grvDenied_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grvDenied.NeedDataSource
        Try
            Dim listDenied = GridData.Where(Function(p) p.STATUS = 3).ToList

            grvDenied.DataSource = listDenied
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub rgWaiting_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgWaiting.NeedDataSource
        Try
            Dim listWaiting = GridData.Where(Function(p) p.STATUS = 1).ToList

            rgWaiting.DataSource = listWaiting
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub
    Private Sub grvApproved_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grvApproved.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                Dim item = CType(e.Item, GridDataItem)
                Dim link As LinkButton
                link = CType(item("LINK_POPUP").FindControl("lbtnLink"), LinkButton)
                If item.GetDataKeyValue("LINK_POPUP") Is Nothing Then
                    link.Visible = False
                Else
                    link.OnClientClick = item.GetDataKeyValue("LINK_POPUP")
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub grvDenied_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grvDenied.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                Dim item = CType(e.Item, GridDataItem)
                Dim link As LinkButton
                link = CType(item("LINK_POPUP").FindControl("lbtnLink"), LinkButton)
                If item.GetDataKeyValue("LINK_POPUP") Is Nothing Then
                    link.Visible = False
                Else
                    link.OnClientClick = item.GetDataKeyValue("LINK_POPUP")
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rgWaiting_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgWaiting.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                Dim item = CType(e.Item, GridDataItem)
                Dim link As LinkButton
                link = CType(item("LINK_POPUP").FindControl("lbtnLink"), LinkButton)
                If item.GetDataKeyValue("LINK_POPUP") Is Nothing Then
                    link.Visible = False
                Else
                    link.OnClientClick = item.GetDataKeyValue("LINK_POPUP")
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Refresh()
    End Sub

    Private Sub btnApprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        Dim sAction As String = "LEAVE"
        Dim dtDataUserPHEPNAM As New DataTable
        Dim dtDataUserPHEPBU As New DataTable
        Dim totalDayRes As New DataTable
        Dim totalDayWanApp As New Decimal
        Dim at_entilement As New AT_ENTITLEMENTDTO
        Dim at_compensatory As New AT_COMPENSATORYDTO
        Dim rep As New AttendanceRepository
        Try
            If rgWaiting.MasterTableView.GetSelectedItems().Length = 0 Then
                ShowMessage(Translate("Bạn chưa chọn bản đăng ký cần phê duyệt."), NotifyType.Warning)
                Exit Sub
            End If

            For Each item As GridDataItem In rgWaiting.SelectedItems

                Dim id As Decimal = item.GetDataKeyValue("ID_REGGROUP")
                Dim AppemployeeId As Decimal = item.GetDataKeyValue("ID_EMPLOYEE")

                Dim itemReg = (From p In GridData Where p.ID_REGGROUP = id).FirstOrDefault
                Dim sign = (From p In ListManual Where p.ID = Decimal.Parse(itemReg.ID_SIGN)).FirstOrDefault

                ' Check phép năm
                dtDataUserPHEPNAM = rep.GetTotalPHEPNAM(AppemployeeId, itemReg.TO_DATE.Value.Year, itemReg.ID_SIGN)
                at_entilement = rep.GetPhepNam(AppemployeeId, itemReg.TO_DATE.Value.Year)
                at_compensatory = rep.GetNghiBu(AppemployeeId, itemReg.TO_DATE.Value.Year)
                totalDayRes = rep.GetTotalDAY(AppemployeeId, 251, itemReg.FROM_DATE, itemReg.TO_DATE)
                totalDayWanApp = rep.GetTotalLeaveInYear(AppemployeeId, itemReg.TO_DATE.Value.Year)
                ' nếu là kiểu đăng ký nghỉ phép
                'If sign.MORNING_ID = 251 And sign.AFTERNOON_ID = 251 Then
                '    If dtDataUserPHEPNAM IsNot Nothing And at_entilement IsNot Nothing Then
                '        'If at_entilement.TOTAL_HAVE.Value - (dtDataUserPHEPNAM.Rows(0)(0) + totalDayRes.Rows(0)(0) + totalDayWanApp) < 0 Then
                '        If at_entilement.TOTAL_HAVE.Value - (totalDayRes.Rows(0)(0) + totalDayWanApp) < 0 Then
                '            ShowMessage(Translate("Tổng số ngày nghỉ phép của bạn trong năm nay đã vượt quá mức cho phép."), NotifyType.Error)
                '            Exit Sub
                '        End If
                '    End If
                'ElseIf sign.MORNING_ID = 251 Or sign.AFTERNOON_ID = 251 Then
                '    If dtDataUserPHEPNAM IsNot Nothing And at_entilement IsNot Nothing Then
                '        If at_entilement.TOTAL_HAVE.Value - ((totalDayRes.Rows(0)(0) / 2) + totalDayWanApp) < 0 Then
                '            ShowMessage(Translate("Tổng số ngày nghỉ phép của bạn trong năm nay đã vượt quá mức cho phép."), NotifyType.Error)
                '            Exit Sub
                '        End If
                '    End If
                'End If
                ' nếu là kiểu đăng ký nghỉ bù
                dtDataUserPHEPBU = rep.GetTotalPHEPBU(AppemployeeId, itemReg.TO_DATE.Value.Year, itemReg.ID_SIGN)
                totalDayRes = rep.GetTotalDAY(AppemployeeId, 255, itemReg.FROM_DATE, itemReg.TO_DATE)
                'If sign.MORNING_ID = 255 And sign.AFTERNOON_ID = 255 Then
                '    If dtDataUserPHEPBU IsNot Nothing And at_entilement IsNot Nothing Then
                '        If at_compensatory.TOTAL_HAVE.Value - (dtDataUserPHEPBU.Rows(0)(0) + totalDayRes.Rows(0)(0)) < 0 Then
                '            ShowMessage(Translate("Tổng số ngày nghỉ bù của bạn đã vượt quá mức cho phép"), NotifyType.Error)
                '            Exit Sub
                '        End If
                '    End If
                'ElseIf sign.MORNING_ID = 255 Or sign.AFTERNOON_ID = 255 Then
                '    If dtDataUserPHEPBU IsNot Nothing And at_entilement IsNot Nothing Then
                '        If at_compensatory.TOTAL_HAVE.Value - (dtDataUserPHEPBU.Rows(0)(0) + (totalDayRes.Rows(0)(0) / 2) + totalDayWanApp) < 0 Then
                '            ShowMessage(Translate("Tổng số ngày nghỉ bù của bạn đã vượt quá mức cho phép"), NotifyType.Error)
                '            Exit Sub
                '        End If
                '    End If
                'End If

                Dim db As New AttendanceRepository

                If db.ApprovePortalRegister(id, EmployeeID, 2, txtNote.Text,
                                            Request.Url.Scheme & Uri.SchemeDelimiter & Request.Url.DnsSafeHost,
                                            ProcessApprove) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            Next
            Refresh()

        Catch ex As Exception
            ShowMessage(Translate(ex.Message), NotifyType.Error)
        End Try
    End Sub

    Private Sub btnDeny_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeny.Click
        Try

            If rgWaiting.MasterTableView.GetSelectedItems().Length = 0 Then
                ShowMessage(Translate("Bạn chưa chọn bản đăng ký cần phê duyệt."), NotifyType.Warning)
                Exit Sub
            End If
            If txtNote.Text.Length = 0 Then
                ShowMessage(Translate("Bạn chưa nhập ý kiến"), NotifyType.Warning)
                Exit Sub
            End If
            For Each item As GridDataItem In rgWaiting.SelectedItems
                ' Dim id = Decimal.Parse(rgWaiting.MasterTableView.GetSelectedItems()(0).GetDataKeyValue("ID_REGGROUP").ToString)
                Dim id As Decimal? = item.GetDataKeyValue("ID_REGGROUP")
                Dim db As New AttendanceRepository

                If db.ApprovePortalRegister(id, EmployeeID, 3, txtNote.Text,
                                            Request.Url.Scheme & Uri.SchemeDelimiter & Request.Url.DnsSafeHost,
                                            ProcessApprove) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)

                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            Next
            Refresh()

        Catch ex As Exception
            ShowMessage(Translate(ex.Message), NotifyType.Error)
        End Try
    End Sub
#End Region

#Region "Custom"
    Private Sub LoadComboData()
        Dim rep As New AttendanceRepository
        Try
            If ListComboData Is Nothing Then
                ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
                ListComboData.GET_LIST_TYPE_MANUAL_LEAVE = True
                rep.GetComboboxData(ListComboData)
            End If
            FillRadCombobox(cboLeaveTypeSearch, ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE, "NAME_VN", "ID", True)
            ListManual = ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE
            If ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE.Count > 0 Then
                cboLeaveTypeSearch.SelectedIndex = 0
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, NotifyType.Error)
        End Try
    End Sub
#End Region
End Class