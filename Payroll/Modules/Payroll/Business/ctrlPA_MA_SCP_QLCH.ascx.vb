Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_MA_SCP_QLCH
    Inherits Common.CommonView

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Payroll\Modules\Business" + Me.GetType().Name.ToString()


#Region "Property"

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STORE_NAME", GetType(String))
                dt.Columns.Add("STORE_ID", GetType(String))
                dt.Columns.Add("PERIOD_NAME", GetType(String))
                dt.Columns.Add("PERIOD_ID", GetType(String))
                dt.Columns.Add("DTTD", GetType(String))
                dt.Columns.Add("DTTD_NG", GetType(String))
                dt.Columns.Add("DTTD_KNG1", GetType(String))
                dt.Columns.Add("DTTD_KNG2", GetType(String))
                dt.Columns.Add("UPT_TD", GetType(String))
                dt.Columns.Add("CON_TD", GetType(String))
                dt.Columns.Add("RATE_RR12", GetType(String))
                dt.Columns.Add("RATE_SPSG", GetType(String))
                dt.Columns.Add("RATE_CSS", GetType(String))
                dt.Columns.Add("RATE_FSOM", GetType(String))
                dt.Columns.Add("RATE_MRA", GetType(String))

                dt.Columns.Add("RATE_CR", GetType(String))
                dt.Columns.Add("RATE_EMAILCUSTOMER", GetType(String))
                dt.Columns.Add("RATE_MBS", GetType(String))
                dt.Columns.Add("RATE_90D", GetType(String))
                dt.Columns.Add("RATE_MA", GetType(String))
                dt.Columns.Add("RATE_SCP", GetType(String))

                dt.Columns.Add("RR6_TD", GetType(String))
                dt.Columns.Add("SLBILL_TD", GetType(String))
                dt.Columns.Add("SLTV", GetType(String))
                dt.Columns.Add("SLTV_6MONTH", GetType(String))
                dt.Columns.Add("SLBILL_NONMEMBER", GetType(String))
                dt.Columns.Add("SLBILL_NEWMEMBER", GetType(String))
                dt.Columns.Add("TARGET_GROUP_ID", GetType(String))
                dt.Columns.Add("TARGET_GROUP_NAME", GetType(String))

                dt.Columns.Add("SLKH_RETURN_YEAR", GetType(String))
                dt.Columns.Add("SLKH_MEMBER_YEAR", GetType(String))

                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    Property lstTarget As List(Of OT_OTHERLIST_DTO)
        Get
            Return ViewState(Me.ID & "_lstTarget")
        End Get
        Set(ByVal value As List(Of OT_OTHERLIST_DTO))
            ViewState(Me.ID & "_lstTarget") = value
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
        Dim rep As New PayrollRepository
        Dim objPeriod As List(Of ATPeriodDTO)
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ListComboData = New ComboBoxDataDTO
            cboTARGET_GROUP.ClearSelection()
            ListComboData.GET_TARGET_GROUP = True
            rep.GetComboboxData(ListComboData)
            lstTarget = ListComboData.LIST_TARGET_GROUP
            FillDropDownList(cboTARGET_GROUP, lstTarget, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboTARGET_GROUP.SelectedValue)
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
            Dim row2 As DataRow = table.NewRow
            row2("ID") = DBNull.Value
            row2("YEAR") = DBNull.Value
            table.Rows.Add(row2)
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year

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
    ''' Ham khoi taso toolbar voi cac button them moi, sua, xuat file, xoa, in hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarContracts

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export, ToolbarItem.ExportTemplate, ToolbarItem.Import, ToolbarItem.Calculate)
            MainToolBar.Items(1).Text = Translate("Xuất file mẫu")
            MainToolBar.Items(2).Text = Translate("Nhập file mẫu")
            MainToolBar.Items(3).Text = Translate("Tải dữ liệu")
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
            'ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
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
        Dim rep As New PayrollRepository
        Dim sp As New PayrollStoreProcedure
        Try
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
                            rgData.ExportExcel(Server, Response, dtData, "DS_DTTĐ_CHTT")
                            Exit Sub
                        End If
                    End Using
                Case CommonMessage.TOOLBARTIEM_CALCULATE

                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim store As New PayrollStoreProcedure
                    Dim dsData As DataSet = sp.GET_IMPORT_PA_MA_SCP_QLCH()
                    dsData.Tables(0).TableName = "Table"
                    dsData.Tables(1).TableName = "Table1"
                    dsData.Tables(2).TableName = "Table2"
                    ''ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_TimeSheet_Monthly')", True)
                    ExportTemplate("Payroll\Import\Template_import_MA_SCP_QLCH.xls",
                                              dsData, Nothing,
                                              "Template_import_DTCHTT" & Format(Date.Now, "yyyyMMdd"))
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
            End Select
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
                CurrentState = CommonMessage.STATE_DELETE
            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_LOCK And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_UNLOCK And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
            End If
            UpdateControlState()
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
    ''' xu ly su kien SelectedNodeChanged cua control ctrlOrg
    ''' Xet lai cac thiet lap trang thai cho grid rgData
    ''' Bind lai du lieu cho rgData
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        rgData.CurrentPageIndex = 0
    '        rgData.MasterTableView.SortExpressions.Clear()
    '        rgData.Rebind()
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

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
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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
            For Each rows As DataRow In dsDataPrepare.Tables(0).Rows
                If String.IsNullOrEmpty(rows("STORE_ID").ToString) AndAlso String.IsNullOrEmpty(rows("PERIOD_ID").ToString) Then Continue For
                Dim newRow As DataRow = dtData.NewRow
                newRow("TARGET_GROUP_NAME") = rows("TARGET_GROUP_NAME")
                newRow("TARGET_GROUP_ID") = rows("TARGET_GROUP_ID")
                newRow("STORE_ID") = rows("STORE_ID")
                newRow("STORE_NAME") = rows("STORE_NAME")
                newRow("PERIOD_ID") = rows("PERIOD_ID")
                newRow("PERIOD_NAME") = rows("PERIOD_NAME")
                newRow("DTTD") = rows("DTTD").ToString
                newRow("DTTD_NG") = rows("DTTD_NG").ToString
                newRow("DTTD_KNG1") = rows("DTTD_KNG1").ToString
                newRow("DTTD_KNG2") = rows("DTTD_KNG2").ToString
                newRow("UPT_TD") = rows("UPT_TD").ToString
                newRow("CON_TD") = rows("CON_TD").ToString
                newRow("RATE_RR12") = rows("RATE_RR12").ToString
                newRow("RATE_SPSG") = rows("RATE_SPSG").ToString

                newRow("RATE_CSS") = rows("RATE_CSS").ToString
                newRow("RATE_FSOM") = rows("RATE_FSOM").ToString
                newRow("RATE_MRA") = rows("RATE_MRA").ToString
                newRow("RATE_CR") = rows("RATE_CR").ToString
                newRow("RATE_EMAILCUSTOMER") = rows("RATE_EMAILCUSTOMER").ToString
                newRow("RATE_MBS") = rows("RATE_MBS").ToString
                newRow("RATE_90D") = rows("RATE_90D").ToString

                newRow("RATE_MA") = rows("RATE_MA").ToString
                newRow("RATE_SCP") = rows("RATE_SCP").ToString

                newRow("RR6_TD") = rows("RR6_TD").ToString
                newRow("SLBILL_TD") = rows("SLBILL_TD").ToString
                newRow("SLTV") = rows("SLTV").ToString
                newRow("SLTV_6MONTH") = rows("SLTV_6MONTH").ToString
                newRow("SLBILL_NONMEMBER") = rows("SLBILL_NONMEMBER").ToString
                newRow("SLBILL_NEWMEMBER") = rows("SLBILL_NEWMEMBER").ToString

                newRow("SLKH_RETURN_YEAR") = rows("SLKH_RETURN_YEAR").ToString
                newRow("SLKH_MEMBER_YEAR") = rows("SLKH_MEMBER_YEAR").ToString

                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim sp As New PayrollStoreProcedure
                If sp.IMPORT_PA_MA_SCP_QLCH(DocXml, LogHelper.GetUserLog().Username.ToUpper) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                    rgData.Rebind()
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import lỗi"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ChangeToolbarState()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
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
        Dim _filter As New PA_MA_SCP_QLCHDTO
        Try
            'If ctrlOrg.CurrentValue Is Nothing Then
            '    rgData.DataSource = New List(Of PA_MA_SCP_QLCHDTO)
            '    Exit Function
            'End If
            'Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
            '                                .IS_DISSOLVE = ctrlOrg.IsDissolve}
            Dim _param = New ParamDTO
            If cboPeriod.SelectedValue <> "" Then
                _filter.PERIOD_ID = cboPeriod.SelectedValue
            End If
            '_filter.IS_TER = chkTerminate.Checked
            If cboTARGET_GROUP.SelectedValue <> "" Then
                _filter.TARGET_GROUP_NAME = cboTARGET_GROUP.Text
            End If
            SetValueObjectByRadGrid(rgData, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstObj As New List(Of PA_MA_SCP_QLCHDTO)
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetPA_MA_SCP_QLCH(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetPA_MA_SCP_QLCH(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    lstObj = rep.GetPA_MA_SCP_QLCH(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
                Else
                    lstObj = rep.GetPA_MA_SCP_QLCH(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
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

                sError = "Chưa nhập Nhóm Target"
                ImportValidate.EmptyValue("TARGET_GROUP_ID", row, rowError, isError, sError)

                If Not IsDBNull(row("TARGET_GROUP_ID")) AndAlso Not String.IsNullOrEmpty(row("TARGET_GROUP_ID")) Then
                    Dim t_check = (From p In lstTarget Where p.ID = CDec(row("TARGET_GROUP_ID").ToString)).FirstOrDefault
                    If t_check IsNot Nothing AndAlso t_check.CODE.ToUpper.Equals("TARGET_CH") Then
                        sError = "Chưa nhập Mã cửa hàng"
                        ImportValidate.EmptyValue("STORE_NAME", row, rowError, isError, sError)
                    End If
                End If



                If Not IsDBNull(row("PERIOD_ID")) AndAlso Not String.IsNullOrEmpty(row("PERIOD_ID")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("PERIOD_ID", row, rowError, isError, sError)
                End If


                If Not IsDBNull(row("DTTD")) AndAlso Not String.IsNullOrEmpty(row("DTTD")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("DTTD", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("DTTD_NG")) AndAlso Not String.IsNullOrEmpty(row("DTTD_NG")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("DTTD_NG", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("DTTD_KNG1")) AndAlso Not String.IsNullOrEmpty(row("DTTD_KNG1")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("DTTD_KNG1", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("DTTD_KNG2")) AndAlso Not String.IsNullOrEmpty(row("DTTD_KNG2")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("DTTD_KNG2", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("UPT_TD")) AndAlso Not String.IsNullOrEmpty(row("UPT_TD")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("UPT_TD", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("CON_TD")) AndAlso Not String.IsNullOrEmpty(row("CON_TD")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("CON_TD", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("RATE_RR12")) AndAlso Not String.IsNullOrEmpty(row("RATE_RR12")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("RATE_RR12", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("RATE_SPSG")) AndAlso Not String.IsNullOrEmpty(row("RATE_SPSG")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("RATE_SPSG", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("RATE_CSS")) AndAlso Not String.IsNullOrEmpty(row("RATE_CSS")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("RATE_CSS", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("RATE_FSOM")) AndAlso Not String.IsNullOrEmpty(row("RATE_FSOM")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("RATE_FSOM", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("RATE_MRA")) AndAlso Not String.IsNullOrEmpty(row("RATE_MRA")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("RATE_MRA", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("RATE_CR")) AndAlso Not String.IsNullOrEmpty(row("RATE_CR")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("RATE_CR", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("RATE_EMAILCUSTOMER")) AndAlso Not String.IsNullOrEmpty(row("RATE_EMAILCUSTOMER")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("RATE_EMAILCUSTOMER", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("RATE_MBS")) AndAlso Not String.IsNullOrEmpty(row("RATE_MBS")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("RATE_MBS", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("RATE_90D")) AndAlso Not String.IsNullOrEmpty(row("RATE_90D")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("RATE_90D", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("RATE_MA")) AndAlso Not String.IsNullOrEmpty(row("RATE_MA")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("RATE_MA", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("RATE_SCP")) AndAlso Not String.IsNullOrEmpty(row("RATE_SCP")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("RATE_SCP", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("RR6_TD")) AndAlso Not String.IsNullOrEmpty(row("RR6_TD")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("RR6_TD", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("SLBILL_TD")) AndAlso Not String.IsNullOrEmpty(row("SLBILL_TD")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("SLBILL_TD", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("SLTV")) AndAlso Not String.IsNullOrEmpty(row("SLTV")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("SLTV", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("SLTV_6MONTH")) AndAlso Not String.IsNullOrEmpty(row("SLTV_6MONTH")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("SLTV_6MONTH", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("SLBILL_NONMEMBER")) AndAlso Not String.IsNullOrEmpty(row("SLBILL_NONMEMBER")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("SLBILL_NONMEMBER", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("SLBILL_NEWMEMBER")) AndAlso Not String.IsNullOrEmpty(row("SLBILL_NEWMEMBER")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("SLBILL_NEWMEMBER", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("SLKH_RETURN_YEAR")) AndAlso Not String.IsNullOrEmpty(row("SLKH_RETURN_YEAR")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("SLKH_RETURN_YEAR", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("SLKH_MEMBER_YEAR")) AndAlso Not String.IsNullOrEmpty(row("SLKH_MEMBER_YEAR")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("SLKH_MEMBER_YEAR", row, rowError, isError, sError)
                End If

                If isError Then
                    If IsDBNull(rowError("TARGET_GROUP_NAME")) Then
                        rowError("TARGET_GROUP_NAME") = row("TARGET_GROUP_NAME").ToString
                    End If
                    If IsDBNull(rowError("PERIOD_NAME")) Then
                        rowError("PERIOD_NAME") = row("PERIOD_NAME").ToString
                    End If
                    If IsDBNull(rowError("STORE_NAME")) Then
                        rowError("STORE_NAME") = row("STORE_NAME").ToString
                    End If
                    dtError.Rows.Add(rowError)
                End If
                iRow = iRow + 1
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("MA_SCP_QLCH_Error") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_import_MA_SCP_QLCH_Error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            If isError Then
                Return False
            Else
                Return True
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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