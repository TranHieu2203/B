Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_Bank
    Inherits CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/List/" + Me.GetType().Name.ToString()

#Region "Property"

#End Region

#Region "Page"
    '''<lastupdate>
    ''' 28/06/2017 8:31
    ''' </lastupdate>
    ''' <summary>
    ''' Hien thi thong tin danh muc ngan hang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            UpdateControlState()
            rgBank.SetFilter()
            rgBank.AllowCustomPaging = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    '''<lastupdate>
    ''' 28/06/2017 9:02
    ''' </lastupdate>
    ''' <summary>
    ''' Goi ham khoi tao du lieu cho cac control trong page
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
    '''<lastupdate>
    ''' 28/06/2017 9:02
    ''' </lastupdate>
    ''' <summary>
    ''' Khoi tao du lieu cho cac control trong page
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
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    '''<lastupdate>
    ''' 28/06/2017 9:02
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai cho cac control tren page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileRepository

            Select Case CurrentState
                Case STATE_NEW
                    Utilities.EnabledGridNotPostback(rgBank, False)
                    EnableControlAll(True, txtCode, txtName, txtShortName)
                    'txtCode.ReadOnly = True
                Case STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgBank, True)
                    EnableControlAll(False, txtCode, txtName, txtShortName)
                    ClearControlValue(txtCode, txtName, txtShortName)
                    'txtCode.ReadOnly = True
                Case STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgBank, False)
                    EnableControlAll(True, txtCode, txtName, txtShortName)
                    'txtCode.ReadOnly = True
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgBank.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgBank.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveBank(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgBank.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgBank.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgBank.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveBank(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgBank.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
            End Select
            rep.Dispose()
            txtName.Focus()
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    '''<lastupdate>
    ''' 28/06/2017 9:02
    ''' </lastupdate>
    ''' <summary>
    ''' Ham lam moi gia tri CurrentState set o thoi diem hien tai ve gia tri default
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
    '''<lastupdate>
    ''' 28/06/2017 9:02
    ''' </lastupdate>
    ''' <summary>
    ''' Thiet lap ngon ngu cho cac column trong table
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", txtCode)
            dic.Add("NAME", txtName)
            dic.Add("SHORT_NAME", txtShortName)
            Utilities.OnClientRowSelectedChanged(rgBank, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw
        End Try

    End Sub
#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 28/06/2017 9:09
    ''' </lastupdate>
    ''' <summary>
    ''' Su kien click cho control OnMainToolbar
    ''' Xu ly cac su kien "them moi", "sua", "kich hoat", "huy kich hoat", "xoa", "xuat excel", "luu", "huy"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objBank As New BankDTO
        Dim rep As New ProfileRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_CREATE
                    CurrentState = STATE_NEW 'Thiet lap trang thai là them moi
                    ClearControlValue(txtName, txtCode, txtShortName)
                    'txtCode.Text = rep.AutoGenCode("NH", "HU_BANK", "CODE")
                    For Each column In rgBank.MasterTableView.OwnerGrid.Columns
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter
                        column.CurrentFilterValue = String.Empty
                    Next

                    rgBank.MasterTableView.FilterExpression = String.Empty
                    rgBank.Rebind()
                Case TOOLBARITEM_EDIT
                    If rgBank.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    ElseIf rgBank.SelectedItems.Count > 1 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    ElseIf rgBank.SelectedItems.Count > 0 Then
                        CurrentState = STATE_EDIT
                        UpdateControlState()
                    End If
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgBank.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgBank.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgBank.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgBank.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgBank.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item("ID").Text))
                    Next
                    If Not rep.CheckExistInDatabase(lstDeletes, ProfileCommonTABLE_NAME.HU_BANK) Then
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
                            rgBank.ExportExcel(Server, Response, dtData, "Banks")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objBank.NAME = txtName.Text.Trim()
                        objBank.CODE = txtCode.Text.Trim()
                        objBank.SHORT_NAME = txtShortName.Text.Trim()
                        If CurrentState = STATE_NEW Then ' Trường hợp thêm mới
                            objBank.ACTFLG = "A"
                            If rep.InsertBank(objBank, gID) Then
                                Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                rgBank.CurrentPageIndex = 0
                                rgBank.MasterTableView.SortExpressions.Clear()
                                rgBank.Rebind()
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                                ClearControlValue(txtCode, txtName, txtShortName)
                            Else
                                Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                        Else ' Trường hợp sửa
                            objBank.ID = rgBank.SelectedValue
                            Dim lstID As New List(Of Decimal)
                            lstID.Add(objBank.ID)
                            Using re As New ProfileBusinessRepository
                                If re.ValidateBusiness("HU_BANK", "ID", lstID) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                    Exit Sub
                                End If
                            End Using
                            If rep.ModifyBank(objBank, gID) Then
                                Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                rgBank.Rebind()
                                ClearControlValue(txtCode, txtName, txtShortName)
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                            Else
                                Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                        End If
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgBank')")
                    End If

                Case TOOLBARITEM_CANCEL
                    CurrentState = STATE_NORMAL
                    ClearControlValue(txtName, txtCode, txtShortName)
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
    ''' 28/06/2017 9:16
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien ButtonCommand (button Yes/No) cho control ctrlMessageBox
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
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New ProfileRepository
                Dim strError As String = ""
                Dim lstDeletes As New List(Of Decimal)
                For idx = 0 To rgBank.SelectedItems.Count - 1
                    Dim item As GridDataItem = rgBank.SelectedItems(idx)
                    lstDeletes.Add(item.GetDataKeyValue("ID"))
                Next
                If rep.DeleteBank(lstDeletes, strError) Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgBank.Rebind()
                    CurrentState = STATE_NORMAL
                Else
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
                rep.Dispose()
            End If
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 28/06/2017 09:22
    ''' </lastupdate>
    ''' <summary>
    ''' Ket noi du lieu truoc khi render cho control rgBank
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgBank.NeedDataSource
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

    ''' <lastupdate>
    ''' 28/06/2017
    ''' </lastupdate>
    ''' <summary>
    ''' Set server validate cho control cvalCode
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New BankDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = STATE_EDIT Then

                _validate.ID = rgBank.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateBank(_validate)

            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateBank(_validate)
            End If

            'If Not args.IsValid Then
            '    txtCode.Text = rep.AutoGenCode("NH", "HU_BANK", "CODE")
            'End If
            rep.Dispose()
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
    ''' 28/06/2017 10:02
    ''' </lastupdate>
    ''' <summary>
    ''' Ham tao du lieu theo filter
    ''' Do du lieu sau filter vao grid rgBanks
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileRepository
        Dim _filter As New BankDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgBank, _filter)
            Dim Sorts As String = rgBank.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetBank(_filter, Sorts).ToTable()
                Else
                    Return rep.GetBank(_filter).ToTable()
                End If
            Else
                Dim lstBanks As New List(Of BankDTO)
                If Sorts IsNot Nothing Then
                    lstBanks = rep.GetBank(_filter, rgBank.CurrentPageIndex, rgBank.PageSize, MaximumRows, Sorts)
                Else
                    lstBanks = rep.GetBank(_filter, rgBank.CurrentPageIndex, rgBank.PageSize, MaximumRows)
                End If
                rgBank.VirtualItemCount = MaximumRows
                rgBank.DataSource = lstBanks
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
#End Region

End Class