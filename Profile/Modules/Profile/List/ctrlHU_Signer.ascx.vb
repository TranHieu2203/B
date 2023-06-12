Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Public Class ctrlHU_Signer
    Inherits Common.CommonView
    Dim log As New UserLog
    Dim psp As New ProfileStoreProcedure
    Dim com As New CommonProcedureNew
    ' Dim cons As New Contant_OtherList_Iprofile
    Dim cons_com As New Contant_Common
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
#Region "Property"
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("ID", GetType(String))
                dt.Columns.Add("SIGNER_CODE", GetType(String))
                dt.Columns.Add("NAME", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("REMARK", GetType(String))
                dt.Columns.Add("ORG_ID", GetType(String))
                dt.Columns.Add("ACTFLG", GetType(String))
                dt.Columns.Add("SIGNER_ID", GetType(Decimal))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    Public Property dtExport As DataTable
        Get
            Return PageViewState(Me.ID & "_dtExport")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtExport") = value
        End Set
    End Property

    Public Property CurrentUpdateCode As String
        Get
            Return PageViewState(Me.ID & "_CurrentUpdateCode")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_CurrentUpdateCode") = value
        End Set
    End Property

    Public Property CurrentUpdateDate As Date
        Get
            Return PageViewState(Me.ID & "_CurrentUpdateDate")
        End Get
        Set(ByVal value As Date)
            PageViewState(Me.ID & "_CurrentUpdateDate") = value
        End Set
    End Property



    Public Property SelectedItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_DeleteItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_DeleteItemList") = value
        End Set
    End Property

    Protected Function RepareDataForAction() As List(Of Decimal)
        Dim lst As New List(Of Decimal)
        For Each dr As Telerik.Web.UI.GridDataItem In rgData.SelectedItems
            lst.Add(dr.GetDataKeyValue("ID"))
        Next
        Return lst
    End Function


    Protected Property SelectedId As Decimal
        Get
            Return PageViewState(Me.ID & "_SelectedId")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_SelectedId") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
            UpdateControlState1()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        SetGridFilter(rgData)
        rgData.AllowCustomPaging = True
        rgData.PageSize = Common.Common.DefaultPageSize
        'rgData.ClientSettings.EnablePostBackOnRowClick = True
        InitControl()
        If Not IsPostBack Then

            GetDataComboBox()
            ViewConfig(RadPane2)
            GirdConfig(rgData)
        End If
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Export, ToolbarItem.Delete,
                                       ToolbarItem.Active,
                                    ToolbarItem.Deactive)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
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

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub GetDataCombo()
        Dim dtData As New DataTable
        Dim rep As New ProfileRepository
        Try
            'Default là trạng thái 
            'dtData = com.GET_COMBOBOX(cons.OT_OTHER_LIST, "CODE", "NAME_VN", " AND TYPE_CODE='" + "GROUP_DECISION" + "' ", "NAME_VN", True)
            'If dtData.Rows.Count > 0 Then
            '    FillRadCombobox(cboDecisionGroup, dtData, "NAME_VN", "CODE", False)
            'End If
            'dtData = com.GET_COMBOBOX(cons.OT_OTHER_LIST, "CODE", "NAME_VN", " AND TYPE_CODE='" + cons.CONCURENTLY_TYPE + "' ", "NAME_VN", True)
            'FillRadCombobox(cboConcurrentlyType, dtData, "NAME_VN", "CODE", False)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Sub UpdateControlState1()
        Try
            If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = False
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
                Case 2
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = False
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = True
            End Select

        Catch ex As Exception
            Throw ex
        End Try
        ChangeToolbarState()
    End Sub
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    psp.DisableControls(MainPanel, False)
                    Utilities.EnabledGridNotPostback(rgData, True)
                Case CommonMessage.STATE_NEW
                    psp.DisableControls(MainPanel, True)
                    Utilities.EnabledGridNotPostback(rgData, False)
                    txtCode.Enabled = False
                Case CommonMessage.STATE_EDIT
                    psp.DisableControls(MainPanel, True)
                    Utilities.EnabledGridNotPostback(rgData, False)
                    txtCode.Enabled = False
                Case CommonMessage.STATE_DEACTIVE

                    Dim lstDeletes As New List(Of Decimal)
                    Dim lst As String
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        'lstDeletes.Add(item.GetDataKeyValue("ID"))
                        lst += item.GetDataKeyValue("ID").ToString() + ";"
                    Next
                    If rep.DeactiveAndActiveSigner(lst, 0) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    Dim lst As String
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        'lstDeletes.Add(item.GetDataKeyValue("ID"))
                        lst += (item.GetDataKeyValue("ID")).ToString() + ";"
                    Next
                    If rep.DeactiveAndActiveSigner(lst, 1) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                    End If
            End Select
            ChangeToolbarState()

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        GetDataCombo()
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("SIGNER_CODE", txtCode)
        dic.Add("NAME", txtTYPE_NAME)
        dic.Add("TITLE_NAME", txtNAME_EN)
        dic.Add("ORG_ID", rtORG_ID)
        dic.Add("ORG_NAME", rtOrg_Name)
        'dic.Add("REMARK", txtRemark)
        Utilities.OnClientRowSelectedChanged(rgData, dic)
    End Sub
    Private Sub rgData_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgData.SelectedIndexChanged
        Try
            If rgData.SelectedItems.Count > 0 Then
                Dim slItem As GridDataItem
                slItem = rgData.SelectedItems(0)
                txtCode.Text = slItem.GetDataKeyValue("SIGNER_CODE").ToString
                txtTYPE_NAME.Text = slItem.GetDataKeyValue("NAME").ToString
                txtNAME_EN.Text = slItem.GetDataKeyValue("TITLE_NAME").ToString
                rtOrg_Name.Text = slItem.GetDataKeyValue("ORG_NAME").ToString
                txtRemark.Text = slItem.GetDataKeyValue("REMARK").ToString
                If slItem.GetDataKeyValue("SIGNER_ID").ToString <> "" Then
                    hidEmpID.Value = slItem.GetDataKeyValue("SIGNER_ID")
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"
    Private Sub btnFindEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindEmp.Click
        Try
            isLoadPopup = 1
            UpdateControlState1()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Try
            Select Case isLoadPopup
                Case 1 'Employee
                    lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
                    For Each item In lstCommonEmployee
                        hidEmpID.Value = item.EMPLOYEE_ID
                        txtCode.Text = item.EMPLOYEE_CODE
                        txtTYPE_NAME.Text = item.FULLNAME_VN
                        txtNAME_EN.Text = item.TITLE_NAME
                        rtOrg_Name.Text = item.ORG_NAME
                        rtOrg_Name.ToolTip = item.ORG_DESC
                        'rtORG_ID.Text = item.ORG_ID
                    Next
            End Select
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        'Dim gID As Decimal
        Dim rep As New ProfileRepository
        Dim lstEmp As New List(Of EmployeePopupFindDTO)
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    psp.ResetControlValue(MainPanel)
                    IDSelect = 0
                    rgData.Rebind()

                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    EnabledGrid(rgData, False)
                    IDSelect = rgData.SelectedValue
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim filter As New SignerDTO
                    filter.USER_ID = LogHelper.CurrentUser.ID
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        Dim _param = New ProfileBusiness.ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                         .IS_DISSOLVE = ctrlOrg.IsDissolve}
                        dtData = rep.GET_HU_SIGNER(filter, _param)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "danh sach")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If Save() Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            rgData.Rebind()
                            SelectedItemDataGridByKey(rgData, Decimal.Parse(IDSelect))
                        End If
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearControlValue(rtOrg_Name, txtCode, txtTYPE_NAME, txtNAME_EN, txtRemark)
                    rgData.Rebind()
            End Select
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            Dim rep As New ProfileRepository
            Dim tr As String
            log = LogHelper.GetUserLog
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim lst As String
                For idx = 0 To rgData.SelectedItems.Count - 1
                    Dim item As GridDataItem = rgData.SelectedItems(idx)
                    'lstDeletes.Add(item.GetDataKeyValue("ID"))
                    lst += item.GetDataKeyValue("ID").ToString() + ";"
                Next
                If lst.ToString <> "" Then
                    rep.DeleteSigner(lst)
                    ClearControlValue(rtOrg_Name, txtCode, txtNAME_EN, txtRemark, txtTYPE_NAME)
                    CurrentState = CommonMessage.STATE_NORMAL
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState()
            End If

            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim filter As New SignerDTO
        Dim rep As New ProfileRepository
        Try
            SetValueObjectByRadGrid(rgData, filter)
            Dim _param = New ProfileBusiness.ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}
            If IsNumeric(ctrlOrg.CurrentValue) Then
                rtORG_ID.Text = ctrlOrg.CurrentValue
            End If
            filter.USER_ID = LogHelper.CurrentUser.ID
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim dt = rep.GET_HU_SIGNER(filter, _param)
            If dt IsNot Nothing Then
                dtData = dt
            End If
            'If Not IsPostBack Then
            '    DesignGrid(dtData)
            'End If
            rgData.DataSource = dtData
            rgData.VirtualItemCount = dtData.Rows.Count
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        Finally
            filter = Nothing
            rep.Dispose()
        End Try
    End Sub
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgData.Rebind()
            If IsNumeric(ctrlOrg.CurrentValue) Then
                rtORG_ID.Text = ctrlOrg.CurrentValue
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    '' Kiểm tra trùng 
    'Protected Sub validateCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles validateCode.ServerValidate
    '    Try
    '        Select Case CurrentState
    '            Case CommonMessage.STATE_NEW
    '                Dim dt = com.CHECK_CODE_TABLE(txtCode.Text, cons.PA_GROUP_SALARY)
    '                If Decimal.Parse(dt(0)("TEMP").ToString) > 0 Then
    '                    args.IsValid = False
    '                Else
    '                    args.IsValid = True
    '                End If
    '        End Select
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    ' Kiểm tra trùng ngay nghi
    'Protected Sub validateDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles validateDate.ServerValidate
    '    Try
    '        Select Case CurrentState
    '            Case CommonMessage.STATE_NEW
    '                If dtpHolidayDate.SelectedDate IsNot Nothing AndAlso _
    '                    psp.Check_DateHOLIDAY(dtpHolidayDate.SelectedDate) > 0 Then
    '                    args.IsValid = False
    '                Else
    '                    args.IsValid = True
    '                End If
    '        End Select
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
#End Region

