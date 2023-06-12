Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_District
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

    Property ProvinceID As Decimal
        Get
            Return PageViewState(Me.ID & "_ProvinceID")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_ProvinceID") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView, la ham load du lieu, control states cua usercontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Refresh()
            UpdateControlState()
            rgMain.SetFilter()
            rgMain.AllowCustomPaging = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khoi tao cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try

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
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Set lại trạng thái form khi load lại trang
    ''' </summary>
    ''' <param name="Message">không sử dụng</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
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
    ''' <summary>
    ''' Load du lieu cac combobox va set ngon ngu cac control va column name
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If lstComboData Is Nothing Then
                lstComboData = New ComboBoxDataDTO
                lstComboData.GET_NATION = True
                lstComboData.GET_PROVINCE = True
                Dim rep As New ProfileRepository
                rep.GetComboList(lstComboData) 'Lấy danh sách các Combo.
                rep.Dispose()
            End If
            If lstComboData IsNot Nothing Then
                FillRadCombobox(cboNation, lstComboData.LIST_NATION, "NAME_VN", "ID")
            End If

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", txtCode)
            dic.Add("NAME_VN", txtVNName)
            dic.Add("NATION_ID;NATION_NAME", cboNation)
            dic.Add("PROVINCE_ID;PROVINCE_NAME", cboProvince)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' Hàm xử lý các event click trên menu toolbar: thêm mới, sửa, lưu, hủy, Áp dụng, ngừng áp dụng, xóa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objDistrict As New DistrictDTO
        Dim rep As New ProfileRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_CREATE
                    CurrentState = STATE_NEW 'Thiet lap trang thai là them moi
                    ClearControlValue(txtCode, txtVNName, cboNation, cboProvince)
                    'txtCode.Text = rep.AutoGenCode("QH", "HU_DISTRICT", "CODE")
                    cboNation.Items.Clear()
                    cboNation.SelectedValue = ""
                    cboNation.Text = ""
                    cboProvince.Items.Clear()
                    cboProvince.SelectedValue = ""
                    cboProvince.Text = ""
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
                    End If
                    CurrentState = STATE_EDIT
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        ProvinceID = item.GetDataKeyValue("PROVINCE_ID")
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next


                    'If Not rep.CheckExistInDatabase(lstID, ProfileCommonTABLE_NAME.HU_DISTRICT) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                    '    Return
                    'End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        ProvinceID = item.GetDataKeyValue("PROVINCE_ID")
                    Next
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
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                        ProvinceID = item.GetDataKeyValue("PROVINCE_ID")
                    Next

                    If Not rep.CheckExistInDatabase(lstDeletes, ProfileCommonTABLE_NAME.HU_DISTRICT) Then
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
                            rgMain.ExportExcel(Server, Response, dtData, "District")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objDistrict.CODE = txtCode.Text.Trim()
                        objDistrict.NAME_VN = txtVNName.Text.Trim()
                        objDistrict.PROVINCE_ID = Decimal.Parse(cboProvince.SelectedValue)
                        If CurrentState = STATE_NEW Then ' Trường hợp thêm mới
                            objDistrict.ACTFLG = "A"
                            If rep.InsertDistrict(objDistrict, gID) Then
                                'Show message success
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                rgMain.CurrentPageIndex = 0
                                rgMain.MasterTableView.SortExpressions.Clear()
                                rgMain.Rebind()
                                'SelectedItemDataGridByKey(rgMain, gID, )
                                ClearControlValue(txtCode, txtVNName, cboNation, cboProvince)
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                                ClearAllData()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)

                            End If
                        Else ' Trường hợp sửa
                            Dim validate As New DistrictDTO
                            validate.ID = rgMain.SelectedValue
                            If rep.ValidateDistrict(validate) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                Exit Sub
                            End If
                            objDistrict.ID = rgMain.SelectedValue
                            If rep.ModifyDistrict(objDistrict, gID) Then
                                'Show message success
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                rgMain.Rebind()
                                'SelectedItemDataGridByKey(rgMain, gID, , rgMain.CurrentPageIndex)
                                ClearControlValue(txtCode, txtVNName, cboNation, cboProvince)
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                                ClearAllData()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                        End If
                    End If
                Case TOOLBARITEM_CANCEL 'Truong hop huy
                    ClearControlValue(txtCode, txtVNName, cboNation, cboProvince)
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
    ''' Event Yes, No khi popup messagebox => khi click nut Ap dung, Ngung Ap Dung, Xoa
    ''' Va update lai trang thai control
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
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New ProfileRepository
                Dim strError As String = ""
                Dim lstDeletes As New List(Of Decimal)
                For idx = 0 To rgMain.SelectedItems.Count - 1
                    Dim item As GridDataItem = rgMain.SelectedItems(idx)
                    lstDeletes.Add(item.GetDataKeyValue("ID"))
                    ProvinceID = item.GetDataKeyValue("PROVINCE_ID")
                Next

                If rep.DeleteDistrict(lstDeletes, strError, ProvinceID) Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgMain.Rebind()
                    ClearAllData()
                Else
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
                rep.Dispose()
                CurrentState = STATE_NORMAL
            End If
            UpdateControlState()

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <summary>
    ''' Load du lieu len grid
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
    ''' event thay doi gia tri combobox Quoc gia => Filter lai combobox Quan huyen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboNation_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboNation.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If cboNation.SelectedValue <> "" AndAlso cboNation.SelectedValue > 0 Then
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
    ''' Ham xu ly viec check gia tri ID, Code
    ''' Tu dong gen code khi them moi
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New DistrictDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgMain.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateDistrict(_validate)
            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateDistrict(_validate)
            End If

            If Not args.IsValid Then
                'txtCode.Text = rep.AutoGenCode("QH", "HU_DISTRICT", "CODE")
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
#End Region

