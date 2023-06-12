Imports System.Globalization
Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Calendar
Imports WebAppLog

Public Class ctrlRegisterCONewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()

#Region "Property"

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property isFlag As Boolean
        Get
            Return ViewState(Me.ID & "_isFlag")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isFlag") = value
        End Set
    End Property

    Property isEdit As Boolean
        Get
            Return ViewState(Me.ID & "_isEdit")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isEdit") = value
        End Set
    End Property

    Property Employee_id As Integer
        Get
            Return ViewState(Me.ID & "_Employee_id")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_Employee_id") = value
        End Set
    End Property

    Property _Value As Decimal?
        Get
            Return ViewState(Me.ID & "_Value")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_Value") = value
        End Set
    End Property

    Property nBALANCE As Decimal?
        Get
            Return ViewState(Me.ID & "_nBALANCE")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_nBALANCE") = value
        End Set
    End Property

    Property RegisterLeave As AT_LEAVESHEETDTO
        Get
            Return ViewState(Me.ID & "_AT_LEAVESHEETDTO")
        End Get
        Set(ByVal value As AT_LEAVESHEETDTO)
            ViewState(Me.ID & "_AT_LEAVESHEETDTO") = value
        End Set
    End Property

    Property ListComboData As Attendance.AttendanceBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Attendance.AttendanceBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    Public ReadOnly Property CurrentUser As UserDTO
        Get
            Return LogHelper.CurrentUser
        End Get
    End Property

    Property totalDayResOld As Integer
        Get
            Return ViewState(Me.ID & "_totalDayResOld")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_totalDayResOld") = value
        End Set
    End Property

    Property dtDetail As DataTable
        Get
            Return ViewState(Me.ID & "_dtDetail")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDetail") = value
        End Set
    End Property

    Property rPH As DataRow
        Get
            Return ViewState(Me.ID & "_rPH")
        End Get
        Set(ByVal value As DataRow)
            ViewState(Me.ID & "_rPH") = value
        End Set
    End Property

    Property lstManual As List(Of AT_TIME_MANUALDTO)
        Get
            Return ViewState(Me.ID & "_lstManual")
        End Get
        Set(ByVal value As List(Of AT_TIME_MANUALDTO))
            ViewState(Me.ID & "_lstManual") = value
        End Set
    End Property
    Property numberHide As Integer
        Get
            Return ViewState(Me.ID & "_numberHide")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_numberHide") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CurrentState = CommonMessage.STATE_EDIT
            If Not IsPostBack Then
                Label1.Visible = False
                rnTS_SON.Visible = False
                rnTS_SON.Value = 1
                txtNote.Visible = False
                lblLeaveTT_TS.Visible = False
                rdLeaveTT_TS.Visible = False
                Refresh()
            End If
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgWorkschedule
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel)

            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Bind lai du lieu cho grid rgWorkschedule
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dsLeaveSheet As New DataSet()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Message = Request.Params("VIEW")
            Dim Struct As Decimal = 1
            Dim ID_PH As Decimal = 0
            If IsNumeric(Request.Params("ID")) Then
                Struct = 0
                ID_PH = Decimal.Parse(Request.Params("ID"))
            End If
            Using rep As New AttendanceRepository
                dsLeaveSheet = rep.GetLeaveSheet_ById(ID_PH, Struct)
            End Using
            If dsLeaveSheet IsNot Nothing Then
                If dsLeaveSheet.Tables(0) IsNot Nothing Then
                    rPH = dsLeaveSheet.Tables(0).NewRow
                    If dsLeaveSheet.Tables(0).Rows.Count > 0 Then
                        rPH = dsLeaveSheet.Tables(0).Rows(0)
                        Dim TYPE = (From p In lstManual Where p.ID = CDec(dsLeaveSheet.Tables(0).Rows(0)("MANUAL_ID").ToString)).FirstOrDefault.ORDER_W
                        If IsNumeric(TYPE) AndAlso TYPE = 1 Then
                            ChangeVisiable(True)
                        Else
                            ChangeVisiable(False)
                        End If
                        Dim count = (From p In lstManual Where p.ID = CDec(dsLeaveSheet.Tables(0).Rows(0)("MANUAL_ID").ToString) And p.CODE = "TS").Count()

                        If count = 1 Then
                            Label1.Visible = True
                            rnTS_SON.Visible = True
                            lblLeaveTT_TS.Visible = True
                            rdLeaveTT_TS.Visible = True
                        Else
                            Label1.Visible = False
                            rnTS_SON.Visible = False
                            lblLeaveTT_TS.Visible = False
                            rdLeaveTT_TS.Visible = False
                        End If
                        Dim count1 = (From p In lstManual Where p.ID = CDec(dsLeaveSheet.Tables(0).Rows(0)("MANUAL_ID").ToString) And p.CODE = "P").Count()
                        Dim count2 = (From p In lstManual Where p.ID = CDec(dsLeaveSheet.Tables(0).Rows(0)("MANUAL_ID").ToString) And p.CODE = "B").Count()
                        If count = 1 Then 'TS
                            HideItem(1)
                        ElseIf count1 = 1 Then 'P
                            HideItem(2)
                        ElseIf count2 = 1 Then 'B
                            HideItem(3)
                        Else
                            HideItem(1)
                        End If
                    Else
                        HideItem(numberHide)
                    End If
                Else
                    HideItem(numberHide)
                End If
                If dsLeaveSheet.Tables(1) IsNot Nothing Then
                    If dtDetail Is Nothing Then
                        dtDetail = dsLeaveSheet.Tables(1).Clone()
                        dtDetail = dsLeaveSheet.Tables(1)
                    End If
                End If
            Else
                HideItem(numberHide)
            End If
            Select Case Message
                Case "TRUE"
                    CreateDataBinDing(1)
            End Select
            'rdFROM_LEAVE_SelectedDateChanged(Nothing, Nothing) --???
            rgData.Rebind()

            'For Each item As GridDataItem In rgData.MasterTableView.Items
            '    item.Edit = True
            'Next
            'rgData.MasterTableView.Rebind()

            For Each item As GridDataItem In rgData.Items
                item.Edit = True
            Next
            rgData.Rebind()

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        Finally
            dsLeaveSheet.Dispose()
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            'Refresh()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi taso toolbar voi cac button them moi, sua, xuat file, xoa, in hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rgRegisterLeave
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter(False)
            'GetLeaveSheet_Detail()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    'Protected Sub ckNON_LEAVE_OnCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    Try
    '        Dim edit = CType(sender, CheckBox)
    '        If edit.Enabled = False Then
    '            Exit Sub
    '        End If
    '        Dim item = CType(edit.NamingContainer, GridEditableItem)
    '        Dim LEAVE_DAY = item.GetDataKeyValue("LEAVE_DAY")
    '        For Each rows In dtDetail.Rows
    '            If rows("LEAVE_DAY") = LEAVE_DAY Then

    '            End If
    '        Next
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Protected Sub cbSTATUS_SHIFT_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            Dim edit = CType(sender, RadComboBox)
            If edit.Enabled = False Then
                Exit Sub
            End If
            Dim item = CType(edit.NamingContainer, GridEditableItem)
            ' If Not IsNumeric(edit.SelectedValue) Then Exit Sub
            Dim EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
            Dim LEAVE_DAY = item.GetDataKeyValue("LEAVE_DAY")
            For Each rows In dtDetail.Rows
                If rows("LEAVE_DAY") = LEAVE_DAY Then
                    rows("STATUS_SHIFT") = If(IsNumeric(edit.SelectedValue), edit.SelectedValue, 0)
                    If edit.SelectedValue IsNot Nothing AndAlso edit.SelectedValue <> "" Then
                        'rows("DAY_NUM") = 0.5
                        rows("DAY_NUM") = Decimal.Parse(rows("SHIFT_DAY")) / 2
                    Else
                        'rows("DAY_NUM") = 1
                        rows("DAY_NUM") = Decimal.Parse(rows("SHIFT_DAY"))
                    End If
                    Exit For
                End If
            Next
            rgData.Rebind()
            For Each items As GridDataItem In rgData.MasterTableView.Items
                items.Edit = True
            Next
            rgData.MasterTableView.Rebind()
            Cal_DayLeaveSheet()

            ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "text", "IsBlock()", True)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rdFROM_LEAVE_SelectedDateChanged(ByVal sender As Object, ByVal e As SelectedDateChangedEventArgs) Handles rdLEAVE_FROM.SelectedDateChanged
        If (Not IsDate(rdLEAVE_FROM.SelectedDate) OrElse dtDetail Is Nothing OrElse Not IsNumeric(rtEmployee_id.Text)) Then Exit Sub
        Dim store As New AttendanceStoreProcedure
        Try
            Dim ds = store.GET_TER_LAST_DATE(Decimal.Parse(rtEmployee_id.Text.Trim), rdLEAVE_FROM.SelectedDate)
            Dim dateCheck As Boolean
            Dim result As Date
            If ds.Rows.Count > 0 AndAlso Not IsDBNull(ds.Rows(0)("TER_LAST_DATE")) Then
                dateCheck = DateTime.TryParseExact(ds.Rows(0)("TER_LAST_DATE"), "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result)
                If result < rdLEAVE_FROM.SelectedDate Then
                    ShowMessage(Translate("Nhân viên đã nghỉ việc từ ngày " + result.AddDays(1)), NotifyType.Warning)
                    rdLEAVE_FROM.SelectedDate = Nothing
                    Exit Sub
                End If
                If (IsDate(rdLEAVE_TO.SelectedDate) AndAlso result < rdLEAVE_TO.SelectedDate) Then
                    ShowMessage(Translate("Nhân viên đã nghỉ việc từ ngày " + result.AddDays(1)), NotifyType.Warning)
                    rdLEAVE_TO.SelectedDate = Nothing
                    Exit Sub
                End If
            End If

            ' Else
            Dim dt = store.GET_RESIDUAL_ALLOWANCES(Decimal.Parse(rtEmployee_id.Text.Trim), Decimal.Parse(cbMANUAL_ID.SelectedValue), rdLEAVE_FROM.SelectedDate)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim Day_Num = If(dt.Rows(0)("PV_DAY_NUM") IsNot Nothing, dt.Rows(0)("PV_DAY_NUM"), Nothing)
                Dim Total_Day_Registed As Decimal = If(dt.Rows(0)("PV_TOTAL") IsNot Nothing, dt.Rows(0)("PV_TOTAL"), Nothing)
                If Day_Num IsNot Nothing AndAlso Day_Num > 0 Then
                    Dim count_ts = (From p In lstManual Where p.ID = cbMANUAL_ID.SelectedValue And p.CODE = "TS").Count()
                    If count_ts = 1 Then
                        nBALANCE = Day_Num - Total_Day_Registed
                        rnTS_SON_TextChanged(Nothing, Nothing)
                    Else
                        rnBALANCE.Text = Day_Num - Total_Day_Registed
                        nBALANCE = Day_Num - Total_Day_Registed
                        lblQPCL.Text = Day_Num - Total_Day_Registed

                    End If

                End If
            End If
            Cal_DayEntitlement()
            If Not IsDate(rdLEAVE_TO.SelectedDate) Then Exit Sub
            If rdLEAVE_TO.SelectedDate < rdLEAVE_FROM.SelectedDate Then
                ShowMessage(Translate("Đến ngày phải lớn hơn hoặc bằng Từ ngày"), NotifyType.Warning)
                Exit Sub
            End If
            GetLeaveSheet_Detail()
            Cal_DayLeaveSheet()


            'End If
        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Sub

    Private Sub rdLEAVE_TO_SelectedDateChanged(ByVal sender As Object, ByVal e As SelectedDateChangedEventArgs) Handles rdLEAVE_TO.SelectedDateChanged
        If (Not IsDate(rdLEAVE_FROM.SelectedDate) OrElse dtDetail Is Nothing OrElse Not IsNumeric(rtEmployee_id.Text)) Then Exit Sub
        Dim store As New AttendanceStoreProcedure
        Try
            Dim ds = store.GET_TER_LAST_DATE(Decimal.Parse(rtEmployee_id.Text.Trim), rdLEAVE_FROM.SelectedDate)
            Dim dateCheck As Boolean
            Dim result As Date
            If ds.Rows.Count > 0 AndAlso Not IsDBNull(ds.Rows(0)("TER_LAST_DATE")) Then
                dateCheck = DateTime.TryParseExact(ds.Rows(0)("TER_LAST_DATE"), "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result)
                If result < rdLEAVE_FROM.SelectedDate Then
                    ShowMessage(Translate("Nhân viên đã nghỉ việc từ ngày " + result.AddDays(1)), NotifyType.Warning)
                    rdLEAVE_FROM.SelectedDate = Nothing
                    Exit Sub
                End If
                If (IsDate(rdLEAVE_TO.SelectedDate) AndAlso result < rdLEAVE_TO.SelectedDate) Then
                    ShowMessage(Translate("Nhân viên đã nghỉ việc từ ngày " + result.AddDays(1)), NotifyType.Warning)
                    rdLEAVE_TO.SelectedDate = Nothing
                    Exit Sub
                End If
            End If


            'Else
            Dim dt = store.GET_RESIDUAL_ALLOWANCES(Decimal.Parse(rtEmployee_id.Text.Trim), Decimal.Parse(cbMANUAL_ID.SelectedValue), rdLEAVE_FROM.SelectedDate)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim Day_Num = If(dt.Rows(0)("PV_DAY_NUM") IsNot Nothing, dt.Rows(0)("PV_DAY_NUM"), Nothing)
                Dim Total_Day_Registed As Decimal = If(dt.Rows(0)("PV_TOTAL") IsNot Nothing, dt.Rows(0)("PV_TOTAL"), Nothing)
                If Day_Num IsNot Nothing AndAlso Day_Num > 0 Then
                    rnBALANCE.Text = Day_Num - Total_Day_Registed
                    nBALANCE = Day_Num - Total_Day_Registed
                    lblQPCL.Text = Day_Num - Total_Day_Registed

                End If
            End If

            If Not IsDate(rdLEAVE_TO.SelectedDate) Then Exit Sub
            If rdLEAVE_TO.SelectedDate < rdLEAVE_FROM.SelectedDate Then
                ShowMessage(Translate("Đến ngày phải lớn hơn hoặc bằng Từ ngày"), NotifyType.Warning)
                Exit Sub
            End If
            GetLeaveSheet_Detail()
            Cal_DayLeaveSheet()
            Cal_DayEntitlement()

            'End If
        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Sub
    Private Sub cbMANUAL_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles cbMANUAL_ID.SelectedIndexChanged
        Try
            ClearControlValue(rtFROM_LOCATION, rtTO_LOCATION, rnNUMBER_KILOMETER, rnBALANCE, txtCUR_HAVE, cbPROVINCE_ID, lblQPCL, rdLEAVE_FROM, rdLEAVE_TO, rnDAY_NUM)
            If cbMANUAL_ID.SelectedValue <> "" Then
                Dim TYPE = (From p In lstManual Where p.ID = cbMANUAL_ID.SelectedValue).FirstOrDefault.ORDER_W
                If IsNumeric(TYPE) AndAlso TYPE = 1 Then
                    ChangeVisiable(True)
                Else
                    ChangeVisiable(False)
                End If
                Dim count = (From p In lstManual Where p.ID = cbMANUAL_ID.SelectedValue And p.CODE = "TS").Count()

                If count = 1 Then
                    Label1.Visible = True
                    rnTS_SON.Visible = True
                    lblLeaveTT_TS.Visible = True
                    rdLeaveTT_TS.Visible = True
                    If rnBALANCE.Text <> "" Then
                        rnBALANCE.Text = nBALANCE + (rnTS_SON.Value - 1) * 30
                        lblQPCL.Text = nBALANCE + (rnTS_SON.Value - 1) * 30
                    End If

                Else
                    Label1.Visible = False
                    rnTS_SON.Visible = False
                    lblLeaveTT_TS.Visible = False
                    rdLeaveTT_TS.Visible = False
                End If

                Dim count2 = (From p In lstManual Where p.ID = cbMANUAL_ID.SelectedValue And (p.CODE = "P" Or p.CODE1 = "P" Or p.CODE2 = "P")).Count()
                If count2 = 1 Then
                    Dim BALANCE As Decimal = 0
                    If IsNumeric(rnCUR_HAVE_INMONTH.Text) Then
                        BALANCE += Decimal.Parse(rnCUR_HAVE_INMONTH.Text)
                    End If
                    If IsNumeric(rtCUR_USED_INMONTH.Text) Then
                        BALANCE -= Decimal.Parse(rtCUR_USED_INMONTH.Text)
                    End If
                    If IsNumeric(rnChange.Text) Then
                        BALANCE += Decimal.Parse(rnChange.Text)
                    End If
                    If IsNumeric(rnPREVTOTAL_HAVE.Text) Then
                        BALANCE += Decimal.Parse(rnPREVTOTAL_HAVE.Text)
                    End If
                    If IsNumeric(rnSENIORITYHAVE.Text) Then
                        BALANCE += Decimal.Parse(rnSENIORITYHAVE.Text)
                    End If
                    If IsNumeric(rnLeave.Text) Then
                        BALANCE -= Decimal.Parse(rnLeave.Text)
                    End If
                    If IsNumeric(rnRO.Text) Then
                        BALANCE -= Decimal.Parse(rnRO.Text)
                    End If
                    rnBALANCE.Text = BALANCE.ToString()
                    lblQPCL.Text = BALANCE.ToString()
                End If
                Dim count3 = (From p In lstManual Where p.ID = cbMANUAL_ID.SelectedValue And (p.CODE = "B" Or p.CODE1 = "B" Or p.CODE2 = "B")).Count()
                If count = 1 Then 'TS
                    numberHide = 1
                    HideItem(numberHide)
                ElseIf count2 = 1 Then 'P
                    numberHide = 2
                    HideItem(numberHide)
                ElseIf count3 = 1 Then 'B
                    numberHide = 3
                    HideItem(numberHide)
                Else
                    numberHide = 1
                    HideItem(numberHide)
                End If
                Dim count_CheckNotify = (From p In lstManual Where p.ID = cbMANUAL_ID.SelectedValue And p.IS_LEAVE = -1).Count()
                If count_CheckNotify = 1 Then
                    txtNote.Visible = True
                    Dim Note = (From p In lstManual Where p.ID = cbMANUAL_ID.SelectedValue).FirstOrDefault.NOTE
                    txtNote.Text = Note
                Else
                    txtNote.Visible = False
                End If
            Else
                ChangeVisiable(False)
            End If
            If IsDate(rdLEAVE_FROM.SelectedDate) Then
                'Cal_DayEntitlement()
                rdFROM_LEAVE_SelectedDateChanged(Nothing, Nothing)
            End If
            If (Not IsDate(rdLEAVE_FROM.SelectedDate) OrElse Not IsDate(rdLEAVE_TO.SelectedDate) OrElse dtDetail Is Nothing OrElse Not IsNumeric(rtEmployee_id.Text)) Then Exit Sub
            'GetLeaveSheet_Detail()
            'Cal_DayLeaveSheet()
            'Cal_DayEntitlement()
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Event click button search ma nhan vien
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFindCommon_Click(ByVal sender As Object,
                                    ByVal e As EventArgs) Handles _
                                btnFindEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
            End Select
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click nut cancel cua popup ctrlFindPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 0
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command xuat luu, huy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim store As New AttendanceStoreProcedure
        Try
            Dim objValidate As New AT_LEAVESHEETDTO
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        'check so ngay dang ky nghi

                        If Not IsNumeric(rnDAY_NUM.Value) OrElse rnDAY_NUM.Value <= 0 Then
                            ShowMessage(Translate("Số ngày đăng ký nghỉ phải lơn hơn 0"), NotifyType.Warning)
                            Exit Sub
                        End If

                        Dim code As String = ""
                        Dim code1 As String = ""
                        Dim code2 As String = ""
                        If cbMANUAL_ID.SelectedValue <> "" Then
                            code = (From p In lstManual Where p.ID = cbMANUAL_ID.SelectedValue).FirstOrDefault.CODE
                            code1 = (From p In lstManual Where p.ID = cbMANUAL_ID.SelectedValue).FirstOrDefault.CODE1
                            code2 = (From p In lstManual Where p.ID = cbMANUAL_ID.SelectedValue).FirstOrDefault.CODE2
                        End If

                        If code.ToUpper.Equals("P") Or (code1 <> code2 And (code1 = "P" Or code2 = "P")) Then
                            Dim intBalance As Double = If(rnBALANCE.Text.Trim = "", 0, Double.Parse(rnBALANCE.Text.Trim))
                            Dim daynum As Double = rnDAY_NUM.Value
                            If code1 <> code2 And (code1 = "P" Or code2 = "P") Then
                                daynum = daynum / 2
                            End If

                            If (daynum > intBalance) Then
                                ShowMessage(Translate("Số ngày đăng ký nghỉ	lớn hơn Quỹ phép còn lại, vui lòng điều chỉnh lại dữ liệu"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If


                        If code.ToUpper.Equals("P") OrElse (code1 <> code2 And (code1 = "P" Or code2 = "P")) OrElse code.ToUpper.Equals("B") OrElse (code1 <> code2 And (code1 = "B" OrElse code2 = "B")) Then
                            Dim dtExpireDateData As DataTable = rep.GET_EXPIREDATE_P_BU(rtEmployee_id.Text, rdLEAVE_FROM.SelectedDate)
                            If dtExpireDateData Is Nothing OrElse dtExpireDateData.Rows.Count = 0 Then
                                ShowMessage(Translate("Nhân viên chưa có phép năm. Vui lòng tổng hợp phép năm cho nhân viên."), NotifyType.Warning)
                                Exit Sub
                            End If
                            Dim expireDateP As Date
                            Dim expireDateB As Date
                            Dim daynum As Double = rnDAY_NUM.Value
                            Dim dateCheck = DateTime.TryParseExact(dtExpireDateData(0)("EXPIREDATE_P"), "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, expireDateP)
                            Dim dateCheckB = DateTime.TryParseExact(dtExpireDateData(0)("EXPIREDATE_P"), "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, expireDateB)

                            If (code.ToUpper.Equals("P") Or (code1 <> code2 And (code1 = "P" Or code2 = "P"))) AndAlso dateCheck Then
                                If rdLEAVE_FROM.SelectedDate <= expireDateP And rdLEAVE_TO.SelectedDate >= expireDateP And Double.Parse(rnPREVTOTAL_HAVE.Text.Trim) > 0 Then
                                    Dim sumDay As Decimal
                                    For Each rows As DataRow In dtDetail.Rows
                                        Dim LEAVE_DAY = rows("LEAVE_DAY")
                                        If LEAVE_DAY <= expireDateP Then
                                            sumDay += rows("DAY_NUM")
                                        End If
                                    Next
                                    Dim PrevcurHave As Decimal = Math.Min(Double.Parse(rnPREVTOTAL_HAVE.Text.Trim), sumDay)
                                    If code1 <> code2 And (code1 = "P" Or code2 = "P") Then
                                        daynum = daynum / 2
                                    End If
                                    If (PrevcurHave + Double.Parse(rnBALANCE.Text.Trim)) < daynum Then
                                        ShowMessage(Translate("Số ngày đăng ký nghỉ	lớn hơn Quỹ phép còn lại, vui lòng điều chỉnh lại dữ liệu"), NotifyType.Warning)
                                        Exit Sub
                                    End If
                                Else
                                    Dim intBalance As Double = If(rnBALANCE.Text.Trim = "", 0, Double.Parse(rnBALANCE.Text.Trim))
                                    If code1 <> code2 And (code1 = "P" Or code2 = "P") Then
                                        daynum = daynum / 2
                                    End If
                                    If (daynum > intBalance) Then
                                        ShowMessage(Translate("Số ngày đăng ký nghỉ	lớn hơn Quỹ phép còn lại, vui lòng điều chỉnh lại dữ liệu"), NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If

                            End If


                            If (code.ToUpper.Equals("B") Or (code1 <> code2 And (code1 = "B" Or code2 = "B"))) AndAlso dateCheckB Then

                                Dim dtSourceNB = store.GET_INFO_NGHIBU(Decimal.Parse(rtEmployee_id.Text.Trim), rdLEAVE_FROM.SelectedDate)
                                Dim PREV_HAVE1 As Decimal = 0
                                If dtSourceNB IsNot Nothing AndAlso dtSourceNB.Rows.Count > 0 Then
                                    PREV_HAVE1 = If(dtSourceNB.Rows(0)("PREV_HAVE1") Is Nothing, 0, CDec(dtSourceNB.Rows(0)("PREV_HAVE1").ToString()))
                                End If
                                If rdLEAVE_FROM.SelectedDate <= expireDateB And rdLEAVE_TO.SelectedDate >= expireDateB And PREV_HAVE1 > 0 Then
                                    Dim sumDay As Decimal
                                    For Each rows As DataRow In dtDetail.Rows
                                        Dim LEAVE_DAY = rows("LEAVE_DAY")
                                        If LEAVE_DAY <= expireDateB Then
                                            sumDay += rows("DAY_NUM")
                                        End If
                                    Next
                                    Dim PrevcurHave As Decimal = Math.Min(PREV_HAVE1, sumDay)
                                    If code1 <> code2 And (code1 = "B" Or code2 = "B") Then
                                        daynum = daynum / 2
                                    End If
                                    If (PrevcurHave + daynum) > Double.Parse(rnBALANCE.Text.Trim) Then
                                        ShowMessage(Translate("Số ngày đăng ký nghỉ	lớn hơn Quỹ phép phép bù còn lại, vui lòng điều chỉnh lại dữ liệu"), NotifyType.Warning)
                                        Exit Sub
                                    End If
                                Else

                                    'Dim intBalance As Double = If(rnCUR_DANGKY.Text.Trim = "", 0, Double.Parse(rnCUR_DANGKY.Text.Trim))
                                    Dim intBalance As Double = If(dtSourceNB.Rows(0)("BALANCE") Is Nothing, 0, CDec(dtSourceNB.Rows(0)("BALANCE").ToString())) ' If(rnBALANCE.Text.Trim = "", 0, Double.Parse(rnBALANCE.Text.Trim))
                                    If code1 <> code2 And (code1 = "B" Or code2 = "B") Then
                                        daynum = daynum / 2
                                    End If
                                    If (daynum > intBalance) Then
                                        ShowMessage(Translate("Số ngày đăng ký nghỉ lớn hơn Quỹ phép phép bù còn lại, vui lòng điều chỉnh lại dữ liệu"), NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If
                            End If
                        Else
                            Dim item = (From p In lstManual Where p.ID = cbMANUAL_ID.SelectedValue).FirstOrDefault
                            If item.FML_DAYNUM.HasValue AndAlso item.FML_DAYNUM > 0 AndAlso rnDAY_NUM.Value > (If(rnBALANCE.Text.Trim = "", 0, Double.Parse(rnBALANCE.Text.Trim)) + If(rnPREVTOTAL_HAVE.Text.Trim = "", 0, Double.Parse(rnPREVTOTAL_HAVE.Text.Trim))) Then
                                ShowMessage(Translate("Quỹ phép còn lại không đủ, vui lòng cập nhật ngày nghỉ"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If

                        If code.ToUpper.Equals("TS") OrElse code.ToUpper.Equals("KT") OrElse code.ToUpper.Equals("DS") Then
                            Dim dtData1 As DataTable = store.GET_GENDER(rtEmployee_id.Text)
                            If dtData1 IsNot Nothing AndAlso dtData1.Rows.Count > 0 Then
                                If dtData1.Rows(0)(0) = "0" Then
                                    ShowMessage(Translate("Loại nghỉ phép chỉ áp dụng nhân viên có giới tính là nữ"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If
                        End If

                        CreateDataBinDing(0)
                        objValidate.LEAVE_FROM = rdLEAVE_FROM.SelectedDate
                        objValidate.LEAVE_TO = rdLEAVE_TO.SelectedDate
                        objValidate.ID = Utilities.ObjToDecima(rPH("ID"))
                        objValidate.EMPLOYEE_ID = CDec(rtEmployee_id.Text)
                        'objValidate.LEAVETT_TS = rdLeaveTT_TS.SelectedDate
                        If (New AttendanceBusinessClient).ValidateLeaveSheetDetail(objValidate) = False Then
                            ShowMessage(Translate("Ngày đăng ký nghỉ đã bị trùng"), NotifyType.Warning)
                            Exit Sub
                        End If

                        If SaveDB() Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            'CurrentState = CommonMessage.STATE_NEW
                            Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlRegisterCO&group=Business")
                        Else
                            ShowMessage(Translate("Xảy ra lỗi"), NotifyType.Error)
                        End If
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule')")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlRegisterCO&group=Business")
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim empID = ctrlFindEmployeePopup.SelectedEmployeeID(0)
            FillData(empID)
            ClearControlValue(rnCUR_HAVE, rnPREV_HAVE, rnCUR_HAVE_INMONTH, rnCUR_USED, rtCUR_USED_INMONTH, rnPREVTOTAL_HAVE, rnCUR_DANGKY, rnSENIORITYHAVE,
                              txtCUR_HAVE, rnBALANCE, cbMANUAL_ID, rnDAY_NUM, rnTS_SON, rdLEAVE_FROM, rdLEAVE_TO, rdLeaveTT_TS, rtNote, rtReason_leave,
                              rtFROM_LOCATION, rtTO_LOCATION, rnNUMBER_KILOMETER, cbPROVINCE_ID, lblQPCL)
            ChangeVisiable()
            Label1.Visible = False
            rnTS_SON.Visible = False
            rnTS_SON.Value = 1
            txtNote.Visible = False
            lblLeaveTT_TS.Visible = False
            rdLeaveTT_TS.Visible = False
            rgData.DataSource = New DataTable()
            rgData.DataBind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles rgData.ItemDataBound
        Dim cbo As New RadComboBox
        Dim arr As New ArrayList()
        Try
            If e.Item.Edit Then
                Dim edit = CType(e.Item, GridEditableItem)
                Dim item As GridDataItem = CType(e.Item, GridDataItem)
                cbo = CType(edit.FindControl("cbSTATUS_SHIFT"), RadComboBox)
                arr.Add(New DictionaryEntry("", Nothing))
                arr.Add(New DictionaryEntry("Đầu ca", 1))
                arr.Add(New DictionaryEntry("Cuối ca", 2))
                With cbo
                    .DataSource = arr
                    .DataValueField = "Value"
                    .DataTextField = "Key"
                    cbo.DataBind()
                    .SelectedIndex = 0
                End With
                SetDataToGrid(edit)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            cbo.Dispose()
            arr = Nothing
        End Try
    End Sub

#End Region

#Region "Custom"

    Private Sub GetLeaveSheet_Detail()
        Dim dtSource As New DataTable()
        Try
            Using rep As New AttendanceRepository
                Dim employee_id As Decimal = Decimal.Parse(rtEmployee_id.Text.Trim)
                Dim manualID As Decimal = Decimal.Parse(cbMANUAL_ID.SelectedValue)
                dtSource = rep.GetLeaveSheet_Detail_ByDate(employee_id, rdLEAVE_FROM.SelectedDate, rdLEAVE_TO.SelectedDate, manualID)
            End Using
            dtDetail = dtSource
            rgData.Rebind()
            For Each item As GridDataItem In rgData.MasterTableView.Items
                item.Edit = True
            Next
            rgData.MasterTableView.Rebind()
        Catch ex As Exception
            Throw ex
        Finally
            If dtSource IsNot Nothing Then
                dtSource.Dispose()
            End If
        End Try
    End Sub

    Private Sub Cal_DayLeaveSheet()
        Try
            If dtDetail IsNot Nothing Then
                Dim sumDay = dtDetail.Compute("SUM(DAY_NUM)", "1=1")

                sumDay = If(IsNumeric(sumDay), sumDay, 0)
                rnDAY_NUM.NumberFormat.AllowRounding = False
                rnDAY_NUM.NumberFormat.DecimalDigits = 2
                rnDAY_NUM.Value = CType(sumDay, Decimal)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Cal_DayEntitlement()
        Try
            If rtEmployee_id.Text = "" Or rdLEAVE_FROM.SelectedDate Is Nothing Then
                Exit Sub
            End If

            Dim dtSourceEntitlement As New DataTable()
            Dim dtManual As New DataTable()
            Dim employee_id As Decimal = Decimal.Parse(rtEmployee_id.Text.Trim)
            Dim manualID As Decimal
            Try

                Using rep As New AttendanceRepository
                    manualID = Decimal.Parse(cbMANUAL_ID.SelectedValue)
                    dtManual = rep.GET_MANUAL_BY_ID(manualID)
                End Using
                If dtManual.Rows.Count > 0 Then
                    Clearn__DayEntitlement()

                    Dim strCode As String = dtManual.Rows(0)("CODE").ToString()
                    Dim strMorning As String = If(dtManual.Rows(0)("MORNING_ID") IsNot Nothing, dtManual.Rows(0)("MORNING_ID").ToString(), Nothing)
                    Dim strAfternoon As String = If(dtManual.Rows(0)("AFTERNOON_ID") IsNot Nothing, dtManual.Rows(0)("AFTERNOON_ID").ToString(), Nothing)
                    Dim strMorning_Code As String = If(dtManual.Rows(0)("MORNING_CODE") IsNot Nothing, dtManual.Rows(0)("MORNING_CODE").ToString(), Nothing)
                    Dim strAfternoon_code As String = If(dtManual.Rows(0)("AFTERNOON_CODE") IsNot Nothing, dtManual.Rows(0)("AFTERNOON_CODE").ToString(), Nothing)
                    If (strMorning = "251" Or strAfternoon = "251") Then
                        Using rep As New AttendanceRepository
                            dtSourceEntitlement = rep.GET_INFO_PHEPNAM(employee_id, rdLEAVE_FROM.SelectedDate)
                            If dtSourceEntitlement.Rows.Count > 0 Then
                                isFlag = True
                                rnCUR_HAVE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CHE_DO") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CHE_DO").ToString())
                                rnSENIORITYHAVE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_THAM_NIEN") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_THAM_NIEN").ToString())
                                rnPREV_HAVE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CU_CHUYEN_QUA") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CU_CHUYEN_QUA").ToString())
                                rnCUR_USED.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CU_DA_NGHI") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CU_DA_NGHI").ToString())
                                rnCUR_HAVE_INMONTH.Text = If(dtSourceEntitlement.Rows(0)("PHEP_THANG_LVIEC") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_THANG_LVIEC").ToString())
                                rnPREVTOTAL_HAVE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CU_CON_HLUC") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CU_CON_HLUC").ToString())
                                rtCUR_USED_INMONTH.Text = If(dtSourceEntitlement.Rows(0)("PHEP_DA_NGHI") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_DA_NGHI").ToString())
                                'rnBALANCE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CONLAI") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CONLAI").ToString())
                                rnChange.Text = If(dtSourceEntitlement.Rows(0)("PHEP_DIEUCHINH") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_DIEUCHINH").ToString())
                                rnOldChange.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CU_DIEUCHINH") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CU_DIEUCHINH").ToString())
                                rnOldLeave.Text = If(dtSourceEntitlement.Rows(0)("LEAVEOLD_PAYMENT") Is Nothing, "0", dtSourceEntitlement.Rows(0)("LEAVEOLD_PAYMENT").ToString())
                                rnLeave.Text = If(dtSourceEntitlement.Rows(0)("LEAVENEW_PAYMENT") Is Nothing, "0", dtSourceEntitlement.Rows(0)("LEAVENEW_PAYMENT").ToString())
                                rnRO.Text = If(dtSourceEntitlement.Rows(0)("LEAVE_RO") Is Nothing, "0", dtSourceEntitlement.Rows(0)("LEAVE_RO").ToString())
                                Dim BALANCE As Decimal = 0
                                If IsNumeric(rnCUR_HAVE_INMONTH.Text) Then
                                    BALANCE += Decimal.Parse(rnCUR_HAVE_INMONTH.Text)
                                End If
                                If IsNumeric(rtCUR_USED_INMONTH.Text) Then
                                    BALANCE -= Decimal.Parse(rtCUR_USED_INMONTH.Text)
                                End If
                                If IsNumeric(rnChange.Text) Then
                                    BALANCE += Decimal.Parse(rnChange.Text)
                                End If
                                If IsNumeric(rnPREVTOTAL_HAVE.Text) Then
                                    BALANCE += Decimal.Parse(rnPREVTOTAL_HAVE.Text)
                                End If
                                If IsNumeric(rnSENIORITYHAVE.Text) Then
                                    BALANCE += Decimal.Parse(rnSENIORITYHAVE.Text)
                                End If
                                If IsNumeric(rnLeave.Text) Then
                                    BALANCE -= Decimal.Parse(rnLeave.Text)
                                End If
                                If IsNumeric(rnRO.Text) Then
                                    BALANCE -= Decimal.Parse(rnRO.Text)
                                End If
                                BALANCE += If(dtSourceEntitlement.Rows(0)("ADJUST_MONTH_TN") Is Nothing, "0", dtSourceEntitlement.Rows(0)("ADJUST_MONTH_TN").ToString())
                                rnBALANCE.Text = BALANCE.ToString()
                                lblQPCL.Text = BALANCE.ToString()
                            Else
                                Clearn__DayEntitlement()
                                rnBALANCE.Text = ""
                                lblQPCL.Text = ""
                            End If
                        End Using
                    ElseIf (strMorning_Code = "B" Or strAfternoon_code = "B") Then
                        Dim store As New AttendanceStoreProcedure
                        Dim dtSourceNB = store.GET_INFO_NGHIBU(Decimal.Parse(rtEmployee_id.Text.Trim), rdLEAVE_FROM.SelectedDate)
                        If dtSourceNB.Rows.Count > 0 Then
                            txtCUR_HAVE.Text = If(dtSourceNB.Rows(0)("CUR_USED") Is Nothing, 0, CDec(dtSourceNB.Rows(0)("CUR_USED").ToString()))
                            rtCUR_USED_INMONTH.Text = If(dtSourceNB.Rows(0)("PHEP_DA_NGHI") Is Nothing, "0", dtSourceNB.Rows(0)("PHEP_DA_NGHI").ToString())
                            rnCUR_DANGKY.Text = If(dtSourceNB.Rows(0)("CUR_HAVE") Is Nothing, "0", dtSourceNB.Rows(0)("CUR_HAVE").ToString())

                            rnPREV_HAVE.Text = If(dtSourceNB.Rows(0)("PREV_HAVE") Is Nothing, "0", dtSourceNB.Rows(0)("PREV_HAVE").ToString())
                            rnCUR_USED.Text = If(dtSourceNB.Rows(0)("PREV_USED") Is Nothing, "0", dtSourceNB.Rows(0)("PREV_USED").ToString())
                            rnPREVTOTAL_HAVE.Text = If(dtSourceNB.Rows(0)("PREVTOTAL_HAVE") Is Nothing, "0", dtSourceNB.Rows(0)("PREVTOTAL_HAVE").ToString())
                            rnBALANCE.Text = If(dtSourceNB.Rows(0)("BALANCE") Is Nothing, "0", dtSourceNB.Rows(0)("BALANCE").ToString())
                            rnChange.Text = If(dtSourceNB.Rows(0)("PHEP_DIEUCHINH") Is Nothing, "0", dtSourceNB.Rows(0)("PHEP_DIEUCHINH").ToString())
                            rdChangeB.Text = If(dtSourceNB.Rows(0)("PHEP_DIEUCHINH") Is Nothing, "0", dtSourceNB.Rows(0)("PHEP_DIEUCHINH").ToString())
                            rnOldChange.Text = If(dtSourceNB.Rows(0)("PHEP_CU_DIEUCHINH") Is Nothing, "0", dtSourceNB.Rows(0)("PHEP_CU_DIEUCHINH").ToString())
                            If dtSourceEntitlement.Rows.Count > 0 Then
                                rnOldLeave.Text = If(dtSourceEntitlement.Rows(0)("PV_LEAVEOLD_PAYMENT") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PV_LEAVEOLD_PAYMENT").ToString())
                                rnLeave.Text = If(dtSourceEntitlement.Rows(0)("PV_LEAVENEW_PAYMENT") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PV_LEAVENEW_PAYMENT").ToString())
                                rnRO.Text = If(dtSourceEntitlement.Rows(0)("PV_LEAVE_RO") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PV_LEAVE_RO").ToString())
                            End If
                            Dim BALANCE As Decimal = 0
                            If IsNumeric(rnCUR_HAVE_INMONTH.Text) Then
                                BALANCE += Decimal.Parse(rnCUR_HAVE_INMONTH.Text)
                            End If
                            If IsNumeric(rnCUR_DANGKY.Text) Then
                                BALANCE += Decimal.Parse(rnCUR_DANGKY.Text)
                            End If
                            If IsNumeric(txtCUR_HAVE.Text) Then
                                BALANCE -= Decimal.Parse(txtCUR_HAVE.Text)
                            End If
                            If IsNumeric(rtCUR_USED_INMONTH.Text) Then
                                BALANCE -= Decimal.Parse(rtCUR_USED_INMONTH.Text)
                            End If
                            If IsNumeric(rnChange.Text) Then
                                BALANCE += Decimal.Parse(rnChange.Text)
                            End If
                            If IsNumeric(rnPREVTOTAL_HAVE.Text) Then
                                BALANCE += Decimal.Parse(rnPREVTOTAL_HAVE.Text)
                            End If
                            If IsNumeric(rnSENIORITYHAVE.Text) Then
                                BALANCE += Decimal.Parse(rnSENIORITYHAVE.Text)
                            End If
                            If IsNumeric(rnLeave.Text) Then
                                BALANCE -= Decimal.Parse(rnLeave.Text)
                            End If
                            If IsNumeric(rnRO.Text) Then
                                BALANCE -= Decimal.Parse(rnRO.Text)
                            End If
                            'rnBALANCE.Text = BALANCE.ToString()
                            lblQPCL.Text = rnBALANCE.Text
                        Else
                            txtCUR_HAVE.Text = 0
                            rnCUR_DANGKY.Text = 0
                        End If
                    Else
                        Clearn__DayEntitlement()
                        'rnBALANCE.Text = ""
                    End If
                    'If (strCode.ToUpper() = "B" And (strMorning_Code = "B" Or strAfternoon_code = "B")) Then

                    'Else
                    '    Clearn__DayEntitlement()
                    'End If
                Else
                    Clearn__DayEntitlement()
                    'rnBALANCE.Text = ""
                End If


            Catch ex As Exception
                Throw ex
            Finally
                dtSourceEntitlement.Dispose()
                dtManual.Dispose()
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Clearn__DayEntitlement()
        isFlag = False
        rnCUR_HAVE.Text = ""
        rnSENIORITYHAVE.Text = ""
        rnPREV_HAVE.Text = ""
        rnCUR_USED.Text = ""
        rnCUR_HAVE_INMONTH.Text = ""
        rnPREVTOTAL_HAVE.Text = ""
        rtCUR_USED_INMONTH.Text = ""
        rnCUR_DANGKY.Text = ""
        rnChange.Text = ""
        rnOldChange.Text = ""
        rnOldLeave.Text = ""
        rnLeave.Text = ""
        rnRO.Text = ""
        'rnBALANCE.Text = ""
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức update trạng thái của các control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.LoadAllOrganization = False
                        ctrlFindEmployeePopup.MustHaveContract = True
                    End If
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien load data cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New AttendanceRepository
        Dim repNS As New Profile.ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ListComboData Is Nothing Then
                ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO

                'Điều chỉnh Loại nghỉ (thêm điều kiện Loại xử lý Kiểu công: Đăng ký)
                ListComboData.GET_LIST_TYPE_MANUAL_LEAVE_FULL = True
                rep.GetComboboxData(ListComboData)
                FillRadCombobox(cbMANUAL_ID, ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE_FULL, "NAME_VN", "ID", True)
                lstManual = ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE_FULL

                Dim lstP As DataTable
                lstP = repNS.GetProvinceList(True)
                FillRadCombobox(cbPROVINCE_ID, lstP, "NAME", "ID")
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức lấy data cho rgRegisterLeave
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_LEAVESHEETDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If dtDetail IsNot Nothing Then
                rgData.VirtualItemCount = dtDetail.Rows.Count
                rgData.DataSource = dtDetail


                Dim objC = (From p In lstManual Where p.ID = cbMANUAL_ID.SelectedValue).FirstOrDefault
                If objC.IS_REG_SHIFT = True Then
                    If objC.CODE <> "" AndAlso objC.CODE <> "TS" AndAlso objC.CODE <> "NKL" AndAlso dtDetail.Rows.Count > 0 Then
                        If dtDetail.Select("SHIFT_DAY IS NULL").Any Then
                            ShowMessage("Ngày đăng ký nghỉ chưa được xếp ca", NotifyType.Warning)
                            ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "text", "showDetail('');", True)
                        Else
                            ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "text", "showDetail('none');", True)
                        End If
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "text", "showDetail('none');", True)
                    End If
                Else
                    If objC.CODE <> "" AndAlso objC.CODE <> "TS" AndAlso objC.CODE <> "NKL" AndAlso dtDetail.Rows.Count > 0 AndAlso objC.CODE1 <> objC.CODE2 Then
                        If objC.CODE1 = "P" OrElse objC.CODE1 = "B" OrElse objC.CODE2 = "P" OrElse objC.CODE2 = "B" Then
                            If dtDetail.Select("SHIFT_DAY IS NULL").Any Then
                                ShowMessage("Ngày đăng ký nghỉ chưa được xếp ca", NotifyType.Warning)
                                ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "text", "showDetail('');", True)
                            Else
                                ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "text", "showDetail('none');", True)
                            End If
                        End If
                    End If
                End If


                'rgData.Rebind()
                'For Each item As GridDataItem In rgData.MasterTableView.Items
                '    item.Edit = True
                'Next
                'rgData.MasterTableView.Rebind()
                Cal_DayLeaveSheet()
                Cal_DayEntitlement()
            End If
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
        Return New DataTable()
    End Function

    ''' <summary>
    ''' Fill data len control theo ID
    ''' </summary>
    ''' <param name="empid">Ma nhan vien</param>
    ''' <remarks></remarks>
    Private Sub FillData(ByVal empid As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dtData As New DataTable()
        Dim dateValue = Date.Now
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New Profile.ProfileBusinessRepository
                Dim obj = rep.GetEmployeCurrentByID(New WorkingDTO With {.EMPLOYEE_ID = empid})
                If IsNumeric(obj.ID) Then
                    rtEmployee_id.Text = obj.EMPLOYEE_ID.ToString()
                End If
                rtEmployee_Name.Text = obj.EMPLOYEE_NAME
                rtEmployee_Code.Text = obj.EMPLOYEE_CODE
                rtOrg_Name.Text = obj.ORG_NAME
                If IsNumeric(obj.ORG_ID) Then
                    rtOrg_id.Text = obj.ORG_ID.ToString()
                End If
                rtTitle_Name.Text = obj.TITLE_NAME
                If IsNumeric(obj.TITLE_ID) Then
                    rtTitle_Id.Text = obj.TITLE_ID.ToString()
                End If

            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub SetDataToGrid(ByVal EditItem As GridEditableItem)
        Dim cbo As New RadComboBox
        Dim arr As New ArrayList()
        Try
            Dim LEAVE_DAY = EditItem.GetDataKeyValue("LEAVE_DAY")
            Dim STATUS_SHIFT
            Dim MANUAL_ID
            Dim IS_DEDUCT_SHIFT
            Dim SHIFT_DAY
            For Each rows As DataRow In dtDetail.Rows
                If rows("LEAVE_DAY") = LEAVE_DAY Then
                    STATUS_SHIFT = rows("STATUS_SHIFT")
                    MANUAL_ID = rows("MANUAL_ID")
                    IS_DEDUCT_SHIFT = rows("IS_DEDUCT_SHIFT")
                    SHIFT_DAY = rows("SHIFT_DAY")
                    Exit For
                End If
            Next
            cbo = CType(EditItem.FindControl("cbSTATUS_SHIFT"), RadComboBox)
            arr.Add(New DictionaryEntry("", Nothing))
            arr.Add(New DictionaryEntry("Đầu ca", 1))
            arr.Add(New DictionaryEntry("Cuối ca", 2))
            With cbo
                .DataSource = arr
                .DataValueField = "Value"
                .DataTextField = "Key"
                cbo.DataBind()
                .SelectedIndex = 0
            End With
            If IsNumeric(STATUS_SHIFT) Then
                cbo.SelectedValue = STATUS_SHIFT
                cbo.Enabled = If(Not IsNumeric(MANUAL_ID), False, True)
            End If
            If IS_DEDUCT_SHIFT = 0 Then
                cbo.Enabled = False
            Else
                If IsDBNull(SHIFT_DAY) = True Then
                    cbo.Enabled = False
                Else
                    If SHIFT_DAY = 0 Then
                        cbo.Enabled = False
                    Else
                        cbo.Enabled = True
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub CreateDataBinDing(ByVal Mode As Decimal)
        '1 set data in list control
        '0 get data to list control
        Select Case Mode
            Case 1
                For Each ctrs As Control In RadPane1.Controls
                    Try
                        Select Case ctrs.ID.ToString.ToUpper.Substring(0, 2)
                            Case "cb".ToUpper
                                CType(ctrs, RadComboBox).SelectedValue = rPH(ctrs.ID.ToString.ToUpper.Substring(2))
                            Case "rt".ToUpper
                                CType(ctrs, RadTextBox).Text = rPH(ctrs.ID.ToString.ToUpper.Substring(2))
                            Case "rn".ToUpper
                                CType(ctrs, RadNumericTextBox).Value = If(IsNumeric(rPH(ctrs.ID.ToString.ToUpper.Substring(2))), CDec(rPH(ctrs.ID.ToString.ToUpper.Substring(2))), Nothing)
                            Case "rd".ToUpper
                                CType(ctrs, RadDatePicker).SelectedDate = rPH(ctrs.ID.ToString.ToUpper.Substring(2))
                            Case Else
                                Continue For
                        End Select
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
            Case 0
                For Each ctrs As Control In RadPane1.Controls
                    Try
                        Select Case ctrs.ID.ToString.ToUpper.Substring(0, 2)
                            Case "cb".ToUpper
                                rPH(ctrs.ID.ToString.ToUpper.Substring(2)) = CType(ctrs, RadComboBox).SelectedValue
                            Case "rt".ToUpper
                                rPH(ctrs.ID.ToString.ToUpper.Substring(2)) = CType(ctrs, RadTextBox).Text
                            Case "rn".ToUpper
                                rPH(ctrs.ID.ToString.ToUpper.Substring(2)) = CType(ctrs, RadNumericTextBox).Value
                            Case "rd".ToUpper
                                rPH(ctrs.ID.ToString.ToUpper.Substring(2)) = CType(ctrs, RadDatePicker).SelectedDate
                            Case Else
                                Continue For
                        End Select
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
        End Select

    End Sub

    Private Function SaveDB() As Boolean
        Dim rep As New AttendanceRepository
        Dim PH As DataTable = New DataTable()
        Dim dr As DataRow() = New DataRow() {rPH}
        PH = dr.CopyToDataTable()
        PH.TableName = "PH"
        Dim dsLeaveSheet As New DataSet("DATA")
        Dim CT As New DataTable()
        dsLeaveSheet.Tables.Add(PH)
        CT = dtDetail
        CT.TableName = "CT"
        'dsLeaveSheet.Tables.Remove("CT")
        dsLeaveSheet.Tables.Add(CT.Copy())
        Try
            Return rep.SaveLeaveSheet(dsLeaveSheet)
        Catch ex As Exception
            Return False
        Finally
            rep.Dispose()
            CT.Dispose()
            PH.Dispose()
        End Try
    End Function

    Private Sub ChangeVisiable(Optional ByVal _status As Boolean = False)
        lbFromLoc.Visible = _status
        lbToLoc.Visible = _status
        lbNumKM.Visible = _status
        rtFROM_LOCATION.Visible = _status
        rtTO_LOCATION.Visible = _status
        rnNUMBER_KILOMETER.Visible = _status
        lbPROVINCEEMP_ID.Visible = _status
        cbPROVINCE_ID.Visible = _status
    End Sub

    Private Sub rnTS_SON_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rnTS_SON.TextChanged
        Try
            Dim count = (From p In lstManual Where p.ID = cbMANUAL_ID.SelectedValue And p.CODE = "TS").Count()
            'If rnBALANCE.Text <> "" Then
            '    rnBALANCE.Text = nBALANCE + (rnTS_SON.Value - 1) * 30
            'End If
            If rnTS_SON.Value Is Nothing OrElse rnTS_SON.Value = 0 Then
                rnTS_SON.Value = 1
            End If

            If count >= 1 Then
                rnBALANCE.Text = nBALANCE + (rnTS_SON.Value - 1) * 30
                lblQPCL.Text = nBALANCE + (rnTS_SON.Value - 1) * 30
                'Tính rdLEAVE_TO,rdLeaveTT_TS
                If IsDate(rdLEAVE_FROM.SelectedDate) AndAlso IsNumeric(rnBALANCE.Text) Then
                    Dim LEAVE_FROM_Af = CDate(rdLEAVE_FROM.SelectedDate)
                    'Dim formula = CDec(rnBALANCE.Text) + (CInt(rnTS_SON.Value) - 1) * 30
                    Dim dateTo = LEAVE_FROM_Af.AddMonths(6)
                    dateTo = dateTo.AddDays(-1)
                    If rnTS_SON.Value > 1 Then
                        dateTo = dateTo.AddDays((rnTS_SON.Value - 1) * 30)
                    End If

                    rdLEAVE_TO.SelectedDate = dateTo ' LEAVE_FROM_Af.AddDays(CDec(rnBALANCE.Text) - 1)
                    If IsPostBack Then
                        rdLeaveTT_TS.SelectedDate = dateTo ' LEAVE_FROM_Af.AddDays(CDec(rnBALANCE.Text) - 1)
                    End If
                    GetLeaveSheet_Detail()
                    Cal_DayLeaveSheet()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rtEmployee_Code_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rtEmployee_Code.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If rtEmployee_Code.Text <> "" Then
                    Reset_Find_Emp()
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = rtEmployee_Code.Text
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        rtEmployee_Code.Text = ""
                    ElseIf Count = 1 Then
                        Dim item = EmployeeList(0)
                        FillData(item.EMPLOYEE_ID)
                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                            phFindEmp.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = rtEmployee_Code.Text
                            ctrlFindEmployeePopup.MultiSelect = False
                            ctrlFindEmployeePopup.LoadAllOrganization = False
                            ctrlFindEmployeePopup.MustHaveContract = False
                            phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                            ctrlFindEmployeePopup.Show()
                            isLoadPopup = 1
                        End If
                    End If
                Else
                    Reset_Find_Emp()
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub Reset_Find_Emp()
        rtEmployee_id.Text = ""
        rtEmployee_Name.Text = ""
        'rtEmployee_Code.Text = ""
        rtOrg_Name.Text = ""
        rtOrg_id.Text = ""
        rtTitle_Name.Text = ""
        rtTitle_Id.Text = ""
    End Sub
    Private Sub HideItem(ByVal pcheck As Decimal)
        If pcheck = 1 Then 'TS
            rwQuater1.Visible = True
            rwQuater2.Visible = True
            rwQuater3.Visible = False
            rwQuater4.Visible = False
            rwQuater5.Visible = False
            rwQuater6.Visible = False
            rwQuater7.Visible = False
            rwQuater8.Visible = False
            rwQuater9.Visible = False
            rwQuater10.Visible = False
        ElseIf pcheck = 2 Then 'P
            rwQuater1.Visible = True
            rwQuater2.Visible = True
            rwQuater3.Visible = True
            rwQuater4.Visible = True
            rwQuater5.Visible = True
            rwQuater6.Visible = True
            rwQuater7.Visible = True
            rwQuater8.Visible = True
            rwQuater9.Visible = False
            rwQuater10.Visible = False
        ElseIf pcheck = 3 Then 'B
            rwQuater1.Visible = True
            rwQuater2.Visible = True
            rwQuater3.Visible = False
            rwQuater4.Visible = False
            rwQuater5.Visible = False
            rwQuater6.Visible = False
            rwQuater7.Visible = False
            rwQuater8.Visible = False
            rwQuater9.Visible = True
            rwQuater10.Visible = True

        Else
            rwQuater1.Visible = False
            rwQuater2.Visible = False
            rwQuater3.Visible = False
            rwQuater4.Visible = False
            rwQuater5.Visible = False
            rwQuater6.Visible = False
            rwQuater7.Visible = False
            rwQuater8.Visible = False
            rwQuater9.Visible = False
            rwQuater10.Visible = False
        End If
    End Sub
#End Region
End Class

