Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlApproveSetupEmp
    Inherits CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/ApproveProcess/" + Me.GetType().Name.ToString()
#Region "Property"
    Public Property ApproveSetups() As List(Of ApproveSetupDTO)
        Get
            Return ViewState(Me.ID & "_ApproveSetups")
        End Get
        Set(ByVal value As List(Of ApproveSetupDTO))
            ViewState(Me.ID & "_ApproveSetups") = value
        End Set
    End Property

    Public Property Employees() As List(Of EmployeeDTO)
        Get
            Return ViewState(Me.ID & "_Employees")
        End Get
        Set(ByVal value As List(Of EmployeeDTO))
            ViewState(Me.ID & "_Employees") = value
        End Set
    End Property

    Public Property ListToDelete As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_Delete_List")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_Delete_List") = value
        End Set
    End Property

    Public Property OrgID As Decimal?
        Get
            Return ViewState(Me.ID & "_OrgID")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_OrgID") = value
        End Set
    End Property

    Public Property EmployeeID As Decimal?
        Get
            Return ViewState(Me.ID & "_EmployeeID")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_EmployeeID") = value
        End Set
    End Property

    Public Property IDDetailSelecting As Decimal?
        Get
            Return ViewState(Me.ID & "_ID_Detail_Selecting")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_ID_Detail_Selecting") = value
        End Set
    End Property
#End Region

