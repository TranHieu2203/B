Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_BHLDItem
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Modules/Profile/List/" + Me.GetType().Name.ToString()

#Region "Property"

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property
#End Region

#Region "Page"

    '''<lastupdate>
    ''' 27/06/2017 11:02
    ''' </lastupdate>
    ''' <summary>
    ''' Hien thi thong tin loai hop dong
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                Refresh()
                UpdateControlState()
                SetGridFilter(rgContractType)
                rgContractType.AllowCustomPaging = True
                _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
                DisplayException(Me.ViewName, Me.ID, ex)
            End Try
        End If
    End Sub

    '''<lastupdate>
    ''' 27/06/2017 11:02
    ''' </lastupdate>
    ''' <summary>
    ''' Hien thi thong tin khoi tao ban dau
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            InitControl()
            'If Not IsPostBack Then
            '    ViewConfig(RadPane1)
            '    GirdConfig(rgContractType)
            'End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    '''<lastupdate>
    ''' 27/06/2017 11:02
    ''' </lastupdate>
    ''' <summary>
    ''' Hien thi thong tin khoi tao ban dau
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarContractTypes
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete, ToolbarItem.Seperator,
                                       ToolbarItem.Active,
                                       ToolbarItem.Deactive,
                                       ToolbarItem.Export)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    '''<lastupdate>
    ''' 27/06/2017 11:02
    ''' </lastupdate>
    ''' <summary>
    ''' Lam moi thong tin tren page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        'Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgContractType.Rebind()
                        'SelectedItemDataGridByKey(rgContractType, IDSelect, , rgContractType.CurrentPageIndex)
                        ClearControlValue(txtCode, chkIsRequirement, txtName, txtRemark, rntxtPeriod, rntxtdg, rnSTT, chkHocviec)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgContractType.CurrentPageIndex = 0
                        rgContractType.MasterTableView.SortExpressions.Clear()
                        rgContractType.Rebind()
                        ClearControlValue(txtCode, chkIsRequirement, txtName, txtRemark, rntxtPeriod, rntxtdg, rnSTT, chkHocviec)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgContractType.MasterTableView.ClearSelectedItems()
                End Select
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    '''<lastupdate>
    ''' 27/06/2017 11:06
    ''' </lastupdate>
    ''' <summary>
    ''' Hien thi va set gia tri, trang thai cho DataFilter
    ''' </summary>
    ''' <param name="isFull"> Mac dinh gia tri False</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileRepository
        Dim _filter As New BHLDItemDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgContractType, _filter)

            Dim Sorts As String = rgContractType.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetBHLDItem(_filter, Sorts).ToTable()
                Else
                    Return rep.GetBHLDItem(_filter).ToTable()
                End If
            Else
                Dim ContractTypes As List(Of BHLDItemDTO)
                If Sorts IsNot Nothing Then
                    ContractTypes = rep.GetBHLDItem(_filter, rgContractType.CurrentPageIndex, rgContractType.PageSize, MaximumRows, Sorts)
                Else
                    ContractTypes = rep.GetBHLDItem(_filter, rgContractType.CurrentPageIndex, rgContractType.PageSize, MaximumRows)
                End If

                rgContractType.VirtualItemCount = MaximumRows
                rgContractType.DataSource = ContractTypes
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function

    '''<lastupdate>
    ''' 27/06/2017 11:06
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai cac control tren page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    Utilities.EnabledGridNotPostback(rgContractType, False)

                    txtCode.ReadOnly = False
                    txtName.ReadOnly = False
                    txtRemark.ReadOnly = False
                    rntxtPeriod.ReadOnly = False
                    rntxtdg.ReadOnly = False
                    rnSTT.ReadOnly = False
                    chkHocviec.Enabled = True
                    chkIsRequirement.Enabled = True
                Case CommonMessage.STATE_NORMAL
                    ', cboFMD, txtNameVisibleForm,cboCodeGetEndDate)
                    Utilities.EnabledGridNotPostback(rgContractType, True)
                    txtCode.ReadOnly = True
                    txtName.ReadOnly = True
                    txtRemark.ReadOnly = True
                    rntxtPeriod.ReadOnly = True

                    chkHocviec.Enabled = False
                    chkIsRequirement.Enabled = False
                    rntxtdg.ReadOnly = True
                    rnSTT.ReadOnly = True
                Case CommonMessage.STATE_EDIT

                    Utilities.EnabledGridNotPostback(rgContractType, False)
                    txtCode.ReadOnly = False
                    txtName.ReadOnly = False
                    txtRemark.ReadOnly = False
                    rntxtPeriod.ReadOnly = False

                    chkHocviec.Enabled = True
                    chkIsRequirement.Enabled = True
                    rntxtdg.ReadOnly = False
                    rnSTT.ReadOnly = False
                Case CommonMessage.STATE_DEACTIVE

                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgContractType.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgContractType.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveContractType(lstDeletes, "I") Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgContractType.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgContractType.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveContractType(lstDeletes, "A") Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgContractType.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgContractType.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteContractType(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                    End If
            End Select
            rep.Dispose()
            txtCode.Focus()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        UpdateToolbarState()
    End Sub

    '''<lastupdate>
    ''' 27/06/2017 11:08
    ''' </lastupdate>
    ''' <summary>
    ''' Bind du lieu vao grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            LoadCombobox()

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", txtCode)
            dic.Add("NAME_VN", txtName)
            dic.Add("MONEY", rntxtPeriod)
            dic.Add("REMARK", txtRemark)
            dic.Add("UNIT", rntxtdg)
            dic.Add("HIDE", chkHocviec)
            dic.Add("AUTOGEN", chkIsRequirement)
            dic.Add("ORDER_NUM", rnSTT)
            Utilities.OnClientRowSelectedChanged(rgContractType, dic)

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    Private Sub LoadCombobox()
        Try
            Dim rep As New ProfileRepository

            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    '''<lastupdate>
    ''' 27/06/2017 11:08
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien khi click tren toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objContractType As New BHLDItemDTO
        Dim gID As Decimal
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtCode, chkIsRequirement, txtName, txtRemark, rntxtPeriod, rntxtdg, rnSTT, chkHocviec)

                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgContractType.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgContractType.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgContractType.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgContractType.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DELETE

                    If rgContractType.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If


                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgContractType.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgContractType.SelectedItems(idx)
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next

                    If Not rep.CheckExistInDatabase(lstID, ProfileCommonTABLE_NAME.HU_CONTRACT_TYPE) Then
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
                            rgContractType.ExportExcel(Server, Response, dtData, "ContractType")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objContractType.CODE = txtCode.Text
                        objContractType.NAME_VN = txtName.Text
                        objContractType.REMARK = txtRemark.Text
                        objContractType.HIDE = chkHocviec.Checked
                        objContractType.AUTOGEN = chkIsRequirement.Checked
                        objContractType.UNIT = CDec(Val(rntxtPeriod.Value))
                        objContractType.MONEY = CDec(Val(rntxtdg.Value))
                        objContractType.ORDER_NUM = CDec(Val(rnSTT.Value))


                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objContractType.ACTFLG = "A"
                                If rep.checkOrderNum(0, CDec(Val(rnSTT.Value))) > 0 Then
                                    ShowMessage("Trùng số thứ tự", NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.InsertBHLDItem(objContractType, gID) Then
                                    CurrentState = CommonMessage.STATE_NEW
                                    IDSelect = gID
                                    Refresh("InsertView")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objContractType.ID = rgContractType.SelectedValue
                                If rep.checkOrderNum(objContractType.ID, CDec(Val(rnSTT.Value))) > 0 Then
                                    ShowMessage("Trùng số thứ tự", NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.ModifyBHLDItem(objContractType, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objContractType.ID
                                    Refresh("UpdateView")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgContractType')")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    ClearControlValue(txtCode, chkIsRequirement, txtName, txtRemark, rntxtPeriod, rntxtdg, rnSTT, chkHocviec)
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
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

    '''<lastupdate>
    ''' 27/06/2017 11:08
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien button command cho control ctrMessageBox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState()
            End If
            ClearControlValue(txtCode, chkIsRequirement, txtName, txtRemark, rntxtPeriod, rntxtdg, rnSTT, chkHocviec)
            rgContractType.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    '''<lastupdate>
    ''' 27/06/2017 11:08
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho grid rgContractType
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgContractType.NeedDataSource
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

    '''<lastupdate>
    ''' 27/06/2017 11:08
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien servervalidate cho control cvalCode
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New BHLDItemDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgContractType.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateBHLDItem(_validate)
            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateBHLDItem(_validate)
            End If
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
    '''<lastupdate>
    ''' 27/06/2017 11:08
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai Toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

#End Region

End Class