Imports System.IO
Imports Aspose.Cells
Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Profile
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlDMCaLamViec
    Inherits Common.CommonView

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _shift_Day_ID As Decimal = 0
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Dim _classPath As String = "Attendance/Module/Attendance/List/" + Me.GetType().Name.ToString()
#Region "Property"

    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <remarks></remarks>
    Public IDSelect As Integer

    ''' <summary>
    ''' AT_SHIFT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AT_SHIFT As List(Of AT_SHIFTDTO)
        Get
            Return ViewState(Me.ID & "_AT_SHIFT")
        End Get
        Set(ByVal value As List(Of AT_SHIFTDTO))
            ViewState(Me.ID & "_AT_SHIFT") = value
        End Set
    End Property
    Dim lst As New List(Of CommonBusiness.UserOrgAccessDTO)
    Public Property LIST_ORG As List(Of CommonBusiness.UserOrgAccessDTO)
        Get
            Return ViewState(Me.ID & "_LIST_ORG")
        End Get
        Set(ByVal value As List(Of CommonBusiness.UserOrgAccessDTO))
            ViewState(Me.ID & "_LIST_ORG") = value
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
    ''' ValueMaCong
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueMaCong As Decimal
        Get
            Return ViewState(Me.ID & "_ValueMaCong")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueMaCong") = value
        End Set
    End Property

    ''' <summary>
    ''' ValueCaThu7
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueCaThu7 As Decimal
        Get
            Return ViewState(Me.ID & "_ValueCaThu7")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueCaThu7") = value
        End Set
    End Property

    ''' <summary>
    ''' ValueKieuCongCN
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueKieuCongCN As Decimal
        Get
            Return ViewState(Me.ID & "_ValueKieuCongCN")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueKieuCongCN") = value
        End Set
    End Property

    Property isLoadPopup As Decimal
        Get
            Return PageViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    Property Enabled As Boolean
        Get
            Return PageViewState(Me.ID & "_Enabled")
        End Get
        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_Enabled") = value
        End Set
    End Property

    Public Property List_Oganization_ID As List(Of Decimal)
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_List_Oganization_ID") = value
        End Set
        Get
            Return PageViewState(Me.ID & "_List_Oganization_ID")
        End Get
    End Property
    Property ID_Select As Decimal
        Get
            Return ViewState(Me.ID & "_ID_Select")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ID_Select") = value
        End Set
    End Property
    Private Property dtLogs As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLogs")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtLogs") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView, la ham load du lieu, control states cua usercontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
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
            If Not IsPostBack Then
                GirdConfig(rgDanhMuc)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
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

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                        ToolbarItem.Delete,
                                       ToolbarItem.Active, ToolbarItem.Deactive,
                                        ToolbarItem.Export)
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
                                                                    ToolbarIcons.Export,
                                                                    ToolbarAuthorize.Export,
                                                                    Translate("Xuất file mẫu")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            Refresh("")

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
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
                If Not LogHelper.CurrentUser.USERNAME.ToUpper.Equals("ADMIN") Then
                    lbIsLeave.Visible = False
                    chkSaturdayShift.Visible = False
                    lbIS_REG_SHIFT.Visible = False
                    chkSundayShift.Visible = False
                    lbIsCalHoliday.Visible = False
                    chkHolydayShift.Visible = False
                    lbMorningShift.Visible = False
                    chkMorningShift.Visible = False
                    lbAfternoonShift.Visible = False
                    chkAfternoonShift.Visible = False
                    lbMiddleShift.Visible = False
                    chkMiddleShift.Visible = False
                    lbBrokenShift.Visible = False
                    chkBrokenShift.Visible = False
                    lbTimeShift.Visible = False
                    chkTimeShift.Visible = False
                    lbSupportCHShift.Visible = False
                    chkSupportCHShift.Visible = False
                End If
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.Rebind()
                        'SelectedItemDataGridByKey(rgDanhMuc, IDSelect, , rgDanhMuc.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.CurrentPageIndex = 0
                        rgDanhMuc.MasterTableView.SortExpressions.Clear()
                        rgDanhMuc.Rebind()
                        'SelectedItemDataGridByKey(rgDanhMuc, IDSelect, )

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

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_SHIFTDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgDanhMuc, obj)
            Dim Sorts As String = rgDanhMuc.MasterTableView.SortExpressions.GetSortString()
            'hoaivv add 2 rc
            Dim user = LogHelper.CurrentUser
            obj.SHIFT_DAY = user.ID
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.AT_SHIFT = rep.GetAT_SHIFT(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows, "CREATED_DATE desc")
                Else
                    Me.AT_SHIFT = rep.GetAT_SHIFT(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows)
                End If
                rgDanhMuc.VirtualItemCount = MaximumRows
                rgDanhMuc.DataSource = Me.AT_SHIFT
            Else
                If Sorts IsNot Nothing Then
                    Return rep.GetAT_SHIFT(obj, 0, Integer.MaxValue, 0, Sorts).ToTable
                Else
                    Return rep.GetAT_SHIFT(obj).ToTable
                End If
                'Return rep.GetAT_SHIFT(obj).ToTable
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If ctrlFindOrgPopup IsNot Nothing AndAlso phPopupOrg.Controls.Contains(ctrlFindOrgPopup) Then
                phPopupOrg.Controls.Remove(ctrlFindOrgPopup)
            End If
            'Select Case isLoadPopup
            '    Case 1
            If isLoadPopup = 1 Then
                ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                ctrlFindOrgPopup.ShowCheckBoxes = TreeNodeTypes.All
                ctrlFindOrgPopup.CheckChildNodes = True
                If IsNumeric(ID_Select) AndAlso ID_Select > 0 Then
                    List_Oganization_ID = rep.GetAT_SHIFT_ORG_ACCESS_By_ID(ID_Select)
                    ctrlFindOrgPopup.Bind_CheckedValueKeys = List_Oganization_ID
                Else
                    ctrlFindOrgPopup.Bind_CheckedValueKeys = New List(Of Decimal)
                End If
                ctrlFindOrgPopup.Enabled = Enabled
                phPopupOrg.Controls.Add(ctrlFindOrgPopup)
                'End Select
            Else
                Select Case CurrentState
                    Case CommonMessage.STATE_NEW
                        txtCode.Text = ""
                        txtNameVN.Text = ""
                        txtNote.Text = ""
                        cboCongTy.SelectedIndex = Nothing
                        cboNgayCongCa.SelectedIndex = 2

                        rdHours_Start.SelectedDate = Nothing
                        rdHours_Stop.SelectedDate = Nothing
                        rdEND_MID_HOURS.SelectedDate = Nothing
                        rdSTART_MID_HOURS.SelectedDate = Nothing
                        rdHOURS_STAR_CHECKIN.SelectedDate = Nothing
                        rdHOURS_STAR_CHECKOUT.SelectedDate = Nothing
                        chkIS_MID_END.Checked = Nothing
                        chkIS_HOURS_STOP.Checked = Nothing
                        chkIS_HOURS_CHECKOUT.Checked = Nothing
                        ClearControlValue(rdLateHour, rdEarlyHour, chkIsTomorrow, rnShiftHour, rnOTHourMax, rnOTHourMin, chkSaturdayShift, chkSundayShift,
                                          chkHolydayShift, chkMorningShift, chkAfternoonShift, chkMiddleShift, chkBrokenShift, chkTimeShift, chkSupportCHShift)
                        EnableControlAll(True, rdLateHour, rdEarlyHour, chkIsTomorrow, rnShiftHour, rnOTHourMax, rnOTHourMin, chkSaturdayShift, chkSundayShift, btnFindOrg,
                                          chkHolydayShift, chkMorningShift, chkAfternoonShift, chkMiddleShift, chkBrokenShift, chkTimeShift, chkSupportCHShift)
                        txtCode.Enabled = True
                        txtNameVN.Enabled = True
                        cboNgayCongCa.Enabled = True
                        cboCongTy.Enabled = True
                        rdHours_Start.Enabled = True
                        rdHours_Stop.Enabled = True
                        rdEND_MID_HOURS.Enabled = True
                        rdSTART_MID_HOURS.Enabled = True
                        rdHOURS_STAR_CHECKIN.Enabled = True
                        rdHOURS_STAR_CHECKOUT.Enabled = True
                        chkIS_MID_END.Enabled = True
                        chkIS_HOURS_STOP.Enabled = True
                        chkIS_HOURS_CHECKOUT.Enabled = True
                        txtNote.Enabled = True
                        rgDanhMuc.Rebind()
                        EnabledGridNotPostback(rgDanhMuc, False)
                    Case CommonMessage.STATE_NORMAL
                        txtNameVN.Text = ""
                        cboCongTy.SelectedIndex = Nothing
                        cboNgayCongCa.SelectedIndex = 2
                        rdHours_Start.SelectedDate = Nothing
                        rdHours_Stop.SelectedDate = Nothing
                        rdEND_MID_HOURS.SelectedDate = Nothing
                        rdSTART_MID_HOURS.SelectedDate = Nothing
                        rdHOURS_STAR_CHECKIN.SelectedDate = Nothing
                        rdHOURS_STAR_CHECKOUT.SelectedDate = Nothing
                        chkIS_MID_END.Checked = Nothing
                        chkIS_HOURS_STOP.Checked = Nothing
                        chkIS_HOURS_CHECKOUT.Checked = Nothing
                        txtNote.Text = ""
                        txtCode.Text = ""
                        ClearControlValue(rdLateHour, rdEarlyHour, chkIsTomorrow, rnShiftHour, rnOTHourMax, rnOTHourMin, chkSaturdayShift, chkSundayShift,
                                          chkHolydayShift, chkMorningShift, chkAfternoonShift, chkMiddleShift, chkBrokenShift, chkTimeShift, chkSupportCHShift)
                        EnableControlAll(False, rdLateHour, rdEarlyHour, chkIsTomorrow, rnShiftHour, rnOTHourMax, rnOTHourMin, chkSaturdayShift, chkSundayShift, btnFindOrg,
                                          chkHolydayShift, chkMorningShift, chkAfternoonShift, chkMiddleShift, chkBrokenShift, chkTimeShift, chkSupportCHShift)
                        txtCode.Enabled = False
                        txtNameVN.Enabled = False
                        cboCongTy.Enabled = False
                        cboNgayCongCa.Enabled = False
                        rdHours_Start.Enabled = False
                        rdHours_Stop.Enabled = False
                        rdEND_MID_HOURS.Enabled = False
                        rdSTART_MID_HOURS.Enabled = False
                        rdHOURS_STAR_CHECKIN.Enabled = False
                        rdHOURS_STAR_CHECKOUT.Enabled = False
                        chkIS_MID_END.Enabled = False
                        chkIS_HOURS_STOP.Enabled = False
                        chkIS_HOURS_CHECKOUT.Enabled = False
                        txtNote.Enabled = False

                        EnabledGridNotPostback(rgDanhMuc, True)
                    Case CommonMessage.STATE_EDIT
                        txtCode.Enabled = True
                        txtNameVN.Enabled = True
                        EnableControlAll(True, rdLateHour, rdEarlyHour, chkIsTomorrow, rnShiftHour, rnOTHourMax, rnOTHourMin, chkSaturdayShift, chkSundayShift, btnFindOrg,
                                          chkHolydayShift, chkMorningShift, chkAfternoonShift, chkMiddleShift, chkBrokenShift, chkTimeShift, chkSupportCHShift)
                        cboCongTy.Enabled = True
                        cboNgayCongCa.Enabled = True
                        rdHours_Start.Enabled = True
                        rdHours_Stop.Enabled = True
                        rdEND_MID_HOURS.Enabled = True
                        rdSTART_MID_HOURS.Enabled = True
                        rdHOURS_STAR_CHECKIN.Enabled = True
                        rdHOURS_STAR_CHECKOUT.Enabled = True
                        chkIS_MID_END.Enabled = True
                        chkIS_HOURS_STOP.Enabled = True
                        chkIS_HOURS_CHECKOUT.Enabled = True
                        txtNote.Enabled = True
                        'rntxtMinHours.Enabled = True
                        EnabledGridNotPostback(rgDanhMuc, False)
                    Case CommonMessage.STATE_DEACTIVE
                        Dim lstDeletes As New List(Of Decimal)
                        For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                            Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                            lstDeletes.Add(item.GetDataKeyValue("ID"))
                        Next
                        If rep.ActiveAT_SHIFT(lstDeletes, "I") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgDanhMuc.Rebind()
                            ClearControlValue(txtCode, txtNameVN, rdHours_Start, rdHours_Stop, txtNote, cboCongTy, cboNgayCongCa, rdEND_MID_HOURS, rdSTART_MID_HOURS, rdHOURS_STAR_CHECKIN, rdHOURS_STAR_CHECKOUT, chkIS_MID_END, chkIS_HOURS_STOP, chkIS_HOURS_CHECKOUT)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If

                    Case CommonMessage.STATE_ACTIVE
                        Dim lstDeletes As New List(Of Decimal)
                        For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                            Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                            lstDeletes.Add(item.GetDataKeyValue("ID"))
                        Next
                        If rep.ActiveAT_SHIFT(lstDeletes, "A") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgDanhMuc.Rebind()
                            ClearControlValue(txtCode, txtNameVN, rdHours_Start, rdHours_Stop, txtNote, cboCongTy, cboNgayCongCa, rdEND_MID_HOURS, rdSTART_MID_HOURS, rdHOURS_STAR_CHECKIN, rdHOURS_STAR_CHECKOUT, chkIS_MID_END, chkIS_HOURS_STOP, chkIS_HOURS_CHECKOUT)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If

                    Case CommonMessage.STATE_DELETE
                        Dim lstDeletes As New List(Of Decimal)
                        For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                            Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                            lstDeletes.Add(item.GetDataKeyValue("ID"))
                        Next
                        If rep.DeleteAT_SHIFT(lstDeletes) Then
                            Refresh("UpdateView")
                            UpdateControlState()

                        Else
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            UpdateControlState()
                        End If
                End Select
                UpdateToolbarState()
            End If



            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
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
            dic.Add("CODE", txtCode)
            dic.Add("NAME_VN", txtNameVN)

            dic.Add("LATE_HOUR", rdLateHour)
            dic.Add("EARLY_HOUR", rdEarlyHour)
            dic.Add("IS_TOMORROW", chkIsTomorrow)
            dic.Add("SHIFT_HOUR", rnShiftHour)
            dic.Add("HOURS_START", rdHours_Start)
            dic.Add("HOURS_STOP", rdHours_Stop)

            dic.Add("OT_HOUR_MIN", rnOTHourMin)
            dic.Add("OT_HOUR_MAX", rnOTHourMax)
            dic.Add("IS_HOLYDAY_SHIFT", chkHolydayShift)
            dic.Add("IS_MORNING_SHIFT", chkMorningShift)
            dic.Add("IS_AFTERNOON_SHIFT", chkAfternoonShift)
            dic.Add("IS_MIDDLE_SHIFT", chkMiddleShift)
            dic.Add("IS_BROKEN_SHIFT", chkBrokenShift)
            dic.Add("IS_TIME_SHIFT", chkTimeShift)
            dic.Add("IS_CH_SUPPORT_SHIFT", chkSupportCHShift)
            dic.Add("IS_SATURDAY_SHIFT", chkSaturdayShift)
            dic.Add("IS_SUNDAY_SHIFT", chkSundayShift)

            dic.Add("NOTE", txtNote)
            dic.Add("ORG_ID", hidOrgID)
            dic.Add("ORG_NAME", txtOrgName2)
            dic.Add("SHIFT_DAY", cboNgayCongCa)
            dic.Add("IS_HOURS_STOP", chkIS_HOURS_STOP)
            dic.Add("IS_HOURS_CHECKOUT", chkIS_HOURS_CHECKOUT)
            dic.Add("IS_MID_END", chkIS_MID_END)

            dic.Add("START_MID_HOURS", rdSTART_MID_HOURS)
            dic.Add("END_MID_HOURS", rdEND_MID_HOURS)
            dic.Add("HOURS_STAR_CHECKIN", rdHOURS_STAR_CHECKIN)
            dic.Add("HOURS_STAR_CHECKOUT", rdHOURS_STAR_CHECKOUT)

            Utilities.OnClientRowSelectedChanged(rgDanhMuc, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Set ngon ngu cho cac lable control va cac column name tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New AttendanceRepository
        Dim repS As New ProfileStoreProcedure
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO

                ListComboData.GET_LIST_SHIFT_SUNDAY = True
                rep.GetComboboxData(ListComboData)
            End If

            'hoaivv
            Dim dtOrgLevel As DataTable
            'Dim US As New USERDTO
            Dim user_id As Decimal
            Dim user = LogHelper.CurrentUser
            user_id = user.ID
            dtOrgLevel = repS.GET_ORGID_COMPANY_LEVEL_USER_ID(user_id)
            Dim dr As DataRow = dtOrgLevel.NewRow
            dr("ORG_ID") = "-1"
            dr("ORG_NAME_VN") = "Dùng  Chung"
            dtOrgLevel.Rows.Add(dr)

            dtOrgLevel.DefaultView.Sort = "ORG_ID ASC"
            FillRadCombobox(cboCongTy, dtOrgLevel, "ORG_NAME_VN", "ORG_ID", True)

            Dim table As New DataTable

            ' Create four typed columns in the DataTable.
            table.Columns.Add("ID", GetType(Double))
            table.Columns.Add("NAME", GetType(String))


            ' Add five rows with those columns filled in the DataTable.
            table.Rows.Add(0, "0")
            table.Rows.Add(0.5, "0.5")
            table.Rows.Add(1, "1")
            table.Rows.Add(1.5, "1.5")
            FillRadCombobox(cboNgayCongCa, table, "NAME", "ID")
            'end

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Set lai ngon ngu cho cac lable control va cac column name tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReloadCombobox()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ListComboData = New ComboBoxDataDTO

            ListComboData.GET_LIST_SHIFT_SUNDAY = True
            rep.GetComboboxData(ListComboData)

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objShift As New AT_SHIFTDTO
        Dim rep As New AttendanceRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ID_Select = Nothing
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
                    'IDSelect = Nothing
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

                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstID.Add(Decimal.Parse(item("ID").Text))
                    Next

                    If Not rep.CheckExistInDatabase(lstID, AttendanceCommonTABLE_NAME.AT_SHIFT) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                        Return
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case "EXPORT_TEMP"
                    Dim store As New AttendanceStoreProcedure
                    Dim dsData As DataSet = store.Get_CALAMVIEC_IMPORT()
                    ExportTemplate("Attendance\Import\Template_ImportShift_Org.xls",
                                              dsData, Nothing,
                                              "Template_ImportShift_Org" & Format(Date.Now, "yyyyMMdd"))

                Case "IMPORT_TEMP"
                    ctrlUpload.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then

                        objShift.CODE = txtCode.Text.Trim
                        objShift.NAME_VN = txtNameVN.Text.Trim

                        objShift.HOURS_START = rdHours_Start.SelectedDate
                        objShift.HOURS_STOP = rdHours_Stop.SelectedDate
                        objShift.START_MID_HOURS = rdSTART_MID_HOURS.SelectedDate
                        objShift.END_MID_HOURS = rdEND_MID_HOURS.SelectedDate

                        objShift.HOURS_STAR_CHECKIN = rdHOURS_STAR_CHECKIN.SelectedDate
                        objShift.HOURS_STAR_CHECKOUT = rdHOURS_STAR_CHECKOUT.SelectedDate
                        objShift.IS_HOURS_STOP = chkIS_HOURS_STOP.Checked
                        objShift.IS_MID_END = chkIS_MID_END.Checked
                        objShift.IS_HOURS_CHECKOUT = chkIS_HOURS_CHECKOUT.Checked
                        objShift.NOTE = txtNote.Text.Trim
                        'hoaivv
                        'objShift.ORG_ID = cboCongTy.SelectedValue
                        'If hidOrgID.Value <> "" Then
                        '    objShift.ORG_ID = Decimal.Parse(hidOrgID.Value)
                        'End If
                        If LIST_ORG IsNot Nothing Then
                            If LIST_ORG.Count > 0 Then
                                objShift.LIST_ORG = LIST_ORG
                            Else
                                objShift.LIST_ORG = New List(Of CommonBusiness.UserOrgAccessDTO)
                            End If
                        Else
                            objShift.LIST_ORG = New List(Of CommonBusiness.UserOrgAccessDTO)
                        End If

                        If IsDate(rdLateHour.SelectedDate) Then
                            objShift.LATE_HOUR = rdLateHour.SelectedDate
                        End If
                        If IsDate(rdEarlyHour.SelectedDate) Then
                            objShift.EARLY_HOUR = rdEarlyHour.SelectedDate
                        End If
                        objShift.IS_TOMORROW = chkIsTomorrow.Checked
                        objShift.SHIFT_HOUR = Convert.ToDecimal(rnShiftHour.Value)
                        If cboNgayCongCa.SelectedValue <> "" Then
                            objShift.SHIFT_DAY = cboNgayCongCa.SelectedValue
                        End If
                        objShift.OT_HOUR_MIN = Convert.ToDecimal(rnOTHourMin.Value)
                        objShift.OT_HOUR_MAX = Convert.ToDecimal(rnOTHourMax.Value)
                        objShift.IS_HOLYDAY_SHIFT = chkHolydayShift.Checked
                        objShift.IS_MORNING_SHIFT = chkMorningShift.Checked
                        objShift.IS_AFTERNOON_SHIFT = chkAfternoonShift.Checked
                        objShift.IS_MIDDLE_SHIFT = chkMiddleShift.Checked
                        objShift.IS_BROKEN_SHIFT = chkBrokenShift.Checked
                        objShift.IS_TIME_SHIFT = chkTimeShift.Checked
                        objShift.IS_CH_SUPPORT_SHIFT = chkSupportCHShift.Checked
                        objShift.IS_SATURDAY_SHIFT = chkSaturdayShift.Checked
                        objShift.IS_SUNDAY_SHIFT = chkSundayShift.Checked

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objShift.ACTFLG = "A"
                                If rep.InsertAT_SHIFT(objShift, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    CreateDataFilter()
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                    'ReloadCombobox()
                                    ClearControlValue(txtCode, txtNameVN, rdHours_Start, rdHours_Stop, txtNote)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)

                                End If

                            Case CommonMessage.STATE_EDIT

                                Dim cmRep As New CommonRepository
                                Dim lstID As New List(Of Decimal)

                                lstID.Add(Convert.ToDecimal(rgDanhMuc.SelectedValue))

                                If cmRep.CheckExistIDTable(lstID, "AT_SHIFT", "ID") Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Refresh("Cancel")
                                    UpdateControlState()
                                    Exit Sub
                                End If

                                objShift.ID = rgDanhMuc.SelectedValue

                                For Each re As AT_SHIFTDTO In Me.AT_SHIFT
                                    If re.ID = objShift.ID Then
                                        objShift.CREATED_DATE = re.CREATED_DATE
                                        Exit For
                                    End If
                                Next

                                If rep.ModifyAT_SHIFT(objShift, rgDanhMuc.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    CreateDataFilter()
                                    IDSelect = objShift.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                    ClearControlValue(txtCode, txtNameVN, rdHours_Start, rdHours_Stop, txtNote)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Script", "setDefaultSize()")
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
                            rgDanhMuc.ExportExcel(Server, Response, dtDatas, "CaLamViec")
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

    ''' <lastupdate>14/08/2017</lastupdate>
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

    ''' <lastupdate>14/08/2017</lastupdate>
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
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly viec check gia tri ID, Code
    ''' Tu dong gen code khi them moi
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New AttendanceRepository
        Dim _validate As New AT_SHIFTDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgDanhMuc.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateAT_SHIFT(_validate)
            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateAT_SHIFT(_validate)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub RadDateTime_SelectedDateChanged(sender As Object, e As EventArgs) Handles rdHours_Start.SelectedDateChanged, rdHours_Stop.SelectedDateChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            If CurrentState = CommonMessage.STATE_NEW Or CurrentState = CommonMessage.STATE_EDIT Then
                If rdHours_Start.SelectedDate IsNot Nothing Then
                    rdLateHour.SelectedDate = rdHours_Start.SelectedDate
                End If
                If rdHours_Stop.SelectedDate IsNot Nothing Then
                    rdEarlyHour.SelectedDate = rdHours_Stop.SelectedDate
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Import_Data()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"
    Private Sub Import_Data()
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim rep As New AttendanceRepository
            Dim store As New AttendanceStoreProcedure
            Dim log As New CommonBusiness.UserLog
            log = LogHelper.GetUserLog
            '_mylog = LogHelper.GetUserLog
            Dim ds As New DataSet
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                file.SaveAs(fileName, True)
                Using ep As New ExcelPackage
                    ds = ep.ReadExcelToDataSet(fileName, False)
                End Using
            End If
            If ds Is Nothing Then
                Exit Sub
            End If
            TableMapping1(ds.Tables(0))
            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If store.IMPORT_CALAMVIEC_ACCESS(DocXml, log.Username, log.Ip + log.ComputerName) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgDanhMuc.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('ORG_ACCESS')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)

            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
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
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Kiem tra gia tri dang đươc chọn cua combox MÃ CÔNG có tồn tại hoặc bị ngừng ap dụng hay không?
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cvalMaCong_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalMaCong.ServerValidate
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim rep As New AttendanceRepository

    '    Try
    '        If String.IsNullOrEmpty(cboMaCong.SelectedValue) Then
    '            Return
    '        End If

    '        ValueMaCong = cboMaCong.SelectedValue
    '        Dim result() As DataRow = rep.GetAT_TIME_MANUALBINCOMBO().Select("ID = " & cboMaCong.SelectedValue)
    '        If result.Length = 0 Then
    '            args.IsValid = False

    '            cboMaCong.ClearSelection()
    '            Dim dtData As DataTable
    '            dtData = rep.GetAT_TIME_MANUALBINCOMBO()
    '            FillRadCombobox(cboMaCong, dtData, "NAME", "ID")
    '            cboMaCong.SelectedIndex = 0
    '        Else
    '            args.IsValid = True
    '        End If

    '        _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Kiem tra gia tri dang đươc chọn cua combox KIỂU CÔNG CHỦ NHẬT có tồn tại hoặc bị ngừng áp dụng hay không?
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>

    Protected Sub rgDanhMuc_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgDanhMuc.SelectedIndexChanged
        Dim item As GridDataItem

        Try
            item = CType(rgDanhMuc.SelectedItems(0), GridDataItem)
            IDSelect = item.GetDataKeyValue("ID").ToString
            Dim AT_SHIFT1 = (From p In AT_SHIFT Where p.ID = Decimal.Parse(IDSelect)).SingleOrDefault
            'Dim value As String = Replace(AT_SHIFT1.SHIFT_DAY.ToString(), ",", ".")
            'cboNgayCongCa.SelectedValue = value
            'rdHours_Start.SelectedDate = AT_SHIFT1.HOURS_START
            'rdHours_Stop.SelectedDate = AT_SHIFT1.HOURS_STOP
            'rdSTART_MID_HOURS.SelectedDate = AT_SHIFT1.START_MID_HOURS
            'rdEND_MID_HOURS.SelectedDate = AT_SHIFT1.END_MID_HOURS
            'rdHOURS_STAR_CHECKIN.SelectedDate = AT_SHIFT1.HOURS_STAR_CHECKIN
            'rdHOURS_STAR_CHECKOUT.SelectedDate = AT_SHIFT1.HOURS_STAR_CHECKOUT
            'rdLateHour.SelectedDate = AT_SHIFT1.LATE_HOUR
            'rdEarlyHour.SelectedDate = AT_SHIFT1.EARLY_HOUR

            If (IDSelect = 1 Or IDSelect = 2) Then
                Me.MainToolBar = tbarCostCenters
                MainToolBar.Items(1).Enabled = False
                MainToolBar.Items(4).Enabled = False
            Else
                MainToolBar.Items(1).Enabled = True
                MainToolBar.Items(4).Enabled = True
            End If
            ID_Select = AT_SHIFT1.ID
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgDanhMuc_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgDanhMuc.ItemCommand
        Try
            Select Case e.CommandName
                Case "DETAIL"
                    Dim item = CType(e.Item, GridDataItem)
                    ID_Select = item.GetDataKeyValue("ID")
                    'Dim str As String = "window.open('Default.aspx?mid=Recruitment&fid=ctrlRC_HRPlaningDetail&group=Business&ID=" & item.GetDataKeyValue("ID") & "');"
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    isLoadPopup = 1
                    Enabled = False
                    UpdateControlState()
                    ctrlFindOrgPopup.Show()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindOrg.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 1
            Enabled = True
            UpdateControlState()
            ctrlFindOrgPopup.Show()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlPopupCommon_CancelClicked(ByVal sender As Object, ByVal e As EventArgs) Handles ctrlFindOrgPopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    Private Sub ctrlPopupCommon_CloseClicked(ByVal sender As Object, ByVal e As EventArgs) Handles ctrlFindOrgPopup.CloseClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            LIST_ORG = New List(Of CommonBusiness.UserOrgAccessDTO)
            'Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            Dim orgItem_Group = ctrlFindOrgPopup.CheckedValueKeys
            For i = 0 To orgItem_Group.Count - 1
                LIST_ORG.Add(New CommonBusiness.UserOrgAccessDTO() With {.ORG_ID = orgItem_Group(i)})
            Next
            'If orgItem IsNot Nothing Then
            '    hidOrgID.Value = e.CurrentValue
            '    txtOrgName2.Text = e.CurrentText
            'End If

            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
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
    Private Sub TableMapping1(ByVal dtTemp As System.Data.DataTable)
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "STT"
        dtTemp.Columns(1).ColumnName = "SHIFT_CODE"
        dtTemp.Columns(5).ColumnName = "AT_SHIFT_ID"
        dtTemp.Columns(6).ColumnName = "ORG_ID"
        dtTemp.Rows(0).Delete()

        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        Dim rep As New ProfileBusinessRepository
        Dim rep1 As New AttendanceRepository
        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            dtLogs.Columns.Add("ID", GetType(Integer))
            dtLogs.Columns.Add("SHIFT_CODE", GetType(String))
            dtLogs.Columns.Add("DISCIPTION", GetType(String))
        End If
        dtLogs.Clear()
        'XOA NHUNG DONG DU LIEU NULL SHIFT_ID
        Dim rowDel As DataRow
        For i As Integer = 1 To dtTemp.Rows.Count - 1
            If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            rowDel = dtTemp.Rows(i)
            If rowDel("AT_SHIFT_ID").ToString.Trim = "" Then
                dtTemp.Rows(i).Delete()
            End If
        Next

        For Each rows As DataRow In dtTemp.Rows
            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For
            newRow = dtLogs.NewRow
            newRow("SHIFT_CODE") = rows("SHIFT_CODE")

            Dim A = rows("ORG_ID")
            Dim B = rows("AT_SHIFT_ID")
            If Not IsNumeric(rows("ORG_ID")) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Phòng ban - Không đúng định dạng,"
                _error = False
            End If
            If Not IsNumeric(rows("AT_SHIFT_ID")) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Mã Ca làm việc - Không đúng định dạng,"
                _error = False
            End If
            If IsNumeric(rows("ORG_ID")) AndAlso IsNumeric(rows("AT_SHIFT_ID")) Then
                If rep1.ValidateAT_ORG_SHIFT(CDec(rows("AT_SHIFT_ID")), CDec(rows("ORG_ID"))) Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "dữ liệu đã tồn tại,"
                    _error = False
                End If
            End If

            If _error = False Then
                dtLogs.Rows.Add(newRow)
                _error = True
            End If
            count += 1
        Next

        dtTemp.AcceptChanges()
    End Sub
#End Region

End Class