#Region "Custom"
    Protected Sub DesignGrid(ByVal dt As DataTable)
        Dim rCol As GridBoundColumn
        Dim rColCheck As GridClientSelectColumn
        'Dim rColFile As GridButtonColumn
        Dim rColDate As GridDateTimeColumn
        Dim rColCheckBox As GridCheckBoxColumn
        rgData.MasterTableView.Columns.Clear()
        For Each column As DataColumn In dt.Columns
            If column.ColumnName = "CBSTATUS" Then
                rColCheck = New GridClientSelectColumn()
                rgData.MasterTableView.Columns.Add(rColCheck)
                rColCheck.HeaderStyle.Width = 30
                rColCheck.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            End If
            If Not column.ColumnName.Contains("ID") And Not column.ColumnName.Contains("CHECK") And _
             column.ColumnName <> "CBSTATUS" And Not column.ColumnName.Contains("DATE") Then
                rCol = New GridBoundColumn()
                rgData.MasterTableView.Columns.Add(rCol)
                rCol.DataField = column.ColumnName
                rCol.HeaderText = Translate(column.ColumnName)
                rCol.HeaderStyle.Width = 150
                rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rCol.AutoPostBackOnFilter = True
                rCol.CurrentFilterFunction = GridKnownFunction.Contains
                rCol.ShowFilterIcon = False
                rCol.FilterControlWidth = 130
                rCol.HeaderTooltip = Translate(column.ColumnName)
                rCol.FilterControlToolTip = Translate(column.ColumnName)
            End If
            If column.ColumnName.Contains("DATE") Then
                rColDate = New GridDateTimeColumn()
                rgData.MasterTableView.Columns.Add(rColDate)
                rColDate.DataField = column.ColumnName
                rColDate.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
                rColDate.HeaderText = Translate(column.ColumnName)
                rColDate.HeaderStyle.Width = 150
                rColDate.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rColDate.AutoPostBackOnFilter = True
                rColDate.CurrentFilterFunction = GridKnownFunction.EqualTo
                rColDate.ShowFilterIcon = False
                rColDate.FilterControlWidth = 130
                rColDate.FilterDateFormat = "dd/MM/yyyy"
                rColDate.HeaderTooltip = Translate(column.ColumnName)
                rColDate.FilterControlToolTip = Translate(column.ColumnName)
            End If
            If column.ColumnName.Contains("CHECK") Then
                rColCheckBox = New GridCheckBoxColumn()
                rgData.MasterTableView.Columns.Add(rColCheckBox)
                rColCheckBox.DataField = column.ColumnName
                rColCheckBox.HeaderText = Translate(column.ColumnName)
                rColCheckBox.HeaderStyle.Width = 50
                rColCheckBox.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rColCheckBox.FilterControlWidth = 50
                rColCheckBox.ShowFilterIcon = False
                rColCheckBox.HeaderTooltip = Translate(column.ColumnName)
                rColCheckBox.FilterControlToolTip = Translate(column.ColumnName)
            End If
        Next
    End Sub

    Private Function Save() As Boolean
        Dim rep As New ProfileRepository
        Dim result As Integer
        'Dim gID As Decimal
        Dim PA As New SignerDTO
        If txtCode.Text = "" Then
            ShowMessage(Translate("Bạn vui lòng nhập mã người ký"), Utilities.NotifyType.Warning)
            Exit Function
        End If

        log = LogHelper.GetUserLog
        PA.ID = IDSelect
        PA.SIGNER_ID = hidEmpID.Value
        PA.SIGNER_CODE = txtCode.Text
        If IsNumeric(rtORG_ID.Text) Then
            PA.ORG_ID = Decimal.Parse(rtORG_ID.Text)
        Else
            ShowMessage(Translate("Bạn vui lòng chọn phòng ban bên cây sơ đồ tổ chức"), Utilities.NotifyType.Warning)
            Exit Function
        End If
        PA.TITLE_NAME = txtNAME_EN.Text
        PA.NAME = txtTYPE_NAME.Text

        PA.REMARK = txtRemark.Text

        PA.CREATED_BY = log.Username
        PA.CREATED_LOG = log.Ip + "/" + log.ComputerName
        ' Cập nhât thông tin 
        If IDSelect <> 0 Then
            Dim check1 As Integer = rep.CHECK_EXIT(hidEmpID.Value, IDSelect, PA.ORG_ID, PA.TITLE_NAME)
            If check1 <> 0 Then
                ShowMessage(Translate("Dữ liệu đã tồn tại"), Utilities.NotifyType.Warning)
                Exit Function
            End If
            result = rep.UPDATE_HU_SIGNER(PA)
            ' Thêm mới thông tin
        Else
            Dim check As Integer = rep.CHECK_EXIT(hidEmpID.Value, 0, PA.ORG_ID, PA.TITLE_NAME)
            If check <> 0 Then
                ShowMessage(Translate("Dữ liệu đã tồn tại"), Utilities.NotifyType.Warning)
                Exit Function
            End If
            result = rep.INSERT_HU_SIGNER(PA)
        End If
        If result <> 0 Then
            IDSelect = result
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetDataComboBox()
        Dim repS As New ProfileStoreProcedure
        Try

        Catch ex As Exception
            Throw ex
        End Try
        'Dim dtData As New DataTable

        'dtData = com.GET_COMBOBOX(cons.OT_OTHER_LIST, "CODE", "NAME_VN", " AND TYPE_CODE='" + cons.ALLOW_TYPE + "' ", "NAME_VN", True)
        'If dtData.Rows.Count > 0 Then
        '    FillRadCombobox(cboTypeAllow, dtData, "NAME_VN", "CODE", True)
        'End If

        'dtData = com.GET_COMBOBOX(cons.OT_OTHER_LIST, "CODE", "NAME_VN", " AND TYPE_CODE='" + cons.ALLOW_GROUP + "' ", "NAME_VN", True)
        'If dtData.Rows.Count > 0 Then
        '    FillRadCombobox(cboGroupAllow, dtData, "NAME_VN", "CODE", True)
        'End If
    End Sub
#End Region



End Class