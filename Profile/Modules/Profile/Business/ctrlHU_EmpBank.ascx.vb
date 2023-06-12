Imports System.IO
Imports Aspose.Cells
Imports Aspose.Words
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
'Imports Ionic.Crc
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_EmpBank
    Inherits Common.CommonView

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()


#Region "Property"
    Property dtDataImportContract As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportContract")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportContract") = value
        End Set
    End Property
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("FULLNAME_VN", GetType(String))

                dt.Columns.Add("CONTRACT_TYPE_NAME", GetType(String))
                dt.Columns.Add("CONTRACT_TYPE_ID", GetType(String))
                dt.Columns.Add("START_DATE", GetType(String))
                dt.Columns.Add("END_DATE", GetType(String))
                dt.Columns.Add("CONTRACT_TYPE_CODE", GetType(String))


                dt.Columns.Add("WORKING_ID", GetType(String))
                dt.Columns.Add("WORK_PLACE_NAME", GetType(String))
                dt.Columns.Add("WORK_PLACE_ID", GetType(String))

                dt.Columns.Add("STATUS_NAME", GetType(String))
                dt.Columns.Add("STATUS_ID", GetType(String))
                dt.Columns.Add("WORK_TIME", GetType(String))
                dt.Columns.Add("WORK_DECRIPTION", GetType(String))
                dt.Columns.Add("CONTRACT_NO", GetType(String))

                dt.Columns.Add("SIGN_DATE", GetType(String))
                dt.Columns.Add("SIGN_ID", GetType(String))

                dt.Columns.Add("THEORY_PHASE_FROM", GetType(String))
                dt.Columns.Add("THEORY_PHASE_TO", GetType(String))
                dt.Columns.Add("PRACTICE_PHASE_FROM", GetType(String))
                dt.Columns.Add("PRACTICE_PHASE_TO", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property
    ''' <summary>
    ''' Obj ContractDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Contract As ContractDTO
        Get
            Return ViewState(Me.ID & "_Contract")
        End Get
        Set(ByVal value As ContractDTO)
            ViewState(Me.ID & "_Contract") = value
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
    ''' <summary>
    ''' List obj Contracts
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Contracts As List(Of EmpBankDTO)
        Get
            Return ViewState(Me.ID & "_Contracts")
        End Get
        Set(ByVal value As List(Of EmpBankDTO))
            ViewState(Me.ID & "_Contracts") = value
        End Set
    End Property
    ''' <summary>
    ''' Insert ContractDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InsertContracts As ContractDTO
        Get
            Return ViewState(Me.ID & "_InsertContracts")
        End Get
        Set(ByVal value As ContractDTO)
            ViewState(Me.ID & "_InsertContracts") = value
        End Set
    End Property
    ''' <summary>
    ''' Delete ContractDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeleteContract As ContractDTO
        Get
            Return ViewState(Me.ID & "_DeleteContract")
        End Get
        Set(ByVal value As ContractDTO)
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

            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Export)


            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_CHECK,
                                                                  ToolbarIcons.Export,
                                                                  ToolbarAuthorize.Special1,
                                                                  "Xuất file mẫu"))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_IMPORT,
                                                                  ToolbarIcons.Import,
                                                                  ToolbarAuthorize.Special1,
                                                                  "Nhập file mẫu"))
            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_CREATE_BATCH,
            '                                                      ToolbarIcons.Add,
            '                                                      ToolbarAuthorize.Special1,
            '                                                      "Phê duyệt hàng loạt"))
            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_UNLOCK,
            '                                                      ToolbarIcons.Unlock,
            '                                                      ToolbarAuthorize.Special1,
            '                                                      "Mở phê duyệt"))
            'CType(MainToolBar.Items(4), RadToolBarButton).Text = "In hợp đồng"

            'CType(MainToolBar.Items(4), RadToolBarButton).Text = Translate("Thanh lý hợp đồng")
            'CType(MainToolBar.Items(4), RadToolBarButton).ImageUrl = CType(MainToolBar.Items(1), RadToolBarButton).ImageUrl
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
        Dim status As Integer
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_CHECK
                    Template_ImportHopdong()
                Case CommonMessage.TOOLBARITEM_PRINT
                    Dim dtData As DataTable
                    Dim dtDataCon As DataTable
                    Dim sourcePath = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/")
                    Dim folderName As String = "ContractSupport"
                    Dim filePath As String = ""
                    Dim extension As String = ""
                    Dim iError As Integer = 0
                    Dim path As String = ""
                    Dim lstID As String = ""
                    Dim limitRecord As Decimal = 0
                    Dim items = rgContract.SelectedItems

                    limitRecord = 100
                    If items.Count >= limitRecord Then
                        ShowMessage("Chỉ được In " + limitRecord.ToString + " dòng", NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstType As New List(Of String)
                    For Each lst In items
                        Dim lstIDs As String = ""
                        lstIDs = lstIDs & Decimal.Parse(lst("ID").Text).ToString
                        lstType.Add(CType(lst, GridDataItem).GetDataKeyValue("CONTRACTTYPE_CODE").ToString)
                        'check loại hợp đồng đã gán biểu mẫu in
                        Using rep As New ProfileBusinessRepository
                            Dim temp As New ContractDTO
                            Dim id = CDec(Val(lstIDs))
                            'temp = rep.GetEmpBankByID(New ContractDTO With {.ID = id})
                            If IsNumeric(temp.CONTRACTTYPE_ID) Then
                                lstID &= IIf(lstID = vbNullString, Decimal.Parse(lst("ID").Text).ToString, "," & Decimal.Parse(lst("ID").Text).ToString)
                            Else
                                ShowMessage("Vui lòng chọn biểu mẫu trước khi in !", NotifyType.Warning)
                                Exit Sub
                            End If
                        End Using
                    Next

                    'Using rep As New ProfileRepository
                    '    dtDataCon = rep.GetCheckContractTypeID(lstID)
                    '    If dtDataCon.Rows(0)(0) = 2 Then
                    '        ShowMessage("Các bản ghi không cùng loại hợp đồng !", NotifyType.Warning)
                    '        Exit Sub
                    '    End If
                    'End Using

                    lstType = lstType.Distinct.ToList
                    If lstType.Count > 1 Then
                        ShowMessage("Các bản ghi không cùng loại hợp đồng !", NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim icheck As GridDataItem = items.Item(0)



                    ' Kiểm tra file theo thông tin trong database
                    If Not String.IsNullOrEmpty(icheck.GetDataKeyValue("CONTRACTTYPE_CODE")) Then
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
                    Else
                        ShowMessage("Biểu mẫu không tồn tại", NotifyType.Warning)
                        Exit Sub
                    End If


                    Using rep As New ProfileRepository
                        dtData = rep.GetHU_DataDynamicContract(lstID, ProfileCommon.HU_TEMPLATE_TYPE.CONTRACT_ID, folderName)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                        If folderName = "" Then
                            ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                        'If dtData.Rows(0)("ORG_CODE2") = "TNE&C SG" Then
                        '    ShowMessage("Không thể in hợp đồng có đơn vị TNE&C SG", NotifyType.Warning)
                        '    Exit Sub
                        'End If
                    End Using

                    Using word As New WordCommon
                        word.ExportMailMerge(filePath,
                                             icheck.GetDataKeyValue("CONTRACTTYPE_CODE") & _
                                             Format(Date.Now, "yyyyMMddHHmmss") & extension,
                                             dtData,
                                             sourcePath,
                                             Response)
                    End Using

                Case CommonMessage.TOOLBARITEM_NEXT
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
                    If Not Utilities.GetTemplateLinkFile("TMF_Offer_Letter_Revised",
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
                        'ElseIf rgContract.SelectedItems.Count > 1 Then
                        '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        '    Exit Sub
                    End If
                    'Dim item As GridDataItem = rgContract.SelectedItems(0)
                    'If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                    '    ShowMessage(Translate("Bản ghi đã phê duyệt. Thao tác thực hiện không thành công"), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    'If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.NOT_APPROVE_ID Then
                    '    ShowMessage(Translate("Bản ghi không phê duyệt. Thao tác thực hiện không thành công"), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    'DeleteContract = New ContractDTO With {.ID = Decimal.Parse(item("ID").Text),
                    '                                       .EMPLOYEE_ID = Decimal.Parse(item("EMPLOYEE_ID").Text)}
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
                Case CommonMessage.TOOLBARITEM_UNLOCK
                    BatchApproveContract("M")
                Case CommonMessage.TOOLBARITEM_CREATE_BATCH
                    BatchApproveContract("P")
                Case CommonMessage.TOOLBARITEM_REFRESH
                    If rgContract.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgContract.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim item As GridDataItem = rgContract.SelectedItems(0)
                    status = If(item.GetDataKeyValue("STATUS_ID").ToString() = "", 0, Decimal.Parse(item.GetDataKeyValue("STATUS_ID").ToString()))
                    If status <> 447 Then
                        ShowMessage("Hợp đồng chưa phê duyệt, vui lòng kiểm tra lại", NotifyType.Warning)
                        Exit Sub
                    End If
                    'ctrlMessageBox.MessageText = "Bạn thực sự muốn thanh lý hợp đồng này ?"
                    'ctrlMessageBox.MessageTitle = item.GetDataKeyValue("EMPLOYEE_CODE").ToString
                    'ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_REFRESH
                    'ctrlMessageBox.DataBind()
                    'ctrlMessageBox.Show()
            End Select

            'UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub Template_ImportHopdong()
        Dim rep As New Profile.ProfileBusinessRepository
        Dim rep1 As New ProfileRepository
        Try

            Dim _filter As New EmpBankDTO
            Dim startTime As DateTime = DateTime.UtcNow
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

            If ctrlOrg.CurrentValue Is Nothing Then
                rgContract.DataSource = New List(Of EmpBankDTO)
                Exit Sub
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
            Dim dt As New DataTable
            If Sorts IsNot Nothing Then
                dt = rep.GetEmpBank(_filter, rgContract.CurrentPageIndex, 3000, MaximumRows, _param, Sorts).ToTable()
            Else
                dt = rep.GetEmpBank(_filter, rgContract.CurrentPageIndex, 3000, MaximumRows, _param).ToTable()
            End If

            Dim lst_emp() = New String(rgContract.SelectedItems.Count) {}
            Dim i = 0
            For Each item As GridDataItem In rgContract.SelectedItems
                lst_emp(i) = item.GetDataKeyValue("EMPLOYEE_ID").ToString
                i += 1
            Next

            Dim dt_new = dt.Clone

            For Each rows As DataRow In dt.Rows
                For Each item In lst_emp
                    If item = rows("EMPLOYEE_ID").ToString Then
                        Dim newRow As DataRow = dt_new.NewRow
                        newRow("EMPLOYEE_ID") = rows("EMPLOYEE_ID")
                        newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
                        newRow("EMPLOYEE_NAME") = rows("EMPLOYEE_NAME")
                        newRow("ORG_NAME") = rows("ORG_NAME")
                        newRow("TITLE_NAME") = rows("TITLE_NAME")
                        newRow("STK") = rows("STK")
                        newRow("PERSON_INHERITANCE") = rows("PERSON_INHERITANCE")
                        newRow("BANK_NAME") = rows("BANK_NAME")
                        dt_new.Rows.Add(newRow)
                    End If
                Next

            Next

            ListComboData = New ComboBoxDataDTO
            ListComboData.GET_BANK = True
            rep1.GetComboList(ListComboData)

            Dim bank = ListComboData.LIST_BANK.ToTable()
            Dim configPath As String = ConfigurationManager.AppSettings("PathImportFolder")
            Dim dsData As New DataSet

            dsData.Tables.Add(dt_new)
            dsData.Tables.Add(bank)

            dsData.Tables(0).TableName = "Table"
            dsData.Tables(1).TableName = "Table1"
            rep.Dispose()
            rep1.Dispose()


            ExportTemplate("Profile\Import\Import_Emp_Bank.xls",
                                  dsData, Nothing, "Import_Emp_Bank" & Format(Date.Now, "yyyymmdd"))


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
                'Dim item As GridDataItem = rgContract.SelectedItems(0)
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
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            tbarContracts.Enabled = True
            rgContract.Enabled = True
            ctrlOrg.Enabled = True
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    'If rep.DeleteContract(DeleteContract) Then
                    '    DeleteContract = Nothing
                    '    IDSelect = Nothing
                    '    Refresh("UpdateView")
                    'Else
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    'End If
                    Dim lstId As New List(Of Decimal)
                    For Each _item As GridDataItem In rgContract.SelectedItems
                        If _item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("đã được phê duyệt, không thể xóa. vui lòng kiểm tra lại !"), NotifyType.Warning)
                            Exit Sub
                        End If
                        lstId.Add(_item.GetDataKeyValue("ID"))
                    Next
                    If rep.Delete_List_Contract(lstId) Then
                        Contract = Nothing
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgContract.Rebind()
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
        Dim rep As New ProfileRepository
        Try
            'Using rep As New ProfileRepository
            '    dtData = rep.GetOtherList("CONTRACT_SUPPORT")
            '    FillRadCombobox(cboPrintSupport, dtData, "NAME", "ID")
            'End Using
            ListComboData = New ComboBoxDataDTO
            ListComboData.GET_CONTRACTTYPE = True
            rep.GetComboList(ListComboData)
            FillDropDownList(cboContractType, ListComboData.LIST_CONTRACTTYPE, "NAME", "ID", Common.Common.SystemLanguage, False)
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
        Dim _filter As New EmpBankDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If ctrlOrg.CurrentValue Is Nothing Then
                rgContract.DataSource = New List(Of EmpBankDTO)
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
                    Return rep.GetEmpBank(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetEmpBank(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.Contracts = rep.GetEmpBank(_filter, rgContract.CurrentPageIndex, rgContract.PageSize, MaximumRows, _param, Sorts)
                Else
                    Me.Contracts = rep.GetEmpBank(_filter, rgContract.CurrentPageIndex, rgContract.PageSize, MaximumRows, _param)
                End If

                rgContract.VirtualItemCount = MaximumRows
                rgContract.DataSource = Me.Contracts

                Return Me.Contracts.ToTable()
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý phê duyệt hop dong</summary>
    ''' <remarks></remarks>
    Private Sub BatchApproveContract(ByVal acti As String)
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lstID As New List(Of Decimal)

        Try
            '1. Check có rows nào được select hay không
            If rgContract Is Nothing OrElse rgContract.SelectedItems.Count <= 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If

            Dim listCon As New List(Of ContractDTO)

            For Each dr As Telerik.Web.UI.GridDataItem In rgContract.SelectedItems
                Dim ID As New Decimal
                If Not dr.GetDataKeyValue("STATUS_ID").Equals("447") And acti = "P" Then
                    ID = dr.GetDataKeyValue("ID")
                    lstID.Add(ID)
                End If
                If Not dr.GetDataKeyValue("STATUS_ID").Equals("446") And acti = "M" Then
                    ID = dr.GetDataKeyValue("ID")
                    lstID.Add(ID)
                End If
            Next
            If lstID.Count > 0 Then
                'Dim bCheckHasfile = rep.CheckHasFileContract(lstID)
                For Each item As GridDataItem In rgContract.SelectedItems
                    If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.APPROVE_ID And acti = "P" Then
                        ShowMessage(Translate("Bản ghi đã phê duyệt."), NotifyType.Warning)
                        Exit Sub
                    End If
                    If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID And acti = "M" Then
                        ShowMessage(Translate("Bản ghi đang ở trạng thái chờ phê duyệt."), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                'If bCheckHasfile = 1 Then
                '    ShowMessage(Translate("Duyệt khi tất cả các record đã có tập tin đính kèm,bạn kiểm tra lại"), NotifyType.Warning)
                '    Exit Sub
                'End If
                If rep.ApproveListContract(lstID, acti) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgContract.Rebind()
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

    Private Sub rgContract_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgContract.ItemDataBound
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

#End Region

    Function loadToGrid() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtError As New DataTable("ERROR")
        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            dtDataImportContract = dtData.Clone
            dtError = dtData.Clone
            Dim iRow = 1
            Dim _filter As New WorkingDTO
            Dim rep As New ProfileBusinessRepository
            Dim sp As New ProfileStoreProcedure()
            Dim IBusiness As IProfileBusiness = New ProfileBusinessClient()
            Dim dsData As DataSet = rep.GetHopdongImport()
            'Dim dt_work As New DataTable
            'dt_work = dsData.Tables(4)
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)
                If row("START_DATE") Is DBNull.Value OrElse row("START_DATE") = "" Then
                    sError = "Chưa nhập ngày bắt đầu"
                    ImportValidate.IsValidTime("START_DATE", row, rowError, isError, sError)
                Else
                    If IsDate(row("START_DATE")) = False Then
                        sError = "Ngày bắt đầu không đúng định dạng"
                        ImportValidate.IsValidTime("START_DATE", row, rowError, isError, sError)
                    End If
                    'Try
                    '    If IBusiness.ValEffectdateByEmpCode(row("EMPLOYEE_CODE"), ToDate(row("EFFECT_DATE"))) = False Then
                    '        sError = "Tồn tại hồ sơ lương trùng ngày hiệu lực"
                    '        ImportValidate.IsValidTime("EFFECT_DATE", row, rowError, isError, sError)
                    '    End If
                    'Catch ex As Exception
                    '    GoTo VALIDATE
                    'End Try
                End If
VALIDATE:
                Dim empId = rep.CheckEmployee_Exits(row("EMPLOYEE_CODE"))
                If IsDate(row("SIGN_DATE")) = False Then
                    sError = "Ngày bắt đầu không đúng định dạng"
                    ImportValidate.IsValidTime("SIGN_DATE", row, rowError, isError, sError)
                End If
                If empId = 0 Then
                    sError = "Mã nhân viên không tồn tại"
                    ImportValidate.IsValidTime("FULLNAME_VN", row, rowError, isError, sError)
                End If

                If row("STATUS_ID") Is DBNull.Value OrElse row("STATUS_ID") = "0" Then
                    sError = "Chưa chọn trạng thái"
                    ImportValidate.IsValidTime("STATUS_NAME", row, rowError, isError, sError)
                End If
                If row("CONTRACT_TYPE_ID") Is DBNull.Value OrElse row("CONTRACT_TYPE_ID") = "0" Then
                    sError = "Chưa chọn loại hợp đồng"
                    ImportValidate.IsValidTime("CONTRACT_TYPE_NAME", row, rowError, isError, sError)
                End If
                If row("WORK_PLACE_ID") Is DBNull.Value OrElse row("WORK_PLACE_ID") = "0" Then
                    sError = "Chưa chọn nơi làm việc"
                    ImportValidate.IsValidTime("WORK_PLACE_NAME", row, rowError, isError, sError)
                End If
                If row("WORKING_ID") Is DBNull.Value OrElse row("WORKING_ID") = "0" OrElse row("WORKING_ID") = "-1" Then
                    sError = "Nhân viên chưa có hồ sơ lương"
                    ImportValidate.IsValidTime("WORKING_ID", row, rowError, isError, sError)
                End If

                If row("END_DATE").ToString <> "" Then
                    If IsDate(row("END_DATE")) = False Then
                        sError = "Ngày kết thúc không đúng định dạng"
                        ImportValidate.IsValidTime("END_DATE", row, rowError, isError, sError)
                    End If
                    If Convert.ToDateTime(row("START_DATE")) > Convert.ToDateTime(row("END_DATE")) Then
                        sError = "Ngày bắt đầu lớn hơn ngày kết thúc"
                        ImportValidate.IsValidTime("END_DATE", row, rowError, isError, sError)
                    End If
                Else
                    If IsDate(row("START_DATE")) = True And IsNumeric(row("CONTRACT_TYPE_ID")) Then
                        If IsDate(SetEndEffectDate(row("START_DATE"), CInt(row("CONTRACT_TYPE_ID")))) Then
                            row("END_DATE") = SetEndEffectDate(row("START_DATE"), CInt(row("CONTRACT_TYPE_ID"))).ToString("dd/MM/yyyy")
                        Else
                            row("END_DATE") = "NULL"
                        End If

                    End If
                End If
                If row("CONTRACT_TYPE_CODE") = "TNECSG_BDH_HD" And (row("END_DATE") Is DBNull.Value OrElse row("END_DATE") = "") Then
                    sError = "Chưa nhập ngày kết thúc"
                    ImportValidate.IsValidTime("END_DATE", row, rowError, isError, sError)
                End If


                ''NGUOI KY k có trong hệ thống
                If row("SIGN_ID").ToString <> "" Then
                    If IBusiness.CHECK_SIGN(row("SIGN_ID")) = 0 Then
                        sError = "Người ký - Không tồn tại"
                        ImportValidate.IsValidTime("SIGN_ID", row, rowError, isError, sError)
                    Else
                        Dim SIG_Id = rep.CheckEmployee_Exits(row("SIGN_ID"))
                        If SIG_Id <> 0 Then
                            row("SIGN_ID") = SIG_Id.ToString
                        Else
                            row("SIGN_ID") = 0
                        End If
                    End If
                Else
                    If IsDate(row("START_DATE")) Then
                        Dim item = sp.GET_ORG_BY_EMPCODE(row("EMPLOYEE_CODE").ToString, CDate(row("START_DATE")))
                        If item IsNot Nothing AndAlso item.Rows.Count > 0 Then
                            Dim signer = sp.GET_SIGNER_BY_FUNC("ctrlHU_EmpBankNewEdit", CDate(row("START_DATE")), If(IsNumeric(item.Rows(0)("ORG_ID")), CDec(item.Rows(0)("ORG_ID")), 0), If(IsNumeric(row("CONTRACT_TYPE_ID")), CDec(row("CONTRACT_TYPE_ID")), 0))
                            If signer.Rows.Count > 0 Then
                                row("SIGN_ID") = signer.Rows(0)("ID").ToString
                            Else
                                row("SIGN_ID") = 0
                            End If
                        Else
                            row("SIGN_ID") = 0
                        End If

                    Else
                        row("SIGN_ID") = 0
                    End If
                End If

                If row("THEORY_PHASE_FROM").ToString <> "" Then
                    If IsDate(row("THEORY_PHASE_FROM")) = False Then
                        sError = "Giai đoạn học lý thuyết từ không đúng định dạng"
                        ImportValidate.IsValidTime("THEORY_PHASE_FROM", row, rowError, isError, sError)
                    End If
                End If

                If row("THEORY_PHASE_TO").ToString <> "" Then
                    If IsDate(row("THEORY_PHASE_TO")) = False Then
                        sError = "Giai đoạn học lý thuyết đến không đúng định dạng"
                        ImportValidate.IsValidTime("THEORY_PHASE_TO", row, rowError, isError, sError)
                    End If
                End If

                If row("PRACTICE_PHASE_FROM").ToString <> "" Then
                    If IsDate(row("PRACTICE_PHASE_FROM")) = False Then
                        sError = "Giai đoạn thực tập từ không đúng định dạng"
                        ImportValidate.IsValidTime("PRACTICE_PHASE_FROM", row, rowError, isError, sError)
                    End If
                End If

                If row("PRACTICE_PHASE_TO").ToString <> "" Then
                    If IsDate(row("PRACTICE_PHASE_TO")) = False Then
                        sError = "Giai đoạn thực tập đến không đúng định dạng"
                        ImportValidate.IsValidTime("PRACTICE_PHASE_TO", row, rowError, isError, sError)
                    End If
                End If

                If isError Then
                    rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    'rowError("FULLNAME_VN") = row("FULLNAME_VN").ToString
                    'If rowError("FULLNAME_VN").ToString = "" Then
                    '    rowError("FULLNAME_VN") = row("FULLNAME_VN").ToString
                    'End If
                    dtError.Rows.Add(rowError)
                Else
                    dtDataImportContract.ImportRow(row)
                End If
                iRow = iRow + 1
            Next
            If dtError.Rows.Count > 0 Then
                Session("EXPORTREPORT") = dtError
                rep.Dispose()
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('TEMP_IMPORT_CONTRACT');", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            If isError OrElse (dtError IsNot Nothing AndAlso dtError.Rows.Count > 0) Then
                Return False
            Else
                Return True
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    Private Sub ctrlUpload1_OkClicked(sender As Object, e As EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim rep As New Profile.ProfileBusinessRepository
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim countFile As Integer = ctrlUpload1.UploadedFiles.Count
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
            If rep.saveEmpBank(ds.Tables(0)) = 1 Then
                ShowMessage(Translate("Import thành công"), NotifyType.Success)
                rgContract.Rebind()
            Else
                ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                Exit Sub
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        Try
            ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
            dtTemp.Columns(0).ColumnName = "EMPLOYEE_CODE"
            dtTemp.Columns(1).ColumnName = "EMPLOYEE_NAME"
            dtTemp.Columns(2).ColumnName = "ORG_NAME"
            dtTemp.Columns(3).ColumnName = "TITLE_NAME"
            dtTemp.Columns(4).ColumnName = "STK"
            dtTemp.Columns(5).ColumnName = "PERSON_INHERITANCE"
            dtTemp.Columns(6).ColumnName = "BANK_NAME"
            dtTemp.Columns(7).ColumnName = "BANK_NAME_NEW"
            dtTemp.Columns(8).ColumnName = "BANK_ID"
            'XOA DONG TIEU DE VA HEADER
            dtTemp.Rows(0).Delete()
            'dtTemp.Rows(1).Delete()

            ' add Log
            Dim _error As Boolean = True
            Dim count As Integer
            Dim newRow As DataRow
            Dim dsEMP As DataTable

            'XOA NHUNG DONG DU LIEU NULL STT
            Dim rowDel As DataRow
            For i As Integer = 0 To dtTemp.Rows.Count - 1
                If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
                rowDel = dtTemp.Rows(i)
                If rowDel("EMPLOYEE_CODE").ToString.Trim = "" Then
                    dtTemp.Rows(i).Delete()
                End If
            Next

            dtTemp.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Function SetEndEffectDate(ByVal dtdate As Date, ByVal type As Integer) As Date
        'trường”theo tháng/ngày” 
        Dim code As String = ""
        Dim date_end As Date = Nothing
        'Quy tắc lấy ngày hết hiệu lực
        Dim code_enddate As String = ""
        Dim obj As New ContractTypeDTO
        Try
            Using rep As New ProfileRepository
                'obj = rep.GetEmpBankTypeID(Decimal.Parse(type))
                If obj IsNot Nothing Then
                    code = obj.FLOWING_MD
                    code_enddate = obj.CODE_GET_ENDDATE
                Else
                    Return Nothing
                End If
            End Using

            Select Case code
                Case "KXD"
                    date_end = Nothing
                Case "NGAY"
                    date_end = dtdate.AddDays(obj.PERIOD)
                Case "THANG"
                    If code_enddate = "THANG" Then
                        date_end = dtdate.AddMonths(obj.PERIOD).LastDateOfMonth()
                    End If

                    If code_enddate = "QUY" Then
                        Dim month = dtdate.AddMonths(obj.PERIOD).Month
                        Dim year = dtdate.AddMonths(obj.PERIOD).Year

                        'Quý 1
                        If month >= 1 And month <= 3 Then
                            Dim d As New Date(year, 3, 1)
                            date_end = d.LastDateOfMonth()
                        End If
                        'Quý 2
                        If month >= 4 And month <= 6 Then
                            Dim d As New Date(year, 6, 1)
                            date_end = d.LastDateOfMonth()
                        End If
                        'Quý 3
                        If month >= 7 And month <= 9 Then
                            Dim d As New Date(year, 9, 1)
                            date_end = d.LastDateOfMonth()
                        End If
                        'Quý 4
                        If month >= 10 And month <= 12 Then
                            Dim d As New Date(year, 12, 1)
                            date_end = d.LastDateOfMonth()
                        End If
                    End If

            End Select
            Return date_end.Date
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class