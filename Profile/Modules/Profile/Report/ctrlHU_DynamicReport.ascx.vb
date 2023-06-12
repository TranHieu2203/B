Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_DynamicReport
    Inherits CommonView
    Public Shared _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Public Shared _classPath As String = "Profile/Modules/Profile/Report/ctrlHU_DynamicReport"
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim sExpression As String

#Region "Property"

    ''' <summary>
    ''' Obj lstColumn
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstColumn As List(Of RptDynamicColumnDTO)
        Get
            Return ViewState(Me.ID & "_lstColumn")
        End Get
        Set(ByVal value As List(Of RptDynamicColumnDTO))
            ViewState(Me.ID & "_lstColumn") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj lstFilterColumn
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstFilterColumn As List(Of RptDynamicColumnDTO)
        Get
            Return ViewState(Me.ID & "_lstFilterColumn")
        End Get
        Set(ByVal value As List(Of RptDynamicColumnDTO))
            ViewState(Me.ID & "_lstFilterColumn") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj filters
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property filters As String
        Get
            Return ViewState(Me.ID & "filters")
        End Get
        Set(value As String)
            ViewState(Me.ID & "filters") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 17/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            Common.Common.BuildToolbar(tbarExport, ToolbarItem.Export, ToolbarItem.Approve, ToolbarItem.Save, ToolbarItem.Delete)
            tbarExport.Items(0).Text = Translate("Xuất excel")
            tbarExport.Items(1).Text = Translate("Xuất pdf")
            tbarExport.OnClientButtonClicking = "OnClientButtonClicking"
            AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            ctrlOrganization.AutoPostBack = False
            ctrlOrganization.LoadDataAfterLoaded = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            _myLog.WriteLog(_myLog._info, _classPath, method,
                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/07/2017 10:00
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
            rgData.AllowMultiRowSelection = False
            rgData.ClientSettings.EnablePostBackOnRowClick = True
            Refresh()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc reset lai session dua ve trang thai nothing
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Common.Common.DynamicReportDataSession = Nothing
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/07/2017 10:00
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
            'AccessPanelBar()
            GetcomboReport()
            Dim rep As New ProfileReportRepositoty

            If cboReport.Items.Count > 0 Then
                'cboReport.SelectedIndex = 0
                If cboReport.SelectedValue <> "" Then
                    lstColumn = rep.GetDynamicReportColumn(cboReport.SelectedValue)
                    lstColumn = lstColumn.OrderBy(Function(x) x.TRANSLATE).ToList()
                    lstColumns.Items.Clear()
                    For Each item As RptDynamicColumnDTO In lstColumn
                        lstColumns.Items.Add(New RadListBoxItem(Translate(item.TRANSLATE), item.COLUMN_NAME))
                    Next

                    CreateFilter(lstColumn, RadFilter1)
                End If
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetcomboReport()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep2 As New ProfileReportRepositoty
            Dim lstReport As Dictionary(Of Decimal, String) = rep2.GetDynamicReportList()

            For Each item In lstReport
                cboReport.Items.Add(New RadComboBoxItem(Translate(item.Value), item.Key.ToString))
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức check quyen va an hien cac control theo quyen
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AccessPanelBar()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep2 As New ProfileReportRepositoty
            Dim lstReport As Dictionary(Of Decimal, String) = rep2.GetDynamicReportList()

            For Each item In lstReport
                cboReport.Items.Add(New RadComboBoxItem(Translate(item.Value), item.Key.ToString))
            Next

            Dim i As Integer = 0
            ' lặp item của RadPanelBar
            While (i < cboReport.Items.Count)
                ' lấy item của đang lặp
                Dim itm As String = cboReport.Items(i).Value
                Using rep As New CommonRepository
                    ' lấy thông tin user đang đăng nhập
                    Dim user = LogHelper.CurrentUser
                    ' check xem có phải admin không
                    Dim GroupAdmin As Boolean = rep.CheckGroupAdmin(Common.Common.GetUsername)
                    ' nếu không phải admin
                    If GroupAdmin = False Then
                        ' lấy quyền của user
                        Dim permissions As List(Of PermissionDTO) = rep.GetUserPermissions(Common.Common.GetUsername)
                        ' nếu có quyền
                        If permissions IsNot Nothing Then
                            ' kiểm tra các chức năng ngoại trừ chức năng là báo cáo
                            ' thay vì .value e phải xài .ID đúng không nào ;)
                            Dim isPermissions = (From p In permissions Where (p.GroupID <> 3 Or p.GroupID <> 1) And p.FID = itm).Any
                            ' nếu không tồn tại --> xóa item
                            If Not isPermissions Then
                                cboReport.Items.Remove(i)
                                Continue While
                            End If
                        End If
                    Else
                        ' nếu là admin + có quyền
                        If user.MODULE_ADMIN = "*" OrElse (user.MODULE_ADMIN IsNot Nothing AndAlso user.MODULE_ADMIN.Contains(Me.ModuleName)) Then
                        Else
                            ' xóa nếu admin không có quyền
                            lstReport.Remove(i)
                            Continue While
                        End If
                    End If
                End Using
                i = i + 1
            End While
            ' làm tưởng tự tabstrip
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try


    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 17/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command button cua control tbarExport
    ''' Cap nhat trang thai cua cac control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbarExport_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles tbarExport.ButtonClick
        Dim _error As Integer = 0
        Dim dtData As DataTable
        Dim sType As Object = Nothing
        Dim column As New List(Of String)
        Dim columnType As New List(Of String)
        Dim columnText As New Dictionary(Of String, String)
        Dim lstConditionCol As New List(Of HuConditionColDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CType(e.Item, Telerik.Web.UI.RadToolBarButton).CommandName <> CommonMessage.TOOLBARITEM_DELETE Then
                If lstColumnSelected.Items Is Nothing OrElse lstColumnSelected.Items.Count = 0 Then
                    ShowMessage(Translate("Bạn chưa chọn cột dữ liệu hiển thị"), Utilities.NotifyType.Error)
                    Exit Sub
                End If
                RadFilter1.FireApplyCommand()
                For Each item As RadListBoxItem In lstColumnSelected.Items
                    column.Add(item.Value)
                    columnText.Add(item.Value.ToUpper.Trim(), item.Text)
                    columnType.Add((From p In lstColumn Where p.COLUMN_NAME = item.Value Select p.COLUMN_TYPE).FirstOrDefault)
                    Dim obj As New HuConditionColDTO
                    obj.COL_ID = (From p In lstColumn Where p.COLUMN_NAME = item.Value Select p.ID).FirstOrDefault
                    lstConditionCol.Add(obj)
                Next
            End If
            Using rep As New ProfileReportRepositoty
                Using xls As New ExcelCommon
                    Select Case CType(e.Item, Telerik.Web.UI.RadToolBarButton).CommandName
                        Case CommonMessage.TOOLBARITEM_EXPORT
                            If ctrlOrganization.CurrentValue = "" Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ORG), Utilities.NotifyType.Error)
                                Exit Sub
                            End If
                            sType = ExcelCommon.ExportType.Excel
                        Case CommonMessage.TOOLBARITEM_APPROVE
                            If ctrlOrganization.CurrentValue = "" Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ORG), Utilities.NotifyType.Error)
                                Exit Sub
                            End If
                            sType = ExcelCommon.ExportType.PDF

                        Case CommonMessage.TOOLBARITEM_SAVE
                            If txtReportName.Text = "" Then
                                ShowMessage(Translate("Bạn phải nhập Tên báo cáo"), Utilities.NotifyType.Error)
                                Exit Sub
                            End If
                            Dim SaveReport As New HuDynamicConditionDTO
                            SaveReport.VIEW_ID = cboReport.SelectedValue
                            SaveReport.REPORT_NAME = txtReportName.Text
                            SaveReport.CONDITION = RadFilter1.SaveSettings()
                            If rep.SaveDynamicReport(SaveReport, lstConditionCol) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                rgData.Rebind()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                            End If
                        Case CommonMessage.TOOLBARITEM_DELETE
                            If rgData.SelectedValue Is Nothing Then
                                Exit Sub
                            End If
                            ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                            ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                            ctrlMessageBox.DataBind()
                            ctrlMessageBox.Show()
                    End Select

                    If sType IsNot Nothing Then
                        dtData = rep.GetDynamicReport(cboReport.SelectedValue,
                                                      ctrlOrganization.CurrentValue,
                                                      ctrlOrganization.IsDissolve,
                                                      chkTerminate.Checked,
                                                      chkHasTerminate.Checked,
                                                      column, sExpression)
                        Dim i As Integer = 0
                        For Each dc As DataColumn In dtData.Columns
                            dc.Caption = columnText(dc.ColumnName.Trim())
                            i = i + 1
                        Next
                        Dim bCheck = ExportExcelNoTemplateWithImage(Server.MapPath("~/ReportTemplates//" &
                                                                              Request.Params("mid") & "/" &
                                                                              Request.Params("group") & "/Dynamic.xls"),
                                                                          "Dynamic", dtData, Response, _error, sType, 0, txtReportName.Text)
                        If Not bCheck Then
                            Select Case _error
                                Case 1
                                    ShowMessage(Translate("Mẫu báo cáo không tồn tại"), NotifyType.Warning)
                                Case 2
                                    ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
                            End Select
                            Exit Sub
                        End If
                    End If

                End Using
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(ex.Message, Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileReportRepositoty
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                If rep.DeleteDynamicReport(rgData.SelectedValue) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgData.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                End If
                UpdateControlState()
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 17/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien apply custom filter cho control RadFilter1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadFilter1_ApplyExpressions(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadFilterApplyExpressionsEventArgs) Handles RadFilter1.ApplyExpressions
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ctrlOrganization.CurrentValue = "" Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ORG), Utilities.NotifyType.Error)
                Exit Sub
            End If
            If lstColumnSelected.Items Is Nothing OrElse lstColumnSelected.Items.Count = 0 Then
                ShowMessage(Translate("Bạn chưa chọn cột dữ liệu nào"), Utilities.NotifyType.Error)
                Exit Sub
            End If
            Dim provider2 As New RadFilterSqlQueryProvider()
            provider2.ProcessGroup(e.ExpressionRoot)
            sExpression = provider2.Result
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    ''' <lastupdate>
    ''' 17/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien khoi tao control de edit cho control RadFilter1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadFilter1_FieldEditorCreating(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadFilterFieldEditorCreatingEventArgs) Handles RadFilter1.FieldEditorCreating
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.EditorType = "RadFilterDateTimeEditor" Then
                e.Editor = New RadFilterDateTimeEditor()
            ElseIf e.EditorType = "RadFilterStringEditor" Then
                e.Editor = New RadFilterStringEditor()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    ''' <lastupdate>
    ''' 17/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien ajax cho control AjaxManager
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Argument = "Close" Then
                rwReport.VisibleOnPageLoad = False
                rwReport.Visible = False
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    ''' <lastupdate>
    ''' 17/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien SelectedIndexChanged cho cboReport
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboReport_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboReport.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If cboReport.SelectedValue <> "" Then
                Dim rep As New ProfileReportRepositoty
                Dim id As Decimal = Decimal.Parse(cboReport.SelectedValue)
                lstColumn = rep.GetDynamicReportColumn(id)
                lstColumn = lstColumn.OrderBy(Function(x) x.TRANSLATE).ToList()
                txtFilter.Text = ""
                lstColumns.Items.Clear()
                lstColumnSelected.Items.Clear()
                RadFilter1.RootGroup.Expressions.Clear()
                RadFilter1.RecreateControl()

                For Each item As RptDynamicColumnDTO In lstColumn
                    lstColumns.Items.Add(New RadListBoxItem(Translate(item.TRANSLATE), item.COLUMN_NAME))
                Next
                rep.Dispose()
                CreateFilter(lstColumn, RadFilter1)
            Else
                txtFilter.Text = ""
                lstColumns.Items.Clear()
                lstColumnSelected.Items.Clear()
                RadFilter1.RootGroup.Expressions.Clear()
                RadFilter1.RecreateControl()
                RadFilter1.FieldEditors.Clear()
            End If
            rgData.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien bind data cho control rgData
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgData_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If cboReport.SelectedValue <> "" Then
                Using rep As New ProfileReportRepositoty
                    rgData.DataSource = rep.GetListReportName(cboReport.SelectedValue)
                End Using
            Else
                rgData.DataSource = New List(Of HuDynamicConditionDTO)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien SelectedIndexChanged cho control rgData
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgData_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgData.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rgData.SelectedItems.Count > 0 Then
                Dim item = rgData.SelectedValues
                If item IsNot Nothing Then
                    txtReportName.Text = item.Item("REPORT_NAME").ToString
                    RadFilter1.LoadSettings(item.Item("CONDITION").ToString)
                    Using rep As New ProfileReportRepositoty
                        lstColumnSelected.DataSource = rep.GetConditionColumn(item.Item("ID"))
                        lstColumnSelected.DataTextField = "TRANSLATE"
                        lstColumnSelected.DataValueField = "COLUMN_NAME"
                        lstColumnSelected.DataBind()
                    End Using
                End If
            End If
            'If (rgData.MasterTableView.GetSelectedItems.Count > 1 Or rgData.MasterTableView.GetSelectedItems.Count = 0) Then
            '    ClearControlValue(txtFilter, txtReportName, cboReport)
            'End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    Public Function ExportExcelNoTemplateWithImage(ByVal filePath As String,
                                                  ByVal fileName As String,
                                                  ByVal dtData As DataTable,
                                                  ByVal Response As System.Web.HttpResponse,
                                                  Optional ByRef _error As String = "",
                                                  Optional ByVal type As ExportType = ExportType.Excel,
                                                  Optional ByVal iRowStart As Integer = 0,
                                                  Optional ByVal titleName As String = "") As Boolean
        Dim designer As WorkbookDesigner
        Dim rep As New ProfileRepository
        Try
            If Not File.Exists(filePath) Then
                _error = 1
                Return False
            End If
            If dtData Is Nothing OrElse (dtData IsNot Nothing AndAlso dtData.Rows.Count = 0) Then
                _error = 2
                Return False
            End If
            dtData.TableName = "DATA"
            designer = New WorkbookDesigner
            designer.Open(filePath)
            Dim cells As Cells = designer.Workbook.Worksheets(0).Cells
            Dim style As Style
            If titleName <> "" Then
                cells(1, 0).PutValue(titleName)
                style = cells(1, 0).GetStyle
                style.Font.IsBold = True
                style.Font.Size = 15
                cells(1, 0).SetStyle(style)
                iRowStart += 3
            End If
            For iCol As Integer = 0 To dtData.Columns.Count - 1
                cells(iRowStart, iCol).PutValue(dtData.Columns(iCol).Caption)
                style = cells(iRowStart, iCol).GetStyle
                style.Font.IsBold = True
                style.Borders.SetStyle(CellBorderType.Thin)
                style.Borders(BorderType.DiagonalDown).LineStyle = CellBorderType.None
                style.Borders(BorderType.DiagonalUp).LineStyle = CellBorderType.None
                style.VerticalAlignment = TextAlignmentType.Center
                style.HorizontalAlignment = TextAlignmentType.Center
                cells(iRowStart, iCol).SetStyle(style)
                For iRow As Integer = 0 To dtData.Rows.Count - 1
                    style = cells(iRow + (iRowStart + 1), iCol).GetStyle()
                    style.VerticalAlignment = TextAlignmentType.Center
                    style.Borders.SetStyle(CellBorderType.Thin)
                    style.Borders(BorderType.DiagonalDown).LineStyle = CellBorderType.None
                    style.Borders(BorderType.DiagonalUp).LineStyle = CellBorderType.None
                    Dim value = dtData.Rows(iRow)(iCol)
                    Select Case dtData.Rows(iRow)(iCol).GetType
                        Case GetType(Date), GetType(Date?), GetType(DateTime), GetType(DateTime?)
                            Dim test As New TimeSpan(0, 0, 0)
                            If value IsNot DBNull.Value AndAlso CType(value, DateTime).TimeOfDay <> test Then
                                style.Custom = "HH:mm"
                            Else
                                style.Custom = "dd/MM/yyyy"
                            End If

                            style.HorizontalAlignment = TextAlignmentType.Center
                        Case GetType(Int16), GetType(Int16?), GetType(Int32), GetType(Int32?), GetType(Int64), GetType(Int64?),
                            GetType(Integer), GetType(Integer?)
                            style.Number = 0
                            style.HorizontalAlignment = TextAlignmentType.Right
                        Case GetType(Double), GetType(Double?), GetType(Decimal), GetType(Decimal?)
                            style.Number = 43
                            style.HorizontalAlignment = TextAlignmentType.Right
                        Case GetType(Boolean), GetType(Boolean?)
                            style.Number = 49
                            style.HorizontalAlignment = TextAlignmentType.Center
                            If value IsNot DBNull.Value AndAlso value IsNot Nothing AndAlso value Then
                                value = "X"
                            Else
                                value = ""
                            End If
                        Case Else
                            style.Number = 49
                            style.HorizontalAlignment = TextAlignmentType.Left
                            style.IsTextWrapped = True
                    End Select
                    If dtData.Columns(iCol).ColumnName = "IMG" Then
                        If Not IsDBNull(dtData.Rows(iRow)(iCol)) Then
                            Dim fByte = rep.GetTemplateFileHost("EmployeeImage\" & dtData.Rows(iRow)(iCol))
                            If fByte IsNot Nothing Then
                                Dim ms As New System.IO.MemoryStream(fByte)
                                cells.SetRowHeight(iRow + 1, 70)
                                designer.Workbook.Worksheets(0).Shapes.AddPicture(iRow + (iRowStart + 1), iCol, iRow + (iRowStart + 1) + 1, iCol + 1, ms)
                            End If
                        End If
                    Else
                        cells(iRow + (iRowStart + 1), iCol).PutValue(dtData.Rows(iRow)(iCol))
                    End If
                    Dim row = designer.Workbook.Worksheets(0).Cells
                    cells(iRow + (iRowStart + 1), iCol).SetStyle(style)
                Next
            Next

            designer.Process()

            With designer.Workbook
                .Worksheets(0).AutoFitColumns()
                .CalculateFormula()
                Select Case type
                    Case ExportType.Excel
                        .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                    Case ExportType.Pdf
                        .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(FileFormatType.Pdf))
                End Select
            End With
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 17/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien create cac filter tuy theo COLUMN_TYPE
    ''' Cap nhat trang thai cua cac control
    ''' </summary>
    ''' <param name="_lstColumn"></param>
    ''' <param name="_filter"></param>
    ''' <remarks></remarks>
    Public Sub CreateFilter(ByVal _lstColumn As List(Of RptDynamicColumnDTO), ByVal _filter As RadFilter)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            _filter.FieldEditors.Clear()
            If _lstColumn Is Nothing Then
                Exit Sub
            End If
            For Each p In _lstColumn
                If p.COLUMN_TYPE.ToUpper.Contains("VARCHAR") Then
                    Dim textFieldEditor As New RadFilterStringEditor()
                    _filter.FieldEditors.Add(textFieldEditor)
                    textFieldEditor.FieldName = p.COLUMN_NAME
                    textFieldEditor.DisplayName = Translate(p.TRANSLATE)
                    textFieldEditor.DataType = GetType(String)
                ElseIf p.COLUMN_TYPE.ToUpper.Contains("NUMBER") Then
                    Dim numFieldEditor As New RadFilterNumericFieldEditor()
                    _filter.FieldEditors.Add(numFieldEditor)
                    numFieldEditor.FieldName = p.COLUMN_NAME
                    numFieldEditor.DisplayName = Translate(p.TRANSLATE)
                    numFieldEditor.DataType = GetType(Decimal)
                ElseIf p.COLUMN_TYPE.ToUpper.Contains("DATE") Then
                    Dim dateFieldEditor As New RadFilterDateTimeEditor()
                    _filter.FieldEditors.Add(dateFieldEditor)
                    dateFieldEditor.FieldName = p.COLUMN_NAME
                    dateFieldEditor.DisplayName = Translate(p.TRANSLATE)
                    dateFieldEditor.DataType = GetType(Object)
                End If
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    ''' <lastupdate>
    ''' 17/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien PreRender cho control RadFilter1
    ''' set height, auto srcoll va data cho RadFilter1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadFilter1_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles RadFilter1.PreRender
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim menu = TryCast(RadFilter1.FindControl("rfContextMenu"), RadContextMenu)
            menu.DefaultGroupSettings.Height = Unit.Pixel(300)
            menu.EnableAutoScroll = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho control Button1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            lstColumns.Items.Clear()
            If txtFilter.Text.Trim() <> "" Then
                Dim lstSelect As List(Of String) = (From p In lstColumnSelected.Items Select p.Value).ToList
                lstFilterColumn = (From p In lstColumn
                                   Where p.TRANSLATE.ToUpper.Contains(txtFilter.Text.Trim().ToUpper)).ToList
                lstFilterColumn = lstFilterColumn.OrderBy(Function(x) x.TRANSLATE).ToList()
                For Each item As RptDynamicColumnDTO In lstFilterColumn
                    If Not lstSelect.Contains(item.COLUMN_NAME) Then
                        lstColumns.Items.Add(New RadListBoxItem(Translate(item.TRANSLATE), item.COLUMN_NAME))
                    End If
                Next
                CreateFilter(lstFilterColumn, RadFilter1)
            Else
                Dim lstSelect As List(Of String) = (From p In lstColumnSelected.Items Select p.Value).ToList
                For Each item As RptDynamicColumnDTO In lstColumn
                    If Not lstSelect.Contains(item.COLUMN_NAME) Then
                        lstColumns.Items.Add(New RadListBoxItem(Translate(item.TRANSLATE), item.COLUMN_NAME))
                    End If
                Next
                CreateFilter(lstColumn, RadFilter1)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

#End Region

#Region "Rad Custome Filter Class"
    Public Class RadFilterDateTimeEditor
        Inherits RadFilterDataFieldEditor

        ''' <lastupdate>
        ''' 17/07/2017 10:00
        ''' </lastupdate>
        ''' <summary>
        ''' Phuong thuc xu ly CopySettings tu baseEditor sang RadFilterDataFieldEditor
        ''' </summary>
        ''' <param name="baseEditor"></param>
        ''' <remarks></remarks>
        Protected Overrides Sub CopySettings(ByVal baseEditor As RadFilterDataFieldEditor)
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                MyBase.CopySettings(baseEditor)
                Dim editor = TryCast(baseEditor, RadFilterDateTimeEditor)
                _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

            End Try

        End Sub

        ''' <lastupdate>
        ''' 17/07/2017 10:00
        ''' </lastupdate>
        ''' <summary>
        ''' Phuong thuc xu ly lay values duoc select va add vao list
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Function ExtractValues() As System.Collections.ArrayList
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Dim list As New ArrayList()
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                If _date1.SelectedDate IsNot Nothing Then
                    Dim strDate1 As String = _date1.SelectedDate.Value.ToString("dd-MMM-yyyy")
                    list.Add(strDate1)
                End If
                _myLog.WriteLog(_myLog._info, _classPath, method,
                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

            End Try
            Return list
        End Function

        ''' <lastupdate>
        ''' 17/07/2017 10:00
        ''' </lastupdate>
        ''' <summary>
        ''' Phuong thuc khoi tao cac control date
        ''' </summary>
        ''' <param name="container"></param>
        ''' <remarks></remarks>
        Public Overrides Sub InitializeEditor(ByVal container As System.Web.UI.Control)
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                _date1 = New RadDatePicker()
                _date1.ID = "MyDatePicker1"
                _date1.MinDate = New Date(1900, 1, 1)
                _date1.MaxDate = New Date(3000, 1, 1)
                _date1.Width = 100
                container.Controls.Add(_date1)
                _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

            End Try

        End Sub

        ''' <lastupdate>
        ''' 17/07/2017 10:00
        ''' </lastupdate>
        ''' <summary>
        ''' Phuong thuc set gia tri cho cac control date
        ''' </summary>
        ''' <param name="values"></param>
        ''' <remarks></remarks>
        Public Overrides Sub SetEditorValues(ByVal values As System.Collections.ArrayList)
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                If values IsNot Nothing AndAlso values.Count > 0 Then
                    If values(0) Is Nothing Then
                        Return
                    End If
                    _date1.SelectedDate = CDate(values(0))
                End If
                _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

            End Try

        End Sub

        Private _date1 As RadDatePicker
    End Class

    Public Class RadFilterStringEditor
        Inherits RadFilterDataFieldEditor

        ''' <lastupdate>
        ''' 17/07/2017 10:00
        ''' </lastupdate>
        ''' <summary>
        ''' Phuong thuc xu ly CopySettings tu baseEditor sang RadFilterDataFieldEditor
        ''' </summary>
        ''' <param name="baseEditor"></param>
        ''' <remarks></remarks>
        Protected Overrides Sub CopySettings(ByVal baseEditor As RadFilterDataFieldEditor)
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                MyBase.CopySettings(baseEditor)
                Dim editor = TryCast(baseEditor, RadFilterStringEditor)
                _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

            End Try

        End Sub

        ''' <lastupdate>
        ''' 17/07/2017 10:00
        ''' </lastupdate>
        ''' <summary>
        ''' Phuong thuc xu ly lay values duoc select va add vao list
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Function ExtractValues() As System.Collections.ArrayList
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Dim list As New ArrayList()
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                list.Add(_textbox1.Text.ToUpper)
                _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
                list = Nothing
            End Try
            Return list
        End Function

        ''' <lastupdate>
        ''' 17/07/2017 10:00
        ''' </lastupdate>
        ''' <summary>
        ''' Phuong thuc khoi tao cac control textbox
        ''' </summary>
        ''' <param name="container"></param>
        ''' <remarks></remarks>
        Public Overrides Sub InitializeEditor(ByVal container As System.Web.UI.Control)
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                _textbox1 = New RadTextBox()
                _textbox1.ID = "MyTextbox1"
                _textbox1.Width = 100
                container.Controls.Add(_textbox1)
                _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

            End Try

        End Sub

        ''' <lastupdate>
        ''' 17/07/2017 10:00
        ''' </lastupdate>
        ''' <summary>
        ''' Phuong thuc set gia tri cho cac control textbox
        ''' </summary>
        ''' <param name="values"></param>
        ''' <remarks></remarks>
        Public Overrides Sub SetEditorValues(ByVal values As System.Collections.ArrayList)
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                If values IsNot Nothing AndAlso values.Count > 0 Then
                    If values(0) Is Nothing Then
                        Return
                    End If
                    _textbox1.Text = CStr(values(0))
                End If
                _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

            End Try

        End Sub

        Private _textbox1 As RadTextBox
    End Class
#End Region

End Class