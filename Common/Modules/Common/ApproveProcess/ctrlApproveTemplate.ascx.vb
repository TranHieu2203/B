Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlApproveTemplate
    Inherits CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/ApproveProcess/" + Me.GetType().Name.ToString()
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Private rep As New CommonProcedureNew
    Dim user As UserLog

#Region "Propety"
    ''' <lastupdate>
    ''' 24/07/2017 11:27
    ''' </lastupdate>
    ''' <summary>
    ''' ApproveTemplates
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ApproveTemplates As List(Of ApproveTemplateDTO)
        Get
            Return ViewState(Me.ID & "_ApproveTemplates")
        End Get
        Set(ByVal value As List(Of ApproveTemplateDTO))
            ViewState(Me.ID & "_ApproveTemplates") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 24/07/2017 11:27
    ''' </lastupdate>
    ''' <summary>
    ''' Chi tiết phê duyệt template
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ApproveTemplateDetails As List(Of ApproveTemplateDetailDTO)
        Get
            Return ViewState(Me.ID & "_ApproveTemplateDetails")
        End Get
        Set(ByVal value As List(Of ApproveTemplateDetailDTO))
            ViewState(Me.ID & "_ApproveTemplateDetails") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 24/07/2017 11:27
    ''' </lastupdate>
    ''' <summary>
    ''' Trạng thái load popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 24/07/2017 11:27
    ''' </lastupdate>
    ''' <summary>
    ''' ID chi tiết của template được chọn
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IDDetailSelecting As Decimal?
        Get
            Return ViewState(Me.ID & "_ID_Detail_Selecting")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_ID_Detail_Selecting") = value
        End Set
    End Property
    ''' <summary>
    ''' ID Template được chọn
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IDTemplateSelecting As Decimal?
        Get
            Return ViewState(Me.ID & "_IDTemplateSelecting")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_IDTemplateSelecting") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 24/07/2017 11:27
    ''' </lastupdate>
    ''' <summary>
    ''' Danh sách các thiết lập template cần xóa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListToDelete As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_Delete_List")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_Delete_List") = value
        End Set
    End Property
    ''' <summary>
    ''' DeleteWhat
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeleteWhat As ItemToDelete
        Get
            Return ViewState(Me.ID & "_DeleteWhat")
        End Get
        Set(ByVal value As ItemToDelete)
            ViewState(Me.ID & "_DeleteWhat") = value
        End Set
    End Property
    ''' <summary>
    ''' Item to delete
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ItemToDelete
        Template
        TemplateDetail
    End Enum
#End Region

#Region "Event"
#Region "-- View"
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức load trang( viewload)
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                RefreshTemplate()
            End If

            ShowPopupEmployee()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo ViewInit
    ''' Khởi tạo thiết lập cho rad grid rgTemplate, rgDetail
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'rgApproveProcess.AllowCustomPaging = True
            'rgApproveProcess.PageSize = Common.Common.DefaultPageSize

            rgTemplate.ClientSettings.EnablePostBackOnRowClick = True
            rgDetail.ClientSettings.EnablePostBackOnRowClick = True

            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

#End Region
#Region "-- Grid Template"
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource cho rgTemplate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgTemplate_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgTemplate.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(sender, RadGrid).DataSource = ApproveTemplates

            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedIndexChanged cho control rgTemplate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgTemplate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgTemplate.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            RefreshTemplateDetail()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region
#Region "-- Grid Detail"
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSourced control rgDetail
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgDetail_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgDetail.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(sender, RadGrid).DataSource = ApproveTemplateDetails
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Click khi click btnReloadGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnReloadGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReloadGrid.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            RefreshTemplate()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedIndexChanged của control rgDetail
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgDetail_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgDetail.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ShowTemplateDetail()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region
#Region "FindEmployeeButton"
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện cancel click khi click hủy trên ctrlFindEmployeePopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Try
            isLoadPopup = 0
        Catch ex As Exception

        End Try

    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện EmployeeSelected trên user control ctrlFindEmployeePopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New CommonRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidEmployeeID.Value = item.ID.ToString
                txtEmplloyeeCode.Text = item.EMPLOYEE_CODE
                txtEmployeeName.Text = item.FULLNAME_VN
            End If
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện click khi click button btnSearchEmp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearchEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchEmp.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 1

            ShowPopupEmployee()
            ctrlFindEmployeePopup.Show()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region
