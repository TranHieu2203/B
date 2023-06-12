Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlRitual_Hose
    Inherits Common.CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/List/" + Me.GetType().Name.ToString()

#Region "Property"
    Public IDSelect As Integer
    Public Property At_Holiday As List(Of AT_HOLIDAYDTO)
        Get
            Return ViewState(Me.ID & "_Holiday")
        End Get
        Set(ByVal value As List(Of AT_HOLIDAYDTO))
            ViewState(Me.ID & "_Holiday") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SetGridFilter(rgDanhMucHS)
            rgDanhMucHS.AllowCustomPaging = True
            InitControl()
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                GirdConfig(rgDanhMucHS)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
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
                                       ToolbarItem.Active, ToolbarItem.Deactive,
                                       ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Refresh("")
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
                        rgDanhMucHS.Rebind()
                        'SelectedItemDataGridByKey(rgDanhMucHS, IDSelect, , rgDanhMucHS.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMucHS.CurrentPageIndex = 0
                        rgDanhMucHS.MasterTableView.SortExpressions.Clear()
                        rgDanhMucHS.Rebind()
                        'SelectedItemDataGridByKey(rgDanhMucHS, IDSelect, )
                    Case "Cancel"
                        rgDanhMucHS.MasterTableView.ClearSelectedItems()
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
        Dim obj As New AT_HOLIDAYDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgDanhMucHS, obj)
            Dim Sorts As String = rgDanhMucHS.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then

                If Sorts IsNot Nothing Then
                    Me.At_Holiday = rep.GetHoliday_Hose(obj, rgDanhMucHS.CurrentPageIndex, rgDanhMucHS.PageSize, MaximumRows, Sorts)
                Else
                    Me.At_Holiday = rep.GetHoliday_Hose(obj, rgDanhMucHS.CurrentPageIndex, rgDanhMucHS.PageSize, MaximumRows)
                End If
                rgDanhMucHS.VirtualItemCount = MaximumRows
                rgDanhMucHS.DataSource = Me.At_Holiday
            Else
                Return rep.GetHoliday(obj).ToTable
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
            Select Case CurrentState
                Case CommonMessage.STATE_NEW

                    txtCode.Text = rep.AutoGenCode("NNL", "AT_HOLIDAY", "CODE")
                    txtNameVN.Text = ""
                    rdNote.Text = ""
                    rdFromDate.SelectedDate = Nothing
                    rdToDate.SelectedDate = Nothing
                    ckIsSA.Enabled = True
                    ckIsSU.Enabled = True
                    txtCode.Enabled = False
                    txtNameVN.Enabled = True
                    rdFromDate.Enabled = True
                    rdToDate.Enabled = True
                    rdNote.Enabled = True
                    ckIsSA.Checked = True 'Khi thêm mới tự động check
                    ckIsSU.Checked = True 'Khi thêm mới tự động check
                    EnabledGridNotPostback(rgDanhMucHS, False)

                Case CommonMessage.STATE_NORMAL
                    txtCode.Text = ""
                    txtNameVN.Text = ""
                    rdNote.Text = ""
                    rdFromDate.SelectedDate = Nothing
                    rdToDate.SelectedDate = Nothing
                    ckIsSA.Enabled = False
                    ckIsSU.Enabled = False
                    rdFromDate.Enabled = False
                    rdToDate.Enabled = False
                    txtCode.Enabled = False
                    txtNameVN.Enabled = False
                    rdNote.Enabled = False
                    EnabledGridNotPostback(rgDanhMucHS, True)
                Case CommonMessage.STATE_EDIT

                    rdFromDate.Enabled = True
                    rdToDate.Enabled = True
                    txtCode.Enabled = False
                    txtNameVN.Enabled = True
                    rdNote.Enabled = True
                    EnabledGridNotPostback(rgDanhMucHS, False)
                    ckIsSA.Enabled = True
                    ckIsSU.Enabled = True
                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMucHS.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMucHS.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveHoliday_Hose(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgDanhMucHS.Rebind()
                        ClearControlValue(txtCode, rdFromDate, rdToDate, rdNote, txtNameVN)
                        rdFromDate.SelectedDate = Nothing
                        rdToDate.SelectedDate = Nothing
                        rgDanhMucHS.SelectedIndexes.Clear()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMucHS.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMucHS.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveHoliday_Hose(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgDanhMucHS.Rebind()
                        ClearControlValue(txtCode, rdFromDate, rdToDate, rdNote, txtNameVN)
                        rdFromDate.SelectedDate = Nothing
                        rdToDate.SelectedDate = Nothing
                        rgDanhMucHS.SelectedIndexes.Clear()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMucHS.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMucHS.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If Not rep.CheckExistInDatabase(lstDeletes, AttendanceCommonTABLE_NAME.AT_HOLIDAY) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                        Return
                    End If
                    If rep.DeleteHoliday_Hose(lstDeletes) Then
                        Refresh("UpdateView")
                        rdFromDate.Clear()
                        rdToDate.Clear()
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            txtCode.Focus()
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
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", txtCode)
            dic.Add("NAME_VN", txtNameVN)
            dic.Add("FROMDATE", rdFromDate)
            dic.Add("TODATE", rdToDate)
            dic.Add("NOTE", rdNote)
            dic.Add("ID", txtID)
            dic.Add("IS_SA", ckIsSA)
            dic.Add("IS_SUN", ckIsSU)
            Utilities.OnClientRowSelectedChanged(rgDanhMucHS, dic)
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
        Dim objHoliday As New AT_HOLIDAYDTO
        Dim rep As New AttendanceRepository
        Dim psp As New AttendanceStoreProcedure
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                    ClearControlValue(txtNameVN, rdFromDate, rdToDate, rdNote)
                    rdFromDate.Clear()
                    rdToDate.Clear()
                    rdFromDate.DateInput.Clear()
                    rdToDate.DateInput.Clear()
                    rgDanhMucHS.SelectedIndexes.Clear()
                    txtNameVN.Focus()
                Case CommonMessage.TOOLBARITEM_EDIT
                    txtNameVN.Focus()
                    If rgDanhMucHS.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgDanhMucHS.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgDanhMucHS.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgDanhMucHS.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgDanhMucHS.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objHoliday.CODE = txtCode.Text.Trim
                        objHoliday.YEAR = rdFromDate.SelectedDate.Value.Year
                        objHoliday.NAME_VN = txtNameVN.Text.Trim
                        objHoliday.FROMDATE = rdFromDate.SelectedDate
                        objHoliday.TODATE = rdToDate.SelectedDate
                        objHoliday.NOTE = rdNote.Text.Trim
                        objHoliday.IS_SA = ckIsSA.Checked
                        objHoliday.IS_SUN = ckIsSU.Checked
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                'Kiểm tra trùng ngày
                                Dim check = psp.CHECK_VALIDATE(0, objHoliday.CODE, objHoliday.FROMDATE, objHoliday.TODATE)
                                If check <> 0 Then
                                    ShowMessage(Translate("Ngày nghỉ lễ đã tồn tại. Vui lòng chọn ngày khác"), Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                                Dim Validate As New AT_HOLIDAYDTO
                                Validate.CODE = txtCode.Text
                                If Not rep.ValidateHOLIDAY_Hose(Validate) Then
                                    txtCode.Text = rep.AutoGenCode("NNL", "AT_HOLIDAY", "CODE")
                                End If
                                objHoliday.ACTFLG = "A"
                                If rep.InsertHoliday_Hose(objHoliday, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                                End If
                            Case CommonMessage.STATE_EDIT
                                Dim check = psp.CHECK_VALIDATE(1, objHoliday.CODE, objHoliday.FROMDATE, objHoliday.TODATE)
                                If check <> 0 Then
                                    ShowMessage(Translate("Ngày nghỉ lễ đã tồn tại. Vui lòng chọn ngày khác"), Utilities.NotifyType.Error)
                                    Exit Sub
                                End If
                                objHoliday.ID = rgDanhMucHS.SelectedValue
                                If rep.InsertHoliday_Hose(objHoliday, rgDanhMucHS.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objHoliday.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                        rgDanhMucHS.SelectedIndexes.Clear()
                    Else
                        'ExcuteScript("Resize", "ResizeSplitter()")
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
                            Dim data As DataTable
                            data = dtDatas.Clone
                            For Each r As DataColumn In data.Columns
                                If r.ColumnName.ToString.ToUpper = "YEAR" Then
                                    r.DataType = System.Type.GetType("System.Int32")
                                End If
                            Next
                            For Each row As DataRow In dtDatas.Rows
                                data.ImportRow(row)
                            Next
                            rgDanhMucHS.ExportExcel(Server, Response, data, "DMNghiLe")
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
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDanhMucHS.NeedDataSource
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

    ''' <summary>
    ''' Ham xu ly viec check gia tri ID, Code
    ''' Tu dong gen code khi them moi
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New AttendanceRepository
        Dim _validate As New AT_HOLIDAYDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgDanhMucHS.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                ' args.IsValid = rep.ValidateHoliday(_validate)
            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateHoliday(_validate)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Ham xu ly viec check ngay thang
    ''' Tu dong gen code khi them moi
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cvDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvDate.ServerValidate
    '    Dim rep As New AttendanceRepository
    '    Dim _validate As New AT_HOLIDAYDTO
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If CurrentState = CommonMessage.STATE_EDIT Then
    '            _validate.ID = rgDanhMucHS.SelectedValue
    '            _validate.WORKINGDAY = rdDate.SelectedDate
    '            args.IsValid = rep.ValidateHoliday(_validate)
    '        Else
    '            _validate.WORKINGDAY = rdDate.SelectedDate
    '            args.IsValid = rep.ValidateHoliday(_validate)
    '        End If
    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    ''' <summary>
    ''' Xu ly su kien PreRender cho rad grid - click row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
    Protected Sub rgDanhMucHS_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgDanhMucHS.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If rgDanhMucHS.SelectedItems.Count > 0 Then
                Dim slItem As GridDataItem
                slItem = rgDanhMucHS.SelectedItems(0)
                If slItem.GetDataKeyValue("IS_SA").ToString = True Then
                    ckIsSA.Checked = True
                Else
                    ckIsSA.Checked = False
                End If
                If slItem.GetDataKeyValue("IS_SUN").ToString = True Then
                    ckIsSU.Checked = True
                Else
                    ckIsSU.Checked = False
                End If
            End If

        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
End Class