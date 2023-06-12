Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlApproveSetupExt
    Inherits CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/ApproveProcess/" + Me.GetType().Name.ToString()
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

#Region "Property"

    ''' <summary>
    ''' Obj ApproveSetupExts
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ApproveSetupExts() As List(Of ApproveSetupExtDTO)
        Get
            Return ViewState(Me.ID & "_ApproveSetups")
        End Get
        Set(ByVal value As List(Of ApproveSetupExtDTO))
            ViewState(Me.ID & "_ApproveSetups") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj Employees
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Employees() As List(Of EmployeeDTO)
        Get
            Return ViewState(Me.ID & "_Employees")
        End Get
        Set(ByVal value As List(Of EmployeeDTO))
            ViewState(Me.ID & "_Employees") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj ListToDelete
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
    ''' Obj OrgID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OrgID As Decimal?
        Get
            Return ViewState(Me.ID & "_OrgID")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_OrgID") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj EmployeeID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EmployeeID As Decimal?
        Get
            Return ViewState(Me.ID & "_EmployeeID")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_EmployeeID") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj IDDetailSelecting
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
    ''' Obj isLoadPopup
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
#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 25/07/2017 10:00
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
            If Not IsPostBack Then
                Refresh()
            End If
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgDetail và rgEmp
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarDetail
            Common.BuildToolbar(tbarDetail,
                                           ToolbarItem.Create,
                                           ToolbarItem.Edit, ToolbarItem.Seperator,
                                           ToolbarItem.Save,
                                           ToolbarItem.Cancel, ToolbarItem.Seperator,
                                           ToolbarItem.Delete)

            CType(tbarDetail.Items(3), RadToolBarButton).CausesValidation = True
            btnSearchEmp.CausesValidation = False
            Me.MainToolBar.OnClientButtonClicking = "tbarDetail_ClientButtonClicking"
            SetStatusToolbar(tbarDetail, CommonMessage.STATE_NORMAL, False)

            rgEmp.SetFilter()
            rgDetail.ClientSettings.EnablePostBackOnRowClick = True
            rgEmp.ClientSettings.EnablePostBackOnRowClick = True

            LoadComboData()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện load data cho rgDetail
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgDetail_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgDetail.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgDetail.DataSource = ApproveSetupExts
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedIndexChanged cho rgDetail
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgDetail_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgDetail.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            IDDetailSelecting = Decimal.Parse(CType(rgDetail.SelectedItems(0), GridDataItem).GetDataKeyValue("ID").ToString)
            LoadDetailView()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedNodeChanged cho ctrlOrg
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            OrgID = Decimal.Parse(ctrlOrg.CurrentValue)
            EmployeeID = Nothing
            SetStatusToolbar(tbarDetail, CurrentState, False)
            Refresh("employee click")
            ClearControlForInsert()
            cboApproveProcess.ClearValue()
            rgDetail.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện click cho tbarDetail
    ''' tương đương OnToolbar_Command trong các trang khác
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbarDetail_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles tbarDetail.ButtonClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    Me.CurrentState = CommonMessage.STATE_NEW
                    IDDetailSelecting = Nothing
                    UpdateControlState()
                    ClearControlForInsert()
                Case CommonMessage.TOOLBARITEM_EDIT
                    Me.CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE

                    If Not Page.IsValid Then
                        Exit Sub
                    End If

                    Dim db As New CommonRepository
                    If Not IDDetailSelecting.HasValue Then
                        'Thêm mới
                        If chkReplaceAll.Checked = False Then
                            If rdFromDate.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Nhập thay thế từ ngày"), NotifyType.Warning)
                                Exit Sub
                            End If
                            If rdToDate.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Nhập đến ngày"), NotifyType.Warning)
                            End If
                        End If
                        Dim itemAdd As New ApproveSetupExtDTO
                        With itemAdd
                            .EMPLOYEE_ID = EmployeeID
                            .PROCESS_ID = Decimal.Parse(cboApproveProcess.SelectedValue)
                            .SUB_EMPLOYEE_ID = Decimal.Parse(hidEmployeeID.Value)
                            .FROM_DATE = rdFromDate.SelectedDate
                            .TO_DATE = rdToDate.SelectedDate
                            .REPALCEALL = chkReplaceAll.Checked
                        End With
                        If (From p In ApproveSetupExts
                            Where ((itemAdd.FROM_DATE >= p.FROM_DATE And itemAdd.TO_DATE <= p.TO_DATE) _
                             Or (itemAdd.TO_DATE >= p.FROM_DATE And itemAdd.TO_DATE <= p.TO_DATE) _
                             Or (itemAdd.FROM_DATE < p.FROM_DATE And itemAdd.TO_DATE > p.TO_DATE)) _
                         And p.PROCESS_ID = itemAdd.PROCESS_ID).Count > 0 Then
                            ShowMessage(Translate("Tại một thời điểm không thể tồn tại nhiều nhân viên thay thế. Thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If db.InsertApproveSetupExt(itemAdd) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            Exit Sub
                        End If
                    Else
                        ' Sửa đổi
                        If chkReplaceAll.Checked = False Then
                            If rdFromDate.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Nhập thay thế từ ngày"), NotifyType.Warning)
                                Exit Sub
                            End If
                            If rdToDate.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Nhập đến ngày"), NotifyType.Warning)
                            End If
                        End If
                        Dim idEdit As Decimal = IDDetailSelecting.Value

                        Dim itemEdit As ApproveSetupExtDTO = db.GetApproveSetupExt(idEdit)
                        With itemEdit
                            .PROCESS_ID = Decimal.Parse(cboApproveProcess.SelectedValue)
                            .SUB_EMPLOYEE_ID = Decimal.Parse(hidEmployeeID.Value)
                            .FROM_DATE = rdFromDate.SelectedDate
                            .TO_DATE = rdToDate.SelectedDate
                            .REPALCEALL = chkReplaceAll.Checked
                        End With
                        Dim lstID As New List(Of Decimal)
                        lstID.Add(idEdit)
                        Using rep As New CommonRepository
                            If rep.CheckExistIDTable(lstID, "SE_APP_SETUPEXT", "ID") Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                Exit Sub
                            End If
                        End Using
                        If (From p In ApproveSetupExts
                            Where ((itemEdit.FROM_DATE >= p.FROM_DATE And itemEdit.TO_DATE <= p.TO_DATE) _
                                   Or (itemEdit.TO_DATE >= p.FROM_DATE And itemEdit.TO_DATE <= p.TO_DATE) _
                                   Or (itemEdit.FROM_DATE < p.FROM_DATE And itemEdit.TO_DATE > p.TO_DATE)) _
                               And p.ID <> itemEdit.ID And p.PROCESS_ID = itemEdit.PROCESS_ID).Count > 0 Then
                            ShowMessage(Translate("Tại một thời điểm không thể tồn tại nhiều nhân viên thay thế. Thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If db.UpdateApproveSetupExt(itemEdit) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            Exit Sub
                        End If
                    End If

                    Refresh("update view")
                    Me.CurrentState = CommonMessage.STATE_NORMAL
                    ClearControlForInsert()
                    cboApproveProcess.ClearValue()
                    rgDetail.Rebind()
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Me.CurrentState = CommonMessage.STATE_NORMAL
                    LoadDetailView()
                    ClearControlForInsert()
                    cboApproveProcess.ClearValue()
                    rgDetail.Rebind()
                    UpdateControlState()
                    Me.ExcuteScript("Resize", "ResizeSplitter")

                Case CommonMessage.TOOLBARITEM_DELETE
                    Dim idDelete = CType(rgDetail.SelectedItems(0), GridDataItem).GetDataKeyValue("ID")

                    Dim db As New CommonRepository

                    Dim listDelete As New List(Of Decimal)
                    listDelete.Add(idDelete)

                    ListToDelete = listDelete

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
    ''' 25/07/2017 10:00
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
                Dim db As New CommonRepository

                If db.DeleteApproveSetupExt(ListToDelete) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Refresh("update view")
                    ClearControlForInsert()
                    cboApproveProcess.ClearValue()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If

            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện load data cho rgEmp
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmp_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEmp.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgEmp.DataSource = Employees
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedIndexChanged cho rgEmp
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgEmp.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CurrentState = CommonMessage.STATE_NORMAL
            UpdateControlState()

            EmployeeID = Decimal.Parse(CType(rgEmp.SelectedItems(0), GridDataItem).GetDataKeyValue("ID").ToString())

            Refresh("update view")
            ClearControlForInsert()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#Region "FindEmployeeButton"

    ''' <lastupdate>
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện click nút cancel cho ctrlFindEmployeePopup
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện chọn 1 nhân viên cho ctrlFindEmployeePopup
    ''' </summary>
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
                If item.ID = EmployeeID Then
                    isLoadPopup = 0
                    ShowMessage("Nhân viên thay thế không được giống nhân viên được thay thế. Thao tác thực hiện không thành công", NotifyType.Warning)
                    Exit Sub
                End If
                hidEmployeeID.Value = item.ID.ToString
                txtEmployeeCode.Text = item.EMPLOYEE_CODE
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
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện click nút tìm kiếm nhần viên cho ctrlFindEmployeePopup
    ''' </summary>
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

#Region "Validator"

    ''' <lastupdate>
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện validate cho control rdToDate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalFromDateToDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalFromDateToDate.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            args.IsValid = True
            If rdToDate.IsEmpty OrElse rdFromDate.IsEmpty Then
                Exit Sub
            End If

            If rdFromDate.SelectedDate.Value > rdToDate.SelectedDate.Value Then
                args.IsValid = False
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện validate cho control rdFromDate and rdToDate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCheckDateExist_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCheckDateExist.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim db As New CommonRepository
            args.IsValid = True
            Dim idExclude As Decimal? = IDDetailSelecting
            If rdFromDate.SelectedDate.HasValue AndAlso rdToDate.SelectedDate.HasValue Then
                Dim itemCheck As New ApproveSetupExtDTO _
                    With {
                        .EMPLOYEE_ID = EmployeeID,
                        .PROCESS_ID = Decimal.Parse(cboApproveProcess.SelectedValue),
                        .FROM_DATE = rdFromDate.SelectedDate,
                        .TO_DATE = rdToDate.SelectedDate
                    }

                args.IsValid = db.IsExistApproveSetupExtByDate(itemCheck, idExclude)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region
