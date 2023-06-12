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

Public Class ctrlInsManagerHeathIns
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

    ''' <lastupdate>
    ''' 06/09/2017 14:00
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

            'Refresh()
            'UpdateControlState()
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
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize

            Dim startTime As DateTime = DateTime.UtcNow
            SetGridFilter(rgData)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit,
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
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrg.CheckBoxes = TreeNodeTypes.None
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
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
        'Dim gID As Decimal
        ' Dim objData As New INS_SUN_CARE_DTO
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
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
            ChangeToolbarState()
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
    ''' Xu ly su kien click cho button ctrUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
                'Dim count As Integer = ds.Tables(0).Columns.Count - 6
                'For i = 0 To count
                '    If ds.Tables(0).Columns(i).ColumnName.Contains("Column") Then
                '        ds.Tables(0).Columns.RemoveAt(i)
                '        i = i - 1
                '    End If
                'Next

                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.INPORT_MANAGER_SUN_CARE(DocXml) Then
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
        dtTemp.Columns(0).ColumnName = "EMPLOYEE_CODE"
        dtTemp.Columns(2).ColumnName = "YEAR"
        dtTemp.Columns(13).ColumnName = "INS_CONTRACT_ID"
        dtTemp.Columns(14).ColumnName = "INS_CONTRACT_DE_ID"
        dtTemp.Columns(16).ColumnName = "FAMYLI_ID"
        dtTemp.Columns(15).ColumnName = "DT_CHITRA"

        dtTemp.Columns(7).ColumnName = "JOIN_DATE"
        dtTemp.Columns(8).ColumnName = "EFFECT_DATE"
        dtTemp.Columns(9).ColumnName = "REDUCE_DATE"

        dtTemp.Columns(10).ColumnName = "DATE_RECEIVE_MONEY"
        dtTemp.Columns(11).ColumnName = "EMP_RECEIVE_MONEY"
        dtTemp.Columns(12).ColumnName = "NOTES"
        dtTemp.Columns.Add("MONEY_INS", GetType(String))
        dtTemp.Columns.Add("REFUND", GetType(String))
        dtTemp.Columns.Add("CHECK_BHNT", GetType(String))
        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(1).Delete()
        dtTemp.Rows(2).Delete()
        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        Dim dsEMP As DataTable
        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            dtLogs.Columns.Add("ID", GetType(Integer))
            dtLogs.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtLogs.Columns.Add("YEAR", GetType(String))
            dtLogs.Columns.Add("INS_CONTRACT_NO", GetType(String))
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

            If Not IsDBNull(rows("JOIN_DATE")) Then
                If CheckDate(rows("JOIN_DATE")) = False Then
                    rows("JOIN_DATE") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày tham gia - Không đúng định dạng,"
                    _error = False
                End If
            End If
            rows("MONEY_INS") = 0
            rows("REFUND") = 0
            rows("CHECK_BHNT") = 0
            If IsNumeric(rows("FAMYLI_ID")) Then
                rows("CHECK_BHNT") = -1
            End If
            If IsDBNull(rows("EFFECT_DATE")) OrElse CheckDate(rows("EFFECT_DATE")) = False Then
                rows("EFFECT_DATE") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày hiệu lực - Không đúng định dạng,"
                _error = False
            End If

            If Not IsDBNull(rows("DATE_RECEIVE_MONEY")) AndAlso CheckDate(rows("DATE_RECEIVE_MONEY")) = False Then
                rows("DATE_RECEIVE_MONEY") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày nhận tiền - Không đúng định dạng,"
                _error = False
            End If
            Dim SoTien As Decimal = 0
            If IsDBNull(rows("INS_CONTRACT_DE_ID")) Then
                rows("INS_CONTRACT_DE_ID") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Chương trình bảo hiểm - bắt buộc nhập,"
                _error = False
            Else
                If IsNumeric(rows("INS_CONTRACT_ID")) Then
                    Dim lst_Program = rep_ser.GetINS_LIST_CONTRACT_DETAIL_BY_ID_COMBOBOX(Utilities.ObjToInt(rows("INS_CONTRACT_ID")))

                    Dim row As DataRow = lst_Program.Select("ID=" & Utilities.ObjToInt(rows("INS_CONTRACT_DE_ID"))).FirstOrDefault()
                    If Not row Is Nothing Then
                        If IsNumeric(row.Item("MONEY_INS")) Then
                            SoTien = row.Item("MONEY_INS")
                        Else
                            SoTien = 0
                        End If
                    Else
                        rows("INS_CONTRACT_DE_ID") = "NULL"
                        newRow("DISCIPTION") = newRow("DISCIPTION") + "Chương trình bảo hiểm - không tồn tại,"
                        _error = False
                    End If

                End If
            End If
            Dim HDTuNgay As Date
            Dim HDDenNgay As Date
            If IsNumeric(rows("INS_CONTRACT_ID")) = False Then
                rows("INS_CONTRACT_ID") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Số HĐ bảo hiểm - bắt buộc nhập,"
                _error = False
            Else
                Dim dtCost As New INS_LIST_CONTRACTDTO
                dtCost = rep.GetINS_LIST_CONTRACT_BY_ID(Utilities.ObjToInt(rows("INS_CONTRACT_ID")))
                If dtCost Is Nothing Then
                    rows("INS_CONTRACT_ID") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Số HĐ bảo hiểm - Không tồn tại,"
                    _error = False
                Else
                    HDTuNgay = dtCost.START_DATE
                    HDDenNgay = dtCost.EXPIRE_DATE
                    If IsDate(HDTuNgay) AndAlso IsDate(HDDenNgay) AndAlso IsDate(rows("EFFECT_DATE")) Then
                        Dim dEffect As Date = CDate(rows("EFFECT_DATE"))
                        Dim dExpire As Date = HDDenNgay
                        Dim Songaysudung = DateDiff(DateInterval.Day, dEffect, dExpire) + 1
                        If Songaysudung >= 0 Then
                            Dim SotienBHTang As Decimal = (SoTien / 365 * Songaysudung)
                            rows("MONEY_INS") = CDec(SotienBHTang).ToString("n0")
                        Else
                            rows("MONEY_INS") = 0.ToString
                        End If
                    End If
                End If
            End If
            If Not IsDBNull(rows("REDUCE_DATE")) AndAlso IsDate(rows("REDUCE_DATE")) = False Then
                rows("REDUCE_DATE") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày báo giảm - Không đúng định dạng,"
                _error = False
            Else
                If IsDate(rows("REDUCE_DATE")) Then
                    Dim dEnd_date As Date = CDate(rows("REDUCE_DATE"))
                    If IsDate(rows("EFFECT_DATE")) Then
                        Dim dEffect As Date = CDate(rows("EFFECT_DATE"))
                        Dim SoNgaySuDung = DateDiff("d", dEffect, dEnd_date) + 1
                        Dim SotienBHHoanLai As Decimal = SoTien - (SoTien / 365 * SoNgaySuDung)
                        rows("REFUND") = CDec(SotienBHHoanLai).ToString("n0")
                    End If
                End If
            End If
            If IsDate(HDTuNgay) AndAlso IsDate(HDDenNgay) AndAlso IsDate(rows("EFFECT_DATE")) Then
                If CDate(rows("EFFECT_DATE")) < CDate(HDTuNgay) Or CDate(rows("EFFECT_DATE")) > CDate(HDDenNgay) Then
                    rows("EFFECT_DATE") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày hiệu lực - phải nằm trong thời gian HĐBH"
                    _error = False
                End If
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


    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <remarks></remarks>
    'Function loadToGrid() As Boolean
    '    Dim dtError As New DataTable("ERROR")

    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If dtData.Rows.Count = 0 Then
    '            ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
    '            Return False
    '        End If
    '        Dim rowError As DataRow
    '        Dim isError As Boolean = False
    '        Dim sError As String = String.Empty
    '        Dim rep As New InsuranceRepository
    '        Dim lstEmp As New List(Of String)
    '        dtError = dtData.Clone
    '        Dim irow = 7
    '        For Each row As DataRow In dtData.Rows
    '            isError = False
    '            rowError = dtError.NewRow
    '            sError = "Chưa nhập mã nhân viên"
    '            ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)

    '            If row("START_DATE") <> "" Then
    '                ImportValidate.IsValidDate("START_DATE", row, rowError, isError, sError)
    '            End If
    '            If row("END_DATE") <> "" Then
    '                ImportValidate.IsValidDate("END_DATE", row, rowError, isError, sError)
    '            End If

    '            sError = "Nhóm hưởng bảo hiểm"
    '            ImportValidate.IsValidList("LEVEL_NAME", "LEVEL_ID", row, rowError, isError, sError)

    '            If row("COST") <> "" Then
    '                sError = "Mức chi phí"
    '                ImportValidate.IsValidNumber("COST", row, rowError, isError, sError)
    '            End If

    '            If isError Then
    '                rowError("STT") = irow
    '                If rowError("EMPLOYEE_CODE").ToString = "" Then
    '                    rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
    '                End If
    '                rowError("ORG_NAME") = row("ORG_NAME").ToString
    '                rowError("TITLE_NAME") = row("TITLE_NAME").ToString
    '                rowError("STAFF_RANK_NAME") = row("STAFF_RANK_NAME").ToString
    '                dtError.Rows.Add(rowError)
    '            End If
    '            irow = irow + 1
    '        Next
    '        If dtError.Rows.Count > 0 Then
    '            dtError.TableName = "DATA"
    '            Session("EXPORTREPORT") = dtError
    '            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportInfoIns_error')", True)
    '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
    '            Return False
    '        Else
    '            Return True
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method,
    '                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Function

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Insert data vao database
    ''' </summary>
    ''' <remarks></remarks>
    'Function SaveData() As Boolean
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        Dim rep As New InsuranceRepository
    '        Dim obj As INS_SUN_CARE_DTO
    '        Dim gId As New Decimal?
    '        gId = 0
    '        For Each row As DataRow In dtDataImp.Rows
    '            obj = New INS_SUN_CARE_DTO
    '            obj.EMPLOYEE_ID = CDec(row("EMPLOYEE_ID"))
    '            obj.THOIDIEMHUONG = ToDate(row("THOIDIEMHUONG"))
    '            obj.START_DATE = ToDate(row("START_DATE"))
    '            obj.END_DATE = ToDate(row("END_DATE"))
    '            obj.LEVEL_ID = CDec(row("LEVEL_ID"))
    '            obj.COST = CDec(row("COST"))
    '            obj.NOTE = row("NOTE")
    '            rep.InsertSunCare(obj, gId)
    '        Next
    '        Return True
    '        _mylog.WriteLog(_mylog._info, _classPath, method,
    '                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    '    Return False
    'End Function

    ''' <lastupdate>
    ''' 06/09/2017 14:00
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

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien SelectedNodeChanged cua control ctrlOrg
    ''' Xet lai cac thiet lap trang thai cho grid rgRegisterLeave
    ''' Bind lai du lieu cho rgRegisterLeave
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Call LoadDataGrid()
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
    ''' Xu ly su kien click cho button btnSearch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Call LoadDataGrid()
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
    ''' xu ly su kien ItemDataBound cua grid rgData
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("PHONG_BAN").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
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

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Private Function LoadDataGrid(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New InsuranceRepository
        Dim obj As New INS_HEALTH_INSURANCE_DTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer = 0
            SetValueObjectByRadGrid(rgData, obj)
            Dim lstSource As List(Of INS_HEALTH_INSURANCE_DTO)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim ISTER As Integer?
            Dim ISCLAIM As Integer?
            If chkChecknghiViec.Checked Then
                ISTER = 0
            Else
                ISTER = Nothing
            End If
            If chkClaim.Checked Then
                ISCLAIM = 0
            Else
                ISCLAIM = Nothing
            End If

            If Not isFull Then
                If Sorts IsNot Nothing Then

                    lstSource = rep.GET_INS_HEALTH_INSURANCE(Utilities.ObjToInt(ctrlOrg.CurrentValue), ISTER, ISCLAIM, True, rdJoinFrom.SelectedDate, rdJoinTo.SelectedDate, rdEffectFrom.SelectedDate, rdEffectTo.SelectedDate, rdReduceFrom.SelectedDate, rdReduceTo.SelectedDate, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                Else
                    lstSource = rep.GET_INS_HEALTH_INSURANCE(Utilities.ObjToInt(ctrlOrg.CurrentValue), ISTER, ISCLAIM, True, rdJoinFrom.SelectedDate, rdJoinTo.SelectedDate, rdEffectFrom.SelectedDate, rdEffectTo.SelectedDate, rdReduceFrom.SelectedDate, rdReduceTo.SelectedDate, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                End If
                rgData.DataSource = lstSource
                rgData.MasterTableView.VirtualItemCount = MaximumRows
                rgData.CurrentPageIndex = rgData.MasterTableView.CurrentPageIndex
            Else
                If Sorts IsNot Nothing Then
                    Return rep.GET_INS_HEALTH_INSURANCE(Utilities.ObjToInt(ctrlOrg.CurrentValue), ISTER, ISCLAIM, True, rdJoinFrom.SelectedDate, rdJoinTo.SelectedDate, rdEffectFrom.SelectedDate, rdEffectTo.SelectedDate, rdReduceFrom.SelectedDate, rdReduceTo.SelectedDate, , , , Sorts).ToTable
                Else
                    Return rep.GET_INS_HEALTH_INSURANCE(Utilities.ObjToInt(ctrlOrg.CurrentValue), ISTER, ISCLAIM, True, rdJoinFrom.SelectedDate, rdJoinTo.SelectedDate, rdEffectFrom.SelectedDate, rdEffectTo.SelectedDate, rdReduceFrom.SelectedDate, rdReduceTo.SelectedDate).ToTable
                End If

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Update trang thai cua cac control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New InsuranceRepository
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.Delete_INS_HEALTH_INSURANCE(lstDeletes) Then
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
    'Private Sub GetInformationLists()
    '    Dim rep As New InsuranceRepository
    '    Dim dsDB As New DataSet
    '    Dim dtTonGiao, dtMoiQH, dtQuocGia, dtTinh, dtHuyen, dtXa, dtChuyenNganh,
    '        dtChuyenMon, dtHonNhan, dtDanToc, dtLogo As New DataTable
    '    Try
    '        dsDB = rep.GET_ALL_LIST()
    '        If dsDB.Tables.Count > 0 Then

    '            'Ton Giao
    '            If dsDB.Tables(0) IsNot Nothing AndAlso dsDB.Tables(0).Rows.Count > 0 Then
    '                dtTonGiao = dsDB.Tables(0)
    '            End If

    '            'Moi Quan He
    '            If dsDB.Tables(1) IsNot Nothing AndAlso dsDB.Tables(1).Rows.Count > 0 Then
    '                dtMoiQH = dsDB.Tables(1)
    '            End If

    '            'Quoc Gia
    '            If dsDB.Tables(2) IsNot Nothing AndAlso dsDB.Tables(2).Rows.Count > 0 Then
    '                dtQuocGia = dsDB.Tables(2)
    '            End If


    '            ExportTemplate("Recruitment\Import\Import_Ungvien_Template.xls", dtTonGiao, dtMoiQH, dtQuocGia, dtTinh, dtHuyen, dtXa, dtChuyenNganh, dtChuyenMon, dtHonNhan, dtDanToc, dtLogo, "Import_UngVien")
    '        End If

    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    Private Sub Template_Import()
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Try
            Dim configPath As String = ConfigurationManager.AppSettings("PathImportFolder")
            Dim dsData As DataSet = rep.GET_INS_HEATH_IMPORT()
            dsData.Tables(0).TableName = "Table"
            dsData.Tables(1).TableName = "Table1"
            dsData.Tables(2).TableName = "Table2"
            ExportTemplate("Insurance\Import\Import_INS_SUNCARE.xls",
                                  dsData, Nothing, "Import_ManagerHeath" & Format(Date.Now, "yyyymmdd"))


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
#End Region

End Class