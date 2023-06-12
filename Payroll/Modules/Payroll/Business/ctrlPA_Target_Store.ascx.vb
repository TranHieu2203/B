Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_Target_Store
    Inherits Common.CommonView
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup
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
                dt.Columns.Add("TARGET_DT_REDUCE", GetType(String))
                dt.Columns.Add("DAYS_TARGET_NUMBER", GetType(String))
                dt.Columns.Add("UPDATE", GetType(String))
                dt.Columns.Add("NOTE", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get

        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property


    Property dtTargetGroup As DataTable
        Get
            Return ViewState(Me.ID & "_dtTargetGroup")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtTargetGroup") = value
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

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete, ToolbarItem.Export, ToolbarItem.ExportTemplate, ToolbarItem.Import, ToolbarItem.Calculate)

            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(6), RadToolBarButton).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(7), RadToolBarButton).Text = Translate("Nhập file mẫu")
            CType(Me.MainToolBar.Items(8), RadToolBarButton).Text = Translate("Tải dữ liệu")
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
                        ClearControlValue(hidStoreID, hidPeriodID, cbMonth, cbYear, cbTargetGroup, txtStore, rnTarget_CON, rnTarget_DT, rnTarget_UPT, txtNote, rnMBS_Target, rnRR6_Target, rnSLBill_Target, ntxtTargetDtReduce, ntxtDayTargetNumber)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        ClearControlValue(hidStoreID, hidPeriodID, cbMonth, cbYear, cbTargetGroup, txtStore, rnTarget_CON, rnTarget_DT, rnTarget_UPT, txtNote, rnMBS_Target, rnRR6_Target, rnSLBill_Target, ntxtTargetDtReduce, ntxtDayTargetNumber)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                        ClearControlValue(hidStoreID, hidPeriodID, cbMonth, cbYear, cbTargetGroup, txtStore, rnTarget_CON, rnTarget_DT, rnTarget_UPT, txtNote, rnMBS_Target, rnRR6_Target, rnSLBill_Target, ntxtTargetDtReduce, ntxtDayTargetNumber)
                    Case ""
                        cbYear.AutoPostBack = False
                        cbMonth.AutoPostBack = False
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
        Dim _filter As New PA_TARGET_STOREDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim _param = New ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue,
                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve}
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GET_PA_TARGET_STORE(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GET_PA_TARGET_STORE(_filter, _param).ToTable()
                End If
            Else
                Dim PA_TARGET_STORE As List(Of PA_TARGET_STOREDTO)
                If Sorts IsNot Nothing Then
                    PA_TARGET_STORE = rep.GET_PA_TARGET_STORE(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, _param, MaximumRows, Sorts)
                Else
                    PA_TARGET_STORE = rep.GET_PA_TARGET_STORE(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, _param, MaximumRows)
                End If
                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = PA_TARGET_STORE
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
            LoadPopup(isLoadPopup)
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, hidStoreID, hidPeriodID, cbMonth, cbYear, cbTargetGroup, rnTarget_CON, rnTarget_DT, rnTarget_UPT, txtNote, btnFindOrg, rnMBS_Target, rnRR6_Target, rnSLBill_Target, ntxtTargetDtReduce, ntxtDayTargetNumber)

                    cbTargetGroup.AutoPostBack = True
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    EnableControlAll(False, hidStoreID, hidPeriodID, cbMonth, cbYear, cbTargetGroup, txtStore, rnTarget_CON, rnTarget_DT, rnTarget_UPT, txtNote, btnFindOrg, rnMBS_Target, rnRR6_Target, rnSLBill_Target, ntxtTargetDtReduce, ntxtDayTargetNumber)
                    cbTargetGroup.AutoPostBack = False
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, hidStoreID, hidPeriodID, cbMonth, cbYear, cbTargetGroup, rnTarget_CON, rnTarget_DT, rnTarget_UPT, txtNote, btnFindOrg, rnMBS_Target, rnRR6_Target, rnSLBill_Target, ntxtTargetDtReduce, ntxtDayTargetNumber)
                    cbTargetGroup.AutoPostBack = True
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)

                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.DELETE_PA_TARGET_STORE(lstDeletes) Then
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
                dtData = repS.GetPeriodNameAndYear(True).Tables(0)
                FillRadCombobox(cbMonth, dtData, "PERIOD_NAME", "PERIOD_NAME")
                dtTargetGroup = rep.GetOtherList("TARGET_GROUP", False)
                dtData = rep.GetOtherList("TARGET_GROUP", True)
                FillRadCombobox(cbTargetGroup, dtData, "NAME", "ID")
                dtData = repS.GetPeriodNameAndYear(True).Tables(1)
                FillRadCombobox(cbYear, dtData, "YEAR", "YEAR")
            End Using

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("TARGET_GROUP", cbTargetGroup)
            dic.Add("PERIOD_YEAR", cbYear)
            dic.Add("PERIOD_NAME", cbMonth)
            dic.Add("PERIOD_ID", hidPeriodID)
            dic.Add("STORE_CODE", hidStoreID)
            dic.Add("STORE_NAME", txtStore)
            dic.Add("TARGET_DT", rnTarget_DT)
            dic.Add("TARGET_CON", rnTarget_CON)
            dic.Add("DAYS_TARGET_NUMBER", ntxtDayTargetNumber)
            dic.Add("TARGET_DT_REDUCE", ntxtTargetDtReduce)
            dic.Add("TARGET_UPT", rnTarget_UPT)
            dic.Add("MBS_TARGET", rnMBS_Target)
            dic.Add("RR6_TARGET", rnRR6_Target)
            dic.Add("SLBILL_TARGET", rnSLBill_Target)
            dic.Add("NOTE", txtNote)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
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

        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
                ClearControlValue(hidStoreID, hidPeriodID, cbMonth, cbYear, cbTargetGroup, txtStore, rnTarget_CON, rnTarget_DT, rnTarget_UPT, txtNote, rnMBS_Target, rnRR6_Target, rnSLBill_Target, ntxtTargetDtReduce, ntxtDayTargetNumber)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlFind_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrgPopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, 0, Nothing, "ctrlFind_CancelClick")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
    Private Sub cbTargetGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbTargetGroup.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New PayrollStoreProcedure
        Try
            If cbTargetGroup.SelectedValue <> "" Then
                Dim code As String = rep.GET_TARGET_GROUP_CODE(Convert.ToDecimal(cbTargetGroup.SelectedValue)).Rows(0).Item(0).ToString
                If code = "TARGET_CH" Then
                    reqStore.Visible = True
                Else
                    reqStore.Visible = False
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, 0, Nothing, "ctrlFind_CancelClick")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Dim orgItem = ctrlOrgPopup.CurrentItemDataObject
            Dim dtData As DataTable = Nothing
            If orgItem IsNot Nothing Then
                hidStoreID.Value = e.CurrentValue
                txtStore.Text = orgItem.CODE
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindOrg.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlOrgPopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
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
        Dim gID As Decimal
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim commonRes As New CommonRepository

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(hidStoreID, hidPeriodID, cbMonth, cbYear, cbTargetGroup, txtStore, rnTarget_CON, rnTarget_DT, rnTarget_UPT, txtNote, rnMBS_Target, rnRR6_Target, rnSLBill_Target, ntxtTargetDtReduce, ntxtDayTargetNumber)
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
                    ClearControlValue(True, hidStoreID, hidPeriodID, cbMonth, cbYear, cbTargetGroup, txtStore, rnTarget_CON, rnTarget_DT, rnTarget_UPT, txtNote, rnMBS_Target, rnRR6_Target, rnSLBill_Target, ntxtTargetDtReduce, ntxtDayTargetNumber)

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "PA_TARGET_STORE")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        obj.NOTE = txtNote.Text.Trim
                        If rnSLBill_Target.Text.Trim <> "" Then
                            obj.SLBILL_TARGET = rnSLBill_Target.Value
                        End If
                        If rnTarget_UPT.Text.Trim <> "" Then
                            obj.TARGET_UPT = rnTarget_UPT.Value
                        End If
                        If rnTarget_DT.Text.Trim <> "" Then
                            obj.TARGET_DT = rnTarget_DT.Value
                        End If
                        If rnTarget_CON.Text.Trim <> "" Then
                            obj.TARGET_CON = rnTarget_CON.Value
                        End If
                        If ntxtTargetDtReduce.Text.Trim <> "" Then
                            obj.TARGET_DT_REDUCE = ntxtTargetDtReduce.Value
                        End If
                        If ntxtDayTargetNumber.Text.Trim <> "" Then
                            obj.DAYS_TARGET_NUMBER = ntxtDayTargetNumber.Value
                        End If
                        If rnMBS_Target.Text.Trim <> "" Then
                            obj.MBS_TARGET = rnMBS_Target.Value
                        End If
                        If rnRR6_Target.Text.Trim <> "" Then
                            obj.RR6_TARGET = rnRR6_Target.Value
                        End If
                        If hidStoreID.Value <> "" Then
                            obj.STORE_CODE = hidStoreID.Value
                        End If
                        If cbTargetGroup.SelectedValue <> "" Then
                            obj.TARGET_GROUP = cbTargetGroup.SelectedValue
                            Dim targetCheck = (From p In dtTargetGroup Where p("ID") = cbTargetGroup.SelectedValue).FirstOrDefault
                            If targetCheck IsNot Nothing AndAlso targetCheck("CODE").ToString.ToUpper.Equals("TARGET_CH") AndAlso IsNothing(hidStoreID.Value) Then
                                ShowMessage(Translate("Phải chọn mã cửa hàng cho Nhóm target 'Cửa hàng'!!"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                        If cbMonth.SelectedValue <> "" And cbYear.SelectedValue <> "" Then
                            Dim repS As New PayrollStoreProcedure
                            Dim dtData As DataTable
                            dtData = repS.GetPeriodByNameAndYear(cbYear.SelectedValue, cbMonth.SelectedValue)
                            If dtData.Rows.Count > 0 Then
                                hidPeriodID.Value = dtData(0).Item(0)
                                obj.PERIOD_ID = hidPeriodID.Value
                            Else
                                ShowMessage("Kỳ công không tồn tại", Utilities.NotifyType.Error)
                                ClearControlValue(True, hidStoreID, hidPeriodID, cbMonth, cbYear, txtStore, rnTarget_CON, ntxtDayTargetNumber, ntxtTargetDtReduce, rnTarget_DT, rnTarget_UPT, txtNote)
                                CurrentState = CommonMessage.STATE_NORMAL
                                Refresh("Cancel")
                                UpdateControlState()
                            End If
                        End If

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                obj.ID = 0
                                If rep.VALIDATE_PA_TARGET_STORE(obj) Then
                                    ShowMessage("Dữ liệu đã tồn tại", Utilities.NotifyType.Error)
                                ElseIf rep.INSERT_PA_TARGET_STORE(obj, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If

                            Case CommonMessage.STATE_EDIT
                                obj.ID = rgMain.SelectedValue

                                Dim lst As New List(Of Decimal)
                                lst.Add(obj.ID)
                                If rep.VALIDATE_PA_TARGET_STORE(obj) Then
                                    ShowMessage("Dữ liệu đã tồn tại", Utilities.NotifyType.Error)
                                ElseIf rep.MODIFY_PA_TARGET_STORE(obj, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = obj.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select

                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgMain')")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ClearControlValue(True, hidStoreID, hidPeriodID, cbMonth, cbYear, cbTargetGroup, txtStore, rnTarget_CON, ntxtTargetDtReduce, ntxtDayTargetNumber, rnTarget_DT, rnTarget_UPT, txtNote, rnMBS_Target, rnRR6_Target, rnSLBill_Target)
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim store As New PayrollStoreProcedure
                    Dim dsData As DataSet = store.GET_PA_TARGET_STORE_IMPORT_DATA()
                    ExportTemplate("Payroll\Import\Template_Import_PA_TARGET_STORE.xlsx", _
                                              dsData, Nothing, _
                                              "Template_Import_PA_TARGET_STORE" & Format(Date.Now, "yyyyMMdd"))
                Case CommonMessage.TOOLBARTIEM_CALCULATE
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xlsx")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            TableMapping(dsDataPrepare.Tables(0))
            For Each rows As DataRow In dsDataPrepare.Tables(0).Rows
                If String.IsNullOrEmpty(rows("STORE_ID").ToString) And String.IsNullOrEmpty(rows("PERIOD_ID").ToString) Then Continue For
                Dim newRow As DataRow = dtData.NewRow
                newRow("STORE_CODE") = rows("STORE_CODE")
                newRow("STORE_ID") = rows("STORE_ID")
                newRow("PERIOD_T") = rows("PERIOD_T")
                newRow("PERIOD_ID") = rows("PERIOD_ID")
                newRow("TARGET_DT") = rows("TARGET_DT")
                newRow("TARGET_UPT") = rows("TARGET_UPT")
                newRow("TARGET_CON") = rows("TARGET_CON")
                newRow("TARGET_GROUP_NAME") = rows("TARGET_GROUP_NAME")
                newRow("TARGET_GROUP") = rows("TARGET_GROUP")
                newRow("MBS_TARGET") = rows("MBS_TARGET")
                newRow("RR6_TARGET") = rows("RR6_TARGET")
                newRow("SLBILL_TARGET") = rows("SLBILL_TARGET")
                newRow("DAYS_TARGET_NUMBER") = rows("DAYS_TARGET_NUMBER")
                newRow("TARGET_DT_REDUCE") = rows("TARGET_DT_REDUCE")
                newRow("UPDATE") = rows("UPDATE")
                newRow("NOTE") = rows("NOTE")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, True)
                DocXml = sw.ToString
                Dim sp As New PayrollStoreProcedure
                If sp.IMPORT_PA_TARGET_STORE(DocXml) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                    rgMain.Rebind()
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
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

    ''' <summary>
    ''' Event ctrlOrganization SelectedNodeChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            rgMain.CurrentPageIndex = 0
            rgMain.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Custom"

    Function loadToGrid() As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New Attendance.AttendanceBusiness.AttendanceBusinessClient
        Dim rep2 As New PayrollRepository
        Dim reps As New PayrollStoreProcedure
        Dim obj As New PA_TARGET_STOREDTO
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            dtError = dtData.Clone
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa chọn Tháng bán hàng"
                ImportValidate.EmptyValue("PERIOD_T", row, rowError, isError, sError)
                sError = "Chưa chọn Nhóm Target"
                ImportValidate.EmptyValue("TARGET_GROUP_NAME", row, rowError, isError, sError)
                sError = "Chưa nhập Target_DT"
                ImportValidate.EmptyValue("TARGET_DT", row, rowError, isError, sError)
                If Not IsDBNull(row("TARGET_GROUP")) AndAlso Not String.IsNullOrEmpty(row("TARGET_GROUP")) Then
                    If IsNumeric(row("TARGET_GROUP")) Then
                        Dim code As String = reps.GET_TARGET_GROUP_CODE(Convert.ToDecimal(row("TARGET_GROUP"))).Rows(0).Item(0).ToString
                        If code = "TARGET_CH" Then
                            sError = "Chưa chọn Mã Cửa hàng"
                            ImportValidate.EmptyValue("STORE_CODE", row, rowError, isError, sError)
                            row("IS_STORE_VALIDATE") = "1"
                        Else
                            row("IS_STORE_VALIDATE") = "0"
                        End If
                    End If
                End If

                If Not IsDBNull(row("UPDATE")) AndAlso row("UPDATE") = "1" AndAlso IsNumeric(row("STORE_ID")) AndAlso IsNumeric(row("PERIOD_ID")) Then
                    If reps.GET_STORE_CODE_PERIOD_EXITS(row("STORE_ID"), row("PERIOD_ID")) = 0 Then
                        sError = "Không tồn tại cửa hàng và tháng cần cập nhật thông tin"
                        ImportValidate.IsValidNumber("UPDATE", row, rowError, isError, sError)
                    End If
                End If

                If Not IsDBNull(row("TARGET_GROUP")) AndAlso Not String.IsNullOrEmpty(row("TARGET_GROUP")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("TARGET_GROUP", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("TARGET_DT")) AndAlso Not String.IsNullOrEmpty(row("TARGET_DT")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("TARGET_DT", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("TARGET_UPT")) AndAlso Not String.IsNullOrEmpty(row("TARGET_UPT")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("TARGET_UPT", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("TARGET_CON")) AndAlso Not String.IsNullOrEmpty(row("TARGET_CON")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("TARGET_CON", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("DAYS_TARGET_NUMBER")) AndAlso Not String.IsNullOrEmpty(row("DAYS_TARGET_NUMBER")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("DAYS_TARGET_NUMBER", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("TARGET_DT_REDUCE")) AndAlso Not String.IsNullOrEmpty(row("TARGET_DT_REDUCE")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("TARGET_DT_REDUCE", row, rowError, isError, sError)
                End If

                If isError Then
                    If IsDBNull(rowError("PERIOD_T")) Then
                        rowError("PERIOD_T") = row("PERIOD_T").ToString
                    End If
                    dtError.Rows.Add(rowError)
                End If
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("PA_TARGET_STORE") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_PA_TARGET_STORE_Error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            If isError Then
                Return False
            Else
                Return True
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
    Private Sub TableMapping(ByVal dtdata As DataTable)
        Dim row As DataRow = dtdata.Rows(0)
        Dim index As Integer = 0
        For Each cols As DataColumn In dtdata.Columns
            Try
                cols.ColumnName = row(index)
                index += 1
                If index > row.ItemArray.Length - 1 Then Exit For
            Catch ex As Exception
                Exit For
            End Try
        Next
        dtdata.Rows(0).Delete()
        dtdata.AcceptChanges()
    End Sub
    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack();", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", Aspose.Words.ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Sub LoadPopup(ByVal popupType As Int32)
        Select Case popupType
            Case 2
                ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                phPopup.Controls.Add(ctrlOrgPopup)
        End Select
    End Sub
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