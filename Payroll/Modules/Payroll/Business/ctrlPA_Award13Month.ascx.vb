Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_Award13Month
    Inherits Common.CommonView
    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Payroll\Modules\Payroll\Business" + Me.GetType().Name.ToString()

#Region "Property"

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STORE_CODE", GetType(String))
                dt.Columns.Add("STORE_ID", GetType(String))
                dt.Columns.Add("PERIOD_T", GetType(String))
                dt.Columns.Add("PERIOD_ID", GetType(String))
                dt.Columns.Add("TARGET_DT", GetType(String))
                dt.Columns.Add("TARGET_UPT", GetType(String))
                dt.Columns.Add("TARGET_CON", GetType(String))
                dt.Columns.Add("MBS_TARGET", GetType(String))
                dt.Columns.Add("DAYS_TARGET_NUMBER", GetType(String))
                dt.Columns.Add("TARGET_GROUP_NAME", GetType(String))
                dt.Columns.Add("TARGET_GROUP", GetType(String))
                dt.Columns.Add("RR6_TARGET", GetType(String))
                dt.Columns.Add("SLBILL_TARGET", GetType(String))
                dt.Columns.Add("IS_STORE_VALIDATE", GetType(String))
                dt.Columns.Add("NOTE", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    Property lstAward As List(Of PA_Award_13MonthDTO)
        Get
            Return ViewState(Me.ID & "_lstAward")
        End Get
        Set(ByVal value As List(Of PA_Award_13MonthDTO))
            ViewState(Me.ID & "_lstAward") = value
        End Set
    End Property

    Property isAutofill As Decimal
        Get
            Return ViewState(Me.ID & "_isAutofill")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_isAutofill") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Refresh()
            UpdateControlState()
            rgMain.SetFilter()
            rgMain.AllowCustomPaging = True
            rgMain.PageSize = Common.Common.DefaultPageSize
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            rgMain.AllowCustomPaging = True
            rgMain.SetFilter()
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Lock, ToolbarItem.Unlock, ToolbarItem.Calculate, _
                                       ToolbarItem.Export, ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Delete)

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("RE_CAL",
                                                                     ToolbarIcons.Calculator,
                                                                     ToolbarAuthorize.Special1,
                                                                     Translate("Tính lại")))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("UPDATE_PERIOD",
                                                                     ToolbarIcons.Edit,
                                                                     ToolbarAuthorize.Special2,
                                                                     Translate("Cập nhật")))
            CType(MainToolBar.Items(5), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
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
                End Select
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>  Load data len grid </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        CreateDataFilter = Nothing
        Dim rep As New PayrollRepository
        Dim _filter As New PA_Award_13MonthDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If isAutofill = -1 Then
                rgMain.DataSource = Me.lstAward
                isAutofill = 0
                Exit Function
            End If
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            Dim lstOrg = ctrlOrganization.CheckedValueKeys
            If cbYear.SelectedValue <> "" Then
                _filter.AWARD_YEAR = cbYear.SelectedValue
            End If
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetAward13Month(_filter, lstOrg, Sorts).ToTable()
                Else
                    Return rep.GetAward13Month(_filter, lstOrg).ToTable()
                End If
            Else
                Me.lstAward = New List(Of PA_Award_13MonthDTO)
                If Sorts IsNot Nothing Then
                    Me.lstAward = rep.GetAward13Month(_filter, lstOrg, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    Me.lstAward = rep.GetAward13Month(_filter, lstOrg, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If
                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = Me.lstAward
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function


    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>
    ''' Load control, Khoi tao popup tim Ma Nhan Vien, Tim Don Vi To Chuc
    ''' Set Trang thai control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New PayrollRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)

                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.DeleteAward13Month(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_EFFECT_NOT_DELETE), NotifyType.Error)
                        UpdateControlState()
                    End If

            End Select
            rep.Dispose()

            UpdateToolbarState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Fill du lieu len combobox</summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim repS As New PayrollStoreProcedure
        Try
            Dim dtData As DataTable
            Using rep As New PayrollRepository
                dtData = repS.GetPeriodNameAndYear(True).Tables(1)
                FillRadCombobox(cbYear, dtData, "YEAR", "YEAR")
                FillRadCombobox(cboYearAllow, dtData, "YEAR", "YEAR")

            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> Event xử lý sự kiện khi ấn button trên MsgBox </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_LOCK And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim lstID As New List(Of Decimal)
                For Each item As GridDataItem In rgMain.SelectedItems
                    lstID.Add(item.GetDataKeyValue("ID"))
                Next
                If rep.ChangeStatusAward13Month(lstID, -1) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgMain.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_UNLOCK And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim lstID As New List(Of Decimal)
                For Each item As GridDataItem In rgMain.SelectedItems
                    lstID.Add(item.GetDataKeyValue("ID"))
                Next
                If rep.ChangeStatusAward13Month(lstID, 0) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgMain.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As New PA_TARGET_STOREDTO
        Dim rep As New PayrollRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim commonRes As New CommonRepository

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "Award 13")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim lst As New List(Of PA_Award_13MonthDTO)
                    For Each item As GridDataItem In rgMain.EditItems
                        If item.Edit = True Then
                            Dim edit = CType(item, GridEditableItem)
                            Dim rnNONSALARY_UNITEDIT As RadNumericTextBox = CType(edit("NONSALARY_UNITEDIT").Controls(1), RadNumericTextBox)
                            Dim txtNOTE As RadTextBox = CType(edit("NOTE").Controls(1), RadTextBox)

                            Dim objAw As New PA_Award_13MonthDTO
                            With objAw
                                .ID = item.GetDataKeyValue("ID")
                                .NOTE = txtNOTE.Text
                                .NONSALARY_UNITEDIT = rnNONSALARY_UNITEDIT.Value
                            End With
                            lst.Add(objAw)
                        End If
                    Next

                    If rep.UpdateAward13Month(lst) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        For Each item As GridDataItem In rgMain.EditItems
                            item.Edit = False
                        Next

                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                        rgMain.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        Exit Sub
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    For Each item As GridDataItem In rgMain.EditItems
                        item.Edit = False
                    Next

                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                    rgMain.Rebind()

                Case CommonMessage.TOOLBARITEM_LOCK
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_LOCK)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_LOCK
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_UNLOCK
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_UNLOCK)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_UNLOCK
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARTIEM_CALCULATE
                    If cbYear.SelectedValue = "" Then
                        ShowMessage(Translate("Chưa chọn năm"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If ctrlOrganization.CheckedValueKeys.Count = 0 Then
                        ShowMessage(Translate("Chưa chọn phòng ban"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rep.CAL_AWARD_13MONTH(ctrlOrganization.CheckedValueKeys, cbYear.SelectedValue) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        Exit Sub
                    End If
                Case "RE_CAL"
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstID As New List(Of Decimal)
                    For Each item As GridDataItem In rgMain.SelectedItems
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.RE_CAL_AWARD_13MONTH(lstID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        Exit Sub
                    End If
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgMain.SelectedItems
                        item.Edit = True
                    Next
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                    rgMain.Rebind()
                Case "UPDATE_PERIOD"
                    Dim lst As New List(Of PA_Award_13MonthDTO)
                    For Each item As GridDataItem In rgMain.SelectedItems

                        Dim objAw As New PA_Award_13MonthDTO
                        With objAw
                            .ID = item.GetDataKeyValue("ID")
                            .PERIOD_ID = item.GetDataKeyValue("PERIOD_ID")
                        End With
                        lst.Add(objAw)
                    Next

                    If rep.UpdateAward13MonthPeriod(lst) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                        rgMain.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        Exit Sub
                    End If
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Event click của button Tìm kiếm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgMain.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgMain_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgMain.ItemDataBound
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Try
                If e.Item.Edit Then
                    Dim edit = CType(e.Item, GridEditableItem)
                    Dim txtNOTE As New RadTextBox
                    Dim rnNONSALARY_UNITEDIT As New RadNumericTextBox
                    txtNOTE = CType(edit.FindControl("txtNOTE"), RadTextBox)
                    rnNONSALARY_UNITEDIT = CType(edit.FindControl("rnNONSALARY_UNITEDIT"), RadNumericTextBox)
                    Dim id = edit.GetDataKeyValue("ID")

                    Dim item = (From p In lstAward Where p.ID = id).FirstOrDefault
                    If item IsNot Nothing Then
                        txtNOTE.Text = item.NOTE
                        rnNONSALARY_UNITEDIT.Value = item.NONSALARY_UNITEDIT
                    End If
                End If
                If dtData IsNot Nothing Then dtData.Dispose()
            Catch ex As Exception
                DisplayException(Me.ViewName, Me.ID, ex)
            End Try
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub btnAutoFill_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAutoFill.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If cboPeriodAllow.SelectedValue = "" Then
                ShowMessage(Translate("Chưa chọn tháng chi thưởng"), NotifyType.Warning)
                Exit Sub
            End If

            If rgMain.SelectedItems.Count < 1 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If
            For Each item As GridDataItem In rgMain.SelectedItems
                Dim obj = (From p In Me.lstAward Where p.ID = item.GetDataKeyValue("ID")).FirstOrDefault
                obj.PERIOD_ID = cboPeriodAllow.SelectedValue
                obj.PERIOD_NAME = cboPeriodAllow.SelectedItem.Text
            Next
            isAutofill = -1
            rgMain.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub cboYearAllow_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboYearAllow.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            cboPeriodAllow.Items.Clear()
            If cboYearAllow.SelectedValue <> "" Then
                Dim dtData As New DataTable
                Dim rep As New PayrollRepository
                dtData = rep.GetPeriod(cboYearAllow.SelectedValue)
                FillRadCombobox(cboPeriodAllow, dtData, "PERIOD_T", "ID", False)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Cập nhật trạng thái cho Toolbar</summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ChangeToolbarState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region
End Class