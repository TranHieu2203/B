Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlInsListParamInsurance
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = True
    Public IDSelect As Integer
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Insurance/Modules/Insurance/List/" + Me.GetType().Name.ToString()
#Region "Property"
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <summary>
    ''' Reload, khởi tạo page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.MainToolBar = tbarOtherLists
            Me.ctrlMessageBox.Listener = Me
            Me.rgGridDataRate.SetFilter()
            Me.rgGridDataRate.AllowCustomPaging = True
            If Not IsPostBack Then
                Call Refresh()
                ViewConfig(RadPaneField)
                GirdConfig(rgGridDataRate)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' khoi tao page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarOtherLists
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Export)
            CType(Me.MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <summary>
    ''' Load data from grid to control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            CurrentState = CommonMessage.STATE_NORMAL
            Dim dic As New Dictionary(Of String, Control)
            InitControl()
            GetDataCombo()
            'dic.Add("LOCATION_ID", cboLocation)
            dic.Add("EFFECTIVE_DATE", rdEffectiveDate)
            dic.Add("SI", radnmSI)
            dic.Add("HI", radnmHI)
            dic.Add("UI", radnmUI)
            dic.Add("SI_COM", radnmSI_COM)
            dic.Add("SI_EMP", radnmSI_EMP)
            dic.Add("HI_COM", radnmHI_COM)
            dic.Add("HI_EMP", radnmHI_EMP)
            dic.Add("UI_COM", radnmUI_COM)
            dic.Add("UI_EMP", radnmUI_EMP)
            dic.Add("BHTNLD_BNN_COM", radnmTNLD_BNN_COM)
            dic.Add("BHTNLD_BNN_EMP", radnmTNLD_BNN_EMP)
            dic.Add("SICK", radnmSICK)
            dic.Add("MATERNITY", radnmMATERNITY)
            dic.Add("OFF_IN_HOUSE", radnmOFF_IN_HOUSE)
            dic.Add("OFF_TOGETHER", radnmOFF_TOGETHER)
            dic.Add("RETIRE_MALE", radnmRETIRE_MALE)
            dic.Add("RETIRE_FEMALE", radnmRETIRE_FEMALE)
            dic.Add("SI_DATE", txtSI_DATE)
            dic.Add("HI_DATE", txtHI_DATE)
            dic.Add("SI_NN", radnmSI_NN)
            dic.Add("SI_COM_NN", radnmSI_COM_NN)
            dic.Add("SI_EMP_NN", radnmSI_EMP_NN)
            dic.Add("FOREIGN", chkForeigner)
            Utilities.OnClientRowSelectedChanged(rgGridDataRate, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Khởi tạo, load menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Reload page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceBusinessClient
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgGridDataRate.Rebind()
                        'SelectedItemDataGridByKey(rgGridDataRate, IDSelect, , rgGridDataRate.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgGridDataRate.CurrentPageIndex = 0
                        rgGridDataRate.MasterTableView.SortExpressions.Clear()
                        rgGridDataRate.Rebind()
                        'SelectedItemDataGridByKey(rgGridDataRate, IDSelect, )
                    Case "Cancel"
                        rgGridDataRate.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <summary>
    ''' Reload grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGridDataRate_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGridDataRate.NeedDataSource
        LoadDataGrid()
    End Sub
    ''' <summary>
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim gID As Decimal
        Dim objData As New INS_SPECIFIED_OBJECTS_DTO
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgGridDataRate.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objData.EFFECTIVE_DATE = rdEffectiveDate.SelectedDate
                        objData.SI = radnmSI.Value
                        objData.HI = radnmHI.Value
                        objData.UI = radnmUI.Value
                        objData.SI_COM = radnmSI_COM.Value
                        objData.SI_EMP = radnmSI_EMP.Value
                        objData.HI_COM = radnmHI_COM.Value
                        objData.HI_EMP = radnmHI_EMP.Value
                        objData.UI_COM = radnmUI_COM.Value
                        objData.UI_EMP = radnmUI_EMP.Value
                        objData.BHTNLD_BNN_COM = radnmTNLD_BNN_COM.Value
                        objData.BHTNLD_BNN_EMP = radnmTNLD_BNN_EMP.Value
                        objData.SICK = radnmSICK.Value
                        objData.MATERNITY = radnmMATERNITY.Value
                        objData.OFF_IN_HOUSE = radnmOFF_IN_HOUSE.Value
                        objData.OFF_TOGETHER = radnmOFF_TOGETHER.Value
                        objData.RETIRE_MALE = radnmRETIRE_MALE.Value
                        objData.RETIRE_FEMALE = radnmRETIRE_FEMALE.Value
                        objData.SI_DATE = txtSI_DATE.Value
                        objData.HI_DATE = txtHI_DATE.Value
                        objData.SI_NN = radnmSI_NN.Value
                        objData.SI_COM_NN = radnmSI_COM_NN.Value
                        objData.SI_EMP_NN = radnmSI_EMP_NN.Value
                        objData.FOREIGN = chkForeigner.Checked
                        SaveData(objData, gID)
                    Else
                        ExcuteScript("Resize", " ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgGridDataRate')")
                    End If

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgGridDataRate.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgGridDataRate.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgGridDataRate.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item.GetDataKeyValue("ID")))
                    Next

                    If Not rep.CheckExistInDatabase(lstDeletes, InsuranceCommonTABLE_NAME.INS_SPECIFIED_OBJECTS) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                        Return
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = LoadDataGrid(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgGridDataRate.ExportExcel(Server, Response, dtDatas, "DanhMucDoiTuongDongBH")
                        End If
                    End Using
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event Yes/No Hỏi xóa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim lst As New List(Of Decimal)
                For Each item As GridDataItem In rgGridDataRate.SelectedItems
                    lst.Add(Utilities.ObjToDecima(item.GetDataKeyValue("ID")))
                Next
                If rep.DeleteSpecifiedObjects(lst) Then
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("UpdateView")
                    UpdateControlState()
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Function & Sub"
    ''' <summary>
    ''' Update state control theo state page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgGridDataRate, True)
                    EnableControlAll(False, rdEffectiveDate, radnmSI, radnmHI, radnmUI, radnmSI_COM, radnmSI_EMP, radnmHI_COM, radnmHI_EMP, radnmUI_COM, radnmUI_EMP, radnmTNLD_BNN_COM, radnmTNLD_BNN_EMP, radnmSICK, radnmMATERNITY, radnmOFF_IN_HOUSE, radnmOFF_TOGETHER, radnmRETIRE_MALE, radnmRETIRE_FEMALE, txtSI_DATE, txtHI_DATE, radnmSI_NN, radnmSI_COM_NN, radnmSI_EMP_NN, chkForeigner)
                    ClearControlValue(rdEffectiveDate, radnmSI, radnmHI, radnmUI, radnmSI_COM, radnmSI_EMP, radnmHI_COM, radnmHI_EMP, radnmUI_COM, radnmUI_EMP, radnmTNLD_BNN_COM, radnmTNLD_BNN_EMP, radnmSICK, radnmMATERNITY, radnmOFF_IN_HOUSE, radnmOFF_TOGETHER, radnmRETIRE_MALE, radnmRETIRE_FEMALE, txtSI_DATE, txtHI_DATE, radnmSI_NN, radnmSI_COM_NN, radnmSI_EMP_NN, chkForeigner)
                Case CommonMessage.STATE_NEW
                    Utilities.EnabledGridNotPostback(rgGridDataRate, False)
                    EnableControlAll(True, rdEffectiveDate, radnmSI, radnmHI, radnmUI, radnmSI_COM, radnmSI_EMP, radnmHI_COM, radnmHI_EMP, radnmUI_COM, radnmUI_EMP, radnmTNLD_BNN_COM, radnmTNLD_BNN_EMP, radnmSICK, radnmMATERNITY, radnmOFF_IN_HOUSE, radnmOFF_TOGETHER, radnmRETIRE_MALE, radnmRETIRE_FEMALE, txtSI_DATE, txtHI_DATE, radnmSI_NN, radnmSI_COM_NN, radnmSI_EMP_NN, chkForeigner)
                    'If rgGridDataRate.SelectedItems.Count = 0 Then
                    '    ClearControlValue(rdEffectiveDate, radnmSI, radnmHI, radnmUI, radnmSI_COM, radnmSI_EMP, radnmHI_COM, radnmHI_EMP, radnmUI_COM, radnmUI_EMP, radnmTNLD_BNN_COM, radnmTNLD_BNN_EMP, radnmSICK, radnmMATERNITY, radnmOFF_IN_HOUSE, radnmOFF_TOGETHER, radnmRETIRE_MALE, radnmRETIRE_FEMALE, txtSI_DATE, txtHI_DATE, radnmSI_NN, radnmSI_COM_NN, radnmSI_EMP_NN, chkForeigner)
                    'End If
                Case CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgGridDataRate, False)
                    EnableControlAll(True, rdEffectiveDate, radnmSI, radnmHI, radnmUI, radnmSI_COM, radnmSI_EMP, radnmHI_COM, radnmHI_EMP, radnmUI_COM, radnmUI_EMP, radnmTNLD_BNN_COM, radnmTNLD_BNN_EMP, radnmSICK, radnmMATERNITY, radnmOFF_IN_HOUSE, radnmOFF_TOGETHER, radnmRETIRE_MALE, radnmRETIRE_FEMALE, txtSI_DATE, txtHI_DATE, radnmSI_NN, radnmSI_COM_NN, radnmSI_EMP_NN, chkForeigner)
                Case "Nothing"
                    Utilities.EnabledGridNotPostback(rgGridDataRate, True)
                    EnableControlAll(False, rdEffectiveDate, radnmSI, radnmHI, radnmUI, radnmSI_COM, radnmSI_EMP, radnmHI_COM, radnmHI_EMP, radnmUI_COM, radnmUI_EMP, radnmTNLD_BNN_COM, radnmTNLD_BNN_EMP, radnmSICK, radnmMATERNITY, radnmOFF_IN_HOUSE, radnmOFF_TOGETHER, radnmRETIRE_MALE, radnmRETIRE_FEMALE, txtSI_DATE, txtHI_DATE, radnmSI_NN, radnmSI_COM_NN, radnmSI_EMP_NN, chkForeigner)
            End Select
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try

    End Sub
    ''' <summary>
    ''' Save data to DB
    ''' </summary>
    ''' <param name="objData"></param>
    ''' <param name="gID"></param>
    ''' <remarks></remarks>
    Private Sub SaveData(ByVal objData As INS_SPECIFIED_OBJECTS_DTO, ByVal gID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    If rep.VALIDATE_SPECIFIED_OBJECTS(rdEffectiveDate.SelectedDate, chkForeigner.Checked, -1) Then
                        ShowMessage("Trùng ngày hiệu lực với dòng dữ liệu trước đó, vui lòng kiểm tra lại.", Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    If rep.InsertSpecifiedObjects(objData, gID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        IDSelect = gID
                        Refresh("InsertView")
                        UpdateControlState()
                        ClearControlValue(rdEffectiveDate, radnmSI, radnmHI, radnmUI, radnmSI_COM, radnmSI_EMP, radnmHI_COM, radnmHI_EMP, radnmUI_COM, radnmUI_EMP, radnmTNLD_BNN_COM, radnmTNLD_BNN_EMP, radnmSICK, radnmMATERNITY, radnmOFF_IN_HOUSE, radnmOFF_TOGETHER, radnmRETIRE_MALE, radnmRETIRE_FEMALE, txtSI_DATE, txtHI_DATE, chkForeigner)
                        rgGridDataRate.SelectedIndexes.Clear()
                        rgGridDataRate.MasterTableView.ClearSelectedItems()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
                Case CommonMessage.STATE_EDIT

                    objData.ID = Utilities.ObjToDecima(rgGridDataRate.SelectedValue)
                    Dim repcomm As New CommonRepository
                    Dim lstID As New List(Of Decimal)
                    lstID.Add(objData.ID)
                    If repcomm.CheckExistIDTable(lstID, "INS_SPECIFIED_OBJECTS", "ID") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rep.VALIDATE_SPECIFIED_OBJECTS(rdEffectiveDate.SelectedDate, chkForeigner.Checked, objData.ID) Then
                        ShowMessage("Trùng ngày hiệu lực với dòng dữ liệu trước đó, vui lòng kiểm tra lại.", Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    If rep.ModifySpecifiedObjects(objData, gID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        IDSelect = gID
                        Refresh("InsertView")
                        UpdateControlState()
                        ClearControlValue(rdEffectiveDate, radnmSI, radnmHI, radnmUI, radnmSI_COM, radnmSI_EMP, radnmHI_COM, radnmHI_EMP, radnmUI_COM, radnmUI_EMP, radnmTNLD_BNN_COM, radnmTNLD_BNN_EMP, radnmSICK, radnmMATERNITY, radnmOFF_IN_HOUSE, radnmOFF_TOGETHER, radnmRETIRE_MALE, radnmRETIRE_FEMALE, txtSI_DATE, txtHI_DATE, chkForeigner)
                        rgGridDataRate.SelectedIndexes.Clear()
                        rgGridDataRate.MasterTableView.ClearSelectedItems()
                        'rgGridDataRate.SelectedItems.
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <summary>
    ''' Xóa data
    ''' </summary>
    ''' <param name="gID"></param>
    ''' <remarks></remarks>
    Private Sub DeleteData(ByVal gID As List(Of Decimal))
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceRepository
        Try
            If rep.DeleteSpecifiedObjects(gID) Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
    ''' <summary>
    ''' Get data for combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        'Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim startTime As DateTime = DateTime.UtcNow
        'Dim rep As New InsuranceRepository
        'Try
        '    If ListComboData Is Nothing Then
        '        ListComboData = New ComboBoxDataDTO
        '        ListComboData.GET_LOCALTION = True
        '        rep.GetComboboxData(ListComboData)
        '    End If
        '    FillDropDownList(ListComboData.LIST_LOCALTION, "REGION_NAME", "ID", Common.Common.SystemLanguage, True, cboLocation.SelectedValue)
        '    _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        'Catch ex As Exception
        '    _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        '    Throw ex
        'End Try
    End Sub
    ''' <summary>
    ''' Load data from DB to grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LoadDataGrid(Optional ByVal isFull As Boolean = False)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceRepository
        Dim obj As New INS_SPECIFIED_OBJECTS_DTO
        Try
            Dim MaximumRows As Integer = 0
            Dim lstSource As List(Of INS_SPECIFIED_OBJECTS_DTO)
            SetValueObjectByRadGrid(rgGridDataRate, obj)
            Dim Sorts As String = rgGridDataRate.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    lstSource = rep.GetSpecifiedObjects(obj, rgGridDataRate.CurrentPageIndex, rgGridDataRate.PageSize, MaximumRows, Sorts)
                Else
                    lstSource = rep.GetSpecifiedObjects(obj, rgGridDataRate.CurrentPageIndex, rgGridDataRate.PageSize, MaximumRows)
                End If
            Else
                Return rep.GetSpecifiedObjects(obj).ToTable()
            End If


            rgGridDataRate.DataSource = lstSource
            rgGridDataRate.MasterTableView.VirtualItemCount = MaximumRows
            rgGridDataRate.CurrentPageIndex = rgGridDataRate.MasterTableView.CurrentPageIndex
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
#End Region

End Class