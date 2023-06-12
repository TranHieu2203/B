Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_Norm
    Inherits Common.CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Payroll\Modules\Payroll\Setting" + Me.GetType().Name.ToString()

#Region "Property"
    Public Property List_Oganization_ID As List(Of Decimal)
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_List_Oganization_ID") = value
        End Set
        Get
            Return PageViewState(Me.ID & "_List_Oganization_ID")
        End Get
    End Property
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("ORG_CODE", GetType(String))
                dt.Columns.Add("ORG_ID", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("NORM_ID", GetType(String))
                dt.Columns.Add("NORM_CODE", GetType(String))
                dt.Columns.Add("NORM_NAME", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("EFFECT_DATE", GetType(String))
                dt.Columns.Add("NOTE", GetType(String))
                dt.Columns.Add("VALUE", GetType(String))
                dt.Columns.Add("IS_ORG_OR_EMP", GetType(String))
                dt.Columns.Add("NO", GetType(String))
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
    ''' vData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property vData As List(Of NormDTO)
        Get
            Return ViewState(Me.ID & "vData")
        End Get
        Set(ByVal value As List(Of NormDTO))
            ViewState(Me.ID & "vData") = value
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
            Return ViewState(Me.ID & "vIDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "vIDSelect") = value
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
    ''' ValueObjSalary
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueObjSalary As Decimal
        Get
            Return ViewState(Me.ID & "_ValueObjSalary")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueObjSalary") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView, la ham load du lieu, control states cua usercontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        My.Application.ChangeCulture("EN-US")
        Try
            If Not IsPostBack Then
                rtxt_Year_Seniority.Visible = False
                tdYear_Seniority.Visible = False
            End If

            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            'rgData.ClientSettings.EnablePostBackOnRowClick = True
            InitControl()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>Load cac control, menubar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarSalaryGroups

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Export, ToolbarItem.ExportTemplate, ToolbarItem.Import, ToolbarItem.Delete)

            MainToolBar.Items(5).Text = Translate("Xuất file mẫu")
            MainToolBar.Items(6).Text = Translate("Nhập file mẫu")
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If Not IsPostBack Then
                GetDataCombo()
                orgChk.Checked = True
                lblEmpCode.Visible = False
                btnEmployee.Visible = False
                txtEmployeeCode.Visible = False
                lbEmployeeName.Visible = False
                txtEmployeeName.Visible = False
                lbOrg.Visible = False
                txtOrg.Visible = False
                lbTITLE.Visible = False
                txtTITLE.Visible = False
                reqEmployeeCode.Enabled = False

                btnFindOrg.Enabled = False
                btnEmployee.Enabled = False

                lbOrgName.Visible = True
                txtOrgName.Visible = True
                btnFindOrg.Visible = True
                reqOrg.Enabled = True

                CurrentState = CommonMessage.STATE_NORMAL
                rgData.Rebind()
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        'SelectedItemDataGridByKey(rgData, IDSelect, , rgData.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                        'SelectedItemDataGridByKey(rgData, IDSelect, )
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                        ClearControlValue(rcboNorm, rtxtValue, txtDesc)
                        ExcuteScript("Clear", "clRadDatePicker()")
                        rgData.Rebind()
                End Select
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            LoadPopup(isLoadPopup)
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    EnableControlAll(False, rtxtValue, rcboNorm, rdpStartDate, rdpEndDate, txtDesc, empChk, orgChk, btnFindOrg, btnEmployee, rtxt_Year_Seniority)

                Case CommonMessage.STATE_NEW
                    'ClearControlValue(rtxtValue, rcboNorm, rdpStartDate, rdpEndDate, txtDesc)
                    EnableControlAll(True, rtxtValue, rcboNorm, rdpStartDate, rdpEndDate, txtDesc, empChk, orgChk, btnFindOrg, btnEmployee, rtxt_Year_Seniority)

                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, rtxtValue, rcboNorm, rdpStartDate, rdpEndDate, txtDesc, empChk, orgChk, btnFindOrg, btnEmployee, rtxt_Year_Seniority)
            End Select

            ChangeToolbarState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim objSalaryGroup As New SalaryGroupDTO
        Dim gID As Decimal

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    ClearControlValue(rtxtValue, rcboNorm, rdpStartDate, rdpEndDate, txtDesc)
                    CurrentState = CommonMessage.STATE_NEW

                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    IDSelect = Utilities.ObjToDecima(rgData.SelectedValue)
                    CurrentState = CommonMessage.STATE_EDIT

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "DS_TLDINHMUC")
                            Exit Sub
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim rep As New PayrollRepository
                        Dim obj As New NormDTO
                        obj.NORM_ID = rcboNorm.SelectedValue
                        If empChk.Checked Then
                            obj.EMPLOYEE_ID = hidIDEmp.Value
                        Else
                            obj.ORG_ID = hidOrg.Value
                        End If
                        obj.EFFECT_DATE = rdpStartDate.SelectedDate
                        obj.EXPIRE_DATE = rdpEndDate.SelectedDate
                        obj.NOTE = txtDesc.Text
                        obj.VALUE = rtxtValue.Value
                        obj.YEAR_SENIORITY = rtxt_Year_Seniority.Value
                        Dim count = (From p In ListComboData.LIST_NORM_ID Where p.ID = Decimal.Parse(rcboNorm.SelectedValue) And p.ATTRIBUTE1 = 1).Count
                        If count = 1 AndAlso rtxt_Year_Seniority.Value Is Nothing Then
                            ShowMessage("Bạn phải nhập định mức năm", Utilities.NotifyType.Error)
                            Exit Sub
                        End If
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                obj.ID = 0
                                If Not rep.checkDup(obj) Then
                                    ShowMessage("Thiết lập đã tồn tại", Utilities.NotifyType.Error)
                                    Exit Sub
                                End If
                                If rep.InsertNorm(obj, 0) Then
                                    CurrentState = CommonMessage.STATE_NEW
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                    ExcuteScript("Clear", "clRadDatePicker()")
                                    ClearControlValue(rcboNorm, rtxtValue, txtDesc, rtxt_Year_Seniority)
                                    rgData.Rebind()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If

                            Case CommonMessage.STATE_EDIT
                                obj.ID = hidID.Value
                                If Not rep.checkDup(obj) Then
                                    ShowMessage("Thiết lập đã tồn tại", Utilities.NotifyType.Error)
                                    Exit Sub
                                End If
                                If rep.ModifyNorm(obj, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = obj.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                    ExcuteScript("Clear", "clRadDatePicker()")
                                    ClearControlValue(rcboNorm, rtxtValue, txtDesc)
                                    rgData.Rebind()
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
                Case CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim store As New PayrollStoreProcedure
                    Dim dsData As DataSet = store.GET_PA_STANDARD_SETUP_IMPORT_DATA()
                    ''ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_TimeSheet_Monthly')", True)
                    ExportTemplate("Payroll\Import\Template_Import_PA_STANDARD_SETUP.xlsx", _
                                              dsData, Nothing, _
                                              "Template_Import_PA_STANDARD_SETUP" & Format(Date.Now, "yyyyMMdd"))
            End Select

            UpdateControlState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
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
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New PayrollRepository
                Dim Objdata As New PAFomulerGroup
                Objdata.ID = Utilities.ObjToDecima(rgData.SelectedValue)

                Select Case e.ActionName
                    Case CommonMessage.TOOLBARITEM_DELETE
                        CurrentState = CommonMessage.STATE_DELETE
                        Dim lstDeletes As New List(Of Decimal)
                        For idx = 0 To rgData.SelectedItems.Count - 1
                            Dim item As GridDataItem = rgData.SelectedItems(idx)
                            lstDeletes.Add(item.GetDataKeyValue("ID"))
                        Next
                        If rep.DeleteNorm(lstDeletes) Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgData.Rebind()
                            ExcuteScript("Clear", "clRadDatePicker()")
                            ClearControlValue(rcboNorm, rtxtValue, txtDesc, txtEmployeeCode, txtEmployeeName, txtOrg, txtOrgName, txtTITLE, rdpStartDate, rdpEndDate)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                End Select

                UpdateControlState()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Kiem tra gia tri dang đươc chọn cua combox ĐỐI TƯỢNG LƯƠNG có tồn tại hoặc bị ngừng áp dụng hay không? 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    ''' 
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
                If String.IsNullOrEmpty(rows("NO").ToString) Then Continue For
                Dim newRow As DataRow = dtData.NewRow
                newRow("ORG_CODE") = rows("ORG_CODE")
                newRow("ORG_ID") = rows("ORG_ID")
                newRow("ORG_NAME") = rows("ORG_NAME")
                newRow("NORM_ID") = rows("NORM_ID")
                newRow("NORM_NAME") = rows("NORM_NAME")
                newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
                newRow("EFFECT_DATE") = rows("EFFECT_DATE")
                newRow("NOTE") = rows("NOTE")
                newRow("VALUE") = rows("VALUE")
                newRow("IS_ORG_OR_EMP") = rows("IS_ORG_OR_EMP")
                newRow("NO") = rows("NO")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, True)
                DocXml = sw.ToString
                Dim sp As New PayrollStoreProcedure
                If sp.IMPORT_PA_STANDARD_SETUP(DocXml) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                    rgData.Rebind()
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
#End Region

#Region "Custom"
    Function loadToGrid() As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New Attendance.AttendanceBusiness.AttendanceBusinessClient
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
                sError = "Chưa chọn theo phòng ban hay theo nhân viên"
                ImportValidate.EmptyValue("IS_ORG_OR_EMP", row, rowError, isError, sError)

                If Not IsDBNull(row("IS_ORG_OR_EMP")) AndAlso Not String.IsNullOrEmpty(row("IS_ORG_OR_EMP")) Then
                    If row("IS_ORG_OR_EMP") = "1" Then
                        If Not IsDBNull(row("ORG_ID")) AndAlso Not String.IsNullOrEmpty(row("ORG_ID")) Then
                            sError = "Chỉ được nhập số"
                            ImportValidate.IsValidNumber("IS_ORG_OR_EMP", row, rowError, isError, sError)
                        ElseIf IsDBNull(row("ORG_ID")) AndAlso String.IsNullOrEmpty(row("ORG_ID")) Then
                            sError = "Chưa nhập id phòng ban"
                            ImportValidate.EmptyValue("ORG_ID", row, rowError, isError, sError)
                        End If
                        row("EMPLOYEE_ID") = ""
                    ElseIf row("IS_ORG_OR_EMP") = "2" Then
                        If Not IsDBNull(row("EMPLOYEE_CODE")) Then
                            sError = "Nhân viên " & row("EMPLOYEE_CODE") & " không tồn tại"
                            Dim checkEmp = rep.GetEmployeeIDExits(row("EMPLOYEE_CODE"))
                            If checkEmp Is Nothing OrElse checkEmp.Rows.Count = 0 Then
                                ImportValidate.IsValidTime("EMPLOYEE_CODE", row, rowError, isError, sError)
                            Else
                                row("EMPLOYEE_ID") = checkEmp.Rows(0)("ID")
                            End If
                        ElseIf IsDBNull(row("EMPLOYEE_CODE")) Then
                            sError = "Chưa nhập id nhân viên"
                            ImportValidate.IsValidTime("EMPLOYEE_CODE", row, rowError, isError, sError)
                        End If
                        row("ORG_ID") = ""
                    Else
                        sError = "Chỉ được nhập 1 hoặc 2"
                        ImportValidate.IsValidTime("IS_ORG_OR_EMP", row, rowError, isError, sError)
                    End If
                End If

                sError = "Chưa nhập ngày hiệu lực"
                ImportValidate.EmptyValue("EFFECT_DATE", row, rowError, isError, sError)
                If Not IsDBNull(row("EFFECT_DATE")) AndAlso Not String.IsNullOrEmpty(row("EFFECT_DATE")) Then
                    sError = "Sai định dạng ngày"
                    ImportValidate.IsValidDate("EFFECT_DATE", row, rowError, isError, sError)
                End If

                sError = "Chưa nhập số tiền định mức"
                ImportValidate.EmptyValue("VALUE", row, rowError, isError, sError)
                If Not IsDBNull(row("VALUE")) AndAlso Not String.IsNullOrEmpty(row("VALUE")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("VALUE", row, rowError, isError, sError)
                End If

                sError = "Chưa nhập id loại định mức"
                ImportValidate.EmptyValue("NORM_ID", row, rowError, isError, sError)
                If Not IsDBNull(row("NORM_ID")) AndAlso Not String.IsNullOrEmpty(row("NORM_ID")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("NORM_ID", row, rowError, isError, sError)
                End If

                If isError Then
                    If IsDBNull(rowError("IS_ORG_OR_EMP")) Then
                        rowError("IS_ORG_OR_EMP") = row("IS_ORG_OR_EMP").ToString
                    End If
                    dtError.Rows.Add(rowError)
                End If
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("PA_STANDARD_SETUP") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_PA_STANDARD_SETUP_Error')", True)
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
        dtdata.Rows(0).Delete()
        dtdata.AcceptChanges()
    End Sub
    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim obj As New NormDTO
        CreateDataFilter = Nothing

        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, obj)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            obj.IS_TER = chkTerminate.Checked

            Using rep As New PayrollRepository
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetAllNorm(obj, Sorts).ToTable
                    Else
                        Return rep.GetAllNorm(obj).ToTable
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        Me.vData = rep.GetAllNorm(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                    Else
                        Me.vData = rep.GetAllNorm(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                    End If
                    rgData.VirtualItemCount = MaximumRows
                    rgData.DataSource = Me.vData
                End If
            End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Get data và bind vao combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository

        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_NORM_ID = True
                rep.GetComboboxData(ListComboData)
            End If

            FillDropDownList(rcboNorm, ListComboData.LIST_NORM_ID, "NAME_VN", "ID", Common.Common.SystemLanguage, True, rcboNorm.SelectedValue)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub orgChk_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles orgChk.CheckedChanged
        If orgChk.Checked Then
            lblEmpCode.Visible = False
            btnEmployee.Visible = False
            txtEmployeeCode.Visible = False
            lbEmployeeName.Visible = False
            txtEmployeeName.Visible = False
            lbOrg.Visible = False
            txtOrg.Visible = False
            lbTITLE.Visible = False
            txtTITLE.Visible = False
            reqEmployeeCode.Enabled = False

            lbOrgName.Visible = True
            txtOrgName.Visible = True
            btnFindOrg.Visible = True
            reqOrg.Enabled = True

            ClearControlValue(txtEmployeeCode, txtEmployeeName, txtOrg, txtTITLE, hidIDEmp)
        End If
    End Sub
    Private Sub empChk_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles empChk.CheckedChanged
        If empChk.Checked Then
            lblEmpCode.Visible = True
            btnEmployee.Visible = True
            txtEmployeeCode.Visible = True
            lbEmployeeName.Visible = True
            txtEmployeeName.Visible = True
            lbOrg.Visible = True
            txtOrg.Visible = True
            lbTITLE.Visible = True
            txtTITLE.Visible = True
            reqEmployeeCode.Enabled = True

            lbOrgName.Visible = False
            txtOrgName.Visible = False
            btnFindOrg.Visible = False
            reqOrg.Enabled = False

            ClearControlValue(txtOrgName, hidOrg)
        End If
    End Sub

    Private Sub chkTerminate_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTerminate.CheckedChanged
        rgData.Rebind()
    End Sub


    Private Sub LoadPopup(ByVal popupType As Int32)
        Select Case popupType
            Case 1
                If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
                    ctrlFindEmployeePopup.MustHaveContract = False
                End If
            Case 2
                ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                ctrlOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                If List_Oganization_ID IsNot Nothing AndAlso List_Oganization_ID.Count > 0 Then
                    ctrlOrgPopup.Bind_Find_ValueKeys = List_Oganization_ID

                End If
                ctrlOrgPopup.IS_HadLoad = False
                phPopup.Controls.Add(ctrlOrgPopup)
        End Select
    End Sub

    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 1
            'LoadPopup(1)
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstEmpID As New List(Of Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstEmpID = ctrlFindEmployeePopup.SelectedEmployeeID
            If lstEmpID.Count <> 0 Then
                FillDataEmp(lstEmpID(0))
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlFind_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles ctrlFindEmployeePopup.CancelClicked,
                ctrlOrgPopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, 0, Nothing, "ctrlFind_CancelClick")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    Private Sub FillDataEmp(ByVal empID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim empObj = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            txtEmployeeName.Text = empObj(0).FULLNAME_VN
            txtEmployeeCode.Text = empObj(0).EMPLOYEE_CODE
            txtOrg.Text = empObj(0).ORG_NAME
            txtTITLE.Text = empObj(0).TITLE_NAME
            hidIDEmp.Value = empObj(0).ID
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
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
                hidOrg.Value = e.CurrentValue
                txtOrgName.Text = orgItem.NAME_VN
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
            If sender IsNot Nothing Then
                List_Oganization_ID = New List(Of Decimal)
            End If
            UpdateControlState()
            ctrlOrgPopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgData.SelectedIndexChanged
        Dim a = CType(rgData.SelectedItems.Item(0), GridDataItem)
        If IsNumeric(a.GetDataKeyValue("ORG_ID")) Then
            lblEmpCode.Visible = False
            btnEmployee.Visible = False
            txtEmployeeCode.Visible = False
            lbEmployeeName.Visible = False
            txtEmployeeName.Visible = False
            lbOrg.Visible = False
            txtOrg.Visible = False
            lbTITLE.Visible = False
            txtTITLE.Visible = False
            reqEmployeeCode.Enabled = False

            lbOrgName.Visible = True
            txtOrgName.Visible = True
            btnFindOrg.Visible = True
            reqOrg.Enabled = True

            hidOrg.Value = a.GetDataKeyValue("ORG_ID")
            txtOrgName.Text = a.GetDataKeyValue("ORG")
            ClearControlValue(txtEmployeeCode, txtEmployeeName, txtOrg, txtTITLE, hidIDEmp)
            empChk.Checked = False
            orgChk.Checked = True
        Else
            lblEmpCode.Visible = True
            btnEmployee.Visible = True
            txtEmployeeCode.Visible = True
            lbEmployeeName.Visible = True
            txtEmployeeName.Visible = True
            lbOrg.Visible = True
            txtOrg.Visible = True
            lbTITLE.Visible = True
            txtTITLE.Visible = True
            reqEmployeeCode.Enabled = True

            lbOrgName.Visible = False
            txtOrgName.Visible = False
            btnFindOrg.Visible = False
            reqOrg.Enabled = False

            hidIDEmp.Value = a.GetDataKeyValue("EMPLOYEE_ID")
            txtEmployeeCode.Text = a.GetDataKeyValue("EMP_CODE")
            txtEmployeeName.Text = a.GetDataKeyValue("EMP_NAME")
            txtOrg.Text = a.GetDataKeyValue("ORG_NAME")
            txtTITLE.Text = a.GetDataKeyValue("TITLE_NAME")
            ClearControlValue(txtOrgName, hidOrg)
            orgChk.Checked = False
            empChk.Checked = True
        End If

        hidID.Value = a.GetDataKeyValue("ID")
        txtDesc.Text = a.GetDataKeyValue("NOTE")
        rcboNorm.SelectedValue = a.GetDataKeyValue("NORM_ID")
        rdpStartDate.SelectedDate = a.GetDataKeyValue("EFFECT_DATE")
        rdpEndDate.SelectedDate = a.GetDataKeyValue("EXPIRE_DATE")
        rtxtValue.Text = a.GetDataKeyValue("VALUE")
        rtxt_Year_Seniority.Text = a.GetDataKeyValue("YEAR_SENIORITY")
        Dim count = (From p In ListComboData.LIST_NORM_ID Where p.ID = Decimal.Parse(rcboNorm.SelectedValue) And p.ATTRIBUTE1 = 1).Count
        If count = 1 Then
            rtxt_Year_Seniority.Visible = True
            tdYear_Seniority.Visible = True
        Else
            rtxt_Year_Seniority.Visible = False
            tdYear_Seniority.Visible = False
        End If
    End Sub

    Private Sub rcboNorm_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcboNorm.SelectedIndexChanged
        Try
            If rcboNorm.SelectedValue <> "" Then
                Dim count = (From p In ListComboData.LIST_NORM_ID Where p.ID = Decimal.Parse(rcboNorm.SelectedValue) And p.ATTRIBUTE1 = 1).Count
                If count = 1 Then
                    rtxt_Year_Seniority.Visible = True
                    tdYear_Seniority.Visible = True

                Else
                    rtxt_Year_Seniority.Visible = False
                    tdYear_Seniority.Visible = False
                End If
            End If
        Catch ex As Exception
            Throw ex
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
#End Region
    Private Sub txtOrgName_TextChanged(sender As Object, e As EventArgs) Handles txtOrgName.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            If txtOrgName.Text.Trim <> "" Then
                Dim List_org = rep.GetOrganizationLocationTreeView()
                Dim orgList = (From p In List_org Where p.NAME_VN.ToUpper.Contains(txtOrgName.Text.Trim.ToUpper)).ToList
                If orgList.Count <= 0 Then
                    ShowMessage(Translate("Phòng ban vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                    ClearControlValue(hidOrg, txtOrgName)
                ElseIf orgList.Count = 1 Then
                    hidOrg.Value = orgList(0).ID
                    txtOrgName.Text = orgList(0).NAME_VN
                Else
                    List_Oganization_ID = (From p In orgList Select p.ID).ToList
                    btnFindOrg_Click(Nothing, Nothing)
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub txtEmployeeName_TextChanged(sender As Object, e As EventArgs) Handles txtEmployeeName.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If txtEmployeeName.Text <> "" Then
                    Reset_Find_Emp()
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = txtEmployeeName.Text
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        txtEmployeeName.Text = ""
                    ElseIf Count = 1 Then
                        'Dim empID = EmployeeList(0)
                        txtEmployeeName.Text = EmployeeList(0).FULLNAME_VN
                        hidIDEmp.Value = EmployeeList(0).ID.ToString()
                        txtTITLE.Text = EmployeeList(0).TITLE_NAME.ToString()
                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            FindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmployeeName.Text
                            ctrlFindEmployeePopup.MultiSelect = False
                            ctrlFindEmployeePopup.MustHaveContract = False
                            FindEmployee.Controls.Add(ctrlFindEmployeePopup)
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
        'txtEmployeeName.Text = EmployeeList(0).FULLNAME_VN
        hidIDEmp.Value = ""
        txtTITLE.Text = ""
    End Sub
End Class