#Region "Custom"
    ''' <summary>
    ''' Clear data control
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearAllData()
        Try
            ClearControlValue(txtCode, txtVNName, cboNation, cboProvince)
            rgMain.SelectedIndexes.Clear()
        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' Update trang thai cac control sau khi an nut: them, sua, xoa, ap dung, ngung ap dung
    ''' Va process event: xoa, ap dung, ngung ap dung
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
                    EnableControlAll(False, txtVNName, cboNation, cboProvince, txtCode)
                    EnabledGridNotPostback(rgMain, True)
                    cboNation.AutoPostBack = False
                    'txtCode.ReadOnly = True
                Case STATE_NEW
                    EnableControlAll(True, txtVNName, cboNation, cboProvince, txtCode)
                    EnabledGridNotPostback(rgMain, False)
                    cboNation.AutoPostBack = True
                    'txtCode.ReadOnly = True
                Case STATE_EDIT
                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, txtVNName, cboNation, cboProvince, txtCode)
                    cboNation.AutoPostBack = True
                    'txtCode.ReadOnly = True
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveDistrict(lstDeletes, "A", ProvinceID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                        ClearAllData()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveDistrict(lstDeletes, "I", ProvinceID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                        ClearAllData()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
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
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileRepository
        Dim _filter As New DistrictDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetDistrict(_filter, Sorts).ToTable()
                Else
                    Return rep.GetDistrict(_filter).ToTable()
                End If
            Else
                Dim lstDistrct As New List(Of DistrictDTO)
                If Sorts IsNot Nothing Then
                    lstDistrct = rep.GetDistrict(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    lstDistrct = rep.GetDistrict(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If
                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = lstDistrct
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
    ''' Load combobox Quan huyen
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
                    lstProvinces = (From p In lstComboData.LIST_PROVINCE Where p.NATION_ID = Decimal.Parse(sNationID)).ToList
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
#End Region

End Class