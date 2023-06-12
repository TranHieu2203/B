Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_Formula
    Inherits Common.CommonView

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Payroll\Modules\Payroll\Setting" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' vData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property vData As List(Of PAFomulerGroup)
        Get
            Return ViewState(Me.ID & "vData")
        End Get
        Set(ByVal value As List(Of PAFomulerGroup))
            ViewState(Me.ID & "vData") = value
        End Set
    End Property

    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "vIDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "vIDSelect") = value
        End Set
    End Property

    ''' <summary>
    ''' ListComboData
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

    ''' <summary>
    ''' ValueObjSalary
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueObjSalary As Decimal
        Get
            Return ViewState(Me.ID & "_ValueObjSalary")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueObjSalary") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>23/08/2017</lastupdate>
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
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            'rgData.ClientSettings.EnablePostBackOnRowClick = True
            InitControl()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>Load cac control, menubar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarSalaryGroups

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Active, ToolbarItem.Deactive,
                                       ToolbarItem.Calculate)

            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            Me.MainToolBar.Items(6).Text = "Copy Formuler"
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

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
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                        'SelectedItemDataGridByKey(rgData, IDSelect, )
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                        ClearControlValue(rcboCopyObjSalary, rcboObjSalary, rtxtNameVN, rnmIdx, txtDesc)
                        ExcuteScript("Clear", "clRadDatePicker()")
                        rgData.Rebind()
                End Select
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            EnabledGridNotPostback(rgData, True)

            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgData, True)
                    EnableControlAll(False, rtxtNameVN, rcboObjSalary, rdpStartDate, rdpEndDate, txtDesc, rtAward_Code)

                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgData, False)
                    ClearControlValue(rtxtNameVN, rcboObjSalary, rdpStartDate, rdpEndDate, txtDesc, rtAward_Code)
                    EnableControlAll(True, rtxtNameVN, rcboObjSalary, rdpStartDate, rdpEndDate, txtDesc, rtAward_Code)

                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, rtxtNameVN, rcboObjSalary, rdpStartDate, rdpEndDate, txtDesc, rtAward_Code)
            End Select

            ChangeToolbarState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            GetDataCombo()
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("OBJ_SAL_ID", rcboObjSalary)
            dic.Add("NAME_VN", rtxtNameVN)
            dic.Add("START_DATE", rdpStartDate)
            dic.Add("END_DATE", rdpEndDate)
            dic.Add("SDESC", txtDesc)
            dic.Add("IDX", rnmIdx)
            dic.Add("AWARD_CODE", rtAward_Code)
            'rtAward_Code
            Utilities.OnClientRowSelectedChanged(rgData, dic)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim objSalaryGroup As New SalaryGroupDTO
        Dim gID As Decimal

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW

                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    IDSelect = Utilities.ObjToDecima(rgData.SelectedValue)
                    CurrentState = CommonMessage.STATE_EDIT

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim rep As New PayrollRepository
                        If IsNumeric(rcboObjSalary.SelectedValue) Then
                            ListComboData = New ComboBoxDataDTO
                            ListComboData.GET_OBJECT_PAYMENT = True
                            rep.GetComboboxData(ListComboData)
                            Dim isAWARD = (From p In ListComboData.LIST_OBJECT_PAYMENT Where p.CODE.ToUpper = "AWARD" And p.ID = CDec(rcboObjSalary.SelectedValue) Select p).Count
                            If isAWARD = 1 Then
                                If String.IsNullOrEmpty(rtAward_Code.Text) Then
                                    ShowMessage(Translate("Bạn chưa nhập mã thưởng "), NotifyType.Warning)
                                    Exit Sub
                                End If
                            Else
                                rtAward_Code.ClearValue()
                            End If
                        End If
                        Dim obj As New PAFomulerGroup
                        obj.NAME_VN = rtxtNameVN.Text.Trim
                        obj.OBJ_SAL_ID = Utilities.ObjToDecima(ValueObjSalary)
                        obj.START_DATE = rdpStartDate.SelectedDate
                        obj.END_DATE = rdpEndDate.SelectedDate
                        obj.STATUS = 1
                        obj.SDESC = txtDesc.Text.Trim
                        obj.IDX = rnmIdx.Value
                        obj.AWARD_CODE = rtAward_Code.Text
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertFomulerGroup(obj, gID) Then
                                    CurrentState = CommonMessage.STATE_NEW
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                    ExcuteScript("Clear", "clRadDatePicker()")
                                    ClearControlValue(rcboCopyObjSalary, rcboObjSalary, rtxtNameVN, rnmIdx, txtDesc, rtAward_Code)
                                    rgData.Rebind()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If

                            Case CommonMessage.STATE_EDIT
                                obj.ID = rgData.SelectedValue
                                If rep.ModifyFomulerGroup(obj, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = obj.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                    ExcuteScript("Clear", "clRadDatePicker()")
                                    ClearControlValue(rcboCopyObjSalary, rcboObjSalary, rtxtNameVN, rnmIdx, txtDesc, rtAward_Code)
                                    rgData.Rebind()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "setDefaultSize()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")

                Case CommonMessage.TOOLBARTIEM_CALCULATE
                    If rgData.MasterTableView.GetSelectedItems().Length = 0 OrElse String.IsNullOrEmpty(rcboCopyObjSalary.SelectedValue) Then
                        ShowMessage(Translate(CommonMessage.CM_CTRLPA_FORMULA_IS_SELECTED_CONTENT_COPY_FORMULAR), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim repPR As New PayrollRepository
                    ListComboData = New ComboBoxDataDTO
                    Dim dto As New PAObjectSalaryDTO
                    Dim list As New List(Of PAObjectSalaryDTO)
                    dto.ID = Convert.ToDecimal(rcboCopyObjSalary.SelectedValue)
                    list.Add(dto)
                    ListComboData.GET_OBJECT_PAYMENT = True
                    ListComboData.LIST_OBJECT_PAYMENT = list

                    If Not repPR.ValidateCombobox(ListComboData) Then
                        rcboCopyObjSalary.ClearSelection()
                        repPR.GetComboboxData(ListComboData)
                        FillDropDownList(rcboCopyObjSalary, ListComboData.LIST_OBJECT_PAYMENT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, rcboCopyObjSalary.SelectedValue)
                        rcboCopyObjSalary.SelectedIndex = 0
                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.CM_CTRLPA_FORMULA_MESSAGE_BOX_CONFIRM)
                    ctrlMessageBox.ActionName = CType(e.Item, RadToolBarButton).CommandName
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_ACTIVE, CommonMessage.TOOLBARITEM_DEACTIVE, CommonMessage.TOOLBARITEM_DELETE
                    If rgData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.CM_CTRLPA_FORMULA_MESSAGE_BOX_CONFIRM)
                    ctrlMessageBox.ActionName = CType(e.Item, RadToolBarButton).CommandName
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select

            UpdateControlState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New PayrollRepository
                Dim Objdata As New PAFomulerGroup
                Objdata.ID = Utilities.ObjToDecima(rgData.SelectedValue)

                Select Case e.ActionName
                    Case CommonMessage.TOOLBARITEM_DELETE
                        CurrentState = CommonMessage.STATE_DELETE
                        If rep.DeleteFomulerGroup(Objdata) Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgData.Rebind()
                            ExcuteScript("Clear", "clRadDatePicker()")
                            ClearControlValue(rcboCopyObjSalary, rcboObjSalary, rtxtNameVN, rnmIdx, txtDesc, rtAward_Code)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If

                    Case CommonMessage.TOOLBARITEM_ACTIVE
                        CurrentState = CommonMessage.STATE_ACTIVE
                        If rep.ActiveFolmulerGroup(Objdata.ID, 1) Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgData.Rebind()
                            ExcuteScript("Clear", "clRadDatePicker()")
                            ClearControlValue(rcboCopyObjSalary, rcboObjSalary, rtxtNameVN, rnmIdx, txtDesc, rtAward_Code)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If

                    Case CommonMessage.TOOLBARITEM_DEACTIVE
                        CurrentState = CommonMessage.STATE_DEACTIVE
                        If rep.ActiveFolmulerGroup(Objdata.ID, 0) Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgData.Rebind()
                            ExcuteScript("Clear", "clRadDatePicker()")
                            ClearControlValue(rcboCopyObjSalary, rcboObjSalary, rtxtNameVN, rnmIdx, txtDesc, rtAward_Code)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If

                    Case CommonMessage.TOOLBARTIEM_CALCULATE
                        CurrentState = CommonMessage.STATE_NORMAL

                        Objdata.OBJ_SAL_ID = Utilities.ObjToDecima(rcboCopyObjSalary.SelectedValue)
                        If rep.CopyFomuler(Objdata.ID, Objdata.OBJ_SAL_ID) Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgData.Rebind()
                            ExcuteScript("Clear", "clRadDatePicker()")
                            ClearControlValue(rcboCopyObjSalary, rcboObjSalary, rtxtNameVN, rnmIdx, txtDesc, rtAward_Code)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                End Select

                UpdateControlState()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Kiem tra gia tri dang đươc chọn cua combox ĐỐI TƯỢNG LƯƠNG có tồn tại hoặc bị ngừng áp dụng hay không? 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalObjSalary_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalObjSalary.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository

        Try
            ValueObjSalary = Convert.ToDecimal(rcboObjSalary.SelectedValue)
            ListComboData = New ComboBoxDataDTO
            Dim dto As New PAObjectSalaryDTO
            Dim list As New List(Of PAObjectSalaryDTO)

            dto.ID = Convert.ToDecimal(rcboObjSalary.SelectedValue)
            list.Add(dto)

            ListComboData.GET_OBJECT_PAYMENT = True
            ListComboData.LIST_OBJECT_PAYMENT = list

            If rep.ValidateCombobox(ListComboData) Then
                args.IsValid = True
            Else
                args.IsValid = False
                rcboObjSalary.ClearSelection()
                rep.GetComboboxData(ListComboData)
                FillDropDownList(rcboObjSalary, ListComboData.LIST_OBJECT_PAYMENT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, rcboObjSalary.SelectedValue)
                rcboObjSalary.SelectedIndex = 0
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim obj As New PAFomulerGroup
        CreateDataFilter = Nothing

        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, obj)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            Using rep As New PayrollRepository
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetAllFomulerGroup(obj, Sorts).ToTable
                    Else
                        Return rep.GetAllFomulerGroup(obj).ToTable
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        Me.vData = rep.GetAllFomulerGroup(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                    Else
                        Me.vData = rep.GetAllFomulerGroup(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                    End If
                    rgData.VirtualItemCount = MaximumRows
                    rgData.DataSource = Me.vData
                End If
            End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Get data và bind vao combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository

        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_TYPE_PAYMENT = True
                ListComboData.GET_OBJECT_PAYMENT = True
                rep.GetComboboxData(ListComboData)
            End If

            FillDropDownList(rcboObjSalary, ListComboData.LIST_OBJECT_PAYMENT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, rcboObjSalary.SelectedValue)
            FillDropDownList(rcboCopyObjSalary, ListComboData.LIST_OBJECT_PAYMENT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, rcboCopyObjSalary.SelectedValue)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


#End Region

End Class