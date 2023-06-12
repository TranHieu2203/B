Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI

Public Class ctrlAlowance
    Inherits Common.CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents TerminateView As ViewBase
#Region "Property"
    Dim dtData As New DataTable
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    Property vData As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataView")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataView") = value
        End Set
    End Property
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
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            UpdateControlState()
            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrg.CheckBoxes = TreeNodeTypes.None
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgData.AllowCustomPaging = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            ctrlUpload.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload.MaxFileInput = 1
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub


    Private Sub GetDataAllowance_list()
        Dim rep As New PayrollRepository
        Try
            Dim _filter As New AllowanceListDTO

            _filter.ACTFLG = "A"
            _filter.IS_INSURANCE = 0

            Dim listAllowance = rep.GetAllowanceList(_filter)
            rep.Dispose()
            FillDropDownList(cboPhucap, listAllowance, "NAME", "ID", Common.Common.SystemLanguage, False, cboPhucap.SelectedValue)
            'FillDropDownList(cboGROUP_TYPE, ListComboData.LIST_GROUP_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, False, cboGROUP_TYPE.SelectedValue)
            'FillDropDownList(cboImport_Type, ListComboData.LIST_IMPORT_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, False, cboImport_Type.SelectedValue)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Overrides Sub BindData()
        Try
            rgData.PageSize = 50
            GetDataAllowance_list()


            'Dim dic As New Dictionary(Of String, Control)
            'dic.Add("ID", hidID)
            ''dic.Add("TYPE_PAYMENT", cboTYPE_PAYMENT)

            'dic.Add("NAME_VN", txtNAME_VN)
            ''dic.Add("INPUT_FORMULER", txtINPUT_FORMULER)
            'dic.Add("DATA_TYPE", cboDATA_TYPE)
            'dic.Add("GROUP_TYPE_ID", cboGROUP_TYPE)
            'dic.Add("COL_NAME;COL_NAME", cboFIELD)
            'dic.Add("OBJ_SAL_ID", cboOBJ_SALARY)
            'dic.Add("COL_INDEX", nmCOL_INDEX)
            'dic.Add("IS_VISIBLE", chkIS_VISIBLE)
            ''dic.Add("IS_INPUT", chkIS_INPUT)
            ''dic.Add("IS_CALCULATE", chkIS_CALCULATE)
            'dic.Add("IS_IMPORT", chkIS_IMPORT)
            'dic.Add("IS_WORKARISING", chkIS_WorkArising)
            'dic.Add("IS_SUMARISING", chkIS_SumArising)
            'dic.Add("IS_PAYBACK", chkIS_Payback)
            ''dic.Add("IS_WORKDAY", chkIS_WorkDay)
            ''dic.Add("IS_SUMDAY", chkIS_SumDay)
            'dic.Add("IMPORT_TYPE_ID", cboIMPORT_TYPE)
            'dic.Add("REMARK", txtRemark)
            'dic.Add("EFFECTIVE_DATE", dtpEffect)
            'dic.Add("EXPIRE_DATE", dtpExpire)
            'Utilities.OnClientRowSelectedChanged(rgData, dic)
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Active, ToolbarItem.Deactive,
                                       ToolbarItem.Delete, ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgData, True)
                    EnableControlAll(False, cboPhucap, txtEmployeeCode, txtSotien, txtTennhanvien, txtGhichu)
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, cboPhucap, txtEmployeeCode, txtSotien, txtTennhanvien, txtGhichu)
                    ClearControlValue(cboPhucap, txtEmployeeCode, txtSotien, txtTennhanvien, txtGhichu)
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, cboPhucap, txtEmployeeCode, txtSotien, txtTennhanvien, txtGhichu)
                Case CommonMessage.STATE_DEACTIVE, CommonMessage.STATE_ACTIVE, CommonMessage.STATE_DELETE
                    EnabledGridNotPostback(rgData, True)
                    EnableControlAll(False, cboPhucap, txtEmployeeCode, txtSotien, txtTennhanvien, txtGhichu)
                    ClearControlValue(cboPhucap, txtEmployeeCode, txtSotien, txtTennhanvien, txtGhichu)
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    'Private Sub btnSeach_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeach.Click
    '    rgData.Rebind()
    'End Sub

    Protected Sub btnFindCommon_Click(ByVal sender As Object,
                                    ByVal e As EventArgs) Handles _
                                btnFindEmployee.Click
        Try
            If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                ctrlFindEmployeePopup.MultiSelect = False
                ctrlFindEmployeePopup.MustHaveContract = False
            End If
            ctrlFindEmployeePopup.Show()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Try
            Dim empID = ctrlFindEmployeePopup.SelectedEmployeeID(0)
            txtTennhanvien.Text = empID
            txtEmployid.Text = empID

            'If Not String.IsNullOrEmpty(cboDecisionType.SelectedValue) And Not String.IsNullOrEmpty(txtEmployeeCode.Text) Then
            '    txtDecisionNo.Text = txtEmployeeCode.Text + "/" + hidDecision.Value.Trim + "/" + DateTime.Now.ToString("MMyy")
            'Else
            '    txtDecisionNo.Text = String.Empty
            'End If            

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub



    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

        Dim objAllowanceLis As New AllowanceListDTO
        Dim objAllowance As New AllowanceDTO
        Dim gID As Decimal

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
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
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next

                    Using rep As New PayrollRepository
                        If Not rep.CheckExistInDatabase(lstID, TABLE_NAME.PA_SALARY_GROUP) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                            Return
                        End If
                    End Using

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        '  dtData = CreateDataFilter()
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "SalaryType")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objAllowance.employee_id = txtTennhanvien.Text
                        objAllowance.allowance_type = cboPhucap.SelectedValue
                        objAllowance.Amount = txtSotien.Text
                        objAllowance.Effect_date = dpTungay.SelectedDate
                        objAllowance.Exp_date = dpDenngay.SelectedDate
                        objAllowance.remark = txtGhichu.Text

                        Using rep As New PayrollRepository
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    objAllowance.actflg = "A"
                                    If rep.InsertAllowance(objAllowance, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = gID
                                        Refresh("InsertView")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                                Case CommonMessage.STATE_EDIT
                                    objAllowance.id = rgData.SelectedValue
                                    If rep.ModifyAllowance(objAllowance, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = objAllowance.id
                                        Refresh("UpdateView")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                            End Select
                        End Using
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

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
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearControlValue(txtEmployeeCode, txtGhichu, txtSotien, cboPhucap)
            End Select

            UpdateControlState()
            'Select Case CType(e.Item, RadToolBarButton).CommandName
            '    Case CommonMessage.TOOLBARITEM_EXPORT
            '        Dim dtColName As New DataTable
            '        dtColName.Columns.Add("COLVAL")
            '        dtColName.Columns.Add("COLNAME")
            '        dtColName.Columns.Add("COLDATA")

            '        'For Each node As RadTreeNode In ctrlListSalary.CheckedNodes
            '        '    If node.Value = "NULL" Or node.Value = "0" Then Continue For
            '        '    Dim row As DataRow = dtColName.NewRow
            '        '    row("COLVAL") = node.Value
            '        '    row("COLNAME") = node.Text
            '        '    row("COLDATA") = "&=DATA." & node.Value
            '        '    dtColName.Rows.Add(row)
            '        'Next
            '        Session("IMPORTSALARY_COLNAME") = dtColName
            '        'Using rep As New PayrollRepository
            '        '    Dim dt = rep.GetImportSalary(Utilities.ObjToInt(cboPeriod.SelectedValue), Utilities.ObjToInt(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve, Utilities.ObjToString(rtxtEmployee.Text))
            '        '    Session("IMPORTSALARY_DATACOL") = dt
            '        'End Using
            '        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportSalary')", True)
            '    Case CommonMessage.TOOLBARITEM_IMPORT
            '        ctrlUpload.Show()
            '        'Case CommonMessage.TOOLBARITEM_SAVE
            '        '    Dim rep As New PayrollRepository
            '        '    Dim stringKey As New List(Of String)
            '        '    For Each node As RadTreeNode In ctrlListSalary.CheckedNodes
            '        '        If node.Value = "NULL" Or node.Value = "0" Then Continue For
            '        '        stringKey.Add(node.Value)
            '        '    Next
            '        '    Dim RecordSussces As Integer = 0
            '        '    If stringKey.Count <= 0 Then
            '        '        ShowMessage("Bạn phải chọn ít nhất 1 khoản tiền", NotifyType.Warning)
            '        '        Exit Sub
            '        '    End If
            '        'If rep.SaveImport(Utilities.ObjToDecima(cboPeriod.SelectedValue), vData, stringKey, RecordSussces) Then
            '        '    ShowMessage("Lưu dữ liệu thành công " & RecordSussces & " bản ghi.", NotifyType.Success)
            '        'End If
            'End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_APPROVE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try

            'rgData.CurrentPageIndex = 0
            'rgData.MasterTableView.SortExpressions.Clear()
            'rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Protected Sub rgData_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub



#End Region

#Region "Custom"

    Protected Sub CreateDataFilter()
        'Dim rep As New PayrollRepository
        Try
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim TotalRow As Decimal = 0
            'If Sorts Is Nothing Then
            '    vData = rep.GetImportSalary(Utilities.ObjToInt(cboPeriod.SelectedValue), Utilities.ObjToInt(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve, Utilities.ObjToString(rtxtEmployee.Text))
            'Else
            '    vData = rep.GetImportSalary(Utilities.ObjToInt(cboPeriod.SelectedValue), Utilities.ObjToInt(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve, Utilities.ObjToString(rtxtEmployee.Text), Sorts)
            'End If
            rgData.VirtualItemCount = TotalRow
            'For Each node As RadTreeNode In ctrlListSalary.CheckedNodes
            '    If Not vData.Columns.Contains(node.Value) Then
            '        vData.Columns.Add(node.Value)
            '    End If
            'Next
            rgData.DataSource = New DataTable
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region


End Class