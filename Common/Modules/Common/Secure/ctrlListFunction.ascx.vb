Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlListFunction
    Inherits CommonView
    Protected WithEvents UserView As ViewBase
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"

    Property lstFunctions As List(Of FunctionDTO)
        Get
            Return ViewState(Me.ID & "_lstFunctions")
        End Get
        Set(ByVal value As List(Of FunctionDTO))
            ViewState(Me.ID & "_lstFunctions") = value
        End Set
    End Property

    Property ActiveFunctions As List(Of FunctionDTO)
        Get
            Return ViewState(Me.ID & "_ActiveFunctions")
        End Get
        Set(ByVal value As List(Of FunctionDTO))
            ViewState(Me.ID & "_ActiveFunctions") = value
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

    Property lstDeleteDecimals As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_lstDeleteDecimals")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_lstDeleteDecimals") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgListFunctions.SetFilter()
            rgListFunctions.AllowCustomPaging = True
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = rtbMainToolBar
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, _
                 ToolbarItem.Edit, _
                 ToolbarItem.Seperator, _
                 ToolbarItem.Save, _
                 ToolbarItem.Cancel, _
                 ToolbarItem.Seperator, _
                 ToolbarItem.Active, _
                 ToolbarItem.Deactive, _
                 ToolbarItem.Delete)
            CType(Me.MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                rgListFunctions.Rebind()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New CommonRepository
            Dim comboBoxDataDTO As New ComboBoxDataDTO
            comboBoxDataDTO.GET_MODULE = True
            comboBoxDataDTO.GET_FUNCTION_GROUP = True
            rep.GetComboList(comboBoxDataDTO) 'Lấy danh sách các Combo.
            If Not comboBoxDataDTO Is Nothing Then
                FillDropDownList(cboModule, comboBoxDataDTO.LIST_MODULE, "NAME", "ID", Common.SystemLanguage, True)
                FillDropDownList(cboFunctionGroup, comboBoxDataDTO.LIST_FUNCTION_GROUP, "NAME", "ID", Common.SystemLanguage, True)

                FillDropDownList(cboModuleFind, comboBoxDataDTO.LIST_MODULE, "NAME", "ID", Common.SystemLanguage, True)
                FillDropDownList(cboFunctionGroupFind, comboBoxDataDTO.LIST_FUNCTION_GROUP, "NAME", "ID", Common.SystemLanguage, True)
            End If

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("NAME", txtFunctionName)
            dic.Add("MODULE_ID", cboModule)
            dic.Add("FID", txtFunctionID)
            dic.Add("FUNCTION_GROUP_ID", cboFunctionGroup)
            dic.Add("SETUP_SIGNER", chkSigner)
            dic.Add("ADMIN_HR", ChkClient)

            Utilities.OnClientRowSelectedChanged(rgListFunctions, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objFunction As New FunctionDTO
        Dim rep As New CommonRepository

        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW 'Thiet lap trang thai là them moi
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgListFunctions.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgListFunctions.SelectedItems.Count > 1 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgListFunctions.SelectedItems.Count > 0 Then
                        CurrentState = CommonMessage.STATE_EDIT
                        UpdateControlState()
                    End If

                Case CommonMessage.TOOLBARITEM_ACTIVE, CommonMessage.TOOLBARITEM_DEACTIVE
                    Dim lstActiveFunctions As New List(Of FunctionDTO)
                    Dim sActive As String = ""
                    Dim bCheck As Boolean = True
                    For intIndex As Int16 = 0 To rgListFunctions.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgListFunctions.SelectedItems(intIndex)
                        If sActive = "" Then
                            sActive = item.GetDataKeyValue("ACTFLG")
                        ElseIf sActive <> item.GetDataKeyValue("ACTFLG") Then
                            bCheck = False
                            Exit For
                        End If
                        lstActiveFunctions.Add(New FunctionDTO With {.ID = Decimal.Parse(item.GetDataKeyValue("ID")),
                                                                 .ACTFLG = sActive})
                    Next
                    ActiveFunctions = lstActiveFunctions 'Đưa danh sách vào property để thực hiện Active/DeActive trong  UpdateControlState
                    If lstActiveFunctions.Count > 0 Then
                        If Not bCheck Then
                            Me.ShowMessage("Các bản ghi không cùng 1 trạng thái, kiểm tra lại!", NotifyType.Warning)
                            Exit Sub
                        End If
                        If sActive = "A" Or sActive.ToUpper = "ÁP DỤNG" Then
                            If CType(e.Item, RadToolBarButton).CommandName = CommonMessage.TOOLBARITEM_ACTIVE Then
                                Me.ShowMessage("Các bản ghi đã ở trạng thái áp dụng trước đó, kiểm tra lại!", NotifyType.Warning)
                                Exit Sub
                            End If
                        Else
                            If CType(e.Item, RadToolBarButton).CommandName = CommonMessage.TOOLBARITEM_DEACTIVE Then
                                Me.ShowMessage("Các bản ghi đã ở trạng thái ngưng áp dụng trước đó, kiểm tra lại!", NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                        If sActive = "A" Or sActive.ToUpper = "ÁP DỤNG" Then
                            ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)

                        Else
                            ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)

                        End If
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_ACTIVE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgListFunctions.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgListFunctions.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item.GetDataKeyValue("ID")))
                    Next

                    lstDeleteDecimals = lstDeletes
                    If lstDeleteDecimals.Count > 0 Then
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    GridExportExcel(rgListFunctions, "Functions")
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objFunction.FID = txtFunctionID.Text.Trim()
                        objFunction.NAME = txtFunctionName.Text.Trim()
                        objFunction.SETUP_SIGNER = chkSigner.Checked
                        objFunction.ADMIN_HR = ChkClient.Checked
                        objFunction.MODULE_ID = Decimal.Parse(cboModule.SelectedValue)
                        objFunction.FUNCTION_GROUP_ID = Decimal.Parse(cboFunctionGroup.SelectedValue)
                        If CurrentState = CommonMessage.STATE_NEW Then ' Trường hợp thêm mới
                            objFunction.ACTFLG = "A"
                            If rep.InsertFunctionList(objFunction) Then
                                'Show message success
                                Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                'IDSelect = gID
                                rgListFunctions.CurrentPageIndex = 0
                                rgListFunctions.MasterTableView.SortExpressions.Clear()
                                rgListFunctions.Rebind()
                                'SelectedItemDataGridByKey(rgListFunctions, IDSelect, )
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                        Else ' Trường hợp sửa
                            'Dim _tmpFuntion = rep.GetFunctionList(New FunctionDTO With {.ID = rgListFunctions.SelectedValue}, 0, 0, 0)
                            objFunction.ID = rgListFunctions.SelectedValue
                            'objFunction.ACTFLG = _tmpFuntion(0).ACTFLG
                            Dim lst As New List(Of FunctionDTO)
                            lst.Add(objFunction)
                            If rep.UpdateFunctionList(lst) Then
                                'Show message success
                                Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                rgListFunctions.Rebind()
                                SelectedItemDataGridByKey(rgListFunctions, IDSelect, , rgListFunctions.CurrentPageIndex)
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                        End If
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                    ExcuteScript("Resize", "ResizeSplitter()")
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New CommonRepository
                If ActiveFunctions(0).ACTFLG = "A" Or ActiveFunctions(0).ACTFLG.ToUpper = "ÁP DỤNG" Then
                    If rep.ActiveFunctions(ActiveFunctions, "I") Then
                        ActiveFunctions = Nothing
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Else
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                    End If
                Else
                    If rep.ActiveFunctions(ActiveFunctions, "A") Then
                        ActiveFunctions = Nothing
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Else
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                End If
                rgListFunctions.Rebind()
                SelectedItemDataGridByKey(rgListFunctions, IDSelect, , rgListFunctions.CurrentPageIndex)
                CurrentState = CommonMessage.STATE_NORMAL
                UpdateControlState()
            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New CommonProcedureNew
                Dim lstid = ","
                For Each item In lstDeleteDecimals
                    lstid = lstid + item.ToString + ","
                Next
                If rep.DELETE_FUNCTION(lstid) Then
                    lstDeleteDecimals = Nothing
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                Else
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
                rgListFunctions.Rebind()
                CurrentState = CommonMessage.STATE_NORMAL
                UpdateControlState()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgListFunctions.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Custom"

    Protected Sub CreateDataFilter()
        Dim rep As New CommonRepository
        Dim obj As New FunctionDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgListFunctions, obj)
            If cboFunctionGroupFind.SelectedValue <> "" Then
                obj.FUNCTION_GROUP_ID = Decimal.Parse(cboFunctionGroupFind.SelectedValue)
            End If
            If cboModuleFind.SelectedValue <> "" Then
                obj.MODULE_ID = Decimal.Parse(cboModuleFind.SelectedValue)
            End If
            Dim Sorts As String = rgListFunctions.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                Me.lstFunctions = rep.GetFunctionList(obj, rgListFunctions.CurrentPageIndex, rgListFunctions.PageSize, MaximumRows, Sorts)
            Else
                Me.lstFunctions = rep.GetFunctionList(obj, rgListFunctions.CurrentPageIndex, rgListFunctions.PageSize, MaximumRows)
            End If
            rgListFunctions.VirtualItemCount = MaximumRows
            rgListFunctions.DataSource = Me.lstFunctions
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Select Case CurrentState
                ''-----------Tab Function----------------'
                Case CommonMessage.STATE_NORMAL
                    txtFunctionID.Text = ""
                    txtFunctionName.Text = ""
                    txtFunctionID.ReadOnly = True
                    txtFunctionName.ReadOnly = True
                    cboFunctionGroup.SelectedIndex = 0
                    cboModule.SelectedIndex = 0
                    chkSigner.Checked = False
                    chkSigner.Enabled = False
                    ChkClient.Checked = False
                    ChkClient.Enabled = False
                    Utilities.EnableRadCombo(cboFunctionGroup, False)
                    Utilities.EnableRadCombo(cboModule, False)
                    EnabledGridNotPostback(rgListFunctions, True)
                Case CommonMessage.STATE_NEW
                    txtFunctionID.Text = ""
                    txtFunctionName.Text = ""
                    txtFunctionID.ReadOnly = False
                    txtFunctionName.ReadOnly = False
                    cboFunctionGroup.SelectedIndex = 0
                    chkSigner.Checked = False
                    chkSigner.Enabled = True
                    ChkClient.Checked = False
                    ChkClient.Enabled = True
                    cboModule.SelectedIndex = 0
                    Utilities.EnableRadCombo(cboFunctionGroup, True)
                    Utilities.EnableRadCombo(cboModule, True)
                    EnabledGridNotPostback(rgListFunctions, True)
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgListFunctions, False)
                    txtFunctionID.ReadOnly = False
                    txtFunctionName.ReadOnly = False
                    chkSigner.Enabled = True
                    ChkClient.Enabled = True
                    Utilities.EnableRadCombo(cboFunctionGroup, True)
                    Utilities.EnableRadCombo(cboModule, True)
            End Select
            ChangeToolbarState()
            txtFunctionID.Focus()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Private Sub CustomValidatorFunctionID_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidatorFunctionID.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim obj As New FunctionDTO
            obj.FID = txtFunctionID.Text.Trim
            If CurrentState = CommonMessage.STATE_NEW Then
                obj.ID = 0
            ElseIf CurrentState = CommonMessage.STATE_EDIT Then
                obj.ID = rgListFunctions.SelectedValue
            End If
            Dim rep As New CommonRepository
            If rep.ValidateFunctionList(obj) Then
                args.IsValid = True
            Else
                args.IsValid = False
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        rgListFunctions.Rebind()
    End Sub

#End Region

End Class