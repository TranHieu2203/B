Imports System.IO
Imports Aspose.Cells
Imports Attendance
Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_Employee_QuyLuong
    Inherits Common.CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Payroll/Module/Payroll/List/" + Me.GetType().Name.ToString()
    Dim log As New UserLog
#Region "Property"

    Property NotEmpQuyLuong As List(Of EmpQuyLuongDTO)
        Get
            Return ViewState(Me.ID & "_NotEmpQuyLuong")
        End Get
        Set(ByVal value As List(Of EmpQuyLuongDTO))
            ViewState(Me.ID & "_NotEmpQuyLuong") = value
        End Set
    End Property

    Property EmpQuyLuong As List(Of EmpQuyLuongDTO)
        Get
            Return ViewState(Me.ID & "_EmpQuyLuong")
        End Get
        Set(ByVal value As List(Of EmpQuyLuongDTO))
            ViewState(Me.ID & "_EmpQuyLuong") = value
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


    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Public Property SelectedItemEmpNot As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemEmpNot") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemEmpNot") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemEmpNot")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemEmpNot") = value
        End Set
    End Property

    Public Property SelectedItemEmp As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemEmp") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemEmp") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemEmp")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemEmp") = value
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
            rgNotEmp.SetFilter()
            rgEmp.SetFilter()

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
            'If Not IsPostBack Then
            '    ViewConfig(RadPane1)
            '    GirdConfig(rgData)
            'End If
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
            'Me.MainToolBar = tbarSalaryGroups
            'Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
            '                           ToolbarItem.Edit,
            '                           ToolbarItem.Save, ToolbarItem.Cancel,
            '                           ToolbarItem.Delete, ToolbarItem.Export)

            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
            '                                                         ToolbarIcons.Export,
            '                                                         ToolbarAuthorize.Import,
            '                                                         Translate("Xuất file mẫu")))
            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
            '                                                         ToolbarIcons.Import,
            '                                                         ToolbarAuthorize.Import,
            '                                                         Translate("Nhập file mẫu")))


            'CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            'CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
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
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        'rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        'rgData.CurrentPageIndex = 0
                        'rgData.MasterTableView.SortExpressions.Clear()
                        'rgData.Rebind()
                    Case "Cancel"
                        'rgData.MasterTableView.ClearSelectedItems()
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
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim obj As New EmpQuyLuongDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgNotEmp, obj)
            Dim Sorts As String = rgNotEmp.MasterTableView.SortExpressions.GetSortString()

            If cboPeriodSearch.SelectedValue <> "" Then
                obj.PERIOD_ID = cboPeriodSearch.SelectedValue
            End If
            If cboDonVi.SelectedValue <> "" Then
                obj.DONVI_QUYLUONG_ID = cboDonVi.SelectedValue
            End If


            Dim _param = New PA_ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                           .IS_DISSOLVE = ctrlOrg.IsDissolve}

            Using rep As New PayrollRepository
                If Sorts IsNot Nothing Then
                    NotEmpQuyLuong = rep.GetEmpNotQuyLuong(obj, _param, rgNotEmp.CurrentPageIndex, rgNotEmp.PageSize, MaximumRows, Sorts)
                Else
                    NotEmpQuyLuong = rep.GetEmpNotQuyLuong(obj, _param, rgNotEmp.CurrentPageIndex, rgNotEmp.PageSize, MaximumRows)
                End If
                ntxtNotEmp.Value = MaximumRows
                rgNotEmp.DataSource = NotEmpQuyLuong
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function



    Protected Function CreateDataFilter1(Optional ByVal isFull As Boolean = False) As DataTable
        Dim obj As New EmpQuyLuongDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgEmp, obj)
            Dim Sorts As String = rgEmp.MasterTableView.SortExpressions.GetSortString()

            If cboPeriodSearch.SelectedValue <> "" Then
                obj.PERIOD_ID = cboPeriodSearch.SelectedValue
            End If

            If cboDonVi.SelectedValue <> "" Then
                obj.DONVI_QUYLUONG_ID = cboDonVi.SelectedValue
            End If

            Dim _param = New PA_ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                           .IS_DISSOLVE = ctrlOrg.IsDissolve}

            Using rep As New PayrollRepository
                If Sorts IsNot Nothing Then
                    EmpQuyLuong = rep.GetEmpQuyLuong(obj, _param, rgNotEmp.CurrentPageIndex, rgNotEmp.PageSize, MaximumRows, Sorts)
                Else
                    EmpQuyLuong = rep.GetEmpQuyLuong(obj, _param, rgNotEmp.CurrentPageIndex, rgNotEmp.PageSize, MaximumRows)
                End If
                ntxtEmp.Value = MaximumRows
                rgEmp.DataSource = EmpQuyLuong
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
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
            'rgContract.CurrentPageIndex = 0
            'rgContract.MasterTableView.SortExpressions.Clear()
            'rgContract.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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
                Case CommonMessage.STATE_NORMAL
                Case CommonMessage.STATE_EDIT
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    'For idx = 0 To rgData.SelectedItems.Count - 1
                    '    Dim item As GridDataItem = rgData.SelectedItems(idx)
                    '    lstDeletes.Add(item.GetDataKeyValue("ID"))
                    'Next
                    Using rep As New PayrollRepository
                        If rep.DeleteSalaryGroup(lstDeletes) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                        End If
                    End Using
            End Select
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
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
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim dic As New Dictionary(Of String, Control)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim lsData As List(Of AT_PERIODDTO)
            Dim rep As New AttendanceRepository
            lsData = rep.LOAD_PERIODYEAR()
            FillRadCombobox(cboYearSearch, lsData, "YEAR", "YEAR", False)

            Using rep1 As New PayrollRepository
                FillRadCombobox(cboDonVi, rep1.LOAD_DONVI_QUYLUONG(), "NAME", "ID", False)
            End Using

            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <summary>
    ''' Event SelectedIndexChange combobox năm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboYearSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYearSearch.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtData As List(Of AT_PERIODDTO)
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Try
            period.ORG_ID = 1
            period.YEAR = Decimal.Parse(cboYearSearch.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            FillRadCombobox(cboPeriodSearch, dtData, "PERIOD_T", "PERIOD_ID", True)

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        Finally
            rep.Dispose()
            period = Nothing
            startTime = Nothing
            dtData = Nothing
        End Try

    End Sub
    Private Sub cboDonVi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDonVi.SelectedIndexChanged
        Try
            rgEmp.Rebind()
            rgNotEmp.Rebind()
        Catch ex As Exception

        End Try
    End Sub

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
        Dim objSalaryGroup As New SalaryQuyLuongDTO
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    'If rgData.SelectedItems.Count = 0 Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    'Dim lstID As New List(Of Decimal)
                    'For idx = 0 To rgData.SelectedItems.Count - 1
                    '    Dim item As GridDataItem = rgData.SelectedItems(idx)
                    '    lstID.Add(item.GetDataKeyValue("ID"))
                    'Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    'Dim dtData As DataTable
                    'Using xls As New ExcelCommon
                    '    dtData = CreateDataFilter(True)
                    '    If dtData.Rows.Count > 0 Then
                    '        rgData.ExportExcel(Server, Response, dtData, "SalaryGroup")
                    '    Else
                    '        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                    '    End If
                    'End Using

                Case "EXPORT_TEMP"
                    Dim tempPath = "~/ReportTemplates//Payroll//Import//Import_QuyLuong.xls"
                    If Not File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath))) Then
                        ' Mẫu báo cáo không tồn tại
                        ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
                        Exit Sub
                    End If

                    Using rep As New PayrollRepository
                        Dim dsdata As DataSet = rep.GET_IMPORT_QUYLUONG()
                        ExportTemplate("Payroll/Import/Import_QuyLuong.xls", dsdata, Nothing, _
                                              "IMPORT_QuyLuong" & Format(Date.Now, "yyyyMMdd"))
                    End Using
                Case "IMPORT_TEMP"
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
            If e.ActionName = "INSERT" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim obj As New EmpQuyLuongDTO
                obj.PERIOD_ID = cboPeriodSearch.SelectedValue
                obj.DONVI_QUYLUONG_ID = cboDonVi.SelectedValue
                Using rep As New PayrollRepository
                    rep.InsertEmpQuyLuong(obj, SelectedItemEmpNot)
                End Using
                rgEmp.Rebind()
                rgNotEmp.Rebind()
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
            End If

            If e.ActionName = "DELETE" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Using rep As New PayrollRepository
                    rep.DeleteEmpQuyLuong(SelectedItemEmp)
                End Using
                rgEmp.Rebind()
                rgNotEmp.Rebind()
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ' ''' <lastupdate>
    ' ''' 23/08/2017 09:00
    ' ''' </lastupdate>
    ' ''' <summary>
    ' ''' Load data len grid
    ' ''' </summary>
    ' ''' <param name="source"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    Protected Sub rgNotEmp_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgNotEmp.NeedDataSource
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

    ' ''' <lastupdate>
    ' ''' 23/08/2017 09:00
    ' ''' </lastupdate>
    ' ''' <summary>
    ' ''' Load data len grid
    ' ''' </summary>
    ' ''' <param name="source"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    Protected Sub rgEmp_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEmp.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter1()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
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

    Private Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        If cboDonVi.SelectedValue = "" Then
            ShowMessage(Translate("Đơn vị quỹ lương bắt buộc nhập, Vui lòng kiểm tra lại."), NotifyType.Warning)
            Exit Sub
        End If
        If cboPeriodSearch.SelectedValue = "" Then
            ShowMessage(Translate("Kỳ lương bắt buộc nhập, Vui lòng kiểm tra lại."), NotifyType.Warning)
            Exit Sub
        End If

        GetEmpNotSelected()
        ctrlMessageBox.MessageText = "Bạn có chắc chắn Gán " & SelectedItemEmpNot.Count & " nhân viên vào Đơn vị quỹ lương?"
        ctrlMessageBox.ActionName = "INSERT"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub


    Private Sub GetEmpNotSelected()
        SelectedItemEmpNot.Clear()
        For Each dr As Telerik.Web.UI.GridDataItem In rgNotEmp.SelectedItems
            Dim id As String = dr.GetDataKeyValue("ID")
            SelectedItemEmpNot.Add(id)
        Next
    End Sub


    Private Sub btnInsertAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsertALL.Click

        If cboDonVi.SelectedValue = "" Then
            ShowMessage(Translate("Đơn vị quỹ lương bắt buộc nhập, Vui lòng kiểm tra lại."), NotifyType.Warning)
            Exit Sub
        End If
        If cboPeriodSearch.SelectedValue = "" Then
            ShowMessage(Translate("Kỳ lương bắt buộc nhập, Vui lòng kiểm tra lại."), NotifyType.Warning)
            Exit Sub
        End If

        GetEmpNotAllSelected()
        ctrlMessageBox.MessageText = "Bạn có chắc chắn Gán " & ntxtNotEmp.Value & " nhân viên vào Đơn vị quỹ lương?"
        ctrlMessageBox.ActionName = "INSERT"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub


    Private Sub GetEmpNotAllSelected()
        SelectedItemEmpNot.Clear()
        For Each dr As Telerik.Web.UI.GridDataItem In rgNotEmp.Items
            Dim id As String = dr.GetDataKeyValue("ID")
            SelectedItemEmpNot.Add(id)
        Next
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If cboDonVi.SelectedValue = "" Then
            ShowMessage(Translate("Đơn vị quỹ lương bắt buộc nhập, Vui lòng kiểm tra lại."), NotifyType.Warning)
            Exit Sub
        End If
        If cboPeriodSearch.SelectedValue = "" Then
            ShowMessage(Translate("Kỳ lương bắt buộc nhập, Vui lòng kiểm tra lại."), NotifyType.Warning)
            Exit Sub
        End If
        GetEmpSelected()
        ctrlMessageBox.MessageText = "Bạn có chắc chắn xóa " & SelectedItemEmp.Count & " nhân viên vào Đơn vị quỹ lương?"
        ctrlMessageBox.ActionName = "DELETE"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub GetEmpSelected()
        SelectedItemEmp.Clear()
        For Each dr As Telerik.Web.UI.GridDataItem In rgEmp.SelectedItems
            Dim id As String = dr.GetDataKeyValue("ID")
            SelectedItemEmp.Add(id)
        Next
    End Sub

    Private Sub btnDeleteAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteALL.Click
        If cboDonVi.SelectedValue = "" Then
            ShowMessage(Translate("Đơn vị quỹ lương bắt buộc nhập, Vui lòng kiểm tra lại."), NotifyType.Warning)
            Exit Sub
        End If
        If cboPeriodSearch.SelectedValue = "" Then
            ShowMessage(Translate("Kỳ lương bắt buộc nhập, Vui lòng kiểm tra lại."), NotifyType.Warning)
            Exit Sub
        End If
        GetEmpAllSelected()
        ctrlMessageBox.MessageText = "Bạn có chắc chắn xóa " & ntxtEmp.Value & " nhân viên vào Đơn vị quỹ lương?"
        ctrlMessageBox.ActionName = "DELETE"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub GetEmpAllSelected()
        SelectedItemEmp.Clear()
        For Each dr As Telerik.Web.UI.GridDataItem In rgEmp.Items
            Dim id As String = dr.GetDataKeyValue("ID")
            SelectedItemEmp.Add(id)
        Next
    End Sub

#End Region

End Class