Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlListGroup
    Inherits CommonView
    Protected WithEvents UserView As ViewBase
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()


#Region "Property"

    ''' <summary>
    ''' Obj Group
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Group As GroupDTO
        Get
            Return PageViewState(Me.ID & "_Group")
        End Get
        Set(ByVal value As GroupDTO)
            PageViewState(Me.ID & "_Group") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj ListGroups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListGroups As List(Of GroupDTO)
        Get
            Return PageViewState(Me.ID & "_ListGroups")
        End Get
        Set(ByVal value As List(Of GroupDTO))
            PageViewState(Me.ID & "_ListGroups") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj DeleteItemList
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

    ''' <summary>
    ''' Obj MaximumRows
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

    ''' <summary>
    ''' Obj SelectedID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SelectedID As Decimal
        Get
            Return PageViewState(Me.ID & "_SelectedID")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_SelectedID") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 25/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgGrid
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = rtbMain
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, _
                                                ToolbarItem.Edit, _
                                                ToolbarItem.Seperator, _
                                                ToolbarItem.Save,
                                                ToolbarItem.Cancel,
                                                ToolbarItem.Delete, _
                                                ToolbarItem.Seperator, _
                                                ToolbarItem.Active, _
                                                ToolbarItem.Deactive)
            CType(rtbMain.Items(3), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"

            UserView = Me.Register("ctrlListGroupNewEdit", "Common", "ctrlListGroupNewEdit", "Secure")
            ViewPlaceHolder.Controls.Add(UserView)
            UserView.DataBind()

            rgGrid.SetFilter()
            rgGrid.AllowCustomPaging = True
            rgGrid.ClientSettings.EnablePostBackOnRowClick = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdatePageViewState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức enable data grid theo state
    ''' Gọi hàm cập nhật trạng thái toolbar để enable các nút trên toolbar theo state
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UpdateControlStatus()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    Utilities.EnabledGrid(rgGrid, False)
                Case CommonMessage.STATE_NORMAL, ""
                    Utilities.EnabledGrid(rgGrid, True)
            End Select
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgGrid_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rgGrid.SelectedItems.Count = 0 Or rgGrid.SelectedItems.Count > 1 Then
                Group = Nothing
                UpdatePageViewState(CommonMessage.ACTION_UPDATED)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Xet cac tuy chon gia tri cua Message
    ''' Gọi hàm để bind lại dữ liệu cho rgGrid theo Message
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Protected Sub UpdatePageViewState(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If
            UpdateControlStatus()
            UserView.CurrentState = CurrentState
            Dim gdiItem As GridDataItem = rgGrid.MasterTableView.FindItemByKeyValue("ID", SelectedID)
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    UserView.SetProperty("Group", Group)
                Case CommonMessage.STATE_EDIT
                    UserView.SetProperty("Group", Group)
            End Select
            If Message = CommonMessage.ACTION_UPDATED Or Message = CommonMessage.ACTION_SAVED Then
                UserView.UpdateControlState()
                UserView.Refresh(Message)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ReSize()
        ExcuteScript("Resize", "ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize)")
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Xet cac tuy chon gia tri cua Message
    ''' Bind lai du lieu cho grid rgGrid
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New CommonRepository
            If Not IsPostBack Or Message = CommonMessage.ACTION_UPDATED Then
                Dim Sorts As String = rgGrid.MasterTableView.SortExpressions.GetSortString()
                Dim _filter As New GroupDTO
                SetValueObjectByRadGrid(rgGrid, _filter)
                If Sorts IsNot Nothing Then
                    Me.ListGroups = rep.GetGroupList(_filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows, Sorts)
                Else
                    Me.ListGroups = rep.GetGroupList(_filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows)
                End If
                'Đưa dữ liệu vào Grid
                rgGrid.VirtualItemCount = MaximumRows
                rgGrid.DataSource = Me.ListGroups
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 25/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command thêm, sửa, lưu, hủy, xóa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdatePageViewState(CommonMessage.ACTION_SAVED)
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgGrid.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.SelectedItems
                        If dr("IS_HR_ADMIN").Text = "-1" And dr("IS_ADMIN").Text = "True" Then
                            ShowMessage("Nhóm tài khoản admin nhân sự không được xóa, chỉnh sửa", NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    Dim lstGroups As List(Of GroupDTO) = rgGrid.DataSource
                    CurrentState = CommonMessage.STATE_EDIT
                    'Group = (From p In lstGroups Where p.ID = SelectedID).SingleOrDefault
                    UpdatePageViewState(CommonMessage.ACTION_SAVED)
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.SelectedItems
                        If dr("IS_HR_ADMIN").Text = "-1" And dr("IS_ADMIN").Text = "True" Then
                            ShowMessage("Nhóm tài khoản admin nhân sự không được xóa, chỉnh sửa", NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    DeleteItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    Using ret As New CommonRepository
                        If ret.CheckExistIDTable(DeleteItemList, "SE_GROUP", "ID") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                            Exit Sub
                        End If
                    End Using
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    Using ret As New CommonRepository
                        If ret.CheckExistIDTable(DeleteItemList, "SE_GROUP", "ID") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                            Exit Sub
                        End If
                    End Using
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
            CType(UserView, ctrlListGroupNewEdit).OnToolbar_Command(sender, e)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện sau khi sự kiện OnToolbar_Command chạy xong
    ''' Show message thành công, load lại các control và data trên rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub View_OnReceiveData(ByVal sender As IViewListener(Of ViewBase), ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.FromViewID = "ctrlListGroupNewEdit" Then
                If CurrentState = CommonMessage.STATE_NEW Then
                    SelectedID = 0
                End If
                CurrentState = CommonMessage.STATE_NORMAL
                If e.EventData = CommonMessage.ACTION_SUCCESS Then
                    UserView.BindData()
                    rgGrid.Rebind()
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    rgGrid_SelectedIndexChanged(Nothing, Nothing)
                    Exit Sub
                ElseIf e.EventData = CommonMessage.ACTION_UNSUCCESS Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                End If
                UpdatePageViewState(CommonMessage.ACTION_UPDATED)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command button cua control ctrkMessageBox
    ''' Cap nhat trang thai cua cac control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New CommonRepository
        Dim _error As String = ""
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Select Case e.ActionName
                    Case CommonMessage.TOOLBARITEM_DELETE
                        If rep.DeleteGroup(DeleteItemList, _error) Then
                            If _error.Trim <> "" Then
                                ShowMessage(Translate("Các nhóm {0} còn người sử dụng, bạn hãy xóa người sử dụng của nhóm trước.", _error), NotifyType.Warning)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            End If
                            rgGrid.Rebind()
                            UserView.BindData()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & "<br />" & Translate(_error), NotifyType.Error)
                        End If
                    Case CommonMessage.TOOLBARITEM_ACTIVE
                        If rep.UpdateGroupStatus(DeleteItemList, "A") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgGrid.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & "<br />" & Translate(_error), NotifyType.Error)
                        End If
                    Case CommonMessage.TOOLBARITEM_DEACTIVE
                        If rep.UpdateGroupStatus(DeleteItemList, "I") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgGrid.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & "<br />" & Translate(_error), NotifyType.Error)
                        End If
                End Select
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện khi click chuyển trang và gán lại giá trị Selected cho grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgGrid.PageIndexChanged
        SelectedID = 0
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rgGrid
    ''' Gọi hàm Refresh
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
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện khi chọn 1 bản ghi cho rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgGrid.SelectedIndexChanged
        Dim item As GridDataItem
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_NORMAL Then
                item = CType(rgGrid.SelectedItems(0), GridDataItem)
                SelectedID = Decimal.Parse(item.GetDataKeyValue("ID").ToString)
                Group = (From p In Me.ListGroups Where p.ID = SelectedID).SingleOrDefault
                UpdatePageViewState(CommonMessage.ACTION_UPDATED)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid1_ItemCreated(ByVal sender As Object, ByVal e As GridItemEventArgs)
        If TypeOf e.Item Is GridHeaderItem Then
            Dim headerItem As GridHeaderItem = CType(e.Item, GridHeaderItem)
            Dim headerChkBox As CheckBox = CType(headerItem("cbStatus").Controls(0), CheckBox)
            headerChkBox.AutoPostBack = True
            AddHandler headerChkBox.CheckedChanged, AddressOf headerChkBox_CheckedChanged
        End If
    End Sub

    Private Sub headerChkBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Group = Nothing
        UpdatePageViewState(CommonMessage.ACTION_UPDATED)
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 25/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Tạo list gôm các ID để chuẩn bị xóa
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function RepareDataForDelete() As List(Of Decimal)
        Dim lst As New List(Of Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.SelectedItems
                lst.Add(Decimal.Parse(dr.GetDataKeyValue("ID").ToString))
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