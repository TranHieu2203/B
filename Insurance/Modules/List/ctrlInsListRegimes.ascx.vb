Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlInsListRegimes
    Inherits Common.CommonView
    Public IDSelect As Integer
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Insurance/Module/Insurance/List/" + Me.GetType().Name.ToString()

#Region "Property"
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Dữ liệu cho danh sách combobox
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Danh sách nhóm chế độ bảo hiểm
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property vLstChangeType As List(Of INS_GROUP_REGIMESDTO)
        Get
            Return ViewState(Me.ID & "_INS_CHANGE_TYPEDTO")
        End Get
        Set(ByVal value As List(Of INS_GROUP_REGIMESDTO))
            ViewState(Me.ID & "_INS_CHANGE_TYPEDTO") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad: load các thành phần trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                Me.MainToolBar = tbarOtherLists
                Me.ctrlMessageBox.Listener = Me
                Me.rgGridData.SetFilter()
                Me.rgGridData.AllowCustomPaging = True
                Refresh()
                UpdateToolbarState(CommonMessage.STATE_NORMAL)
                rgGridData.Rebind()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewInit để khởi tạo các control trên trang
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

        End Try

    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức bind data lên form thêm mới/sửa
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CurrentState = CommonMessage.STATE_NORMAL
            GetDataCombo()
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("GROUP_ARISING_TYPE_ID", rcboCHANGE_TYPE)
            dic.Add("NAME_VN", txtNAME)
            dic.Add("CODE", txtCode)
            dic.Add("DAY_OFF_SUMMARY", rnmDAY_OFF_SUMMARY)
            dic.Add("ENJOY_LEVEL", rnmENJOY_LEVEL)
            dic.Add("REGIME_SALARY_TYPE", ddlREGIME_SALARY_TYPE)
            dic.Add("REGIME_DAY_TYPE", ddlREGIME_DAY_TYPE)
            Utilities.OnClientRowSelectedChanged(rgGridData, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo thiết lập, giá trị cho các control trên trang: toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarOtherLists
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Active,
                                       ToolbarItem.Deactive,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Export)
            CType(Me.MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức làm mới các thành phần trên trang
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New InsuranceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgGridData.Rebind()
                        'SelectedItemDataGridByKey(rgGridData, IDSelect, , rgGridData.CurrentPageIndex)
                        ClearControlValue(txtNAME, txtCode, rcboCHANGE_TYPE, rnmDAY_OFF_SUMMARY, ddlREGIME_SALARY_TYPE, ddlREGIME_DAY_TYPE)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgGridData.CurrentPageIndex = 0
                        rgGridData.MasterTableView.SortExpressions.Clear()
                        rgGridData.Rebind()
                        'SelectedItemDataGridByKey(rgGridData, IDSelect, )
                        ClearControlValue(txtNAME, txtCode, rcboCHANGE_TYPE, rnmDAY_OFF_SUMMARY, ddlREGIME_SALARY_TYPE, ddlREGIME_DAY_TYPE)
                    Case "Cancel"
                        rgGridData.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState(CurrentState)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức cập nhật các trạng thái cho các control trên trang
    ''' </summary>
    ''' <param name="sState"></param>
    ''' <remarks></remarks>
    Public Overloads Sub UpdateControlState(ByVal sState As String)
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    EnableControlAll(False, txtNAME, txtCode, rcboCHANGE_TYPE, ddlREGIME_SALARY_TYPE, ddlREGIME_DAY_TYPE, rnmDAY_OFF_SUMMARY, rnmENJOY_LEVEL)
                    ClearControlValue(txtNAME, txtCode, rcboCHANGE_TYPE, ddlREGIME_SALARY_TYPE, ddlREGIME_DAY_TYPE, rnmDAY_OFF_SUMMARY, rcboCHANGE_TYPE, rnmENJOY_LEVEL)
                    EnabledGridNotPostback(rgGridData, True)
                Case CommonMessage.STATE_NEW
                    EnableControlAll(True, txtNAME, txtCode, rcboCHANGE_TYPE, ddlREGIME_SALARY_TYPE, ddlREGIME_DAY_TYPE, rnmDAY_OFF_SUMMARY, rnmENJOY_LEVEL)
                    ClearControlValue(txtNAME, txtCode, rcboCHANGE_TYPE, ddlREGIME_SALARY_TYPE, ddlREGIME_DAY_TYPE, rnmDAY_OFF_SUMMARY, rcboCHANGE_TYPE, rnmENJOY_LEVEL)
                    EnabledGridNotPostback(rgGridData, False)
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, txtNAME, txtCode, rcboCHANGE_TYPE, ddlREGIME_SALARY_TYPE, ddlREGIME_DAY_TYPE, rnmDAY_OFF_SUMMARY, rnmENJOY_LEVEL)
                    EnabledGridNotPostback(rgGridData, False)
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgGridData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgGridData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteEntitledType(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgGridData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgGridData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveEntitledType(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgGridData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgGridData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgGridData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveEntitledType(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgGridData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
            End Select
            rcboCHANGE_TYPE.Focus()
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try

    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức cập nhật trạng thái cho toolbar
    ''' </summary>
    ''' <param name="sState"></param>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState(ByVal sState As String)
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

#Region "Event"
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Command khi click item trên toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        'Dim objOrgFunction As New OrganizationDTO

        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    txtNAME.Focus()
                    UpdateControlState(CurrentState)
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgGridData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If (rgGridData.SelectedItems.Count > 1) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    txtNAME.Focus()
                    UpdateControlState(CurrentState)
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState(CurrentState)
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Call SaveData()
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgGridData')")
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần xóa."), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgGridData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgGridData.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item.GetDataKeyValue("ID")))
                    Next

                    If Not rep.CheckExistInDatabase(lstDeletes, InsuranceCommonTABLE_NAME.INS_ENTITLED_TYPE) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                        Return
                    End If

                    ctrlMessageBox.MessageText = Translate("Bạn có muốn xóa dữ liệu ?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgGridData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgGridData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = LoadDataGrid(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgGridData.ExportExcel(Server, Response, dtDatas, "DanhMucVung")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using
            End Select
            LoadDataGrid()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource cho rad grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGridData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGridData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            LoadDataGrid()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện lấy lại data trước khi có action
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function RepareDataForAction() As List(Of Decimal)
        Dim lst As New List(Of Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            For Each dr As Telerik.Web.UI.GridDataItem In rgGridData.SelectedItems
                lst.Add(dr.GetDataKeyValue("ID"))
            Next
            Return lst
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return Nothing
        End Try
    End Function
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien PreRender cho rad grid - click row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgGridData_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If (CurrentState <> CommonMessage.STATE_NEW And (rgGridData.SelectedItems.Count = 0 Or rgGridData.SelectedItems.Count > 1)) Then
                ClearControlValue(txtNAME, txtCode, rnmDAY_OFF_SUMMARY, ddlREGIME_SALARY_TYPE, ddlREGIME_DAY_TYPE, rcboCHANGE_TYPE)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
    ''' <lastupdate>
    ''' 05/09/2017 14:00
    ''' </lastupdate>
    '''<summary>
    ''' Phuong thuc khi grid duoc tao
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgGridData_ItemCreated(ByVal sender As Object, ByVal e As GridItemEventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If TypeOf e.Item Is GridHeaderItem Then
                Dim headerItem As GridHeaderItem = CType(e.Item, GridHeaderItem)
                Dim headerChkBox As CheckBox = CType(headerItem("cbStatus").Controls(0), CheckBox)
                headerChkBox.AutoPostBack = True
                AddHandler headerChkBox.CheckedChanged, AddressOf headerChkBox_CheckedChanged
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 05/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc khi check vao check box tren header
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub headerChkBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If sender.Checked And rgGridData.SelectedItems.Count > 1 Then
                ClearControlValue(txtNAME, txtCode, rcboCHANGE_TYPE, rnmDAY_OFF_SUMMARY, ddlREGIME_SALARY_TYPE, ddlREGIME_DAY_TYPE)
                rnmDAY_OFF_SUMMARY.ClearValue()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện ButtunCommand trên ctrlMessage
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
                UpdateControlState(CurrentState)
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState(CurrentState)
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState(CurrentState)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 14/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly viec check gia tri ID, Code
    ''' Tu dong gen code khi the
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cusCHANGE_TYPE_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusCHANGE_TYPE.ServerValidate
        Dim rep As New InsuranceRepository
        Dim _validate As New INS_GROUP_REGIMESDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                If (rcboCHANGE_TYPE.SelectedValue <> "") Then
                    _validate.ID = rcboCHANGE_TYPE.SelectedValue
                    _validate.REGIMES_NAME = rcboCHANGE_TYPE.Text.Trim
                    args.IsValid = rep.ValidateGroupRegime(_validate)
                Else
                    args.IsValid = False
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Function & Sub"
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện lưu lại dữ liệu 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim gId As Decimal
            Dim rep As New InsuranceRepository
            Dim objData As New INS_ENTITLED_TYPE_DTO
            objData.GROUP_ARISING_TYPE_ID = Utilities.ObjToDecima(rcboCHANGE_TYPE.SelectedValue)
            objData.NAME_VN = Utilities.ObjToString(txtNAME.Text.Trim.ToString)

            objData.CODE = Utilities.ObjToString(txtCode.Text.Trim.ToString)

            objData.DAY_OFF_SUMMARY = Utilities.ObjToDecima(rnmDAY_OFF_SUMMARY.Value)
            objData.ENJOY_LEVEL = Utilities.ObjToDecima(rnmENJOY_LEVEL.Value)
            objData.REGIME_SALARY_TYPE = Utilities.ObjToDecima(ddlREGIME_SALARY_TYPE.SelectedValue)
            objData.REGIME_DAY_TYPE = Utilities.ObjToDecima(ddlREGIME_DAY_TYPE.SelectedValue)
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    objData.STATUS = "A"
                    If rep.InsertEntitledType(objData, gId) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        IDSelect = gId
                        Refresh("InsertView")
                        UpdateControlState(CurrentState)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
                Case CommonMessage.STATE_EDIT
                    objData.ID = Utilities.ObjToDecima(rgGridData.SelectedValue)
                    'Dim ins_group As New INS_GROUP_REGIMESDTO
                    'ins_group.ACTFLG = "A"
                    'ins_group.ID = objData.ID
                    'Using re As New InsuranceRepository
                    '    If re.ValidateGroupRegime(ins_group) Then
                    '        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                    '        Exit Sub
                    '    End If
                    'End Using
                    If rep.ModifyEntitledType(objData, gId) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        IDSelect = objData.ID
                        Refresh("UpdateView")
                        UpdateControlState(CurrentState)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
#End Region

#Region "Custorms"
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức load dữ liệu cho rad grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LoadDataGrid(Optional ByVal isFull As Boolean = False)
        Dim rep As New InsuranceRepository
        Dim obj As New INS_ENTITLED_TYPE_DTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer = 0
            SetValueObjectByRadGrid(rgGridData, obj)
            Dim lstSource As List(Of INS_ENTITLED_TYPE_DTO)
            Dim Sorts As String = rgGridData.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    lstSource = rep.GetEntitledType(obj, rgGridData.CurrentPageIndex, rgGridData.PageSize, MaximumRows, Sorts)
                Else
                    lstSource = rep.GetEntitledType(obj, rgGridData.CurrentPageIndex, rgGridData.PageSize, MaximumRows)
                End If
            Else
                Return rep.GetEntitledType(obj).ToTable()
            End If

            rgGridData.DataSource = lstSource
            rgGridData.MasterTableView.VirtualItemCount = MaximumRows
            rgGridData.CurrentPageIndex = rgGridData.MasterTableView.CurrentPageIndex
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Get dữ liệu cho combobox nhóm chế độ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_LIST_GROUP_REGIMES = True ' Nhóm chế độ 
                rep.GetComboboxData(ListComboData)
            End If
            rcboCHANGE_TYPE.ClearValue()
            FillDropDownList(rcboCHANGE_TYPE, ListComboData.LIST_LIST_GROUP_REGIMES, "REGIMES_NAME", "ID", Common.Common.SystemLanguage, True, rcboCHANGE_TYPE.SelectedValue)
            vLstChangeType = ListComboData.LIST_LIST_GROUP_REGIMES

            Dim dtData = rep.GetOtherList("REGIME_SALARY_TYPE", False)
            FillRadCombobox(ddlREGIME_SALARY_TYPE, dtData, "NAME", "ID", False)

            dtData = rep.GetOtherList("REGIME_DAY_TYPE", False)
            FillRadCombobox(ddlREGIME_DAY_TYPE, dtData, "NAME", "ID", False)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
#End Region


End Class