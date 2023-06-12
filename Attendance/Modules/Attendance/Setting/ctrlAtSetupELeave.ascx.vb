Imports System.Globalization
Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlAtSetupELeave
    Inherits Common.CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/List/" + Me.GetType().Name.ToString()
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup

#Region "Property"
    Public IDSelect As Integer
    Public Property At_Holiday As List(Of AT_SETUP_ELEAVEDTO)
        Get
            Return ViewState(Me.ID & "_Holiday")
        End Get
        Set(ByVal value As List(Of AT_SETUP_ELEAVEDTO))
            ViewState(Me.ID & "_Holiday") = value
        End Set
    End Property

    'Kieu man hinh tim kiem
    '0 - normal
    '1 - Employee
    '2 - Sign
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <summary>
    ''' Khởi tạo, load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Refresh("")
            UpdateControlState()
            SetGridFilter(rgDanhMuc)
            rgDanhMuc.AllowCustomPaging = True
            'rgDanhMuc.ClientSettings.EnablePostBackOnRowClick = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarCostCenters
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.CurrentPageIndex = 0
                        rgDanhMuc.MasterTableView.SortExpressions.Clear()
                        rgDanhMuc.Rebind()
                    Case "Cancel"
                        rgDanhMuc.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_SETUP_ELEAVEDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgDanhMuc, obj)
            Dim Sorts As String = rgDanhMuc.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then

                If Sorts IsNot Nothing Then
                    Me.At_Holiday = rep.GetAtSetupELeave(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows, Sorts)
                Else
                    Me.At_Holiday = rep.GetAtSetupELeave(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows)
                End If
                rgDanhMuc.VirtualItemCount = MaximumRows
                rgDanhMuc.DataSource = Me.At_Holiday
            Else
                Return rep.GetAtSetupELeave(obj).ToTable
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Select Case isLoadPopup
                Case 1
                    If Not phFindOrg.Controls.Contains(ctrlFindOrgPopup) Then
                        ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                        phFindOrg.Controls.Add(ctrlFindOrgPopup)
                    End If
            End Select

            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    ClearControlValue(cboYear, chkAnnualYear, chkAnnualMonth, chkOffcialDate, chkStartDate, txtOldAnnual_Time, txtOldAnnual_Renew, ntxtAnnual_transfer, ntxtAnnual_Paid, ntxtStartdate_from1,
                                        ntxtStartdate_to1, ntxtAnnual_Start1, ntxtStartdate_from2, ntxtStartdate_to2, ntxtAnnual_Start2, ntxtLeavedate_from1, ntxtLeavedate_to1, ntxtAnnual_Leave1, ntxtLeavedate_from2,
                                        ntxtLeavedate_to2, ntxtAnnual_Leave2, ntxtWorkdate_from1, ntxtWorkdate_to1, ntxtAnnual_Work1, ntxtWorkdate_from2, ntxtWorkdate_to2, ntxtAnnual_Work2, txtOldAnnual_Time_bu, txtOldAnnual_Renew_bu, ntxtAnnual_transfer_bu)

                    EnableControlAll(False, cboYear, chkAnnualYear, chkAnnualMonth, chkOffcialDate, chkStartDate, txtOldAnnual_Time, txtOldAnnual_Renew, ntxtAnnual_transfer, ntxtAnnual_Paid, ntxtStartdate_from1,
                                        ntxtStartdate_to1, ntxtAnnual_Start1, ntxtStartdate_from2, ntxtStartdate_to2, ntxtAnnual_Start2, ntxtLeavedate_from1, ntxtLeavedate_to1, ntxtAnnual_Leave1, ntxtLeavedate_from2,
                                        ntxtLeavedate_to2, ntxtAnnual_Leave2, ntxtWorkdate_from1, ntxtWorkdate_to1, ntxtAnnual_Work1, ntxtWorkdate_from2, ntxtWorkdate_to2, ntxtAnnual_Work2, txtOldAnnual_Time_bu, txtOldAnnual_Renew_bu, ntxtAnnual_transfer_bu)
                    cboYear.SelectedValue = Date.Now.Year
                    EnabledGridNotPostback(rgDanhMuc, True)
                Case CommonMessage.STATE_EDIT

                    EnableControlAll(True, cboYear, chkAnnualYear, chkAnnualMonth, chkOffcialDate, chkStartDate, txtOldAnnual_Time, txtOldAnnual_Renew, ntxtAnnual_transfer, ntxtAnnual_Paid, ntxtStartdate_from1,
                                        ntxtStartdate_to1, ntxtAnnual_Start1, ntxtStartdate_from2, ntxtStartdate_to2, ntxtAnnual_Start2, ntxtLeavedate_from1, ntxtLeavedate_to1, ntxtAnnual_Leave1, ntxtLeavedate_from2,
                                        ntxtLeavedate_to2, ntxtAnnual_Leave2, ntxtWorkdate_from1, ntxtWorkdate_to1, ntxtAnnual_Work1, ntxtWorkdate_from2, ntxtWorkdate_to2, ntxtAnnual_Work2, txtOldAnnual_Time_bu, txtOldAnnual_Renew_bu, ntxtAnnual_transfer_bu)
                    EnabledGridNotPostback(rgDanhMuc, False)

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteAtSetupELeave(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            UpdateToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Set ngon ngu cho cac lable control va cac column name tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2015 To Date.Now.Year + 1
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ID", hidID)
            dic.Add("EFFECT_YEAR", cboYear)
            dic.Add("ANNUAL_YEAR", chkAnnualYear)
            dic.Add("ANNUAL_MONTH", chkAnnualMonth)
            dic.Add("OFFICIAL_DATE", chkOffcialDate)
            dic.Add("START_DATE", chkStartDate)
            dic.Add("OLDANNUAL_TIME", txtOldAnnual_Time)
            dic.Add("OLDANNUAL_RENEW", txtOldAnnual_Renew)
            dic.Add("ANNUAL_TRANSFER", ntxtAnnual_transfer)
            dic.Add("OLDANNUAL_TIME_BU", txtOldAnnual_Time_bu)
            dic.Add("OLDANNUAL_RENEW_BU", txtOldAnnual_Renew_bu)
            dic.Add("ANNUAL_TRANSFER_BU", ntxtAnnual_transfer_bu)
            dic.Add("ANNUAL_PAID", ntxtAnnual_Paid)
            dic.Add("STARTDATE_FROM1", ntxtStartdate_from1)
            dic.Add("STARTDATE_TO1", ntxtStartdate_to1)
            dic.Add("ANNUAL_START1", ntxtAnnual_Start1)
            dic.Add("STARTDATE_FROM2", ntxtStartdate_from2)
            dic.Add("STARTDATE_TO2", ntxtStartdate_to2)
            dic.Add("LEAVEDATE_FROM1", ntxtLeavedate_from1)
            dic.Add("ANNUAL_START2", ntxtAnnual_Start2)
            dic.Add("LEAVEDATE_TO1", ntxtLeavedate_to1)
            dic.Add("ANNUAL_LEAVE1", ntxtAnnual_Leave1)
            dic.Add("LEAVEDATE_FROM2", ntxtLeavedate_from2)
            dic.Add("LEAVEDATE_TO2", ntxtLeavedate_to2)
            dic.Add("ANNUAL_LEAVE2", ntxtAnnual_Leave2)
            dic.Add("WORKDATE_FROM1", ntxtWorkdate_from1)
            dic.Add("WORKDATE_TO1", ntxtWorkdate_to1)
            dic.Add("ANNUAL_WORK1", ntxtAnnual_Work1)
            dic.Add("WORKDATE_FROM2", ntxtWorkdate_from2)
            dic.Add("WORKDATE_TO2", ntxtWorkdate_to2)
            dic.Add("ANNUAL_WORK2", ntxtAnnual_Work2)
            Utilities.OnClientRowSelectedChanged(rgDanhMuc, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As New AT_SETUP_ELEAVEDTO
        Dim rep As New AttendanceRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    EnableControlAll(True, cboYear, chkAnnualYear, chkAnnualMonth, chkOffcialDate, chkStartDate, txtOldAnnual_Time, txtOldAnnual_Renew, ntxtAnnual_transfer, ntxtAnnual_Paid, ntxtStartdate_from1,
                                        ntxtStartdate_to1, ntxtAnnual_Start1, ntxtStartdate_from2, ntxtStartdate_to2, ntxtAnnual_Start2, ntxtLeavedate_from1, ntxtLeavedate_to1, ntxtAnnual_Leave1, ntxtLeavedate_from2,
                                        ntxtLeavedate_to2, ntxtAnnual_Leave2, ntxtWorkdate_from1, ntxtWorkdate_to1, ntxtAnnual_Work1, ntxtWorkdate_from2, ntxtWorkdate_to2, ntxtAnnual_Work2, txtOldAnnual_Time_bu, txtOldAnnual_Renew_bu, ntxtAnnual_transfer_bu)
                    ClearControlValue(cboYear, chkAnnualYear, chkAnnualMonth, chkOffcialDate, chkStartDate, txtOldAnnual_Time, txtOldAnnual_Renew, ntxtAnnual_transfer, ntxtAnnual_Paid, ntxtStartdate_from1,
                                        ntxtStartdate_to1, ntxtAnnual_Start1, ntxtStartdate_from2, ntxtStartdate_to2, ntxtAnnual_Start2, ntxtLeavedate_from1, ntxtLeavedate_to1, ntxtAnnual_Leave1, ntxtLeavedate_from2,
                                        ntxtLeavedate_to2, ntxtAnnual_Leave2, ntxtWorkdate_from1, ntxtWorkdate_to1, ntxtAnnual_Work1, ntxtWorkdate_from2, ntxtWorkdate_to2, ntxtAnnual_Work2, txtOldAnnual_Time_bu, txtOldAnnual_Renew_bu, ntxtAnnual_transfer_bu)

                    cboYear.SelectedValue = Date.Now.Year
                    chkAnnualYear.Checked = True
                    chkOffcialDate.Checked = True
                    rgDanhMuc.SelectedIndexes.Clear()
                    EnabledGridNotPostback(rgDanhMuc, False)
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgDanhMuc.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        'Thời hạn phép cũ
                        Dim startDate As Date
                        'Gia hạn phép cũ
                        Dim endDate As Date

                        'check date
                        If txtOldAnnual_Renew.Text <> "" AndAlso Not CheckDate(String.Format("{0}/{1}", txtOldAnnual_Renew.Text, Date.Now.Year), endDate) Then
                            ShowMessage(Translate("Gia hạn phép cũ không đúng định dạng, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If txtOldAnnual_Time.Text <> "" AndAlso Not CheckDate(String.Format("{0}/{1}", txtOldAnnual_Time.Text, Date.Now.Year), startDate) Then
                            ShowMessage(Translate("Thời hạn phép cũ không đúng định dạng, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If txtOldAnnual_Renew.Text <> "" AndAlso txtOldAnnual_Time.Text <> "" AndAlso endDate <= startDate Then
                            ShowMessage(Translate("Gia hạn phép cũ phải lớn hơn Thời hạn phép cũ, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        ' check tinh phep cho nhan vien moi vao trong thang
                        If ntxtStartdate_from1.Value IsNot Nothing AndAlso ntxtStartdate_to1.Value AndAlso ntxtStartdate_from1.Value >= ntxtStartdate_to1.Value Then
                            ShowMessage(Translate("Vào làm đến ngày 1 phải lớn hơn Vào làm từ ngày 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If ntxtStartdate_from1.Value IsNot Nothing AndAlso ntxtStartdate_from2.Value AndAlso ntxtStartdate_from1.Value >= ntxtStartdate_from2.Value Then
                            ShowMessage(Translate("Vào làm từ ngày 2 phải lớn hơn Vào làm từ ngày 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If ntxtStartdate_from1.Value IsNot Nothing AndAlso ntxtStartdate_to2.Value AndAlso ntxtStartdate_from1.Value >= ntxtStartdate_to2.Value Then
                            ShowMessage(Translate("Vào làm đến ngày 2 phải lớn hơn Vào làm từ ngày 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If ntxtStartdate_to1.Value IsNot Nothing AndAlso ntxtStartdate_from2.Value AndAlso ntxtStartdate_to1.Value >= ntxtStartdate_from2.Value Then
                            ShowMessage(Translate("Vào làm từ ngày 2 phải lớn hơn Vào làm đến ngày 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If ntxtStartdate_to1.Value IsNot Nothing AndAlso ntxtStartdate_to2.Value AndAlso ntxtStartdate_to1.Value >= ntxtStartdate_to2.Value Then
                            ShowMessage(Translate("Vào làm đến ngày 2 phải lớn hơn Vào làm đến ngày 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If ntxtStartdate_from2.Value IsNot Nothing AndAlso ntxtStartdate_to2.Value AndAlso ntxtStartdate_from2.Value >= ntxtStartdate_to2.Value Then
                            ShowMessage(Translate("Vào làm đến ngày 2 phải lớn hơn Vào làm từ ngày 2, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        ' check tinh phep cho nhan vien nghi việc trong thang
                        If ntxtLeavedate_from1.Value IsNot Nothing AndAlso ntxtLeavedate_to1.Value AndAlso ntxtLeavedate_from1.Value >= ntxtLeavedate_to1.Value Then
                            ShowMessage(Translate("Nghỉ việc đến ngày 1 phải lớn hơn Nghỉ việc từ ngày 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If ntxtLeavedate_from1.Value IsNot Nothing AndAlso ntxtLeavedate_from2.Value AndAlso ntxtLeavedate_from1.Value >= ntxtLeavedate_from2.Value Then
                            ShowMessage(Translate("Nghỉ việc từ ngày 2 phải lớn hơn Nghỉ việc từ ngày 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If ntxtLeavedate_from1.Value IsNot Nothing AndAlso ntxtLeavedate_to2.Value AndAlso ntxtLeavedate_from1.Value >= ntxtLeavedate_to2.Value Then
                            ShowMessage(Translate("Nghỉ việc đến ngày 2 phải lớn hơn Nghỉ việc từ ngày 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If ntxtLeavedate_to1.Value IsNot Nothing AndAlso ntxtLeavedate_from2.Value AndAlso ntxtLeavedate_to1.Value >= ntxtLeavedate_from2.Value Then
                            ShowMessage(Translate("Nghỉ việc từ ngày 2 phải lớn hơn Nghỉ việc đến ngày 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If ntxtLeavedate_to1.Value IsNot Nothing AndAlso ntxtLeavedate_to2.Value AndAlso ntxtLeavedate_to1.Value >= ntxtLeavedate_to2.Value Then
                            ShowMessage(Translate("Nghỉ việc đến ngày 2 phải lớn hơn Nghỉ việc đến ngày 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If ntxtLeavedate_from2.Value IsNot Nothing AndAlso ntxtLeavedate_to2.Value AndAlso ntxtLeavedate_from2.Value >= ntxtLeavedate_to2.Value Then
                            ShowMessage(Translate("Nghỉ việc đến ngày 2 phải lớn hơn Nghỉ việc từ ngày 2, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If


                        ' check tinh phep cho nhan vien đang làm việc trong thang
                        If ntxtWorkdate_from1.Value IsNot Nothing AndAlso ntxtWorkdate_to1.Value AndAlso ntxtWorkdate_from1.Value >= ntxtWorkdate_to1.Value Then
                            ShowMessage(Translate("Đang làm việc đến ngày 1 phải lớn hơn Đang làm việc từ ngày 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If ntxtWorkdate_from1.Value IsNot Nothing AndAlso ntxtWorkdate_from2.Value AndAlso ntxtWorkdate_from1.Value >= ntxtWorkdate_from2.Value Then
                            ShowMessage(Translate("Đang làm việc từ ngày 2 phải lớn hơn Đang làm việc từ ngày 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If ntxtWorkdate_from1.Value IsNot Nothing AndAlso ntxtWorkdate_to2.Value AndAlso ntxtWorkdate_from1.Value >= ntxtWorkdate_to2.Value Then
                            ShowMessage(Translate("Đang làm việc đến ngày 2 phải lớn hơn Đang làm việc từ ngày 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If ntxtWorkdate_to1.Value IsNot Nothing AndAlso ntxtWorkdate_from2.Value AndAlso ntxtWorkdate_to1.Value >= ntxtWorkdate_from2.Value Then
                            ShowMessage(Translate("Đang làm việc từ ngày 2 phải lớn hơn Đang làm việc đến ngày 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If ntxtWorkdate_to1.Value IsNot Nothing AndAlso ntxtWorkdate_to2.Value AndAlso ntxtWorkdate_to1.Value >= ntxtWorkdate_to2.Value Then
                            ShowMessage(Translate("Đang làm việc đến ngày 2 phải lớn hơn Đang làm việc đến ngày 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If ntxtWorkdate_from2.Value IsNot Nothing AndAlso ntxtWorkdate_to2.Value AndAlso ntxtWorkdate_from2.Value >= ntxtWorkdate_to2.Value Then
                            ShowMessage(Translate("Đang làm việc đến ngày 2 phải lớn hơn Đang làm việc từ ngày 2, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If


                        obj.EFFECT_YEAR = cboYear.SelectedValue

                        If chkAnnualYear.Checked Then
                            obj.ANNUAL_YEAR = -1
                        Else
                            obj.ANNUAL_YEAR = 0
                        End If

                        If chkAnnualMonth.Checked Then
                            obj.ANNUAL_MONTH = -1
                        Else
                            obj.ANNUAL_MONTH = 0
                        End If

                        If chkOffcialDate.Checked Then
                            obj.OFFICIAL_DATE = -1
                        Else
                            obj.OFFICIAL_DATE = 0
                        End If

                        If chkStartDate.Checked Then
                            obj.START_DATE = -1
                        Else
                            obj.START_DATE = 0
                        End If

                        obj.OLDANNUAL_TIME = txtOldAnnual_Time.Text
                        obj.OLDANNUAL_RENEW = txtOldAnnual_Renew.Text
                        obj.ANNUAL_TRANSFER = If(ntxtAnnual_transfer.Value Is Nothing, 0, ntxtAnnual_transfer.Value)
                        obj.OLDANNUAL_TIME_BU = txtOldAnnual_Time_bu.Text
                        obj.OLDANNUAL_RENEW_BU = txtOldAnnual_Renew_bu.Text
                        obj.ANNUAL_TRANSFER_BU = If(ntxtAnnual_transfer_bu.Value Is Nothing, 0, ntxtAnnual_transfer_bu.Value)

                        obj.ANNUAL_PAID = If(ntxtAnnual_Paid.Value Is Nothing, 0, ntxtAnnual_Paid.Value)
                        obj.STARTDATE_FROM1 = If(ntxtStartdate_from1.Value Is Nothing, 0, ntxtStartdate_from1.Value)
                        obj.STARTDATE_TO1 = If(ntxtStartdate_to1.Value Is Nothing, 0, ntxtStartdate_to1.Value)
                        obj.ANNUAL_START1 = If(ntxtAnnual_Start1.Value Is Nothing, 0, ntxtAnnual_Start1.Value)
                        obj.STARTDATE_FROM2 = If(ntxtStartdate_from2.Value Is Nothing, 0, ntxtStartdate_from2.Value)
                        obj.STARTDATE_TO2 = If(ntxtStartdate_to2.Value Is Nothing, 0, ntxtStartdate_to2.Value)
                        obj.ANNUAL_START2 = If(ntxtAnnual_Start2.Value Is Nothing, 0, ntxtAnnual_Start2.Value)
                        obj.LEAVEDATE_FROM1 = If(ntxtLeavedate_from1.Value Is Nothing, 0, ntxtLeavedate_from1.Value)
                        obj.LEAVEDATE_TO1 = If(ntxtLeavedate_to1.Value Is Nothing, 0, ntxtLeavedate_to1.Value)
                        obj.ANNUAL_LEAVE1 = If(ntxtAnnual_Leave1.Value Is Nothing, 0, ntxtAnnual_Leave1.Value)
                        obj.LEAVEDATE_FROM2 = If(ntxtLeavedate_from2.Value Is Nothing, 0, ntxtLeavedate_from2.Value)
                        obj.LEAVEDATE_TO2 = If(ntxtLeavedate_to2.Value Is Nothing, 0, ntxtLeavedate_to2.Value)
                        obj.ANNUAL_LEAVE2 = If(ntxtAnnual_Leave2.Value Is Nothing, 0, ntxtAnnual_Leave2.Value)
                        obj.WORKDATE_FROM1 = If(ntxtWorkdate_from1.Value Is Nothing, 0, ntxtWorkdate_from1.Value)
                        obj.WORKDATE_TO1 = If(ntxtWorkdate_to1.Value Is Nothing, 0, ntxtWorkdate_to1.Value)
                        obj.ANNUAL_WORK1 = If(ntxtAnnual_Work1.Value Is Nothing, 0, ntxtAnnual_Work1.Value)
                        obj.WORKDATE_FROM2 = If(ntxtWorkdate_from2.Value Is Nothing, 0, ntxtWorkdate_from2.Value)
                        obj.WORKDATE_TO2 = If(ntxtWorkdate_to2.Value Is Nothing, 0, ntxtWorkdate_to2.Value)
                        obj.ANNUAL_WORK2 = If(ntxtAnnual_Work2.Value Is Nothing, 0, ntxtAnnual_Work2.Value)

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertAtSetupELeave(obj, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                obj.ID = rgDanhMuc.SelectedValue
                                If rep.ModifyAtSetupELeave(obj, rgDanhMuc.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = obj.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                        rgDanhMuc.SelectedIndexes.Clear()
                    Else
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgDanhMuc.ExportExcel(Server, Response, dtDatas, "Thiết lập phép chuẩn theo cơ cấu")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using
            End Select
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Function CheckDate(ByVal value As String, ByRef result As Date) As Boolean
        Dim dateCheck As Boolean
        If value = "" Or value = "&nbsp;" Then
            value = ""
            Return True
        End If

        Try
            dateCheck = DateTime.TryParseExact(value, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result)
            Return dateCheck
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub rgDanhMuc_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgDanhMuc.SelectedIndexChanged
        Dim dtdata As DataTable = Nothing
        Dim item = CType(rgDanhMuc.SelectedItems(0), GridDataItem)
        Dim ID = item.GetDataKeyValue("ID").ToString
        Dim At_ListParamSystem1 = (From p In At_Holiday Where p.ID = Decimal.Parse(ID)).SingleOrDefault
    End Sub

    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDanhMuc.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <summary>
    ''' Update trang thai menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
#End Region

End Class