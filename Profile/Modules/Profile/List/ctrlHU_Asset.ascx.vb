Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_Asset
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/List/" + Me.GetType().Name.ToString()
    Protected WithEvents AssetView As ViewBase

#Region "Properties"

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
    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Hien thi cac control tren page theo trang thai cac control da duoc thiet lap
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Goi ham khoi tao gia tri ban dau cho cac control tren page
    ''' khoi tao trang thai cho grid rgAssets voi cac thiet lap filter
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgAssets.SetFilter()
            rgAssets.AllowCustomPaging = True
            'rgAssets.ClientSettings.EnablePostBackOnRowClick = True
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Khoi tao gia tri cho cac control tren page
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
                 ToolbarItem.Edit, ToolbarItem.Save,
                 ToolbarItem.Cancel, ToolbarItem.Active,
                 ToolbarItem.Deactive, ToolbarItem.Export,
                 ToolbarItem.Delete)
            CType(Me.MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Refresh("")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Rebind du lieu cho rgAssets theo case message
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgAssets.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, , rgSignWork.CurrentPageIndex)
                        ClearControlValue(txtCode, txtName, txtRemark, cboAssetGroup)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgAssets.CurrentPageIndex = 0
                        rgAssets.MasterTableView.SortExpressions.Clear()
                        rgAssets.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, )
                        ClearControlValue(txtCode, txtName, txtRemark, cboAssetGroup)
                    Case "Cancel"
                        rgAssets.MasterTableView.ClearSelectedItems()
                End Select
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
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Fill du lieu cho control combobox, thiet lap ngon ngu hien thi cho cac control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileRepository
            Dim dtData = rep.GetOtherList("ASSET_GROUP") ' Tam thoi fix cung type ( OtherListType="ASSET_GROUP" - Nhom tai san cap phat).
            FillRadCombobox(cboAssetGroup, dtData, "NAME", "ID")
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", txtCode)
            dic.Add("NAME", txtName)
            dic.Add("GROUP_ID", cboAssetGroup)
            dic.Add("REMARK", txtRemark)
            rep.Dispose()
            Utilities.OnClientRowSelectedChanged(rgAssets, dic)
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
    ''' 30/06/2017 15:33
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command cho control OnMainToolbar
    ''' Cac command la them moi, sua, kich hoat, huy kich hoat, xoa, xuat file, luu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objAsset As New AssetDTO
        Dim rep As New ProfileRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_CREATE
                    CurrentState = STATE_NEW 'Thiet lap trang thai là them moi
                    ClearControlValue(txtName, txtCode, txtRemark, cboAssetGroup)
                    txtCode.Text = rep.AutoGenCode("TSCP", "HU_ASSET", "CODE")
                    UpdateControlState()
                    rgAssets.Rebind()
                Case TOOLBARITEM_EDIT
                    If rgAssets.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgAssets.SelectedItems.Count > 1 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgAssets.SelectedItems.Count > 0 Then
                        CurrentState = STATE_EDIT
                        UpdateControlState()
                    End If
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgAssets.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgAssets.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgAssets.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgAssets.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgAssets.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item("ID").Text))
                    Next
                    If Not rep.CheckExistInDatabase(lstDeletes, ProfileCommonTABLE_NAME.HU_ASSET) Then
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
                            rgAssets.ExportExcel(Server, Response, dtData, "Asset")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objAsset.NAME = txtName.Text.Trim()
                        objAsset.CODE = txtCode.Text.Trim()
                        objAsset.GROUP_ID = Decimal.Parse(cboAssetGroup.SelectedValue)
                        objAsset.REMARK = txtRemark.Text.Trim()
                        If CurrentState = STATE_NEW Then ' Trường hợp thêm mới
                            objAsset.ACTFLG = "A"
                            If rep.InsertAsset(objAsset, gID) Then
                                CurrentState = CommonMessage.STATE_NORMAL
                                IDSelect = gID
                                Refresh("InsertView")
                                UpdateControlState()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                        Else ' Trường hợp sửa
                            objAsset.ID = rgAssets.SelectedValue
                            'check exist item asset
                            Dim _validate As New AssetDTO
                            _validate.ID = rgAssets.SelectedValue
                            If rep.ValidateAsset(_validate) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                ClearControlValue(txtCode, txtName, txtRemark, cboAssetGroup)
                                rgAssets.Rebind()
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                                Exit Sub
                            End If
                            If rep.ModifyAsset(objAsset, gID) Then
                                CurrentState = CommonMessage.STATE_NORMAL
                                IDSelect = objAsset.ID
                                Refresh("UpdateView")
                                UpdateControlState()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                        End If
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgAssets')")
                    End If
                Case TOOLBARITEM_CANCEL
                    CurrentState = STATE_NORMAL
                    UpdateControlState()
                    rgAssets.Rebind()
            End Select
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 15:37
    ''' </lastupdate>
    ''' <summary>
    ''' Xu lu su kien button command cho control ctrlMessageBox
    ''' Xu ly cho cac command kich hoat, huy kich hoat, xoa
    ''' Cap nhat lai trang thai cac control theo tung command
    ''' Rebind lai du lieu tren grid rgAssets
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
                For idx = 0 To rgAssets.SelectedItems.Count - 1
                    Dim item As GridDataItem = rgAssets.SelectedItems(idx)
                    lstDeletes.Add(Decimal.Parse(item("ID").Text))
                Next
                If rep.DeleteAsset(lstDeletes, strError) Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgAssets.Rebind()
                    CurrentState = STATE_NORMAL
                Else
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
                UpdateControlState()
                rep.Dispose()
            End If
            ClearControlValue(txtName, txtCode, txtRemark, cboAssetGroup)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 15:44
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc dung de ket noi du lieu truoc khi bind du lieu va grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgAssets.NeedDataSource
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
    ''' 30/06/2017 15:46
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly server validate cho control cvalCode
    ''' Call ham AutogenCode(firstChar As String, tableName As String, colName As String)
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New AssetDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = STATE_EDIT Then

                _validate.ID = rgAssets.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateAsset(_validate)
            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateAsset(_validate)
            End If

            If Not args.IsValid Then
                txtCode.Text = rep.AutoGenCode("TSCP", "HU_ASSET", "CODE")
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 31/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly server validate cho control cusvalAssetGroup
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cusvalAssetGroup_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusvalAssetGroup.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New OtherListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboAssetGroup.SelectedValue
                validate.ACTFLG = "A"
                validate.CODE = ProfileCommon.OT_ASSET_GROUP.Code
                args.IsValid = rep.ValidateOtherList(validate)
            End If
            If Not args.IsValid Then
                Dim dtData = rep.GetOtherList("ASSET_GROUP")
                FillRadCombobox(cboAssetGroup, dtData, "NAME", "ID")
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 30/06/2017 15:52
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai control tren page theo trang thai them moi, sua hay default, kich hoat
    ''' huy kich hoat, update trang thai cac item tren toolbar theo tung truong hop 
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case STATE_NEW
                    EnabledGridNotPostback(rgAssets, False)
                    EnableControlAll(True, txtName, txtCode, txtRemark, cboAssetGroup)
                    'ClearControlValue(txtName, txtCode, txtRemark, cboAssetGroup)
                    txtCode.ReadOnly = True
                Case STATE_NORMAL
                    EnabledGridNotPostback(rgAssets, True)
                    EnableControlAll(False, txtName, txtCode, txtRemark, cboAssetGroup)
                    ClearControlValue(txtName, txtCode, txtRemark, cboAssetGroup)
                    txtCode.ReadOnly = True


                Case STATE_EDIT
                    EnabledGridNotPostback(rgAssets, False)
                    EnableControlAll(True, txtName, txtCode, txtRemark, cboAssetGroup)
                    txtCode.ReadOnly = True
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgAssets.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgAssets.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAsset(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgAssets.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgAssets.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgAssets.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAsset(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgAssets.Rebind()
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
    ''' 30/06/2017 15:54
    ''' </lastupdate>
    ''' <summary>
    ''' Ham tao du lieu filter theo cac tham so dau vao
    ''' mac dinh isFull  load full man hinh hay ko?
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileRepository
        Dim _filter As New AssetDTO
        Dim lstAssets As List(Of AssetDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgAssets, _filter)
            Dim Sorts As String = rgAssets.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetAsset(_filter, Sorts).ToTable()
                Else
                    Return rep.GetAsset(_filter).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    lstAssets = rep.GetAsset(_filter, rgAssets.CurrentPageIndex, rgAssets.PageSize, MaximumRows, Sorts)
                Else
                    lstAssets = rep.GetAsset(_filter, rgAssets.CurrentPageIndex, rgAssets.PageSize, MaximumRows)
                End If
                rgAssets.VirtualItemCount = MaximumRows
                rgAssets.DataSource = lstAssets
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