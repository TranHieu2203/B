Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_Commitee
    Inherits Common.CommonView
    Protected WithEvents ctrlFindEmployee As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmpSign As ctrlFindEmployeePopup
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
    Public Overrides Property MustAuthorize As Boolean = True

#Region "Property"
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property isProcessPopup As Boolean
        Get
            Return ViewState(Me.ID & "_isProcessPopup")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isProcessPopup") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            ctrlOrg.is_UYBAN = True
            rgData.AllowCustomPaging = True
            rgData.SetFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Export,
                                       ToolbarItem.Delete)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            'Dim dic As New Dictionary(Of String, Control)
            'dic.Add("ID", hidID)
            'dic.Add("EMPLOYEE_ID", hidEMPLOYEE_ID)
            'dic.Add("EMPLOYEE_CODE", txtEMPLOYEE_CODE)
            'dic.Add("EMPLOYEE_NAME", txtEMPLOYEE_NAME)
            'dic.Add("EMPLOYEE_TITLE", txtEMPLOYEE_TITLE)
            'dic.Add("EMPLOYEE_LEVEL", txtEMPLOYEE_LEVEL)
            'dic.Add("EMPLOYEE_ORG", txtEMPLOYEE_ORG)
            'dic.Add("EMPLOYEE_ORG_DESC", hidEMPLOYEE_ORG_DESC)
            'dic.Add("ORG_ID", hidORG_ID)
            'dic.Add("ORG_NAME", txtORG_NAME)
            ''dic.Add("TITLE_ID", cboTitle)
            'dic.Add("FROM_DATE", rdFROM_DATE)
            'dic.Add("TO_DATE", rdTO_DATE)
            'dic.Add("DECISION_NO", txtDECISION_NO)
            'dic.Add("REMARK", txtREMARK)
            'dic.Add("SIGNER_ID", hidSIGNER_ID)
            'dic.Add("SIGNER_NAME", txtSIGNER_NAME)
            'Utilities.OnClientRowSelectedChanged(rgData, dic)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case Message
                Case "UpdateView"
                    rgData.Rebind()
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    CurrentState = CommonMessage.STATE_NORMAL
            End Select
            If CurrentState Is Nothing Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If
            txtEMPLOYEE_ORG.ToolTip = Utilities.DrawTreeByString(hidEMPLOYEE_ORG_DESC.Value)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If isProcessPopup Then
                If phFindEmployee.Controls.Contains(ctrlFindEmployee) Then
                    phFindEmployee.Controls.Remove(ctrlFindEmployee)
                End If
                If phFindSigner.Controls.Contains(ctrlFindEmpSign) Then
                    phFindSigner.Controls.Remove(ctrlFindEmpSign)
                End If
                Select Case isLoadPopup
                    Case 1
                        ctrlFindEmployee = Me.Register("ctrlFindEmployee", "Common", "ctrlFindEmployeePopup")
                        ctrlFindEmployee.MustHaveContract = True
                        ctrlFindEmployee.LoadAllOrganization = False
                        ctrlFindEmployee.IsOnlyWorkingWithoutTer = True
                        phFindEmployee.Controls.Add(ctrlFindEmployee)
                    Case 2
                        ctrlFindEmpSign = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                        ctrlFindEmpSign.MustHaveContract = True
                        ctrlFindEmpSign.LoadAllOrganization = False
                        ctrlFindEmpSign.IsOnlyWorkingWithoutTer = True
                        ctrlFindEmpSign.FunctionName = "ctrlHU_Commitee"
                        ctrlFindEmpSign.EmployeeOrg = If(hidOrg.Value <> "", CDec(hidOrg.Value), 0)
                        ctrlFindEmpSign.EffectDate = If(rdFROM_DATE.SelectedDate IsNot Nothing, CDbl(rdFROM_DATE.SelectedDate.Value.ToOADate), CDbl(New Date().ToOADate))
                        phFindSigner.Controls.Add(ctrlFindEmpSign)
                End Select
            Else
                Select Case CurrentState
                    Case CommonMessage.STATE_NORMAL
                        ctrlOrg.Enabled = True
                        RadPane2.Enabled = True
                        Utilities.EnabledGridNotPostback(rgData, True)
                        EnableControlAll(False, cboTitle, rdFROM_DATE, rdTO_DATE, txtDECISION_NO, txtREMARK, btnFindEmployee, btnFindSigner)
                    Case CommonMessage.STATE_NEW
                        ctrlOrg.Enabled = True
                        RadPane2.Enabled = False
                        EnableControlAll(True, cboTitle, rdFROM_DATE, rdTO_DATE, txtDECISION_NO, txtREMARK, btnFindEmployee, btnFindSigner)
                        ClearControlValue(hidID, hidEMPLOYEE_ID, hidORG_ID, hidSIGNER_ID, txtEMPLOYEE_CODE, txtORG_NAME, txtEMPLOYEE_NAME, txtEMPLOYEE_TITLE, cboTitle, txtEMPLOYEE_LEVEL, txtEMPLOYEE_ORG, rdFROM_DATE, rdTO_DATE, txtDECISION_NO, txtREMARK, txtSIGNER_NAME)
                        If IsNumeric(ctrlOrg.CurrentValue) Then
                            hidORG_ID.Value = Decimal.Parse(ctrlOrg.CurrentValue)
                            txtORG_NAME.Text = ctrlOrg.CurrentText
                            If hidORG_ID.Value <> "" Then
                                Using rep As New ProfileRepository
                                    Dim dtDataTitle = rep.GetDataByProcedures(9, hidORG_ID.Value, "0", Common.Common.SystemLanguage.Name)
                                    cboTitle.DataSource = dtDataTitle
                                    FillRadCombobox(cboTitle, dtDataTitle, "NAME", "ID", False)
                                End Using
                            End If
                        Else
                            ClearControlValue(cboTitle)
                        End If
                    Case CommonMessage.STATE_EDIT
                        ctrlOrg.Enabled = True
                        RadPane2.Enabled = False
                        EnableControlAll(True, cboTitle, rdFROM_DATE, rdTO_DATE, txtDECISION_NO, txtREMARK, btnFindEmployee, btnFindSigner)
                        If hidORG_ID.Value <> "" Then
                            Using rep As New ProfileRepository
                                Dim dtDataTitle = rep.GetDataByProcedures(9, hidORG_ID.Value, "0", Common.Common.SystemLanguage.Name)
                                cboTitle.DataSource = dtDataTitle
                                FillRadCombobox(cboTitle, dtDataTitle, "NAME", "ID", False)
                                cboTitle.SelectedValue = hidTitleID.Value
                            End Using
                        End If
                End Select
            End If
            ChangeToolbarState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Event"
    Private Sub rdFROM_DATE_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdFROM_DATE.SelectedDateChanged
        Try
            ClearControlValue(hidSIGNER_ID, txtSIGNER_NAME)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileRepository
        Dim objCommit As New CommiteeDTO
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    rgData.MasterTableView.ClearSelectedItems()
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
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If IsDate(rdFROM_DATE.SelectedDate) AndAlso IsDate(rdTO_DATE.SelectedDate) AndAlso rdTO_DATE.SelectedDate <= rdFROM_DATE.SelectedDate Then
                            ShowMessage("Ngày thôi nhiệm phải lớn hơn Ngày quyết định", NotifyType.Warning)
                            Exit Sub
                        End If
                        If IsNumeric(hidEMPLOYEE_ID.Value) Then
                            objCommit.EMPLOYEE_ID = Decimal.Parse(hidEMPLOYEE_ID.Value)
                        End If
                        If IsNumeric(hidORG_ID.Value) Then
                            objCommit.ORG_ID = Decimal.Parse(hidORG_ID.Value)
                        End If
                        'objCommit.COMMITTE_POSITION = txtCOMMITTE_POSITION.Text
                        If cboTitle.SelectedValue <> "" Then
                            objCommit.TITLE_ID = cboTitle.SelectedValue
                        End If
                        If IsDate(rdFROM_DATE.SelectedDate) Then
                            objCommit.FROM_DATE = rdFROM_DATE.SelectedDate
                        Else
                            objCommit.FROM_DATE = Nothing
                        End If
                        If IsDate(rdTO_DATE.SelectedDate) Then
                            objCommit.TO_DATE = rdTO_DATE.SelectedDate
                        Else
                            objCommit.TO_DATE = Nothing
                        End If
                        objCommit.DECISION_NO = txtDECISION_NO.Text
                        objCommit.REMARK = txtREMARK.Text
                        If IsNumeric(hidSIGNER_ID.Value) Then
                            objCommit.SIGNER_ID = Decimal.Parse(hidSIGNER_ID.Value)
                        Else
                            objCommit.SIGNER_ID = Nothing
                        End If
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertCommitee(objCommit) Then
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                    Exit Sub
                                End If
                            Case CommonMessage.STATE_EDIT
                                objCommit.ID = Decimal.Parse(hidID.Value)
                                If rep.ModifyCommitee(objCommit) Then
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                    Exit Sub
                                End If
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    rgData.MasterTableView.ClearSelectedItems()
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "Commitee")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub rgDanhMuc_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgData.SelectedIndexChanged
        Dim item = CType(rgData.SelectedItems(0), GridDataItem)
        If item.GetDataKeyValue("TITLE_ID") IsNot Nothing Then
            hidTitleID.Value = item.GetDataKeyValue("TITLE_ID").ToString
        End If
        If item.GetDataKeyValue("EMPLOYEE_ID") IsNot Nothing Then
            hidEMPLOYEE_ID.Value = item.GetDataKeyValue("EMPLOYEE_ID").ToString
        End If
        If item.GetDataKeyValue("ID") IsNot Nothing Then
            hidID.Value = item.GetDataKeyValue("ID").ToString
        End If
        If item.GetDataKeyValue("EMPLOYEE_CODE") IsNot Nothing Then
            txtEMPLOYEE_CODE.Text = item.GetDataKeyValue("EMPLOYEE_CODE").ToString
        End If
        If item.GetDataKeyValue("EMPLOYEE_NAME") IsNot Nothing Then
            txtEMPLOYEE_NAME.Text = item.GetDataKeyValue("EMPLOYEE_NAME").ToString
        End If
        If item.GetDataKeyValue("EMPLOYEE_TITLE") IsNot Nothing Then
            txtEMPLOYEE_TITLE.Text = item.GetDataKeyValue("EMPLOYEE_TITLE").ToString
        End If
        If item.GetDataKeyValue("EMPLOYEE_LEVEL") IsNot Nothing Then
            txtEMPLOYEE_LEVEL.Text = item.GetDataKeyValue("EMPLOYEE_LEVEL").ToString
        End If
        If item.GetDataKeyValue("EMPLOYEE_ORG") IsNot Nothing Then
            txtEMPLOYEE_ORG.Text = item.GetDataKeyValue("EMPLOYEE_ORG").ToString
        End If
        If item.GetDataKeyValue("EMPLOYEE_ORG_DESC") IsNot Nothing Then
            hidEMPLOYEE_ORG_DESC.Value = item.GetDataKeyValue("EMPLOYEE_ORG_DESC").ToString
        End If
        If item.GetDataKeyValue("ORG_ID") IsNot Nothing Then
            hidORG_ID.Value = item.GetDataKeyValue("ORG_ID").ToString
        End If
        If item.GetDataKeyValue("ORG_ID_EMP") IsNot Nothing Then
            hidOrg.Value = item.GetDataKeyValue("ORG_ID_EMP").ToString
        End If
        If item.GetDataKeyValue("ORG_NAME") IsNot Nothing Then
            txtORG_NAME.Text = item.GetDataKeyValue("ORG_NAME").ToString
        End If
        If IsDate(item.GetDataKeyValue("FROM_DATE")) Then
            rdFROM_DATE.SelectedDate = CDate(item.GetDataKeyValue("FROM_DATE"))
        End If
        If IsDate(item.GetDataKeyValue("TO_DATE")) Then
            rdTO_DATE.SelectedDate = CDate(item.GetDataKeyValue("TO_DATE"))
        End If
        If item.GetDataKeyValue("DECISION_NO") IsNot Nothing Then
            txtDECISION_NO.Text = item.GetDataKeyValue("DECISION_NO").ToString
        End If
        If item.GetDataKeyValue("REMARK") IsNot Nothing Then
            txtREMARK.Text = item.GetDataKeyValue("REMARK").ToString
        End If
        If item.GetDataKeyValue("SIGNER_NAME") IsNot Nothing Then
            txtSIGNER_NAME.Text = item.GetDataKeyValue("SIGNER_NAME").ToString
        End If
        If item.GetDataKeyValue("SIGNER_ID") IsNot Nothing Then
            hidSIGNER_ID.Value = item.GetDataKeyValue("SIGNER_ID").ToString
        End If
        If hidORG_ID.Value <> "" Then
            Using rep As New ProfileRepository
                Dim dtDataTitle = rep.GetDataByProcedures(9, hidORG_ID.Value, "0", Common.Common.SystemLanguage.Name)
                cboTitle.DataSource = dtDataTitle
                FillRadCombobox(cboTitle, dtDataTitle, "NAME", "ID", False)
                cboTitle.SelectedValue = hidTitleID.Value
            End Using
        End If
    End Sub
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Dim _filter As New CommiteeDTO
        Dim _param As New ParamDTO
        Dim MaximumRows As Integer
        Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            _filter.IS_INACTIVE = If(chkIS_INACTIVE.Checked, True, False)
            _param.ORG_ID = If(IsNumeric(ctrlOrg.CurrentValue), Decimal.Parse(ctrlOrg.CurrentValue), 1)
            _param.IS_DISSOLVE = ctrlOrg.IsDissolve

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetCommitees(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetCommitees(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    rgData.DataSource = rep.GetCommitees(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
                Else
                    rgData.DataSource = rep.GetCommitees(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
                End If
                rgData.VirtualItemCount = MaximumRows
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileRepository
        Dim lstID As New List(Of Decimal)
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                For Each item As GridDataItem In rgData.SelectedItems
                    lstID.Add(item.GetDataKeyValue("ID"))
                Next
                If rep.DeleteCommitee(lstID) Then
                    Refresh("UpdateView")
                    UpdateControlState()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    Exit Sub
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub chkIS_INACTIVE_CheckedChanged(sender As Object, e As EventArgs) Handles chkIS_INACTIVE.CheckedChanged
        rgData.CurrentPageIndex = 0
        rgData.MasterTableView.SortExpressions.Clear()
        rgData.Rebind()
        ClearControlValue(hidID, hidEMPLOYEE_ID, hidORG_ID, hidSIGNER_ID, txtEMPLOYEE_CODE, txtORG_NAME, txtEMPLOYEE_NAME, txtEMPLOYEE_TITLE, cboTitle, txtEMPLOYEE_LEVEL, txtEMPLOYEE_ORG, rdFROM_DATE, rdTO_DATE, txtDECISION_NO, txtREMARK, txtSIGNER_NAME)
    End Sub

    Protected Sub btnFindCommon_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee.Click, btnFindSigner.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            isProcessPopup = True
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
                    UpdateControlState()
                    ctrlFindEmployee.Show()
                Case btnFindSigner.ID
                    isLoadPopup = 2
                    UpdateControlState()
                    ctrlFindEmpSign.Show()
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployee.CancelClicked, ctrlFindEmpSign.CancelClicked
        isProcessPopup = False
    End Sub

    Private Sub ctrlFindEmployee_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployee.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Try
            ClearControlValue(hidSIGNER_ID, txtSIGNER_NAME)
            lstCommonEmployee = CType(ctrlFindEmployee.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count > 0 Then
                Dim item = lstCommonEmployee(0)
                hidEMPLOYEE_ID.Value = item.EMPLOYEE_ID
                txtEMPLOYEE_CODE.Text = item.EMPLOYEE_CODE
                txtEMPLOYEE_NAME.Text = item.FULLNAME_VN
                txtEMPLOYEE_TITLE.Text = item.TITLE_NAME
                txtEMPLOYEE_LEVEL.Text = item.STAFF_RANK_NAME
                txtEMPLOYEE_ORG.Text = item.ORG_NAME
                hidEMPLOYEE_ORG_DESC.Value = item.ORG_DESC
                hidOrg.Value = item.ORG_ID
            End If
            txtEMPLOYEE_ORG.ToolTip = Utilities.DrawTreeByString(hidEMPLOYEE_ORG_DESC.Value)
            isProcessPopup = False
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlFindSigner_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmpSign.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Try
            lstCommonEmployee = CType(ctrlFindEmpSign.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count > 0 Then
                Dim item = lstCommonEmployee(0)
                hidSIGNER_ID.Value = item.EMPLOYEE_ID
                txtSIGNER_NAME.Text = item.FULLNAME_VN
            End If
            isProcessPopup = False
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    rgData.CurrentPageIndex = 0
                    rgData.MasterTableView.SortExpressions.Clear()
                    rgData.Rebind()
                    ClearControlValue(hidID, hidEMPLOYEE_ID, hidORG_ID, hidSIGNER_ID, txtEMPLOYEE_CODE, txtORG_NAME, txtEMPLOYEE_NAME, txtEMPLOYEE_TITLE, cboTitle, txtEMPLOYEE_LEVEL, txtEMPLOYEE_ORG, rdFROM_DATE, rdTO_DATE, txtDECISION_NO, txtREMARK, txtSIGNER_NAME)
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    If IsNumeric(ctrlOrg.CurrentValue) Then
                        hidORG_ID.Value = Decimal.Parse(ctrlOrg.CurrentValue)
                        txtORG_NAME.Text = ctrlOrg.CurrentText
                    End If
            End Select
            If IsNumeric(ctrlOrg.CurrentValue) Then
                hidORG_ID.Value = Decimal.Parse(ctrlOrg.CurrentValue)
                Using rep As New ProfileRepository
                    Dim dtDataTitle = rep.GetDataByProcedures(9, ctrlOrg.CurrentValue, "0", Common.Common.SystemLanguage.Name)
                    cboTitle.DataSource = dtDataTitle
                    FillRadCombobox(cboTitle, dtDataTitle, "NAME", "ID", False)
                End Using
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
                datarow("EMPLOYEE_ORG").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("EMPLOYEE_ORG_DESC"))
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub txtEMPLOYEE_CODE_TextChanged(sender As Object, e As EventArgs) Handles txtEMPLOYEE_CODE.TextChanged
        txtEMPLOYEE_ORG.ToolTip = Utilities.DrawTreeByString(hidEMPLOYEE_ORG_DESC.Value)
    End Sub
#End Region
End Class