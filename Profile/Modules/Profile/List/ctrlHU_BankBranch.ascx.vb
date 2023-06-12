Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_BankBranch
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/List/" + Me.GetType().Name.ToString()

#Region "Property"

#End Region

#Region "Page"

    Private Property AjaxManager As RadAjaxManager

    Private Property AjaxManagerId As String

    ''' <lastupdate>
    ''' 28/06/2017 10:04
    ''' </lastupdate>
    ''' <summary>
    ''' Ham set du lieu va hien thi du lieu cho page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            UpdateControlState()
            rgBankBranchs.SetFilter()
            rgBankBranchs.AllowCustomPaging = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 28/06/2017 10:07
    ''' </lastupdate>
    ''' <summary>
    ''' Ham goi phuong thuc khoi tao trang thai, gia tri, su kien cho cac control tren page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                ViewConfig(RadPane2)
                GirdConfig(rgBankBranchs)
            End If
            rgBankBranchs.SetFilter()
            rgBankBranchs.AllowCustomPaging = True
            rgBankBranchs.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <lastupdate>
    ''' 28/06/2017 10:08
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc khoi tao trang thai, gia tri, su kien cho cac control tren page
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            'Khoi tao ToolBar
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, _
                 ToolbarItem.Edit, _
                 ToolbarItem.Seperator, _
                 ToolbarItem.Save, _
                 ToolbarItem.Cancel, _
                 ToolbarItem.Seperator, _
                 ToolbarItem.Active, _
                 ToolbarItem.Deactive, _
                 ToolbarItem.Export, _
                 ToolbarItem.Delete)
            CType(Me.MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <lastupdate>
    ''' 28/06/2017 10:08
    ''' </lastupdate>
    ''' <summary>
    ''' Ham lam moi du lieu, trang thai cho cac control tren page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = STATE_NORMAL
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 28/06/2017 10:08
    ''' </lastupdate>
    ''' <summary>
    ''' Ham bind du lieu cho control tren page
    ''' Set ngon ngu cho cac control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'Lay danh sach quoc gia de bind len dropdownlist.
            Dim rep As New ProfileRepository
            Dim comboBoxDataDTO As New ComboBoxDataDTO
            comboBoxDataDTO.GET_NATION = True
            comboBoxDataDTO.GET_BANK = True
            rep.GetComboList(comboBoxDataDTO) 'Lấy danh sách các Combo.
            'Lay danh sach ngan hang
            If Not comboBoxDataDTO Is Nothing Then
                FillDropDownList(cboBank, comboBoxDataDTO.LIST_BANK, "NAME", "ID", Common.Common.SystemLanguage, True, cboBank.SelectedValue)
            End If

            Dim dic As New Dictionary(Of String, Control)
            'dic.Add("CODE", txtCode)
            dic.Add("NAME", txtName)
            dic.Add("BANK_ID", cboBank)
            dic.Add("REMARK", txtRemark)
            rep.Dispose()
            Utilities.OnClientRowSelectedChanged(rgBankBranchs, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try

    End Sub


#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 28/06/2017 10:12
    ''' </lastupdate>
    ''' <summary>
    ''' Ham su ly su kien Command cho control OnMainToolbar
    ''' Cac command bao gom: tao moi, sua, kick hoat, huy kick hoat, xoa, xuat excel, luu, huy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objBankBranch As New BankBranchDTO
        Dim rep As New ProfileRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_CREATE
                    CurrentState = STATE_NEW 'Thiet lap trang thai là them moi
                    ClearControlValue(txtName, txtCode, txtRemark, cboBank)
                    'txtCode.Text = rep.AutoGenCode("CNNH", "HU_BANK_BRANCH", "CODE")
                    rgBankBranchs.Rebind()
                Case TOOLBARITEM_EDIT
                    If rgBankBranchs.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgBankBranchs.SelectedItems.Count > 1 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = STATE_EDIT
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgBankBranchs.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgBankBranchs.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgBankBranchs.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgBankBranchs.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgBankBranchs.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item("ID").Text))
                    Next

                    If Not rep.CheckExistInDatabase(lstDeletes, ProfileCommonTABLE_NAME.HU_BANK_BRANCH) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                        Return
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgBankBranchs.ExportExcel(Server, Response, dtData, "BankBranchs")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using
                Case TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objBankBranch.NAME = txtName.Text.Trim()
                        objBankBranch.REMARK = txtRemark.Text.Trim()
                        objBankBranch.CODE = txtCode.Text.Trim()

                        objBankBranch.BANK_ID = Decimal.Parse(cboBank.SelectedValue)
                        If CurrentState = STATE_NEW Then ' Trường hợp thêm mới
                            objBankBranch.ACTFLG = "A"
                            If rep.InsertBankBranch(objBankBranch, gID) Then
                                'Show message success
                                Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                rgBankBranchs.CurrentPageIndex = 0
                                rgBankBranchs.MasterTableView.SortExpressions.Clear()
                                rgBankBranchs.Rebind()
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                                ClearControlValue(txtCode, txtName, txtRemark, cboBank)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                        Else ' Trường hợp sửa
                            objBankBranch.ID = rgBankBranchs.SelectedValue
                            Dim lstID As New List(Of Decimal)
                            lstID.Add(objBankBranch.ID)
                            Using re As New ProfileBusinessRepository
                                If re.ValidateBusiness("HU_BANK_BRANCH", "ID", lstID) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                    Exit Sub
                                End If
                            End Using
                            If rep.ModifyBankBranch(objBankBranch, gID) Then
                                Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                rgBankBranchs.Rebind()
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                                ClearControlValue(txtCode, txtName, txtRemark, cboBank)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                        End If
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgBankBranchs')")
                    End If
                    rgBankBranchs.Rebind()
                Case TOOLBARITEM_CANCEL
                    CurrentState = STATE_NORMAL
                    ClearControlValue(txtName, txtCode, txtRemark, cboBank)
            End Select
            rep.Dispose()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 28/06/2017 10:16
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly button command cho control ctrlMessageBox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New ProfileRepository
                Dim strError As String = ""
                Dim lstDeletes As New List(Of Decimal)
                For idx = 0 To rgBankBranchs.SelectedItems.Count - 1
                    Dim item As GridDataItem = rgBankBranchs.SelectedItems(idx)
                    lstDeletes.Add(item.GetDataKeyValue("ID"))
                Next
                If rep.DeleteBankBranch(lstDeletes, strError) Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgBankBranchs.Rebind()
                    CurrentState = STATE_NORMAL
                Else
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
                rep.Dispose()
                UpdateControlState()
                rgBankBranchs.Rebind()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <lastupdate>
    ''' 28/06/2017
    ''' </lastupdate>
    ''' <summary>
    ''' Ham ket noi data truoc khi render cua grid rgBankBranchs
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgBankBranchs.NeedDataSource
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
    ''' <lastupdate>
    ''' 28/06/2017 10:26
    ''' </lastupdate>
    ''' <summary>
    ''' Ham tao du lieu cho filter 
    ''' Do du lieu cho data grid rgBankBranchs
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileRepository
        Dim _filter As New BankBranchDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgBankBranchs, _filter)
            Dim Sorts As String = rgBankBranchs.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetBankBranch(_filter, Sorts).ToTable()
                Else
                    Return rep.GetBankBranch(_filter).ToTable()
                End If
            Else
                Dim lstBankBranchs As List(Of BankBranchDTO)
                If Sorts IsNot Nothing Then
                    lstBankBranchs = rep.GetBankBranch(_filter, rgBankBranchs.CurrentPageIndex, rgBankBranchs.PageSize, MaximumRows, Sorts)
                Else
                    lstBankBranchs = rep.GetBankBranch(_filter, rgBankBranchs.CurrentPageIndex, rgBankBranchs.PageSize, MaximumRows)
                End If
                rgBankBranchs.VirtualItemCount = MaximumRows
                rgBankBranchs.DataSource = lstBankBranchs
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 28/06/2017 10:27
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat cac trang thai cho control tren page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Dim rep As New ProfileRepository
            Select Case CurrentState
                ''-----------Tab BankBranch----------------'
                Case STATE_NORMAL
                    EnabledGridNotPostback(rgBankBranchs, True)
                    txtName.ReadOnly = True
                    txtCode.ReadOnly = True
                    txtRemark.ReadOnly = True
                    Utilities.EnableRadCombo(cboBank, False)
                    ClearControlValue(cboBank, txtName, txtCode, txtRemark)
                Case STATE_NEW
                    EnabledGridNotPostback(rgBankBranchs, False)
                    txtName.ReadOnly = False
                    txtCode.ReadOnly = True
                    txtRemark.ReadOnly = False
                    Utilities.EnableRadCombo(cboBank, True)
                Case STATE_EDIT
                    EnabledGridNotPostback(rgBankBranchs, False)
                    txtName.ReadOnly = False
                    txtCode.ReadOnly = True
                    txtRemark.ReadOnly = False
                    Utilities.EnableRadCombo(cboBank, True)
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgBankBranchs.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgBankBranchs.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveBankBranch(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgBankBranchs.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgBankBranchs.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgBankBranchs.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveBankBranch(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgBankBranchs.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
            End Select
            rep.Dispose()
            ChangeToolbarState()
            cboBank.Focus()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ' ''' <lastupdate>
    ' ''' 28/06/2017 10:30
    ' ''' </lastupdate>
    ' ''' <summary>
    ' ''' Phuong thuc xu ly server validate cho control cvalCode
    ' ''' </summary>
    ' ''' <param name="source"></param>
    ' ''' <param name="args"></param>
    ' ''' <remarks></remarks>
    'Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
    '    Dim rep As New ProfileRepository
    '    Dim _validate As New BankBranchDTO
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If CurrentState = CommonMessage.STATE_EDIT Then
    '            _validate.CODE = txtCode.Text
    '            _validate.ID = rgBankBranchs.SelectedValue
    '            args.IsValid = rep.ValidateBankBranch(_validate)
    '        Else
    '            _validate.CODE = txtCode.Text
    '            args.IsValid = rep.ValidateBankBranch(_validate)
    '        End If

    '        If Not args.IsValid Then
    '            txtCode.Text = rep.AutoGenCode("CNNH", "HU_BANK_BRANCH", "CODE")
    '        End If
    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Server Validate cua control cvalDisciplineLevel
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cusBank_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusBank.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileRepository
            Dim validate As New BankDTO
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                Try
                    If (cboBank.SelectedValue <> "") Then
                        validate.ID = cboBank.SelectedValue
                        validate.ACTFLG = "A"
                        args.IsValid = rep.ValidateBank(validate)
                    Else
                        args.IsValid = False
                    End If
                Catch ex As Exception
                    args.IsValid = False
                End Try
                rep.Dispose()
            End If
            If Not args.IsValid Then
                LoadCombo()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Load data for combobox Bank
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadCombo()
        Dim rep As New ProfileRepository
        Dim comboBoxDataDTO As New ComboBoxDataDTO
        comboBoxDataDTO.GET_BANK = True
        rep.GetComboList(comboBoxDataDTO) 'Lấy danh sách các Combo.
        If Not comboBoxDataDTO Is Nothing Then
            FillDropDownList(cboBank, comboBoxDataDTO.LIST_BANK, "NAME", "ID", Common.Common.SystemLanguage, True)
        End If
        rep.Dispose()
    End Sub
#End Region

End Class