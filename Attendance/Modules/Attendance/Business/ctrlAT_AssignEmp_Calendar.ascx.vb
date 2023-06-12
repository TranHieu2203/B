Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlAT_AssignEmp_Calendar
    Inherits Common.CommonView
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String

    Dim _myLog As New MyLog
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()

#Region "Property"

    Public Property SelectedItemCanNotSchedule As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCanNotSchedule") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCanNotSchedule") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCanNotSchedule")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCanNotSchedule") = value
        End Set
    End Property

    Public Property SelectedItemCanSchedule As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCanSchedule") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCanSchedule") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCanSchedule")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCanSchedule") = value
        End Set
    End Property
    Property flag As Decimal
        Get
            Return ViewState(Me.ID & "_flag")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_flag") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlOrganization.AutoPostBack = True
            ctrlOrganization.LoadDataAfterLoaded = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckBoxes = TreeNodeTypes.None
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
            Dim table As New DataTable

            Dim _filter_PERIOD As New AT_PERIODDTO
            Dim _filter_SIGN_ORG As New AT_SIGNDEFAULT_ORGDTO

            Using rep As New AttendanceRepository
                Dim query = rep.LOAD_PERIODBylinq(_filter_PERIOD)
                If query IsNot Nothing Then
                    table = (From p In query.AsQueryable Select p.YEAR Distinct).Select(Function(p) New AT_ASSIGNEMP_CALENDARDTO With {.YEAR = p}).ToList.ToTable
                    FillRadCombobox(cboYear, table, "YEAR", "YEAR")
                    cboYear.SelectedValue = Date.Now.Year

                    _filter_SIGN_ORG.YEAR = cboYear.SelectedValue
                    Dim querySignOrg = rep.GetAT_SIGNDEFAULT_ORG(_filter_SIGN_ORG)
                    If querySignOrg IsNot Nothing Then
                        table = querySignOrg.ToTable
                        FillRadCombobox(cboWorkSchedule, table, "CALENDAR", "ID")
                    End If
                End If


            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If
            UpdateControlState()
        Catch ex As Exception
            ShowMessage(ex.Message, Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try

            Using rep As New AttendanceRepository
                Select Case CurrentState

                    Case CommonMessage.STATE_NORMAL
                        ClearControlValue(cboWorkSchedule)

                        SelectedItemCanNotSchedule = Nothing
                        SelectedItemCanSchedule = Nothing
                        rgCanNotSchedule.Rebind()
                        rgCanSchedule.Rebind()

                    Case CommonMessage.STATE_NEW
                        Dim _filter As New AT_ASSIGNEMP_CALENDARDTO
                        Dim lst As String
                        If Not IsNumeric(cboYear.SelectedValue) Then
                            ShowMessage(Translate("Mời chọn năm"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If Not IsNumeric(cboWorkSchedule.SelectedValue) Then
                            ShowMessage(Translate("Mời chọn lịch làm việc"), NotifyType.Warning)
                            Exit Sub
                        End If
                        lst = String.Join(",", (From p In SelectedItemCanNotSchedule).ToArray)
                        _filter.YEAR = cboYear.SelectedValue
                        _filter.CALENDAR_ID = cboWorkSchedule.SelectedValue
                        _filter.lstEmp = lst
                        _filter.ORG_ID = ctrlOrganization.CurrentValue
                        _filter.IS_DISSOLVE = ctrlOrganization.IsDissolve
                        Dim RS = rep.InsertEmployeeByCalendarID(_filter)
                        If RS = 4 Then
                            ShowMessage(Translate("Nhân viên đã phát sinh dữ liệu theo ca trong năm."), NotifyType.Error)
                            Exit Sub
                        ElseIf RS = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            Exit Sub
                        Else
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)

                        End If
                        UpdateControlState()

                    Case CommonMessage.STATE_DELETE
                        Dim _filter As New AT_ASSIGNEMP_CALENDARDTO

                        If Not IsNumeric(cboYear.SelectedValue) Then
                            ShowMessage(Translate("Mời chọn năm"), NotifyType.Warning)
                            Exit Sub
                        End If
                        'Get list ID of main page 
                        Dim lst = String.Join(",", (From p In SelectedItemCanSchedule).ToArray)
                        _filter.lstEmp = lst
                        _filter.YEAR = cboYear.SelectedValue
                        Dim RS = rep.DeleteEmployeeByCalendarID(_filter)
                        If RS = 4 Then
                            ShowMessage(Translate("Nhân viên đã phát sinh dữ liệu theo ca trong năm."), NotifyType.Error)
                            Exit Sub

                        ElseIf RS = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            Exit Sub
                        Else
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            UpdateControlState()
                        End If
                End Select

            End Using
        Catch ex As Exception
            ShowMessage(ex.Message, Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub

#End Region

#Region "Event"
    Private Sub rgCanNotSchedule_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCanNotSchedule.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                Dim id = datarow.GetDataKeyValue("EMPLOYEE_ID")
                If SelectedItemCanNotSchedule IsNot Nothing AndAlso SelectedItemCanNotSchedule.Contains(id) Then
                    datarow.Selected = True
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub rgCanNotSchedule_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles rgCanNotSchedule.SortCommand
        GetCanNotScheduleSelected()
    End Sub

    Private Sub rgCanSchedule_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCanSchedule.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                Dim id = datarow.GetDataKeyValue("ID")
                If SelectedItemCanSchedule IsNot Nothing AndAlso SelectedItemCanSchedule.Contains(id) Then
                    datarow.Selected = True
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, Framework.UI.Utilities.NotifyType.Error)
        End Try

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
        Try
            SelectedItemCanSchedule = Nothing
            GetCanScheduleSelected()
            If SelectedItemCanSchedule.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If
            CurrentState = CommonMessage.STATE_DELETE

            ctrlMessageBox.MessageText = "Bạn có chắc chắn xóa " & SelectedItemCanSchedule.Count & " Nhân viên đã gán Lịch làm việc?"
            ctrlMessageBox.ActionName = "DELETE"
            ctrlMessageBox.DataBind()
            ctrlMessageBox.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnDeleteAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteAll.Click
        Try
            SelectedItemCanSchedule = Nothing
            GetCanScheduleSelected(True)
            CurrentState = CommonMessage.STATE_DELETE

            ctrlMessageBox.MessageText = "Bạn có chắc chắn xóa " & SelectedItemCanSchedule.Count & " Nhân viên đã gán Lịch làm việc?"
            ctrlMessageBox.ActionName = "DELETE"
            ctrlMessageBox.DataBind()
            ctrlMessageBox.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        Try
            SelectedItemCanNotSchedule = Nothing
            GetCanNotScheduleSelected()
            If SelectedItemCanNotSchedule.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If
            CurrentState = CommonMessage.STATE_NEW

            ctrlMessageBox.MessageText = "Bạn có chắc chắn muốn Gán " & SelectedItemCanNotSchedule.Count & " Nhân viên vào Lịch làm việc?"
            ctrlMessageBox.ActionName = "INSERT"
            ctrlMessageBox.DataBind()
            ctrlMessageBox.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnInsertAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsertAll.Click
        Try
            SelectedItemCanNotSchedule = Nothing
            GetCanNotScheduleSelected(True)
            CurrentState = CommonMessage.STATE_NEW

            ctrlMessageBox.MessageText = "Bạn có chắc chắn muốn Gán " & SelectedItemCanNotSchedule.Count & " Nhân viên vào Lịch làm việc?"
            ctrlMessageBox.ActionName = "INSERT"
            ctrlMessageBox.DataBind()
            ctrlMessageBox.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = "INSERT" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                UpdateControlState()
            End If
            If e.ActionName = "DELETE" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
#End Region

#Region "Custom"
    Private Sub CreateDataFilterStudent()

        Try
            Using rep As New AttendanceRepository
                Dim _filter As New AT_ASSIGNEMP_CALENDARDTO
                If Not IsNumeric(cboYear.SelectedValue) AndAlso Not IsNumeric(cboWorkSchedule.SelectedValue) Then
                    rgCanSchedule.VirtualItemCount = 0
                    rgCanSchedule.DataSource = New List(Of AT_ASSIGNEMP_CALENDARDTO)
                    Exit Sub
                End If
                SetValueObjectByRadGrid(rgCanSchedule, _filter)
                If IsNumeric(cboYear.SelectedValue) Then
                    _filter.YEAR = cboYear.SelectedValue
                End If
                If cboWorkSchedule.SelectedValue <> "" Then
                    _filter.CALENDAR_ID = cboWorkSchedule.SelectedValue
                End If

                If ctrlOrganization.CurrentValue IsNot Nothing Then
                    _filter.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
                    _filter.IS_DISSOLVE = ctrlOrganization.IsDissolve
                End If

                Dim Sorts As String = rgCanSchedule.MasterTableView.SortExpressions.GetSortString()
                Dim MaximumRows As Decimal = 0
                Dim lstData As List(Of AT_ASSIGNEMP_CALENDARDTO)
                If Sorts IsNot Nothing Then
                    lstData = rep.GetEmployeeByCalendarID(_filter, 0, Integer.MaxValue, MaximumRows, Sorts)
                Else
                    lstData = rep.GetEmployeeByCalendarID(_filter, 0, Integer.MaxValue, MaximumRows)
                End If
                If lstData IsNot Nothing AndAlso lstData.Count > 0 Then
                    'rgCanSchedule.VirtualItemCount = MaximumRows
                    rgCanSchedule.DataSource = lstData
                    txtEmpAssign.Text = MaximumRows
                Else
                    rgCanSchedule.DataSource = New List(Of AT_ASSIGNEMP_CALENDARDTO)
                    txtEmpAssign.Text = 0
                End If
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub CreateDataFilterCanNotSchedule()

        Try
            Using rep As New AttendanceRepository
                Dim _filter As New AT_ASSIGNEMP_CALENDARDTO
                If Not IsNumeric(cboYear.SelectedValue) AndAlso Not IsNumeric(cboWorkSchedule.SelectedValue) Then
                    rgCanNotSchedule.VirtualItemCount = 0
                    rgCanNotSchedule.DataSource = New List(Of AT_ASSIGNEMP_CALENDARDTO)
                    Exit Sub
                End If
                SetValueObjectByRadGrid(rgCanNotSchedule, _filter)
                If IsNumeric(cboYear.SelectedValue) Then
                    _filter.YEAR = cboYear.SelectedValue
                End If
                If cboWorkSchedule.SelectedValue <> "" Then
                    _filter.CALENDAR_ID = cboWorkSchedule.SelectedValue
                End If

                If ctrlOrganization.CurrentValue IsNot Nothing Then
                    _filter.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
                    _filter.IS_DISSOLVE = ctrlOrganization.IsDissolve
                End If

                Dim Sorts As String = rgCanNotSchedule.MasterTableView.SortExpressions.GetSortString()
                Dim MaximumRows As Decimal = 0
                Dim lstData As List(Of AT_ASSIGNEMP_CALENDARDTO)
                If Sorts IsNot Nothing Then
                    lstData = rep.GetEmployeeNotByCalendarID(_filter, 0, Integer.MaxValue, MaximumRows, Sorts)
                Else
                    lstData = rep.GetEmployeeNotByCalendarID(_filter, 0, Integer.MaxValue, MaximumRows)
                End If

                If lstData IsNot Nothing AndAlso lstData.Count > 0 Then
                    'rgCanNotSchedule.VirtualItemCount = MaximumRows
                    rgCanNotSchedule.DataSource = lstData
                    txtEmpNotAssign.Text = MaximumRows
                Else
                    rgCanNotSchedule.DataSource = New List(Of AT_ASSIGNEMP_CALENDARDTO)
                    txtEmpNotAssign.Text = 0
                End If
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub GetCanNotScheduleSelected(Optional ByVal isFull As Boolean = False)
        Try
            If isFull Then
                For Each dr As Telerik.Web.UI.GridDataItem In rgCanNotSchedule.Items
                    Dim id As String = dr.GetDataKeyValue("EMPLOYEE_ID")

                    If Not SelectedItemCanNotSchedule.Contains(id) Then
                        SelectedItemCanNotSchedule.Add(id)
                    Else
                        SelectedItemCanNotSchedule.Remove(id)
                    End If
                Next
            Else
                For Each dr As Telerik.Web.UI.GridDataItem In rgCanNotSchedule.SelectedItems
                    Dim id As String = dr.GetDataKeyValue("EMPLOYEE_ID")
                    If dr.Selected Then
                        If Not SelectedItemCanNotSchedule.Contains(id) Then SelectedItemCanNotSchedule.Add(id)
                    Else
                        If SelectedItemCanNotSchedule.Contains(id) Then SelectedItemCanNotSchedule.Remove(id)
                    End If
                Next

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub GetCanScheduleSelected(Optional ByVal isFull As Boolean = False)
        Try
            If isFull Then
                For Each dr As Telerik.Web.UI.GridDataItem In rgCanSchedule.Items
                    Dim id As String = dr.GetDataKeyValue("ID")
                    If Not SelectedItemCanSchedule.Contains(id) Then
                        SelectedItemCanSchedule.Add(id)
                    Else
                        SelectedItemCanSchedule.Remove(id)
                    End If
                Next
            Else
                For Each dr As Telerik.Web.UI.GridDataItem In rgCanSchedule.SelectedItems
                    Dim id As String = dr.GetDataKeyValue("ID")
                    If dr.Selected Then
                        If Not SelectedItemCanSchedule.Contains(id) Then SelectedItemCanSchedule.Add(id)
                    Else
                        If SelectedItemCanSchedule.Contains(id) Then SelectedItemCanSchedule.Remove(id)
                    End If
                Next

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

    Protected Sub cboYear_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        Try
            Dim dtData As New DataTable
            Dim _filter_SIGN_ORG As New AT_SIGNDEFAULT_ORGDTO
            Using rep As New AttendanceRepository
                _filter_SIGN_ORG.YEAR = cboYear.SelectedValue
                Dim querySignOrg = rep.GetAT_SIGNDEFAULT_ORG(_filter_SIGN_ORG)
                If querySignOrg IsNot Nothing Then
                    dtData = querySignOrg.ToTable
                    FillRadCombobox(cboWorkSchedule, dtData, "CALENDAR", "ID")
                End If
            End Using
            rgCanNotSchedule.Rebind()
            rgCanSchedule.Rebind()
            SelectedItemCanNotSchedule = Nothing
            SelectedItemCanSchedule = Nothing
            ' _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrganization_CheckedNodeChanged(sender As Object, e As EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            rgCanNotSchedule.Rebind()
            rgCanSchedule.Rebind()
            SelectedItemCanNotSchedule = Nothing
            SelectedItemCanSchedule = Nothing
            ' _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ' _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboWorkSchedule_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboWorkSchedule.SelectedIndexChanged
        Try
            rgCanNotSchedule.Rebind()
            rgCanSchedule.Rebind()
            SelectedItemCanNotSchedule = Nothing
            SelectedItemCanSchedule = Nothing
            ' _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ' _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
End Class