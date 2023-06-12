Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlDeclare_TimeRice
    Inherits Common.CommonView
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim dsDataComper As New DataTable

    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()

#Region "Properties"
    Private Property REGISTER_OT As List(Of AT_TIME_RICEDTO)
        Get
            Return ViewState(Me.ID & "_REGISTER_OT")
        End Get
        Set(ByVal value As List(Of AT_TIME_RICEDTO))
            ViewState(Me.ID & "_REGISTER_OT") = value
        End Set
    End Property
    Private Property PERIOD As List(Of AT_PERIODDTO)
        Get
            Return ViewState(Me.ID & "_PERIOD")
        End Get
        Set(ByVal value As List(Of AT_PERIODDTO))
            ViewState(Me.ID & "_PERIOD") = value
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
    Property dtDataImportEmployee As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportEmployee")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportEmployee") = value
        End Set

    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 16/08/2017 14:00
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
            ctrlOrganization.AutoPostBack = True
            ctrlOrganization.LoadDataAfterLoaded = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckBoxes = TreeNodeTypes.None
            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SetGridFilter(rgDeclare_TimeRice)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgDeclare_TimeRice.AllowCustomPaging = True
            rgDeclare_TimeRice.ClientSettings.EnablePostBackOnRowClick = False
            ctrlUpload.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload.MaxFileInput = 1
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                        ToolbarItem.Export,
                                        ToolbarItem.Delete)
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
                                                                     ToolbarIcons.Export,
                                                                     ToolbarAuthorize.Export,
                                                                     Translate("Xuất file mẫu")))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
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
            Me.PERIOD = lsData
            FillRadCombobox(cboPeriod, lsData, "PERIOD_NAME", "PERIOD_ID", True)
            If lsData.Count > 0 Then
                Dim periodid = (From d In lsData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                    rdtungay.SelectedDate = periodid.START_DATE
                    rdDenngay.SelectedDate = periodid.END_DATE
                Else
                    cboPeriod.SelectedIndex = lsData.Count - 1
                    Dim periodid1 = (From d In lsData Where d.PERIOD_ID.ToString.Contains(cboPeriod.SelectedValue.ToString) Select d).FirstOrDefault

                    If periodid1 IsNot Nothing Then
                        rdtungay.SelectedDate = periodid1.START_DATE
                        rdDenngay.SelectedDate = periodid1.END_DATE
                    End If
                    'If (Common.Common.GetShortDatePattern().Trim.ToUpper.Contains("dd/MM/yyyy")) Then
                    '    rdtungay.SelectedDate = CType("01/01/" & cboYear.SelectedValue.Trim.ToString, Date)
                    '    rdDenngay.SelectedDate = CType("31/01/" & cboYear.SelectedValue.Trim.ToString, Date)
                    'Else
                    '    rdtungay.SelectedDate = CType("01/01/" & cboYear.SelectedValue.Trim.ToString, Date)
                    '    rdDenngay.SelectedDate = CType("01/31/" & cboYear.SelectedValue.Trim.ToString, Date)
                    'End If
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New AttendanceRepository

            Select Case isLoadPopup
                Case 2
                    phPopup.Controls.Clear()
                    ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopup.Controls.Add(ctrlOrgPopup)
                    Exit Sub
            End Select

            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    ' kiem tra ky cong da dong chua?
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDeclare_TimeRice.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDeclare_TimeRice.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteDelareRice(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_DEACTIVE
                    ' kiem tra ky cong da dong chua?
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDeclare_TimeRice.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDeclare_TimeRice.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveDelareRice(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgDeclare_TimeRice.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    ' kiem tra ky cong da dong chua?
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDeclare_TimeRice.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDeclare_TimeRice.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveDelareRice(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgDeclare_TimeRice.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
            End Select
            UpdateToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien SelectedNodeChanged cho ctrlYear
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim dtData As List(Of AT_PERIODDTO)
            Dim rep As New AttendanceRepository
            Dim period As New AT_PERIODDTO
            period.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            cboPeriod.ClearSelection()
            Me.PERIOD = dtData
            FillRadCombobox(cboPeriod, dtData, "PERIOD_NAME", "PERIOD_ID", True)
            If dtData.Count > 0 Then
                Dim periodid = (From d In dtData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                    rdtungay.SelectedDate = periodid.START_DATE
                    rdDenngay.SelectedDate = periodid.END_DATE
                    rgDeclare_TimeRice.Rebind()
                Else
                    cboPeriod.SelectedIndex = dtData.Count - 1
                    Dim per = (From c In dtData Where c.PERIOD_ID = cboPeriod.SelectedValue).FirstOrDefault
                    rdtungay.SelectedDate = per.START_DATE
                    rdDenngay.SelectedDate = per.END_DATE
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien SelectedIndexChanged cho cboPeriod
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPeriod_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New AttendanceRepository
            Dim period As New AT_PERIODDTO
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            period.ORG_ID = 46
            If cboPeriod.SelectedValue <> "" Then
                Dim Lstperiod = rep.LOAD_PERIODBylinq(period)

                Dim p = (From o In Lstperiod Where o.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)).FirstOrDefault
                rdtungay.SelectedDate = p.START_DATE
                rdDenngay.SelectedDate = p.END_DATE

            End If
            rgDeclare_TimeRice.Rebind()
            ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
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
                        rgDeclare_TimeRice.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDeclare_TimeRice.CurrentPageIndex = 0
                        rgDeclare_TimeRice.MasterTableView.SortExpressions.Clear()
                        rgDeclare_TimeRice.Rebind()
                    Case "Cancel"
                        rgDeclare_TimeRice.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Su ly su kien SelectedNodeChanged cua control ctrlOrganization
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            End If
            rgDeclare_TimeRice.CurrentPageIndex = 0
            rgDeclare_TimeRice.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New AttendanceRepository
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue), _
                                            .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve}
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    If rgDeclare_TimeRice.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    If rgDeclare_TimeRice.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    If rgDeclare_TimeRice.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case "EXPORT_TEMP"
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    isLoadPopup = 2 'Chọn Org
                    UpdateControlState()
                    ctrlOrgPopup.Show()

                Case "IMPORT_TEMP"
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    ctrlUpload.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgDeclare_TimeRice.ExportExcel(Server, Response, dtDatas, "DeclareRice")
                        End If
                    End Using
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly import cac ban ghi
    ''' </summary>
    ''' <remarks></remarks>
    Function SaveOT() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New AttendanceRepository
            Dim obj As New AT_TIME_RICEDTO
            Dim gId As New Decimal?
            gId = 0
            For Each row As DataRow In dtDataImportEmployee.Rows
                obj.EMPLOYEE_CODE = row("EMPLOYEE_CODE")
                obj.WORKINGDAY = Date.Parse(row("WORKINGDAY"))
                rep.InsertDelareRice(obj, gId)
            Next
            Return True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
        Return False
    End Function

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly doc va luu cac ban ghi trong file import vao ViewState
    ''' </summary>
    ''' <remarks></remarks>
    Property dtData As DataTable
        Get
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                If ViewState(Me.ID & "_dtData") Is Nothing Then
                    Dim dt As New DataTable
                    dt.Columns.Add("ID", GetType(String))
                    dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                    dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                    dt.Columns.Add("VN_FULLNAME", GetType(String))
                    dt.Columns.Add("TITLE_NAME", GetType(String))
                    dt.Columns.Add("ORG_ID", GetType(String))
                    dt.Columns.Add("ORG_NAME", GetType(String))
                    dt.Columns.Add("ORG_PATH", GetType(String))
                    dt.Columns.Add("DATE_FROM", GetType(String))
                    dt.Columns.Add("DATE_TO", GetType(String))
                    dt.Columns.Add("PRICE", GetType(String))
                    ViewState(Me.ID & "_dtData") = dt
                End If
                Return ViewState(Me.ID & "_dtData")
                _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            End Try
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly validate cac ban ghi trong file import
    ''' </summary>
    ''' <remarks></remarks>
    Function loadToGrid() As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim dtEmpID As DataTable

            Dim _validate As New AT_TIME_RICEDTO
            Dim rep As New AttendanceRepository
            Dim lstEmp As New List(Of String)
            dtDataImportEmployee = dtData.Clone
            dtError = dtData.Clone
            Dim irow = 5
            Dim irowEm = 5
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)
                'If row("EMPLOYEE_CODE").ToString <> "" And rowError("EMPLOYEE_CODE").ToString = "" Then
                '    Dim empCode = row("EMPLOYEE_CODE").ToString
                '    If Not lstEmp.Contains(empCode) Then
                '        lstEmp.Add(empCode)
                '    Else
                '        isError = True
                '        rowError("EMPLOYEE_CODE") = "Nhân viên bị trùng trong file import"
                '    End If
                'End If

                sError = "Từ ngày không được để trống"
                ImportValidate.IsValidDate("DATE_FROM", row, rowError, isError, sError)
                sError = "Đến ngày không được để trống"
                ImportValidate.IsValidDate("DATE_TO", row, rowError, isError, sError)
                sError = "Đơn giá không được để trống"
                ImportValidate.EmptyValue("PRICE", row, rowError, isError, sError)
                If row("PRICE").ToString <> "" Then
                    sError = "Đơn giá không phải kiểu số"
                    ImportValidate.IsValidNumber("PRICE", row, rowError, isError, sError)
                End If


                'sError = "Từ ngày không đúng định dạng"
                'If ImportValidate.ValidateDate(row("DATE_FROM"), sError) = False Then
                '    rowError("DATE_FROM") = sError
                '    isError = True
                'End If
                'sError = "Đến ngày không đúng định dạng"
                'If ImportValidate.ValidateDate(row("DATE_TO"), sError) = False Then
                '    rowError("DATE_TO") = sError
                '    isError = True
                'End If

                If rowError("DATE_FROM").ToString = "" And _
                    rowError("DATE_TO").ToString = "" And _
                     row("DATE_FROM").ToString <> "" And _
                    row("DATE_TO").ToString <> "" Then
                    Dim startdate = Date.Parse(row("DATE_FROM"))
                    Dim enddate = Date.Parse(row("DATE_TO"))
                    If startdate > enddate Then
                        rowError("DATE_FROM") = "Từ ngày phải nhỏ hơn đến ngày"
                        isError = True
                    End If
                End If


                If isError Then
                    rowError("ID") = irow
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("ORG_PATH") = row("ORG_PATH").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    dtError.Rows.Add(rowError)
                Else
                    dtDataImportEmployee.ImportRow(row)
                End If
                irow = irow + 1
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Import_DeclareTimeRice_Error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            If isError Then
                Return False
            Else
                ' check nv them vao file import có nằm trong hệ thống không.
                For j As Integer = 0 To dsDataComper.Rows.Count - 1
                    If dsDataComper(j)("EMPLOYEE_ID") = "" Then
                        dtEmpID = New DataTable
                        dtEmpID = rep.GetEmployeeID(dsDataComper(j)("EMPLOYEE_CODE"), cboPeriod.SelectedValue)
                        rowError = dtError.NewRow
                        If dtEmpID Is Nothing Or dtEmpID.Rows.Count <= 0 Then
                            rowError("EMPLOYEE_CODE") = "Mã nhân viên " & dsDataComper(j)("EMPLOYEE_CODE") & " không tồn tại trên hệ thống."
                            isError = True
                        Else
                            dsDataComper(j)("EMPLOYEE_ID") = dtEmpID.Rows(0)("ID")
                        End If
                    End If
                    ' check ngày hiệu lực bị trùng.
                    If dsDataComper(j)("EMPLOYEE_ID") <> "" Then
                        Dim startdate As New Date
                        startdate = ToDate(dsDataComper(j)("DATE_FROM"))

                        While startdate <= ToDate(dsDataComper(j)("DATE_TO"))
                            _validate.ID = Nothing
                            _validate.EMPLOYEE_ID = CDec(dsDataComper(j)("EMPLOYEE_ID"))
                            _validate.WORKINGDAY = ToDate(dsDataComper(j)("DATE_FROM"))
                            '_validate.EFFECT_DATE_TO = ToDate(dsDataComper(j)("DATE_TO"))

                            If rep.ValidateDelareRice(_validate) Then
                                rowError("DATE_FROM") = "Khoảng thời gian hiệu lực bị trùng."
                                isError = True
                            End If
                            startdate = startdate.AddDays(1)
                        End While

                    End If
                    If isError Then
                        rowError("ID") = irowEm
                        dtError.Rows.Add(rowError)
                    End If
                    irowEm = irowEm + 1
                    isError = False
                Next
                If dtError.Rows.Count > 0 Then
                    dtError.TableName = "DATA"
                    Session("EXPORTREPORT") = dtError
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Import_DeclareTimeRice_Error')", True)
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Else
                    Return True
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Su ly su kien click nut OK cua button ctrlUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim rep As New AttendanceRepository
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ctrlUpload.UploadedFiles.Count = 0 Then
                ShowMessage(Translate("Bạn chưa chọn biễu mẫu import"), NotifyType.Warning)
                Exit Sub
            End If
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            For Each file As UploadedFile In ctrlUpload.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                If workbook.Worksheets.GetSheetByCodeName("DataImportDeclareTimeRice") Is Nothing Then
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End If
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(3, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            dsDataComper = dsDataPrepare.Tables(0).Clone()
            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If Not isRow Then
                        Continue For
                    End If
                    dtData.ImportRow(row)
                    dsDataComper.ImportRow(row)
                Next
            Next
            If loadToGrid() Then
                Dim objSIGN As AT_TIME_RICEDTO
                Dim gID As Decimal
                Dim dtDataImp As DataTable = dsDataPrepare.Tables(0)
                For Each dr In dsDataComper.Rows
                    Dim startdate As New Date
                    startdate = ToDate(dr("DATE_FROM"))

                    While startdate <= ToDate(dr("DATE_TO"))
                        objSIGN = New AT_TIME_RICEDTO
                        objSIGN.EMPLOYEE_ID = CInt(dr("EMPLOYEE_ID"))
                        objSIGN.ORG_ID = CInt(dr("ORG_ID"))
                        objSIGN.TITLE_ID = CInt(dr("TITLE_ID"))
                        objSIGN.STAFF_RANK_ID = 0
                        objSIGN.WORKINGDAY = startdate
                        objSIGN.PRICE = CInt(dr("PRICE"))
                        objSIGN.ACTFLG = "A"
                        objSIGN.NOTE = ""

                        rep.InsertDelareRice(objSIGN, gID)

                        startdate = startdate.AddDays(1)
                    End While


                Next
                CurrentState = CommonMessage.STATE_NORMAL
                Refresh("InsertView")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Su ly su kien OrganizationSelected cua control ctrlOrgPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New AttendanceRepository
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(e.CurrentValue), _
                                           .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                           .IS_DISSOLVE = ctrlOrgPopup.IsDissolve}
            If rep.IS_PERIODSTATUS(_param) = False Then
                ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                Exit Sub
            End If

            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportDeclareTimeRice&PERIOD_ID=" & cboPeriod.SelectedValue & "&orgid=" & e.CurrentValue & "&IS_DISSOLVE=" & IIf(ctrlOrgPopup.IsDissolve, "1", "0") & "')", True)

            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Su ly su kien click nut Cancel cua button ctrlOrgPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrgPopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
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
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_TIME_RICEDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgDeclare_TimeRice, obj)
            Dim _param = New ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue, _
                                           .IS_DISSOLVE = ctrlOrganization.IsDissolve,
                                           .IS_FULL = True}
            Dim Sorts As String = rgDeclare_TimeRice.MasterTableView.SortExpressions.GetSortString()

            'If Not String.IsNullOrEmpty(txtName.Text) Then
            '    obj.EMPLOYEE_CODE = txtName.Text
            'End If
            If rdtungay.SelectedDate.HasValue Then
                obj.FROM_DATE = rdtungay.SelectedDate
            End If
            If rdDenngay.SelectedDate.HasValue Then
                obj.END_DATE = rdDenngay.SelectedDate
            End If
            If chkChecknghiViec.Checked Then
                obj.IS_TERMINATE = True
            End If
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.REGISTER_OT = rep.GetDelareRice(obj, _param, MaximumRows, rgDeclare_TimeRice.CurrentPageIndex, rgDeclare_TimeRice.PageSize, "CREATED_DATE desc")
                Else
                    Me.REGISTER_OT = rep.GetDelareRice(obj, _param, MaximumRows, rgDeclare_TimeRice.CurrentPageIndex, rgDeclare_TimeRice.PageSize)
                End If
            Else
                Return rep.GetDelareRice(obj, _param).ToTable
            End If

            rgDeclare_TimeRice.VirtualItemCount = MaximumRows
            rgDeclare_TimeRice.DataSource = Me.REGISTER_OT
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDeclare_TimeRice.NeedDataSource
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
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Su ly su kien click cua button btnSearch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgDeclare_TimeRice.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Su ly su kien PageIndexChanged cua button rgDeclare_TimeRice
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgDeclare_TimeRice.PageIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien AjaxRequest cua combobox AjaxManager
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim url = e.Argument
            If (url.Contains("reload=1")) Then
                rgDeclare_TimeRice.CurrentPageIndex = 0
                rgDeclare_TimeRice.Rebind()
                If rgDeclare_TimeRice.Items IsNot Nothing AndAlso rgDeclare_TimeRice.Items.Count > 0 Then
                    rgDeclare_TimeRice.Items(0).Selected = True
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Su ly su kien ItemDataBound cua button rgDeclare_TimeRice
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgDeclare_TimeRice_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgDeclare_TimeRice.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
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
    ''' 16/08/2017 14:00
    ''' </lastupdate>
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
        End Try
    End Sub

#End Region
End Class