Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlGroupUserN
    Inherits CommonView
    Protected WithEvents UserView As ViewBase
    Property GroupInfo As CommonBusiness.GroupDTO
    Property ShowInactiveUser As Boolean
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' Obj ViewShowed
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

    ''' <summary>
    ''' Obj GroupID_Old
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GroupID_Old As Decimal
        Get
            Return PageViewState(Me.ID & "_GroupID_Old")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_GroupID_Old") = value
        End Set
    End Property
    Public Property ShowInactiveUserOld As Boolean
        Get
            Return PageViewState(Me.ID & "_ShowInactiveUserOld")
        End Get
        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_ShowInactiveUserOld") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj ListUsers
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListUsers As List(Of UserDTO)
        Get
            Return PageViewState(Me.ID & "_ListUsers")
        End Get
        Set(ByVal value As List(Of UserDTO))
            PageViewState(Me.ID & "_ListUsers") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj ListUserSendMail
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListUserSendMail As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_ListUserSendMail")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_ListUserSendMail") = value
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
    ''' Obj MaximumRows1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MaximumRows1 As Integer
        Get
            If PageViewState(Me.ID & "_MaximumRows1") Is Nothing Then
                Return 0
            End If
            Return PageViewState(Me.ID & "_MaximumRows1")
        End Get
        Set(ByVal value As Integer)
            PageViewState(Me.ID & "_MaximumRows1") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj MaximumRows2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MaximumRows2 As Integer
        Get
            If PageViewState(Me.ID & "_MaximumRows2") Is Nothing Then
                Return 0
            End If
            Return PageViewState(Me.ID & "_MaximumRows2")
        End Get
        Set(ByVal value As Integer)
            PageViewState(Me.ID & "_MaximumRows2") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 26/07/2017 14:00
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
    ''' 26/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Tạo các command cho màn hình gồm thêm, lưu, hủy, xóa
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = rtbMain
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Export,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Delete)
            UserView = Me.Register("ctrlGroupUserNewEdit", "Common", "ctrlGroupUserNewEdit", "Secure")
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            rgGrid.SetFilter()
            rgGrid.AllowCustomPaging = True

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateToolbarStatus()
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    ViewData.Visible = False
                Case CommonMessage.STATE_NORMAL, ""
                    ViewData.Visible = True
            End Select
            Me.ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức ẩn hiện và gọi hàm Refresh cho các control trong tab
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UpdatePageViewState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateControlState()
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    If Not GroupInfo Is Nothing Then
                        'UserView.CurrentState = CurrentState
                        If Not ViewPlaceHolder.Controls.Contains(UserView) Then
                            ViewPlaceHolder.Controls.Add(UserView)
                        End If
                        UserView.SetProperty("ViewShowed", True)
                        UserView.SetProperty("GroupInfo", GroupInfo)
                        UserView.SetProperty("ShowInactiveUser", ShowInactiveUser)
                        UserView.Refresh(CommonMessage.ACTION_UPDATED)
                    End If
                    ViewPlaceHolder.Visible = True
                Case CommonMessage.STATE_NORMAL, ""
                    ViewPlaceHolder.Controls.Clear()
                    ViewPlaceHolder.Visible = False
                    UserView.SetProperty("ViewShowed", False)
                    UserView.SetProperty("ShowInactiveUser", ShowInactiveUser)
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý việc enable hoặc disable các control trên toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UpdateToolbarStatus()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ChangeToolbarState()
            If GroupInfo Is Nothing Then
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
    ''' 26/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Gọi hàm RefreshUser và bind lại data cho các control
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ViewShowed Then
                RefreshUser(Message)
                rgGrid.DataBind()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Bind lai du lieu cho grid rgGrid
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Protected Sub RefreshUser(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New CommonRepository
            If GroupInfo Is Nothing Then
                rgGrid.DataSource = New List(Of UserDTO)
                Exit Sub
            End If

            Dim Sorts As String = rgGrid.MasterTableView.SortExpressions.GetSortString()
            Dim _filter As New UserDTO
            _filter.IS_SHOW_INACTIVE_USER = ShowInactiveUser
            SetValueObjectByRadGrid(rgGrid, _filter)
            If GroupID_Old = Nothing OrElse GroupID_Old <> GroupInfo.ID _
                OrElse Message = CommonMessage.ACTION_SAVED _
                OrElse ListUsers Is Nothing _
                OrElse ShowInactiveUserOld <> ShowInactiveUser Then
                If Sorts IsNot Nothing Then
                    Me.ListUsers = rep.GetUserListInGroup1(GroupInfo.ID, _filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows1, Sorts)
                Else
                    Me.ListUsers = rep.GetUserListInGroup1(GroupInfo.ID, _filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows1)
                End If
            End If

            'Đưa dữ liệu vào Grid
            rgGrid.VirtualItemCount = MaximumRows1
            rgGrid.DataSource = Me.ListUsers

            GroupID_Old = GroupInfo.ID
            ShowInactiveUserOld = ShowInactiveUser
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
    ''' 26/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New CommonRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_NEW Then
                CType(UserView, ctrlGroupUserNewEdit).OnToolbar_Command(sender, e)
            End If
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdatePageViewState()
                    'Gửi thông điệp cho Parent View
                    Me.Send(CommonMessage.ACTION_UPDATING)
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.MessageTitle = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE_TITLE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DELETED
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_SENDMAIL
                    'Dim lst As List(Of UserDTO)
                    'Dim lstUserNew As String()
                    'Dim lstUserReset As String()
                    'Dim rep As New CommonRepository
                    'If GroupInfo Is Nothing Then
                    '    ShowMessage(Translate("Bạn chưa chọn nhóm tài khoản nào"), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    'lst = rep.GetUserNeedSendMail(GroupInfo.ID)
                    'lstUserNew = (From p In lst Where p.IS_CHANGE_PASS = 0 Select p.USERNAME & " - " & p.FULLNAME).ToArray
                    'lstUserReset = (From p In lst Where p.IS_CHANGE_PASS = 1 Select p.USERNAME & " - " & p.FULLNAME).ToArray
                    'ListUserSendMail = (From p In lst Select p.ID).ToList
                    'txtResult.Text = Translate("Các tài khoản dưới sẽ được gửi Email thông báo mật khẩu mới: ") & _
                    '    vbNewLine & vbNewLine & "Tài khoản tạo mới" & vbNewLine & String.Join(vbNewLine, lstUserNew) & _
                    'vbNewLine & vbNewLine & "Tài khoản reset mật khẩu" & vbNewLine & String.Join(vbNewLine, lstUserReset)
                    'rwSenMail.VisibleOnPageLoad = True
                    'rwSenMail.Visible = True
                    ''Kiểm tra xem danh sách người gửi Email có rỗng ko, rỗng Disable Send button
                    'If lstUserNew.Length + lstUserReset.Length = 0 Then
                    '    btnSend.Enabled = False
                    'Else
                    '    btnSend.Enabled = True
                    'End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Dim Sorts As String = rgGrid.MasterTableView.SortExpressions.GetSortString()
                    Dim _filter As New UserDTO
                    SetValueObjectByRadGrid(rgGrid, _filter)
                    Using xls As New ExcelCommon
                        If Sorts IsNot Nothing Then
                            dtData = rep.GetUserListInGroup(GroupInfo.ID, _filter, 0, Integer.MaxValue, 0, Sorts).ToTable()
                        Else
                            dtData = rep.GetUserListInGroup(GroupInfo.ID, _filter, 0, Integer.MaxValue, 0).ToTable()
                        End If
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgGrid.ExportExcel(Server, Response, dtData, "User")
                            Exit Sub
                        End If
                    End Using
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien OnReceiveData cua control ctrlUser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub ctrlUser_OnReceiveData(ByVal sender As IViewListener(Of ViewBase), ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.FromViewID = "ctrlGroupUserNewEdit" Then
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
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 14:00
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
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ButtonID = MessageBoxButtonType.ButtonYes And e.ActionName = CommonMessage.ACTION_DELETED Then
                If rep.DeleteUserGroup(GroupInfo.ID, DeleteItemList) Then
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
    ''' 26/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cua control rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            RefreshUser(CommonMessage.ACTION_SAVED)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 26/07/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện load trạng thái của checkbox để chuẩn bị update
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