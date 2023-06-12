Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_Ward
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = True
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Modules/Profile/Report/" + Me.GetType().Name.ToString()
#Region "Property"

    Property lstComboData As ComboBoxDataDTO
        Get
            Return PageViewState(Me.ID & "_lstComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            PageViewState(Me.ID & "_lstComboData") = value
        End Set
    End Property

#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            rgMain.SetFilter()
            rgMain.AllowCustomPaging = True
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            InitControl()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, _
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
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CurrentState = STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, , rgSignWork.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, )
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                End Select
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        If lstComboData Is Nothing Then
            lstComboData = New ComboBoxDataDTO
            lstComboData.GET_NATION = True
            lstComboData.GET_PROVINCE = True
            lstComboData.GET_DISTRICT = True
            Dim rep As New ProfileRepository
            rep.GetComboList(lstComboData) 'Lấy danh sách các Combo.
            rep.Dispose()
        End If

        Dim dic As New Dictionary(Of String, Control)
        dic.Add("CODE", txtCode)
        dic.Add("NAME_VN", txtVNName)
        dic.Add("NATION_ID;NATION_NAME", cboNation)
        dic.Add("PROVINCE_ID;PROVINCE_NAME", cboProvince)
        dic.Add("DISTRICT_ID;DISTRICT_NAME", cboDistrict)
        dic.Add("NOTE", txtNote)
        Utilities.OnClientRowSelectedChanged(rgMain, dic)
    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objWard As New Ward_DTO
        Dim rep As New ProfileRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_CREATE
                    CurrentState = STATE_NEW 'Thiet lap trang thai là them moi
                    ClearControlValue(cboDistrict, cboNation, cboProvince, txtCode, txtNote, txtVNName)
                    'txtCode.Text = rep.AutoGenCode("XP", "HU_WARD", "CODE")
                    cboNation.Items.Clear()
                    cboNation.SelectedValue = ""
                    cboNation.Text = ""
                    cboProvince.Items.Clear()
                    cboProvince.SelectedValue = ""
                    cboProvince.Text = ""
                    cboDistrict.Items.Clear()
                    cboDistrict.SelectedValue = ""
                    cboDistrict.Text = ""
                    FillRadCombobox(cboNation, lstComboData.LIST_NATION, "NAME_VN", "ID")
                Case TOOLBARITEM_EDIT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgMain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    cboNation.Items.Clear()
                    cboNation.SelectedValue = ""
                    cboNation.Text = ""
                    cboProvince.Items.Clear()
                    cboProvince.SelectedValue = ""
                    cboProvince.Text = ""
                    cboDistrict.Items.Clear()
                    cboDistrict.SelectedValue = ""
                    cboDistrict.Text = ""

                    Dim item As GridDataItem = rgMain.SelectedItems(0)
                    FillRadCombobox(cboNation, lstComboData.LIST_NATION, "NAME_VN", "ID")
                    If item.GetDataKeyValue("NATION_ID") IsNot Nothing Then
                        cboNation.SelectedValue = item.GetDataKeyValue("NATION_ID")
                        cboNation.Text = item.GetDataKeyValue("NATION_NAME")
                        cboNation_SelectedIndexChanged(Nothing, Nothing)
                    End If
                    If item.GetDataKeyValue("PROVINCE_ID") IsNot Nothing Then
                        cboProvince.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
                        cboProvince.Text = item.GetDataKeyValue("PROVINCE_NAME")
                        cboProvince_SelectedIndexChanged(Nothing, Nothing)
                    End If
                    If item.GetDataKeyValue("DISTRICT_ID") IsNot Nothing Then
                        cboDistrict.SelectedValue = item.GetDataKeyValue("DISTRICT_ID")
                        cboDistrict.Text = item.GetDataKeyValue("DISTRICT_NAME")
                    End If
                    CurrentState = STATE_EDIT

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item("ID").Text))
                    Next

                    If Not rep.CheckExistInDatabase(lstDeletes, ProfileCommonTABLE_NAME.HU_WARD) Then
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
                            rgMain.ExportExcel(Server, Response, dtData, "Ward")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objWard.CODE = txtCode.Text.Trim()
                        objWard.NAME_VN = txtVNName.Text
                        objWard.NOTE = txtNote.Text
                        objWard.DISTRICT_ID = Decimal.Parse(cboDistrict.SelectedValue)
                        If CurrentState = STATE_NEW Then ' Trường hợp thêm mới
                            objWard.ACTFLG = "A"
                            If rep.InsertWard(objWard, gID) Then
                                'Show message success
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                rgMain.CurrentPageIndex = 0
                                rgMain.MasterTableView.SortExpressions.Clear()
                                rgMain.Rebind()
                                'SelectedItemDataGridByKey(rgMain, gID, )
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                                ClearAllData()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)

                            End If
                        Else ' Trường hợp sửa
                            Dim validate As New Ward_DTO
                            validate.ID = rgMain.SelectedValue
                            If rep.ValidateWard(validate) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                Exit Sub
                            End If
                            objWard.ID = rgMain.SelectedValue
                            If rep.ModifyWard(objWard, gID) Then
                                'Show message success
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                rgMain.Rebind()
                                'SelectedItemDataGridByKey(rgMain, gID, , rgMain.CurrentPageIndex)
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                                ClearAllData()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                        End If
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgMain')")
                    End If
                Case TOOLBARITEM_CANCEL 'Truong hop huy
                    ClearControlValue(cboDistrict, cboNation, cboProvince, txtCode, txtNote, txtVNName)
                    CurrentState = STATE_NORMAL
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
    ''' <summary>
    ''' Event Yes/No trên message thông báo hỏi xóa, ngừng áp dụng, áp dụng
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
                UpdateControlState()
                ClearAllData()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState()
                ClearAllData()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
                ClearAllData()

            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Load, reload grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <summary>
    ''' Event Selected combobox quốc gia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboNation_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboNation.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If cboNation.SelectedValue <> "" Then
                GetProvinceByNationID(cboProvince, cboNation.SelectedValue, cboProvince.SelectedValue)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event Selected combobox Tỉnh thành
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboProvince_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboProvince.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If cboProvince.SelectedValue <> "" Then
                GetDistrictByProvinceID(cboDistrict, cboProvince.SelectedValue, cboDistrict.SelectedValue)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Validate Quốc gia có tồn tại hay ko?có còn áp dụng hay ko?
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalNation_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalNation.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New NationDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = STATE_EDIT Or CurrentState = STATE_NEW Then
                validate.ID = cboNation.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateNation(validate)
            End If
            If Not args.IsValid Then
                lstComboData = New ComboBoxDataDTO
                lstComboData.GET_NATION = True
                lstComboData.GET_PROVINCE = True
                rep.GetComboList(lstComboData) 'Lấy danh sách các Combo.
                If lstComboData IsNot Nothing Then
                    FillRadCombobox(cboNation, lstComboData.LIST_NATION, "NAME_VN", "ID")
                End If
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Validate Tỉnh thành có tồn tại hay ko?có còn áp dụng hay ko?
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalProvince_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalProvince.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New ProvinceDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = STATE_EDIT Or CurrentState = STATE_NEW Then
                validate.ID = cboProvince.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateProvince(validate)
            End If
            If Not args.IsValid Then
                If cboNation.SelectedValue <> "" AndAlso cboNation.SelectedValue > 0 Then
                    GetProvinceByNationID(cboProvince, cboNation.SelectedValue, cboProvince.SelectedValue)
                End If
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Validate Quận huyện có tồn tại hay ko?có còn áp dụng hay ko?
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalDistrict_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalDistrict.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New DistrictDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = STATE_EDIT Or CurrentState = STATE_NEW Then
                validate.ID = cboDistrict.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateDistrict(validate)
            End If
            If Not args.IsValid Then
                If cboProvince.SelectedValue <> "" Then
                    GetDistrictByProvinceID(cboDistrict, cboProvince.SelectedValue, cboDistrict.SelectedValue)
                End If
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
    ''' <summary>
    ''' Clear data control
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearAllData()
        ClearControlValue(txtCode, txtVNName, cboNation, cboProvince, cboDistrict)
        rgMain.SelectedIndexes.Clear()
    End Sub
    ''' <summary>
    ''' Update trạng thái page, control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New ProfileRepository
            Select Case CurrentState
                ''-----------Tab District----------------'
                Case STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    EnableControlAll(False, cboDistrict, cboNation, cboProvince, txtCode, txtNote, txtVNName)
                    cboNation.AutoPostBack = False
                    cboProvince.AutoPostBack = False
                    'txtCode.ReadOnly = True
                Case STATE_NEW
                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, cboDistrict, cboNation, cboProvince, txtCode, txtNote, txtVNName)
                    cboNation.AutoPostBack = True
                    cboProvince.AutoPostBack = True
                    'txtCode.ReadOnly = True
                Case STATE_EDIT
                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, cboDistrict, cboNation, cboProvince, txtCode, txtNote, txtVNName)
                    cboNation.AutoPostBack = True
                    cboProvince.AutoPostBack = True
                    'txtCode.ReadOnly = True
                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    cboNation.AutoPostBack = False
                    cboProvince.AutoPostBack = False
                    Dim districtID As String
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                        districtID = item.GetDataKeyValue("DISTRICT_ID")
                    Next
                    If rep.ActiveWard(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    cboNation.AutoPostBack = False
                    cboProvince.AutoPostBack = False
                    Dim districtID As String
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                        districtID = item.GetDataKeyValue("DISTRICT_ID")
                    Next
                    If rep.ActiveWard(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    cboNation.AutoPostBack = False
                    cboProvince.AutoPostBack = False
                    Dim districtID As String
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                        districtID = item.GetDataKeyValue("DISTRICT_ID")
                    Next
                    If rep.DeleteWard(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            rep.Dispose()
            cboNation.Focus()
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Load data from DB to Grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileRepository
        Dim _filter As New Ward_DTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetWard(_filter, Sorts).ToTable()
                Else
                    Return rep.GetWard(_filter).ToTable()
                End If
            Else
                Dim lstWard As New List(Of Ward_DTO)
                If Sorts IsNot Nothing Then
                    lstWard = rep.GetWard(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    lstWard = rep.GetWard(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If
                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = lstWard
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' get data combobox tỉnh thành filter theo quốc gia
    ''' </summary>
    ''' <param name="cboProvince"></param>
    ''' <param name="sNationID"></param>
    ''' <param name="sProvinceID"></param>
    ''' <remarks></remarks>
    Private Sub GetProvinceByNationID(ByVal cboProvince As Telerik.Web.UI.RadComboBox, Optional ByVal sNationID As String = "", Optional ByVal sProvinceID As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim lstProvinces As New List(Of ProvinceDTO)
            If lstComboData.LIST_PROVINCE.Count > 0 Then
                If sNationID <> "" Then
                    lstProvinces = (From p In lstComboData.LIST_PROVINCE Where p.NATION_ID = sNationID).ToList
                End If
            End If
            FillDropDownList(cboProvince, lstProvinces, "NAME_VN", "ID", Common.Common.SystemLanguage, True, sProvinceID)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Get data cho combo quận huyện filter theo tỉnh thành
    ''' </summary>
    ''' <param name="cboDistrict"></param>
    ''' <param name="sProvinceID"></param>
    ''' <param name="sDistrictID"></param>
    ''' <remarks></remarks>
    Private Sub GetDistrictByProvinceID(ByVal cboDistrict As Telerik.Web.UI.RadComboBox, Optional ByVal sProvinceID As String = "", Optional ByVal sDistrictID As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim lstDistrict As New List(Of DistrictDTO)
            If lstComboData.LIST_PROVINCE.Count > 0 Then
                If sProvinceID <> "" Then
                    lstDistrict = (From p In lstComboData.LIST_DISTRICT Where p.PROVINCE_ID = sProvinceID).ToList
                End If
            End If
            FillDropDownList(cboDistrict, lstDistrict, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboDistrict.SelectedValue)

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Validate code
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New BankDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If CurrentState = STATE_EDIT Then

                _validate.ID = rgMain.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateBank(_validate)

            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateBank(_validate)
            End If

            If Not args.IsValid Then
                'txtCode.Text = rep.AutoGenCode("XP", "HU_WARD", "CODE")
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


End Class