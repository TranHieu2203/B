Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlListParamSystem
    Inherits Common.CommonView

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/List/" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <remarks></remarks>
    Public IDSelect As Integer

    ''' <summary>
    ''' At_ListParamSystem
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property At_ListParamSystem As List(Of AT_LISTPARAM_SYSTEAMDTO)
        Get
            Return ViewState(Me.ID & "_ParamSystem")
        End Get
        Set(ByVal value As List(Of AT_LISTPARAM_SYSTEAMDTO))
            ViewState(Me.ID & "_ParamSystem") = value
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

    ''' <summary>
    ''' ValueStaffRank
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueStaffRank As Decimal
        Get
            Return ViewState(Me.ID & "_ValueStaffRank")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueStaffRank") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>15/08/2017</lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            SetGridFilter(rgDanhMuc)
            rgDanhMuc.AllowCustomPaging = True
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>15/08/2017</lastupdate>
    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarCostCenters

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete, ToolbarItem.Active,
                                       ToolbarItem.Deactive, ToolbarItem.Export)

            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Refresh("")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            rgDanhMuc.ClientSettings.EnablePostBackOnRowClick = True

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>15/08/2017</lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.Rebind()
                        SelectedItemDataGridByKey(rgDanhMuc, IDSelect, , rgDanhMuc.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.CurrentPageIndex = 0
                        rgDanhMuc.MasterTableView.SortExpressions.Clear()
                        rgDanhMuc.Rebind()
                        SelectedItemDataGridByKey(rgDanhMuc, IDSelect, )

                    Case "Cancel"
                        rgDanhMuc.MasterTableView.ClearSelectedItems()
                End Select
            End If

            UpdateControlState()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>15/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_LISTPARAM_SYSTEAMDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgDanhMuc, obj)
            Dim Sorts As String = rgDanhMuc.MasterTableView.SortExpressions.GetSortString()

            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.At_ListParamSystem = rep.GetListParamItime(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows, "CREATED_DATE desc")
                Else
                    Me.At_ListParamSystem = rep.GetListParamItime(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows)
                End If
                rgDanhMuc.VirtualItemCount = MaximumRows
                rgDanhMuc.DataSource = Me.At_ListParamSystem
            Else
                If Sorts IsNot Nothing Then
                    Return rep.GetListParamItime(obj, 0, Integer.MaxValue, 0, Sorts).ToTable
                Else
                    Return rep.GetListParamItime(obj).ToTable
                End If
                'Return rep.GetListParamItime(obj).ToTable
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'Throw ex
        End Try
    End Function

    ''' <lastupdate>15/08/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    ClearControlValue(rdEFFECT_DATE_FROM, rdEffect_date_to_NB, rdTO_LEAVE_YEAR, cboRANK_PAY_OT, nmHOUR_CAL_OT, nmHOUR_MAX_OT, txtNOTE, cbOrg_id, ckNoEffect_date, ckNoEffect_date_NB)
                    EnableControlAll(True, rdEFFECT_DATE_FROM, rdEffect_date_to_NB, rdTO_LEAVE_YEAR, cboRANK_PAY_OT, nmHOUR_CAL_OT, nmHOUR_MAX_OT, txtNOTE, cbOrg_id, ckNoEffect_date, ckNoEffect_date_NB)
                    EnabledGridNotPostback(rgDanhMuc, False)
                Case CommonMessage.STATE_NORMAL
                    ClearControlValue(rdEFFECT_DATE_FROM, rdEffect_date_to_NB, rdTO_LEAVE_YEAR, cboRANK_PAY_OT, nmHOUR_CAL_OT, nmHOUR_MAX_OT, txtNOTE, cbOrg_id, ckNoEffect_date, ckNoEffect_date_NB)
                    EnableControlAll(False, rdEFFECT_DATE_FROM, rdEffect_date_to_NB, rdTO_LEAVE_YEAR, cboRANK_PAY_OT, nmHOUR_CAL_OT, nmHOUR_MAX_OT, txtNOTE, cbOrg_id, ckNoEffect_date, ckNoEffect_date_NB)
                    EnabledGridNotPostback(rgDanhMuc, True)

                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, rdEFFECT_DATE_FROM, rdEffect_date_to_NB, rdTO_LEAVE_YEAR, cboRANK_PAY_OT, nmHOUR_CAL_OT, nmHOUR_MAX_OT, txtNOTE, cbOrg_id, ckNoEffect_date, ckNoEffect_date_NB)
                    EnabledGridNotPostback(rgDanhMuc, False)

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveListParamItime(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgDanhMuc.Rebind()
                        ExcuteScript("Clear", "clRadDatePicker()")
                        ClearControlValue(rdEFFECT_DATE_FROM, rdEffect_date_to_NB, rdTO_LEAVE_YEAR, cboRANK_PAY_OT, nmHOUR_CAL_OT, nmHOUR_MAX_OT, txtNOTE, cbOrg_id, ckNoEffect_date, ckNoEffect_date_NB)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveListParamItime(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgDanhMuc.Rebind()
                        ExcuteScript("Clear", "clRadDatePicker()")
                        ClearControlValue(rdEFFECT_DATE_FROM, rdEffect_date_to_NB, rdTO_LEAVE_YEAR, cboRANK_PAY_OT, nmHOUR_CAL_OT, nmHOUR_MAX_OT, txtNOTE, cbOrg_id, ckNoEffect_date, ckNoEffect_date_NB)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)

                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteListParamItime(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                        ExcuteScript("Clear", "clRadDatePicker()")
                        ClearControlValue(rdEFFECT_DATE_FROM, rdEffect_date_to_NB, rdTO_LEAVE_YEAR, cboRANK_PAY_OT, nmHOUR_CAL_OT, nmHOUR_MAX_OT, txtNOTE)
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select

            UpdateToolbarState()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>15/08/2017</lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            GetDataCombo()
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("EFFECT_DATE_FROM", rdEFFECT_DATE_FROM)
            dic.Add("EFFECT_DATE_TO_NB", rdEffect_date_to_NB)
            'dic.Add("TO_LEAVE_YEAR", rdTO_LEAVE_YEAR)
            'dic.Add("RANK_PAY_OT", cboRANK_PAY_OT)
            'dic.Add("HOUR_CAL_OT", nmHOUR_CAL_OT)
            'dic.Add("HOUR_MAX_OT", nmHOUR_MAX_OT)
            'dic.Add("NOTE", txtNOTE)
            Utilities.OnClientRowSelectedChanged(rgDanhMuc, dic)

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>15/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objData As New AT_LISTPARAM_SYSTEAMDTO
        Dim rep As New AttendanceRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
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

                        If ckNoEffect_date_NB.Checked = False And rdEffect_date_to_NB.SelectedDate Is Nothing Then
                            ShowMessage(Translate("Bạn phải nhập ngày hết hạn nghỉ bù năm trước"), NotifyType.Warning)
                            Exit Sub
                        End If

                        objData.CODE = ""
                        objData.NAME = ""

                        objData.EFFECT_DATE_FROM = rdEFFECT_DATE_FROM.SelectedDate
                        objData.EFFECT_DATE_TO_NB = rdEffect_date_to_NB.SelectedDate
                        objData.TO_LEAVE_YEAR = rdTO_LEAVE_YEAR.SelectedDate
                        If cboRANK_PAY_OT.SelectedValue = "" Then
                            objData.RANK_PAY_OT = Utilities.ObjToInt(ValueStaffRank)
                        Else
                            objData.RANK_PAY_OT = Utilities.ObjToInt(cboRANK_PAY_OT.SelectedValue)
                        End If
                        If IsNumeric(cbOrg_id.SelectedValue) Then
                            objData.ORG_ID = cbOrg_id.SelectedValue
                        End If
                        objData.NO_EFFECT_ENT = CType(ckNoEffect_date.Checked, Boolean)

                        objData.HOUR_CAL_OT = Utilities.ObjToInt(nmHOUR_CAL_OT.Value)
                        objData.HOUR_MAX_OT = Utilities.ObjToInt(nmHOUR_MAX_OT.Value)
                        objData.CREATE_BY_SHOW = Common.Common.GetUsername
                        objData.CREATE_DATE_SHOW = Date.Now
                        objData.NOTE = txtNOTE.Text
                        objData.YEAR_P = rdYear.Value
                        objData.YEAR_TN = rdYearTN.Value
                        objData.DAY_TN = rdDayTN.Value
                        objData.NO_EFFECT_NB = ckNoEffect_date_NB.Checked
                        If IsNumeric(cbOrg_id.SelectedValue) Then
                            objData.ORG_ID = cbOrg_id.SelectedValue
                        End If

                        If IsNumeric(txtAbsentDayLimit.Value) Then
                            objData.ABSENT_DAY_LIMIT = txtAbsentDayLimit.Value
                        Else
                            objData.ABSENT_DAY_LIMIT = 0
                        End If

                        objData.NO_EFFECT_ENT = ckNoEffect_date.Checked
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objData.ACTFLG = "A"
                                If rep.InsertListParamItime(objData, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                    rgDanhMuc.Rebind()
                                    ExcuteScript("Clear", "clRadDatePicker()")
                                    ClearControlValue(rdEFFECT_DATE_FROM, rdEffect_date_to_NB, rdTO_LEAVE_YEAR, cboRANK_PAY_OT, nmHOUR_CAL_OT, nmHOUR_MAX_OT, txtNOTE)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objData.ID = rgDanhMuc.SelectedValue
                                If rep.ModifyListParamItime(objData, rgDanhMuc.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objData.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                    rgDanhMuc.Rebind()
                                    ExcuteScript("Clear", "clRadDatePicker()")
                                    ClearControlValue(rdEFFECT_DATE_FROM, rdEffect_date_to_NB, rdTO_LEAVE_YEAR, cboRANK_PAY_OT, nmHOUR_CAL_OT, nmHOUR_MAX_OT, txtNOTE)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "setDefaultSize()")
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
                            rgDanhMuc.ExportExcel(Server, Response, dtDatas, "DMThamSoHeThong")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
            End Select

            CreateDataFilter()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>15/08/2017</lastupdate>
    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
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

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>15/08/2017</lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDanhMuc.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateDataFilter()
            'đẩy dữ liệu tam.
            'rgDanhMuc.VirtualItemCount = 3
            'rgDanhMuc.DataSource = GetTable()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <lastupdate>15/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly viec check gia tri ID, Code
    ''' Tu dong gen code khi them moi
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New AttendanceRepository
        Dim _validate As New AT_LISTPARAM_SYSTEAMDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgDanhMuc.SelectedValue
                _validate.EFFECT_DATE_FROM = rdEFFECT_DATE_FROM.SelectedDate
                _validate.ORG_ID = cbOrg_id.SelectedValue
                args.IsValid = rep.ValidateListParamItime(_validate)
            Else
                _validate.EFFECT_DATE_FROM = rdEFFECT_DATE_FROM.SelectedDate
                _validate.ORG_ID = cbOrg_id.SelectedValue
                args.IsValid = rep.ValidateListParamItime(_validate)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>15/08/2017</lastupdate>
    ''' <summary>
    ''' Kiem tra gia tri dang đươc chọn cua combox CẤP NHÂN SỰ có tồn tại hoặc bị ngừng ap dụng hay không? 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalRANK_PAY_OT_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalRANK_PAY_OT.ServerValidate
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ValueStaffRank = cboRANK_PAY_OT.SelectedValue
            ListComboData = New ComboBoxDataDTO
            Dim dto As New HU_STAFF_RANKDTO
            Dim list As New List(Of HU_STAFF_RANKDTO)

            dto.ID = Convert.ToDecimal(cboRANK_PAY_OT.SelectedValue)
            list.Add(dto)

            ListComboData.GET_LIST_STAFF_RANK = True
            ListComboData.LIST_LIST_STAFF_RANK = list

            args.IsValid = rep.ValidateCombobox(ListComboData)

            If rep.ValidateCombobox(ListComboData) Then
                args.IsValid = True
            Else
                args.IsValid = False
                cboRANK_PAY_OT.ClearSelection()
                rep.GetComboboxData(ListComboData)
                FillDropDownList(cboRANK_PAY_OT, ListComboData.LIST_LIST_STAFF_RANK, "NAME", "ID", Common.Common.SystemLanguage, True, cboRANK_PAY_OT.SelectedValue)
                cboRANK_PAY_OT.SelectedIndex = 0
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>15/08/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'Throw ex
        End Try
    End Sub

    ''' <lastupdate>15/08/2017</lastupdate>
    ''' <summary>
    ''' Get data và bind vao combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New AttendanceRepository
        Dim dtOrgLevel As DataTable = New DataTable()
        Dim repS As New Profile.ProfileStoreProcedure
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_LIST_STAFF_RANK = True
                rep.GetComboboxData(ListComboData)
            End If
            FillDropDownList(cboRANK_PAY_OT, ListComboData.LIST_LIST_STAFF_RANK, "NAME", "ID", Common.Common.SystemLanguage, True, cboRANK_PAY_OT.SelectedValue)
            dtOrgLevel = repS.GET_ORGID_COMPANY_LEVEL()
            Dim dr As DataRow = dtOrgLevel.NewRow
            dr("ORG_ID") = "-1"
            dr("ORG_NAME_VN") = "Dùng  Chung"
            dtOrgLevel.Rows.Add(dr)
            dtOrgLevel.DefaultView.Sort = "ORG_ID ASC"
            FillRadCombobox(cbOrg_id, dtOrgLevel, "ORG_NAME_VN", "ORG_ID", True)
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'Throw ex
        Finally
            rep.Dispose()
            dtOrgLevel.Dispose()
            startTime = Nothing
        End Try
    End Sub


    Protected Sub rgDanhMuc_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgDanhMuc.SelectedIndexChanged

        Dim item = CType(rgDanhMuc.SelectedItems(0), GridDataItem)
        Dim ID = item.GetDataKeyValue("ID").ToString
        Dim At_ListParamSystem1 = (From p In At_ListParamSystem Where p.ID = Decimal.Parse(ID)).FirstOrDefault

        rdYear.Value = At_ListParamSystem1.YEAR_P
        rdYearTN.Value = At_ListParamSystem1.YEAR_TN
        rdDayTN.Value = At_ListParamSystem1.DAY_TN
        rdTO_LEAVE_YEAR.SelectedDate = At_ListParamSystem1.TO_LEAVE_YEAR
        cboRANK_PAY_OT.SelectedValue = At_ListParamSystem1.RANK_PAY_OT
        nmHOUR_CAL_OT.Value = At_ListParamSystem1.HOUR_CAL_OT
        nmHOUR_MAX_OT.Value = At_ListParamSystem1.HOUR_MAX_OT
        txtNOTE.Text = At_ListParamSystem1.NOTE
        If IsNumeric(At_ListParamSystem1.ORG_ID) Then
            cbOrg_id.SelectedValue = At_ListParamSystem1.ORG_ID
        End If
        ckNoEffect_date.Checked = CBool(At_ListParamSystem1.NO_EFFECT_ENT)
        ckNoEffect_date_NB.Checked = CBool(At_ListParamSystem1.NO_EFFECT_NB)
        txtAbsentDayLimit.Value = At_ListParamSystem1.ABSENT_DAY_LIMIT
    End Sub



#End Region

End Class