#Region "Event"
    ''' <summary>
    ''' Khởi tạo, load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
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
        If Not IsPostBack Then
            'ctrlOrg_SelectedNodeChanged(Nothing, Nothing)
            Refresh()
        End If
        UpdateControlState()
    End Sub
    ''' <summary>
    ''' Set datasource cho grid chi tiết thiết lập
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub rgDetail_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgDetail.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgDetail.DataSource = ApproveSetups
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Get ID Row được chọn trên grid chi tiết thiết lập
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgDetail_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgDetail.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            IDDetailSelecting = Decimal.Parse(CType(rgDetail.SelectedItems(0), GridDataItem).GetDataKeyValue("ID").ToString)
            LoadDetailView()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event selected Node treeview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try


            rgEmp.Rebind()

            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
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
                        Dim itemAdd As New ApproveSetupDTO
                        With itemAdd
                            .EMPLOYEE_ID = EmployeeID
                            .PROCESS_ID = Decimal.Parse(cboApproveProcess.SelectedValue)
                            .TEMPLATE_ID = Decimal.Parse(IIf(cboApproveTemplate.SelectedValue = "", 0, cboApproveTemplate.SelectedValue))
                            .FROM_DATE = rdFromDate.SelectedDate
                            .TO_DATE = rdToDate.SelectedDate
                        End With

                        If db.InsertApproveSetup(itemAdd) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            Exit Sub
                        End If
                    Else
                        ' Sửa đổi

                        Dim idEdit As Decimal = IDDetailSelecting.Value
                        Dim lst As New List(Of Decimal)
                        lst.Add(idEdit)
                        If db.CheckExistIDTable(lst, "SE_APP_SETUP", "ID") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                            Exit Sub
                        End If
                        Dim itemEdit As ApproveSetupDTO = db.GetApproveSetup(idEdit)
                        With itemEdit
                            .PROCESS_ID = Decimal.Parse(cboApproveProcess.SelectedValue)
                            .TEMPLATE_ID = Decimal.Parse(IIf(cboApproveTemplate.SelectedValue = "", 0, cboApproveTemplate.SelectedValue))
                            .FROM_DATE = rdFromDate.SelectedDate
                            .TO_DATE = rdToDate.SelectedDate
                        End With

                        If db.UpdateApproveSetup(itemEdit) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            Exit Sub
                        End If
                    End If

                    Refresh("1")
                    Me.CurrentState = CommonMessage.STATE_NORMAL

                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Me.CurrentState = CommonMessage.STATE_NORMAL
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
    ''' <summary>
    ''' Event Yes.No hỏi xóa
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

                If db.DeleteApproveSetup(ListToDelete) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Refresh("1")
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
    ''' <summary>
    ''' Set datasource cho grid Nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmp_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEmp.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            OrgID = Decimal.Parse(IIf(ctrlOrg.CurrentValue Is Nothing, 0, ctrlOrg.CurrentValue))
            EmployeeID = Nothing
            SetStatusToolbar(tbarDetail, CurrentState, False)
            Refresh()
            rgEmp.DataSource = Employees
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event click chọn row trên grid nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgEmp.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CurrentState = CommonMessage.STATE_NORMAL
            UpdateControlState()

            EmployeeID = Decimal.Parse(CType(rgEmp.SelectedItems(0), GridDataItem).GetDataKeyValue("ID").ToString())

            Refresh("1")

            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#Region "Validator"
    ''' <summary>
    ''' Validate combobox template
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalApproveTemplate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalApproveTemplate.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If cboApproveProcess.SelectedValue = "" Then Return
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                Dim db As New CommonRepository
                Dim dbTemplate = db.GetApproveTemplateList().Where(Function(p) p.TEMPLATE_TYPE = 1 And p.ID = cboApproveTemplate.SelectedValue And p.ACTFLG = "A").ToList()
                If dbTemplate.Count = 0 Then
                    args.IsValid = False
                End If
            End If
            If Not args.IsValid Then
                LoadComboData()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Validate đến ngày áp dụng
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
    ''' <summary>
    ''' Validate từ ngày áp dụng đã được set
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCheckDateExist_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCheckDateExist.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim db As New CommonRepository

            Dim idExclude As Decimal? = IDDetailSelecting

            Dim itemCheck As New ApproveSetupDTO _
            With {
                .EMPLOYEE_ID = EmployeeID,
                .PROCESS_ID = Decimal.Parse(cboApproveProcess.SelectedValue),
                .TEMPLATE_ID = Decimal.Parse(cboApproveTemplate.SelectedValue),
                .FROM_DATE = rdFromDate.SelectedDate,
                .TO_DATE = rdToDate.SelectedDate
            }

            args.IsValid = db.IsExistApproveSetupByDate(itemCheck, idExclude)

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
    ''' <summary>
    ''' Update trạng thái control theo trạng thái page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SetStatusToolbar(tbarDetail, Me.CurrentState)
            Select Case Me.CurrentState


                Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NEW
                    EnabledGrid(rgDetail, True)
                    EnabledGrid(rgEmp, True)
                    Utilities.EnableRadCombo(cboApproveProcess, True)
                    Utilities.EnableRadCombo(cboApproveTemplate, True)
                    Utilities.EnableRadDatePicker(rdFromDate, True)
                    Utilities.EnableRadDatePicker(rdToDate, True)
                    ctrlOrg.Enabled = False
                Case Else
                    EnabledGrid(rgDetail, True)
                    EnabledGrid(rgEmp, True)
                    'Utilities.EnableRadCombo(cboApproveProcess, False)
                    'Utilities.EnableRadCombo(cboApproveTemplate, False)
                    cboApproveProcess.Enabled = False
                    cboApproveTemplate.Enabled = False
                    Utilities.EnableRadDatePicker(rdFromDate, False)
                    Utilities.EnableRadDatePicker(rdToDate, False)
                    ctrlOrg.Enabled = True
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <summary>
    ''' Load, reload page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case Message
                Case ""
                    If OrgID Is Nothing Then
                        Employees = New List(Of EmployeeDTO)
                        ApproveSetups = New List(Of ApproveSetupDTO)
                    Else
                        Dim db As New CommonRepository
                        Dim orgList = ctrlOrg.GetAllChild(OrgID.Value)

                        Dim empList = db.GetListEmployee(orgList).ToList
                        Employees = empList '.Where(Function(p) p.CONTRACT_ID.HasValue).ToList

                        ApproveSetups = New List(Of ApproveSetupDTO)

                    End If
                    rgDetail.Rebind()
                    'rgEmp.Rebind()
                Case "1"
                    If EmployeeID Is Nothing Then
                        Employees = New List(Of EmployeeDTO)
                    Else
                        Dim db As New CommonRepository

                        Dim Sorts As String = rgDetail.MasterTableView.SortExpressions.GetSortString()
                        If Sorts IsNot Nothing Then
                            ApproveSetups = db.GetApproveSetupByEmployee(EmployeeID.Value, Sorts)
                        Else
                            ApproveSetups = db.GetApproveSetupByEmployee(EmployeeID.Value)
                        End If
                    End If

                    rgDetail.Rebind()
                    ClearControlForInsert()
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try


    End Sub
    ''' <summary>
    ''' Clear data các control khi insert xong
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlForInsert()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            cboApproveProcess.ClearValue()
            cboApproveTemplate.ClearValue()
            rdFromDate.SelectedDate = Nothing
            rdToDate.SelectedDate = Nothing
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <summary>
    ''' Load data cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadComboData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim db As New CommonRepository

            Dim dbProcess = db.GetApproveProcessList().Where(Function(p) p.ACTFLG = "A").ToList()
            cboApproveProcess.DataSource = dbProcess
            cboApproveProcess.DataTextField = "NAME"
            cboApproveProcess.DataValueField = "ID"
            cboApproveProcess.DataBind()
            'cboApproveProcess.Items.Insert(0, New RadComboBoxItem([String].Empty, [String].Empty))
            'cboApproveProcess.SelectedIndex = 0

            Dim dbTemplate = db.GetApproveTemplateList().Where(Function(p) p.TEMPLATE_TYPE = 1).ToList()
            cboApproveTemplate.DataSource = dbTemplate
            cboApproveTemplate.DataTextField = "TEMPLATE_NAME"
            cboApproveTemplate.DataValueField = "ID"
            cboApproveTemplate.DataBind()
            'cboApproveTemplate.Items.Insert(0, New RadComboBoxItem([String].Empty, [String].Empty))
            'cboApproveTemplate.SelectedIndex = 0
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    Private Sub LoadDetailView()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim db As New CommonRepository
            Dim itemSelected = db.GetApproveSetup(IDDetailSelecting)

            If itemSelected IsNot Nothing Then
                cboApproveProcess.SelectedValue = itemSelected.PROCESS_ID.ToString
                cboApproveTemplate.SelectedValue = itemSelected.TEMPLATE_ID.ToString
                rdFromDate.SelectedDate = itemSelected.FROM_DATE
                rdToDate.SelectedDate = itemSelected.TO_DATE
            Else
                ShowMessage(Translate("Đã có lỗi khi truy xuất thông tin đang chọn."), NotifyType.Error)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <summary>
    ''' Set trạng thái của item trên menu toolbar
    ''' </summary>
    ''' <param name="_tbar"></param>
    ''' <param name="formState"></param>
    ''' <param name="stateToAll"></param>
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
#End Region

End Class