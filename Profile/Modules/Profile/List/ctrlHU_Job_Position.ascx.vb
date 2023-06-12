Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_Job_Position
    Inherits Common.CommonView
    Dim repHF As HistaffFrameworkRepository
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/List/" + Me.GetType().Name.ToString()

#Region "Property"

    Public Property IDComco As String
        Get
            Return PageViewState(Me.ID & "_IDComco")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_IDComco") = value
        End Set
    End Property

    Dim IDCom As String
#End Region

#Region "Page"
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
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                 ToolbarItem.Edit,
                 ToolbarItem.Save,
                 ToolbarItem.Cancel,
                 ToolbarItem.Delete,
                 ToolbarItem.Seperator,
                 ToolbarItem.Seperator,
                 ToolbarItem.Active,
                 ToolbarItem.Deactive)
            CType(Me.MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
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
            LoadCombo()

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("JOB_ID", cboJob)
            dic.Add("NAME", txtNameVN)
            dic.Add("NAME_EN", txtNameEN)

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
        Dim obj As New JobPositionDTO
        Dim rep As New ProfileRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_CREATE
                    CurrentState = STATE_NEW 'Thiet lap trang thai là them moi
                    ClearControlValue(txtNameVN, txtNameEN, cboJob)
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

                    Dim lstID As New List(Of Decimal)
                    Dim item As GridDataItem = rgBankBranchs.SelectedItems(0)
                    lstID.Add(Decimal.Parse(item.GetDataKeyValue("ID")))

                    'If Not rep.CheckExistInDatabase(lstID, ProfileCommonTABLE_NAME.HU_JOB_POSITION) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    CurrentState = STATE_EDIT
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgBankBranchs.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgBankBranchs.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgBankBranchs.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item.GetDataKeyValue("ID")))
                    Next

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
                        lstDeletes.Add(Decimal.Parse(item.GetDataKeyValue("ID")))
                    Next

                    'If Not rep.CheckExistInDatabase(lstDeletes, ProfileCommonTABLE_NAME.HU_JOB_POSITION) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                    '    Return
                    'End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If cboJob.SelectedValue = "" Then
                            ShowMessage("Bạn phải chọn Tên công việc.", NotifyType.Warning)
                            Exit Sub
                        End If

                        obj.NAME = txtNameVN.Text.Trim()
                        obj.NAME_EN = txtNameEN.Text.Trim()
                        obj.JOB_ID = Decimal.Parse(cboJob.SelectedValue)

                        If CurrentState = STATE_NEW Then ' Trường hợp thêm mới
                            obj.ACTFLG = "A"
                            If rep.InsertjobPosition(obj, gID) Then
                                'Show message success
                                Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                rgBankBranchs.CurrentPageIndex = 0
                                rgBankBranchs.MasterTableView.SortExpressions.Clear()
                                rgBankBranchs.Rebind()
                                CurrentState = CommonMessage.STATE_NEW
                                IDCom = cboJob.SelectedValue
                                UpdateControlState()
                                ClearControlValue(txtNameVN, txtNameEN, cboJob)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                        Else ' Trường hợp sửa
                            obj.ID = rgBankBranchs.SelectedValue
                            If rep.ModifyjobPosition(obj, gID) Then
                                Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                rgBankBranchs.Rebind()
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                                ClearControlValue(txtNameVN, txtNameEN, cboJob)
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
                    ClearControlValue(txtNameVN, txtNameEN, cboJob)
            End Select
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
                If rep.DeletejobPosition(lstDeletes) Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgBankBranchs.Rebind()
                    CurrentState = STATE_NORMAL
                Else
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
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
    Protected Function CreateDataFilter()
        Dim rep As New ProfileRepository
        Dim _filter As New JobPositionDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgBankBranchs, _filter)
            Dim Sorts As String = rgBankBranchs.MasterTableView.SortExpressions.GetSortString()
            Dim lst As List(Of JobPositionDTO)
            If Sorts IsNot Nothing Then
                lst = rep.GetjobPosition(_filter, rgBankBranchs.CurrentPageIndex, rgBankBranchs.PageSize, MaximumRows, Common.Common.SystemLanguage.Name, Sorts)
            Else
                lst = rep.GetjobPosition(_filter, rgBankBranchs.CurrentPageIndex, rgBankBranchs.PageSize, MaximumRows, Common.Common.SystemLanguage.Name)
            End If
            rgBankBranchs.VirtualItemCount = MaximumRows
            rgBankBranchs.DataSource = lst
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
                    txtNameVN.ReadOnly = True
                    txtNameEN.ReadOnly = True
                    Utilities.EnableRadCombo(cboJob, False)
                    ClearControlValue(cboJob, txtNameEN, txtNameVN)
                Case STATE_NEW
                    EnabledGridNotPostback(rgBankBranchs, False)
                    txtNameVN.ReadOnly = False
                    txtNameEN.ReadOnly = False
                    Utilities.EnableRadCombo(cboJob, True)
                    cboJob.SelectedValue = IDCom
                Case STATE_EDIT
                    EnabledGridNotPostback(rgBankBranchs, False)
                    txtNameVN.ReadOnly = False
                    txtNameEN.ReadOnly = False
                    Utilities.EnableRadCombo(cboJob, True)
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgBankBranchs.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgBankBranchs.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActivejobPosition(lstDeletes, "A") Then
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
                    If rep.ActivejobPosition(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgBankBranchs.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
            End Select
            ChangeToolbarState()
            cboJob.Focus()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    'Protected Sub cusBank_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusBank.ServerValidate
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        Dim rep As New ProfileRepository
    '        Dim validate As New BankDTO
    '        If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
    '            Try
    '                If (cboJob.SelectedValue <> "") Then
    '                    validate.ID = cboJob.SelectedValue
    '                    validate.ACTFLG = "A"
    '                    args.IsValid = rep.ValidateBank(validate)
    '                Else
    '                    args.IsValid = False
    '                End If
    '            Catch ex As Exception
    '                args.IsValid = False
    '            End Try

    '        End If
    '        If Not args.IsValid Then
    '            LoadCombo()
    '        End If
    '        _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    ''' <summary>
    ''' Load data for combobox Bank
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadCombo()
        Dim rep As New ProfileRepository
        'repHF = New HistaffFrameworkRepository
        'Dim dtData1 = repHF.ExecuteToDataSet("PKG_OMS_BUSINESS.GET_JOB", New List(Of Object)({Common.Common.SystemLanguage.Name})).Tables(0)
        Dim dtData1 = rep.GetDataByProcedures(5, 0, "", Common.Common.SystemLanguage.Name)
        If dtData1 IsNot Nothing Then
            FillRadCombobox(cboJob, dtData1, "NAME", "ID")
        End If
    End Sub
#End Region

End Class