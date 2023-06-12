Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_SalaryLevel
    Inherits Common.CommonView
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Attendance/Module/PayRoll/List/" + Me.GetType().Name.ToString()

#Region "Property"
    ''' <lastupdate>
    ''' 23/08/2017 11:13
    ''' </lastupdate>
    ''' <summary>
    ''' Danh sách cấp bậc lương
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalaryLevels As List(Of SalaryLevelDTO)
        Get
            Return ViewState(Me.ID & "_SalaryLevels")
        End Get
        Set(ByVal value As List(Of SalaryLevelDTO))
            ViewState(Me.ID & "_SalaryLevels") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 23/08/2017 11:13
    ''' </lastupdate>
    ''' <summary>
    ''' Danh sách cấp bậc lương cần xóa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeleteSalaryLevels As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_DeleteSalaryLevels")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_DeleteSalaryLevels") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 23/08/2017 11:13
    ''' </lastupdate>
    ''' <summary>
    ''' Mã cấp bậc lương được chọn
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    ''' 23/08/2017 11:13
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị các control, thiết lập trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 23/08/2017 11:13
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo, khởi tạo các thiết lập giá trị cho các control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            'rgData.ClientSettings.EnablePostBackOnRowClick = True
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <lastupdate>
    ''' 23/08/2017 11:13
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo các thiết lập, giá trị cho các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarSalaryLevels
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Active, ToolbarItem.Deactive,
                                       ToolbarItem.Delete, ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            Utilities.EnableRadCombo(cboSalaryGroup, False)
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 23/08/2017 11:13
    ''' </lastupdate>
    ''' <summary>
    ''' Làm mới các thiết lập, các giá trị cho các control trên trang
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                rgData.Rebind()
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        'SelectedItemDataGridByKey(rgData, IDSelect, , rgData.CurrentPageIndex)
                        ClearControlValue(txtCode, txtName, txtRemark, cboSalaryGroup, rntxtOrders)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                        'SelectedItemDataGridByKey(rgData, IDSelect, )
                        ClearControlValue(txtCode, txtName, txtRemark, cboSalaryGroup, rntxtOrders)
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Xu ly su kien PreRender cho rad grid - click row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgData_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If (CurrentState <> CommonMessage.STATE_NEW And (rgData.SelectedItems.Count = 0 Or rgData.SelectedItems.Count > 1)) Then
                ClearControlValue(txtCode, txtName, txtRemark, cboSalaryGroup, rntxtOrders)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
    ''' <lastupdate>
    ''' 23/08/2017 11:13
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức tạo dữ liệu cho filter của rad grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable

        Dim _filter As New SalaryLevelDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, _filter)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            Using rep As New PayrollRepository
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetSalaryLevel(_filter, Sorts).ToTable
                    Else
                        Return rep.GetSalaryLevel(_filter).ToTable
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        Me.SalaryLevels = rep.GetSalaryLevel(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                    Else
                        Me.SalaryLevels = rep.GetSalaryLevel(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                    End If
                    rgData.VirtualItemCount = MaximumRows
                    rgData.DataSource = Me.SalaryLevels
                End If

            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 23/08/2017 11:13
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW

                    EnabledGridNotPostback(rgData, False)

                    txtCode.Text = ""
                    txtName.Text = ""
                    txtRemark.Text = ""
                    rntxtOrders.Text = "1"
                    txtCode.ReadOnly = False
                    txtName.ReadOnly = False
                    txtRemark.ReadOnly = False
                    rntxtOrders.ReadOnly = False
                    txtCode.Focus()

                    Utilities.EnableRadCombo(cboSalaryGroup, True)

                Case CommonMessage.STATE_NORMAL

                    EnabledGridNotPostback(rgData, True)
                    txtCode.ReadOnly = True
                    txtName.ReadOnly = True
                    txtRemark.ReadOnly = True
                    rntxtOrders.ReadOnly = True

                    txtCode.Focus()
                Case CommonMessage.STATE_EDIT

                    EnabledGridNotPostback(rgData, False)
                    Utilities.EnableRadCombo(cboSalaryGroup, True)
                    txtCode.ReadOnly = True
                    txtName.ReadOnly = False
                    txtRemark.ReadOnly = False
                    rntxtOrders.ReadOnly = False

                    txtName.Focus()
                Case CommonMessage.STATE_DELETE
                    Using rep As New PayrollRepository
                        If rep.DeleteSalaryLevel(DeleteSalaryLevels) Then
                            DeleteSalaryLevels = Nothing
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                        End If

                    End Using
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.ActiveSalaryLevel(lstDeletes, "A") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If

                    End Using

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.ActiveSalaryLevel(lstDeletes, "I") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If

                    End Using
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    ''' <lastupdate>
    ''' 23/08/2017 11:13
    ''' </lastupdate>
    ''' <summary>
    ''' Bind dữ liệu lên form edit
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("CODE", txtCode)
        dic.Add("NAME", txtName)
        dic.Add("REMARK", txtRemark)
        dic.Add("ORDERS", rntxtOrders)
        dic.Add("SAL_GROUP_ID", cboSalaryGroup)

        Utilities.OnClientRowSelectedChanged(rgData, dic)

        Try
            Dim comboBoxDataDTO As New ComboBoxDataDTO
            Using rep As New PayrollRepository
                comboBoxDataDTO.GET_SALARY_GROUP = True
                rep.GetComboboxData(comboBoxDataDTO)

                FillDropDownList(cboSalaryGroup, comboBoxDataDTO.LIST_SALARY_GROUP, "NAME", "ID", Common.Common.SystemLanguage, False)
                Dim SalaryGroupEffect As SalaryGroupDTO = rep.GetEffectSalaryGroup()
                If cboSalaryGroup.Items.Count > 0 And SalaryGroupEffect IsNot Nothing Then
                    cboSalaryGroup.SelectedValue = SalaryGroupEffect.ID
                End If
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 23/08/2017 11:13
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Command khi click item trên toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objSalaryLevel As New SalaryLevelDTO
        Dim gID As Decimal

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW

                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    CurrentState = CommonMessage.STATE_EDIT

                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE

                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next

                    Using rep As New PayrollRepository
                        If Not rep.CheckExistInDatabase(lstID, TABLE_NAME.PA_SALARY_LEVEL) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                            Return
                        End If
                    End Using


                    DeleteSalaryLevels = lstID
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "SalaryLevel")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If cboSalaryGroup.SelectedValue = "" Or cboSalaryGroup.SelectedValue Is Nothing Then
                            ShowMessage(Translate("Bạn chưa chọn thang bảng lương"), Utilities.NotifyType.Warning)
                            Return
                        End If
                        objSalaryLevel.SAL_GROUP_ID = cboSalaryGroup.SelectedValue
                        objSalaryLevel.CODE = txtCode.Text.Trim
                        objSalaryLevel.NAME = txtName.Text.Trim
                        objSalaryLevel.REMARK = txtRemark.Text.Trim
                        objSalaryLevel.ORDERS = If(rntxtOrders.Text = "", 1, Decimal.Parse(rntxtOrders.Text))

                        Using rep As New PayrollRepository
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    objSalaryLevel.ACTFLG = "A"

                                    If rep.InsertSalaryLevel(objSalaryLevel, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = gID
                                        Refresh("InsertView")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                                Case CommonMessage.STATE_EDIT
                                    objSalaryLevel.ID = rgData.SelectedValue
                                    Dim saDTO As New SalaryLevelDTO
                                    saDTO.ID = rgData.SelectedValue
                                    Using re As New PayrollRepository
                                        If re.ValidateSalaryLevel(saDTO) Then
                                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                            Exit Sub
                                        End If
                                    End Using
                                    If rep.ModifySalaryLevel(objSalaryLevel, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = objSalaryLevel.ID
                                        Refresh("UpdateView")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                            End Select

                        End Using
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData')")
                    End If
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 23/08/2017 11:13
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Button command trên ctrlMessage( Yes/no của delete, active, deactive)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes And e.ActionName = CommonMessage.TOOLBARITEM_DELETE Then
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
        Catch ex As Exception

        End Try

    End Sub
    ''' <lastupdate>
    ''' 23/08/2017 11:13
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource cho rad grid 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <lastupdate>
    ''' 23/08/2017 11:13
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện ServerValidate cho control cvalCode
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate

        Dim _validate As New SalaryLevelDTO
        Try
            Using rep As New PayrollRepository
                If CurrentState = CommonMessage.STATE_EDIT Then
                    _validate.CODE = txtCode.Text.Trim
                    _validate.ID = rgData.SelectedValue
                    args.IsValid = rep.ValidateSalaryLevel(_validate)

                Else
                    _validate.CODE = txtCode.Text.Trim
                    args.IsValid = rep.ValidateSalaryLevel(_validate)
                End If

            End Using

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 23/08/2017 11:13
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện ServerValidate cho control cvalCode
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalSalaryGroup_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalSalaryGroup.ServerValidate
        Dim rep As New PayrollRepository
        Dim validate As New SalaryGroupDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If (cboSalaryGroup.SelectedValue = "") Then
                args.IsValid = False
            Else
                If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                    validate.ID = cboSalaryGroup.SelectedItem.Value.Trim
                    validate.ACTFLG = "A"
                    args.IsValid = rep.ValidateSalaryGroup(validate)
                End If
            End If

            If Not args.IsValid Then
                LoadCombo()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' LoadCombo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New PayrollRepository
            Dim comboBoxDataDTO As New ComboBoxDataDTO
            comboBoxDataDTO.GET_SALARY_GROUP = True
            rep.GetComboboxData(comboBoxDataDTO) 'Lấy danh sách các Combo.
            If Not comboBoxDataDTO Is Nothing Then
                FillDropDownList(cboSalaryGroup, comboBoxDataDTO.LIST_SALARY_GROUP, "NAME", "ID", Common.Common.SystemLanguage, True)
            End If
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
#End Region

#Region "Custom"

#End Region
End Class