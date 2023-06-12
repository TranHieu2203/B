Imports System.Globalization
Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_Family
    Inherits Common.CommonView

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()


#Region "Property"

    Public Property popupId As String
    Public Property AjaxManagerId As String
    ''' <summary>
    ''' Obj FamilyDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Contract As FamilyDTO
        Get
            Return ViewState(Me.ID & "_Contract")
        End Get
        Set(ByVal value As FamilyDTO)
            ViewState(Me.ID & "_Contract") = value
        End Set
    End Property

    ''' <summary>
    ''' List obj Family
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Family As List(Of FamilyDTO)
        Get
            Return ViewState(Me.ID & "_Family")
        End Get
        Set(ByVal value As List(Of FamilyDTO))
            ViewState(Me.ID & "_Family") = value
        End Set
    End Property

    ''' <summary>
    ''' Insert FamilyDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InsertFamily As FamilyDTO
        Get
            Return ViewState(Me.ID & "_InsertFamily")
        End Get
        Set(ByVal value As FamilyDTO)
            ViewState(Me.ID & "_InsertFamily") = value
        End Set
    End Property

    ''' <summary>
    ''' Delete FamilyDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeleteContract As FamilyDTO
        Get
            Return ViewState(Me.ID & "_DeleteContract")
        End Get
        Set(ByVal value As FamilyDTO)
            ViewState(Me.ID & "_DeleteContract") = value
        End Set
    End Property

    ''' <summary>
    '''  Kiem tra trang thai update
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsUpdate")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsUpdate") = value
        End Set
    End Property

    ''' <summary>
    ''' Gia tri _IDSelect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
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
    ''' Xét các trạng thái của grid rgFamily
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            rgFamily.AllowCustomPaging = True
            rgFamily.SetFilter()
            InitControl()
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                GirdConfig(rgFamily)
            End If
            popupId = CType(Me.Page, AjaxPage).PopupWindow.ClientID
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
            GetDataCombo()
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
            Me.MainToolBar = tbarFamily

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Export,
                                       ToolbarItem.Seperator, ToolbarItem.Next, ToolbarItem.Import, ToolbarItem.Delete)
            CType(MainToolBar.Items(4), RadToolBarButton).Text = "Xuất file mẫu"
            CType(MainToolBar.Items(5), RadToolBarButton).Text = "Nhập file mẫu"
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
    ''' Bind lai du lieu cho grid rgFamily
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation

            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                If Session("Result") = "1" Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Session("Result") = Nothing
                End If
            Else
                Select Case Message
                    Case "UpdateView"
                        rgFamily.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        rgFamily.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                End Select
            End If
            rep.Dispose()
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
        Dim objOrgFunction As New OrganizationDTO
        Dim sError As String = ""
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgFamily.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgFamily.SelectedItems
                        If item.GetDataKeyValue("WORK_STATUS") Is Nothing Or
                        (item.GetDataKeyValue("WORK_STATUS") IsNot Nothing AndAlso
                         (item.GetDataKeyValue("WORK_STATUS") <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or
                          (item.GetDataKeyValue("WORK_STATUS") = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And
                           item.GetDataKeyValue("TER_EFFECT_DATE") > Date.Now.Date))) Then

                        Else
                            ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgFamily.ExportExcel(Server, Response, dtData, "Family")
                            Exit Sub
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_NEXT
                    Template_ImportFamily_Mng()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload.Show()
            End Select

            'UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub Template_ImportFamily_Mng()
        Dim rep As New ProfileStoreProcedure
        Try
            Dim configPath As String = "Profile\Template_FamilyInfomation.xlsx"
            'Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
            '                                    .IS_DISSOLVE = ctrlOrg.IsDissolve}
            Dim dsData As DataSet = rep.GET_DATA_IMPORT_FAMILY()

            If File.Exists(System.IO.Path.Combine(Server.MapPath("ReportTemplates\" + configPath))) Then

                Using xls As New AsposeExcelCommon
                    Dim bCheck = ExportTemplate(configPath, dsData, Nothing, "Template_QL_GIACANHNGUOITHAN_" & Format(Date.Now, "yyyyMMdd"))

                End Using
            Else
                ShowMessage(Translate("Template không tồn tại"), Utilities.NotifyType.Error)
                Exit Sub
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
        Dim filePathOut As String
        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName
            filePathOut = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\"
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
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xlsx", Aspose.Cells.ContentDisposition.Attachment, New XlsSaveOptions(Aspose.Cells.SaveFormat.Xlsx))

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

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
                UpdateControlState()
            End If
            ' Mở form thanh lý hợp đồng
            If e.ActionName = CommonMessage.TOOLBARITEM_REFRESH And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim str As String = "Liquidation_Click();"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                'Dim item As GridDataItem = rgFamily.SelectedItems(0)
                'Dim employeeid As String = item.GetDataKeyValue("EMPLOYEE_ID").ToString
                'Dim id As String = item.GetDataKeyValue("ID").ToString
                'Response.Redirect("Dialog.aspx?mid=Profile&fid=ctrlContract_Liquidate&group=Business&noscroll=1&empid=" + employeeid + "&idCT=" + id)
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
    ''' xu ly su kien SelectedNodeChanged cua control ctrlOrg
    ''' Xet lai cac thiet lap trang thai cho grid rgFamily
    ''' Bind lai du lieu cho rgFamily
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgFamily.CurrentPageIndex = 0
            rgFamily.MasterTableView.SortExpressions.Clear()
            rgFamily.Rebind()
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
    ''' Thiets lap trang thai cho rgFamily
    ''' Bind lai du lieu cho rgFamily
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgFamily.CurrentPageIndex = 0
            rgFamily.MasterTableView.SortExpressions.Clear()
            rgFamily.Rebind()
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
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgFamily.NeedDataSource
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

    ' ''' <lastupdate>
    ' ''' 07/07/2017 08:24
    ' ''' </lastupdate>
    ' ''' <summary>
    ' ''' Phuong thuc xu ly viec zip file vao folder Zip
    ' ''' </summary>
    ' ''' <param name="path"></param>
    ' ''' <remarks></remarks>
    Private Sub ZipFiles(ByVal path As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim crc As New Crc32()

            Dim s As New ZipOutputStream(File.Create(AppDomain.CurrentDomain.BaseDirectory & "Zip\DocumentFolder.zip"))
            s.SetLevel(0)
            ' 0 - store only to 9 - means best compression
            For i As Integer = 0 To Directory.GetFiles(path).Length - 1
                ' Must use a relative path here so that files show up in the Windows Zip File Viewer
                ' .. hence the use of Path.GetFileName(...)
                Dim entry As New ZipEntry(Directory.GetFiles(path)(i))
                entry.DateTime = DateTime.Now

                ' Read in the 
                Using fs As FileStream = File.Open(Directory.GetFiles(path)(i), FileMode.Open)
                    Dim buffer As Byte() = New Byte(fs.Length - 1) {}
                    fs.Read(buffer, 0, buffer.Length)
                    entry.Size = fs.Length
                    fs.Close()
                    crc.Reset()
                    crc.Update(buffer)
                    entry.Crc = crc.Value
                    s.PutNextEntry(entry)
                    s.Write(buffer, 0, buffer.Length)
                End Using
            Next
            s.Finish()
            s.Close()

            Using FileStream = File.Open(AppDomain.CurrentDomain.BaseDirectory & "Zip\DocumentFolder.zip", FileMode.Open)
                Dim buffer As Byte() = New Byte(FileStream.Length - 1) {}
                FileStream.Read(buffer, 0, buffer.Length)
                Dim rEx As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_\-\.]+")
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" + rEx.Replace("DocumentFolder.zip", "_"))
                Response.AddHeader("Content-Length", FileStream.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.BinaryWrite(buffer)
                FileStream.Close()
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
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
    ''' Cap nhat trang thai tbarFamily, rgFamily, ctrlOrg
    ''' Cap nhat trang thai hien tai, neu trang thai hien tai cua trang la trang thai xoa
    ''' thi xoa hop dong vaf lam moi lai UpdateView
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            tbarFamily.Enabled = True
            rgFamily.Enabled = True
            ctrlOrg.Enabled = True
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim SelectedItem As New List(Of Decimal)
                    For Each item As GridDataItem In rgFamily.SelectedItems
                        SelectedItem.Add(item.GetDataKeyValue("ID"))
                    Next
                    rep.DeleteEmployeeFamily(SelectedItem)
                    Refresh("UpdateView")
            End Select
            rep.Dispose()
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
    ''' Phuong thuc fill du lieu cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        'Dim dtData As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            'Using rep As New ProfileRepository
            '    dtData = rep.GetOtherList("CONTRACT_SUPPORT")
            '    FillRadCombobox(cboPrintSupport, dtData, "NAME", "ID")
            'End Using
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
        Dim rep As New ProfileBusinessRepository
        Dim _filter As New FamilyDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If ctrlOrg.CurrentValue Is Nothing Then
                rgFamily.DataSource = New List(Of FamilyDTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}


            _filter.IS_TER = chkTerminate.Checked
            _filter.IS_DEDUCT = chkGiamtru.Checked
            SetValueObjectByRadGrid(rgFamily, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgFamily.MasterTableView.SortExpressions.GetSortString()
            Dim lstFamily As List(Of FamilyDTO)

            If Sorts IsNot Nothing Then
                lstFamily = rep.GetEmployeeFamily_1(_filter, rgFamily.CurrentPageIndex, rgFamily.PageSize, MaximumRows, _param, Sorts)
            Else
                lstFamily = rep.GetEmployeeFamily_1(_filter, rgFamily.CurrentPageIndex, rgFamily.PageSize, MaximumRows, _param)
            End If

            rgFamily.VirtualItemCount = MaximumRows
            rgFamily.DataSource = lstFamily

            rep.Dispose()
            Return lstFamily.ToTable
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý phê duyệt hop dong</summary>
    ''' <remarks></remarks>
    Private Sub BatchApproveContract()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lstID As New List(Of Decimal)

        Try
            '1. Check có rows nào được select hay không
            If rgFamily Is Nothing OrElse rgFamily.SelectedItems.Count <= 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If

            Dim listCon As New List(Of FamilyDTO)

            For Each dr As Telerik.Web.UI.GridDataItem In rgFamily.SelectedItems
                Dim ID As New Decimal
                If Not dr.GetDataKeyValue("STATUS_ID").Equals("447") Then
                    ID = dr.GetDataKeyValue("ID")
                    lstID.Add(ID)
                End If
            Next
            If lstID.Count > 0 Then
                Dim bCheckHasfile = rep.CheckHasFileContract(lstID)
                For Each item As GridDataItem In rgFamily.SelectedItems
                    If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        ShowMessage(Translate("Bản ghi đã phê duyệt."), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                'If bCheckHasfile = 1 Then
                '    ShowMessage(Translate("Duyệt khi tất cả các record đã có tập tin đính kèm,bạn kiểm tra lại"), NotifyType.Warning)
                '    Exit Sub
                'End If
                If rep.ApproveListContract(lstID, "P") Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgFamily.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            Else
                ShowMessage("Các hợp đồng được chọn đã được phê duyệt", NotifyType.Information)
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgFamily_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgFamily.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
                Dim strName = If(datarow.GetDataKeyValue("FILE_FAMILY") IsNot Nothing, datarow.GetDataKeyValue("FILE_FAMILY").ToString, "")
                If strName <> "" Then
                    datarow("DowloadCommandColumnFamily").Enabled = True
                Else
                    datarow("DowloadCommandColumnFamily").Enabled = False
                    datarow("DowloadCommandColumnFamily").CssClass = "hide-button"
                End If
                If strName.ToUpper.Contains(".JPG") Or strName.ToUpper.Contains(".GIF") Or strName.ToUpper.Contains(".PNG") Or strName.ToUpper.Contains(".JPEG") Then
                    datarow("ViewCommandColumnFamily").Enabled = True
                Else
                    datarow("ViewCommandColumnFamily").Enabled = False
                    datarow("ViewCommandColumnFamily").CssClass = "hide-button"
                End If
                Dim strName2 = If(datarow.GetDataKeyValue("FILE_NPT") IsNot Nothing, datarow.GetDataKeyValue("FILE_NPT").ToString, "")
                If strName2 <> "" Then
                    datarow("DowloadCommandColumnNPT").Enabled = True
                Else
                    datarow("DowloadCommandColumnNPT").Enabled = False
                    datarow("DowloadCommandColumnNPT").CssClass = "hide-button"
                End If
                If strName2.ToUpper.Contains(".JPG") Or strName2.ToUpper.Contains(".GIF") Or strName2.ToUpper.Contains(".PNG") Or strName2.ToUpper.Contains(".JPEG") Then
                    datarow("ViewCommandColumnNPT").Enabled = True
                Else
                    datarow("ViewCommandColumnNPT").Enabled = False
                    datarow("ViewCommandColumnNPT").CssClass = "hide-button"
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub rgData_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgFamily.ItemCommand
        Try
            If e.CommandName = "DowloadFamily" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_FAMILY").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link = file.LINK
                Dim name = file.FILE_NAME
                ' ZipFiles(path, FileName)
                link = link.Replace("\", "(slash)")
                Dim url As String = "Download.aspx?" & "ctrlHU_ApproveFamily_Edit," & link & "," & name
                Dim str As String = "window.open('" & url + "');"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End If
            If e.CommandName = "ViewFamily" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_FAMILY").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link As String = file.LINK & "\" & file.FILE_NAME
                Dim strName As String = IO.Path.GetExtension(link).ToUpper()

                link = link.Replace("\", "(slash)")
                If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Or strName.Contains(".JPEG") Then
                    Show(link)
                Else
                    ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
                End If

            End If
            If e.CommandName = "DowloadNPT" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_NPT").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link = file.LINK
                Dim name = file.FILE_NAME
                ' ZipFiles(path, FileName)
                link = link.Replace("\", "(slash)")
                Dim url As String = "Download.aspx?" & "ctrlHU_ApproveFamily_Edit," & link & "," & name
                Dim str As String = "window.open('" & url + "');"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End If
            If e.CommandName = "ViewNPT" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_NPT").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link As String = file.LINK & "\" & file.FILE_NAME
                Dim strName As String = IO.Path.GetExtension(link).ToUpper()

                link = link.Replace("\", "(slash)")
                If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Or strName.Contains(".JPEG") Then
                    Show(link)
                Else
                    ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
                End If

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Sub Show(strfile As Object)
        Dim script As String
        script = "var oWnd = $find('" & popupId & "');"
        script &= "oWnd.add_close(" & Me.ClientID & "_OnClientClose);"
        script &= "oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlViewImage&group=Business&state=Normal&imgUrl=" & strfile & "');"
        script &= "oWnd.show();"
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)

    End Sub

    Private Sub ctrlUpload_OkClicked(sender As Object, e As EventArgs) Handles ctrlUpload.OkClicked
        Try
            Import_Data()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Import_Data()
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim rep As New ProfileStoreProcedure
            '_mylog = LogHelper.GetUserLog
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
                    If rep.IMPORT_DATA_FAMILY(DocXml) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgFamily.Rebind()
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
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        Try
            ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
            'dtTemp.Columns(0).ColumnName = "STT"
            'dtTemp.Columns(1).ColumnName = "EMPLOYEE_CODE"
            'dtTemp.Columns(2).ColumnName = "EMPLOYEE_NAME"
            'dtTemp.Columns(3).ColumnName = "ORG_NAME"
            'dtTemp.Columns(4).ColumnName = "FULLNAME"
            'dtTemp.Columns(5).ColumnName = "RELATION_NAME"
            'dtTemp.Columns(6).ColumnName = "RELATION_ID"
            'dtTemp.Columns(7).ColumnName = "BIRTH_DATE"
            'dtTemp.Columns(8).ColumnName = "GENDER_NAME"
            'dtTemp.Columns(9).ColumnName = "GENDER_ID"
            'dtTemp.Columns(10).ColumnName = "IS_SAME_COMPANY"
            'dtTemp.Columns(11).ColumnName = "IS_SAME_COMPANY_ID"
            'dtTemp.Columns(12).ColumnName = "ID_NO"
            'dtTemp.Columns(13).ColumnName = "ID_NO_DATE"
            'dtTemp.Columns(14).ColumnName = "ID_NO_PLACE_NAME"
            'dtTemp.Columns(15).ColumnName = "IS_OWNER"
            'dtTemp.Columns(16).ColumnName = "IS_OWNER_ID"
            'dtTemp.Columns(17).ColumnName = "CERTIFICATE_NUM"
            'dtTemp.Columns(18).ColumnName = "CERTIFICATE_CODE"
            'dtTemp.Columns(19).ColumnName = "CAREER"
            'dtTemp.Columns(20).ColumnName = "ADDRESS"
            'dtTemp.Columns(21).ColumnName = "NATION_ID_NAME" ' THUONG TRU
            'dtTemp.Columns(22).ColumnName = "NATION_ID"
            'dtTemp.Columns(23).ColumnName = "AD_PROVINCE_ID_NAME"
            'dtTemp.Columns(24).ColumnName = "AD_PROVINCE_ID"
            'dtTemp.Columns(25).ColumnName = "AD_DISTRICT_ID_NAME"
            'dtTemp.Columns(26).ColumnName = "AD_DISTRICT_ID"
            'dtTemp.Columns(27).ColumnName = "AD_WARD_ID_NAME"
            'dtTemp.Columns(28).ColumnName = "AD_WARD_ID"
            'dtTemp.Columns(29).ColumnName = "AD_VILLAGE"
            'dtTemp.Columns(30).ColumnName = "ADDRESS_TT"
            'dtTemp.Columns(31).ColumnName = "TT_NATION_ID_NAME" 'TAM TRU
            'dtTemp.Columns(32).ColumnName = "TT_NATION_ID"
            'dtTemp.Columns(33).ColumnName = "TT_PROVINCE_ID_NAME"
            'dtTemp.Columns(34).ColumnName = "TT_PROVINCE_ID"
            'dtTemp.Columns(35).ColumnName = "TT_DISTRICT_ID_NAME"
            'dtTemp.Columns(36).ColumnName = "TT_DISTRICT_ID"
            'dtTemp.Columns(37).ColumnName = "TT_WARD_ID_NAME"
            'dtTemp.Columns(38).ColumnName = "TT_WARD_ID"
            'dtTemp.Columns(39).ColumnName = "TT_VILLAGE"
            'dtTemp.Columns(40).ColumnName = "IS_PASS_NAME"
            'dtTemp.Columns(41).ColumnName = "IS_PASS"
            'dtTemp.Columns(42).ColumnName = "PHONE"
            'dtTemp.Columns(43).ColumnName = "TAXTATION"
            'dtTemp.Columns(44).ColumnName = "TAXTATION_DATE"
            'dtTemp.Columns(45).ColumnName = "TAXTATION_PLACE"
            'dtTemp.Columns(46).ColumnName = "IS_DEDUCT_NAME"
            'dtTemp.Columns(47).ColumnName = "IS_DEDUCT"
            'dtTemp.Columns(48).ColumnName = "DEDUCT_REG"
            'dtTemp.Columns(49).ColumnName = "DEDUCT_FROM"
            'dtTemp.Columns(50).ColumnName = "DEDUCT_TO"
            'dtTemp.Columns(51).ColumnName = "BIRTH_CODE"
            'dtTemp.Columns(52).ColumnName = "QUYEN"
            'dtTemp.Columns(53).ColumnName = "BIRTH_NATION_ID_NAME" 'KHAI SINH
            'dtTemp.Columns(54).ColumnName = "BIRTH_NATION_ID"
            'dtTemp.Columns(55).ColumnName = "BIRTH_PROVINCE_ID_NAME"
            'dtTemp.Columns(56).ColumnName = "BIRTH_PROVINCE_ID"
            'dtTemp.Columns(57).ColumnName = "BIRTH_DISTRICT_ID_NAME"
            'dtTemp.Columns(58).ColumnName = "BIRTH_DISTRICT_ID"
            'dtTemp.Columns(59).ColumnName = "BIRTH_WARD_ID_NAME"
            'dtTemp.Columns(60).ColumnName = "BIRTH_WARD_ID"
            'dtTemp.Columns(61).ColumnName = "REMARK"

            dtTemp.Columns(0).ColumnName = "STT"
            dtTemp.Columns(1).ColumnName = "EMPLOYEE_CODE"
            dtTemp.Columns(2).ColumnName = "EMPLOYEE_NAME"
            dtTemp.Columns(3).ColumnName = "ORG_NAME"
            dtTemp.Columns(4).ColumnName = "FULLNAME"
            dtTemp.Columns(5).ColumnName = "RELATION_NAME"
            dtTemp.Columns(6).ColumnName = "BIRTH_DATE"
            dtTemp.Columns(7).ColumnName = "GENDER_NAME"
            dtTemp.Columns(8).ColumnName = "IS_SAME_COMPANY"
            dtTemp.Columns(9).ColumnName = "ID_NO"
            dtTemp.Columns(10).ColumnName = "ID_NO_DATE"
            dtTemp.Columns(11).ColumnName = "ID_NO_PLACE_NAME"
            dtTemp.Columns(12).ColumnName = "IS_OWNER"
            dtTemp.Columns(13).ColumnName = "CERTIFICATE_NUM"
            dtTemp.Columns(14).ColumnName = "CERTIFICATE_CODE"
            dtTemp.Columns(15).ColumnName = "CAREER"
            dtTemp.Columns(16).ColumnName = "ADDRESS"
            dtTemp.Columns(17).ColumnName = "NATION_ID_NAME"
            dtTemp.Columns(18).ColumnName = "AD_PROVINCE_ID_NAME"
            dtTemp.Columns(19).ColumnName = "AD_DISTRICT_ID_NAME"
            dtTemp.Columns(20).ColumnName = "AD_WARD_ID_NAME"
            dtTemp.Columns(21).ColumnName = "AD_VILLAGE"
            dtTemp.Columns(22).ColumnName = "ADDRESS_TT"
            dtTemp.Columns(23).ColumnName = "TT_NATION_ID_NAME"
            dtTemp.Columns(24).ColumnName = "TT_PROVINCE_ID_NAME"
            dtTemp.Columns(25).ColumnName = "TT_DISTRICT_ID_NAME"
            dtTemp.Columns(26).ColumnName = "TT_WARD_ID_NAME"
            dtTemp.Columns(27).ColumnName = "TT_VILLAGE"
            dtTemp.Columns(28).ColumnName = "IS_PASS_NAME"
            dtTemp.Columns(29).ColumnName = "PHONE"
            dtTemp.Columns(30).ColumnName = "TAXTATION"
            dtTemp.Columns(31).ColumnName = "TAXTATION_DATE"
            dtTemp.Columns(32).ColumnName = "TAXTATION_PLACE"
            dtTemp.Columns(33).ColumnName = "IS_DEDUCT_NAME"
            dtTemp.Columns(34).ColumnName = "DEDUCT_REG"
            dtTemp.Columns(35).ColumnName = "DEDUCT_FROM"
            dtTemp.Columns(36).ColumnName = "DEDUCT_TO"
            dtTemp.Columns(37).ColumnName = "BIRTH_CODE"
            dtTemp.Columns(38).ColumnName = "QUYEN"
            dtTemp.Columns(39).ColumnName = "BIRTH_NATION_ID_NAME"
            dtTemp.Columns(40).ColumnName = "BIRTH_PROVINCE_ID_NAME"
            dtTemp.Columns(41).ColumnName = "BIRTH_DISTRICT_ID_NAME"
            dtTemp.Columns(42).ColumnName = "BIRTH_WARD_ID_NAME"
            dtTemp.Columns(43).ColumnName = "REMARK"
            dtTemp.Columns(44).ColumnName = "RELATION_ID"
            dtTemp.Columns(45).ColumnName = "GENDER_ID"
            dtTemp.Columns(46).ColumnName = "IS_SAME_COMPANY_ID"
            dtTemp.Columns(47).ColumnName = "IS_OWNER_ID"
            dtTemp.Columns(48).ColumnName = "NATION_ID"
            dtTemp.Columns(49).ColumnName = "AD_PROVINCE_ID"
            dtTemp.Columns(50).ColumnName = "AD_DISTRICT_ID"
            dtTemp.Columns(51).ColumnName = "AD_WARD_ID"
            dtTemp.Columns(52).ColumnName = "TT_NATION_ID"
            dtTemp.Columns(53).ColumnName = "TT_PROVINCE_ID"
            dtTemp.Columns(54).ColumnName = "TT_DISTRICT_ID"
            dtTemp.Columns(55).ColumnName = "TT_WARD_ID"
            dtTemp.Columns(56).ColumnName = "IS_PASS"
            dtTemp.Columns(57).ColumnName = "IS_DEDUCT"
            dtTemp.Columns(58).ColumnName = "BIRTH_NATION_ID"
            dtTemp.Columns(59).ColumnName = "BIRTH_PROVINCE_ID"
            dtTemp.Columns(60).ColumnName = "BIRTH_DISTRICT_ID"
            dtTemp.Columns(61).ColumnName = "BIRTH_WARD_ID"



            dtTemp.Rows(0).Delete()
            dtTemp.Rows(1).Delete()
            dtTemp.Rows(2).Delete()

            ' add Log
            Dim _error As Boolean = True
            Dim count As Integer
            Dim newRow As DataRow
            Dim empId As Integer
            Dim startDate As Date
            Dim rep As New ProfileBusinessRepository
            Dim result As Integer

            If dtLogs Is Nothing Then
                dtLogs = New DataTable("data")
                dtLogs.Columns.Add("STT", GetType(Integer))
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
                newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
                newRow("STT") = rows("STT")
                empId = rep.CheckEmployee_Exits(rows("EMPLOYEE_CODE"))

                If empId = 0 Then
                    newRow("DISCIPTION") = "Mã nhân viên - Không tồn tại,"
                    _error = False
                Else
                    rows("EMPLOYEE_CODE") = empId
                End If

                If IsDBNull(rows("FULLNAME")) OrElse rows("FULLNAME") = "" Then
                    rows("FULLNAME") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Họ và tên người thân - Bắt buộc nhập,"
                    _error = False
                End If

                If IsDBNull(rows("RELATION_ID")) OrElse rows("RELATION_ID") Is Nothing Then
                    rows("RELATION_ID") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Mối quan hệ - Bắt buộc chọn,"
                    _error = False
                End If

                If IsDBNull(rows("GENDER_ID")) OrElse rows("GENDER_ID") Is Nothing Then
                    rows("GENDER_ID") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Giới tính - Bắt buộc chọn,"
                    _error = False
                End If

                If (IsDBNull(rows("BIRTH_DATE")) OrElse rows("BIRTH_DATE") = "") OrElse CheckDate(rows("BIRTH_DATE"), startDate) = False Then
                    rows("BIRTH_DATE") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày sinh - Không đúng định dạng,"
                    _error = False
                End If

                If (Not IsDBNull(rows("ID_NO_DATE"))) AndAlso CheckDate(rows("ID_NO_DATE"), startDate) = False Then
                    rows("ID_NO_DATE") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày cấp CMND - Không đúng định dạng,"
                    _error = False
                End If

                If (Not IsDBNull(rows("TAXTATION_DATE"))) AndAlso CheckDate(rows("TAXTATION_DATE"), startDate) = False Then
                    rows("TAXTATION_DATE") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày cấp MST - Không đúng định dạng,"
                    _error = False
                End If

                If (Not IsDBNull(rows("IS_DEDUCT"))) AndAlso rows("IS_DEDUCT") <> "0" Then
                    If (IsDBNull(rows("DEDUCT_REG")) OrElse rows("DEDUCT_REG") = "") OrElse CheckDate(rows("DEDUCT_REG"), startDate) = False Then
                        rows("DEDUCT_REG") = "NULL"
                        newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày đăng ký giảm trừ - Không đúng định dạng,"
                        _error = False
                    End If
                    If (IsDBNull(rows("DEDUCT_FROM")) OrElse rows("DEDUCT_FROM") = "") OrElse CheckDate(rows("DEDUCT_FROM"), startDate) = False Then
                        rows("DEDUCT_FROM") = "NULL"
                        newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày bắt đầu giảm trừ - Không đúng định dạng,"
                        _error = False
                    End If
                    'If (IsDBNull(rows("DEDUCT_TO")) OrElse rows("DEDUCT_TO") = "") OrElse CheckDate(rows("DEDUCT_TO"), startDate) = False Then
                    '    rows("DEDUCT_TO") = "NULL"
                    '    newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày kết thúc giảm trừ - Không đúng định dạng,"
                    '    _error = False
                    'End If
                End If


                If _error = False Then
                    dtLogs.Rows.Add(newRow)
                    _error = True
                End If
                count += 1
            Next

            dtTemp.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function CheckDate(ByVal value As String, ByRef result As Date) As Boolean
        Dim dateCheck As Boolean
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
#End Region

End Class