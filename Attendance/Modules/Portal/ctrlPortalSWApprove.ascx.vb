Imports System.Globalization
Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports Telerik.Web.UI
Imports WebAppLog


Public Class ctrlPortalSWApprove
    Inherits Common.CommonView

    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = True
    Public Property EmployeeCode As String
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Portal/" + Me.GetType().Name.ToString()
    Dim log As New Common.CommonBusiness.UserLog

#Region "Properties"
    Public Property EmployeeID As Decimal

    Private Property fromdate As Date
        Get
            Return PageViewState(Me.ID & "_fromdate")
        End Get
        Set(ByVal value As Date)
            PageViewState(Me.ID & "_fromdate") = value
        End Set
    End Property

    Private Property todate As Date
        Get
            Return PageViewState(Me.ID & "_todate")
        End Get
        Set(ByVal value As Date)
            PageViewState(Me.ID & "_todate") = value
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
            rgSignWork.AllowCustomPaging = True

            rgSignWork.ClientSettings.EnablePostBackOnRowClick = False
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
            BuildToolbar(Me.MainToolBar, ToolbarItem.Approve, ToolbarItem.Reject, ToolbarItem.Export)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).Text = Translate("Phê duyệt (QLTT)")
            CType(Me.MainToolBar.Items(1), RadToolBarButton).Text = Translate("Không phê duyệt (QLTT)")
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
        Try
            Dim dtData As DataTable
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


            dtData = rep.GetOtherList("PROCESS_STATUS", True)
            If dtData IsNot Nothing Then
                Dim data = dtData.AsEnumerable().Where(Function(f) Not f.Field(Of Decimal?)("ID").HasValue _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.WaitingForApproval).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.ApprovedByLM).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.UnApprovedByLM).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.Saved).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.Cancel).ToString()).CopyToDataTable()
                FillRadCombobox(cboStatus, data, "NAME", "ID")

                cboStatus.SelectedValue = PortalStatus.WaitingForApproval
            End If

            rep.Dispose()
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
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
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
            Dim _param = New ParamDTO With {.ORG_ID = 1, _
                                            .PERIOD_ID = Decimal.Parse(cboPeriodId.SelectedValue),
                                            .IS_DISSOLVE = False}

            Select Case CType(e.Item, RadToolBarButton).CommandName
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
                Case CommonMessage.TOOLBARITEM_APPROVE
                    If cboPeriodId.SelectedValue = "" Then
                        ShowMessage(Translate("Chưa chọn kỳ công"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgSignWork.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim listDataCheck As New List(Of AT_PROCESS_DTO)
                    For Each dr As Telerik.Web.UI.GridDataItem In rgSignWork.SelectedItems
                        If dr.GetDataKeyValue("STATUS") = 1 Or dr.GetDataKeyValue("STATUS") = 2 Then
                            ShowMessage(Translate("Thao tác này chỉ áp dụng cho đơn đăng ký Ca ở trạng thái Chờ phê duyệt"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_APPROVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_REJECT
                    If cboPeriodId.SelectedValue = "" Then
                        ShowMessage(Translate("Chưa chọn kỳ công"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgSignWork.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each dr As Telerik.Web.UI.GridDataItem In rgSignWork.SelectedItems
                        If dr.GetDataKeyValue("STATUS") = 1 Or dr.GetDataKeyValue("STATUS") = 2 Then
                            ShowMessage(Translate("Thao tác này chỉ áp dụng cho đơn đăng ký nghỉ ở trạng thái Chờ phê duyệt."), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlCommon_Reject.Show()
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            Dim store As New AttendanceStoreProcedure
            Dim rep As New AttendanceRepository
            Dim IAttendance As IAttendanceBusiness = New AttendanceBusinessClient
            Dim ID_EMPLOYEE As Integer
            Dim ID_REGGROUP As Integer
            Dim workingDay As Date
            Dim DEPARTMENT_ID As Integer
            Dim ListEmp As String

            For Each dr As GridDataItem In rgSignWork.SelectedItems

                ID_EMPLOYEE = dr.GetDataKeyValue("EMPLOYEE_ID")
                ID_REGGROUP = dr.GetDataKeyValue("ID_REGGROUP")
                workingDay = dr.GetDataKeyValue("APP_DATE")
                DEPARTMENT_ID = dr.GetDataKeyValue("DEPARTMENT_ID")

                ListEmp &= IIf(ListEmp = vbNullString, ID_EMPLOYEE, "," & ID_EMPLOYEE)

                Dim periodid = cboPeriodId.SelectedValue

                'Dim check = rep.CHECK_PERIOD_CLOSE(periodid)
                'If check = 0 Then
                '    ShowMessage(Translate("Kì công đã đóng,Xin kiểm tra lại"), NotifyType.Warning)
                '    Exit Sub
                'End If

                Dim result = rep.PRI_PROCESS(EmployeeID, ID_EMPLOYEE, periodid, 1, "WORKSIGN", "", ID_REGGROUP)

                If result = 0 Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Refresh("UpdateView")
                Else
                    CurrentState = CommonMessage.STATE_NORMAL
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            Next

            If ListEmp IsNot Nothing Then
                store.CAL_TIME_TIMESHEET_BY_EMPS(LogHelper.GetUserLog.Username, fromdate, todate, ListEmp)
            End If

            rgSignWork.Rebind()
            UpdateControlState()
        End If
    End Sub

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

            Dim _filter As New AT_WORKSIGNDTO
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

                    ' Dùng để tổng hợp công gốc
                    fromdate = ddate.START_DATE
                    todate = ddate.END_DATE

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
            SetValueObjectByRadGrid(rgSignWork, _filter)
            _filter.EMPLOYEE_ID = EmployeeID
            If cboEmpObj.SelectedValue <> "" Then
                _filter.EMP_OBJ = cboEmpObj.SelectedValue
            End If

            If cboStatus.SelectedValue <> "" Then
                _filter.STATUS = cboStatus.SelectedValue
            End If

            Dim Sorts As String = rgSignWork.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                Dim ds = rep.GetListWSWaitingApprove(_filter, 0)
                If ds IsNot Nothing Then
                    Dim tableSignWork = ds.Tables(0)
                    rgSignWork.VirtualItemCount = ds.Tables(0).Rows.Count 'Decimal.Parse(ds.Tables(1).Rows(0)("TOTAL"))
                    rgSignWork.DataSource = tableSignWork
                Else
                    rgSignWork.VirtualItemCount = 0
                    rgSignWork.DataSource = New DataTable
                End If
            Else
                _filter.PAGE_INDEX = 1
                _filter.PAGE_SIZE = Integer.MaxValue
                Return rep.GetListWSWaitingApprove(_filter, 1).Tables(0)
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
                    Dim List_Sat_Sunday = GetListSaturdayandSunday(fromdate, todate)
                    For Each item In List_Sat_Sunday
                        DataRow("D" & item).BackColor = Drawing.Color.Yellow
                        DataRow("D" & item).ForeColor = Drawing.Color.Black
                    Next
                    Dim dtHoliday = rep.GetHoliday(New AT_HOLIDAYDTO)
                    For Each item In dtHoliday
                        If item.WORKINGDAY >= dtDatas.START_DATE And item.WORKINGDAY <= dtDatas.END_DATE Then
                            datarow("D" & item.WORKINGDAY.Value.Day).BackColor = Drawing.Color.Red
                            datarow("D" & item.WORKINGDAY.Value.Day).ForeColor = Drawing.Color.Black
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

    Protected WithEvents ctrlCommon_Reject As ctrlCommon_Reject
    Private Sub ctrlCommon_Reject_ButtonCommand(ByVal sender As Object, ByVal e As CommandSaveEventArgs) Handles ctrlCommon_Reject.ButtonCommand
        Dim strComment As String = e.Comment
        Dim ID_EMPLOYEE As Integer
        Dim ID_REGGROUP As Integer
        For Each dr As GridDataItem In rgSignWork.SelectedItems
            ID_EMPLOYEE = dr.GetDataKeyValue("EMPLOYEE_ID")
            ID_REGGROUP = dr.GetDataKeyValue("ID_REGGROUP")
            Using rep As New AttendanceRepository
                Dim result = rep.PRI_PROCESS(EmployeeID, ID_EMPLOYEE, cboPeriodId.SelectedValue, 2, "WORKSIGN", strComment, ID_REGGROUP)
                If result = 0 Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Refresh("UpdateView")
                Else
                    CurrentState = CommonMessage.STATE_NORMAL
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            End Using
        Next

        rgSignWork.Rebind()
        UpdateControlState()
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
#End Region

End Class