#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc cap nhat trang thai cac control trong page
    ''' Cap nhat trang thai rgDetail, rgEmp, ctrlOrg
    ''' Cap nhat trang thai hien tai
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SetStatusToolbar(tbarDetail, Me.CurrentState)
            Select Case Me.CurrentState
                Case CommonMessage.STATE_NORMAL
                    EnabledGrid(rgDetail, True)
                    EnabledGrid(rgEmp, True)
                    pnlDetail.Enabled = False
                    ctrlOrg.Enabled = True

                Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NEW
                    EnabledGrid(rgDetail, True)
                    EnabledGrid(rgEmp, True)
                    pnlDetail.Enabled = True
                    ctrlOrg.Enabled = False

            End Select
            ShowPopupEmployee()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức cập nhật lại trạng thái cho các control sau khi thực hiện các command
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            Select Case Message
                Case "employee click"
                    If OrgID Is Nothing Then
                        ApproveSetupExts = New List(Of ApproveSetupExtDTO)
                        Employees = New List(Of EmployeeDTO)
                    Else
                        Dim db As New CommonRepository
                        Dim orgList = ctrlOrg.GetAllChild(OrgID.Value)
                        Employees = db.GetListEmployee(orgList, CDec(Val(chkter.Checked)))
                        ApproveSetupExts = New List(Of ApproveSetupExtDTO)
                    End If
                    rgDetail.Rebind()
                    rgEmp.Rebind()
                Case "update view"
                    If EmployeeID Is Nothing Then
                        ApproveSetupExts = New List(Of ApproveSetupExtDTO)
                    Else
                        Dim db As New CommonRepository

                        Dim Sorts As String = rgDetail.MasterTableView.SortExpressions.GetSortString()
                        If Sorts IsNot Nothing Then
                            ApproveSetupExts = db.GetApproveSetupExtList(EmployeeID.Value, Sorts)
                        Else
                            'ApproveSetupExts = db.GetApproveSetupExtList(OrgID)
                            ApproveSetupExts = db.GetApproveSetupExtList(EmployeeID.Value)
                        End If
                    End If
                    rgDetail.Rebind()
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xóa trạng thái của các textbox để chuẩn bị insert
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlForInsert()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            cboApproveProcess.ClearValue()
            txtEmployeeCode.Text = ""
            txtEmployeeName.Text = ""
            hidEmployeeID.Value = ""
            chkReplaceAll.Checked = False
            rdFromDate.SelectedDate = Nothing
            rdToDate.SelectedDate = Nothing
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức load data cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadComboData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim db As New CommonRepository

            Dim dbProcess = db.GetApproveProcessList()
            cboApproveProcess.DataSource = dbProcess
            cboApproveProcess.DataTextField = "NAME"
            cboApproveProcess.DataValueField = "ID"
            cboApproveProcess.DataBind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức load data lên textbox khi chọn 1 bản ghi
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDetailView()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim db As New CommonRepository
            If IDDetailSelecting IsNot Nothing Then
                Dim itemSelected = db.GetApproveSetupExt(IDDetailSelecting)

                If itemSelected IsNot Nothing Then
                    cboApproveProcess.SelectedValue = itemSelected.PROCESS_ID.ToString
                    hidEmployeeID.Value = itemSelected.SUB_EMPLOYEE_ID.ToString
                    txtEmployeeCode.Text = itemSelected.SUB_EMPLOYEE_CODE
                    txtEmployeeName.Text = itemSelected.SUB_EMPLOYEE_NAME
                    rdFromDate.SelectedDate = itemSelected.FROM_DATE
                    rdToDate.SelectedDate = itemSelected.TO_DATE
                    chkReplaceAll.Checked = itemSelected.REPALCEALL
                Else
                    ShowMessage(Translate("Đã có lỗi khi truy xuất thông tin đang chọn."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức enable hoặc disable các nút chức năng của toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetStatusToolbar(ByVal _tbar As RadToolBar, ByVal formState As String, Optional ByVal stateToAll As Boolean? = Nothing)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'If stateToAll.HasValue Then
            '    For Each item As RadToolBarItem In _tbar.Items
            '        item.Enabled = stateToAll.Value
            '    Next
            '    Exit Sub
            'End If

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
    ''' 25/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức hiển thị popup 
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
#End Region


    Private Sub chkter_CheckedChanged(sender As Object, e As EventArgs) Handles chkter.CheckedChanged
        Refresh("employee click")

    End Sub
End Class