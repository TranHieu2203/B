Imports System.Globalization
Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlInsWorkingBefore
    Inherits Common.CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup

    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim dtDataImp As DataTable

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Insurance/Module/Insurance/Business/" + Me.GetType().Name.ToString()

#Region "Property"

    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
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

    Private Property dtLogs As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLogs")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtLogs") = value
        End Set
    End Property

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("START_DATE", GetType(String))
                dt.Columns.Add("END_DATE", GetType(String))
                dt.Columns.Add("LEVEL_ID", GetType(String))
                dt.Columns.Add("COST", GetType(String))
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

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrg.CheckBoxes = TreeNodeTypes.None
            Me.rgData.SetFilter()
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub BindData()
        Try
            'Dim dic As New Dictionary(Of String, Control)
            'dic.Add("ID", hidID)
            'dic.Add("EMPLOYEE_ID", hidEmp)
            'dic.Add("EMPLOYEE_CODE", rtEMPLOYEE_CODE)
            'dic.Add("EMPLOYEE_NAME", rtEMPLOYEE_NAME)
            'dic.Add("FROM_MONTH", rdFROMMONTH)
            'dic.Add("TO_MONTH", rdTOMONTH)
            'dic.Add("INS_SALARY", rnINS_SALARY)
            'dic.Add("SALARY", rnSALARY)
            'dic.Add("TOXIC", rnTOXIC)
            'dic.Add("COEFFICIENT", rnCOEFFICIENT)
            'dic.Add("COMPANY", rtCOMPANY)
            'dic.Add("TITLE", rtTITTLE)
            'dic.Add("REMARK", rtREMARK)
            'dic.Add("ORG_NAME", rtORGNAME)
            'dic.Add("TITTLE_EMP_NAME", rtEMPLOYEE_TITTLE)
            'Utilities.OnClientRowSelectedChanged(rgData, dic)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SetGridFilter(rgData)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgData.AllowCustomPaging = True
            'rgData.ClientSettings.EnablePostBackOnRowClick = False
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Export)
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_CHECK,
                                                                   ToolbarIcons.Export,
                                                                   ToolbarAuthorize.Special1,
                                                                   Translate("Xuất file mẫu")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Special1,
                                                                     Translate("Nhập file mẫu")))
            CType(Me.MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

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
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub rgGridData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            LoadDataGrid()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New InsuranceRepository
        Dim objWorking As New INS_WORKING_BEFOREDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(hidID, hidEmp)
                    ResetForm()
                    rtEMPLOYEE_NAME.Focus()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    rtEMPLOYEE_CODE.Focus()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim gID As Decimal
                    With objWorking
                        .EMPLOYEE_ID = hidEmp.Value
                        .FROM_MONTH = rdFROMMONTH.SelectedDate
                        .TO_MONTH = rdTOMONTH.SelectedDate
                        .COMPANY = rtCOMPANY.Text
                        .TITLE = rtTITTLE.Text
                        .INS_SALARY = rnINS_SALARY.Value
                        '.SALARY = rnSALARY.Value
                        '.TOXIC = rnTOXIC.Value
                        '.COEFFICIENT = rnCOEFFICIENT.Value
                        .REMARK = rtREMARK.Text
                    End With
                    Select Case CurrentState
                        Case CommonMessage.STATE_NEW
                            If Not rep.ValidateWorkingBefore(objWorking) Then
                                ShowMessage("Dữ liệu đã tồn tại", NotifyType.Warning)
                                Exit Sub
                            End If
                            If rep.InsertInsWorkingBefore(objWorking, gID) Then
                                CurrentState = CommonMessage.STATE_NORMAL
                                Refresh("InsertView")
                                ResetForm()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                        Case CommonMessage.STATE_EDIT
                            objWorking.ID = Decimal.Parse(hidID.Value)
                            If Not rep.ValidateWorkingBefore(objWorking) Then
                                ShowMessage("Dữ liệu đã tồn tại", NotifyType.Warning)
                                Exit Sub
                            End If
                            If rep.ModifyInsWorkingBefore(objWorking, gID) Then
                                CurrentState = CommonMessage.STATE_NORMAL
                                Refresh("UpdateView")
                                ResetForm()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                    End Select
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ResetForm()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = LoadDataGrid(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtDatas, "Infomation_Ins")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_CHECK
                    Template_Import()
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Import_INS_SUNCARE&orgid=" & ctrlOrg.CurrentValue & "');", True)
                Case "IMPORT_TEMP"
                    ctrlUpload.Show()
            End Select
            UpdateControlState()
            ChangeToolbarState()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Import_Data()
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Private Sub Import_Data()
        Try
            Dim rep As New InsuranceRepository
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
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
            TableMapping(ds.Tables(0))

            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then

                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.INPORT_InsWorkingBefore(DocXml) Then
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
            ShowMessage(Translate("Có lỗi trong quá trình import"), Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        'Try
        Dim rep As New InsuranceRepository
        Dim rep_ser As New InsuranceBusinessClient
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "STT"
        dtTemp.Columns(1).ColumnName = "EMPLOYEE_CODE"
        dtTemp.Columns(2).ColumnName = "FROM_MONTH"
        dtTemp.Columns(3).ColumnName = "TO_MONTH"
        dtTemp.Columns(4).ColumnName = "COMPANY"
        dtTemp.Columns(5).ColumnName = "TITLE"
        'dtTemp.Columns(6).ColumnName = "COEFFICIENT"
        dtTemp.Columns(6).ColumnName = "INS_SALARY"
        'dtTemp.Columns(8).ColumnName = "SALARY"
        'dtTemp.Columns(9).ColumnName = "TOXIC"
        dtTemp.Columns(7).ColumnName = "REMARK"

        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(1).Delete()
        Dim a = dtTemp.Rows.Count
        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        Dim dsEMP As DataTable
        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            dtLogs.Columns.Add("ID", GetType(Integer))
            dtLogs.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtLogs.Columns.Add("DISCIPTION", GetType(String))
        End If
        dtLogs.Clear()

        'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
        Dim rowDel As DataRow
        For i As Integer = 3 To dtTemp.Rows.Count - 1
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

            ' Nhân viên k có trong hệ thống
            If rep.CHECK_EMPLOYEE(rows("EMPLOYEE_CODE")) = 0 Then
                newRow("DISCIPTION") = "Mã nhân viên - Không tồn tại,"
                _error = False
            End If
            If Not IsDBNull(rows("FROM_MONTH")) Then
                Dim frommonth As String = "01/" & rows("FROM_MONTH")
                If CheckDate(frommonth) = False Then
                    rows("FROM_MONTH") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Từ tháng - Không đúng định dạng,"
                    _error = False
                Else
                    rows("FROM_MONTH") = frommonth
                End If
            Else
                newRow("DISCIPTION") = "Từ tháng - Không được để trống,"
                _error = False
            End If
            If Not IsDBNull(rows("TO_MONTH")) Then
                Dim Tomonth As String = "01/" & rows("TO_MONTH")
                If CheckDate(Tomonth) = False Then
                    rows("TO_MONTH") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Đến tháng - Không đúng định dạng,"
                    _error = False
                Else
                    rows("TO_MONTH") = Tomonth
                End If
            Else
                newRow("DISCIPTION") = "Đến tháng - Không được để trống,"
                _error = False
            End If


            If _error = False Then
                dtLogs.Rows.Add(newRow)
                count = count + 1
                _error = True
            End If
        Next
        dtTemp.AcceptChanges()
        'Catch ex As Exception

        'End Try
    End Sub

    Private Function CheckDate(ByVal value As String) As Boolean
        Dim dateCheck As Boolean
        Dim result As Date

        If value = "" Or value = "&nbsp;" Then
            value = ""
            Return True
        End If

        Try
            dateCheck = DateTime.TryParseExact(value, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result)
            Return dateCheck
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgData.CurrentPageIndex = 0
            rgData.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                'datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Function & Sub"


    Private Function LoadDataGrid(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New InsuranceRepository
        Dim obj As New INS_WORKING_BEFOREDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer = 0
            SetValueObjectByRadGrid(rgData, obj)
            Dim lstSource As List(Of INS_WORKING_BEFOREDTO)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            If Not isFull Then
                If Sorts IsNot Nothing Then
                    lstSource = rep.GetInsWorkingBefore(obj, Utilities.ObjToInt(ctrlOrg.CurrentValue), "", rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                Else
                    lstSource = rep.GetInsWorkingBefore(obj, Utilities.ObjToInt(ctrlOrg.CurrentValue), "", rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                End If
                rgData.DataSource = lstSource
                rgData.MasterTableView.VirtualItemCount = MaximumRows
            Else
                If Sorts IsNot Nothing Then
                    Return rep.GetInsWorkingBefore(obj, Utilities.ObjToInt(ctrlOrg.CurrentValue), "", , , , Sorts).ToTable
                Else
                    Return rep.GetInsWorkingBefore(obj, Utilities.ObjToInt(ctrlOrg.CurrentValue), "").ToTable
                End If

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New InsuranceRepository
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                    End If
            End Select
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    rtCOMPANY.Enabled = False
                    rdFROMMONTH.Enabled = False
                    rdTOMONTH.Enabled = False
                    rtTITTLE.Enabled = False
                    rtREMARK.Enabled = False
                    'rnCOEFFICIENT.Enabled = False
                    rnINS_SALARY.Enabled = False
                    'rnSALARY.Enabled = False
                    'rnTOXIC.Enabled = False
                    rtEMPLOYEE_CODE.Enabled = False
                    btnFindEmployee.Enabled = False
                    'Utilities.EnabledGridNotPostback(rgData, True)

                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    rtCOMPANY.Enabled = True
                    rdFROMMONTH.Enabled = True
                    rdTOMONTH.Enabled = True
                    rtTITTLE.Enabled = True
                    rtREMARK.Enabled = True
                    'rnCOEFFICIENT.Enabled = True
                    rnINS_SALARY.Enabled = True
                    'rnSALARY.Enabled = True
                    'rnTOXIC.Enabled = True
                    rtEMPLOYEE_CODE.Enabled = True
                    btnFindEmployee.Enabled = True
                    'Utilities.EnabledGridNotPostback(rgData, False)
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.Delete_InsWorkingBefore(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub UpdateToolbarState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    'Utilities.EnabledGridNotPostback(rgData, True)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True

                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False

                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = True

                Case CommonMessage.STATE_NEW
                    'Utilities.EnabledGridNotPostback(rgData, False)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False

                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True

                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = False


                Case CommonMessage.STATE_EDIT
                    'Utilities.EnabledGridNotPostback(rgData, False)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False

                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True

                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = False

                Case CommonMessage.STATE_DELETE

                Case "Nothing"
                    'Utilities.EnabledGridNotPostback(rgData, True)

            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub Template_Import()
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Try
            Dim configPath As String = ConfigurationManager.AppSettings("PathImportFolder")
            'Dim dsData As DataSet = rep.GET_INS_HEATH_IMPORT()
            'dsData.Tables(0).TableName = "Table"
            'dsData.Tables(1).TableName = "Table1"
            'dsData.Tables(2).TableName = "Table2"
            ExportTemplate("Insurance\Import\Import_QTBaohiemtruoc.xls",
                                  Nothing, Nothing, "Import_QTBaohiemtruoc" & Format(Date.Now, "yyyymmdd"))


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

            'cau hinh lai duong dan tren server
            'filePath = sReportFileName

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
    Private Sub ResetForm()
        Try
            'rnTOXIC, rnCOEFFICIENT, rnSALARY,
            ClearControlValue(rtEMPLOYEE_CODE, rtEMPLOYEE_NAME, rtEMPLOYEE_TITTLE, rtORGNAME, rtCOMPANY, rtTITTLE, rtREMARK, rnINS_SALARY, rdFROMMONTH, rdTOMONTH)
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
            End Select

            UpdateControlState()
            Select Case sender.ID
                Case btnFindEmployee.ID
                    ctrlFindEmployeePopup.Show()
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New InsuranceRepository
        Dim rep_client As New InsuranceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                hidEmp.Value = lstCommonEmployee(0).EMPLOYEE_ID
                rtEMPLOYEE_CODE.Text = lstCommonEmployee(0).EMPLOYEE_CODE.ToString
                rtORGNAME.Text = lstCommonEmployee(0).ORG_NAME.ToString
                rtEMPLOYEE_TITTLE.Text = lstCommonEmployee(0).TITLE_NAME.ToString
                rtEMPLOYEE_NAME.Text = lstCommonEmployee(0).FULLNAME_VN.ToString
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rgData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rgData.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try

            If rgData.SelectedItems.Count() = 1 Then
                Dim item As GridDataItem = rgData.SelectedItems(0)
                hidID.Value = item.GetDataKeyValue("ID")
                If IsNumeric(item.GetDataKeyValue("EMPLOYEE_ID")) Then
                    hidEmp.Value = item.GetDataKeyValue("EMPLOYEE_ID")
                End If
                rtEMPLOYEE_CODE.Text = item.GetDataKeyValue("EMPLOYEE_CODE")
                rtEMPLOYEE_NAME.Text = item.GetDataKeyValue("EMPLOYEE_NAME")
                If IsDate(item.GetDataKeyValue("FROM_MONTH")) Then
                    rdFROMMONTH.SelectedDate = CDate(item.GetDataKeyValue("FROM_MONTH"))
                End If
                If IsDate(item.GetDataKeyValue("TO_MONTH")) Then
                    rdTOMONTH.SelectedDate = CDate(item.GetDataKeyValue("TO_MONTH"))
                End If
                If IsNumeric(item.GetDataKeyValue("INS_SALARY")) Then
                    Dim A = item.GetDataKeyValue("INS_SALARY")
                    rnINS_SALARY.Value = CDec(item.GetDataKeyValue("INS_SALARY"))
                End If
                'If IsNumeric(item.GetDataKeyValue("SALARY")) Then
                '    rnSALARY.Value = CDec(item.GetDataKeyValue("SALARY"))
                'End If
                'If IsNumeric(item.GetDataKeyValue("TOXIC")) Then
                '    rnTOXIC.Value = CDec(item.GetDataKeyValue("TOXIC"))
                'End If
                'If IsNumeric(item.GetDataKeyValue("COEFFICIENT")) Then
                '    rnCOEFFICIENT.Value = CDec(item.GetDataKeyValue("COEFFICIENT"))
                'End If
                rtCOMPANY.Text = item.GetDataKeyValue("COMPANY")
                rtTITTLE.Text = item.GetDataKeyValue("TITLE")
                rtREMARK.Text = item.GetDataKeyValue("REMARK")
                rtORGNAME.Text = item.GetDataKeyValue("ORG_NAME")
                rtEMPLOYEE_TITTLE.Text = item.GetDataKeyValue("TITTLE_EMP_NAME")
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class