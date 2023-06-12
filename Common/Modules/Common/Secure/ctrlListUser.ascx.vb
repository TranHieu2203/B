Imports System.IO
Imports Aspose.Cells
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog


Public Class ctrlListUser
    Inherits CommonView
    Protected WithEvents UserView As ViewBase
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Common/Secure/" + Me.GetType().Name.ToString()
    Dim store As New CommonProcedureNew
    Dim user1 As UserLog

#Region "Property"

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_NAME", GetType(String))
                dt.Columns.Add("EFFECT_DATE", GetType(String))
                dt.Columns.Add("EMAIL", GetType(String))
                dt.Columns.Add("TELEPHONE", GetType(String))
                dt.Columns.Add("ACTFLG", GetType(String))
                dt.Columns.Add("IS_LOGIN", GetType(String))
                dt.Columns.Add("EXPIRE_DATE", GetType(String))
                dt.Columns.Add("USERNAME", GetType(String))
                dt.Columns.Add("PASSWORD", GetType(String))
                dt.Columns.Add("IS_AD", GetType(String))
                dt.Columns.Add("IS_APP", GetType(String))
                dt.Columns.Add("IS_PORTAL", GetType(String))
                dt.Columns.Add("NO", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property
    Property User As UserDTO
        Get
            Return PageViewState(Me.ID & "_User")
        End Get
        Set(ByVal value As UserDTO)
            PageViewState(Me.ID & "_User") = value
        End Set
    End Property

    Public Property ListUsers As List(Of UserDTO)
        Get
            Return PageViewState(Me.ID & "_ListUsers")
        End Get
        Set(ByVal value As List(Of UserDTO))
            PageViewState(Me.ID & "_ListUsers") = value
        End Set
    End Property

    Public Property ListGroupCombo As List(Of GroupDTO)
        Get
            Return PageViewState(Me.ID & "_ListGroupCombo")
        End Get
        Set(ByVal value As List(Of GroupDTO))
            PageViewState(Me.ID & "_ListGroupCombo") = value
        End Set
    End Property

    Public Property DeleteItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_DeleteItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_DeleteItemList") = value
        End Set
    End Property


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
    ''' <summary>
    ''' Khởi tạo, load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgGrid.AllowCustomPaging = True
            rgGrid.SetFilter()
            rgGrid.ClientSettings.EnablePostBackOnRowClick = True
            UpdatePageViewState()
            If store.CHECK_EXIST_SE_CONFIG("IS_HIDE_AD_USER") = -1 Then
                rgGrid.MasterTableView.GetColumn("IS_AD").Visible = False
            Else
                rgGrid.MasterTableView.GetColumn("IS_AD").Visible = True

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo, load menu
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = rtbMain
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save)
            Me.MainToolBar.Items.Add(Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_CANCEL,
                                                                ToolbarIcons.Cancel,
                                                                ToolbarAuthorize.Create,
                                                                Translate("Hủy")))
            Me.MainToolBar.Items.Add(Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_DELETE,
                                                                ToolbarIcons.Delete,
                                                                ToolbarAuthorize.Delete,
                                                                Translate("Xóa")))
            Me.MainToolBar.Items.Add(Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_LOCK,
                                                                ToolbarIcons.Lock,
                                                                ToolbarAuthorize.Special2,
                                                                Translate("Khóa")))
            Me.MainToolBar.Items.Add(Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_UNLOCK,
                                                                ToolbarIcons.Unlock,
                                                                ToolbarAuthorize.Special2,
                                                                Translate("Mở khóa")))
            Me.MainToolBar.Items.Add(Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_EXPORT,
                                                                ToolbarIcons.Export,
                                                                ToolbarAuthorize.Export,
                                                                Translate("Xuất excel")))
            Me.MainToolBar.Items.Add(Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE,
                                                                ToolbarIcons.Export,
                                                                ToolbarAuthorize.Print,
                                                                Translate("Xuất file mẫu")))
            Me.MainToolBar.Items.Add(Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_IMPORT,
                                                                ToolbarIcons.Import,
                                                                ToolbarAuthorize.Import,
                                                                Translate("Nhập excel")))
            If store.CHECK_EXIST_SE_CONFIG("IS_HIDE_BUTTON_RESET") <> -1 Then
                Me.MainToolBar.Items.Add(Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_RESET,
                                                                     ToolbarIcons.Reset,
                                                                     ToolbarAuthorize.Special1,
                                                                     CommonMessage.AUTHORIZE_RESET))
            End If

            Me.MainToolBar.Items.Add(Common.CreateToolbarItem("SEND_MAIL_ACC",
                                                                     ToolbarIcons.SendMail,
                                                                     ToolbarAuthorize.Export,
                                                                     Translate("Gửi mail")))

            ''anhvn  2020/09/21
            Me.MainToolBar.Items.Add(Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_SYNC,
                                                          ToolbarIcons.Sync,
                                                          ToolbarAuthorize.Special3,
                                                        Translate("Đồng bộ")))

            'MainToolBar.Items(11).Text = Translate("Xuất file mẫu")
            CType(rtbMain.Items(2), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            UserView = Me.Register("ctrlListUserNewEdit", "Common", "ctrlListUserNewEdit", "Secure")
            ViewPlaceHolder.Controls.Add(UserView)
            UserView.DataBind()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' ResizeSpliter về trạng thái ban đầu
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub ReSize()
        ExcuteScript("Resize", "ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize)")
    End Sub
    ''' <summary>
    ''' ResizeSpliter khi show validate
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub ReSizeSpliter()
        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgGrid')")
    End Sub

    ''' <summary>
    ''' Load, Reload grid
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim MaximumRows As Integer
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New CommonRepository
            If Message = CommonMessage.ACTION_UPDATED Then
                Dim Sorts As String = rgGrid.MasterTableView.SortExpressions.GetSortString()
                Dim _filter As New UserDTO
                SetValueObjectByRadGrid(rgGrid, _filter)
                If Sorts IsNot Nothing Then
                    Me.ListUsers = rep.GetUserList(_filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows, Sorts)
                Else
                    Me.ListUsers = rep.GetUserList(_filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows)
                End If
                'Đưa dữ liệu vào Grid
                rgGrid.VirtualItemCount = MaximumRows
                rgGrid.DataSource = Me.ListUsers
                rgGrid.MasterTableView.DataSource = Me.ListUsers

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Set trạng thái control theo trạng thái page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    'Utilities.EnabledGrid(rgGrid, False)
                    rgGrid.Enabled = False
                    'rgGrid.ClientSettings.Selecting.AllowRowSelect = False
                Case CommonMessage.STATE_NORMAL, ""
                    Utilities.EnabledGrid(rgGrid, True)
            End Select
            ChangeToolbarState()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' set trạng thái page
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
            UpdateControlState()
            UserView.CurrentState = CurrentState
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    UserView.SetProperty("User", User)
                Case CommonMessage.STATE_EDIT
                    UserView.SetProperty("User", User)
            End Select
            'If Message = CommonMessage.ACTION_UPDATED Then
            '    UserView.UpdateControlState()
            '    UserView.Refresh()
            'End If
            'If Message = CommonMessage.ACTION_CANCEL Then
            '    User = Nothing
            '    UserView.Refresh()
            '    rgGrid.SelectedIndexes.Clear()
            'End If
            If Message = CommonMessage.ACTION_UPDATED Or Message = CommonMessage.ACTION_SAVED Then
                UserView.UpdateControlState()
                UserView.Refresh(Message)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <summary>
    ''' Event click item trên menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                'Case "RESET_PASSWORD"
                '    If rgGrid.SelectedItems.Count = 0 Then
                '        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                '        Exit Sub
                '    End If
                '    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_RESET_USER)
                '    ctrlMessageBox.ActionName = "RESET_SE_USER"
                '    ctrlMessageBox.DataBind()
                '    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdatePageViewState(CommonMessage.ACTION_UPDATED)
                    rgGrid.SelectedIndexes.Clear()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgGrid.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    'User = (From p In ListUsers Where p.ID = rgGrid.SelectedValue).SingleOrDefault
                    UpdatePageViewState(CommonMessage.ACTION_UPDATED)
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
                Case CommonMessage.TOOLBARITEM_LOCK
                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_LOCK)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_LOCKED
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_UNLOCK
                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_UNLOCK)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_UNLOCKED
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_RESET
                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn Reset lại mật khẩu?")
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_RESET
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case "SEND_MAIL_ACC"
                    DeleteItemList = RepareDataForDelete()
                    sendmailAcc("new")
                Case CommonMessage.TOOLBARITEM_SYNC
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_SYNC)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_SYNC
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    UpdatePageViewState(CommonMessage.ACTION_CANCEL)
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim rep As New CommonRepository
                    Dim dtData As DataTable
                    Dim Sorts As String = rgGrid.MasterTableView.SortExpressions.GetSortString()
                    Dim _filter As New UserDTO
                    SetValueObjectByRadGrid(rgGrid, _filter)
                    If Sorts IsNot Nothing Then
                        dtData = rep.GetUserList(_filter, Sorts).ToTable()
                    Else
                        dtData = rep.GetUserList(_filter).ToTable()
                    End If

                    Using xls As New ExcelCommon
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgGrid.ExportExcel(Server, Response, dtData, "Title")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    ExportTemplate("Common\Approve\Import_Taikhoan.xls", _
                                              Nothing, Nothing, _
                                              "Import_Taikhoan" & Format(Date.Now, "yyyyMMdd"))
            End Select
            CType(UserView, ctrlListUserNewEdit).OnToolbar_Command(sender, e)
            _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load grid theo trạng thái page
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub ctrlUser_OnReceiveData(ByVal sender As IViewListener(Of ViewBase), ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.FromViewID = "ctrlListUserNewEdit" Then
                If CurrentState = CommonMessage.STATE_NEW Then
                    SelectedID = 0
                    rgGrid.MasterTableView.SortExpressions.AddSortExpression("CREATED_DATE DESC")
                End If
                CurrentState = CommonMessage.STATE_NORMAL
                If e.EventData = CommonMessage.ACTION_SUCCESS Then
                    rgGrid.Rebind()
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    rgGrid_SelectedIndexChanged(Nothing, Nothing)
                    'Exit Sub
                ElseIf e.EventData = CommonMessage.ACTION_UNSUCCESS Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                ElseIf e.EventData = CommonMessage.MESSAGE_WARNING_EXIST_DATABASE Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                End If
                UpdatePageViewState(CommonMessage.ACTION_UPDATED)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event Yes/No hỏi xóa, khóa tài khoản, mở khóa tài khoản, Reset
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New CommonRepository
        Dim _error As String = ""
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Select Case e.ActionName
                    Case CommonMessage.ACTION_DELETED
                        If rep.DeleteUser(DeleteItemList, _error) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgGrid.Rebind()
                            ClearControlValue(txtResult)

                            UserView.SetProperty("User", Nothing)
                            UserView.Refresh()
                            rgGrid.SelectedIndexes.Clear()
                        Else
                            ShowMessage(Translate(_error), NotifyType.Warning)
                        End If
                    Case "RESET_SE_USER"

                        Dim UserID = String.Join(",", (From p In rgGrid.SelectedItems Select p.GetDataKeyValue("ID")).ToArray)
                        If rep.SE_RESET_USER(UserID) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                        rgGrid.SelectedIndexes.Clear()
                    Case CommonMessage.ACTION_LOCKED
                        If rep.UpdateUserListStatus(DeleteItemList, "I") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgGrid.Rebind()
                            UserView.SetProperty("User", Nothing)
                            UserView.Refresh()
                            rgGrid.SelectedIndexes.Clear()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If
                    Case CommonMessage.ACTION_UNLOCKED
                        If rep.UpdateUserListStatus(DeleteItemList, "A") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgGrid.Rebind()
                            UserView.SetProperty("User", Nothing)
                            UserView.Refresh()
                            rgGrid.SelectedIndexes.Clear()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If
                    Case CommonMessage.ACTION_RESET
                        If rep.ResetUserPassword(DeleteItemList,
                                                 CommonConfig.PasswordLength,
                                                 CommonConfig.PasswordLowerChar,
                                                 CommonConfig.PasswordUpperChar,
                                                 CommonConfig.PasswordNumberChar,
                                                 CommonConfig.PasswordSpecialChar,
                                                CommonConfig.PasswordConfig,
                                                CommonConfig.PasswordDefault,
                                                CommonConfig.PasswordDefaultText) Then
                            If sendmailAcc("reset") = 0 Then
                                ShowMessage(Translate("Gửi mail thất bại"), NotifyType.Warning)
                            End If

                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)

                            rgGrid.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If
                    Case CommonMessage.ACTION_SYNC
                        Dim _lstNew As String = ""
                        Dim _lstModify As String = ""
                        Dim _lstDelete As String = ""
                        If rep.SyncUserList(_lstNew, _lstModify, _lstDelete) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            txtResult.Height = Unit.Pixel(150)
                            txtResult.Text = "Các tài khoản thêm mới: " & vbNewLine
                            txtResult.Text &= _lstNew
                            txtResult.Text &= vbNewLine & vbNewLine & "Các tài khoản thay đổi thông tin: " & vbNewLine
                            txtResult.Text &= _lstModify
                            txtResult.Text &= vbNewLine & vbNewLine & "Các tài khoản bị khóa vì không tồn tại: " & vbNewLine
                            txtResult.Text &= _lstDelete
                            rwMessage.VisibleOnPageLoad = True
                            rwMessage.Visible = True
                            rwMessage.Height = Unit.Pixel(500)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If
                End Select
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Function sendmailAcc(ByVal action As String) As Decimal
        Try
            Dim rep As New CommonRepository
            Dim dataMail As List(Of String)
            'Dim dtValues As DataTable
            Dim body As String = ""
            Dim mail As List(Of String)
            Dim mailCC As String = ""
            Dim titleMail As String = ""
            Dim bodyNew As String = ""
            For Each item In DeleteItemList
                mail = rep.Get_user_info(item)
                If action = "reset" Then
                    dataMail = rep.GET_MAIL_TEMPLATE("RESET_MK", "Common")
                Else
                    dataMail = rep.GET_MAIL_TEMPLATE("ACCOUNT_INFO", "Common")
                End If


                body = dataMail(0)
                titleMail = dataMail(2)

                Dim values(mail.Count - 2) As String
                If mail.Count > 0 Then
                    For i As Integer = 0 To mail.Count - 2
                        values(i) = mail(i)
                    Next
                Else
                    ShowMessage(Translate("Chưa có thông tin,Bạn vui lòng kiểm tra lại"), NotifyType.Warning)
                    Exit Function
                End If
                bodyNew = String.Format(body, values)
                'If Not Common.sendEmailByServerMail(mail(2),
                '                                         If(dataMail(1) IsNot Nothing, dataMail(1), mail(2)),
                '                                          titleMail, bodyNew, String.Empty) Then
                '    ShowMessage(Translate("Gửi mail thất bại"), NotifyType.Warning)
                '    Return 0
                '    Exit Function
                'Else
                '    ShowMessage(Translate("Gửi mail thành công"), NotifyType.Success)
                '    Return -1
                '    Exit Function
                'End If
                Dim defaultFrom = CommonConfig.dicConfig("MailFrom")
                If defaultFrom = "" Then
                    ShowMessage(Translate("Chưa thiết lập MailFrom"), NotifyType.Success)
                    Return False
                End If
                If rep.InsertMail(defaultFrom, mail(2), titleMail, bodyNew, If(dataMail(1) IsNot Nothing, dataMail(1), ""), "", "", False, 1) Then
                    ShowMessage(Translate("Gửi mail thành công"), NotifyType.Success)
                    Return -1
                Else
                    ShowMessage(Translate("Gửi mail thất bại"), NotifyType.Warning)
                    Return 0
                End If
            Next
            rgGrid.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgGrid.DataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If SelectedID = 0 Then
                If (rgGrid.MasterTableView.Items.Count > 0) Then
                    'rgGrid.MasterTableView.Items(0).Selected = True
                    rgGrid_SelectedIndexChanged(Nothing, Nothing)
                End If
            End If
            Dim gdiItem As GridDataItem = rgGrid.MasterTableView.FindItemByKeyValue("ID", SelectedID)
            If gdiItem IsNot Nothing Then
                gdiItem.Selected = True
                rgGrid_SelectedIndexChanged(Nothing, Nothing)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load datasource cho grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh(CommonMessage.ACTION_UPDATED)
            _mylog.WriteLog(_mylog._info, _classPath, method,
                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event thay đổi trang hiển thị, set lại dòng được chọn là dòng đầu trang
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub rgGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgGrid.PageIndexChanged
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        SelectedID = 0
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    ''' <summary>
    ''' Event chọn vào row trên grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgGrid.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim item As GridDataItem
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_NORMAL Then
                If rgGrid.SelectedItems.Count > 0 Then
                    item = CType(rgGrid.SelectedItems(0), GridDataItem)
                    SelectedID = Decimal.Parse(item.GetDataKeyValue("ID").ToString)
                    User = (From p In Me.ListUsers Where p.ID = SelectedID).SingleOrDefault
                    UpdatePageViewState(CommonMessage.ACTION_UPDATED)
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    'Private Sub rgGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgGrid.ItemDataBound
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If TypeOf e.Item Is GridDataItem Then
    '            Dim item As GridDataItem = CType(e.Item, GridDataItem)
    '            Dim chkbx As CheckBox = CType(item("IS_AD").Controls(0), CheckBox)
    '            Dim strtxt As String = item("ItemTypeName").Text.ToString()

    '            If strtxt = "IS_AD" Then
    '                chkbx.Visible = False
    '            End If
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method,
    '                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try

    'End Sub
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        user1 = LogHelper.GetUserLog
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            TableMapping(dsDataPrepare.Tables(0))
            For Each rows As DataRow In dsDataPrepare.Tables(0).Rows
                If String.IsNullOrEmpty(rows("EMPLOYEE_CODE").ToString) AndAlso String.IsNullOrEmpty(rows("USERNAME").ToString) AndAlso String.IsNullOrEmpty(rows("EFFECT_DATE").ToString) Then Continue For
                Dim newRow As DataRow = dtData.NewRow
                newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
                newRow("EMPLOYEE_NAME") = rows("EMPLOYEE_NAME")
                newRow("EFFECT_DATE") = rows("EFFECT_DATE")
                newRow("EXPIRE_DATE") = rows("EXPIRE_DATE")
                newRow("USERNAME") = rows("USERNAME")
                newRow("PASSWORD") = rows("PASSWORD")
                newRow("IS_AD") = rows("IS_AD")
                newRow("IS_APP") = rows("IS_APP")
                newRow("IS_PORTAL") = rows("IS_PORTAL")
                newRow("NO") = rows("NO")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, True)
                DocXml = sw.ToString
                Dim sp As New CommonProcedureNew
                If sp.IMPORT_ACCOUNT(DocXml, user1) = 1 Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                    rgGrid.Rebind()
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Function loadToGrid() As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim rep As New CommonProcedureNew
            Dim rep2 As New CommonRepository
            dtError = dtData.Clone
            For Each row As DataRow In dtData.Rows
                Dim checkEmp As New DataTable
                checkEmp = rep.GET_EMPLOYEE_BY_CODE(row("EMPLOYEE_CODE"))
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)
                If Not IsDBNull(row("EMPLOYEE_CODE")) AndAlso Not String.IsNullOrEmpty(row("EMPLOYEE_CODE")) Then
                    sError = "Nhân viên không tồn tại"
                    Dim lstEmpCode As New List(Of String)
                    lstEmpCode.Add("'" + row("EMPLOYEE_CODE") + "'")
                    If checkEmp Is Nothing OrElse checkEmp.Rows.Count = 0 Then
                        ImportValidate.IsValidEmail("EMPLOYEE_CODE", row, rowError, isError, sError)
                    ElseIf Not rep2.CheckExistValue(lstEmpCode, "SE_USER", "EMPLOYEE_CODE") Then
                        sError = "Mã nhân viên đã tồn tại"
                        ImportValidate.IsValidTime("EMPLOYEE_CODE", row, rowError, isError, sError)
                    Else
                        row("EMPLOYEE_ID") = checkEmp.Rows(0)("ID")
                        row("EMAIL") = checkEmp.Rows(0)("WORK_EMAIL")
                        row("EMPLOYEE_NAME") = checkEmp.Rows(0)("FULLNAME_VN")
                        row("TELEPHONE") = checkEmp.Rows(0)("MOBILE_PHONE")
                        row("ACTFLG") = "A"
                        row("IS_LOGIN") = "-1"
                    End If
                End If
                sError = "Chưa nhập tài khoản"
                ImportValidate.EmptyValue("USERNAME", row, rowError, isError, sError)
                sError = "Chưa nhập mật khẩu"
                ImportValidate.EmptyValue("PASSWORD", row, rowError, isError, sError)
                sError = "Chưa nhập họ tên"
                ImportValidate.EmptyValue("EMPLOYEE_NAME", row, rowError, isError, sError)
                If Not String.IsNullOrEmpty(row("PASSWORD")) Then
                    Dim EncryptData As New Framework.UI.EncryptData
                    row("PASSWORD") = EncryptData.EncryptString(row("PASSWORD"))
                End If
                Dim lst As New List(Of String)
                lst.Add("'" + row("USERNAME") + "'")
                If Not rep2.CheckExistValue(lst, "SE_USER", "USERNAME") Then
                    sError = "Tài khoản đã tồn tại"
                    ImportValidate.IsValidTime("USERNAME", row, rowError, isError, sError)
                End If
                If row("IS_AD").ToString = "" AndAlso row("IS_APP").ToString = "" AndAlso row("IS_PORTAL").ToString = "" Then
                    sError = "Chưa nhập loại tài khoản"
                    ImportValidate.EmptyValue("IS_AD", row, rowError, isError, sError)
                Else
                    Dim dtcheck = rep.GET_ACCCOUNT_INFO(row("EFFECT_DATE")).Rows(0)
                    If row("IS_APP").ToString = "-1" Then
                        If dtcheck("COUNT_APP_SET") <= dtcheck("COUNT_APP") Then
                            sError = "Số lượng tài khoản App đăng ký đã vượt giới hạn cho phép"
                            ImportValidate.IsValidTime("IS_APP", row, rowError, isError, sError)
                        End If
                    Else
                        row("IS_APP") = "0"
                    End If

                    If row("IS_PORTAL").ToString = "-1" Then
                        If dtcheck("COUNT_PORTAL_SET") <= dtcheck("COUNT_PORTAL") Then
                            sError = "Số lượng tài khoản Portal đăng ký đã vượt giới hạn cho phép"
                            ImportValidate.IsValidDate("IS_PORTAL", row, rowError, isError, sError)
                        End If
                    Else
                        row("IS_PORTAL") = "0"
                    End If
                    If row("IS_AD").ToString <> "-1" Then
                        row("IS_AD") = "0"
                    End If
                End If
                sError = "Chưa nhập ngày hiệu lực"
                ImportValidate.EmptyValue("EFFECT_DATE", row, rowError, isError, sError)
                If Not IsDBNull(row("EFFECT_DATE")) AndAlso Not String.IsNullOrEmpty(row("EFFECT_DATE")) Then
                    sError = "Sai định dạng ngày"
                    ImportValidate.IsValidDate("EFFECT_DATE", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("EXPIRE_DATE")) AndAlso Not String.IsNullOrEmpty(row("EXPIRE_DATE")) Then
                    sError = "Sai định dạng ngày"
                    ImportValidate.IsValidDate("EXPIRE_DATE", row, rowError, isError, sError)
                End If

                If isError Then
                    If IsDBNull(rowError("EMPLOYEE_CODE")) Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    dtError.Rows.Add(rowError)
                End If
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("ListUser") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Import_Taikhoan_Error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            If isError Then
                Return False
            Else
                Return True
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean
        Dim filePath As String
        Dim templatefolder As String
        Dim designer As WorkbookDesigner
        Try
            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack();", True)
                Return False
            End If
            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", Aspose.Words.ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
#End Region

#Region "Custom"
    ''' <summary>
    ''' Phục hồi dữ liệu đã xóa
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
            _mylog.WriteLog(_mylog._info, _classPath, method,
                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
        Return lst
    End Function
    Private Sub TableMapping(ByVal dtdata As DataTable)
        Dim row As DataRow = dtdata.Rows(0)
        Dim index As Integer = 0
        For Each cols As DataColumn In dtdata.Columns
            Try
                cols.ColumnName = row(index)
                index += 1
                If index > row.ItemArray.Length - 1 Then Exit For
            Catch ex As Exception
                Exit For
            End Try
        Next
        dtdata.Rows(0).Delete()
        dtdata.Rows(0).Delete()
        dtdata.AcceptChanges()
    End Sub
    Protected Sub rgGrid_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        'If Not IsPostBack Then
        '    rgGrid.SelectedIndexes.Clear()
        '    SelectedID = Nothing
        'End If
        'rgGrid.MasterTableView.GetColumn("IS_AD").Visible = False
        If rgGrid.SelectedItems.Count = 0 And CurrentState = CommonMessage.STATE_NORMAL Then
            UserView.SetProperty("User", Nothing)
            UserView.Refresh()
            rgGrid.SelectedIndexes.Clear()
            'UpdatePageViewState(CommonMessage.ACTION_UPDATED)
        End If
        'rgGrid.Rebind()
    End Sub
#End Region

End Class