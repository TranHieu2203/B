Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlUserFunctionException
    Inherits CommonView
    Property UserInfo As CommonBusiness.UserDTO
    Protected WithEvents groupCopy As ctrlGroupCopy
    Protected WithEvents ViewItem As ViewBase
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"
    Property GridIDSelect As Decimal
        Get
            Return PageViewState(Me.ID & "_GridIDSelect")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_GridIDSelect") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' ComboboxData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ComboBoxData As ComboBoxDataDTO
        Get
            Return PageViewState(Me.ID & "_ComboBoxData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            PageViewState(Me.ID & "_ComboBoxData") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' ViewShowed
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewShowed As Boolean
        Get
            Return PageViewState(Me.ID & "_ViewShowed")
        End Get
        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_ViewShowed") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' SelectedItem
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SelectedItem() As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_SelectedItem") = value
        End Set
    End Property
    Public Property ListOrgID() As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_ListOrgID")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_ListOrgID") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Danh sách item sẽ xóa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeleteItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_DeleteItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_DeleteItemList") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' UserFunction
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserFunction As List(Of UserFunctionDTO)
        Get
            Return PageViewState(Me.ID & "_UserFunction")
        End Get
        Set(ByVal value As List(Of UserFunctionDTO))
            PageViewState(Me.ID & "_UserFunction") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' mã tài khoản cũ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserID_Old As Decimal
        Get
            Return PageViewState(Me.ID & "_UserID_Old")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_UserID_Old") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Kiểm tra trạng thái
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CheckState As UserFunctionDTO
        Get
            If PageViewState(Me.ID & "_CheckState") Is Nothing Then
                PageViewState(Me.ID & "_CheckState") = New UserFunctionDTO
            End If
            Return PageViewState(Me.ID & "_CheckState")
        End Get
        Set(ByVal value As UserFunctionDTO)
            PageViewState(Me.ID & "CheckState") = value
        End Set
    End Property
    ''' <summary>
    ''' Số lượng dòng lớn nhất
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MaximumRows As Integer
        Get
            If PageViewState(Me.ID & "_MaximumRows") Is Nothing Then
                Return 0
            End If
            Return PageViewState(Me.ID & "_MaximumRows")
        End Get
        Set(ByVal value As Integer)
            PageViewState(Me.ID & "_MaximumRows") = value
        End Set
    End Property
    '0 - normal
    '1 - User
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' isLoadPopup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property isLoadPopup As Integer
        Get
            Return PageViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            PageViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo ViewInit, khởi tạo, thiết lập các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = rtbMain
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                                ToolbarItem.Edit,
                                                ToolbarItem.Seperator,
                                                ToolbarItem.Save,
                                                ToolbarItem.Cancel,
                                                ToolbarItem.Delete)

            'Me.MainToolBar.Items.Add(Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_SYNC,
            '                                              ToolbarIcons.Sync,
            '                                              ToolbarAuthorize.None,
            '                                            Translate("Sao chép")))

            Me.MainToolBar.OnClientButtonClicking = "clientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            ViewItem = Me.Register("ctrlUserFunctionExceptionAddEdit", "Common", "ctrlUserFunctionExceptionAddEdit", "Secure")


            rgGrid.AllowCustomPaging = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Hiển thị thông tin trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateControlState()
            UpdatePageViewState()
            'Refresh()
            rgGrid.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <summary>
    ''' Bind dữ liệu cho các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'Load Combobox
            Dim rep As New CommonRepository
            If ComboBoxData Is Nothing Then
                ComboBoxData = New ComboBoxDataDTO
                ComboBoxData.GET_MODULE = True
                ComboBoxData.GET_FUNCTION_GROUP = True
                ComboBoxData.GET_WORK_PLACE = True
                rep.GetComboList(ComboBoxData)
            End If
            FillDropDownList(cboMODULE, ComboBoxData.LIST_MODULE, "NAME", "ID", Common.SystemLanguage, True, cboMODULE.SelectedValue)
            FillDropDownList(cboFunctionGroup, ComboBoxData.LIST_FUNCTION_GROUP, "NAME", "ID", Common.SystemLanguage, True)
            FillDropDownList(cboLocation, ComboBoxData.LIST_WORK_PLACE, "NAME", "ID", Common.SystemLanguage, True)

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Me.Controls.Contains(groupCopy) Then
                Me.Controls.Remove(groupCopy)
            End If
            Select Case isLoadPopup
                Case 1
                    groupCopy = Me.Register("ctrlGroupCopy", "Common", "ctrlGroupCopy", "Secure")
                    groupCopy.UserID = UserInfo.ID
                    groupCopy.BindData()
                    Me.Controls.Add(groupCopy)
                Case 3
                    phFindOrg.Controls.Clear()
                    ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                    ctrlOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                    ctrlOrgPopup.ShowCheckBoxes = True
                    If ListOrgID IsNot Nothing AndAlso ListOrgID.Count > 0 Then
                        ctrlOrgPopup.Bind_CheckedValueKeys = ListOrgID
                    End If
                    ctrlOrgPopup.IS_HadLoad = False
                    phFindOrg.Controls.Add(ctrlOrgPopup)
                    Exit Sub
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UpdateToolbarStatus()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ChangeToolbarState()
            If UserInfo Is Nothing Then
                rtbMain.Enabled = False
            Else
                rtbMain.Enabled = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái hiển thị trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UpdatePageViewState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateControlStatus()
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    If Not ViewPlaceHolder.Controls.Contains(ViewItem) Then
                        ViewPlaceHolder.Controls.Add(ViewItem)
                    End If
                    If Not UserInfo Is Nothing Then
                        ViewItem.SetProperty("UserID", UserInfo.ID)
                    End If
                    ViewItem.SetProperty("ViewShowed", True)
                    ViewItem.SetProperty("UserInfo", UserInfo)
                    ViewItem.SetProperty("GridIDSelect", GridIDSelect)
                    ViewItem.Refresh()
                    GridPlaceHolder.Visible = False
                    ViewPlaceHolder.Visible = True

                Case Else
                    ViewPlaceHolder.Controls.Clear()
                    GridPlaceHolder.Visible = True
                    ViewPlaceHolder.Visible = False
                    ViewItem.SetProperty("ViewShowed", False)
                    ViewItem.SetProperty("UserInfo", UserInfo)
                    ViewItem.SetProperty("GridIDSelect", GridIDSelect)
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UpdateControlStatus()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateToolbarStatus()
            Select Case CurrentState
                Case CommonMessage.STATE_EDIT
                    For Each dr As GridDataItem In rgGrid.Items
                        If CDec(dr.GetDataKeyValue("ID")) = GridIDSelect Then
                            For i = 3 To dr.Cells.Count - 1
                                If dr.Cells(i).Controls.Count > 0 Then
                                    Try
                                        Dim chk As CheckBox = DirectCast(dr.Cells(i).Controls(1), CheckBox)
                                        If chk IsNot Nothing Then
                                            chk.Enabled = True
                                        End If
                                    Catch ex As Exception
                                    End Try
                                    Try
                                        Dim btn As RadButton = DirectCast(dr.Cells(i).Controls(1), RadButton)
                                        If btn IsNot Nothing Then
                                            btn.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                                        End If
                                    Catch ex As Exception
                                    End Try
                                End If
                            Next
                        End If
                    Next
                Case Else
                    For Each dr As GridDataItem In rgGrid.Items
                        If CDec(dr.GetDataKeyValue("ID")) = GridIDSelect Then
                            For i = 3 To dr.Cells.Count - 1
                                If dr.Cells(i).Controls.Count > 0 Then
                                    Try
                                        Dim chk As CheckBox = DirectCast(dr.Cells(i).Controls(1), CheckBox)
                                        If chk IsNot Nothing Then
                                            chk.Enabled = False
                                        End If
                                    Catch ex As Exception
                                    End Try
                                    Try
                                        Dim btn As RadButton = DirectCast(dr.Cells(i).Controls(1), RadButton)
                                        If btn IsNot Nothing Then
                                            btn.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                                        End If
                                    Catch ex As Exception
                                    End Try
                                End If
                            Next
                        End If
                    Next
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim lstOrg = ctrlOrgPopup.ListOrgChecked
            If lstOrg IsNot Nothing AndAlso lstOrg.Count > 0 Then
                UserInfo.LST_ORG = (From p In lstOrg Select p.ID).ToList
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức làm mới các control trên trang
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If UserInfo Is Nothing Then
                rgGrid.DataSource = New List(Of UserFunctionDTO)
                Exit Sub
            End If
            If Not ViewShowed Then Exit Sub
            Dim Sorts As String = rgGrid.MasterTableView.SortExpressions.GetSortString()
            Dim filter As New UserFunctionDTO
            filter.FUNCTION_NAME = txtFUNCTION_NAME.Text.Trim
            filter.MODULE_NAME = cboMODULE.Items(cboMODULE.SelectedIndex).Text
            filter.FUNCTION_GROUP_NAME = cboFunctionGroup.Items(cboFunctionGroup.SelectedIndex).Text

            filter.WORK_PLACE_NAME = cboLocation.Items(cboLocation.SelectedIndex).Text

            'filter.LST_ORG = UserInfo.LST_ORG

            If UserID_Old = Nothing Or UserID_Old <> UserInfo.ID _
                Or filter.LST_ORG IsNot Nothing _
                Or Message = CommonMessage.ACTION_SAVED Or Message = CommonMessage.ACTION_UPDATED _
                Or UserFunction Is Nothing Then
                Dim rep As New CommonRepository
                If Sorts IsNot Nothing Then
                    UserFunction = rep.GetUserFunctionPermisionException(UserInfo.ID, filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows, Sorts)
                Else
                    UserFunction = rep.GetUserFunctionPermisionException(UserInfo.ID, filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows)
                End If
            End If

            'Đưa dữ liệu vào Grid
            rgGrid.VirtualItemCount = MaximumRows
            rgGrid.DataSource = UserFunction
            If Message <> CommonMessage.ACTION_UPDATED Then rgGrid.DataBind()

            'Thay đổi trạng thái các control
            UpdateControlStatus()
            UserID_Old = UserInfo.ID
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgGrid_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgGrid.ItemCommand
        If e.CommandName = "FindOrg" Then
            isLoadPopup = 3
            If sender IsNot Nothing Then
                Dim item = (From p In UserFunction Where p.ID = GridIDSelect).FirstOrDefault
                If ListOrgID Is Nothing Then
                    ListOrgID = New List(Of Decimal)
                End If
                If item IsNot Nothing AndAlso item.LST_ORG IsNot Nothing AndAlso item.LST_ORG.Count > 0 Then
                    ListOrgID = item.LST_ORG
                End If
            End If
            UpdateControlState()
            ctrlOrgPopup.Show()
        End If
    End Sub
#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện command khi click các command trênd toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_NEW Then
                CType(ViewItem, ctrlUserFunctionExceptionAddEdit).OnToolbar_Command(sender, e)
                CurrentState = CommonMessage.STATE_NORMAL
                'Update grid
                SelectedItem = Nothing

                Refresh(CommonMessage.ACTION_SAVED)
                'Update View
                UpdatePageViewState()
                'Gửi thông điệp cho Parent View
                Me.Send(CommonMessage.ACTION_UPDATED)
            Else
                Select Case CType(e.Item, Telerik.Web.UI.RadToolBarButton).CommandName
                    Case CommonMessage.TOOLBARITEM_CREATE
                        CurrentState = CommonMessage.STATE_NEW
                        'Update View
                        UpdatePageViewState()
                        'Gửi thông điệp cho Parent View
                        'Me.Send(CommonMessage.ACTION_UPDATING)
                    Case CommonMessage.TOOLBARITEM_EDIT
                        'If rgGrid.SelectedItems.Count = 0 Then
                        '    Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), Utilities.NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        'If rgGrid.SelectedItems.Count > 1 Then
                        '    Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), Utilities.NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        'For Each i As GridDataItem In rgGrid.SelectedItems
                        '    GridIDSelect = CDec(i.GetDataKeyValue("ID"))
                        'Next
                        'CurrentState = CommonMessage.STATE_EDIT
                        ''Update grid
                        'rgGrid.Rebind()
                        ''Update View
                        'UpdatePageViewState()
                        ''Gửi thông điệp cho Parent View
                        'Me.Send(CommonMessage.STATE_NORMAL)
                    Case CommonMessage.TOOLBARITEM_SAVE
                        'Kiểm tra đã select User chưa
                        If Not CheckRowSelected() Then
                            Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If CurrentState = CommonMessage.STATE_EDIT Then
                            Dim lst As List(Of CommonBusiness.UserFunctionDTO)
                            Dim rep As New CommonRepository
                            lst = RepareDataForUpdate()
                            If rep.UpdateUserFunctionException(lst) Then
                                Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Else
                                Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                            End If
                        End If
                        CurrentState = CommonMessage.STATE_NORMAL
                        'Update grid
                        SelectedItem = Nothing
                        Refresh(CommonMessage.ACTION_SAVED)
                        'Update View
                        UpdatePageViewState()
                        'Gửi thông điệp cho Parent View
                        Me.Send(CommonMessage.ACTION_UPDATED)
                    Case CommonMessage.TOOLBARITEM_DELETE
                        'Kiểm tra đã select User chưa
                        If Not CheckRowSelected() Then
                            Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        DeleteItemList = RepareDataForDelete()
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                        ctrlMessageBox.MessageTitle = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE_TITLE)
                        ctrlMessageBox.ActionName = CommonMessage.ACTION_DELETED
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Case CommonMessage.TOOLBARITEM_CANCEL
                        CurrentState = CommonMessage.STATE_NORMAL
                        'Update grid
                        SelectedItem = Nothing
                        rgGrid.Rebind()
                        'Update View
                        UpdatePageViewState()
                        'Gửi thông điệp cho Parent View
                        'Me.Send(CommonMessage.ACTION_UPDATED)
                    Case CommonMessage.TOOLBARITEM_SYNC
                        isLoadPopup = 1
                        UpdateControlState()
                        groupCopy.Show()
                End Select
            End If
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện OnReceiveData của user control ctrlUserFunction
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub ctrlUserFunction_OnReceiveData(ByVal sender As ViewBase, ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.FromViewID = "ctrlUserFunctionExceptionAddEdit" Then
                CurrentState = CommonMessage.STATE_NORMAL
                UpdatePageViewState()
                If e.EventData = CommonMessage.ACTION_SUCCESS Then
                    Refresh(CommonMessage.ACTION_SAVED)
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                ElseIf e.EventData = CommonMessage.ACTION_UNSUCCESS Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                Else
                    Refresh()
                End If
                Me.Send(CommonMessage.ACTION_UPDATED)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện CheckChanged của control checkbox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub Combobox_ItemChecked(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Then
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Sub CheckBox_CheckChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Then
                Dim chk = DirectCast(sender, CheckBox)
                Select Case chk.ID
                    Case "chkCREATE_ALL"
                        CheckState.ALLOW_CREATE = chk.Checked
                        DoCheck("ALLOW_CREATE", "chkCREATE", chk.Checked)
                    Case "chkMODIFY_ALL"
                        CheckState.ALLOW_MODIFY = chk.Checked
                        DoCheck("ALLOW_MODIFY", "chkMODIFY", chk.Checked)
                    Case "chkDELETE_ALL"
                        CheckState.ALLOW_DELETE = chk.Checked
                        DoCheck("ALLOW_DELETE", "chkDELETE", chk.Checked)
                    Case "chkPRINT_ALL"
                        CheckState.ALLOW_PRINT = chk.Checked
                        DoCheck("ALLOW_PRINT", "chkPRINT", chk.Checked)
                    Case "chkIMPORT_ALL"
                        CheckState.ALLOW_IMPORT = chk.Checked
                        DoCheck("ALLOW_IMPORT", "chkIMPORT", chk.Checked)
                    Case "chkEXPORT_ALL"
                        CheckState.ALLOW_EXPORT = chk.Checked
                        DoCheck("ALLOW_EXPORT", "chkEXPORT", chk.Checked)
                    Case "chkSPECIAL1_ALL"
                        CheckState.ALLOW_SPECIAL1 = chk.Checked
                        DoCheck("ALLOW_SPECIAL1", "chkSPECIAL1", chk.Checked)
                    Case "chkSPECIAL2_ALL"
                        CheckState.ALLOW_SPECIAL2 = chk.Checked
                        DoCheck("ALLOW_SPECIAL2", "chkSPECIAL2", chk.Checked)
                    Case "chkSPECIAL3_ALL"
                        CheckState.ALLOW_SPECIAL3 = chk.Checked
                        DoCheck("ALLOW_SPECIAL3", "chkSPECIAL3", chk.Checked)
                    Case "chkSPECIAL4_ALL"
                        CheckState.ALLOW_SPECIAL4 = chk.Checked
                        DoCheck("ALLOW_SPECIAL4", "chkSPECIAL4", chk.Checked)
                    Case "chkSPECIAL5_ALL"
                        CheckState.ALLOW_SPECIAL5 = chk.Checked
                        DoCheck("ALLOW_SPECIAL5", "chkSPECIAL5", chk.Checked)
                End Select
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện ButtonCommand của user control ctrlMessageBox
    ''' Xử lý khi click button OK/Cancel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New CommonRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ButtonID = MessageBoxButtonType.ButtonYes And e.ActionName = CommonMessage.ACTION_DELETED Then
                If rep.DeleteUserFunctionException(DeleteItemList) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Refresh(CommonMessage.ACTION_SAVED)
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện ItemDataBound của rad grid rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgGrid.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Header Then
                Dim chkCREATE As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_CREATE") _
                    .FindControl("chkCREATE_ALL"), CheckBox)
                Dim chkMODIFY As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_MODIFY") _
                    .FindControl("chkMODIFY_ALL"), CheckBox)
                Dim chkDELETE As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_DELETE") _
                    .FindControl("chkDELETE_ALL"), CheckBox)
                Dim chkPRINT As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_PRINT") _
                    .FindControl("chkPRINT_ALL"), CheckBox)
                Dim chkIMPORT As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_IMPORT") _
                    .FindControl("chkIMPORT_ALL"), CheckBox)
                Dim chkEXPORT As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_EXPORT") _
                    .FindControl("chkEXPORT_ALL"), CheckBox)
                Dim chkSPECIAL1 As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_SPECIAL1") _
                    .FindControl("chkSPECIAL1_ALL"), CheckBox)
                Dim chkSPECIAL2 As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_SPECIAL2") _
                    .FindControl("chkSPECIAL2_ALL"), CheckBox)
                Dim chkSPECIAL3 As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_SPECIAL3") _
                    .FindControl("chkSPECIAL3_ALL"), CheckBox)
                Dim chkSPECIAL4 As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_SPECIAL4") _
                    .FindControl("chkSPECIAL4_ALL"), CheckBox)
                Dim chkSPECIAL5 As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_SPECIAL5") _
                    .FindControl("chkSPECIAL5_ALL"), CheckBox)
                If chkCREATE IsNot Nothing Then
                    chkCREATE.Checked = CheckState.ALLOW_CREATE
                    chkCREATE.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkMODIFY IsNot Nothing Then
                    chkMODIFY.Checked = CheckState.ALLOW_MODIFY
                    chkMODIFY.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkDELETE IsNot Nothing Then
                    chkDELETE.Checked = CheckState.ALLOW_DELETE
                    chkDELETE.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkPRINT IsNot Nothing Then
                    chkPRINT.Checked = CheckState.ALLOW_PRINT
                    chkPRINT.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkIMPORT IsNot Nothing Then
                    chkIMPORT.Checked = CheckState.ALLOW_IMPORT
                    chkIMPORT.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkEXPORT IsNot Nothing Then
                    chkEXPORT.Checked = CheckState.ALLOW_EXPORT
                    chkEXPORT.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkSPECIAL1 IsNot Nothing Then
                    chkSPECIAL1.Checked = CheckState.ALLOW_SPECIAL1
                    chkSPECIAL1.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkSPECIAL2 IsNot Nothing Then
                    chkSPECIAL2.Checked = CheckState.ALLOW_SPECIAL2
                    chkSPECIAL2.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkSPECIAL3 IsNot Nothing Then
                    chkSPECIAL3.Checked = CheckState.ALLOW_SPECIAL3
                    chkSPECIAL3.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkSPECIAL4 IsNot Nothing Then
                    chkSPECIAL4.Checked = CheckState.ALLOW_SPECIAL4
                    chkSPECIAL4.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkSPECIAL5 IsNot Nothing Then
                    chkSPECIAL5.Checked = CheckState.ALLOW_SPECIAL5
                    chkSPECIAL5.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
            ElseIf e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                If SelectedItem IsNot Nothing Then
                    If SelectedItem.Contains(Decimal.Parse(datarow("ID").Text)) Then
                        e.Item.Selected = True
                    End If
                End If
                For i = 3 To datarow.Cells.Count - 1
                    If datarow.Cells(i).Controls.Count > 0 Then
                        Try
                            Dim chk As CheckBox = DirectCast(datarow.Cells(i).Controls(1), CheckBox)
                            If chk IsNot Nothing Then
                                chk.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                            End If
                        Catch ex As Exception
                        End Try
                    End If
                Next
            End If
            If (TypeOf e.Item Is GridDataItem) Then
                Dim edit = CType(e.Item, GridEditableItem)
                Dim cbo As RadComboBox
                cbo = CType(edit.FindControl("cboWorkLevel"), RadComboBox)

                If cbo IsNot Nothing Then
                    Dim rep As New CommonProcedureNew
                    Dim jobLevel = rep.GET_JOB_LEVEL_BY_TITLE(0)
                    FillRadCombobox(cbo, jobLevel, "NAME_VN", "ID")
                    If Not IsDBNull(edit.GetDataKeyValue("ID")) Then
                        Dim id = CDec(edit.GetDataKeyValue("ID"))
                        Dim objUserFunction = (From p In UserFunction Where p.ID = id).FirstOrDefault
                        If objUserFunction.LST_WL IsNot Nothing AndAlso objUserFunction.LST_WL.Count > 0 Then
                            For Each item As RadComboBoxItem In cbo.Items
                                For Each cen As Decimal In objUserFunction.LST_WL
                                    If Not item.Value = "" Then
                                        If cen = CDec(item.Value) Then
                                            item.Checked = True
                                        End If
                                    End If
                                Next
                            Next
                        End If
                    End If
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource của rad grid rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh(CommonMessage.ACTION_UPDATED)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện click khi click button btnFIND
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFIND_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFIND.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.UserFunction = Nothing
            rgGrid.CurrentPageIndex = 0
            Refresh()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien chọn nhân viên của control groupCopy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub groupCopy_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles groupCopy.GroupCopySelected
        Dim rep As New CommonProgramsRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If groupCopy.SelectedID IsNot Nothing Then
                If rep.CopyUserFunction(UserID_Old, groupCopy.SelectedID) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Refresh(CommonMessage.ACTION_SAVED)
                End If
            End If
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien nhấn nút cancel của control groupCopy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles groupCopy.CancelClicked, ctrlOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub
#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Phương phương thực hiện do check đối với item trênd rad grid rgGrid
    ''' </summary>
    ''' <param name="UniqueName"></param>
    ''' <param name="CheckBoxName"></param>
    ''' <param name="checked"></param>
    ''' <remarks></remarks>
    Private Sub DoCheck(ByVal UniqueName As String, ByVal CheckBoxName As String, ByVal checked As Boolean)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SelectedItem = New List(Of Decimal)
            For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.SelectedItems
                SelectedItem.Add(Decimal.Parse(dr("ID").Text))
                Dim _chk As CheckBox = DirectCast(dr(UniqueName).FindControl(CheckBoxName), CheckBox)
                If _chk IsNot Nothing Then
                    _chk.Checked = checked
                End If
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức check dòng được chọn
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckRowSelected()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Return rgGrid.SelectedItems.Count > 0
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return False
        End Try
    End Function
    'Private Sub rgGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgGrid.SelectedIndexChanged
    '    Dim item As GridDataItem
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        UserInfo.LST_ORG = New List(Of Decimal)
    '        UserInfo.LST_ORG.Add(1)
    '        Me.SetProperty("UserInfo", UserInfo)
    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm xử lý sửa dữ liệu khi cập nhật 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function RepareDataForUpdate() As List(Of CommonBusiness.UserFunctionDTO)
        Dim lst As New List(Of CommonBusiness.UserFunctionDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.Items
                If CDec(dr.GetDataKeyValue("ID")) = GridIDSelect Then
                    Dim item As New CommonBusiness.UserFunctionDTO
                    item.ALLOW_CREATE = If(DirectCast(dr("ALLOW_CREATE").FindControl("chkCREATE"), CheckBox).Checked, 1, 0)
                    item.ALLOW_MODIFY = If(DirectCast(dr("ALLOW_MODIFY").FindControl("chkMODIFY"), CheckBox).Checked, 1, 0)
                    item.ALLOW_DELETE = If(DirectCast(dr("ALLOW_DELETE").FindControl("chkDELETE"), CheckBox).Checked, 1, 0)
                    item.ALLOW_PRINT = If(DirectCast(dr("ALLOW_PRINT").FindControl("chkPRINT"), CheckBox).Checked, 1, 0)
                    item.ALLOW_IMPORT = If(DirectCast(dr("ALLOW_IMPORT").FindControl("chkIMPORT"), CheckBox).Checked, 1, 0)
                    item.ALLOW_EXPORT = If(DirectCast(dr("ALLOW_EXPORT").FindControl("chkEXPORT"), CheckBox).Checked, 1, 0)
                    item.ALLOW_SPECIAL1 = If(DirectCast(dr("ALLOW_SPECIAL1").FindControl("chkSPECIAL1"), CheckBox).Checked, 1, 0)
                    item.ALLOW_SPECIAL2 = If(DirectCast(dr("ALLOW_SPECIAL2").FindControl("chkSPECIAL2"), CheckBox).Checked, 1, 0)
                    item.ALLOW_SPECIAL3 = If(DirectCast(dr("ALLOW_SPECIAL3").FindControl("chkSPECIAL3"), CheckBox).Checked, 1, 0)
                    item.ALLOW_SPECIAL4 = If(DirectCast(dr("ALLOW_SPECIAL4").FindControl("chkSPECIAL4"), CheckBox).Checked, 1, 0)
                    item.ALLOW_SPECIAL5 = If(DirectCast(dr("ALLOW_SPECIAL5").FindControl("chkSPECIAL5"), CheckBox).Checked, 1, 0)
                    item.ID = GridIDSelect
                    lst.Add(item)
                    If UserInfo.LST_ORG IsNot Nothing AndAlso UserInfo.LST_ORG.Count > 0 Then
                        item.LST_ORG = UserInfo.LST_ORG
                    End If

                End If
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

        Return lst
    End Function
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm xử lý xóa lại dữ liệu
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function RepareDataForDelete() As List(Of Decimal)
        Dim lst As New List(Of Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.SelectedItems
                lst.Add(Decimal.Parse(dr.GetDataKeyValue("ID")))
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
        Return lst
    End Function
#End Region

End Class