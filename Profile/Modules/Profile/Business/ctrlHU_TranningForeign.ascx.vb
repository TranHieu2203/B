Imports System.IO
Imports Aspose.Words
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
'Imports Ionic.Crc
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_TranningForeign
    Inherits Common.CommonView

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()


#Region "Property"
    ''' <summary>
    ''' Obj ContractDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Contract As TrainningForeignDTO
        Get
            Return ViewState(Me.ID & "_Contract")
        End Get
        Set(ByVal value As TrainningForeignDTO)
            ViewState(Me.ID & "_Contract") = value
        End Set
    End Property
    ''' <summary>
    ''' List obj Contracts
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Contracts As List(Of TrainningForeignDTO)
        Get
            Return ViewState(Me.ID & "_Contracts")
        End Get
        Set(ByVal value As List(Of TrainningForeignDTO))
            ViewState(Me.ID & "_Contracts") = value
        End Set
    End Property
    ''' <summary>
    ''' Insert ContractDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InsertContracts As TrainningForeignDTO
        Get
            Return ViewState(Me.ID & "_InsertContracts")
        End Get
        Set(ByVal value As TrainningForeignDTO)
            ViewState(Me.ID & "_InsertContracts") = value
        End Set
    End Property
    ''' <summary>
    ''' Delete ContractDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeleteContract As TrainningForeignDTO
        Get
            Return ViewState(Me.ID & "_DeleteContract")
        End Get
        Set(ByVal value As TrainningForeignDTO)
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
    ''' Xét các trạng thái của grid rgContract
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            rgContract.AllowCustomPaging = True
            rgContract.SetFilter()
            InitControl()
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                GirdConfig(rgContract)
            End If
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
            Me.MainToolBar = tbarContracts

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Export,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Print,
                                        ToolbarItem.Import
                                       )
            CType(MainToolBar.Items(4), RadToolBarButton).Text = "Xuất file mẫu"
            CType(MainToolBar.Items(4), RadToolBarButton).ImageUrl = CType(MainToolBar.Items(2), RadToolBarButton).ImageUrl
            CType(MainToolBar.Items(5), RadToolBarButton).Text = "nhập file mẫu"
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
    ''' Bind lai du lieu cho grid rgContract
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
                        rgContract.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        rgContract.Rebind()
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
                Case CommonMessage.TOOLBARITEM_PRINT
                    Dim dtData As DataTable
                    Dim dtDataCon As DataTable
                    Dim folderName As String = "ContractSupport"
                    Dim filePath As String = ""
                    Dim extension As String = ""
                    Dim iError As Integer = 0
                    Dim path As String = ""
                    If Not Directory.Exists(AppDomain.CurrentDomain.BaseDirectory & "Zip\") Then
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory & "Zip\")
                    End If
                    Dim dir As String() = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory & "Zip\")
                    If dir.Length > 0 Then
                        For Each f As String In dir
                            Try
                                File.Delete(f)
                            Catch ex As Exception
                            End Try
                        Next
                    End If
                    If Not Directory.Exists(AppDomain.CurrentDomain.BaseDirectory & "Files\") Then
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory & "Files\")
                    End If
                    Dim dir2 As String() = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory & "Files\")
                    If dir2.Length > 0 Then
                        For Each f2 As String In dir2
                            Try
                                File.Delete(f2)
                            Catch ex As Exception
                            End Try
                        Next
                    End If

                    Dim item = rgContract.SelectedItems

                    Dim lstIDs As String = ""
                    For idx = 0 To rgContract.SelectedItems.Count - 1
                        If idx <> rgContract.SelectedItems.Count - 1 Then
                            Dim value As GridDataItem = rgContract.SelectedItems(idx)
                            lstIDs = lstIDs & value.GetDataKeyValue("ID") & ","
                        Else
                            Dim value As GridDataItem = rgContract.SelectedItems(idx)
                            lstIDs = lstIDs & value.GetDataKeyValue("ID")
                        End If
                    Next
                    'check loại hợp đồng cùng loại
                    Using rep As New ProfileRepository
                        dtDataCon = rep.GetCheckContractTypeID(lstIDs)
                        If dtDataCon.Rows(0)(0) = 2 Then
                            ShowMessage("Các bản ghi không cùng loại hợp đồng !", NotifyType.Warning)
                            Exit Sub
                        End If
                        If folderName = "" Then
                            ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    Dim icheck As GridDataItem = item.Item(0)
                    ' Kiểm tra file theo thông tin trong database
                    If Not Utilities.GetTemplateLinkFile(icheck.GetDataKeyValue("CONTRACTTYPE_CODE"),
                                                         folderName,
                                                         filePath,
                                                         extension,
                                                         iError) Then
                        Select Case iError
                            Case 1
                                ShowMessage("Biểu mẫu không tồn tại", NotifyType.Warning)
                                Exit Sub
                        End Select
                    End If

                    Dim lstID As String = ""
                    For Each i As GridDataItem In item
                        lstID &= "|" & i.GetDataKeyValue("ID").ToString() & "|"
                    Next

                    Using rep As New ProfileRepository
                        dtData = rep.GetHU_DataDynamicContract(lstID,
                                                       ProfileCommon.HU_TEMPLATE_TYPE.CONTRACT_ID,
                                                       folderName)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                        If folderName = "" Then
                            ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    If item.Count = 1 Then
                        'Export file mẫu
                        Using word As New WordCommon
                            word.ExportMailMerge(filePath,
                                                 icheck.GetDataKeyValue("EMPLOYEE_CODE") & "_HDLD_" & _
                                                 Format(Date.Now, "yyyyMMddHHmmss") & extension,
                                                 dtData,
                                                 Response)
                        End Using
                    Else
                        For lst = 0 To rgContract.SelectedItems.Count - 1
                            Dim item1 As GridDataItem = rgContract.SelectedItems(lst)
                            Dim fileName As String = item1.GetDataKeyValue("EMPLOYEE_CODE") & "_HDLD_" & _
                                                     Format(Date.Now, "yyyyMMddHHmmss") & lst & extension
                            Dim doc As New Document(filePath)
                            doc.MailMerge.Execute(dtData.Rows(lst))
                            path = AppDomain.CurrentDomain.BaseDirectory & "Files\"
                            'path = "Files\"
                            If Not Directory.Exists(path) Then
                                Directory.CreateDirectory(path)
                            End If
                            doc.Save(path & fileName)
                        Next
                        ZipFiles(path)
                    End If

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgContract.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    ElseIf rgContract.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim item As GridDataItem = rgContract.SelectedItems(0)
                    If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.OT_CONTRACT_STATUS.APPROVE_ID Then
                        ShowMessage(Translate("Bản ghi đã phê duyệt. Thao tác thực hiện không thành công"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.OT_CONTRACT_STATUS.NOT_APPROVE_ID Then
                        ShowMessage(Translate("Bản ghi không phê duyệt. Thao tác thực hiện không thành công"), NotifyType.Warning)
                        Exit Sub
                    End If

                    DeleteContract = New TrainningForeignDTO With {.ID = Decimal.Parse(item("ID").Text),
                                                           .EMPLOYEE_ID = Decimal.Parse(item("EMPLOYEE_ID").Text)}
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
                            rgContract.ExportExcel(Server, Response, dtData, "Title")
                            Exit Sub
                        End If
                    End Using
            End Select

            'UpdateControlState()
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
                UpdateControlState()
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
            rgContract.CurrentPageIndex = 0
            rgContract.MasterTableView.SortExpressions.Clear()
            rgContract.Rebind()
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
    ''' Thiets lap trang thai cho rgContract
    ''' Bind lai du lieu cho rgContract
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgContract.CurrentPageIndex = 0
            rgContract.MasterTableView.SortExpressions.Clear()
            rgContract.Rebind()
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
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgContract.NeedDataSource
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

    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Click cua btnPrintSupport
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub btnPrintSupport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintSupport.Click
    '    Dim validate As New OtherListDTO
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If cboPrintSupport.SelectedValue = "" Then
    '            ShowMessage(Translate("Bạn chưa chọn biểu mẫu"), NotifyType.Warning)
    '            Exit Sub
    '        End If
    '        Using rep As New ProfileRepository
    '            validate.ID = cboPrintSupport.SelectedValue
    '            validate.ACTFLG = "A"
    '            validate.CODE = ProfileCommon.CONTRACT_SUPPORT.Code
    '            If Not rep.ValidateOtherList(validate) Then
    '                ShowMessage(Translate("Biểu mẫu không tồn tại hoặc đã ngừng áp dụng."), NotifyType.Warning)
    '                ClearControlValue(cboPrintSupport)
    '                GetDataCombo()
    '                Exit Sub
    '            End If
    '        End Using
    '        Dim dtData As DataTable
    '        Dim folderName As String = ""
    '        Dim filePath As String = ""
    '        Dim extension As String = ""
    '        Dim iError As Integer = 0
    '        Dim strId As String = ""
    '        For Each item As GridDataItem In rgContract.SelectedItems
    '            strId = strId & item.GetDataKeyValue("ID") & ","
    '        Next
    '        strId = strId.Substring(0, strId.Length - 1) 'Loai bỏ kí tự , cuối cùng
    '        ' Kiểm tra + lấy thông tin trong database
    '        Using rep As New ProfileRepository
    '            dtData = rep.GetHU_MultyDataDynamic(strId,
    '                                           ProfileCommon.HU_TEMPLATE_TYPE.CONTRACT_SUPPORT_ID,
    '                                           folderName)
    '            If dtData.Rows.Count = 0 Then
    '                ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
    '                Exit Sub
    '            End If
    '            If folderName = "" Then
    '                ShowMessage(Translate("Thư mục không tồn tại"), NotifyType.Warning)
    '                Exit Sub
    '            End If
    '        End Using

    '        If dtData.Columns.Contains("EMPLOYEE_NAME_EN") Then
    '            For i As Int32 = 0 To dtData.Rows.Count - 1
    '                dtData.Rows(i)("EMPLOYEE_NAME_EN") = Utilities.RemoveUnicode(dtData.Rows(i)("EMPLOYEE_NAME_EN").ToString)
    '            Next
    '        End If

    '        ' Kiểm tra file theo thông tin trong database
    '        If Not Utilities.GetTemplateLinkFile(cboPrintSupport.SelectedValue,
    '                                             folderName,
    '                                             filePath,
    '                                             extension,
    '                                             iError) Then
    '            Select Case iError
    '                Case 1
    '                    ShowMessage("Biểu mẫu không tồn tại", NotifyType.Warning)
    '                    Exit Sub
    '            End Select
    '        End If
    '        ' Export file mẫu
    '        If rgContract.SelectedItems.Count = 1 Then
    '            Dim item As GridDataItem = rgContract.SelectedItems(0)
    '            Using word As New WordCommon
    '                word.ExportMailMerge(filePath,
    '                                     item.GetDataKeyValue("EMPLOYEE_CODE") & "_" & cboPrintSupport.Text & "_" & Format(Date.Now, "yyyyMMddHHmmss"),
    '                                     dtData,
    '                                     Response)
    '            End Using
    '        Else
    '            Dim lstFile As List(Of String) = Utilities.SaveMultyFile(dtData, filePath, cboPrintSupport.Text)
    '            Using zip As New ZipFile
    '                zip.AlternateEncodingUsage = ZipOption.AsNecessary
    '                zip.AddDirectoryByName("Files")
    '                For i As Integer = 0 To lstFile.Count - 1
    '                    Dim file As System.IO.FileInfo = New System.IO.FileInfo(lstFile(i))
    '                    If file.Exists Then
    '                        zip.AddFile(file.FullName, "Files")
    '                    End If
    '                Next
    '                Response.Clear()

    '                Dim zipName As String = [String].Format("{0}_{1}.zip", cboPrintSupport.Text, DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"))
    '                Response.ContentType = "application/zip"
    '                Response.AddHeader("content-disposition", "attachment; filename=" + zipName)
    '                zip.Save(Response.OutputStream)
    '                Response.Flush()
    '                Response.SuppressContent = True
    '                HttpContext.Current.ApplicationInstance.CompleteRequest()
    '            End Using
    '            For i As Integer = 0 To lstFile.Count - 1
    '                'Delete files
    '                Dim file As System.IO.FileInfo = New System.IO.FileInfo(lstFile(i))
    '                If file.Exists Then
    '                    file.Delete()
    '                End If
    '            Next
    '        End If

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
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
    ''' Cap nhat trang thai tbarContracts, rgContract, ctrlOrg
    ''' Cap nhat trang thai hien tai, neu trang thai hien tai cua trang la trang thai xoa
    ''' thi xoa hop dong vaf lam moi lai UpdateView
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            tbarContracts.Enabled = True
            rgContract.Enabled = True
            ctrlOrg.Enabled = True
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    If rep.DeleteTrainingForeign(DeleteContract) Then
                        DeleteContract = Nothing
                        IDSelect = Nothing
                        Refresh("UpdateView")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
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
        Dim _filter As New TrainningForeignDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If ctrlOrg.CurrentValue Is Nothing Then
                rgContract.DataSource = New List(Of TrainningForeignDTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

            If rdFromDate.SelectedDate IsNot Nothing Then
                _filter.FROM_DATE = rdFromDate.SelectedDate
            End If
            If rdToDate.SelectedDate IsNot Nothing Then
                _filter.TO_DATE = rdToDate.SelectedDate
            End If
            _filter.IS_TER = chkTerminate.Checked
            SetValueObjectByRadGrid(rgContract, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgContract.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetTrainingForeign(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetTrainingForeign(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.Contracts = rep.GetTrainingForeign(_filter, rgContract.CurrentPageIndex, rgContract.PageSize, MaximumRows, _param, Sorts)
                Else
                    Me.Contracts = rep.GetTrainingForeign(_filter, rgContract.CurrentPageIndex, rgContract.PageSize, MaximumRows, _param)
                End If

                rgContract.VirtualItemCount = MaximumRows
                rgContract.DataSource = Me.Contracts
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