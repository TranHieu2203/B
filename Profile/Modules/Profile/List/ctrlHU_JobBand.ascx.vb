Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_JobBand
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/List/" + Me.GetType().Name.ToString()

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
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView, la ham load du lieu, control states cua usercontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
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
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgMain.AllowCustomPaging = True
            rgMain.PageSize = Common.Common.DefaultPageSize
            rgMain.SetFilter()
            InitControl()

            If Not IsPostBack Then
                GirdConfig(rgMain)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <summary>
    ''' Load data cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            GetDataCombo()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain ' gan thuoc tinh MainToolBar(CommonView)
            'Tao menu toolbar, add cac nut them moi, sua, luu, huy, xoa, ap dung, ngung ap dung, xuat excel
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Active, ToolbarItem.Deactive)
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
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else

                Select Case Message
                    Case "UpdateView"

                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"

                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        'valSum.Visible = False
                        rgMain.MasterTableView.ClearSelectedItems()
                End Select
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    '''
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileRepository
        Dim _filter As New JobBradDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetJobBand(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts).ToTable() 'Get data DB va co sap xep
                Else
                    Return rep.GetJobBand(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts).ToTable() 'Get data DB va khong sap xep
                End If
            Else
                Dim JobBands As List(Of JobBradDTO)
                If Sorts IsNot Nothing Then
                    JobBands = rep.GetJobBand(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts) 'Get data DB phan trang va co sap xep
                Else
                    JobBands = rep.GetJobBand(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows) 'Get data DB phan trang va ko sap xep
                End If
                Dim dt As DataTable = JobBands.ToTable
                rgMain.DataSource = JobBands
                rgMain.MasterTableView.VirtualItemCount = MaximumRows
                rgMain.CurrentPageIndex = rgMain.MasterTableView.CurrentPageIndex
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung 
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGrid(rgMain, False)
                    EnableControlAll(True, txtName, ntLevelFrom, ntLevelTo, cboJobband)
                Case CommonMessage.STATE_NORMAL
                    ' cap nhat trang thai disable cac control khi o trang thai xem
                    EnabledGrid(rgMain, True)
                    EnableControlAll(False, txtName, ntLevelFrom, ntLevelTo, cboJobband)
                Case CommonMessage.STATE_EDIT
                    ' Cap nhat trang thai enable cho cac control khi an nut sua, Textbox ma visiable
                    EnabledGrid(rgMain, False)
                    EnableControlAll(True, txtName, ntLevelFrom, ntLevelTo, cboJobband)
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteJobBand(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                        ClearControlValue(txtName, txtNameEn, ntLevelFrom, ntLevelTo, cboJobband)
                        rgMain.SelectedIndexes.Clear()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_ACTIVE
                    ActiveAndDeactiveRank(-1, rep)
                    ClearControlValue(txtName, txtNameEn, ntLevelFrom, ntLevelTo, cboJobband)
                    rgMain.SelectedIndexes.Clear()
                Case CommonMessage.STATE_DEACTIVE
                    ActiveAndDeactiveRank(0, rep)
                    ClearControlValue(txtName, txtNameEn, ntLevelFrom, ntLevelTo, cboJobband)
                    rgMain.SelectedIndexes.Clear()
            End Select
            UpdateToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' Creatby: HongDX
    ''' Cdate: 26-06-2017
    ''' <summary>
    ''' Ham xu ly update trang thai Ap dung hay Ngung Ap dung 
    ''' Duoc update vao Feild: ACTFLG (= A (Ap dung), = I (ngung Ap dung))
    ''' </summary>
    ''' <param name="state">Trang thai = A hoac = I</param>
    ''' <param name="rep">Class goi ham ActiveStaffRank</param>
    ''' <remarks></remarks>
    Private Sub ActiveAndDeactiveRank(ByVal state As String, ByVal rep As ProfileRepository)
        Try
            Dim lstDeletes As New List(Of Decimal)
            For idx = 0 To rgMain.SelectedItems.Count - 1
                Dim item As GridDataItem = rgMain.SelectedItems(idx)
                lstDeletes.Add(item.GetDataKeyValue("ID"))
            Next
            'If Not rep.CheckExistInDatabase(lstDeletes, ProfileCommonTABLE_NAME.HU_STAFF_RANK) Then
            '    ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
            '    Return
            'End If
            ' Update trang thai list duoc chon tu grid
            If rep.ActiveJobBand(lstDeletes, state) Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                CurrentState = CommonMessage.STATE_NORMAL
                rgMain.Rebind()
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
            End If
        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Event"
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objJobBand As New JobBradDTO
        Dim rep As New ProfileRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    rgMain.SelectedIndexes.Clear()
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtName, txtNameEn, ntLevelFrom, ntLevelTo, cboJobband)
                    txtName.Focus()
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    ' event sua
                    txtName.Focus()
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgMain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If hidID.Value = "" Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    objJobBand = rep.GetJobBandID(Decimal.Parse(hidID.Value))
                    txtName.Text = objJobBand.NAME_VN
                    txtNameEn.Text = objJobBand.NAME_EN
                    ntLevelFrom.Text = objJobBand.LEVEL_FROM
                    ntLevelTo.Text = objJobBand.LEVEL_TO

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    ' event xoa
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ' Xoa nhieu dong tren luoi
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    ' Kiem tra ton tai ban ghi truoc khi xoa
                    'If Not rep.CheckExistInDatabase(lstDeletes, ProfileCommonTABLE_NAME.HU_JOB_BAND) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                    '    Return
                    'End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    ' event Ap Dung
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    ' event Ngung Ap dung
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If txtName.Text = "" Then
                        ShowMessage(Translate("Cấp bậc không được để trống"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If txtNameEn.Text = "" Then
                        ShowMessage(Translate("Cấp bậc EN không được để trống"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If ntLevelFrom.Text = "" Then
                        ShowMessage(Translate("Cấp từ không được để trống"), NotifyType.Warning)
                        Exit Sub
                    End If
                    'If ntLevelTo.Text = "" Then
                    '    ShowMessage(Translate("Cấp đến không được để trống"), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    'If cboJobband.Text = "" Then
                    '    ShowMessage(Translate("Nhóm cấp bậc không được để trống"), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    'If Integer.Parse(ntLevelFrom.Text) >= Integer.Parse(ntLevelTo.Text) Then
                    '    ShowMessage(Translate("Cấp từ phải nhỏ hơn cấp đến"), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    objJobBand.NAME_VN = txtName.Text
                    objJobBand.NAME_EN = txtNameEn.Text
                    objJobBand.LEVEL_FROM = ntLevelFrom.Text
                    objJobBand.LEVEL_TO = ntLevelTo.Text
                    objJobBand.STATUS = -1
                    'objJobBand.TITLE_GROUP_ID = cboJobband.SelectedValue
                    Select Case CurrentState
                        Case CommonMessage.STATE_NEW
                            ' Luu du lieu khi them moi
                            Dim validateStaff As New JobBradDTO
                            If rep.InsertJobBand(objJobBand, gID) Then
                                CurrentState = CommonMessage.STATE_NORMAL
                                IDSelect = gID
                                Refresh("InsertView")
                                UpdateControlState()
                                ClearControlValue(txtName, txtNameEn, ntLevelFrom, ntLevelTo, cboJobband)
                                rgMain.SelectedIndexes.Clear()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                        Case CommonMessage.STATE_EDIT
                            ' Luu du lieu khi sua
                            objJobBand.ID = Decimal.Parse(hidID.Value)
                            If objJobBand.ID = 0 Then
                                ShowMessage(Translate("Bạn phải chưa chọn dũ liệu"), NotifyType.Warning)
                                ClearControlValue(txtName, txtNameEn, ntLevelFrom, ntLevelTo, cboJobband)
                                Exit Sub
                            End If

                            If rep.ModifJobBand(objJobBand, gID) Then
                                CurrentState = CommonMessage.STATE_NORMAL
                                IDSelect = objJobBand.ID
                                Refresh("UpdateView")
                                UpdateControlState()
                                ClearControlValue(txtName, txtNameEn, ntLevelFrom, ntLevelTo, cboJobband)
                                rgMain.SelectedIndexes.Clear()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                    End Select

                Case CommonMessage.TOOLBARITEM_CANCEL
                    ' event huy
                    ClearControlValue(txtName, txtNameEn, ntLevelFrom, ntLevelTo, cboJobband)
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control 
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
                CurrentState = CommonMessage.STATE_DELETE
            End If
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgMain.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim dataItem As GridDataItem = DirectCast(e.Item, GridDataItem)
                Dim actflg = dataItem.GetDataKeyValue("ACTFLG")

                If actflg <> "Áp dụng" Then
                    dataItem.ForeColor = Drawing.Color.FromArgb(Int32.Parse("969696", System.Globalization.NumberStyles.AllowHexSpecifier))
                    Dim baseCss As String
                    If (e.Item.ItemType = Telerik.Web.UI.GridItemType.Item) Then
                        baseCss = "rgRow"
                    Else
                        baseCss = "rgAltRow"
                    End If
                    dataItem.CssClass = baseCss & " rgRow-alternating-item"
                End If

            End If
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
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

    ''' <summary>
    ''' Ham xu ly viec check gia tri ID, Code
    ''' Tu dong gen code khi them moi
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
    '    Dim rep As New ProfileRepository
    '    Dim _validate As New StaffRankDTO
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If CurrentState = CommonMessage.STATE_EDIT Then
    '            _validate.ID = rgMain.SelectedValue
    '            '_validate.CODE = txtCode.Text.Trim
    '            args.IsValid = rep.ValidateStaffRank(_validate) 'Check ton tai
    '        Else
    '            '_validate.CODE = txtCode.Text.Trim
    '            args.IsValid = rep.ValidateStaffRank(_validate) ' Check ton tai
    '        End If
    '        If Not args.IsValid Then
    '            'txtCode.Text = rep.AutoGenCode("CBNS", "HU_STAFF_RANK", "CODE") ' Gen ma cap nhan su moi khi them moi
    '        End If
    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub


    Private Sub rgMain_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgMain.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim id = dataItem.GetDataKeyValue("COLOR")
            dataItem.ForeColor = Drawing.Color.FromArgb(Int32.Parse(id.Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier))
        End If
    End Sub
#End Region

#Region "Custom"
    ''' <summary>
    ''' Update trang thai menu toolbar
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

    Private Sub rgMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgMain.SelectedIndexChanged

        Dim item As GridDataItem = rgMain.SelectedItems(0)
        If item.GetDataKeyValue("ID") IsNot Nothing Then
            hidID.Value = Decimal.Parse(item.GetDataKeyValue("ID"))
        End If
        If item.GetDataKeyValue("NAME_VN") IsNot Nothing Then
            txtName.Text = item.GetDataKeyValue("NAME_VN")
        End If
        If item.GetDataKeyValue("NAME_EN") IsNot Nothing Then
            txtNameEn.Text = item.GetDataKeyValue("NAME_EN")
        End If
        If item.GetDataKeyValue("LEVEL_FROM") IsNot Nothing Then
            ntLevelFrom.Text = item.GetDataKeyValue("LEVEL_FROM")
        End If
        If item.GetDataKeyValue("LEVEL_TO") IsNot Nothing Then
            ntLevelTo.Text = item.GetDataKeyValue("LEVEL_TO")
        End If
        'If item.GetDataKeyValue("TITLE_GROUP_ID") IsNot Nothing Then
        '    cboJobband.SelectedValue = CDec(item.GetDataKeyValue("TITLE_GROUP_ID"))
        'Else
        '    ClearControlValue(cboJobband)
        'End If

    End Sub
    Private Sub GetDataCombo()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileRepository
        Try
            Dim dtData As DataTable
            Using rep As New ProfileRepository

                dtData = rep.GetOtherList("HU_TITLE_GROUP", True)
                If dtData IsNot Nothing Then
                    FillRadCombobox(cboJobband, dtData, "NAME", "ID")
                End If

            End Using

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
End Class