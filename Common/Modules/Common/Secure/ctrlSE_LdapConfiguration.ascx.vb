Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlSE_LdapConfiguration
    Inherits CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Secure/" + Me.GetType().Name.ToString()
    Dim _lDAPType As String = IIf(ConfigurationManager.AppSettings("LDAPType") IsNot Nothing, ConfigurationManager.AppSettings("LDAPType").ToString.Trim, "").ToString
    Dim _userLDAP As String = IIf(ConfigurationManager.AppSettings("User") IsNot Nothing, ConfigurationManager.AppSettings("User").ToString.Trim, "").ToString
    Dim _passWord As String = IIf(ConfigurationManager.AppSettings("PassWord") IsNot Nothing, ConfigurationManager.AppSettings("PassWord").ToString.Trim, "").ToString

#Region "Property"
    ''' <summary>
    ''' 
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
#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 27/07/2017 11:28
    ''' </lastupdate>
    ''' <summary>
    ''' Hiển thị thông tin các control trên trang
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
    ''' <lastupdate>
    ''' 27/07/2017 11:28
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            InitControl()
        Catch ex As Exception

        End Try
    End Sub
    ''' <lastupdate>
    ''' 27/07/2017 11:28
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete, ToolbarItem.Export, ToolbarItem.Sync)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            CType(MainToolBar.Items(7), RadToolBarButton).Text = Translate("Kiểm tra kết nối")
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
    ''' 27/07/2017 11:28
    ''' </lastupdate>
    ''' <summary>
    ''' Làm mới trạng thái, thiết lập, giá trị của các control trên trang
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New CommonRepository
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
                        ClearControlValue(txtLDAP, txtBaseDN, txtDomainName)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        ClearControlValue(txtLDAP, txtBaseDN, txtDomainName)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
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
    ''' <lastupdate>
    ''' 27/07/2017 11:28
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm tạo dữ liệu filter cho grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New CommonRepository
        Dim _filter As New LdapDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetLdap(_filter, Sorts).ToTable()
                Else
                    Return rep.GetLdap(_filter).ToTable()
                End If
            Else
                Dim Ldaps As List(Of LdapDTO)
                If Sorts IsNot Nothing Then
                    Ldaps = rep.GetLdap(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    Ldaps = rep.GetLdap(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If

                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = Ldaps
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 27/07/2017 11:28
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New CommonRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgMain, False)
                    txtBaseDN.ReadOnly = False
                    txtDomainName.ReadOnly = False
                    txtLDAP.ReadOnly = False

                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    txtBaseDN.ReadOnly = True
                    txtDomainName.ReadOnly = True
                    txtLDAP.ReadOnly = True

                Case CommonMessage.STATE_EDIT

                    EnabledGridNotPostback(rgMain, False)
                    txtBaseDN.ReadOnly = False
                    txtDomainName.ReadOnly = False
                    txtLDAP.ReadOnly = False


            End Select
            txtLDAP.Focus()
            UpdateToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <lastupdate>
    ''' 27/07/2017 11:28
    ''' </lastupdate>
    ''' <summary>
    ''' Bind dữ liệu lên form thêm/ sửa
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("LDAP_NAME", txtLDAP)
            dic.Add("BASE_DN", txtBaseDN)
            dic.Add("DOMAIN_NAME", txtDomainName)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
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
    ''' 27/07/2017 11:28
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Command trên main toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objLdap As New LdapDTO
        Dim rep As New CommonRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim gID As Decimal
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtLDAP, txtBaseDN, txtDomainName)
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgMain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.MessageTitle = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE_TITLE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "Ldap")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objLdap.LDAP_NAME = txtLDAP.Text.Trim
                        objLdap.BASE_DN = txtBaseDN.Text.Trim
                        objLdap.DOMAIN_NAME = txtDomainName.Text.Trim
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertLdap(objLdap, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objLdap.ID = rgMain.SelectedValue
                                If rep.ModifyLdap(objLdap, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ClearControlValue(txtBaseDN, txtDomainName, txtLDAP)
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_SYNC
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim ip As String = ""
                    Dim item As GridDataItem = rgMain.SelectedItems(0)
                    If ((item.GetDataKeyValue("LDAP_NAME") Is Nothing) Or (Not item.GetDataKeyValue("LDAP_NAME").ToString.Trim.ToUpper.Contains("LDAP://"))) Then
                        ShowMessage(Translate("LDAP Name không tồn tại hoặc chưa đúng"), NotifyType.Warning)
                        Exit Sub
                    Else
                        ip = item.GetDataKeyValue("LDAP_NAME")
                    End If
                    If _userLDAP.Contains("") Then
                        ShowMessage(Translate("Tài khoản không tồn tại"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If _passWord Is Nothing Then
                        ShowMessage(Translate("Mật khẩu không tồn tại"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If item.GetDataKeyValue("BASE_DN") Is Nothing Then
                        ShowMessage(Translate("Base DN không tồn tại"), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim bSuccess As Boolean = False
                    Using encry As New EncryptData
                        If (_lDAPType.ToUpper.Trim.Contains("AD")) Then 'window authen
                            bSuccess = TVC.LDAP.ADManager.CheckConnect(ip,
                                                                       _userLDAP,
                                                                       _passWord)
                        Else ' ldap
                            bSuccess = TVC.LDAP.ADManager.CheckConnect(ip,
                                                                       _userLDAP,
                                                                       _passWord, item.GetDataKeyValue("BASE_DN"))
                        End If

                        If bSuccess Then
                            ShowMessage(Translate("Kết nối thành công"), NotifyType.Success)
                        Else
                            ShowMessage(Translate("Kết nối thất bại"), NotifyType.Warning)
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
    ''' 27/07/2017 11:28
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện click Yes/No trên message ctrlMessageBox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New CommonRepository
        Dim _error As String = ""
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                If (DeleteItemList.Count > 0) Then
                    If rep.DeleteLdap(DeleteItemList) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_EFFECT_NOT_DELETE), NotifyType.Error)
                        UpdateControlState()
                    End If
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 27/07/2017 11:28
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource cho control rad grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CreateDataFilter()
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 27/07/2017 11:28
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
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
            For Each dr As Telerik.Web.UI.GridDataItem In rgMain.SelectedItems
                lst.Add(Decimal.Parse(dr("ID").Text))
            Next
            _mylog.WriteLog(_mylog._info, _classPath, method,
                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
        Return lst
    End Function
#End Region

End Class