#Region "-- Toolbar"
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Click khi click button trên tbarTemplate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbarTemplate_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles tbarTemplate.ButtonClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_DELETE

                    Dim idDelete = CType(rgTemplate.SelectedItems(0), GridDataItem).GetDataKeyValue("ID")

                    Dim listDelete As New List(Of Decimal)
                    listDelete.Add(idDelete)

                    ListToDelete = listDelete
                    DeleteWhat = ItemToDelete.Template

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện click khi click button trênd tbarTemplateDetail
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbarTemplateDetail_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                'thêm mới
                Case CommonMessage.TOOLBARITEM_CREATE

                    Me.CurrentState = CommonMessage.STATE_NEW
                    IDDetailSelecting = Nothing
                    UpdateControlState()
                    ClearControlValue(txtEmplloyeeCode, txtEmployeeName, txtInformEmail, cboAppType, rntxtInformDate, nntxtAppLevel, cboCapChucDanhPheDuyet)
                    'Sửa thông tin
                Case CommonMessage.TOOLBARITEM_EDIT

                    Me.CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                    'Lưu thông tin
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dt As DataTable = rep.GET_ALL_APPROVE()
                        If dt.Rows.Count > 0 Then
                            xls.ExportExcelTemplate(Server.MapPath("~/ReportTemplates/Common/Approve.xls"), "DanhSachPheDuyet", dt, Response)
                        Else
                            ShowMessage(Translate("Không có dữ liệu để xuất danh sách"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE

                    If Not Page.IsValid Then
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDetail')")
                        Exit Sub
                    End If

                    If cboAppType.SelectedIndex = 6 Then
                        If String.IsNullOrEmpty(cboCapChucDanhPheDuyet.SelectedValue) Or String.IsNullOrEmpty(cboCapChucDanhPheDuyet.Text) Then
                            ShowMessage(Translate("Chưa chọn vị trí công việc phê duyệt"), NotifyType.Error)
                            Exit Sub
                        End If
                    End If

                    Dim db As New CommonRepository
                    If Not IDDetailSelecting.HasValue Then
                        'Thêm mới
                        Dim itemAdd As New ApproveTemplateDetailDTO
                        With itemAdd
                            .APP_TYPE = cboAppType.SelectedIndex
                            .APP_LEVEL = nntxtAppLevel.Value.ToString.Trim
                            .TEMPLATE_ID = Decimal.Parse(CType(rgTemplate.SelectedItems(0), GridDataItem).GetDataKeyValue("ID").ToString)
                            .INFORM_DATE = If(rntxtInformDate.Value Is Nothing, 0, rntxtInformDate.Value)
                            .INFORM_EMAIL = txtInformEmail.Text.ToString.Trim

                            If .APP_TYPE = 1 Then
                                .APP_ID = Decimal.Parse(hidEmployeeID.Value)
                            ElseIf .APP_TYPE = 6 Then
                                .APP_ID = Decimal.Parse(cboCapChucDanhPheDuyet.SelectedValue)
                            Else
                                .APP_ID = Nothing
                            End If
                        End With

                        If db.InsertApproveTemplateDetail(itemAdd) Then
                            hidEmployeeID.Value = Nothing
                            txtEmplloyeeCode.Text = Nothing
                            txtEmployeeName.Text = Nothing
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            Exit Sub
                        End If
                    Else
                        ' Sửa đổi

                        Dim idEdit As Decimal = IDDetailSelecting.Value

                        Dim itemEdit As ApproveTemplateDetailDTO = db.GetApproveTemplateDetail(idEdit)
                        If (itemEdit IsNot Nothing) Then
                            With itemEdit
                                .APP_TYPE = cboAppType.SelectedIndex
                                .APP_LEVEL = nntxtAppLevel.Value.ToString.Trim

                                .INFORM_DATE = If(rntxtInformDate.Value Is Nothing, 0, rntxtInformDate.Value)
                                .INFORM_EMAIL = txtInformEmail.Text.ToString.Trim
                                If .APP_TYPE = 1 Then
                                    .APP_ID = Decimal.Parse(hidEmployeeID.Value)
                                ElseIf .APP_TYPE = 6 Then
                                    .APP_ID = Decimal.Parse(cboCapChucDanhPheDuyet.SelectedValue)
                                Else
                                    .APP_ID = Nothing
                                End If
                            End With

                            If db.UpdateApproveTemplateDetail(itemEdit) Then
                                hidEmployeeID.Value = Nothing
                                txtEmplloyeeCode.Text = Nothing
                                txtEmployeeName.Text = Nothing
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                Exit Sub
                            End If
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                        End If

                    End If
                    ClearControlValue(txtEmplloyeeCode, txtEmployeeName, txtInformEmail, cboAppType, rntxtInformDate, nntxtAppLevel, cboCapChucDanhPheDuyet)
                    RefreshTemplateDetail()
                    Me.CurrentState = CommonMessage.STATE_NORMAL

                    isLoadPopup = 0
                    UpdateControlState()
                    'Hủy
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Me.CurrentState = CommonMessage.STATE_NORMAL
                    isLoadPopup = 0
                    UpdateControlState()
                    ShowTemplateDetail()
                    'Xóa
                Case CommonMessage.TOOLBARITEM_NEXT
                    Using xls As New ExcelCommon
                        Dim dt As DataTable = New DataTable
                        Dim ds As DataSet = New DataSet
                        xls.ExportExcelTemplate(Server.MapPath("~/ReportTemplates/Common/Approve/Import_Template_Phe_Duyet.xls"), "Template phê duyệt", ds, dt, Response)
                    End Using
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.isMultiple = False
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_DELETE
                    Dim idDelete = CType(rgDetail.SelectedItems(0), GridDataItem).GetDataKeyValue("ID")

                    Dim db As New CommonRepository

                    Dim listDelete As New List(Of Decimal)
                    listDelete.Add(idDelete)

                    ListToDelete = listDelete

                    DeleteWhat = ItemToDelete.TemplateDetail

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện command Yes/No trên ctrlMessageBox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim db As New CommonRepository

                If DeleteWhat = ItemToDelete.Template Then

                    If Not db.IsApproveTemplateUsed(ListToDelete(0)) Then

                        If db.DeleteApproveTemplate(ListToDelete) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            RefreshTemplate()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                    End If
                ElseIf DeleteWhat = ItemToDelete.TemplateDetail Then
                    If db.DeleteApproveTemplateDetail(ListToDelete) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        RefreshTemplateDetail()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                End If
                ClearControlValue(txtEmplloyeeCode, txtEmployeeName, txtInformEmail, cboAppType, rntxtInformDate, nntxtAppLevel, cboCapChucDanhPheDuyet)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedIndexChanged của control cboAppType
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboAppType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboAppType.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            pnlSelectEmp.Enabled = (cboAppType.SelectedIndex = 1)

            If cboAppType.SelectedIndex = 1 Then
                reqEmployee.ControlToValidate = "txtEmplloyeeCode"
            Else
                reqEmployee.ControlToValidate = "cboAppType"
                hidEmployeeID.Value = Nothing
                txtEmplloyeeCode.Text = Nothing
                txtEmployeeName.Text = Nothing

                If cboAppType.SelectedIndex = 6 Then
                    Dim ctx As New CommonRepository
                    Dim dbTitle = ctx.GetTitleList()

                    cboCapChucDanhPheDuyet.Enabled = True
                    cboCapChucDanhPheDuyet.DataSource = dbTitle
                    cboCapChucDanhPheDuyet.DataTextField = "NAME_VN"
                    cboCapChucDanhPheDuyet.DataValueField = "ID"
                    cboCapChucDanhPheDuyet.DataBind()
                End If
            End If

            If cboAppType.SelectedIndex <> 6 Then
                cboCapChucDanhPheDuyet.Enabled = False
                cboCapChucDanhPheDuyet.DataSource = Nothing
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    'Private Sub valCustomInformDate_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles valCustomInformDate.ServerValidate
    '    Try
    '        args.IsValid = True
    '        If Not rntxtInformDate.Value.HasValue Then
    '            args.IsValid = False
    '        End If
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện server validate cho control cvallevel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalLevel_ServerValidate(ByVal sender As Object, ByVal args As ServerValidateEventArgs) Handles cvalLevel.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New CommonRepository

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            args.IsValid = True

            'If Not nntxtAppLevel.Value.HasValue And nntxtAppLevel.Value < 1 Then
            '    args.IsValid = False
            '    Exit Sub
            'End If

            If Not nntxtAppLevel.Value.HasValue Then
                args.IsValid = False
                Exit Sub
            End If

            If IDDetailSelecting.HasValue Then
                args.IsValid = rep.CheckLevelInsert(nntxtAppLevel.Value.Value, IDDetailSelecting.Value, IDTemplateSelecting)
            Else
                args.IsValid = rep.CheckLevelInsert(nntxtAppLevel.Value.Value, 0, IDTemplateSelecting)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Other"
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức hiển thị chi tiết template
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowTemplateDetail()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rgDetail.SelectedItems.Count = 0 Then
                ClearControlValue(txtEmplloyeeCode, txtEmployeeName, txtInformEmail, cboAppType, rntxtInformDate, nntxtAppLevel, cboCapChucDanhPheDuyet)
                Exit Sub
            End If

            Dim db As New CommonRepository
            Dim idSelected As Decimal = Decimal.Parse(CType(rgDetail.SelectedItems(0), GridDataItem).GetDataKeyValue("ID").ToString)
            Dim AppType As Decimal = Decimal.Parse(CType(rgDetail.SelectedItems(0), GridDataItem).GetDataKeyValue("APP_TYPE").ToString)
            IDDetailSelecting = idSelected

            Dim itemSelected = db.GetApproveTemplateDetail(idSelected)

            'If itemSelected IsNot Nothing Then
            '    txtEmplloyeeCode.Text = itemSelected.EMPLOYEE_CODE
            '    txtEmployeeName.Text = itemSelected.EMPLOYEE_NAME
            '    hidEmployeeID.Value = If(itemSelected.APP_ID.HasValue, itemSelected.APP_ID.Value.ToString, "")
            '    nntxtAppLevel.Value = itemSelected.APP_LEVEL
            '    cboAppType.SelectedValue = itemSelected.APP_TYPE
            '    If cboAppType.SelectedIndex = 0 Then
            '        reqEmployee.ControlToValidate = "cboAppType"
            '    Else
            '        reqEmployee.ControlToValidate = "txtEmplloyeeCode"
            '    End If
            '    cboCapChucDanhPheDuyet.SelectedValue = itemSelected.APP_ID
            '    pnlSelectEmp.Enabled = (cboAppType.SelectedValue = "1")
            '    rntxtInformDate.Value = itemSelected.INFORM_DATE
            '    txtInformEmail.Text = itemSelected.INFORM_EMAIL
            'Else
            '    ShowMessage(Translate("Đã có lỗi khi truy xuất thông tin đang chọn."), NotifyType.Warning)
            'End If

            If itemSelected IsNot Nothing Then
                If AppType = 6 Then
                    txtEmplloyeeCode.Text = Nothing
                    txtEmployeeName.Text = Nothing
                    hidEmployeeID.Value = If(itemSelected.APP_ID.HasValue, itemSelected.APP_ID.Value.ToString, "")
                    nntxtAppLevel.Value = itemSelected.APP_LEVEL
                    cboAppType.SelectedValue = itemSelected.APP_TYPE
                    If cboAppType.SelectedIndex = 0 Then
                        reqEmployee.ControlToValidate = "cboAppType"
                    Else
                        reqEmployee.ControlToValidate = "txtEmplloyeeCode"
                    End If

                    Dim ctx As New CommonRepository
                    Dim dbTitle = ctx.GetTitleList()
                    cboCapChucDanhPheDuyet.Enabled = True
                    cboCapChucDanhPheDuyet.DataSource = dbTitle
                    cboCapChucDanhPheDuyet.DataTextField = "NAME_VN"
                    cboCapChucDanhPheDuyet.DataValueField = "ID"
                    cboCapChucDanhPheDuyet.DataBind()
                    cboCapChucDanhPheDuyet.SelectedValue = itemSelected.APP_ID
                    cboCapChucDanhPheDuyet.Text = itemSelected.TITLE_NAME

                    pnlSelectEmp.Enabled = (cboAppType.SelectedValue = "1")
                    rntxtInformDate.Value = itemSelected.INFORM_DATE
                    txtInformEmail.Text = itemSelected.INFORM_EMAIL
                Else
                    txtEmplloyeeCode.Text = itemSelected.EMPLOYEE_CODE
                    txtEmployeeName.Text = itemSelected.EMPLOYEE_NAME
                    hidEmployeeID.Value = If(itemSelected.APP_ID.HasValue, itemSelected.APP_ID.Value.ToString, "")
                    nntxtAppLevel.Value = itemSelected.APP_LEVEL
                    cboAppType.SelectedValue = itemSelected.APP_TYPE
                    If cboAppType.SelectedIndex = 0 Then
                        reqEmployee.ControlToValidate = "cboAppType"
                    Else
                        reqEmployee.ControlToValidate = "txtEmplloyeeCode"
                    End If
                    cboCapChucDanhPheDuyet.Text = Nothing
                    pnlSelectEmp.Enabled = (cboAppType.SelectedValue = "1")
                    rntxtInformDate.Value = itemSelected.INFORM_DATE
                    txtInformEmail.Text = itemSelected.INFORM_EMAIL
                End If
            Else
                ShowMessage(Translate("Đã có lỗi khi truy xuất thông tin đang chọn."), NotifyType.Warning)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức làm mới thông tin của template
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshTemplate()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'Me.CurrentState = CommonMessage.STATE_NORMAL
            Dim db As New CommonRepository

            ApproveTemplates = db.GetApproveTemplateList
            rgTemplate.Rebind()
            RefreshTemplateDetail()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Làm mới thông tin chi tiết template 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshTemplateDetail()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'CurrentState = CommonMessage.STATE_NORMAL
            Dim db As New CommonRepository

            If rgTemplate.SelectedItems.Count = 0 Then
                rgDetail.Enabled = False
                pnlDetail.Enabled = False
                SetStatusToolbar(tbarTemplateDetail, "", False)
                ApproveTemplateDetails = New List(Of ApproveTemplateDetailDTO)
                IDTemplateSelecting = Nothing
            Else
                rgDetail.Enabled = True
                pnlDetail.Enabled = False
                SetStatusToolbar(tbarTemplateDetail, CommonMessage.STATE_NORMAL)
                IDTemplateSelecting = Decimal.Parse(CType(rgTemplate.SelectedItems(0), GridDataItem).GetDataKeyValue("ID").ToString)
                ApproveTemplateDetails = db.GetApproveTemplateDetailList(IDTemplateSelecting)
            End If

            rgDetail.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo các thiết lập, giá trị ban đầu cho các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'Me.CurrentState = CommonMessage.STATE_NORMAL

            Common.BuildToolbar(tbarTemplate,
                                        ToolbarItem.Create,
                                        ToolbarItem.Edit, ToolbarItem.Seperator,
                                        ToolbarItem.Delete)

            Me.MainToolBar = tbarTemplateDetail
            Common.BuildToolbar(tbarTemplateDetail,
                                        ToolbarItem.Create,
                                        ToolbarItem.Edit, ToolbarItem.Seperator,
                                        ToolbarItem.Save,
                                        ToolbarItem.Cancel, ToolbarItem.Seperator,
                                        ToolbarItem.Export,
                                        ToolbarItem.Next,
                                        ToolbarItem.Import, ToolbarItem.Seperator,
                                        ToolbarItem.Delete)

            CType(tbarTemplateDetail.Items(3), RadToolBarButton).CausesValidation = True

            SetStatusToolbar(tbarTemplateDetail, CommonMessage.STATE_NORMAL)
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.MainToolBar.Items(7), RadToolBarButton).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(7), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(6), RadToolBarButton).ImageUrl
            CType(Me.MainToolBar.Items(8), RadToolBarButton).Text = Translate("Nhập file mẫu")
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái các control trên trang theo trạng thái hiện tại của trang là STATE_NORMAL, STATE_EDIT, STATE_NEW
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case Me.CurrentState
                Case CommonMessage.STATE_NORMAL
                    SetStatusToolbar(tbarTemplateDetail, Me.CurrentState)
                    SetStatusToolbar(tbarTemplate, "", True)
                    EnabledGrid(rgDetail, True)
                    EnabledGrid(rgTemplate, True)

                    pnlDetail.Enabled = False
                Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NEW
                    SetStatusToolbar(tbarTemplateDetail, Me.CurrentState)
                    SetStatusToolbar(tbarTemplate, "", False)
                    EnabledGrid(rgDetail, False)
                    EnabledGrid(rgTemplate, True)
                    If cboAppType.SelectedValue <> "6" Then
                        cboCapChucDanhPheDuyet.Enabled = False
                    Else
                        cboCapChucDanhPheDuyet.Enabled = True
                    End If
                    If cboAppType.SelectedIndex = 1 Then
                        reqEmployee.ControlToValidate = "txtEmplloyeeCode"
                    Else
                        reqEmployee.ControlToValidate = "cboAppType"
                    End If
                    pnlDetail.Enabled = True
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xét trạng thái cho toolbar
    ''' </summary>
    ''' <param name="_tbar"></param>
    ''' <param name="formState"></param>
    ''' <param name="stateToAll"></param>
    ''' <remarks></remarks>
    Private Sub SetStatusToolbar(ByVal _tbar As RadToolBar, ByVal formState As String, Optional ByVal stateToAll As Boolean? = Nothing)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If stateToAll.HasValue Then
                For Each item As RadToolBarItem In _tbar.Items
                    item.Enabled = stateToAll.Value
                    Select Case CType(item, RadToolBarButton).CommandName
                        Case CommonMessage.TOOLBARITEM_EXPORT, CommonMessage.TOOLBARITEM_NEXT, CommonMessage.TOOLBARITEM_IMPORT
                            item.Enabled = True
                    End Select
                Next
                Exit Sub
            End If

            Select Case formState
                Case CommonMessage.STATE_NORMAL
                    For Each item As RadToolBarItem In _tbar.Items
                        Select Case CType(item, RadToolBarButton).CommandName
                            Case CommonMessage.TOOLBARITEM_CANCEL, CommonMessage.TOOLBARITEM_SAVE
                                item.Enabled = False
                            Case Else
                                item.Enabled = True
                        End Select
                    Next
                Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NEW
                    For Each item As RadToolBarItem In _tbar.Items
                        Select Case CType(item, RadToolBarButton).CommandName
                            Case CommonMessage.TOOLBARITEM_CANCEL, CommonMessage.TOOLBARITEM_SAVE
                                item.Enabled = True
                            Case Else
                                item.Enabled = False
                        End Select
                    Next
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 11:55
    ''' </lastupdate>
    ''' <summary>
    ''' Hiển thị popup employee
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowPopupEmployee()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If

            If isLoadPopup = 1 Then
                ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                ctrlFindEmployeePopup.MultiSelect = False
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Try
            Import_Data()
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
        End Try
    End Sub

    Private Sub Import_Data()
        Try
            user = LogHelper.GetUserLog
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload1.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim ds As New DataSet
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload1.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                file.SaveAs(fileName, True)
                ds = ExcelPackage.ReadExcelToDataSet(fileName, False)
            End If
            If ds Is Nothing Then
                Exit Sub
            End If

            Dim message As String = TableMapping(ds.Tables(0))
            If message <> "" Then
                ShowMessage(Translate(message), NotifyType.Error)
                Return
            End If

            If ds.Tables(0).Rows.Count > 2 Then
                'Bo qa title, header va 2 dong example data
                For i As Integer = 2 To ds.Tables(0).Rows.Count - 1
                    Dim dr As DataRow = ds.Tables(0).Rows(i)
                    Dim template_type = dr(3).ToString().Trim()
                    If Not IsDBNull(dr(1)) AndAlso Not IsDBNull(dr(2)) AndAlso Not IsDBNull(template_type) Then
                        Dim temp_template_type As Integer = Integer.Parse(template_type)
                        If dr(4).ToString().Trim() <> "" Then
                            rep.IMPORT_APPROVE(dr(1), dr(2), temp_template_type, 1, dr(4), user)
                        End If
                        If dr(5).ToString().Trim() <> "" AndAlso dr(4).ToString.Trim() <> "" Then
                            rep.IMPORT_APPROVE(dr(1), dr(2), temp_template_type, 2, dr(5), user)
                        End If
                        If dr(6).ToString().Trim() <> "" AndAlso dr(5).ToString.Trim() <> "" Then
                            rep.IMPORT_APPROVE(dr(1), dr(2), temp_template_type, 3, dr(6), user)
                        End If
                        If dr(7).ToString().Trim() <> "" AndAlso dr(6).ToString.Trim() <> "" Then
                            rep.IMPORT_APPROVE(dr(1), dr(2), temp_template_type, 4, dr(7), user)
                        End If
                        If dr(8).ToString().Trim() <> "" AndAlso dr(7).ToString.Trim() <> "" Then
                            rep.IMPORT_APPROVE(dr(1), dr(2), temp_template_type, 5, dr(8), user)
                        End If
                        If dr(9).ToString().Trim() <> "" AndAlso dr(8).ToString.Trim() <> "" Then
                            rep.IMPORT_APPROVE(dr(1), dr(2), temp_template_type, 6, dr(9), user)
                        End If
                        If dr(10).ToString().Trim() <> "" AndAlso dr(9).ToString.Trim() <> "" Then
                            rep.IMPORT_APPROVE(dr(1), dr(2), temp_template_type, 7, dr(10), user)
                        End If
                        If dr(11).ToString().Trim() <> "" AndAlso dr(10).ToString.Trim() <> "" Then
                            rep.IMPORT_APPROVE(dr(1), dr(2), temp_template_type, 8, dr(11), user)
                        End If
                        If dr(12).ToString().Trim() <> "" AndAlso dr(11).ToString.Trim() <> "" Then
                            rep.IMPORT_APPROVE(dr(1), dr(2), temp_template_type, 9, dr(12), user)
                        End If
                        If dr(13).ToString().Trim() <> "" AndAlso dr(12).ToString.Trim() <> "" Then
                            rep.IMPORT_APPROVE(dr(1), dr(2), temp_template_type, 10, dr(13), user)
                        End If
                    End If

                Next
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                rgTemplate.Rebind()
                rgDetail.Rebind()
            End If
        Catch ex As Exception
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Function TableMapping(ByVal dtTemp As System.Data.DataTable) As String
        Dim message As String = ""
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "STT"
        dtTemp.Columns(1).ColumnName = "TEMPLATE_ID"
        dtTemp.Columns(2).ColumnName = "TEMPLATE_NAME"
        dtTemp.Columns(3).ColumnName = "TEMPLATE_TYPE"
        dtTemp.Columns(4).ColumnName = "APP_LEVEL_1"
        dtTemp.Columns(5).ColumnName = "APP_LEVEL_2"
        dtTemp.Columns(6).ColumnName = "APP_LEVEL_3"
        dtTemp.Columns(7).ColumnName = "APP_LEVEL_4"
        dtTemp.Columns(8).ColumnName = "APP_LEVEL_5"
        dtTemp.Columns(9).ColumnName = "APP_LEVEL_6"
        dtTemp.Columns(10).ColumnName = "APP_LEVEL_7"
        dtTemp.Columns(11).ColumnName = "APP_LEVEL_8"
        dtTemp.Columns(12).ColumnName = "APP_LEVEL_9"
        dtTemp.Columns(13).ColumnName = "APP_LEVEL_10"

        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()
        'Neu template empty thi xoa row do,
        For i As Integer = dtTemp.Rows.Count - 1 To 1 Step -1 'dtTemp.Rows.Count - 1 To 1 => vi da xoa rows(0) o tren
            If IsDBNull(dtTemp.Rows(i)(1)) AndAlso IsDBNull(dtTemp.Rows(i)(2)) AndAlso IsDBNull(dtTemp.Rows(i)(3)) AndAlso IsDBNull(dtTemp.Rows(i)(4)) Then
                dtTemp.Rows(i).Delete()
            Else
                If IsDBNull(dtTemp.Rows(i)(1)) OrElse IsDBNull(dtTemp.Rows(i)(2)) OrElse IsDBNull(dtTemp.Rows(i)(3)) OrElse IsDBNull(dtTemp.Rows(i)(4)) Then
                    message = "Vui lòng nhập đầy đủ các cột màu đỏ."
                    Exit For
                End If
            End If
        Next

        dtTemp.AcceptChanges()
        Return message
    End Function

#End Region

End Class