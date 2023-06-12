Imports System.Globalization
Imports System.Reflection
Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports Telerik.Web.UI
Imports WebAppLog


Public Class ctrlPortalSignWork
    Inherits Common.CommonView
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String

    Public Overrides Property MustAuthorize As Boolean = True
    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Portal/" + Me.GetType().Name.ToString()
    Dim log As New Common.CommonBusiness.UserLog
    Private psp As New CommonRepository
#Region "Properties"

    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Tạo DataTable lưu trữ thông tin ca làm việc của nhân viên 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))

                dt.Columns.Add("P_START_DATE", GetType(String))
                dt.Columns.Add("P_END_DATE", GetType(String))
                dt.Columns.Add("P_PERIOD_ID", GetType(String))
                dt.Columns.Add("P_EMP_OBJ", GetType(String))

                dt.Columns.Add("VN_FULLNAME", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("ORG_PATH", GetType(String))
                dt.Columns.Add("D1", GetType(String))
                dt.Columns.Add("D2", GetType(String))
                dt.Columns.Add("D3", GetType(String))
                dt.Columns.Add("D4", GetType(String))
                dt.Columns.Add("D5", GetType(String))
                dt.Columns.Add("D6", GetType(String))
                dt.Columns.Add("D7", GetType(String))
                dt.Columns.Add("D8", GetType(String))
                dt.Columns.Add("D9", GetType(String))
                dt.Columns.Add("D10", GetType(String))

                dt.Columns.Add("D11", GetType(String))
                dt.Columns.Add("D12", GetType(String))
                dt.Columns.Add("D13", GetType(String))
                dt.Columns.Add("D14", GetType(String))
                dt.Columns.Add("D15", GetType(String))
                dt.Columns.Add("D16", GetType(String))
                dt.Columns.Add("D17", GetType(String))
                dt.Columns.Add("D18", GetType(String))
                dt.Columns.Add("D19", GetType(String))
                dt.Columns.Add("D20", GetType(String))

                dt.Columns.Add("D21", GetType(String))
                dt.Columns.Add("D22", GetType(String))
                dt.Columns.Add("D23", GetType(String))
                dt.Columns.Add("D24", GetType(String))
                dt.Columns.Add("D25", GetType(String))
                dt.Columns.Add("D26", GetType(String))
                dt.Columns.Add("D27", GetType(String))
                dt.Columns.Add("D28", GetType(String))
                dt.Columns.Add("D29", GetType(String))
                dt.Columns.Add("D30", GetType(String))
                dt.Columns.Add("D31", GetType(String))
                dt.Columns.Add("WS", GetType(String))
                dt.Columns.Add("WS_I", GetType(String))
                dt.Columns.Add("COUNT_OFF", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    ''' <summary>
    '''  Ngày bắt đầu kỳ công
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property startDatePeroid As Date
        Get
            Return ViewState(Me.ID & "_startDatePeroid")
        End Get
        Set(ByVal value As Date)
            ViewState(Me.ID & "_startDatePeroid") = value
        End Set
    End Property

    ''' <summary>
    '''  Ngày kết thúc kỳ công
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property endDatePeriod As Date
        Get
            Return ViewState(Me.ID & "_endDatePeriod")
        End Get
        Set(ByVal value As Date)
            ViewState(Me.ID & "_endDatePeriod") = value
        End Set
    End Property

    Property ListKeyDay As New Dictionary(Of String, Date)

#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức load dữ liệu cho trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            Dim store As New CommonProcedureNew
            If store.CHECK_EXIST_SE_CONFIG("PORTALSIGNWORK") = -1 Then
                cboStatus1.Visible = False
                lbStatus1.Visible = False
                chkSign.Visible = False
                MainToolBar.Items(3).Visible = False
                MainToolBar.Items(4).Visible = False
            Else
                cboStatus1.Visible = True
                lbStatus1.Visible = True
                chkSign.Visible = True
                MainToolBar.Items(3).Visible = True
                MainToolBar.Items(4).Visible = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo các thiết lập, control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            log = LogHelper.GetUserLog
            Dim startTime As DateTime = DateTime.UtcNow
            SetGridFilter(rgSignWork)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgSignWork.AllowCustomPaging = True

            rgSignWork.ClientSettings.EnablePostBackOnRowClick = False
            ctrlUpload1.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload1.MaxFileInput = 1
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức thiết lập toolbar, popup message
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Export,
                                       ToolbarItem.ExportTemplate,
                                       ToolbarItem.Import,
                                       ToolbarItem.Submit,
                                       ToolbarItem.Delete)
            CType(MainToolBar.Items(1), RadToolBarButton).Text = "Xuất file mẫu"
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức bind dữ liệu lên form thêm mới, sửa
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dtData1 As New DataTable
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim lsData As List(Of AT_PERIODDTO)
            Dim rep As New AttendanceRepository
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
            Dim period As New AT_PERIODDTO
            period.ORG_ID = 1
            period.YEAR = Date.Now.Year
            lsData = rep.LOAD_PERIODBylinq(period)
            FillRadCombobox(cboPeriodId, lsData, "PERIOD_NAME", "PERIOD_ID", True)
            If lsData.Count > 0 Then
                'Dim periodid = (From d In lsData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                Dim periodid = (From d In lsData Where d.MONTH = Date.Now.Month And d.YEAR = Date.Now.Year Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriodId.SelectedValue = periodid.PERIOD_ID.ToString()
                Else
                    cboPeriodId.SelectedIndex = 0
                End If
            End If
            lsData = rep.Load_Emp_obj()
            FillRadCombobox(cboEmpObj, lsData, "PERIOD_NAME", "ID", True)
            Dim emp = rep.GetEmpId(EmployeeID)
            cboEmpObj.SelectedValue = emp.OBJECT_EMPLOYEE_ID
            Using rep1 As New AttendanceRepository
                dtData1 = rep1.GetOtherList("PROCESS_STATUS", True)
                If dtData1 IsNot Nothing Then
                    Dim data = dtData1.AsEnumerable().Where(Function(f) Not f.Field(Of Decimal?)("ID").HasValue _
                                                               Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.WaitingForApproval).ToString() _
                                                               Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.ApprovedByLM).ToString() _
                                                               Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.UnApprovedByLM).ToString() _
                                                               Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.Saved).ToString() _
                                                               Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.Cancel).ToString()).CopyToDataTable()
                    FillRadCombobox(cboStatus1, data, "NAME", "ID")
                End If
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Làm mới các thiết lập cho các control trên trang
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceBusinessClient
        Dim rep2 As New Profile.ProfileBusiness.ProfileBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                chkDefault.Checked = True
                CType(MainToolBar.Items(4), RadToolBarButton).Visible = False
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgSignWork.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, , rgSignWork.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgSignWork.CurrentPageIndex = 0
                        rgSignWork.MasterTableView.SortExpressions.Clear()
                        rgSignWork.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, )
                    Case "Cancel"
                        rgSignWork.MasterTableView.ClearSelectedItems()
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
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái cho các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstIDReg As New List(Of Decimal)
                    For Each item As GridDataItem In rgSignWork.SelectedItems
                        lstIDReg.Add(item.GetDataKeyValue("ID_REGGROUP"))
                    Next
                    If rep.DeletePortalWS(lstIDReg) Then
                        Refresh("UpdateView")
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                    CurrentState = CommonMessage.STATE_NORMAL
                    rgSignWork.Rebind()
            End Select
            'UpdateToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Command khi click main toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objSign As New AT_WORKSIGNDTO
        Dim rep As New AttendanceRepository

        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim store As New AttendanceStoreProcedure
            Dim startTime As DateTime = DateTime.UtcNow
            Dim _param = New ParamDTO With {.ORG_ID = 1,
                                            .PERIOD_ID = Decimal.Parse(cboPeriodId.SelectedValue),
                                            .IS_DISSOLVE = False}

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SUBMIT
                    If chkDefault.Checked Then
                        ShowMessage(Translate("Dữ liệu mặc định không thể thao tác"), NotifyType.Error)
                        Exit Sub
                    End If
                    'If rep.IS_PERIODSTATUS(_param) = False Then
                    '    ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                    '    Exit Sub
                    'End If
                    If rgSignWork.SelectedItems.Count = 0 Then 'KIỂM TRA SỐ LƯỢNG ĐƠN GỬI
                        ShowMessage(Translate("Cần tích chọn nhân viên trước khi gửi duyệt"), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgSignWork.SelectedItems
                        If item.GetDataKeyValue("STATUS") <> PortalStatus.Saved Then 'KIỂM TRA TRẠNG THÁI ĐƠN
                            ShowMessage(Translate("Chỉ gửi đơn ở trạng thái Chưa gửi duyệt. Vui lòng thử lại !"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate("Bạn có muốn gửi phê duyệt?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_SUBMIT
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    'If rep.IS_PERIODSTATUS(_param) = False Then
                    '    ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                    '    Exit Sub
                    'End If
                    If cboEmpObj.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn chưa chọn đối tượng nhân viên"), NotifyType.Error)
                        Exit Sub
                    End If
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('WorkShiftImport&emp_obj=" & cboEmpObj.SelectedValue & "&PERIOD_ID=" & cboPeriodId.SelectedValue & "&EMPID=" & EmployeeID & "')", True)
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('WorkShiftImport&emp_obj=" & cboEmpObj.SelectedValue & "&PERIOD_ID=" & cboPeriodId.SelectedValue & "&orgid=1')", True)
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As New DataTable
                        dtDatas = CreateDataFilter(True)
                        If (dtDatas IsNot Nothing) Then
                            If (dtDatas.Rows IsNot Nothing) Then
                                If dtDatas.Rows.Count > 0 Then
                                    rgSignWork.ExportExcel(Server, Response, dtDatas, "SignWork")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                                    Exit Sub
                                End If
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                                Exit Sub
                            End If
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        End If

                    End Using
                Case CommonMessage.TOOLBARITEM_IMPORT
                    If cboPeriodId.SelectedValue = "" Then
                        ShowMessage(Translate("Chưa chọn kỳ công"), NotifyType.Error)
                        Exit Sub
                    End If
                    'If rep.IS_PERIODSTATUS(_param) = False Then
                    '    ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                    '    Exit Sub
                    'End If
                    'If rep.CheckNotSendPortalWS(EmployeeID, cboPeriodId.SelectedValue) = True Then
                    '    ShowMessage(Translate("Chỉ được import khi chưa gửi duyệt"), NotifyType.Error)
                    '    Exit Sub
                    'End If
                    ctrlUpload1.Show()
                Case TOOLBARITEM_DELETE
                    If chkDefault.Checked Then
                        ShowMessage(Translate("Dữ liệu mặc định không thể xóa"), NotifyType.Error)
                        Exit Sub
                    End If
                    If cboPeriodId.SelectedValue = "" Then
                        ShowMessage(Translate("Chưa chọn kỳ công"), NotifyType.Error)
                        Exit Sub
                    End If
                    If rgSignWork.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgSignWork.SelectedItems
                        If item.GetDataKeyValue("STATUS") <> 3 AndAlso item.GetDataKeyValue("STATUS") <> PortalStatus.UnApprovedByLM Then
                            ShowMessage(Translate("Chỉ được xóa khi chưa gửi duyệt hoặc không phê duyệt"), NotifyType.Error)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_SUBMIT And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            Dim period_id As Integer
            Dim id_group As Integer
            Dim id_emp As Decimal
            If rgSignWork.SelectedItems.Count = 1 Then

                Dim item As GridDataItem = rgSignWork.SelectedItems(0)

                period_id = cboPeriodId.SelectedValue
                id_group = item.GetDataKeyValue("ID_REGGROUP")
                id_emp = item.GetDataKeyValue("EMPLOYEE_ID")
                'Using rep As New AttendanceRepository
                '    Dim check = rep.CHECK_PERIOD_CLOSE(period_id)

                '    If check = 0 Then
                '        ShowMessage(Translate("Kì công đã đóng,Xin kiểm tra lại"), NotifyType.Warning)
                '        Exit Sub
                '    End If
                'End Using
                Dim outNumber As Decimal

                Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
                Dim r As New Random
                Dim sb As New StringBuilder
                For i As Integer = 1 To 32
                    Dim idx As Integer = r.Next(0, 35)
                    sb.Append(s.Substring(idx, 1))
                Next
                Dim token = sb.ToString() + EmployeeID.ToString

                Try
                    Dim IAttendance As IAttendanceBusiness = New AttendanceBusinessClient()
                    outNumber = IAttendance.PRI_PROCESS_APP(id_emp, period_id, "WORKSIGN", 0, 0, 0, id_group, token)
                Catch ex As Exception
                    ShowMessage(ex.ToString, NotifyType.Error)
                End Try

                If outNumber = 0 Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                ElseIf outNumber = 1 Then
                    ShowMessage(Translate("Quy trình phê duyệt chưa được thiết lập"), NotifyType.Success)
                ElseIf outNumber = 2 Then
                    ShowMessage(Translate("Thao tác xảy ra lỗi,bạn kiểm tra lại quy trình"), NotifyType.Error)
                ElseIf outNumber = 3 Then
                    ShowMessage(Translate("Nhân viên chưa có thiết lập nhóm chức danh"), NotifyType.Error)
                End If
                rgSignWork.Rebind()
            ElseIf rgSignWork.SelectedItems.Count > 1 Then
                Dim countSuccess As Integer
                For Each item As GridDataItem In rgSignWork.SelectedItems
                    period_id = cboPeriodId.SelectedValue
                    id_group = item.GetDataKeyValue("ID_REGGROUP")
                    id_emp = item.GetDataKeyValue("EMPLOYEE_ID")
                    'Using rep As New AttendanceRepository
                    '    Dim check = rep.CHECK_PERIOD_CLOSE(period_id)

                    '    If check = 0 Then
                    '        ShowMessage(Translate("Kì công đã đóng,Xin kiểm tra lại"), NotifyType.Warning)
                    '        Exit Sub
                    '    End If
                    'End Using
                    Dim outNumber As Decimal

                    Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
                    Dim r As New Random
                    Dim sb As New StringBuilder
                    For i As Integer = 1 To 32
                        Dim idx As Integer = r.Next(0, 35)
                        sb.Append(s.Substring(idx, 1))
                    Next
                    Dim token = sb.ToString() + EmployeeID.ToString

                    Try
                        Dim IAttendance As IAttendanceBusiness = New AttendanceBusinessClient()
                        outNumber = IAttendance.PRI_PROCESS_APP(id_emp, period_id, "WORKSIGN", 0, 0, 0, id_group, token)
                    Catch ex As Exception
                        'ShowMessage(ex.ToString, NotifyType.Error)
                        countSuccess -= 1
                        Continue For
                    End Try

                    If outNumber = 0 Then
                        countSuccess += 1
                    ElseIf outNumber = 1 Then
                        countSuccess -= 1
                    ElseIf outNumber = 2 Then
                        countSuccess -= 1
                    ElseIf outNumber = 3 Then
                        countSuccess -= 1
                    End If
                Next
                If countSuccess > 0 Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    CurrentState = CommonMessage.STATE_NORMAL
                    rgSignWork.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            End If
        ElseIf e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Lưu dữ liệu vào db
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function saveGrid() As Boolean
        Dim rep As New AttendanceRepository
        Dim lstData As New List(Of AT_WORKSIGNDTO)
        Dim gId As New Decimal
        gId = 0
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dtData.TableName = "Data"
            Dim day As New AT_PERIODDTO '= rep.Load_date(CDec(Val(cboPeriodId.SelectedValue)), CDec(Val(cboEmpObj.SelectedValue)))

            Dim start_date As Date
            Dim end_date As Date

            DateTime.TryParseExact(dtData.Rows(0)("P_START_DATE").ToString, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, start_date)
            day.START_DATE = start_date
            DateTime.TryParseExact(dtData.Rows(0)("P_END_DATE").ToString, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, end_date)
            day.END_DATE = end_date
            rep.ImportPortalWS(dtData, CDec(Val(dtData.Rows(0)("P_PERIOD_ID"))), CDec(Val(dtData.Rows(0)("P_EMP_OBJ"))), day.START_DATE, day.END_DATE)
            Return True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return False
        End Try
    End Function

    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource cho rad grid rgSignWork
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgSignWork.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter(False)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện click khi click btnSearch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearchEmp.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkDefault.Checked Then
                CType(MainToolBar.Items(4), RadToolBarButton).Visible = False
            Else
                CType(MainToolBar.Items(4), RadToolBarButton).Visible = True
            End If
            rgSignWork.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Function dowInMonth(whDayOfWeek As DayOfWeek,
                               Optional theDate As DateTime = Nothing) As List(Of DateTime)
        'returns all days of week for a given month  
        If theDate = Nothing Then theDate = DateTime.Now
        Dim d As DateTime = New DateTime(theDate.Year, theDate.Month, 1) 'first day of month  
        'calculate the first day of week  
        d = d.AddDays(whDayOfWeek - d.DayOfWeek)
        If d.Month <> theDate.Month Then
            d = d.AddDays(7)
        End If
        'Debug.WriteLine("{0} {1} {2} {3}", theDate.Month, whDayOfWeek.ToString, d.Month, d.DayOfWeek.ToString)  
        'return all of the days of week  
        dowInMonth = New List(Of Date)
        Do While d.Month = theDate.Month
            dowInMonth.Add(d)
            d = d.AddDays(7)
        Loop
    End Function

    Public Function GetListSaturdayandSunday(ByVal fromDate As Date, ByVal toDate As Date) As List(Of String)
        Dim ds = New List(Of String)
        Dim startdate As Date = fromDate
        Do While startdate <= toDate
            Dim Day = startdate.DayOfWeek
            If Day = DayOfWeek.Saturday Or Day = DayOfWeek.Sunday Then
                For Each p In ListKeyDay
                    If p.Value = startdate Then
                        ds.Add(p.Key)
                        Exit For
                    End If
                Next
            End If
            startdate = startdate.AddDays(1)
        Loop

        Return ds

    End Function

    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm tạo dữ liệu filter cho grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim startdate As Date
            Dim enddate As Date
            Dim rep = New AttendanceRepository
            Dim store As New CommonProcedureNew
            Dim _filter As New AT_WORKSIGNDTO
            If store.CHECK_EXIST_SE_CONFIG("IS_LOAD_DIRECTMNG") = -1 Then
                _filter.IS_LOAD_DIRECTMNG = True
            Else
                _filter.IS_LOAD_DIRECTMNG = False
            End If
            If Not String.IsNullOrEmpty(cboPeriodId.SelectedValue) And cboEmpObj.SelectedValue <> "" Then
                Dim obj As New AT_PERIODDTO
                obj.PERIOD_ID = Decimal.Parse(cboPeriodId.SelectedValue)
                _filter.PERIOD_ID = obj.PERIOD_ID
                Dim ddate = rep.Load_date(CDec(Val(cboPeriodId.SelectedValue)), CDec(Val(cboEmpObj.SelectedValue))) 'rep.LOAD_PERIODByID(obj)

                If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                    startdate = ddate.START_DATE
                    enddate = ddate.END_DATE
                    _filter.END_DATE = enddate
                    _filter.START_DATE = startdate

                    'Lưu tạm để tô màu
                    startDatePeroid = ddate.START_DATE
                    endDatePeriod = ddate.END_DATE

                End If
            End If

            ListKeyDay.Clear()

            If cboEmpObj.SelectedValue <> "" And _filter.START_DATE IsNot Nothing And _filter.END_DATE IsNot Nothing Then
                For i = 1 To 31
                    If startdate <= enddate Then
                        rgSignWork.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM/yyyy")
                        rgSignWork.MasterTableView.GetColumn("D" & i).Visible = True

                        ListKeyDay.Add(i, startdate)
                        startdate = startdate.AddDays(1)
                    Else
                        rgSignWork.MasterTableView.GetColumn("D" & i).Visible = False
                    End If
                Next
            Else
                For i = 1 To 31
                    rgSignWork.MasterTableView.GetColumn("D" & i).Visible = False
                Next
            End If
            Dim _type As String
            If Not chkSign.Checked Then
                _type = "DEFAULT"
                rgSignWork.MasterTableView.GetColumn("cbStatus").Visible = False
                rgSignWork.MasterTableView.GetColumn("STATUS_NAME").Visible = False
                rgSignWork.MasterTableView.GetColumn("APPROVER_NAME").Visible = False
                rgSignWork.MasterTableView.GetColumn("REASON").Visible = False
            Else
                _type = "SIGN"
                rgSignWork.MasterTableView.GetColumn("cbStatus").Visible = True
                rgSignWork.MasterTableView.GetColumn("STATUS_NAME").Visible = True
                rgSignWork.MasterTableView.GetColumn("APPROVER_NAME").Visible = True
                rgSignWork.MasterTableView.GetColumn("REASON").Visible = True
            End If

            SetValueObjectByRadGrid(rgSignWork, _filter)
            _filter.ORG_ID = 1
            _filter.IS_DISSOLVE = False
            _filter.EMPLOYEE_ID = EmployeeID
            If cboEmpObj.SelectedValue <> "" Then
                _filter.EMP_OBJ = cboEmpObj.SelectedValue
            End If
            If IsNumeric(cboStatus1.SelectedValue) Then
                _filter.STATUS = cboStatus1.SelectedValue
            End If
            Dim Sorts As String = rgSignWork.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                Dim ds = rep.GET_PORTAL_WORKSIGN(_filter, _type, 0)
                If ds IsNot Nothing Then
                    Dim tableSignWork = ds.Tables(0)
                    'Dim dataqr
                    'If _filter.IS_LOAD_DIRECTMNG = True AndAlso tableSignWork.Rows.Count > 0 Then
                    '    Dim check_dataqr = tableSignWork.AsEnumerable().Where(Function(f) f("ID") = EmployeeID _
                    '                                                    Or f("DIRECT_MANAGER") = EmployeeID).Any
                    '    If check_dataqr Then
                    '        dataqr = tableSignWork.AsEnumerable().Where(Function(f) f("ID") = EmployeeID _
                    '                                                    Or f("DIRECT_MANAGER") = EmployeeID).CopyToDataTable()
                    '    Else
                    '        dataqr = New DataTable
                    '    End If
                    'Else
                    '    dataqr = tableSignWork
                    'End If
                    'dataqr = tableSignWork
                    rgSignWork.VirtualItemCount = ds.Tables(0).Rows.Count ''Decimal.Parse(ds.Tables(1).Rows(0)("TOTAL"))
                    rgSignWork.DataSource = tableSignWork
                Else
                    rgSignWork.VirtualItemCount = 0
                    rgSignWork.DataSource = New DataTable
                End If
            Else
                _filter.PAGE_INDEX = 1
                _filter.PAGE_SIZE = Integer.MaxValue
                Return rep.GET_PORTAL_WORKSIGN(_filter, _type, 1).Tables(0)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedNodeChanged cho ctrlYear
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim dtData As List(Of AT_PERIODDTO)
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            period.ORG_ID = 1
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            cboPeriodId.ClearSelection()

            FillRadCombobox(cboPeriodId, dtData, "PERIOD_NAME", "PERIOD_ID", True)
            If dtData.Count > 0 Then
                Dim periodid = (From d In dtData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriodId.SelectedValue = periodid.PERIOD_ID.ToString()
                Else
                    cboPeriodId.SelectedIndex = 0
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedIndexChanged cho combobox cboPeriodId
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPeriodId_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriodId.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgSignWork.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện OkClicked của ctrlUpload1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked

        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim dtDataHeader As DataTable
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                If workbook.Worksheets.GetSheetByCodeName("IMPORTSHIFT") Is Nothing Then
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End If
                worksheet = workbook.Worksheets(0)
                dtDataHeader = worksheet.Cells.ExportDataTableAsString(0, 0, 5, worksheet.Cells.MaxColumn + 1, True)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(4, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            dtDataHeader.Rows.RemoveAt(0)
            dtDataHeader.Rows.RemoveAt(0)
            For col As Integer = 0 To dtDataHeader.Columns.Count - 1
                Dim colName = dtDataHeader.Rows(1)(col)
                If colName IsNot DBNull.Value Then
                    dtDataHeader.Columns(col).ColumnName = colName
                End If
            Next
            dtDataHeader.Rows.RemoveAt(1)
            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If isRow Then
                        dtData.ImportRow(row)
                    End If
                Next
            Next
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate("File không có dữ liệu, kiểm tra lại file import"), NotifyType.Error)
                Exit Sub
            End If



            If loadToGrid(dtDataHeader) Then
                If saveGrid() Then
                    Refresh("InsertView")
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện ItemDataBound của rad grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgSignWork_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgSignWork.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New AttendanceRepository
            Dim dtDatas As New AT_PERIODDTO
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
            End If

            If e.Item.Visible Then
                If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                    Dim lsData As List(Of AT_PERIODDTO)
                    Dim period As New AT_PERIODDTO
                    period.ORG_ID = 1
                    period.YEAR = Date.Now.Year
                    lsData = rep.LOAD_PERIODBylinq(period)
                    Dim obj = (From d In lsData Where d.PERIOD_ID = Decimal.Parse(cboPeriodId.SelectedValue) Select d).FirstOrDefault
                    Dim str = "01/" & obj.PERIOD_NAME & "/" & obj.YEAR
                    Dim date_start As Date
                    DateTime.TryParseExact(str, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, date_start)
                    dtDatas.START_DATE = date_start
                    dtDatas.END_DATE = date_start.AddMonths(1).AddDays(-1)
                    'Dim list_SaturdayOfMonth = dowInMonth(6, date_start)
                    'Dim list_SundayOfMonth = dowInMonth(0, date_start)
                    Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                    'For Each item In list_SaturdayOfMonth
                    '    datarow("D" & item.Day).BackColor = Drawing.Color.Yellow
                    '    datarow("D" & item.Day).ForeColor = Drawing.Color.Black
                    'Next
                    'For Each item In list_SundayOfMonth
                    '    datarow("D" & item.Day).BackColor = Drawing.Color.Yellow
                    '    datarow("D" & item.Day).ForeColor = Drawing.Color.Black
                    'Next
                    Dim List_Sat_Sunday = GetListSaturdayandSunday(startDatePeroid, endDatePeriod)
                    For Each item In List_Sat_Sunday
                        datarow("D" & item).BackColor = Drawing.Color.Yellow
                        datarow("D" & item).ForeColor = Drawing.Color.Black
                    Next

                    Dim dtHoliday = rep.GetHoliday(New AT_HOLIDAYDTO)
                    For Each item In dtHoliday
                        If item.WORKINGDAY >= dtDatas.START_DATE And item.WORKINGDAY <= dtDatas.END_DATE Then
                            Dim day As String
                            For Each p In ListKeyDay
                                If p.Value = item.WORKINGDAY Then
                                    day = p.Key
                                    Exit For
                                End If
                            Next
                            datarow("D" & day).BackColor = Drawing.Color.Red
                            datarow("D" & day).ForeColor = Drawing.Color.Black
                        End If
                    Next
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái toolbar
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

    Public Shared Function ConvertToDataTable(Of T)(ByVal list As IList(Of T)) As DataTable
        Dim table As New DataTable()
        Dim fields() As FieldInfo = GetType(T).GetFields()
        For Each field As FieldInfo In fields
            table.Columns.Add(field.Name, field.FieldType)
        Next
        For Each item As T In list
            Dim row As DataRow = table.NewRow()
            For Each field As FieldInfo In fields
                row(field.Name) = field.GetValue(item)
            Next
            table.Rows.Add(row)
        Next
        Return table
    End Function


    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm load grid với header định trước
    ''' </summary>
    ''' <param name="dtDataHeader"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function loadToGrid(ByVal dtDataHeader As DataTable) As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim dtDatas As New AT_PERIODDTO
        Dim rep As New AttendanceRepository
        Dim proc As New AttendanceStoreProcedure
        Dim period As New AT_PERIODDTO
        Dim rowError As DataRow
        Dim isError As Boolean = False
        Dim sError As String = String.Empty
        Dim irow = 6
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                Return False
            End If
            dtError = dtData.Clone
            dtError.Columns.Add("STT")
            dtError.Columns.Add("OTHER")
            period.PERIOD_ID = Decimal.Parse(cboPeriodId.SelectedValue)
            'dtDatas = rep.LOAD_PERIODByID(period)
            Dim start_date As Date '= dtData.Rows(0)("P_START_DATE").ToString
            Dim end_date As Date '= dtData.Rows(0)("P_END_DATE").ToString

            DateTime.TryParseExact(dtData.Rows(0)("P_START_DATE").ToString, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, start_date)

            DateTime.TryParseExact(dtData.Rows(0)("P_END_DATE").ToString, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, end_date)

            dtDatas.START_DATE = start_date
            dtDatas.END_DATE = end_date


            'dtDatas = rep.Load_date(CDec(Val(cboPeriodId.SelectedValue)), CDec(Val(cboEmpObj.SelectedValue)))


            Dim user = LogHelper.CurrentUser

            Dim dtShift = rep.GetAT_SHIFT(New AT_SHIFTDTO With {.SHIFT_DAY = user.ID})
            Dim dtHoliday = rep.GetHoliday(New AT_HOLIDAYDTO)

            Dim config As Dictionary(Of String, String)
            config = psp.GetConfig(0)
            Dim workingLessEqualWS = config("IS_WORKING_LESSEQUAL_WORKINGSTANDARD")
            Dim boundShift = config("IS_BOUND_SHIFT")
            Dim boundShift_Operator = config("AT_BOUND_SHIFT_OPERATOR")
            Dim boundShift_Value = config("AT_BOUND_SHIFT_VALUE")
            For Each row As DataRow In dtData.Rows
                Dim isRow = ImportValidate.TrimRow(row)
                If Not isRow Then
                    irow += 1
                    Continue For
                End If
                isError = False
                ImportValidate.TrimRow(row)
                rowError = dtError.NewRow
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)

                sError = "Mã nhân viên không phải hệ thống chiết xuất"
                ImportValidate.IsValidNumber("EMPLOYEE_ID", row, rowError, isError, sError)
                If rowError("EMPLOYEE_ID").ToString <> "" Then
                    rowError("EMPLOYEE_CODE") = sError
                End If

                If rep.CheckWaittingApprovePTWS(CDec(row("EMPLOYEE_ID").ToString), cboPeriodId.SelectedValue) = True Then
                    sError = "Tồn tại dữ liệu đang chờ phê duyệt"
                    ImportValidate.IsValidDate("EMPLOYEE_ID", row, rowError, isError, sError)
                    rowError("OTHER") = sError
                End If

                If workingLessEqualWS = "-1" AndAlso CDec(row("WS")) < CDec(row("WS_I")) Then
                    rowError("OTHER") = rowError("OTHER") + " Vượt công chuẩn trong tháng"
                    isError = True
                End If

                If boundShift = -1 Then
                    Select Case boundShift_Operator
                        Case ">"
                            If row("COUNT_OFF") <= boundShift_Value Then
                                rowError("OTHER") = rowError("OTHER") + " Số lượng ca OFF trong tháng phải > " & boundShift_Value.ToString
                                isError = True
                            End If
                        Case ">="
                            If row("COUNT_OFF") < boundShift_Value Then
                                rowError("OTHER") = rowError("OTHER") + " Số lượng ca OFF trong tháng phải >= " & boundShift_Value.ToString
                                isError = True
                            End If
                        Case "<"
                            If row("COUNT_OFF") >= boundShift_Value Then
                                rowError("OTHER") = rowError("OTHER") + " Số lượng ca OFF trong tháng phải < " & boundShift_Value.ToString
                                isError = True
                            End If
                        Case "<="
                            If row("COUNT_OFF") > boundShift_Value Then
                                rowError("OTHER") = rowError("OTHER") + " Số lượng ca OFF trong tháng phải <= " & boundShift_Value.ToString
                                isError = True
                            End If
                        Case "="
                            If row("COUNT_OFF") <> boundShift_Value Then
                                rowError("OTHER") = rowError("OTHER") + " Số lượng ca OFF trong tháng phải = " & boundShift_Value.ToString
                                isError = True
                            End If
                    End Select
                End If

                Dim empData As New DataTable
                empData = proc.GET_EMP_BY_ID(row.ItemArray(1))
                Dim query = (From p In empData).FirstOrDefault

                Dim index As Integer = 0
                For index = 1 To (dtDatas.END_DATE - dtDatas.START_DATE).Value.TotalDays + 1
                    If row("D" & index).ToString <> "" Then
                        Dim r = row("D" & index).ToString
                        Dim workingDate As Date = dtDatas.START_DATE.Value.AddDays(index - 1)
                        Dim exists = (From p In dtShift Where p.NAME_VN.ToUpper.Equals(r.ToUpper)).FirstOrDefault
                        Dim existsHoliday = (From p In dtHoliday Where p.WORKINGDAY = workingDate).FirstOrDefault
                        Dim chkDate = DateAdd("d", index - 1, dtDatas.START_DATE)

                        If (chkDate < query.ItemArray(1)) Then
                            rowError("D" & index) = row("D" & index).ToString & " ngày đăng kí không được lớn hơn ngày vào làm"
                            isError = True
                        End If

                        If (query.ItemArray(2).ToString <> "") Then
                            If query.ItemArray(2) < chkDate Then
                                rowError("D" & index) = row("D" & index).ToString & " ngày đăng kí không được nhỏ hơn ngày nghỉ việc"
                                isError = True
                            End If
                        End If

                        If exists Is Nothing Then
                            rowError("D" & index) = row("D" & index).ToString & " không tồn tại"
                            isError = True
                        Else
                            row("D" & index) = ""
                            If IsNumeric(exists.ID) Then
                                row("D" & index) = exists.ID
                                'Else
                                '    If workingDate.DayOfWeek = DayOfWeek.Sunday And exists.SUNDAY.HasValue Then
                                '        row("D" & index) = 81 ' OFF
                                '    Else
                                '        row("D" & index) = exists.ID
                                '    End If
                            End If
                            'If Not existsHoliday Is Nothing Then
                            '    row("D" & index) = 81
                            'End If
                        End If

                    Else

                    End If
                Next
                If index = 28 Then
                    row("D29") = ""
                    row("D30") = ""
                    row("D31") = ""
                End If
                If index = 29 Then
                    row("D30") = ""
                    row("D31") = ""
                End If
                If index = 30 Then
                    row("D31") = ""
                End If

                If isError Then
                    rowError("STT") = irow
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("ORG_PATH") = row("ORG_PATH").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    dtError.Rows.Add(rowError)
                End If
                irow += 1
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                dtDataHeader.TableName = "DATA_HEADER"
                Dim dsData As New DataSet
                dsData.Tables.Add(dtError)
                dsData.Tables.Add(dtDataHeader)
                Session("EXPORTREPORT") = dsData
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportShift_error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            Return True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function

#End Region

End Class