Imports Framework.UI
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlOnlineUser
    Inherits CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Secure/" + Me.GetType().Name.ToString()


    Public Property KillItemList As List(Of String)
        Get
            Return PageViewState(Me.ID & "_KillItemList")
        End Get
        Set(ByVal value As List(Of String))
            PageViewState(Me.ID & "_KillItemList") = value
        End Set
    End Property
    ''' <summary>
    ''' Khởi tạo, load menu
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = rtbOnlineUser
            Common.BuildToolbar(rtbOnlineUser, ToolbarItem.Refresh, ToolbarItem.Kill)
            rtbOnlineUser.Items(1).Text = Translate("Kill")
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Load data from DB to grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub rgOnlineUser_NeedDataSource(sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgOnlineUser.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Refresh()
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Set datasource cho grid
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim list As New List(Of OnlineUserDTO)
            For Each item In LogHelper.OnlineUsers
                If LogHelper.CurrentUser.USERNAME.ToUpper <> "ADMIN" Then
                    If item.Value.User.USERNAME.ToUpper <> "ADMIN" Then
                        list.Add(New OnlineUserDTO With {
                            .SessionID = item.Key,
                            .FULLNAME = item.Value.User.FULLNAME,
                            .USERNAME = item.Value.User.USERNAME,
                            .EMAIL = item.Value.User.EMAIL,
                            .TELEPHONE = item.Value.User.TELEPHONE,
                            .CurrentViewDesc = item.Value.CurrentViewDesc,
                            .Status = item.Value.Status,
                            .ComputerName = item.Value.ComputerName,
                            .IP = item.Value.IP,
                            .LoginDate = item.Value.LoginDate,
                            .LoginTime = item.Value.LoginTime,
                            .LastAccessDate = item.Value.LastAccessDate,
                            .NoAccessTime = (DateTime.Now - item.Value.LastAccessDate).TotalMinutes})
                    End If
                Else
                    list.Add(New OnlineUserDTO With {
                            .SessionID = item.Key,
                            .FULLNAME = item.Value.User.FULLNAME,
                            .USERNAME = item.Value.User.USERNAME,
                            .EMAIL = item.Value.User.EMAIL,
                            .TELEPHONE = item.Value.User.TELEPHONE,
                            .CurrentViewDesc = item.Value.CurrentViewDesc,
                            .Status = item.Value.Status,
                            .ComputerName = item.Value.ComputerName,
                            .IP = item.Value.IP,
                            .LoginDate = item.Value.LoginDate,
                            .LoginTime = item.Value.LoginTime,
                            .LastAccessDate = item.Value.LastAccessDate,
                            .NoAccessTime = (DateTime.Now - item.Value.LastAccessDate).TotalMinutes})
                End If

            Next

            rgOnlineUser.DataSource = list
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_REFRESH
                    rgOnlineUser.Rebind()
                Case CommonMessage.TOOLBARITEM_KILL
                    If rgOnlineUser.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    KillItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn dừng các phiên làm việc đã chọn?")
                    ctrlMessageBox.MessageTitle = CommonMessage.MESSAGE_CONFIRM_DELETE_TITLE
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_KILL
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event Yes.No hỏi xóa
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
                    Case CommonMessage.ACTION_KILL
                        For Each SessionID In KillItemList
                            If SessionID <> Me.Session.SessionID Then
                                LogHelper.Kill(SessionID)
                            Else
                                ShowMessage("Bạn không thể dừng phiên làm việc của chính mình", Utilities.NotifyType.Warning)
                            End If
                        Next
                End Select
                rgOnlineUser.Rebind()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#Region "Custom"
    ''' <summary>
    ''' Add row muốn xóa vào list
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function RepareDataForDelete() As List(Of String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lst As New List(Of String)
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            For Each dr As Telerik.Web.UI.GridDataItem In rgOnlineUser.SelectedItems
                lst.Add(dr("SessionID").Text)
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