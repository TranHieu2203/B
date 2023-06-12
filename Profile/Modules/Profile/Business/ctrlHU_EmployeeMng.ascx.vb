Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_EmployeeMng
    Inherits Common.CommonView

    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

    Private TYPEREPORT As ArrayList

#Region "Properties"
    Private Property dtLogs As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLogs")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtLogs") = value
        End Set
    End Property

    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String

#End Region

#Region "Page"
    ''' <summary>
    ''' Hiển thị thông tin trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        'Edit by: ChienNV 
        'Trước khi Load thì kiểm tra PostBack
        If Not IsPostBack Then
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try

                Dim startTime As DateTime = DateTime.UtcNow
                ctrlOrganization.AutoPostBack = True
                ctrlOrganization.LoadDataAfterLoaded = True
                ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
                ctrlOrganization.CheckBoxes = TreeNodeTypes.None
                rgEmployeeList.SetFilter()
                Refresh()
                _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                'DisplayException(Me.ViewName, Me.ID, ex)
                _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            End Try
        Else
            Exit Sub
        End If

    End Sub
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo các thiết lập, giá trị các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            If Not IsPostBack Then
                ViewConfig(LeftPane)
                ViewConfig(RadPane1)
                'ViewConfig(RadPane4)
                GirdConfig(rgEmployeeList)
            End If
            'rgEmployeeList.SetFilter()
            'rgEmployeeList.AllowCustomPaging = True
            'rgEmployeeList.ClientSettings.EnablePostBackOnRowClick = False
            rgEmployeeList.SetFilter()
            rgEmployeeList.AllowCustomPaging = True
            rgEmployeeList.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' khởi tạo các thành phần trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.View,
                                       ToolbarItem.Export,
                                       ToolbarItem.Print)
            CType(MainToolBar.Items(2), RadToolBarButton).Text = "View"
            CType(MainToolBar.Items(4), RadToolBarButton).Text = "In CV"
            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_NEXT,
            '                                     ToolbarIcons.Export,
            '                                     ToolbarAuthorize.Export,
            '                                     Translate("Xuất file mẫu")))

            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_IMPORT,
            '                                     ToolbarIcons.Import,
            '                                     ToolbarAuthorize.Import,
            '                                     Translate("Nhập file mẫu")))
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    ''' <summary>
    ''' Edit by: ChienNV;
    ''' Fill data in control cboPrintSupport
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Try
            'GetDataCombo()
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Message = CommonMessage.ACTION_UPDATED Then
                rgEmployeeList.Rebind()
            End If
            'If Not IsPostBack Then
            '    If Not LogHelper.CurrentUser.USERNAME.ToUpper.Equals("ADMIN") Then
            '        CType(MainToolBar.Items(4), RadToolBarButton).Visible = False
            '        CType(MainToolBar.Items(5), RadToolBarButton).Visible = False
            '    End If
            'End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Event"

    ''' <summary>
    ''' Xử lý sự kiện click khi click btnSearch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rdToDate.SelectedDate IsNot Nothing AndAlso rdFromDates.SelectedDate IsNot Nothing Then
                If rdToDate.SelectedDate < rdFromDates.SelectedDate Then
                    ShowMessage("Đến ngày phải lớn hơn Từ ngày", NotifyType.Warning)
                    Exit Sub
                End If
            End If
            rgEmployeeList.CurrentPageIndex = 0
            rgEmployeeList.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Xử lý sự kiện SelectedNodeChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If IsPostBack Then
                rgEmployeeList.CurrentPageIndex = 0
                rgEmployeeList.Rebind()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Xử lý sự kiện Command khi click item trên toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim objEmployee As New EmployeeDTO
            Dim rep As New ProfileBusinessRepository
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_DELETE
                    'Kiểm tra các điều kiện để xóa.
                    If rgEmployeeList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        Dim dsData As New DataSet
                        dtData.TableName = "Table"
                        dsData.Tables.Add(dtData)
                        If dtData.Rows.Count > 0 Then
                            dtData.Columns.Add("BIRTH_DATE_EX", GetType(String))
                            dtData.Columns.Add("ID_DATE_EX", GetType(String))
                            dtData.Columns.Add("JOIN_DATE_EX", GetType(String))
                            dtData.Columns.Add("JOIN_DATE_STATE_EX", GetType(String))
                            For Each row As DataRow In dtData.Rows
                                If Not IsDBNull(row("BIRTH_DATE")) AndAlso IsDate(row("BIRTH_DATE")) Then
                                    row("BIRTH_DATE_EX") = CType(row("BIRTH_DATE"), Date).ToString("dd/MM/yyyy")
                                End If
                                If Not IsDBNull(row("ID_DATE")) AndAlso IsDate(row("ID_DATE")) Then
                                    row("ID_DATE_EX") = CType(row("ID_DATE"), Date).ToString("dd/MM/yyyy")
                                End If
                                If Not IsDBNull(row("JOIN_DATE")) AndAlso IsDate(row("JOIN_DATE")) Then
                                    row("JOIN_DATE_EX") = CType(row("JOIN_DATE"), Date).ToString("dd/MM/yyyy")
                                End If
                                If Not IsDBNull(row("JOIN_DATE_STATE")) AndAlso IsDate(row("JOIN_DATE_STATE")) Then
                                    row("JOIN_DATE_STATE_EX") = CType(row("JOIN_DATE_STATE"), Date).ToString("dd/MM/yyyy")
                                End If
                            Next
                            'rgEmployeeList.ExportExcel(Server, Response, dtData, "EmployeeList")
                            ExportTemplate("Profile\Import\Template_Export_Employee.xls",
                                                  dsData, Nothing, "EmployeeList" & Format(Date.Now, "yyyymmdd"))
                        Else
                            ShowMessage(Translate(MESSAGE_WARNING_EXPORT_EMPTY), Utilities.NotifyType.Warning)
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_PRINT
                    Print_CV()
                Case Common.CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.isMultiple = False
                    ctrlUpload1.Show()
                Case Common.CommonMessage.TOOLBARITEM_NEXT
                    Dim repo As New ProfileRepository
                    Dim dataSet As New DataSet
                    Dim dtVariable As New DataTable
                    Dim tempPath = "~/ReportTemplates//Profile//Import//Template_Import_Import_du_lieu_nhan_vien_HSV.xls"
                    If Not File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath))) Then
                        ShowMessage(Translate("Không tồn tại file template"), Framework.UI.Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim dsDanhMuc As DataSet
                    Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                    .IS_DISSOLVE = ctrlOrganization.IsDissolve}
                    dsDanhMuc = repo.EXPORT_CV(_param)

                    Using xls As New AsposeExcelCommon
                        Dim bCheck = xls.ExportExcelTemplate(
                          System.IO.Path.Combine(Server.MapPath(tempPath)), "Import_NhanVien" & Format(Date.Now, "yyyyMMddHHmmss"), dsDanhMuc, Nothing, Response)
                    End Using
            End Select
            rep.Dispose()
            ' UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Xử lý sự kiện item databound
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmployeeList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployeeList.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Xử lý sự kiện ButtonCommand Yes/No của ctrlMessageBox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim strError As String = ""

                DeleteEmployee(strError)
                If strError = "" Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                Else
                    ShowMessage(Translate("Nhân viên đã có hợp đồng " & strError.Substring(1, strError.Length - 1) & " không thực hiện được thao tác này."), Utilities.NotifyType.Error)
                End If
                Refresh(ACTION_UPDATED)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource của rad grid rgEmployeeList
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEmployeeList.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            CreateDataFilter()

        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' RadGrid_PageIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgEmployeeList.PageIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' AjaxManager_AjaxRequest
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
                rgEmployeeList.CurrentPageIndex = 0
                rgEmployeeList.Rebind()
                If rgEmployeeList.Items IsNot Nothing AndAlso rgEmployeeList.Items.Count > 0 Then
                    rgEmployeeList.Items(0).Selected = True
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"
    ''' <summary>
    ''' Hàm xử lý tạo dữ liệu filter
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim EmployeeList As List(Of EmployeeDTO)
        Dim MaximumRows As Integer
        Dim Sorts As String
        Dim _filter As New EmployeeDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Using rep As New ProfileBusinessRepository
                If ctrlOrganization.CurrentValue IsNot Nothing Then
                    _filter.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
                Else
                    rgEmployeeList.DataSource = New List(Of EmployeeDTO)
                    Exit Function
                End If

                Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                .IS_DISSOLVE = ctrlOrganization.IsDissolve}


                SetValueObjectByRadGrid(rgEmployeeList, _filter)

                If rdFromDates.SelectedDate IsNot Nothing Then
                    _filter.FROM_DATE = rdFromDates.SelectedDate
                End If

                If rdToDate.SelectedDate IsNot Nothing Then
                    _filter.TO_DATE = rdToDate.SelectedDate
                End If

                _filter.IS_TER = chkTerminate.Checked
                _filter.GHI_CHU_SUC_KHOE = txtGhiChu.Text
                Sorts = rgEmployeeList.MasterTableView.SortExpressions.GetSortString()
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetListEmployeePaging(_filter, _param, Sorts).ToTable()
                    Else
                        Return rep.GetListEmployeePaging(_filter, _param).ToTable()
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        EmployeeList = rep.GetListEmployeePaging(_filter, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows, _param, Sorts)
                    Else
                        EmployeeList = rep.GetListEmployeePaging(_filter, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows, _param)
                    End If

                    rgEmployeeList.VirtualItemCount = MaximumRows
                    rgEmployeeList.DataSource = EmployeeList
                End If


            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <summary>
    ''' Xử lý sự kiện xóa nhân viên
    ''' </summary>
    ''' <param name="strError"></param>
    ''' <remarks></remarks>
    Private Sub DeleteEmployee(ByRef strError As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileBusinessRepository

            'Kiểm tra các điều kiện trước khi xóa
            Dim lstEmpID As New List(Of Decimal)
            For Each dr As Telerik.Web.UI.GridDataItem In rgEmployeeList.SelectedItems
                lstEmpID.Add(dr.GetDataKeyValue("ID"))
            Next
            'Xóa nhân viên.
            rep.DeleteEmployee(lstEmpID, strError)
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Xử lý action in cv
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Print_CV()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dsData As DataSet
        Dim rp As New ProfileStoreProcedure
        Dim IDEMPLOYEE As Decimal
        Dim reportName As String = String.Empty
        Dim reportNameOut As String = "String.Empty"
        Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")
        Try
            If rgEmployeeList.SelectedItems.Count = 0 Then
                ShowMessage(Translate("Bạn phải chọn nhân viên trước khi in ."), Utilities.NotifyType.Warning)
                Exit Sub
            End If
            If rgEmployeeList.SelectedItems.Count > 1 Then
                ShowMessage(Translate("Chỉ được chọn 1 bản ghi để in ."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            For Each dr As Telerik.Web.UI.GridDataItem In rgEmployeeList.SelectedItems
                IDEMPLOYEE = Decimal.Parse(dr.GetDataKeyValue("ID").ToString())
            Next
            dsData = rp.PRINT_CV(IDEMPLOYEE)

            If dsData Is Nothing AndAlso dsData.Tables(0) IsNot Nothing Then
                ShowMessage("Không có dữ liệu in báo cáo", NotifyType.Warning)
                Exit Sub
            End If

            If Not File.Exists(Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\EmployeeImage\" + dsData.Tables(0).Rows(0)("IMAGE")) Then
                'dsData.Tables(0).Rows(0)("IMAGE") = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\UploadFile\" + "NoImage.jpg"
            Else
                'Delete file trong thu muc tam
                'DeleteDirectory(Server.MapPath("~/RadUploadTemp"))
                'DeleteDirectory(Server.MapPath("~/EmployeeImageTemp"))


                Dim tempPathFile = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\EmployeeImage\"
                Dim Image = dsData.Tables(0).Rows(0)("IMAGE")
                Dim target As String = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\EmployeeImageTemp\"
                Dim tempUpload As String = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\RadUploadTemp\"

                If Not Directory.Exists(target) Then
                    Directory.CreateDirectory(target)
                Else
                    If System.IO.File.Exists(target + "\" + Image) Then
                        System.IO.File.Delete(target + "\" + Image)
                    End If
                End If

                If Not Directory.Exists(tempUpload) Then
                    Directory.CreateDirectory(tempUpload)
                Else
                    If System.IO.File.Exists(tempUpload + "\" + Image) Then
                        System.IO.File.Delete(tempUpload + "\" + Image)
                    End If
                End If

                Dim file = New FileInfo(tempPathFile + "\" + Image)

                Try
                    file.CopyTo(Path.Combine(tempUpload + "\" + Image), True)
                Catch ex As Exception
                    ShowMessage(Translate("Bạn vui lòng xuất lại CV sau 2 phút"), Utilities.NotifyType.Warning)
                    Exit Sub
                End Try

                file.IsReadOnly = False

                Dim originalImage = System.Drawing.Image.FromFile(Path.Combine(tempUpload, Image))
                Dim width = originalImage.Width
                Dim height = originalImage.Height
                Dim ratio = width / height
                Dim newHeight = Math.Floor(90 / ratio)
                Dim thumbnail As New Bitmap(90, Int32.Parse(newHeight))
                Using g As Graphics = Graphics.FromImage(DirectCast(thumbnail, System.Drawing.Image))
                    g.DrawImage(originalImage, 0, 0, 90, Int32.Parse(newHeight))
                End Using
                Dim cfileName = Image
                Dim fileName = System.IO.Path.Combine(target, cfileName)
                If Not Directory.Exists(target) Then
                    Directory.CreateDirectory(target)
                End If
                thumbnail.Save(fileName)

                thumbnail.Dispose()
                originalImage.Dispose()

                dsData.Tables(0).Rows(0)("IMAGE") = Server.MapPath("~/EmployeeImageTemp") + "\" + dsData.Tables(0).Rows(0)("IMAGE")
            End If


            If File.Exists(Server.MapPath("~/ReportTemplates/Profile/LocationInfo/" + dsData.Tables(0).Rows(0)("ATTACH_FILE_LOGO") + dsData.Tables(0).Rows(0)("FILE_LOGO"))) Then


                Dim tempPathFile = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/" + dsData.Tables(0).Rows(0)("ATTACH_FILE_LOGO"))
                Dim Image = dsData.Tables(0).Rows(0)("FILE_LOGO")
                Dim target As String = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\EmployeeImageTemp\"
                Dim tempUpload As String = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\RadUploadTemp\"

                If Not Directory.Exists(target) Then
                    Directory.CreateDirectory(target)
                Else
                    If System.IO.File.Exists(target + "\" + Image) Then
                        System.IO.File.Delete(target + "\" + Image)
                    End If
                End If

                If Not Directory.Exists(tempUpload) Then
                    Directory.CreateDirectory(tempUpload)
                Else
                    If System.IO.File.Exists(tempUpload + "\" + Image) Then
                        System.IO.File.Delete(tempUpload + "\" + Image)
                    End If
                End If

                Dim file = New FileInfo(tempPathFile + "\" + Image)

                Try
                    file.CopyTo(Path.Combine(tempUpload + "\" + Image), True)
                Catch ex As Exception
                    ShowMessage(Translate("Bạn vui lòng xuất lại CV sau 2 phút"), Utilities.NotifyType.Warning)
                    Exit Sub
                End Try

                file.IsReadOnly = False

                Dim originalImage = System.Drawing.Image.FromFile(Path.Combine(tempUpload, Image))
                Dim ratio = originalImage.Width / originalImage.Height
                Dim height = Convert.ToInt32(150 / ratio)
                Dim thumbnail As New Bitmap(150, height)
                Using g As Graphics = Graphics.FromImage(DirectCast(thumbnail, System.Drawing.Image))
                    g.DrawImage(originalImage, 0, 0, 150, height)
                End Using
                Dim cfileName = Image
                Dim fileName = System.IO.Path.Combine(target, cfileName)
                If Not Directory.Exists(target) Then
                    Directory.CreateDirectory(target)
                End If
                thumbnail.Save(fileName)

                thumbnail.Dispose()
                originalImage.Dispose()

                dsData.Tables(0).Rows(0)("FILE_LOGO") = Server.MapPath("~/EmployeeImageTemp") + "\" + dsData.Tables(0).Rows(0)("FILE_LOGO")
            End If
            If File.Exists(Server.MapPath("~/ReportTemplates/Profile/LocationInfo/" + dsData.Tables(0).Rows(0)("ATTACH_FILE_FOOTER") + dsData.Tables(0).Rows(0)("FILE_FOOTER"))) Then


                Dim tempPathFile = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/" + dsData.Tables(0).Rows(0)("ATTACH_FILE_FOOTER"))
                Dim Image = dsData.Tables(0).Rows(0)("FILE_FOOTER")
                Dim target As String = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\EmployeeImageTemp\"
                Dim tempUpload As String = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\RadUploadTemp\"

                If Not Directory.Exists(target) Then
                    Directory.CreateDirectory(target)
                Else
                    If System.IO.File.Exists(target + "\" + Image) Then
                        System.IO.File.Delete(target + "\" + Image)
                    End If
                End If

                If Not Directory.Exists(tempUpload) Then
                    Directory.CreateDirectory(tempUpload)
                Else
                    If System.IO.File.Exists(tempUpload + "\" + Image) Then
                        System.IO.File.Delete(tempUpload + "\" + Image)
                    End If
                End If

                Dim file = New FileInfo(tempPathFile + "\" + Image)

                Try
                    file.CopyTo(Path.Combine(tempUpload + "\" + Image), True)
                Catch ex As Exception
                    ShowMessage(Translate("Bạn vui lòng xuất lại CV sau 2 phút"), Utilities.NotifyType.Warning)
                    Exit Sub
                End Try

                file.IsReadOnly = False

                Dim originalImage = System.Drawing.Image.FromFile(Path.Combine(tempUpload, Image))
                Dim thumbnail As New Bitmap(850, 95)
                Using g As Graphics = Graphics.FromImage(DirectCast(thumbnail, System.Drawing.Image))
                    g.DrawImage(originalImage, 0, 0, 850, 95)
                End Using
                Dim cfileName = Image
                Dim fileName = System.IO.Path.Combine(target, cfileName)
                If Not Directory.Exists(target) Then
                    Directory.CreateDirectory(target)
                End If
                thumbnail.Save(fileName)

                thumbnail.Dispose()
                originalImage.Dispose()

                dsData.Tables(0).Rows(0)("FILE_FOOTER") = Server.MapPath("~/EmployeeImageTemp") + "\" + dsData.Tables(0).Rows(0)("FILE_FOOTER")
            End If
            If File.Exists(Server.MapPath("~/ReportTemplates/Profile/LocationInfo/" + dsData.Tables(0).Rows(0)("ATTACH_FILE_HEADER") + dsData.Tables(0).Rows(0)("FILE_HEADER"))) Then


                Dim tempPathFile = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/" + dsData.Tables(0).Rows(0)("ATTACH_FILE_HEADER"))
                Dim Image = dsData.Tables(0).Rows(0)("FILE_HEADER")
                Dim target As String = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\EmployeeImageTemp\"
                Dim tempUpload As String = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\RadUploadTemp\"

                If Not Directory.Exists(target) Then
                    Directory.CreateDirectory(target)
                Else
                    If System.IO.File.Exists(target + "\" + Image) Then
                        System.IO.File.Delete(target + "\" + Image)
                    End If
                End If

                If Not Directory.Exists(tempUpload) Then
                    Directory.CreateDirectory(tempUpload)
                Else
                    If System.IO.File.Exists(tempUpload + "\" + Image) Then
                        System.IO.File.Delete(tempUpload + "\" + Image)
                    End If
                End If

                Dim file = New FileInfo(tempPathFile + "\" + Image)

                Try
                    file.CopyTo(Path.Combine(tempUpload + "\" + Image), True)
                Catch ex As Exception
                    ShowMessage(Translate("Bạn vui lòng xuất lại CV sau 2 phút"), Utilities.NotifyType.Warning)
                    Exit Sub
                End Try

                file.IsReadOnly = False

                Dim originalImage = System.Drawing.Image.FromFile(Path.Combine(tempUpload, Image))
                Dim thumbnail As New Bitmap(850, 95)
                Using g As Graphics = Graphics.FromImage(DirectCast(thumbnail, System.Drawing.Image))
                    g.DrawImage(originalImage, 0, 0, 850, 95)
                End Using
                Dim cfileName = Image
                Dim fileName = System.IO.Path.Combine(target, cfileName)
                If Not Directory.Exists(target) Then
                    Directory.CreateDirectory(target)
                End If
                thumbnail.Save(fileName)

                thumbnail.Dispose()
                originalImage.Dispose()

                dsData.Tables(0).Rows(0)("FILE_HEADER") = Server.MapPath("~/EmployeeImageTemp") + "\" + dsData.Tables(0).Rows(0)("FILE_HEADER")
            End If
            dsData.Tables(0).TableName = "DT"
            dsData.Tables(1).TableName = "DT1"
            dsData.Tables(2).TableName = "DT2"
            dsData.Tables(3).TableName = "DT3"
            dsData.Tables(4).TableName = "DT4"
            dsData.Tables(5).TableName = "DT5"
            dsData.Tables(6).TableName = "DT6"
            dsData.Tables(7).TableName = "DT7"
            dsData.Tables(8).TableName = "DT8"
            dsData.Tables(9).TableName = "DT9"
            dsData.Tables(10).TableName = "DT10"
            dsData.Tables(11).TableName = "DT11"
            dsData.Tables(12).TableName = "DT12"
            dsData.Tables(13).TableName = "DT13"
            dsData.Tables(14).TableName = "DT14"
            reportName = "HSNV_CV.doc"
            Dim item As GridDataItem = rgEmployeeList.SelectedItems(0)
            reportNameOut = "CV-" & item.GetDataKeyValue("EMPLOYEE_CODE") & ".doc"
            Dim url = Server.MapPath("~/TemplateDynamic/ProfileSupport/")
            If File.Exists(System.IO.Path.Combine(url, reportName)) Then
                ExportWordMailMergeDS(System.IO.Path.Combine(url, reportName),
                                  reportNameOut,
                                  dsData,
                                  Response)
            Else
                ShowMessage("Mẫu báo cáo không tồn tại", NotifyType.Error)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub GetDataCombo()
        Dim dtData As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            'Using rep As New ProfileRepository
            '    dtData = rep.GetOtherList("PROFILE_SUPPORT")
            '    FillRadCombobox(cboPrintSupport, dtData, "NAME", "ID")
            'End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub DeleteDirectory(ByVal path As String)
        If Directory.Exists(path) Then
            For Each file As String In Directory.GetFiles(path)
                Try
                    System.IO.File.Delete(file)
                Catch ex As Exception
                    Continue For
                End Try
            Next
        End If

    End Sub

    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Import_Data()
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Private Sub Import_Data()
        Try
            Dim rep As New ProfileRepository
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload1.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
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
                    If rep.IMPORT_NV(DocXml) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgEmployeeList.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('IMPORT_CV_ERROR')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        Dim rep As New ProfileBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
            dtTemp.Columns(0).ColumnName = "STT"
            dtTemp.Columns(1).ColumnName = "EMPLOYEE_CODE_OLD"
            dtTemp.Columns(2).ColumnName = "ITIME_ID"
            dtTemp.Columns(3).ColumnName = "FIRST_NAME_VN"
            dtTemp.Columns(4).ColumnName = "LAST_NAME_VN"
            dtTemp.Columns(5).ColumnName = "FULLNAME_VN"
            dtTemp.Columns(6).ColumnName = "PROVINCEEMP_IDN"
            dtTemp.Columns(7).ColumnName = "DISTRICTEMP_IDN"
            dtTemp.Columns(8).ColumnName = "WARDEMP_IDN"
            dtTemp.Columns(9).ColumnName = "GENDERN"
            dtTemp.Columns(10).ColumnName = "OTHER_GENDER"
            dtTemp.Columns(11).ColumnName = "BIRTH_DATE"
            dtTemp.Columns(12).ColumnName = "PROVINCENQ_IDN"
            dtTemp.Columns(13).ColumnName = "BIRTH_PLACEN"
            dtTemp.Columns(14).ColumnName = "BIRTH_PLACE_DETAIL"
            dtTemp.Columns(15).ColumnName = "MARITAL_STATUSN"
            dtTemp.Columns(16).ColumnName = "NATIONALITYN"
            dtTemp.Columns(17).ColumnName = "ID_NO"
            dtTemp.Columns(18).ColumnName = "ID_DATE"
            dtTemp.Columns(19).ColumnName = "EXPIRE_DATE_IDNO"
            dtTemp.Columns(20).ColumnName = "ID_PLACE_CODEN"
            dtTemp.Columns(21).ColumnName = "ID_REMARK"
            dtTemp.Columns(22).ColumnName = "CHIEU_CAO"
            dtTemp.Columns(23).ColumnName = "CAN_NANG"
            dtTemp.Columns(24).ColumnName = "NHOM_MAU"
            dtTemp.Columns(25).ColumnName = "NATIVEN"
            dtTemp.Columns(26).ColumnName = "RELIGIONN"
            dtTemp.Columns(27).ColumnName = "PIT_CODE"
            dtTemp.Columns(28).ColumnName = "PIT_CODE_DATE"
            dtTemp.Columns(29).ColumnName = "PIT_CODE_IDN"
            dtTemp.Columns(30).ColumnName = "PIT_CODE_PLACE"
            dtTemp.Columns(31).ColumnName = "ACADEMYN"
            dtTemp.Columns(32).ColumnName = "LEARNING_LEVELN"
            dtTemp.Columns(33).ColumnName = "MAJORN"
            dtTemp.Columns(34).ColumnName = "GRADUATE_SCHOOLN"
            dtTemp.Columns(35).ColumnName = "GRADUATION_YEAR"
            dtTemp.Columns(36).ColumnName = "IS_CHUHO"
            dtTemp.Columns(37).ColumnName = "NO_HOUSEHOLDS"
            dtTemp.Columns(38).ColumnName = "CODE_HOUSEHOLDS"
            dtTemp.Columns(39).ColumnName = "PER_ADDRESS"
            dtTemp.Columns(40).ColumnName = "PER_PROVINCEN"
            dtTemp.Columns(41).ColumnName = "PER_DISTRICTN"
            dtTemp.Columns(42).ColumnName = "PER_WARDN"
            dtTemp.Columns(43).ColumnName = "NAV_ADDRESS"
            dtTemp.Columns(44).ColumnName = "NAV_PROVINCEN"
            dtTemp.Columns(45).ColumnName = "NAV_DISTRICTN"
            dtTemp.Columns(46).ColumnName = "NAV_WARDN"
            dtTemp.Columns(47).ColumnName = "CHECK_NAV"
            dtTemp.Columns(48).ColumnName = "MOBILE_PHONE"
            dtTemp.Columns(49).ColumnName = "HOME_PHONE"
            dtTemp.Columns(50).ColumnName = "WORK_EMAIL"
            dtTemp.Columns(51).ColumnName = "PER_EMAIL"
            dtTemp.Columns(52).ColumnName = "CONTACT_PER"
            dtTemp.Columns(53).ColumnName = "RELATION_PER_CTRN"
            dtTemp.Columns(54).ColumnName = "CONTACT_PER_PHONE"
            dtTemp.Columns(55).ColumnName = "CONTACT_PER_MBPHONE"
            dtTemp.Columns(56).ColumnName = "CONTACT_PER_IDNO"
            dtTemp.Columns(57).ColumnName = "CONTACT_PER_EFFECT_DATE_IDNO"
            dtTemp.Columns(58).ColumnName = "CONTACT_PER_EXPIRE_DATE_IDNO"
            dtTemp.Columns(59).ColumnName = "CONTACT_PER_PLACE_IDNON"
            dtTemp.Columns(60).ColumnName = "ADDRESS_PER_CTR" 'địa chỉ NLH
            dtTemp.Columns(61).ColumnName = "PASS_NO"
            dtTemp.Columns(62).ColumnName = "PASS_DATE"
            dtTemp.Columns(63).ColumnName = "PASS_EXPIRE"
            dtTemp.Columns(64).ColumnName = "PASS_PLACEN"
            dtTemp.Columns(65).ColumnName = "VISA"
            dtTemp.Columns(66).ColumnName = "VISA_DATE"
            dtTemp.Columns(67).ColumnName = "VISA_EXPIRE"
            dtTemp.Columns(68).ColumnName = "VISA_PLACEN"
            dtTemp.Columns(69).ColumnName = "BOOK_NO_LD_CV" 'BQ
            dtTemp.Columns(70).ColumnName = "BOOK_DATE"
            dtTemp.Columns(71).ColumnName = "BOOK_EXPIRE"
            dtTemp.Columns(72).ColumnName = "SSLD_PLACE_IDN"
            dtTemp.Columns(73).ColumnName = "PERSON_INHERITANCE"
            dtTemp.Columns(74).ColumnName = "BANK_IDN"
            dtTemp.Columns(75).ColumnName = "BANK_BRANCH_IDN"
            dtTemp.Columns(76).ColumnName = "BANK_NO"
            dtTemp.Columns(77).ColumnName = "JOIN_DATE"
            dtTemp.Columns(78).ColumnName = "JOIN_DATE_STATE"
            dtTemp.Columns(79).ColumnName = "SENIORITY_DATE"
            dtTemp.Columns(80).ColumnName = "TER_EFFECT_DATE"
            dtTemp.Columns(81).ColumnName = "ORG_IDN"
            dtTemp.Columns(82).ColumnName = "TITLE_IDN"
            dtTemp.Columns(83).ColumnName = "DIRECT_MANAGER"
            dtTemp.Columns(84).ColumnName = "OBJECT_LABORN"
            dtTemp.Columns(85).ColumnName = "OBJECTTIMEKEEPINGN"
            dtTemp.Columns(86).ColumnName = "OBJECT_EMPLOYEE_IDN"
            dtTemp.Columns(87).ColumnName = "OBJECT_ATTENDANT_IDN"
            dtTemp.Columns(88).ColumnName = "WORK_PLACE_IDN"
            dtTemp.Columns(89).ColumnName = "ID_VUNG"
            dtTemp.Columns(90).ColumnName = "OBJECT_INSN"
            dtTemp.Columns(91).ColumnName = "BOOK_NO"
            dtTemp.Columns(92).ColumnName = "HEALTH_AREA_INS_IDN"
            dtTemp.Columns(93).ColumnName = "CONG_DOAN"
            dtTemp.Columns(94).ColumnName = "DOAN_PHI"
            dtTemp.Columns(95).ColumnName = "CHUC_VU_DOAN"
            dtTemp.Columns(96).ColumnName = "NGAY_VAO_DOAN"
            dtTemp.Columns(97).ColumnName = "NOI_VAO_DOAN"
            dtTemp.Columns(98).ColumnName = "DANG_PHI"
            dtTemp.Columns(99).ColumnName = "CHUC_VU_DANG"
            dtTemp.Columns(100).ColumnName = "NGAY_VAO_DANG"
            dtTemp.Columns(101).ColumnName = "NGAY_VAO_DANG_DB"
            dtTemp.Columns(102).ColumnName = "NOI_VAO_DANG"
            dtTemp.Columns(103).ColumnName = "PROVINCEEMP_ID"
            dtTemp.Columns(104).ColumnName = "DISTRICTEMP_ID"
            dtTemp.Columns(105).ColumnName = "WARDEMP_ID"
            dtTemp.Columns(106).ColumnName = "GENDER"
            dtTemp.Columns(107).ColumnName = "PROVINCENQ_ID"
            dtTemp.Columns(108).ColumnName = "BIRTH_PLACE"
            dtTemp.Columns(109).ColumnName = "MARITAL_STATUS"
            dtTemp.Columns(110).ColumnName = "NATIONALITY"
            dtTemp.Columns(111).ColumnName = "ID_PLACE_CODE"
            dtTemp.Columns(112).ColumnName = "NATIVE"
            dtTemp.Columns(113).ColumnName = "RELIGION"
            dtTemp.Columns(114).ColumnName = "PIT_CODE_ID"
            dtTemp.Columns(115).ColumnName = "ACADEMY"
            dtTemp.Columns(116).ColumnName = "LEARNING_LEVEL"
            dtTemp.Columns(117).ColumnName = "MAJOR"
            dtTemp.Columns(118).ColumnName = "GRADUATE_SCHOOL"
            dtTemp.Columns(119).ColumnName = "PER_PROVINCE"
            dtTemp.Columns(120).ColumnName = "PER_DISTRICT"
            dtTemp.Columns(121).ColumnName = "PER_WARD"
            dtTemp.Columns(122).ColumnName = "NAV_PROVINCE"
            dtTemp.Columns(123).ColumnName = "NAV_DISTRICT"
            dtTemp.Columns(124).ColumnName = "NAV_WARD"
            dtTemp.Columns(125).ColumnName = "RELATION_PER_CTR"
            dtTemp.Columns(126).ColumnName = "CONTACT_PER_PLACE_IDNO"
            dtTemp.Columns(127).ColumnName = "PASS_PLACE"
            dtTemp.Columns(128).ColumnName = "VISA_PLACE"
            dtTemp.Columns(129).ColumnName = "SSLD_PLACE_ID"
            dtTemp.Columns(130).ColumnName = "BANK_ID"
            dtTemp.Columns(131).ColumnName = "BANK_BRANCH_ID"
            dtTemp.Columns(132).ColumnName = "ORG_ID"
            dtTemp.Columns(133).ColumnName = "TITLE_ID"
            dtTemp.Columns(134).ColumnName = "OBJECT_LABOR"
            dtTemp.Columns(135).ColumnName = "OBJECTTIMEKEEPING"
            dtTemp.Columns(136).ColumnName = "OBJECT_EMPLOYEE_ID"
            dtTemp.Columns(137).ColumnName = "OBJECT_ATTENDANT_ID"
            dtTemp.Columns(138).ColumnName = "WORK_PLACE_ID"
            dtTemp.Columns(139).ColumnName = "OBJECT_INS"
            dtTemp.Columns(140).ColumnName = "HEALTH_AREA_INS_ID"
            dtTemp.Columns(141).ColumnName = "VUNGBH_ID"


            'XOA DONG TIEU DE VA HEADER
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(1).Delete()
            dtTemp.Rows(2).Delete()
            ' add Log
            Dim _error As Boolean = True
            Dim newRow As DataRow

            dtLogs = dtTemp.Clone
            dtLogs.TableName = "DATA"

            'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
            Dim rowDel As DataRow
            For i As Integer = 0 To dtTemp.Rows.Count - 1
                If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
                rowDel = dtTemp.Rows(i)
                If rowDel("STT").ToString.Trim = "" And rowDel("FIRST_NAME_VN").ToString.Trim = "" Then
                    dtTemp.Rows(i).Delete()
                End If
            Next

            Dim sError As String
            Dim rep1 As New CommonRepository
            Dim store As New ProfileStoreProcedure
            Dim lstEmp As New List(Of String)

            For Each rows As DataRow In dtTemp.Rows
                If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For

                _error = False
                newRow = dtLogs.NewRow
                newRow("STT") = rows("STT")
                'newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")

                'validate
                'sError = "Mã nhân viên chưa nhập"
                'ImportValidate.EmptyValue("EMPLOYEE_CODE", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("DIRECT_MANAGER", rows, newRow, _error, sError)

                'If rows("EMPLOYEE_CODE").ToString <> "" And newRow("EMPLOYEE_CODE").ToString = "" Then
                '    Dim empCode = rows("EMPLOYEE_CODE").ToString
                '    If Not lstEmp.Contains(empCode) Then
                '        lstEmp.Add(empCode)
                '    Else
                '        _error = True
                '        newRow("EMPLOYEE_CODE") = "Mã nhân viên đã bị trùng trong file import"
                '    End If
                'End If

                'Dim chkCode As New List(Of String)
                'chkCode.Add(rows("EMPLOYEE_CODE").ToString)
                'If Not rep1.CheckExistValue(chkCode, "HU_EMPLOYEE", "EMPLOYEE_CODE") Then
                '    _error = True
                '    newRow("EMPLOYEE_CODE") = "Mã nhân viên đã tồn tại trong hệ thống"
                'End If

                Dim chkCodeDM As New List(Of String)
                If rows("DIRECT_MANAGER") <> "" Then
                    chkCodeDM.Add(rows("DIRECT_MANAGER").ToString)
                    If rep1.CheckExistValue(chkCodeDM, "HU_EMPLOYEE", "EMPLOYEE_CODE") Then
                        _error = True
                        newRow("DIRECT_MANAGER") = "Mã nhân viên không tồn tại trong hệ thống"
                    End If
                End If

                'bắt buộc nhập
                sError = "Chưa nhập dữ liệu"
                ImportValidate.EmptyValue("STT", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("FIRST_NAME_VN", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("LAST_NAME_VN", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("FULLNAME_VN", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("BIRTH_PLACE", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("ID_NO", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("IS_CHUHO", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("NO_HOUSEHOLDS", rows, newRow, _error, sError)

                ImportValidate.EmptyValue("PER_ADDRESS", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("CHECK_NAV", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("MOBILE_PHONE", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("JOIN_DATE", rows, newRow, _error, sError)
                'ImportValidate.EmptyValue("BOOK_NO_LD_CV", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("CONG_DOAN", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("DOAN_PHI", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("DANG_PHI", rows, newRow, _error, sError)


                Dim dt = store.GET_EMP_INF(rows("FULLNAME_VN"), rows("ID_NO"))
                If dt IsNot Nothing AndAlso (dt.Tables(0).Rows.Count > 0 Or dt.Tables(1).Rows.Count > 0) Then
                    _error = True
                    For Each item In dt.Tables(0).Rows
                        Dim newRow1 = dtLogs.NewRow
                        If item("ID_NO") <> "" Then
                            newRow1("ID_NO") = "Số CMNN đã tồn tại trong hệ thống. Trùng với nhân viên: " + item("EMPLOYEE_CODE") + "-" + item("FULLNAME_VN")
                        End If
                        dtLogs.Rows.Add(newRow1)
                    Next

                    For Each item In dt.Tables(1).Rows
                        Dim newRow1 = dtLogs.NewRow
                        If item("FULLNAME_VN") <> "" Then
                            newRow1("FULLNAME_VN") = "Tên nhân viên đã tồn tại trong hệ thống. Trùng với nhân viên: " + item("EMPLOYEE_CODE") + "-" + item("FULLNAME_VN")
                        End If
                        dtLogs.Rows.Add(newRow1)
                    Next
                End If

                'check combo box
                ImportValidate.IsValidList("PROVINCEEMP_IDN", "PROVINCEEMP_ID", rows, newRow, _error, sError)

                If Not CType(CommonConfig.dicConfig("APP_SETTING_5"), Boolean) Then
                    ImportValidate.IsValidList("DISTRICTEMP_IDN", "DISTRICTEMP_ID", rows, newRow, _error, sError)

                End If
                If Not CType(CommonConfig.dicConfig("APP_SETTING_6"), Boolean) Then
                    ImportValidate.IsValidList("WARDEMP_IDN", "WARDEMP_ID", rows, newRow, _error, sError)

                End If


                ImportValidate.IsValidList("GENDERN", "GENDER", rows, newRow, _error, sError)
                ImportValidate.IsValidList("PROVINCENQ_IDN", "PROVINCENQ_ID", rows, newRow, _error, sError)
                ImportValidate.IsValidList("BIRTH_PLACEN", "BIRTH_PLACE", rows, newRow, _error, sError)
                ImportValidate.IsValidList("MARITAL_STATUSN", "MARITAL_STATUS", rows, newRow, _error, sError)
                ImportValidate.IsValidList("NATIONALITYN", "NATIONALITY", rows, newRow, _error, sError)
                ImportValidate.IsValidList("ID_PLACE_CODEN", "ID_PLACE_CODE", rows, newRow, _error, sError)
                ImportValidate.IsValidList("NATIVEN", "NATIVE", rows, newRow, _error, sError)
                ImportValidate.IsValidList("RELIGIONN", "RELIGION", rows, newRow, _error, sError)
                'ImportValidate.IsValidList("PIT_CODE_IDN", "PIT_CODE_ID", rows, newRow, _error, sError)
                ImportValidate.IsValidList("ACADEMYN", "ACADEMY", rows, newRow, _error, sError)
                ImportValidate.IsValidList("LEARNING_LEVELN", "LEARNING_LEVEL", rows, newRow, _error, sError)
                ' ImportValidate.IsValidList("MAJORN", "MAJOR", rows, newRow, _error, sError)
                'ImportValidate.IsValidList("GRADUATE_SCHOOLN", "GRADUATE_SCHOOL", rows, newRow, _error, sError)
                ImportValidate.IsValidList("PER_PROVINCEN", "PER_PROVINCE", rows, newRow, _error, sError)
                ImportValidate.IsValidList("PER_DISTRICTN", "PER_DISTRICT", rows, newRow, _error, sError)
                ImportValidate.IsValidList("PER_WARDN", "PER_WARD", rows, newRow, _error, sError)
                ImportValidate.IsValidList("ORG_IDN", "ORG_ID", rows, newRow, _error, sError)
                ImportValidate.IsValidList("TITLE_IDN", "TITLE_ID", rows, newRow, _error, sError)
                If CommonConfig.APP_SETTING_16() = False Then
                    ImportValidate.IsValidList("OBJECT_LABORN", "OBJECT_LABOR", rows, newRow, _error, sError)
                End If
                ImportValidate.IsValidList("OBJECTTIMEKEEPINGN", "OBJECTTIMEKEEPING", rows, newRow, _error, sError)
                ImportValidate.IsValidList("OBJECT_EMPLOYEE_IDN", "OBJECT_EMPLOYEE_ID", rows, newRow, _error, sError)
                ImportValidate.IsValidList("OBJECT_ATTENDANT_IDN", "OBJECT_ATTENDANT_ID", rows, newRow, _error, sError)
                ImportValidate.IsValidList("WORK_PLACE_IDN", "WORK_PLACE_ID", rows, newRow, _error, sError)
                ImportValidate.IsValidList("OBJECT_INSN", "OBJECT_INS", rows, newRow, _error, sError)
                'ImportValidate.IsValidList("HEALTH_AREA_INS_IDN", "HEALTH_AREA_INS_ID", rows, newRow, _error, sError)
                ImportValidate.IsValidList("ID_VUNG", "VUNGBH_ID", rows, newRow, _error, sError)

                'CHECK DATE
                sError = "Ngày sai định dạng"
                If rows("BIRTH_DATE") Is Nothing Then
                    ImportValidate.IsValidDate("BIRTH_DATE", rows, newRow, _error, sError)
                End If
                If rows("PIT_CODE_DATE") Is Nothing Then
                    ImportValidate.IsValidDate("PIT_CODE_DATE", rows, newRow, _error, sError)
                End If
                If rows("JOIN_DATE") Is Nothing Then
                    ImportValidate.IsValidDate("JOIN_DATE", rows, newRow, _error, sError)
                End If
                If rows("CONTACT_PER_EFFECT_DATE_IDNO") Is Nothing Then
                    ImportValidate.IsValidDate("CONTACT_PER_EFFECT_DATE_IDNO", rows, newRow, _error, sError)
                End If
                If rows("CONTACT_PER_EXPIRE_DATE_IDNO") Is Nothing Then
                    ImportValidate.IsValidDate("CONTACT_PER_EXPIRE_DATE_IDNO", rows, newRow, _error, sError)
                End If
                'If rows("JOIN_DATE") Is Nothing Then
                '    ImportValidate.IsValidDate("JOIN_DATE", rows, newRow, _error, sError)
                'End If
                If rows("PASS_EXPIRE") Is Nothing Then
                    ImportValidate.IsValidDate("PASS_EXPIRE", rows, newRow, _error, sError)
                End If
                If rows("PASS_DATE") Is Nothing Then
                    ImportValidate.IsValidDate("PASS_DATE", rows, newRow, _error, sError)
                End If
                If rows("VISA_EXPIRE") Is Nothing Then
                    ImportValidate.IsValidDate("VISA_EXPIRE", rows, newRow, _error, sError)
                End If
                If rows("VISA_DATE") Is Nothing Then
                    ImportValidate.IsValidDate("VISA_DATE", rows, newRow, _error, sError)
                End If
                If rows("JOIN_DATE_STATE") Is Nothing Then
                    ImportValidate.IsValidDate("JOIN_DATE_STATE", rows, newRow, _error, sError)
                End If
                If rows("SENIORITY_DATE") Is Nothing Then
                    ImportValidate.IsValidDate("SENIORITY_DATE", rows, newRow, _error, sError)
                End If
                If rows("TER_EFFECT_DATE") Is Nothing Then
                    ImportValidate.IsValidDate("TER_EFFECT_DATE", rows, newRow, _error, sError)
                End If
                If rows("NGAY_VAO_DANG_DB") Is Nothing Then
                    ImportValidate.IsValidDate("NGAY_VAO_DANG_DB", rows, newRow, _error, sError)
                End If
                If rows("NGAY_VAO_DANG") Is Nothing Then
                    ImportValidate.IsValidDate("NGAY_VAO_DANG", rows, newRow, _error, sError)
                End If

                If _error Then
                    dtLogs.Rows.Add(newRow)
                    _error = False
                End If
            Next
            dtTemp.AcceptChanges()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
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


End Class