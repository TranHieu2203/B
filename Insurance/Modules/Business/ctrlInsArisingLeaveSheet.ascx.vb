Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports WebAppLog


Public Class ctrlInsArisingLeaveSheet
    Inherits Common.CommonView
    'Private WithEvents viewRegister As ctrlShiftPlanningRegister
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Insurance\Modules\Insurance\Business" + Me.GetType().Name.ToString()

#Region "Property & Variable"
    Public Property popup As RadWindow
    Public Property popupId As String
    Dim com As New CommonProcedureNew

    Private Property LastStartDate As Nullable(Of Date)
        Get
            Return PageViewState("LastStartDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            PageViewState("LastStartDate") = value
        End Set
    End Property

    Private Property LastEndDate As Nullable(Of Date)
        Get
            Return PageViewState("LastEndDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            PageViewState("LastEndDate") = value
        End Set
    End Property

    'Private Property ListSign As List(Of SIGN_DTO)
    '    Get
    '        Return PageViewState(Me.ID & "_ListSign")
    '    End Get
    '    Set(ByVal value As List(Of SIGN_DTO))
    '        PageViewState(Me.ID & "_ListSign") = value
    '    End Set
    'End Property

    Public ReadOnly Property PageId As String
        Get
            Return Me.ID
        End Get
    End Property

    'Private Property ListPeriod As List(Of PERIOD_DTO)
    '    Get
    '        Return ViewState(Me.ID & "_ListPeriod")
    '    End Get
    '    Set(ByVal value As List(Of PERIOD_DTO))
    '        ViewState(Me.ID & "_ListPeriod") = value
    '    End Set
    'End Property

    Public Property InsCompList As DataTable
        Get
            Return ViewState(Me.ID & "_InsCompList")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_InsCompList") = value
        End Set
    End Property

    Public Property SelectedItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_SelectedItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_SelectedItemList") = value
        End Set
    End Property

    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            GetDataCombo()

            rgGridData.AllowCustomPaging = True
            rgGridData.PageSize = Common.Common.DefaultPageSize
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popup.Behaviors = WindowBehaviors.Close
            popupId = popup.ClientID

            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Export)

            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrg.CheckBoxes = TreeNodeTypes.None

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            'Me.rgGridData.SetFilter()
            If Not IsPostBack Then
                UpdateControlState(CommonMessage.STATE_NORMAL)
                UpdateToolbarState(CommonMessage.STATE_NORMAL)
                hidID.Value = "0"
                'ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
                'Call LoadDataGrid()
                Refresh()
                ' UpdateControlState()
            End If
            'rgGridData.Culture = Common.Common.SystemLanguage

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dic As New Dictionary(Of String, Control)

            dic.Add("ID", hidID)

            Utilities.OnClientRowSelectedChanged(rgGridData, dic)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")

        Try
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            'Call LoadDataGrid(True)
            'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub UpdateControlState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgGridData, True)

                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgGridData, False)


                Case CommonMessage.STATE_DELETE

                Case "Nothing"
                    Utilities.EnabledGridNotPostback(rgGridData, True)
            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub UpdateToolbarState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgGridData, True)

                Case CommonMessage.STATE_NEW

                Case CommonMessage.STATE_EDIT

                Case CommonMessage.STATE_DELETE

                Case "Nothing"
                    Utilities.EnabledGridNotPostback(rgGridData, True)

            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Event"

    Protected Sub btnFIND_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFind.Click
        rgGridData.Rebind()
    End Sub

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreate.Click
        Dim rep As New InsuranceRepository

        If rdFromLeave.SelectedDate Is Nothing Then
            ShowMessage(Translate("Từ ngày bắt buộc nhập."), NotifyType.Warning)
            Exit Sub
        End If

        If rdToLeave.SelectedDate Is Nothing Then
            ShowMessage(Translate("Đến ngày bắt buộc nhập."), NotifyType.Warning)
            Exit Sub
        End If

        Dim LeaveType As String = ""
        If cboLeaveType.CheckedItems.Count > 0 Then
            For i As Integer = 0 To cboLeaveType.CheckedItems.Count - 1 Step 1
                LeaveType = LeaveType + "," + cboLeaveType.CheckedItems(i).Value
            Next
            LeaveType = LeaveType + ","
        Else
            LeaveType = ""
        End If

        If rep.GET_INS_ARSING_LEAVE_SHEET(Utilities.ObjToInt(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve, rdFromLeave.SelectedDate, rdToLeave.SelectedDate, LeaveType) Then
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
            rgGridData.Rebind()
        End If

        rep.Dispose()

    End Sub

    Protected Sub btnFastComplate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFastComplate.Click
        Dim rep As New InsuranceRepository

        If rdFromMonth.SelectedDate Is Nothing Then
            ShowMessage(Translate("Tháng báo từ bắt buộc nhập."), NotifyType.Warning)
            Exit Sub
        End If

        If rdToMonth.SelectedDate Is Nothing Then
            ShowMessage(Translate("Tháng báo từ đến bắt buộc nhập."), NotifyType.Warning)
            Exit Sub
        End If

        If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
            ShowMessage(Translate("Bạn chưa chọn dữ liệu để điền nhanh."), NotifyType.Warning)
            Exit Sub
        End If

        Dim strID As String
        For Each grid As GridDataItem In rgGridData.SelectedItems
            Dim id = Decimal.Parse(grid.GetDataKeyValue("ID").ToString())
            strID &= IIf(strID = vbNullString, id, "," & id)
        Next

        Dim fromMonth As Date = New Date(rdFromMonth.SelectedDate.Value.Year, rdFromMonth.SelectedDate.Value.Month, 1)

        Dim toMonth As Date = New Date(rdToMonth.SelectedDate.Value.Year, rdToMonth.SelectedDate.Value.Month, 1)

        If rep.UPDATE_INS_ARSING_LEAVE_SHEET(strID, fromMonth, toMonth) Then
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
            rgGridData.Rebind()
        End If

        rep.Dispose()
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        'Dim objOrgFunction As New OrganizationDTO
        'Dim gID As Decimal
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    Call ResetForm()
                    CurrentState = CommonMessage.STATE_NEW
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                Case CommonMessage.TOOLBARITEM_NEXT
                    Call SaveData()
                    CurrentState = CommonMessage.STATE_NORMAL
                Case CommonMessage.TOOLBARITEM_SAVE
                    If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn dữ liệu để chuyển sang quản lý biến động."), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate("Bạn có muốn chuyển sang quản lý biến động?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_SAVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                    CurrentState = CommonMessage.STATE_NORMAL
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần xóa."), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn xóa dữ liệu ?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                    CurrentState = CommonMessage.STATE_NORMAL
                Case CommonMessage.TOOLBARITEM_EXPORT
                    LoadDataGrid(True)
            End Select
            UpdateControlState(CurrentState)
            UpdateToolbarState(CurrentState)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgGridData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGridData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            LoadDataGrid()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm Load dữ liệu cho rad grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LoadDataGrid(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New InsuranceRepository
        Dim obj As New INS_ARISING_LEAVESHEET_DTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer = 0
            Dim lstSource As List(Of INS_ARISING_LEAVESHEET_DTO)
            Dim Sorts As String = rgGridData.MasterTableView.SortExpressions.GetSortString()

            SetValueObjectByRadGrid(rgGridData, obj)

            If rdFromLeave.SelectedDate IsNot Nothing Then
                obj.START_DATE = rdFromLeave.SelectedDate
            End If

            If rdToLeave.SelectedDate IsNot Nothing Then
                obj.END_DATE = rdToLeave.SelectedDate
            End If

            If rdFromMonth.SelectedDate IsNot Nothing Then
                obj.START_MONTH = New Date(rdFromMonth.SelectedDate.Value.Year, rdFromMonth.SelectedDate.Value.Month, 1)
            End If

            If rdToMonth.SelectedDate IsNot Nothing Then
                obj.END_MONTH = New Date(rdToMonth.SelectedDate.Value.Year, rdToMonth.SelectedDate.Value.Month, 1)
            End If

            Dim LeaveType As String = ""
            If cboLeaveType.CheckedItems.Count > 0 Then
                For i As Integer = 0 To cboLeaveType.CheckedItems.Count - 1 Step 1
                    LeaveType = LeaveType + "," + cboLeaveType.CheckedItems(i).Value
                Next
                LeaveType = LeaveType + ","
            Else
                LeaveType = ""
            End If

            If LeaveType <> "" Then
                obj.MANUAL_ID = LeaveType
            End If

            If Not isFull Then
                If Sorts IsNot Nothing Then
                    lstSource = rep.GetInsArisingLeaveSheet(obj, rgGridData.CurrentPageIndex, rgGridData.PageSize, MaximumRows, Utilities.ObjToInt(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve, 0, "", Sorts)
                Else
                    lstSource = rep.GetInsArisingLeaveSheet(obj, rgGridData.CurrentPageIndex, rgGridData.PageSize, MaximumRows, Utilities.ObjToInt(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve, 0, "")
                End If
            Else
                Return rep.GetInsArisingLeaveSheet(obj, 0, Integer.MaxValue, 0, Utilities.ObjToInt(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve, 0, "").ToTable
            End If
            rgGridData.VirtualItemCount = MaximumRows
            rgGridData.DataSource = lstSource

            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function


    Protected Function RepareDataForAction() As List(Of Decimal)
        Dim lst As New List(Of Decimal)
        For Each dr As Telerik.Web.UI.GridDataItem In rgGridData.SelectedItems
            lst.Add(dr.GetDataKeyValue("ID"))
        Next
        Return lst
    End Function

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New InsuranceBusiness.InsuranceBusinessClient
                Dim strID As String
                For Each grid As GridDataItem In rgGridData.SelectedItems
                    Dim id = Decimal.Parse(grid.GetDataKeyValue("ID").ToString())
                    strID &= IIf(strID = vbNullString, id, "," & id)
                Next

                If rep.DELETE_INS_ARSING_LEAVE_SHEET(strID) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    rgGridData.Rebind()
                End If

            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_SAVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                If SaveData() Then
                    Refresh("UpdateView")
                    ResetForm()
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    rgGridData.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    rgGridData.Rebind()
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub


    Private Sub ctrlOrg_SelectedNodeChanged(sender As Object, e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgGridData.CurrentPageIndex = 0
            rgGridData.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Function & Sub"

    Private Sub ResetForm()
        Try
            hidID.Value = "0"
        Catch ex As Exception
        End Try
    End Sub

    Private Function SaveData() As Boolean
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient

            If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                ShowMessage(Translate("Bạn chưa chọn nội dung cần chuyển sang Quản lý biến động!"), NotifyType.Warning)
                Exit Function
            End If

            Dim isFail As Integer = 0
            Dim isResult As Integer = 0

            If isFail = 0 Then
                Return True
                'Refresh("UpdateView")
            Else
                Return False
                'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Function

    Private Sub SaveNote()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient

            If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                ShowMessage(Translate("Bạn chưa chọn nội dung cần lưu."), NotifyType.Warning)
                Exit Sub
            End If

            Dim isFail As Integer = 0
            Dim isResult As Integer = 0
            For i As Integer = 0 To rgGridData.SelectedItems.Count - 1
                Dim item As GridDataItem = rgGridData.SelectedItems(i)
                Dim id As Integer = Integer.Parse(item("ID").Text)
                Dim txtBox As RadTextBox = DirectCast(item("NOTE").FindControl("rtbNote"), RadTextBox)
                Dim note = txtBox.Text
                isResult = rep.UpdateInsArisingNote(Common.Common.GetUsername(), id, note)
                If isResult = 0 Then
                    isFail = 1
                End If
            Next
            If isFail = 0 Then
                Refresh("UpdateView")
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function DeleteData() As Boolean
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient

            Dim isFail As Integer = 0
            Dim isResult As Boolean
            For i As Integer = 0 To rgGridData.SelectedItems.Count - 1
                Dim item As GridDataItem = rgGridData.SelectedItems(i)
                Dim id As Integer = Integer.Parse(item("ID").Text)
                isResult = rep.DeleteInsArising(Common.Common.GetUsername(), id)
                If isResult = False Then
                    isFail = 1
                End If
            Next
            If isFail = 0 Then
                'Refresh("UpdateView")
                Return True
            Else
                Return False
                'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Function

    Private Sub GetDataCombo()
        Try
            Dim rep As New InsuranceRepository
            ListComboData = New ComboBoxDataDTO
            ListComboData.GET_LIST_TYPE_MANUAL_LEAVE_FULL = True
            rep.GetComboboxData(ListComboData)
            FillDropDownList(cboLeaveType, ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE_FULL, "NAME_VN", "ID", Nothing, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
        target = value
        Return value
    End Function

    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện ItemDataBound của rad grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGridData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgGridData.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("DEP_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC").ToString)
            End If

        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class