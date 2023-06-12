Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_AccountingAdjust
    Inherits Common.CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Payroll/Module/Payroll/Business/" + Me.GetType().Name.ToString()

#Region "Property"

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
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

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("YEAR", GetType(String))
                dt.Columns.Add("PERIOD", GetType(String))
                dt.Columns.Add("PERIOD_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("EMPLOYEE_NAME", GetType(String))
                dt.Columns.Add("ORG_CODE", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("COST_CENTER", GetType(String))
                dt.Columns.Add("ORG_ID", GetType(String))
                dt.Columns.Add("ADJUSTING_DATE", GetType(String))
                dt.Columns.Add("ADJUSTING_X", GetType(String))
                dt.Columns.Add("NOTE", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 23/08/2017 09:00
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
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
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
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
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
            Me.MainToolBar = tbMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete, ToolbarItem.Export, ToolbarItem.ExportTemplate, ToolbarItem.Import)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            MainToolBar.Items(6).Text = Translate("Xuất file mẫu")
            MainToolBar.Items(7).Text = Translate("Nhập file mẫu")
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
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnableControlAll(True, btnFindEmployee, cboYear, cboPeriod, rdAdjustingDate, rnAdjustingX, btnFindOrg, txtNote)
                    EnableControlAll(False, cboYearSearch, cboPeriodSearch, rdFromDate, rdToDate, btnSearch)
                    rqAdjustingDate.Enabled = True
                    rqAdjustingX.Enabled = True
                    rqOrgSetName.Enabled = True
                    rqPeriod.Enabled = True
                    reqEmployeeCode.Enabled = True
                    rgData.Enabled = False
                Case CommonMessage.STATE_NORMAL
                    EnableControlAll(False, btnFindEmployee, cboYear, cboPeriod, rdAdjustingDate, rnAdjustingX, btnFindOrg, txtNote)
                    EnableControlAll(True, cboYearSearch, cboPeriodSearch, rdFromDate, rdToDate, btnSearch)
                    rgData.Enabled = True
                    rqAdjustingDate.Enabled = False
                    rqAdjustingX.Enabled = False
                    rqOrgSetName.Enabled = False
                    rqPeriod.Enabled = False
                    reqEmployeeCode.Enabled = False
                Case CommonMessage.STATE_EDIT
                    rgData.Enabled = False
                    EnableControlAll(True, btnFindEmployee, cboYear, cboPeriod, rdAdjustingDate, rnAdjustingX, btnFindOrg, txtNote)
                    EnableControlAll(False, cboYearSearch, cboPeriodSearch, rdFromDate, rdToDate, btnSearch)
                    rqAdjustingDate.Enabled = True
                    rqAdjustingX.Enabled = True
                    rqOrgSetName.Enabled = True
                    rqPeriod.Enabled = True
                    reqEmployeeCode.Enabled = True
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.DeleteAccountingAdjust(lstDeletes) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                            rgData.Rebind()
                            ClearControlValue(txtEmployeeCode, txtEmployeeName, txtOrgName, txtTitleName, rdAdjustingDate, rnAdjustingX, txtNote, txtOrgSetName, hidEmpID, hidOrgID)
                        Else
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                        End If
                    End Using
            End Select
            If isLoadPopup = 2 Then
                ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                phFindOrg.Controls.Add(ctrlFindOrgPopup)
                ctrlFindOrgPopup.Show()
            ElseIf isLoadPopup = 1 Then
                If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
                    ctrlFindEmployeePopup.MustHaveContract = False
                End If
            End If
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim dic As New Dictionary(Of String, Control)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New PayrollRepository
        Dim objPeriod As List(Of ATPeriodDTO)
        Dim objPeriod2 As List(Of ATPeriodDTO)
        Dim id As Integer = 0

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
            FillRadCombobox(cboYearSearch, table, "YEAR", "ID")
            cboYearSearch.SelectedValue = Date.Now.Year

            objPeriod = rep.GetPeriodbyYear(cboYear.SelectedValue)
            cboPeriod.DataSource = objPeriod
            cboPeriod.DataValueField = "ID"
            cboPeriod.DataTextField = "PERIOD_NAME"
            cboPeriod.DataBind()

            objPeriod2 = rep.GetPeriodbyYear(cboYearSearch.SelectedValue)
            objPeriod2.Add(New ATPeriodDTO With {.ID = Nothing, .PERIOD_NAME = Nothing})
            cboPeriodSearch.DataSource = objPeriod2
            cboPeriodSearch.DataValueField = "ID"
            cboPeriodSearch.DataTextField = "PERIOD_NAME"
            cboPeriodSearch.DataBind()

            Dim lst = (From s In objPeriod Where s.MONTH = Date.Now.Month).FirstOrDefault
            If Not lst Is Nothing Then
                cboPeriod.SelectedValue = lst.ID
            Else
                cboPeriod.SelectedIndex = 0
            End If
            Dim lst2 = (From s In objPeriod2 Where s.MONTH = Date.Now.Month).FirstOrDefault
            If Not lst2 Is Nothing Then
                cboPeriodSearch.SelectedValue = lst2.ID
            Else
                cboPeriodSearch.SelectedIndex = 0
            End If
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
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objAccounting As New PA_Accounting_AdjustingDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtEmployeeCode, txtEmployeeName, txtOrgName, txtTitleName, rdAdjustingDate, rnAdjustingX, txtNote, txtOrgSetName, hidEmpID, hidOrgID)
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "Accounting_Adjusting")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objAccounting.EMPLOYEE_ID = hidEmpID.Value
                        If cboPeriod.SelectedValue <> "" Then
                            objAccounting.PERIOD_ID = cboPeriod.SelectedValue
                        End If
                        If IsDate(rdAdjustingDate.SelectedDate) Then
                            objAccounting.ADJUSTING_DATE = rdAdjustingDate.SelectedDate
                        End If
                        If IsNumeric(rnAdjustingX.Value) Then
                            objAccounting.ADJUSTING_X = rnAdjustingX.Value
                        End If
                        If IsNumeric(hidOrgID.Value) Then
                            objAccounting.ORG_SET_ID = hidOrgID.Value
                        End If
                        objAccounting.NOTE = txtNote.Text.Trim
                        Using rep As New PayrollRepository
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    objAccounting.ID = 0
                                    If rep.ValidateAccountingAdjust(objAccounting) Then
                                        ShowMessage(Translate("Dữ liệu đã tồn tại!"), Utilities.NotifyType.Error)
                                        Exit Sub
                                    End If
                                    If rep.InsertAccountingAdjust(objAccounting) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        Refresh("InsertView")
                                        UpdateControlState()
                                        rgData.Rebind()
                                        ClearControlValue(txtEmployeeCode, txtEmployeeName, txtOrgName, txtTitleName, rdAdjustingDate, rnAdjustingX, txtNote, txtOrgSetName, hidEmpID, hidOrgID)
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                                Case CommonMessage.STATE_EDIT
                                    objAccounting.ID = rgData.SelectedValue
                                    If rep.ValidateAccountingAdjust(objAccounting) Then
                                        ShowMessage(Translate("Dữ liệu đã tồn tại!"), Utilities.NotifyType.Error)
                                        Exit Sub
                                    End If
                                    If rep.ModifyAccountingAdjust(objAccounting) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        Refresh("UpdateView")
                                        UpdateControlState()
                                        rgData.Rebind()
                                        ClearControlValue(txtEmployeeCode, txtEmployeeName, txtOrgName, txtTitleName, rdAdjustingDate, rnAdjustingX, txtNote, txtOrgSetName, hidEmpID, hidOrgID)
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                            End Select
                        End Using
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData')")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearControlValue(txtEmployeeCode, txtEmployeeName, txtOrgName, txtTitleName, rdAdjustingDate, rnAdjustingX, txtNote, txtOrgSetName, hidEmpID, hidOrgID)
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim store As New PayrollStoreProcedure
                    Dim dsData As DataSet = store.GET_ADJUSTING_IMPORT_DATA(LogHelper.CurrentUser.USERNAME.ToUpper, 1, False)
                    ''ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_TimeSheet_Monthly')", True)
                    ExportTemplate("Payroll\Import\Template_Import_DieuChinhConghoachtoan.xls", _
                                              dsData, Nothing, _
                                              "Template_Import_DieuChinhConghoachtoan" & Format(Date.Now, "yyyyMMdd"))
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
            End Select
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
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
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                If e.ActionName = CommonMessage.TOOLBARITEM_DELETE Then
                    CurrentState = CommonMessage.STATE_DELETE
                End If
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
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
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

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New PA_Accounting_AdjustingDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, _filter)
            If cboPeriodSearch.SelectedValue <> "" Then
                _filter.PERIOD_ID = cboPeriodSearch.SelectedValue
            End If
            If IsDate(rdFromDate.SelectedDate) Then
                _filter.FROM_DATE = rdFromDate.SelectedDate
            End If
            If IsDate(rdToDate.SelectedDate) Then
                _filter.TO_DATE = rdToDate.SelectedDate
            End If
            Dim _param = New ParamDTO With {.ORG_ID = 1,
                                            .IS_DISSOLVE = False}
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstObj As New List(Of PA_Accounting_AdjustingDTO)
            Using rep As New PayrollRepository
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetAccountingAdjusting(_filter, _param, Sorts).ToTable()
                    Else
                        Return rep.GetAccountingAdjusting(_filter, _param).ToTable()
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        lstObj = rep.GetAccountingAdjusting(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
                    Else
                        lstObj = rep.GetAccountingAdjusting(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
                    End If

                    rgData.VirtualItemCount = MaximumRows
                    rgData.DataSource = lstObj
                End If
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    Private Sub cboYear_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim objPeriod As List(Of ATPeriodDTO)
        Dim rep As New PayrollRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(cboPeriod)
            If cboYear.SelectedValue <> "" Then
                objPeriod = rep.GetPeriodbyYear(cboYear.SelectedValue)
                cboPeriod.DataSource = objPeriod
                cboPeriod.DataValueField = "ID"
                cboPeriod.DataTextField = "PERIOD_NAME"
                cboPeriod.DataBind()

                Dim lst = (From s In objPeriod Where s.MONTH = Date.Now.Month).FirstOrDefault
                If Not lst Is Nothing Then
                    cboPeriod.SelectedValue = lst.ID
                Else
                    cboPeriod.SelectedIndex = 0
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboYearSearch_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboYearSearch.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim objPeriod As List(Of ATPeriodDTO)
        Dim rep As New PayrollRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(cboPeriod)
            If cboYearSearch.SelectedValue <> "" Then
                objPeriod = rep.GetPeriodbyYear(cboYearSearch.SelectedValue)
                objPeriod.Add(New ATPeriodDTO With {.ID = Nothing, .PERIOD_NAME = Nothing})
                cboPeriodSearch.DataSource = objPeriod
                cboPeriodSearch.DataValueField = "ID"
                cboPeriodSearch.DataTextField = "PERIOD_NAME"
                cboPeriodSearch.DataBind()

                Dim lst = (From s In objPeriod Where s.MONTH = Date.Now.Month).FirstOrDefault
                If Not lst Is Nothing Then
                    cboPeriodSearch.SelectedValue = lst.ID
                Else
                    cboPeriodSearch.SelectedIndex = 0
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub btnFindEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee.Click
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstEmpID As New List(Of Decimal)
        Dim repN As New CommonBusiness.CommonBusinessClient
        Try
            ClearControlValue(hidEmpID, txtEmployeeCode, txtEmployeeName, txtOrgName, txtTitleName, hidEmpID)
            lstEmpID = ctrlFindEmployeePopup.SelectedEmployeeID
            If lstEmpID.Count <> 0 Then
                Dim item = repN.GetEmployeeID(lstEmpID(0))
                If item IsNot Nothing Then
                    If IsNumeric(item.ID) Then
                        hidEmpID.Value = item.ID
                    End If
                    txtEmployeeCode.Text = item.EMPLOYEE_CODE
                    txtEmployeeName.Text = item.FULLNAME_VN
                    txtTitleName.Text = item.TITLE_NAME
                    txtOrgName.Text = item.ORG_NAME
                End If
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue
                txtOrgSetName.Text = orgItem.NAME_VN
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindOrgPopup.CancelClicked, ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Private Sub rgData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rgData.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim objPeriod As List(Of ATPeriodDTO)
        Dim rep As New PayrollRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(txtEmployeeCode, txtEmployeeName, txtOrgName, txtTitleName, rdAdjustingDate, rnAdjustingX, txtNote, txtOrgSetName, hidEmpID, hidOrgID)
            If rgData.SelectedItems.Count = 1 Then
                Dim item As GridDataItem = rgData.SelectedItems(0)
                hidEmpID.Value = item.GetDataKeyValue("EMPLOYEE_ID")
                txtEmployeeCode.Text = item.GetDataKeyValue("EMPLOYEE_CODE")
                txtEmployeeName.Text = item.GetDataKeyValue("EMPLOYEE_NAME")
                txtTitleName.Text = item.GetDataKeyValue("TITLE_NAME")
                txtOrgName.Text = item.GetDataKeyValue("ORG_NAME")
                cboYear.SelectedValue = item.GetDataKeyValue("YEAR")
                objPeriod = rep.GetPeriodbyYear(item.GetDataKeyValue("YEAR"))
                cboPeriod.DataSource = objPeriod
                cboPeriod.DataValueField = "ID"
                cboPeriod.DataTextField = "PERIOD_NAME"
                cboPeriod.DataBind()
                cboPeriod.SelectedValue = item.GetDataKeyValue("PERIOD_ID")

                rdAdjustingDate.SelectedDate = item.GetDataKeyValue("ADJUSTING_DATE")
                txtOrgSetName.Text = item.GetDataKeyValue("ORG_SET_NAME")
                hidOrgID.Value = item.GetDataKeyValue("ORG_SET_ID")
                rnAdjustingX.Value = If(IsNumeric(item.GetDataKeyValue("ADJUSTING_X")), CDec(item.GetDataKeyValue("ADJUSTING_X")), Nothing)
                txtNote.Text = item.GetDataKeyValue("NOTE")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

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
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            TableMapping(dsDataPrepare.Tables(0))
            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("STT<>'""'").CopyToDataTable.Rows
                If String.IsNullOrEmpty(rows("ORG_CODE").ToString) AndAlso String.IsNullOrEmpty(rows("EMPLOYEE_CODE").ToString) Then Continue For
                Dim newRow As DataRow = dtData.NewRow
                newRow("STT") = rows("STT")
                newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
                newRow("EMPLOYEE_NAME") = rows("EMPLOYEE_NAME")
                newRow("ORG_CODE") = rows("ORG_CODE")
                newRow("ORG_ID") = rows("ORG_ID")
                newRow("ORG_NAME") = rows("ORG_NAME")
                newRow("COST_CENTER") = rows("COST_CENTER")
                newRow("YEAR") = rows("YEAR")
                newRow("PERIOD") = rows("PERIOD")
                newRow("PERIOD_ID") = rows("PERIOD_ID")
                newRow("ADJUSTING_DATE") = rows("ADJUSTING_DATE")
                newRow("ADJUSTING_X") = rows("ADJUSTING_X")
                newRow("NOTE") = rows("NOTE")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim sp As New PayrollStoreProcedure
                If sp.IMPORT_ADJUSTING(DocXml, LogHelper.GetUserLog().Username.ToUpper) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                    rgData.Rebind()
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly viec parse tu number sang boolean
    ''' </summary>
    ''' <param name="dValue"></param>
    ''' <remarks></remarks>
    Public Function ParseBoolean(ByVal dValue As String) As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If String.IsNullOrEmpty(dValue) Then
                Return False
            Else
                Return If(dValue = "1", True, False)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
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
        dtdata.AcceptChanges()
    End Sub

    Function loadToGrid() As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dtEmpTable As New DataTable
        Dim rep As New PayrollRepository
        Dim rep2 As New Attendance.AttendanceBusiness.AttendanceBusinessClient
        Dim store As New PayrollStoreProcedure
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim lstEmp As New List(Of String)
            dtError = dtData.Clone
            Dim iRow = 5
            Dim irowEm = 5
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)

                sError = "Chưa chọn cửa hàng"
                ImportValidate.EmptyValue("ORG_CODE", row, rowError, isError, sError)

                sError = "Chưa chọn năm"
                ImportValidate.EmptyValue("YEAR", row, rowError, isError, sError)

                sError = "Chưa chọn kỳ công"
                ImportValidate.EmptyValue("PERIOD", row, rowError, isError, sError)

                sError = "Chưa nhập ngày điều chỉnh"
                ImportValidate.EmptyValue("ADJUSTING_DATE", row, rowError, isError, sError)

                sError = "Chưa nhập số công điều chỉnh"
                ImportValidate.EmptyValue("ADJUSTING_X", row, rowError, isError, sError)

                If Not IsDBNull(row("EMPLOYEE_CODE")) Then
                    sError = "Nhân viên không tồn tại"
                    Dim checkEmp = rep2.GetEmployeeID(row("EMPLOYEE_CODE"), DateTime.Now)
                    If checkEmp Is Nothing OrElse checkEmp.Rows.Count = 0 Then
                        ImportValidate.IsValidDate("EMPLOYEE_NAME", row, rowError, isError, sError)
                    Else
                        row("EMPLOYEE_ID") = checkEmp.Rows(0)("ID")
                    End If
                End If


                If Not IsDBNull(row("ADJUSTING_X")) AndAlso Not String.IsNullOrEmpty(row("ADJUSTING_X")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("ADJUSTING_X", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("ADJUSTING_DATE")) AndAlso Not String.IsNullOrEmpty(row("ADJUSTING_DATE")) AndAlso Not IsDate(row("ADJUSTING_DATE")) Then
                    sError = "Sai định dạng"
                    ImportValidate.IsValidDate("ADJUSTING_DATE", row, rowError, isError, sError)
                End If

                If isError Then
                    ''rowError("ID") = iRow
                    rowError("STT") = row("STT").ToString
                    rowError("EMPLOYEE_NAME") = row("EMPLOYEE_NAME").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    If IsDBNull(rowError("ORG_CODE")) Then
                        rowError("ORG_CODE") = row("ORG_CODE").ToString
                    End If
                    If IsDBNull(rowError("EMPLOYEE_CODE")) Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    If IsDBNull(rowError("YEAR")) Then
                        rowError("YEAR") = row("YEAR").ToString
                    End If
                    If IsDBNull(rowError("PERIOD")) Then
                        rowError("PERIOD") = row("PERIOD").ToString
                    End If
                    dtError.Rows.Add(rowError)
                End If
                iRow = iRow + 1
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("ADJUSTING_ERR") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_DieuChinhConghoachtoan_Err')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            If isError Then
                Return False
            Else
                Return True
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

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

#End Region


End Class