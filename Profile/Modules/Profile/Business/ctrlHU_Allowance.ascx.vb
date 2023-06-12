Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlHU_Allowance
    Inherits Common.CommonView

    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    'Protected WithEvents ctrlSignEmployeePopup As ctrlFindSignerPopup
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup
    Dim log As New Common.CommonBusiness.UserLog

#Region "Property"

    Public Property WorkingAllowance As List(Of HUAllowanceDTO)
        Get
            Return ViewState(Me.ID & "_WorkingAllowance")
        End Get
        Set(ByVal value As List(Of HUAllowanceDTO))
            ViewState(Me.ID & "_WorkingAllowance") = value
        End Set
    End Property

    Public Property lstAllowance As List(Of AllowanceListDTO)
        Get
            Return ViewState(Me.ID & "_AllowanceListDTO")
        End Get
        Set(ByVal value As List(Of AllowanceListDTO))
            ViewState(Me.ID & "_AllowanceListDTO") = value
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

    '0 - normal
    '1 - Reimbursement
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set

    End Property

    ''' <summary>
    ''' dtDataImportEmployee
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtDataImportWorking As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportWorking")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportWorking") = value
        End Set
    End Property

    ''' <summary>
    ''' dtData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA_ALLOWANCE")
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("ALLOWANCE_LIST_CODE", GetType(String))
                dt.Columns.Add("ALLOWANCE_TYPE_ID", GetType(String))
                'dt.Columns.Add("FACTOR", GetType(String))
                dt.Columns.Add("AMOUNT", GetType(String))
                dt.Columns.Add("EFFECT_DATE", GetType(String))
                dt.Columns.Add("EXPIRE_DATE", GetType(String)) 'NGAY KET THUC HIEU LUC
                dt.Columns.Add("EMPLOYEE_SIGNER_CODE", GetType(String)) 'MA NGUOI KY
                dt.Columns.Add("REMARK", GetType(String))
                dt.Columns.Add("DATE_SIGNER", GetType(String))
                'dt.Columns.Add("DETAIL_ADD_NAME", GetType(String))
                'dt.Columns.Add("DETAIL_ADD", GetType(String))
                dt.Columns.Add("IS_DEDUCT", GetType(String))
                'dt.Columns.Add("PERCENT_PIT", GetType(String))
                'dt.Columns.Add("AMOUNT_PIT", GetType(String))
                'dt.Columns.Add("IS_INSURRANCE", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrg.CheckBoxes = TreeNodeTypes.None

            Refresh()
            UpdateControlState()
            FromPorfile()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        log = LogHelper.GetUserLog
        rgMain.SetFilter()
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        InitControl()
        If Not IsPostBack Then
            GirdConfig(rgMain)
        End If
        ctrlUpload1.isMultiple = False
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Delete, ToolbarItem.Export, ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.ExportTemplate, ToolbarItem.Import)

            'CType(MainToolBar.Items(4), RadToolBarButton).Text = Translate("Xuất file mẫu")
            'CType(MainToolBar.Items(4), RadToolBarButton).CommandName = "EXPORT_TEMP"

            CType(MainToolBar.Items(4), RadToolBarButton).CausesValidation = True
            'CType(MainToolBar.Items(5), RadToolBarButton).CausesValidation = True
            'CType(MainToolBar.Items(7), RadToolBarButton).CausesValidation = False
            'CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = False
            CType(MainToolBar.Items(6), RadToolBarButton).Text = Translate("Xuất file mẫu")
            CType(MainToolBar.Items(7), RadToolBarButton).Text = Translate("Nhập file mẫu")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                If Request.Params("empID") IsNot Nothing AndAlso Request.Params("kind") IsNot Nothing Then
                    CurrentState = CommonMessage.STATE_NEW
                Else
                    CurrentState = CommonMessage.STATE_NORMAL
                End If
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()
                        'SelectedItemDataGridByKey(rgMain, IDSelect, , rgMain.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        SelectedItemDataGridByKey(rgMain, IDSelect, )
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                End Select
                UpdateControlState()

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileBusinessRepository
        Try
            If ctrlFindEmployeePopup IsNot Nothing AndAlso phEMPR.Controls.Contains(ctrlFindEmployeePopup) Then
                phEMPR.Controls.Remove(ctrlFindEmployeePopup)
            Else
                'ctrlSignEmployeePopup IsNot Nothing AndAlso phEMPRS.Controls.Contains(ctrlSignEmployeePopup) Then
                '                phEMPRS.Controls.Remove(ctrlSignEmployeePopup)
            End If
            Select Case isLoadPopup
                Case 1
                    phEMPR.Controls.Clear()
                    ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                    phEMPR.Controls.Add(ctrlOrgPopup)
                Case 2
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    phEMPR.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
                    ctrlFindEmployeePopup.MustHaveContract = False
                Case 3
                    'ctrlSignEmployeePopup = Me.Register("ctrlFindSignerPopup", "Common", "ctrlFindSignerPopup")
                    'ctrlSignEmployeePopup.MultiSelect = False
                    'ctrlSignEmployeePopup.LoadAllOrganization = True
                    'phEMPRS.Controls.Add(ctrlSignEmployeePopup)
            End Select

            Select Case CurrentState
                Case CommonMessage.STATE_NEW

                    EnableControlAll(True, txtRemark, cboALLOWANCE_LIST, rntxtAmount, rdEFFECT_DATE, rdEXPIRE_DATE, txtRemark, btnDownload, btnUploadFile) ', rntxtFactor, rnPercentPIT
                    EnableControlAll(True, txtEmployeeName, btnFindR)
                    btnFindR.Visible = True
                    'rnPercentPIT.AutoPostBack = True
                    'rntxtAmount.AutoPostBack = True
                    If Request.Params("empID") IsNot Nothing AndAlso Request.Params("kind") IsNot Nothing Then
                        EnableControlAll(False, txtEmployeeName)
                        btnFindR.Visible = False
                        LeftPane.Visible = False
                    Else
                        ' rntxtFactor.NumberFormat.DecimalDigits = 2
                        EnabledGridNotPostback(rgMain, False)
                        cboALLOWANCE_LIST.AutoPostBack = True
                    End If
                Case CommonMessage.STATE_NORMAL
                    'ClearControlValue(txtRemark, txtEmployeeName, cboALLOWANCE_LIST, rntxtAmount, rntxtFactor, rdEFFECT_DATE, rdEXPIRE_DATE, txtSignName, rdSignDate, txtSignLevel, txtSignTitle, txtRemark)
                    EnableControlAll(False, txtRemark, btnFindR, txtEmployeeName, cboALLOWANCE_LIST, rntxtAmount, rdEFFECT_DATE, rdEXPIRE_DATE, txtRemark, btnDownload, btnUploadFile) ', rntxtFactor, rnPercentPIT
                    'rntxtFactor.NumberFormat.DecimalDigits = 2
                    EnabledGridNotPostback(rgMain, True)
                    cboALLOWANCE_LIST.AutoPostBack = False
                    'rnPercentPIT.AutoPostBack = False
                   ' rntxtAmount.AutoPostBack = False
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, txtRemark, btnFindR, txtEmployeeName, cboALLOWANCE_LIST, rntxtAmount, rdEFFECT_DATE, rdEXPIRE_DATE, txtRemark, btnDownload, btnUploadFile) ', rntxtFactor, rnPercentPIT
                    ' rntxtFactor.NumberFormat.DecimalDigits = 2
                    EnabledGridNotPostback(rgMain, False)
                    cboALLOWANCE_LIST.AutoPostBack = True
                    ' rnPercentPIT.AutoPostBack = True
                   ' rntxtAmount.AutoPostBack = True
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of HUAllowanceDTO)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        Dim objWorkingAllowance As New HUAllowanceDTO
                        objWorkingAllowance.ID = Utilities.ObjToDecima(item.GetDataKeyValue("ID"))
                        lstDeletes.Add(objWorkingAllowance)
                    Next
                    If rep.DeleteWorkingAllowance(lstDeletes) Then
                        rgMain.Rebind()
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            rep.Dispose()
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim lstData As New ComboBoxDataDTO
            Using rep As New ProfileRepository
                lstData.GET_WORKING_ALLOWANCE_LIST = True
                rep.GetComboList(lstData)
                lstAllowance = lstData.LIST_WORKING_ALLOWANCE_LIST
                Dim lst As New List(Of AllowanceListDTO)
                For Each l In lstAllowance
                    lst.Add(l)
                Next
                FillRadCombobox(cboALLOWANCE_LIST, lst, "Name", "ID")
                FillRadCombobox(cboLPC, lstData.LIST_WORKING_ALLOWANCE_LIST, "Name", "ID")
            End Using

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("EMPLOYEE_ID", hidEmployeeID)
            dic.Add("EMPLOYEE_SIGNER_ID", hidEmployee_SID)
            dic.Add("EMPLOYEE_NAME", txtEmployeeName)
            dic.Add("ALLOWANCE_LIST_ID", cboALLOWANCE_LIST)
            dic.Add("AMOUNT", rntxtAmount)
            'dic.Add("FACTOR", rntxtFactor)
            'dic.Add("DETAIL_ADD", cboDetail_ADD)
            ' dic.Add("EMPLOYEE_SIGNER_NAME", txtSignName)
            ' dic.Add("EMPLOYEE_SIGNER_TITLE_NAME", txtSignTitle)
            ' dic.Add("EMPLOYEE_SIGNER_SR_NAME", txtSignLevel)
            dic.Add("REMARK", txtRemark)
            dic.Add("EFFECT_DATE", rdEFFECT_DATE)
            dic.Add("EXPIRE_DATE", rdEXPIRE_DATE)
            'dic.Add("PERCENT_PIT", rnPercentPIT)
            'dic.Add("AMOUNT_PIT", rnAmountPIT)
            dic.Add("IS_DEDUCT", chk_Is_Deduct)
            dic.Add("FILE_NAME", txtUpload)
            dic.Add("UPLOAD_FILE", txtUploadFile)
            '  dic.Add("DATE_SIGNER", rdSignDate)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objWA As New HUAllowanceDTO
        Dim rep As New ProfileBusinessRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    ' rntxtFactor.NumberFormat.DecimalDigits = 2
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtRemark, txtEmployeeName, txtTitleName, cboALLOWANCE_LIST, rntxtAmount, rdEFFECT_DATE, rdEXPIRE_DATE, txtRemark, txtUpload, txtUploadFile, chk_Is_Deduct, hidEmployeeID, hidEmployee_SID)
                    UpdateControlState()
                    chk_Is_Deduct.Checked = False
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgMain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstID As New List(Of Decimal)
                    Dim sp As New ProfileStoreProcedure
                    For Each item As GridDataItem In rgMain.SelectedItems
                        Dim objWorkingAllowance As New HUAllowanceDTO
                        'EMPLOYEE_ID
                        lstID.Add(Utilities.ObjToDecima(item.GetDataKeyValue("ALLOWANCE_LIST_ID")))
                        Try
                            If sp.CHECKEXIST_ALLOWANCE(item.GetDataKeyValue("EMPLOYEE_ID"), item.GetDataKeyValue("ALLOWANCE_LIST_ID"), item.GetDataKeyValue("EFFECT_DATE")) > 0 Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                                Exit Sub
                            End If
                        Catch ex As Exception
                            Continue For
                        End Try
                    Next

                    'Dim repProfile As New ProfileRepository
                    'If Not repProfile.CheckExistInDatabase(lstID, ProfileCommonTABLE_NAME.HU_ALLOWANCE_LIST) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                    '    Return
                    'End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case "EXPORT_TEMP"
                    isLoadPopup = 1 'Chọn Org
                    UpdateControlState()
                    ctrlOrgPopup.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "Allowance")
                            Exit Sub
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        'If rntxtAmount.Text <> "" And rntxtFactor.Text <> "" Then
                        '    ShowMessage(Translate("Bạn chỉ được nhập hệ số hoặc số tiền"), NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        'If rntxtAmount.Text = "" And rntxtFactor.Text = "" Then
                        '    ShowMessage(Translate("Bạn phải nhập hệ số hoặc số tiền. "), NotifyType.Warning)
                        '    Exit Sub
                        'End If

                        Dim CW As Decimal = -1
                        Dim TD As Date = Today
                        With objWA

                            If cboALLOWANCE_LIST.SelectedValue <> "" Then
                                .ALLOWANCE_LIST_ID = cboALLOWANCE_LIST.SelectedValue
                            End If
                            'If IsNumeric(rnPercentPIT.Value) Then
                            '    .PERCENT_PIT = rnPercentPIT.Value
                            'End If
                            'If IsNumeric(rnAmountPIT.Value) Then
                            '    .AMOUNT_PIT = rnAmountPIT.Value
                            'End If
                            If rntxtAmount.Text <> "" Then
                                .AMOUNT = rntxtAmount.Value
                            End If
                            'If rntxtFactor.Text <> "" Then
                            '    .FACTOR = rntxtFactor.Value
                            'End If
                            'If rdSignDate.SelectedDate IsNot Nothing Then
                            '    .DATE_SIGNER = rdSignDate.SelectedDate
                            'End If
                            If rdEFFECT_DATE.SelectedDate IsNot Nothing Then
                                .EFFECT_DATE = rdEFFECT_DATE.SelectedDate
                            End If
                            If rdEXPIRE_DATE.SelectedDate IsNot Nothing Then
                                .EXPIRE_DATE = rdEXPIRE_DATE.SelectedDate
                            End If
                            If hidEmployeeID.Value <> "" Then
                                .EMPLOYEE_ID = hidEmployeeID.Value
                            End If
                            'If hidEmployee_SID.Value <> "" Then
                            '    .EMPLOYEE_SIGNER_ID = hidEmployee_SID.Value
                            'End If
                            .REMARK = txtRemark.Text
                            .IS_DEDUCT = chk_Is_Deduct.Checked
                            '.SIGNER_NAME = txtSignName.Text
                            '.SIGNER_LEVEL_NAME = txtSignLevel.Text
                            '.SIGNER_TITLE_NAME = txtSignTitle.Text
                            .FILE_NAME = txtUploadFile.Text
                        End With

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.ValidateNewEdit(objWA) Then
                                    ShowMessage(Translate("Ngày hiệu lực mới phải lớn hơn ngày hiệu lực cũ."), Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.InsertWorkingAllowance(objWA, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate("Một ngày không được tạo một nhân viên có cùng ngày hiệu lực và cùng loại phụ cấp."), Utilities.NotifyType.Warning)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objWA.ID = rgMain.SelectedValue
                                If rep.ValidateNewEdit(objWA) Then
                                    ShowMessage(Translate("Ngày hiệu lực mới phải lớn hơn ngày hiệu lực cũ."), Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.ModifyWorkingAllowanceNew(objWA, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objWA.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate("Một ngày không được tạo một nhân viên có cùng ngày hiệu lực và cùng loại phụ cấp."), Utilities.NotifyType.Warning)
                                End If
                        End Select
                        FromPorfile()

                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    If Request.Params("empID") IsNot Nothing And Request.Params("kind") IsNot Nothing Then
                        ExcuteScript("Close", "window.close(this);")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ClearControlValue(txtRemark, txtEmployeeName, cboALLOWANCE_LIST, rntxtAmount, rdEFFECT_DATE, rdEXPIRE_DATE, txtRemark) ' , rntxtFactorrnAmountPIT, rnPercentPIT
                        Refresh("Cancel")
                        UpdateControlState()
                        chk_Is_Deduct.Checked = False
                    End If
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Template_Export()
            End Select
            rep.Dispose()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            Dim rep As New ProfileBusinessRepository
            Dim objWorkingAllowanceD As New List(Of HUAllowanceDTO)
            Dim lst As New List(Of Decimal)

            For Each item As GridDataItem In rgMain.SelectedItems
                Dim objWorkingAllowance As New HUAllowanceDTO
                objWorkingAllowance.ID = Utilities.ObjToDecima(item.GetDataKeyValue("ID"))
                objWorkingAllowanceD.Add(objWorkingAllowance)
            Next

            If rep.DeleteWorkingAllowance(objWorkingAllowanceD) Then
                ClearControlValue(txtRemark, txtEmployeeName, cboALLOWANCE_LIST, rntxtAmount, rdEFFECT_DATE, rdEXPIRE_DATE, txtRemark) ' , rntxtFactorrnAmountPIT, rnPercentPIT
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                Refresh("UpdateView")
            Else
                ShowMessage(Translate("Phụ cấp đã đến ngày hiệu lực."), Utilities.NotifyType.Warning)
            End If
            rep.Dispose()
            UpdateControlState()
        End If
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False)
        Dim rep As New ProfileBusinessRepository
        Dim obj As New HUAllowanceDTO
        Try
            SetValueObjectByRadGrid(rgMain, obj)
            Dim _param = New ParamDTO With {.ORG_ID = ctrlOrg.CurrentValue,
                                           .IS_DISSOLVE = ctrlOrg.IsDissolve,
                                           .IS_FULL = True}
            Dim MaximumRows As Integer
            If rdTo.SelectedDate IsNot Nothing Then
                obj.TO_DATE = rdTo.SelectedDate
            End If
            If rdFrom.SelectedDate IsNot Nothing Then
                obj.FROM_DATE = rdFrom.SelectedDate
            End If
            If txtSeachEmployee.Text <> "" Then
                obj.EMPLOYEE_CODE = txtSeachEmployee.Text
            End If
            If cboLPC.SelectedValue <> "" Then
                obj.ALLOWANCE_LIST_ID = cboLPC.SelectedValue
            End If

            obj.IS_NEW = chkNewEmp.Checked
            obj.IS_TER = chkChecknghiViec.Checked
            If Request.Params("empID") IsNot Nothing Then
                If Request.Params("kind") IsNot Nothing Then
                    obj.EMPLOYEE_ID = Request.Params("empID")
                End If
            End If
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetWorkingAllowanceFull1(obj, _param, Sorts).ToTable()
                Else
                    Return rep.GetWorkingAllowanceFull1(obj, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.WorkingAllowance = rep.GetWorkingAllowance1(obj, _param, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    Me.WorkingAllowance = rep.GetWorkingAllowance1(obj, _param, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If
                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = Me.WorkingAllowance
            End If
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgMain.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Xử lý sự kiện khi click btnFindR
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFindR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindR.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Xử lý sự kiện click khi btnFindRS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub btnFindRS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindRS.Click
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        isLoadPopup = 3
    '        UpdateControlState()
    '        ctrlSignEmployeePopup.Show()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Dim _filter As New EmployeeDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                txtEmployeeName.Text = lstCommonEmployee(0).FULLNAME_VN
                hidEmployeeID.Value = lstCommonEmployee(0).ID
                txtTitleName.Text = lstCommonEmployee(0).TITLE_NAME
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Private Sub ctrlSignEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlSignEmployeePopup.EmployeeSelected
    '    Dim lstCommonEmployee As New List(Of CommonBusiness.SignerPopupFindDTO)
    '    Dim rep As New ProfileBusinessRepository
    '    Dim _filter As New EmployeeDTO
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        lstCommonEmployee = CType(ctrlSignEmployeePopup.SelectedEmployee, List(Of CommonBusiness.SignerPopupFindDTO))
    '        If lstCommonEmployee.Count <> 0 Then
    '            txtSignName.Text = lstCommonEmployee(0).NAME
    '            hidEmployee_SID.Value = lstCommonEmployee(0).ID.ToString()
    '            Dim objE = rep.GetEmployeeVehicleByEmployeeId(lstCommonEmployee(0).ID)
    '            txtSignTitle.Text = lstCommonEmployee(0).TITLE_NAME
    '            txtSignLevel.Text = lstCommonEmployee(0).LEVEL_NAME
    '        End If
    '        isLoadPopup = 0
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgMain.CurrentPageIndex = 0
            rgMain.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboALLOWANCE_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboALLOWANCE_LIST.SelectedIndexChanged
        Try
            If cboALLOWANCE_LIST.SelectedValue <> "" Then
                Dim idA = (From p In lstAllowance Where p.ID = cboALLOWANCE_LIST.SelectedValue).FirstOrDefault
                If idA IsNot Nothing Then
                    If idA.IS_DEDUCT = -1 Then
                        chk_Is_Deduct.Checked = True
                    Else
                        chk_Is_Deduct.Checked = False
                        'rntxtFactor.Enabled = False
                        'rntxtAmount.Enabled = True
                        'rntxtFactor.Text = ""
                    End If
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly upload file khi click button [OK] o popup ctrlUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet

        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next

            dtData = dtData.Clone()
            'TableMaping(dsDataPrepare.Tables(0))
            TableMapping(dsDataPrepare.Tables(0))

            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("EMPLOYEE_CODE<>'""'").CopyToDataTable.Rows
                'If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached OrElse rows("ALLOWANCE_LIST_CODE").ToString.Split("-")(0) = String.Empty Then Continue For
                If IsDBNull(rows("EMPLOYEE_CODE")) OrElse rows("EMPLOYEE_CODE") = "" Then Continue For
                Dim newRow As DataRow
                newRow = dtData.NewRow
                newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
                'newRow("ALLOWANCE_TYPE") = rows("ALLOWANCE_TYPE")
                newRow("ALLOWANCE_TYPE_ID") = If(IsNumeric(rows("ALLOWANCE_TYPE_ID")), Decimal.Parse(rows("ALLOWANCE_TYPE_ID")), 0)
                'newRow("ALLOWANCE_LIST_CODE") = rows("ALLOWANCE_LIST_CODE").ToString.Split("-")(0)
                newRow("IS_DEDUCT") = If(IsNumeric(rows("IS_DEDUCT")), Decimal.Parse(rows("IS_DEDUCT")), 0)
                Dim check = IsNumeric(rows("AMOUNT"))
                If check = False Then
                    newRow("AMOUNT") = 0
                Else
                    newRow("AMOUNT") = rows("AMOUNT").ToString.Replace(",", ".")
                End If

                'newRow("PERCENT_PIT") = rows("PERCENT_PIT").ToString.Replace(",", ".")
                'newRow("AMOUNT_PIT") = rows("AMOUNT_PIT").ToString.Replace(",", ".")
                'newRow("AMOUNT") = IIf(IsDBNull(rows("AMOUNT")), 0, rows("AMOUNT")).ToString.Replace(",", "")
                newRow("EFFECT_DATE") = rows("EFFECT_DATE")
                newRow("EXPIRE_DATE") = IIf(IsDBNull(rows("EXPIRE_DATE")), "NULL", rows("EXPIRE_DATE"))
                newRow("REMARK") = rows("REMARK")
                'newRow("DETAIL_ADD") = If(IsNumeric(rows("DETAIL_ADD")), Decimal.Parse(rows("DETAIL_ADD")), DBNull.Value)
                'Dim checkFACTOR = IsNumeric(rows("FACTOR"))
                'If checkFACTOR = False Then
                '    newRow("FACTOR") = 0
                'Else
                '    newRow("FACTOR") = rows("FACTOR").ToString.Replace(",", ".")
                'End If
                dtData.Rows.Add(newRow)
            Next


            'dtData.AcceptChanges()
            dtData.TableName = "DATA"
            If loadToGrid() Then

                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim sp As New ProfileStoreProcedure()
                If sp.Import_Allowance(LogHelper.GetUserLog().Username.ToUpper, DocXml) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
                'End edit;
                rgMain.Rebind()

                ' insert to database
                'Edit by: ChienNV
                'Edit date:13/03/2018
                'Dim sp As New ProfileStoreProcedure()
                'For Each rows As DataRow In dtData.Rows
                '    Try
                '        If sp.CheckInsertAllowance(rows("EMPLOYEE_CODE"), rows("EFFECT_DATE").ToString(), rows("ALLOWANCE_LIST_CODE")) Then
                '            Dim msg As String = String.Format("Nhân viên: {0} với mã phụ cấp: {1} ngày hiệu lực: {2} đã được khai báo", rows("EMPLOYEE_CODE").ToString(), rows("ALLOWANCE_LIST_CODE").ToString(), rows("EFFECT_DATE").ToString())
                '            ShowMessage(Translate(msg), NotifyType.Warning)
                '            Exit Sub
                '        End If
                '    Catch ex As Exception
                '        Continue For
                '    End Try
                'Next
                'Dim sw As New StringWriter()
                'Dim DocXml As String = String.Empty
                'dtData.WriteXml(sw, False)
                'DocXml = sw.ToString

                'If (New ProfileStoreProcedure).Imp_Allowance_List(log.Username.ToUpper, DocXml) Then
                '    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                'Else
                '    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                'End If
                'End edit;
                '-----------------------------------------------------------------------------------------
                'rgMain.Rebind()
            End If

        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Private Sub ctrlOrgPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('HU_ALLOWANCE&ORG_ID=" & e.CurrentValue & "&IS_DISSOLVE=" & IIf(ctrlOrgPopup.IsDissolve, "1", "0") & "')", True)
            isLoadPopup = 0
        Catch ex As Exception
        End Try
    End Sub
    'Private Sub cboALLOWANCE_LIST_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboALLOWANCE_LIST.SelectedIndexChanged
    '    Try
    '        If cboALLOWANCE_LIST.SelectedValue <> "" Then
    '            Dim idA = (From p In lstAllowance Where p.ID = cboALLOWANCE_LIST.SelectedValue).FirstOrDefault
    '            If idA IsNot Nothing Then
    '                If idA.ALLOW_LEVEL = 6570 Then

    '                Else

    '                End If
    '            End If
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    Private Sub txtEmployeeName_TextChanged(sender As Object, e As EventArgs) Handles txtEmployeeName.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If txtEmployeeName.Text <> "" Then
                    Reset_Find_Emp()
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = txtEmployeeName.Text
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        txtEmployeeName.Text = ""
                    ElseIf Count = 1 Then
                        'Dim empID = EmployeeList(0)
                        txtEmployeeName.Text = EmployeeList(0).FULLNAME_VN
                        hidEmployeeID.Value = EmployeeList(0).ID.ToString()
                        txtTitleName.Text = EmployeeList(0).TITLE_NAME.ToString()
                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If phEMPR.Controls.Contains(ctrlFindEmployeePopup) Then
                            phEMPR.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not phEMPR.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmployeeName.Text
                            ctrlFindEmployeePopup.MultiSelect = False
                            ctrlFindEmployeePopup.MustHaveContract = False
                            phEMPR.Controls.Add(ctrlFindEmployeePopup)
                            ctrlFindEmployeePopup.Show()
                            isLoadPopup = 2
                        End If
                    End If
                Else
                    Reset_Find_Emp()
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub Reset_Find_Emp()
        'txtEmployeeName.Text = EmployeeList(0).FULLNAME_VN
        hidEmployeeID.Value = ""
        txtTitleName.Text = ""
    End Sub

    'Private Sub rnPercentPIT_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rnPercentPIT.TextChanged
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow

    '    Try
    '        GetAmountPIT()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    'Private Sub rntxtAmount_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rntxtAmount.TextChanged
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Try
    '        GetAmountPIT()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    Private Sub btnUploadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ctrlUpload2.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload2.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlUpload2_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload2.OkClicked
        Dim fileName As String
        Dim rep As New Profile.ProfileBusinessRepository
        Try
            If ctrlUpload2.UploadedFiles.Count > 1 Then
                ShowMessage("Chỉ được chọn 1 file để upload", NotifyType.Error)
                Exit Sub
            Else
                If ctrlUpload2.UploadedFiles.Count > 0 Then
                    Dim file As UploadedFile = ctrlUpload2.UploadedFiles(0)
                    Dim fileContent As Byte() = New Byte(file.ContentLength) {}

                    Dim buffer As Byte() = New Byte(file.ContentLength - 1) {}
                    Using str As Stream = file.InputStream
                        str.Read(buffer, 0, buffer.Length)
                    End Using
                    Dim guidID = Guid.NewGuid.ToString()
                    Dim obj As New FileUploadDTO
                    obj.FILE_NAME = file.FileName
                    obj.CODE_PATH = "HUALLOWANCE"
                    obj.NAME = guidID
                    If Not rep.AddFileUpload(obj, buffer) Then
                        txtUpload.Text = file.FileName
                        txtUploadFile.Text = obj.NAME
                        btnDownload.Visible = True
                    End If
                Else
                    ShowMessage(Translate("Chưa upload được file"), NotifyType.Error)
                End If
            End If
        Catch ex As Exception
            ShowMessage(Translate("Upload file bị lỗi"), NotifyType.Error)
        End Try
    End Sub
    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If txtUpload.Text <> "" Then
                Dim fileObj As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    fileObj = rep.GetFileForView(txtUploadFile.Text)
                End Using
                Dim link = fileObj.LINK
                Dim name = fileObj.FILE_NAME
                Dim path = link & name
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
                Response.WriteFile(file.FullName)
                Response.End()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    'Private Sub rgMain_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgMain.SelectedIndexChanged
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        If txtUpload.Text <> "" Then
    '            btnDownload.Visible = True
    '        Else
    '            btnDownload.Visible = False
    '        End If
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
#End Region

#Region "Custom"

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Check data khi upload
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function loadToGrid() As Boolean
        Dim dtError As New DataTable("ERROR")
        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If

            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            dtDataImportWorking = dtData.Clone
            dtError = dtData.Clone
            Dim iRow = 1
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)

                If row("ALLOWANCE_TYPE_ID") Is DBNull.Value OrElse row("ALLOWANCE_TYPE_ID") = "" Then
                    sError = "Chưa nhập loại phụ cấp"
                    ImportValidate.IsValidTime("ALLOWANCE_TYPE_ID", row, rowError, isError, sError)
                End If

                If row("EFFECT_DATE") Is DBNull.Value OrElse row("EFFECT_DATE") = "" Then
                    sError = "Chưa nhập ngày hiệu lực"
                    ImportValidate.IsValidTime("EFFECT_DATE", row, rowError, isError, sError)
                End If

                'If (row("FACTOR") Is DBNull.Value OrElse row("FACTOR") = "") And (row("AMOUNT") Is DBNull.Value OrElse row("AMOUNT") = "") Then
                '    sError = "Chưa nhập  phụ cấp"
                '    ImportValidate.IsValidTime("FACTOR", row, rowError, isError, sError)
                'End If

                'If Not IsDBNull(row("PERCENT_PIT")) AndAlso Not String.IsNullOrEmpty(row("PERCENT_PIT")) Then
                '    If Not IsNumeric(row("PERCENT_PIT")) Then
                '        rowError("PERCENT_PIT") = "Chỉ được nhập số"
                '        isError = True
                '    Else
                '        row("PERCENT_PIT") = row("PERCENT_PIT").ToString.Replace(",", ".")
                '    End If
                'End If

                'If Not IsDBNull(row("AMOUNT_PIT")) AndAlso Not String.IsNullOrEmpty(row("AMOUNT_PIT")) Then
                '    If Not IsNumeric(row("AMOUNT_PIT")) Then
                '        rowError("AMOUNT_PIT") = "Chỉ được nhập số"
                '        isError = True
                '    Else
                '        row("AMOUNT_PIT") = row("AMOUNT_PIT").ToString.Replace(",", ".")
                '    End If
                'End If

                If isError Then
                    rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString

                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    'rowError("ALLOWANCE_LIST_CODE") = row("ALLOWANCE_LIST_CODE").ToString
                    'rowError("EFFECT_DATE") = row("EFFECT_DATE").ToString
                    'rowError("AMOUNT") = row("AMOUNT").ToString
                    'rowError("FACTOR") = row("FACTOR").ToString
                    dtError.Rows.Add(rowError)
                Else
                    dtDataImportWorking.ImportRow(row)
                End If
                iRow = iRow + 1
            Next

            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                ' gộp các lỗi vào 1 cột ghi chú 
                Dim dtErrorGroup As New DataTable
                Dim RowErrorGroup As DataRow
                dtErrorGroup.Columns.Add("STT")
                dtErrorGroup.Columns.Add("NOTE")

                For j As Integer = 0 To dtError.Rows.Count - 1
                    Dim strNote As String = String.Empty
                    RowErrorGroup = dtErrorGroup.NewRow

                    For k As Integer = 1 To dtError.Columns.Count - 1
                        If Not dtError.Rows(j)(k) Is DBNull.Value Then
                            strNote &= dtError.Rows(j)(k) & "\"
                        End If
                    Next

                    RowErrorGroup("STT") = dtError.Rows(j)("EMPLOYEE_CODE")
                    RowErrorGroup("NOTE") = strNote
                    dtErrorGroup.Rows.Add(RowErrorGroup)
                Next

                dtErrorGroup.TableName = "DATA"
                Dim dsErr = New DataSet
                dsErr.Tables.Add(dtError)
                Session("ALLOWANCE_ERR") = dsErr
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Import_phucap_err')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If

            If isError Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception

            Throw ex
        End Try
    End Function
    'Private Sub TableMaping(ByVal dtdata As DataTable)
    '    If dtdata Is Nothing OrElse dtdata.Columns.Count <> 11 Then Exit Sub
    '    dtdata.Columns(0).ColumnName = "EMPLOYEE_CODE"
    '    dtdata.Columns(1).ColumnName = "FULLNAME"
    '    dtdata.Columns(2).ColumnName = "ALLOWANCE_LIST_CODE"
    '    dtdata.Columns(3).ColumnName = "FACTOR"
    '    dtdata.Columns(4).ColumnName = "AMOUNT"
    '    dtdata.Columns(5).ColumnName = "EFFECT_DATE"
    '    dtdata.Columns(6).ColumnName = "EXPIRE_DATE"
    '    dtdata.Columns(7).ColumnName = "EMPLOYEE_SIGNER_CODE"
    '    dtdata.Columns(8).ColumnName = "NAME_SIGNER"
    '    dtdata.Columns(9).ColumnName = "DATE_SIGNER"
    '    dtdata.Columns(10).ColumnName = "REMARK"
    '    dtdata.Rows(0).Delete()
    '    'dtdata.Rows(1).Delete()
    'End Sub

    Private Sub TableMapping(ByVal dtdata As DataTable)
        Dim row As DataRow = dtdata.Rows(0)
        Dim index As Integer = 0
        For Each cols As DataColumn In dtdata.Columns
            Try
                cols.ColumnName = row(index)
                index += 1
                If index > row.ItemArray.Length - 1 Then Exit For
            Catch ex As Exception
                Exit For
            End Try
        Next
        dtdata.Rows(0).Delete()
        dtdata.Rows(0).Delete()
        dtdata.AcceptChanges()
    End Sub
    Private Sub Template_Export()
        Try
            Using rep As New ProfileBusinessRepository
                Dim dt As New DataSet
                dt = rep.GetDataExport()
                dt.Tables(0).TableName = "Table"
                dt.Tables(1).TableName = "Table1"
                rep.Dispose()
                ExportTemplate("Profile\Import\Import_phucap.xls",
                                dt, Nothing, "Import_phucap" & Format(Date.Now, "yyyyMMdd"))
            End Using
        Catch ex As Exception

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

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            dsData.Tables(0).TableName = "Table"
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Public Function FromPorfile()
        Try
            If Request.Params("empID") IsNot Nothing Then
                If Request.Params("kind") IsNot Nothing Then

                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                    Using rep As New ProfileBusinessClient
                        Dim item = rep.GetEmployeeByEmployeeID(Request.Params("empID"))
                        txtEmployeeName.Text = item.FULLNAME_VN
                        hidEmployeeID.Value = item.ID.ToString()
                        txtTitleName.Text = item.TITLE_NAME_VN.ToString()

                        MainToolBar.Items(0).Visible = False
                        MainToolBar.Items(1).Visible = False
                        MainToolBar.Items(2).Visible = False
                        MainToolBar.Items(3).Visible = False

                        MainToolBar.Items(4).Visible = True
                        MainToolBar.Items(5).Visible = True
                        MainToolBar.Items(4).Enabled = True
                        MainToolBar.Items(5).Enabled = True

                        MainToolBar.Items(6).Visible = False
                        MainToolBar.Items(7).Visible = False

                    End Using

                End If
            End If
        Catch ex As Exception

        End Try
    End Function

    'Private Sub GetAmountPIT()
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Try
    '        rnAmountPIT.ClearValue
    '        If IsNumeric(rnPercentPIT.Value) AndAlso IsNumeric(rntxtAmount.Text) Then
    '            rnAmountPIT.Value = CDec(rntxtAmount.Text) * CDec(rnPercentPIT.Value) / 100
    '        End If
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
#End Region

End Class