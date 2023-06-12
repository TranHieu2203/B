Imports System.IO
Imports Aspose.Cells
Imports Attendance
Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Payroll.PayrollBusiness
Imports Profile
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_PayrollAdvance
    Inherits Common.CommonView

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Payroll\Modules\Business" + Me.GetType().Name.ToString()


#Region "Property"
    Private Property PERIOD As List(Of AT_PERIODDTO)
        Get
            Return ViewState(Me.ID & "_PERIOD")
        End Get
        Set(ByVal value As List(Of AT_PERIODDTO))
            ViewState(Me.ID & "_PERIOD") = value
        End Set
    End Property
    Private Property endDate As Date
        Get
            Return ViewState(Me.ID & "_endDate")
        End Get
        Set(ByVal value As Date)
            ViewState(Me.ID & "_endDate") = value
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
    ''' <lastupdate>
    ''' 06/07/2017 18:11
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
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 18:11
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgData
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            rgData.AllowCustomPaging = True
            rgData.SetFilter()
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 18:11
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
            FillRadCombobox(cboPeriod, lsData, "PERIOD_T", "PERIOD_ID", True)
            If lsData.Count > 0 Then
                Dim periodid = (From d In lsData Where d.MONTH = Date.Now.Month And d.YEAR = Date.Now.Year Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                Else
                    cboPeriod.SelectedValue = lsData.Item(0).PERIOD_ID.ToString()
                End If
            End If

            lsData = rep.Load_Emp_obj()
            FillRadCombobox(cboEmpObj, lsData, "PERIOD_NAME", "ID", True)

            ntxtNotSalary.Value = 3
            ntxtSalary.Value = 7
            chkAll.Checked = True
            ntxtSalAdvance.Value = 0

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Event SelectedIndexChanged combobox kỳ công
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        'Dim repS As New AttendanceStoreProcedure
        Dim period As New AT_PERIODDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If cboPeriod.SelectedValue <> "" AndAlso cboEmpObj.SelectedValue <> "" Then
                Dim ddate = rep.Load_date(CDec(Val(cboPeriod.SelectedValue)), CDec(Val(cboEmpObj.SelectedValue)))

                If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                    rdtungay.SelectedDate = ddate.START_DATE
                    rdDenngay.SelectedDate = ddate.START_DATE.Value.AddDays(14)
                    Dim startStr As String = String.Format("{0}/{1}/{2}", If(ddate.START_DATE.Value.Day < 10, "0" & ddate.START_DATE.Value.Day, ddate.START_DATE.Value.Day), If(ddate.START_DATE.Value.Month < 10, "0" & ddate.START_DATE.Value.Month, ddate.START_DATE.Value.Month), ddate.START_DATE.Value.Year)
                    Dim endStr As String = String.Format("{0}/{1}/{2}", If(ddate.END_DATE.Value.Day < 10, "0" & ddate.END_DATE.Value.Day, ddate.END_DATE.Value.Day), If(ddate.END_DATE.Value.Month < 10, "0" & ddate.END_DATE.Value.Month, ddate.END_DATE.Value.Month), ddate.END_DATE.Value.Year)
                    lbPeriod.Text = String.Format("{0} --> {1}", startStr, endStr)
                    endDate = ddate.END_DATE
                Else
                    ClearControlValue(rdtungay, rdDenngay, lbPeriod)
                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        Finally
            rep.Dispose()
        End Try

    End Sub

    Private Sub cboEmpObj_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboEmpObj.SelectedIndexChanged
        Try
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            If cboPeriod.SelectedValue <> "" AndAlso cboEmpObj.SelectedValue <> "" Then
                Dim rep = New AttendanceRepository
                Dim ddate = rep.Load_date(CDec(Val(cboPeriod.SelectedValue)), CDec(Val(cboEmpObj.SelectedValue)))

                If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                    rdtungay.SelectedDate = ddate.START_DATE
                    rdDenngay.SelectedDate = ddate.START_DATE.Value.AddDays(14)
                    Dim startStr As String = String.Format("{0}/{1}/{2}", If(ddate.START_DATE.Value.Day < 10, "0" & ddate.START_DATE.Value.Day, ddate.START_DATE.Value.Day), If(ddate.START_DATE.Value.Month < 10, "0" & ddate.START_DATE.Value.Month, ddate.START_DATE.Value.Month), ddate.START_DATE.Value.Year)
                    Dim endStr As String = String.Format("{0}/{1}/{2}", If(ddate.END_DATE.Value.Day < 10, "0" & ddate.END_DATE.Value.Day, ddate.END_DATE.Value.Day), If(ddate.END_DATE.Value.Month < 10, "0" & ddate.END_DATE.Value.Month, ddate.END_DATE.Value.Month), ddate.END_DATE.Value.Year)
                    lbPeriod.Text = String.Format("{0} --> {1}", startStr, endStr)
                    endDate = ddate.END_DATE
                Else
                    ClearControlValue(rdtungay, rdDenngay, lbPeriod)
                End If
            End If
        Catch ex As Exception
            '_myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi taso toolbar voi cac button them moi, sua, xuat file, xoa, in hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarContracts

            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Calculate,
                                       ToolbarItem.Export,
                                       ToolbarItem.ExportTemplate,
                                       ToolbarItem.Import,
                                       ToolbarItem.Lock,
                                       ToolbarItem.Unlock,
                                       ToolbarItem.Delete)

            MainToolBar.Items(2).Text = Translate("Xuất file mẫu")
            MainToolBar.Items(3).Text = Translate("Nhập file mẫu")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Xet cac tuy chon gia tri cua Message la UpdateView, InsertView
    ''' Bind lai du lieu cho grid rgData
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation

            If Not IsPostBack Then
            Else
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command in hop dong, xoa hop dong, xuat file excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New PayrollRepository
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "PayrollAdvance")
                            Exit Sub
                        End If
                    End Using
                Case CommonMessage.TOOLBARTIEM_CALCULATE
                    If cboEmpObj.SelectedValue = "" Then
                        ShowMessage(Translate("Đối tượng nhân viên bắt buộc nhập."), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rdDenngay.SelectedDate Is Nothing Or rdtungay.SelectedDate Is Nothing Then
                        ShowMessage(Translate("Chốt công từ	và Chốt công đến ngày bắt buộc nhập."), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rdDenngay.SelectedDate < rdtungay.SelectedDate Or rdDenngay.SelectedDate > endDate Then
                        ShowMessage(Translate("Đến ngày phải phải nằm trong kỳ lương, Vui lòng chọn lại Đến ngày"), NotifyType.Warning)
                        Exit Sub
                    End If

                    If ntxtSalary.Value Is Nothing Then
                        ShowMessage(Translate("Số ngày nghỉ có lương bắt buộc nhập."), NotifyType.Warning)
                        Exit Sub
                    End If

                    If ntxtNotSalary.Value Is Nothing Then
                        ShowMessage(Translate("Số ngày nghỉ không lương bắt buộc nhập."), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rep.CAL_PAYROLL_ADVANCE(cboPeriod.SelectedValue, rdtungay.SelectedDate, rdDenngay.SelectedDate, endDate, Decimal.Parse(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve, ntxtSalary.Value, ntxtNotSalary.Value) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_LOCK
                    If chkAll.Checked Then
                        Dim lstId As New List(Of Decimal)
                        Dim _param = New Payroll.PayrollBusiness.ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

                        If rep.ActivePayrollAdvance(lstId, _param, cboPeriod.SelectedValue, 1) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    Else
                        If rgData.SelectedItems.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                            Exit Sub
                        End If
                        Dim lstId As New List(Of Decimal)
                        For Each _item As GridDataItem In rgData.SelectedItems
                            If _item.GetDataKeyValue("IS_LOCK") = 1 Then
                                ShowMessage(Translate("Đã khóa, không thể xóa. vui lòng kiểm tra lại !"), NotifyType.Warning)
                                Exit Sub
                            End If
                            lstId.Add(_item.GetDataKeyValue("EMPLOYEE_ID"))
                        Next

                        Dim _param = New Payroll.PayrollBusiness.ParamDTO With {.ORG_ID = 0,
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

                        If rep.ActivePayrollAdvance(lstId, _param, cboPeriod.SelectedValue, 1) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    End If

                Case CommonMessage.TOOLBARITEM_UNLOCK
                    If chkAll.Checked Then
                        Dim lstId As New List(Of Decimal)
                        Dim _param = New Payroll.PayrollBusiness.ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

                        If rep.ActivePayrollAdvance(lstId, _param, cboPeriod.SelectedValue, 0) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    Else
                        If rgData.SelectedItems.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                            Exit Sub
                        End If
                        Dim lstId As New List(Of Decimal)
                        For Each _item As GridDataItem In rgData.SelectedItems
                            lstId.Add(_item.GetDataKeyValue("EMPLOYEE_ID"))
                        Next

                        Dim _param = New Payroll.PayrollBusiness.ParamDTO With {.ORG_ID = 0,
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

                        If rep.ActivePayrollAdvance(lstId, _param, cboPeriod.SelectedValue, 0) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim tempPath = "~/ReportTemplates//Payroll//Import//Template_Import_Tamungluong.xls"
                    If Not File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath))) Then
                        ' Mẫu báo cáo không tồn tại
                        ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim dsData = CreateDataFilter(True)
                    If dsData.Rows.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        Exit Sub
                    End If
                    dsData.TableName = "TABLE"

                    ExportTemplate("Payroll/Import/Template_Import_Tamungluong.xls", dsData, Nothing, _
                                              "Import_Tamungluong" & Format(Date.Now, "yyyyMMdd"))
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Event sụ kiên khi click [OK] trên popup Upload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Try
            Import_Data()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub Import_Data()
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload1.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            'Dim rep As New AttendanceRepository
            Dim ds As New DataSet
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload1.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                file.SaveAs(fileName, True)
                Using ep As New ExcelPackage
                    ds = ep.ReadExcelToDataSet(fileName, False)
                End Using
            End If
            If ds Is Nothing Then
                Exit Sub
            End If
            TableMapping(ds.Tables(0))

            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    Dim rep As New PayrollRepository
                    If rep.IMPORT_PAYROLL_ADVANCE(DocXml, Nothing) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('HU_ANNUALLEAVE_PLANS_ERROR')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(1).ColumnName = "EMPLOYEE_CODE"
        dtTemp.Columns(11).ColumnName = "SALARY_ADVANCE"
        dtTemp.Columns(12).ColumnName = "PERIOD_ID"
        dtTemp.Columns(13).ColumnName = "EMPLOYEE_ID"

        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()

        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        Dim empId As Integer
        Dim rep As New ProfileBusinessRepository
        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            dtLogs.Columns.Add("ID", GetType(Integer))
            dtLogs.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtLogs.Columns.Add("DISCIPTION", GetType(String))
        End If
        dtLogs.Clear()

        'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
        Dim rowDel As DataRow
        For i As Integer = 0 To dtTemp.Rows.Count - 1
            If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            rowDel = dtTemp.Rows(i)
            If rowDel("EMPLOYEE_CODE").ToString.Trim = "" Then
                dtTemp.Rows(i).Delete()
            End If
        Next

        For Each rows As DataRow In dtTemp.Rows
            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For

            newRow = dtLogs.NewRow
            newRow("ID") = count + 1
            newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")

            empId = rep.CheckEmployee_Exits(rows("EMPLOYEE_CODE"))

            If empId = 0 Then
                newRow("DISCIPTION") = "Mã nhân viên - Không tồn tại,"
                _error = False
            Else
                rows("EMPLOYEE_CODE") = empId
            End If

            If IsDBNull(rows("EMPLOYEE_ID")) Then
                rows("EMPLOYEE_ID") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Dữ liệu hiện tại không đúng dữ liệu xuất từ hệ thống,"
                _error = False
            Else
                If rows("EMPLOYEE_ID") <> empId Then
                    rows("EMPLOYEE_ID") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Dữ liệu hiện tại không đúng dữ liệu xuất từ hệ thống,"
                    _error = False
                End If
            End If

            If IsDBNull(rows("SALARY_ADVANCE")) Then
                rows("SALARY_ADVANCE") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Số tiền tạm ứng- Bắt buộc nhập,"
                _error = False
            Else
                If Not IsNumeric(rows("SALARY_ADVANCE")) Then
                    rows("SALARY_ADVANCE") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Số tiền tạm ứng- Không đúng định dạng,"
                    _error = False
                End If
            End If

            If _error = False Then
                dtLogs.Rows.Add(newRow)
                _error = True
            End If
        Next
        dtTemp.AcceptChanges()
    End Sub

    Public Function ExportTemplate(ByVal sReportFileName As String,
                                             ByVal dsData As DataTable,
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


    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien SelectedNodeChanged cua control ctrlOrg
    ''' Xet lai cac thiet lap trang thai cho grid rgContract
    ''' Bind lai du lieu cho rgContract
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command button cua control ctrkMessageBox
    ''' Cap nhat trang thai cua cac control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New PayrollRepository
                Dim lstId As New List(Of Decimal)
                For Each _item As GridDataItem In rgData.SelectedItems
                    If _item.GetDataKeyValue("IS_LOCK") = 1 Then
                        ShowMessage(Translate("Đã khóa, không thể xóa. vui lòng kiểm tra lại !"), NotifyType.Warning)
                        Exit Sub
                    End If
                    lstId.Add(_item.GetDataKeyValue("EMPLOYEE_ID"))
                Next
                If rep.DeletePayrollAdvance(lstId, cboPeriod.SelectedValue) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgData.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cua button btnSearch
    ''' Thiets lap trang thai cho rgData
    ''' Bind lai du lieu cho rgData
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New PayrollRepository
            Dim startTime As DateTime = DateTime.UtcNow
            Dim lstId As New List(Of Decimal)

            If rgData.SelectedItems.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If

            If ntxtSalAdvance.Value Is Nothing Then
                ShowMessage(Translate("Số tiền tạm ứng bắt buộc nhập."), NotifyType.Warning)
                Exit Sub
            End If

            For Each _item As GridDataItem In rgData.SelectedItems
                If _item.GetDataKeyValue("IS_LOCK") = 1 Then
                    ShowMessage(Translate("Đã khóa, không thể xóa. vui lòng kiểm tra lại !"), NotifyType.Warning)
                    Exit Sub
                End If
                lstId.Add(_item.GetDataKeyValue("EMPLOYEE_ID"))
            Next

            If rep.ModifyPayrollAdvance(lstId, cboPeriod.SelectedValue, ntxtSalAdvance.Value) Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                rgData.Rebind()
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
            End If

            rgData.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub




    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rad grid 
    ''' Tao du lieu filter
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub


    ''' <summary>
    ''' Event SelectedIndexChange combobox năm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtData As List(Of AT_PERIODDTO)
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Try
            period.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            cboPeriod.ClearSelection()
            FillRadCombobox(cboPeriod, dtData, "PERIOD_T", "PERIOD_ID", True)

            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        Finally
            rep.Dispose()
            period = Nothing
            startTime = Nothing
            dtData = Nothing
        End Try

    End Sub

#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc cap nhat trang thai cac control trong page
    ''' Cap nhat trang thai tbarContracts, rgData, ctrlOrg
    ''' Cap nhat trang thai hien tai, neu trang thai hien tai cua trang la trang thai xoa
    ''' thi xoa hop dong vaf lam moi lai UpdateView
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc tao du lieu filter
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New PayrollRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim _filter As New PayrollAdvanceDTO
        Try

            If ctrlOrg.CurrentValue Is Nothing Then
                rgData.DataSource = New List(Of PayrollAdvanceDTO)
                Exit Function
            End If
            Dim _param = New Payroll.PayrollBusiness.ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

            SetValueObjectByRadGrid(rgData, _filter)
            If cboPeriod.SelectedValue <> "" Then
                _filter.PERIOD_ID = cboPeriod.SelectedValue
            End If

            If ntxtSalary.Value IsNot Nothing Then
                _filter.HAVE_SAL = ntxtSalary.Value
            Else
                _filter.HAVE_SAL = 0
            End If

            If ntxtNotSalary.Value IsNot Nothing Then
                _filter.NOT_SAL = ntxtNotSalary.Value
            Else
                _filter.NOT_SAL = 0
            End If

            If chkLock.Checked Then
                _filter.IS_LOCK = 1
            Else
                If chkUnLock.Checked Then
                    _filter.IS_LOCK = 0
                Else
                    _filter.IS_LOCK = 2
                End If
            End If

            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstObj As New List(Of PayrollAdvanceDTO)
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetPayrollAdvance(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetPayrollAdvance(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    lstObj = rep.GetPayrollAdvance(_filter, _param, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                Else
                    lstObj = rep.GetPayrollAdvance(_filter, _param, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, )
                End If

                rgData.VirtualItemCount = MaximumRows
                rgData.DataSource = lstObj
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

#End Region
End Class