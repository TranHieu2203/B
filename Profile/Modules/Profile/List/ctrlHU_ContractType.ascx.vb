Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_ContractType
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
                        ClearControlValue(txtCode, chkIsRequirement, chkHSL, txtName, txtRemark, rntxtPeriod, cboContract_Type, cboFMD, txtNameVisibleForm, cboCodeGetEndDate, chkHocviec)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgContractType.CurrentPageIndex = 0
                        rgContractType.MasterTableView.SortExpressions.Clear()
                        rgContractType.Rebind()
                        ClearControlValue(txtCode, chkIsRequirement, chkHSL, txtName, txtRemark, rntxtPeriod, cboContract_Type, cboFMD, txtNameVisibleForm, cboCodeGetEndDate, chkHocviec)
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
        Dim _filter As New ContractTypeDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgContractType, _filter)

            Dim Sorts As String = rgContractType.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetContractType(_filter, Sorts).ToTable()
                Else
                    Return rep.GetContractType(_filter).ToTable()
                End If
            Else
                Dim ContractTypes As List(Of ContractTypeDTO)
                If Sorts IsNot Nothing Then
                    ContractTypes = rep.GetContractType(_filter, rgContractType.CurrentPageIndex, rgContractType.PageSize, MaximumRows, Sorts)
                Else
                    ContractTypes = rep.GetContractType(_filter, rgContractType.CurrentPageIndex, rgContractType.PageSize, MaximumRows)
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
                    txtNameVisibleForm.ReadOnly = False
                    cboCodeGetEndDate.Enabled = True
                    cboFMD.Enabled = True
                    chkHocviec.Enabled = True
                    chkHSL.Enabled = True
                    chkIsRequirement.Enabled = True
                    EnableRadCombo(cboContract_Type, True)
                    If cboContract_Type.Items.Count > 0 Then
                        cboContract_Type.SelectedIndex = 0
                    End If
                Case CommonMessage.STATE_NORMAL
                    ', cboFMD, txtNameVisibleForm,cboCodeGetEndDate)
                    Utilities.EnabledGridNotPostback(rgContractType, True)
                    txtCode.ReadOnly = True
                    txtName.ReadOnly = True
                    txtRemark.ReadOnly = True
                    rntxtPeriod.ReadOnly = True
                    chkHSL.Enabled = False
                    chkHocviec.Enabled = False
                    chkIsRequirement.Enabled = False
                    txtNameVisibleForm.ReadOnly = True
                    cboCodeGetEndDate.Enabled = False
                    cboFMD.Enabled = False
                    EnableRadCombo(cboContract_Type, False)
                Case CommonMessage.STATE_EDIT

                    Utilities.EnabledGridNotPostback(rgContractType, False)
                    txtCode.ReadOnly = False
                    txtName.ReadOnly = False
                    txtRemark.ReadOnly = False
                    rntxtPeriod.ReadOnly = False
                    chkHSL.Enabled = True
                    chkHocviec.Enabled = True
                    chkIsRequirement.Enabled = True
                    txtNameVisibleForm.ReadOnly = False
                    cboCodeGetEndDate.Enabled = True
                    cboFMD.Enabled = True
                    EnableRadCombo(cboContract_Type, True)
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

            'Dim dic As New Dictionary(Of String, Control)
            'dic.Add("CODE", txtCode)
            'dic.Add("NAME", txtName)
            'dic.Add("PERIOD", rntxtPeriod)
            'dic.Add("REMARK", txtRemark)
            'dic.Add("TYPE_ID", cboContract_Type)
            'dic.Add("FLOWING_MD_ID", cboFMD)
            'dic.Add("CODE_GET_ENDDATE_ID", cboCodeGetEndDate)
            'dic.Add("NAME_VISIBLE_ONFORM", txtNameVisibleForm)
            'dic.Add("IS_HOCVIEC", chkHocviec)
            'dic.Add("IS_HSL", chkHSL)
            'dic.Add("IS_REQUIREMENT", chkIsRequirement)
            'Utilities.OnClientRowSelectedChanged(rgContractType, dic)

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    Private Sub LoadCombobox()
        Try
            Dim rep As New ProfileRepository
            Dim dtData = rep.GetOtherList("CONTRACT_TYPE")
            FillRadCombobox(cboContract_Type, dtData, "NAME", "ID", False)

            'THEO THANG/NGAY
            Dim dtData1 = rep.GetOtherList("THOIHANHD")
            FillRadCombobox(cboFMD, dtData1, "NAME", "ID", False)

            'QUY TAT LAY NGAY KET THUC
            Dim dtData2 = rep.GetOtherList("QUYTACLAMTRON_HDLD").Select("CODE <> 'LAYTHEOTHANG'").CopyToDataTable
            FillRadCombobox(cboCodeGetEndDate, dtData2, "NAME", "ID", False)

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
        Dim objContractType As New ContractTypeDTO
        Dim gID As Decimal
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtCode, chkIsRequirement, chkHSL, txtName, txtRemark, rntxtPeriod, cboFMD, txtNameVisibleForm, cboCodeGetEndDate, chkHocviec)

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
                        objContractType.NAME = txtName.Text
                        objContractType.REMARK = txtRemark.Text
                        objContractType.NAME_VISIBLE_ONFORM = txtNameVisibleForm.Text
                        objContractType.FLOWING_MD_ID = Decimal.Parse(cboFMD.SelectedValue)
                        objContractType.CODE_GET_ENDDATE_ID = Decimal.Parse(cboCodeGetEndDate.SelectedValue)
                        objContractType.IS_HOCVIEC = chkHocviec.Checked
                        objContractType.IS_HSL = chkHSL.Checked
                        objContractType.IS_REQUIREMENT = chkIsRequirement.Checked
                        If rntxtPeriod.Value Is Nothing Then
                            objContractType.PERIOD = 0
                        Else
                            Try
                                objContractType.PERIOD = rntxtPeriod.Value
                            Catch ex As Exception
                                ShowMessage(Translate(CommonMessage.MESSAGE_NUMBER_EXPIRE), Utilities.NotifyType.Error)
                                Exit Sub
                            End Try

                        End If
                        objContractType.TYPE_ID = Decimal.Parse(cboContract_Type.SelectedValue)
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objContractType.ACTFLG = "A"
                                If rep.InsertContractType(objContractType, gID) Then
                                    CurrentState = CommonMessage.STATE_NEW
                                    IDSelect = gID
                                    Refresh("InsertView")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objContractType.ID = rgContractType.SelectedValue
                                'check exist item asset
                                'Dim _validate As New ContractTypeDTO
                                '_validate.ID = rgContractType.SelectedValue
                                'If rep.ValidateContractType(_validate) Then
                                '    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                '    Exit Sub
                                'End If
                                If rep.ModifyContractType(objContractType, gID) Then
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
                    'ClearControlValue(txtCode, txtName, txtRemark, rntxtPeriod, cboContract_Type)
                    'rgContractType.Rebind()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ClearControlValue(txtCode, chkIsRequirement, chkHSL, txtName, txtRemark, rntxtPeriod, cboContract_Type, cboFMD, txtNameVisibleForm, cboCodeGetEndDate, chkHocviec)
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
            ClearControlValue(txtCode, chkIsRequirement, chkHSL, txtName, txtRemark, rntxtPeriod, cboContract_Type, cboFMD, txtNameVisibleForm, cboCodeGetEndDate, chkHocviec)
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
        Dim _validate As New ContractTypeDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgContractType.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateContractType(_validate)
            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateContractType(_validate)
            End If
            rep.Dispose()
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
    ''' Xu ly su kien servervalidate cho control cvalPeriod
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalPeriod_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalPeriod.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rntxtPeriod.Value Is Nothing Then
                args.IsValid = False
                Exit Sub
            End If
            If rntxtPeriod.Value < 0 Then
                args.IsValid = False
                Exit Sub
            End If
            args.IsValid = True
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

    Protected Sub rgContractType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rgContractType.SelectedIndexChanged
        ClearControlValue(txtCode, txtName, rntxtPeriod, txtRemark, cboContract_Type, cboFMD, cboCodeGetEndDate, txtNameVisibleForm, chkHocviec, chkHSL, chkIsRequirement)
        If rgContractType.SelectedItems.Count = 1 Then
            Dim item As GridDataItem = rgContractType.SelectedItems(0)
            If item.GetDataKeyValue("CODE") IsNot Nothing Then
                txtCode.Text = item.GetDataKeyValue("CODE").ToString
            End If
            If item.GetDataKeyValue("NAME") IsNot Nothing Then
                txtName.Text = item.GetDataKeyValue("NAME").ToString
            End If
            If item.GetDataKeyValue("PERIOD") IsNot Nothing AndAlso IsNumeric(item.GetDataKeyValue("PERIOD").ToString) Then
                rntxtPeriod.Value = Decimal.Parse(item.GetDataKeyValue("PERIOD").ToString)
            End If
            If item.GetDataKeyValue("REMARK") IsNot Nothing Then
                txtRemark.Text = item.GetDataKeyValue("REMARK").ToString
            End If
            If item.GetDataKeyValue("TYPE_ID") IsNot Nothing AndAlso IsNumeric(item.GetDataKeyValue("TYPE_ID").ToString) Then
                cboContract_Type.SelectedValue = Decimal.Parse(item.GetDataKeyValue("TYPE_ID").ToString)
            End If
            If item.GetDataKeyValue("FLOWING_MD_ID") IsNot Nothing AndAlso IsNumeric(item.GetDataKeyValue("FLOWING_MD_ID").ToString) Then
                cboFMD.SelectedValue = Decimal.Parse(item.GetDataKeyValue("FLOWING_MD_ID").ToString)
            End If
            LoadCombobox_cboCodeGetEndDate()
            If item.GetDataKeyValue("CODE_GET_ENDDATE_ID") IsNot Nothing AndAlso IsNumeric(item.GetDataKeyValue("CODE_GET_ENDDATE_ID").ToString) Then
                cboCodeGetEndDate.SelectedValue = Decimal.Parse(item.GetDataKeyValue("CODE_GET_ENDDATE_ID").ToString)
            End If
            If item.GetDataKeyValue("NAME_VISIBLE_ONFORM") IsNot Nothing Then
                txtNameVisibleForm.Text = item.GetDataKeyValue("NAME_VISIBLE_ONFORM").ToString
            End If
            If item.GetDataKeyValue("IS_HOCVIEC") IsNot Nothing AndAlso CType(item.GetDataKeyValue("IS_HOCVIEC"), Boolean) Then
                chkHocviec.Checked = True
            End If
            If item.GetDataKeyValue("IS_HSL") IsNot Nothing AndAlso CType(item.GetDataKeyValue("IS_HSL"), Boolean) Then
                chkHSL.Checked = True
            End If
            If item.GetDataKeyValue("IS_REQUIREMENT") IsNot Nothing AndAlso CType(item.GetDataKeyValue("IS_REQUIREMENT"), Boolean) Then
                chkIsRequirement.Checked = True
            End If
        End If
    End Sub

    Private Sub LoadCombobox_cboCodeGetEndDate()
        Dim rep As New ProfileRepository
        ClearControlValue(cboCodeGetEndDate)
        cboCodeGetEndDate.Items.Clear()
        If IsNumeric(cboFMD.SelectedValue) AndAlso rep.GetOtherList("THOIHANHD").Select("ID = " & cboFMD.SelectedValue).CopyToDataTable.Rows(0)("CODE").ToString = "THANG" Then
            Dim dtData = rep.GetOtherList("QUYTACLAMTRON_HDLD")
            FillRadCombobox(cboCodeGetEndDate, dtData, "NAME", "ID", False)
        Else
            Dim dtData = rep.GetOtherList("QUYTACLAMTRON_HDLD").Select("CODE <> 'LAYTHEOTHANG'").CopyToDataTable
            FillRadCombobox(cboCodeGetEndDate, dtData, "NAME", "ID", False)
        End If
    End Sub

    Protected Sub cboFMD_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboFMD.SelectedIndexChanged
        LoadCombobox_cboCodeGetEndDate()
    End Sub

#End Region

End Class