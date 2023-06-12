Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_OrgTitle
    Inherits Common.CommonView
    Protected WithEvents OrganizationView As ViewBase
    Protected WithEvents ctrlFindTitlePopup As ctrlFindTitlePopup

    'EditDate: 21-06-2017
    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/Setting/" + Me.GetType().Name.ToString()
#Region "Property"
    ''' <lastupdate>
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' Danh sách chức danh theo đơn vị
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InsertOrgTitles As List(Of OrgTitleDTO)
        Get
            Return PageViewState(Me.ID & "_InsertOrgTitles")
        End Get
        Set(ByVal value As List(Of OrgTitleDTO))
            PageViewState(Me.ID & "_InsertOrgTitles") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' SelectOrgFunction
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SelectOrgFunction As String
        Get
            Return PageViewState(Me.ID & "_SelectOrgFunction")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_SelectOrgFunction") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' 0 - normal
    ''' 1 - Title
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
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức viewload 
    ''' Thiết lập các mặc định ban đầu cho các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo thiết lập ban đầu cho các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgOrgTitle.SetFilter()
            rgOrgTitle.AllowCustomPaging = True
            InitControl()
            If Not IsPostBack Then
                GirdConfig(rgOrgTitle)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo thiết lập trạng thái ban đầu cho các item trên toolbar, 
    ''' thiết lập cho ctrlMessageBox
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarOrgTitles
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create, ToolbarItem.Seperator,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Active,
                                       ToolbarItem.Deactive)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức làm mới các thành phần trên trang
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            'ctrlOrg.ShowLevel = 2
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgOrgTitle.CurrentPageIndex = 0
                        rgOrgTitle.MasterTableView.SortExpressions.Clear()
                        rgOrgTitle.Rebind()
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgOrgTitle.CurrentPageIndex = 0
                        rgOrgTitle.MasterTableView.SortExpressions.Clear()
                        rgOrgTitle.Rebind()
                End Select
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức tạo dữ liệu filter cho trang
    ''' Xét giá trị cho các object trên radgrid
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CreateDataFilter()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Dim _filter As New OrgTitleDTO
        Dim OrgTitles As List(Of OrgTitleDTO)
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If SelectOrgFunction Is Nothing Then
                OrgTitles = New List(Of OrgTitleDTO)
                Exit Sub
            End If
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgOrgTitle, _filter)
            _filter.ORG_ID = Decimal.Parse(SelectOrgFunction)
            Dim Sorts As String = rgOrgTitle.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                OrgTitles = rep.GetOrgTitle(_filter, rgOrgTitle.CurrentPageIndex, rgOrgTitle.PageSize, MaximumRows, Sorts)
            Else
                OrgTitles = rep.GetOrgTitle(_filter, rgOrgTitle.CurrentPageIndex, rgOrgTitle.PageSize, MaximumRows)
            End If
            rep.Dispose()
            rgOrgTitle.VirtualItemCount = MaximumRows
            rgOrgTitle.DataSource = OrgTitles
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện cho control tool bar khi click và các item trên tool bar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objOrgFunction As New OrganizationDTO
        Dim sError As String = ""
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                'Click item thêm mới
                Case CommonMessage.TOOLBARITEM_CREATE
                    If rep.GetOrganizationByID(ctrlOrg.CurrentValue).ACTFLG = "I" Then
                        ShowMessage(Translate("Đơn vị đã giải thể, không được thêm."), NotifyType.Warning)
                        Exit Sub
                    End If
                    If SelectOrgFunction = "" Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ORG), NotifyType.Error)
                        Exit Sub
                    End If
                    'Response.Redirect("/Default.aspx?mid=Organize&fid=ctrlHU_OrgTitleNewEdit&group=Business&orgid=" & ctrlOrg.CurrentValue, False)
                    'Page.Validate()
                    'If Page.IsValid Then
                    '    Page.Response.Redirect("/Default.aspx?mid=Organize&fid=ctrlHU_OrgTitleNewEdit&group=Business&orgid=" & ctrlOrg.CurrentValue)
                    'End If
                    isLoadPopup = 1
                    UpdateControlState()
                    ctrlFindTitlePopup.Show()
                    Exit Sub
                    'Click item kích hoạt
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgOrgTitle.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                    'Click item hủy kích hoạt
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgOrgTitle.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For idx = 0 To rgOrgTitle.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgOrgTitle.SelectedItems(idx)
                        'Kiểm tra đã được sử dụng chưa
                        Dim _rep As New ProfileBusinessRepository
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                    ' Click item Xóa
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgOrgTitle.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgOrgTitle.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgOrgTitle.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If Not rep.CheckTitleInEmployee(lstDeletes,
                                                         Decimal.Parse(ctrlOrg.CurrentValue)) Then
                        ShowMessage(Translate("Tồn tại nhân viên đang sử dụng chức danh, không thể thực hiện thao tác này."), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
            rep.Dispose()
            'Cập nhật lại trạng thái các control trên trang
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức tạo cây thư mục các tổ chức trên menu bên trái cấp cha
    ''' Các đơn vị giải thể có màu vàng, khi click chọn checkbox mới hiển thị
    ''' </summary>
    ''' <param name="tree"></param>
    ''' <param name="list"></param>
    ''' <param name="bCheck"></param>
    ''' <remarks></remarks>
    Protected Sub BuildTreeNode(ByVal tree As RadTreeView, ByVal list As List(Of OrganizationDTO), ByVal bCheck As Boolean)
        Dim node As New RadTreeNode
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            tree.Nodes.Clear()
            If list.Count = 0 Then
                Exit Sub
            End If
            Dim listTemp = (From t In list Where t.PARENT_ID Is Nothing Select t).FirstOrDefault
            node.Text = listTemp.CODE & " - " & listTemp.NAME_VN
            node.ToolTip = listTemp.NAME_VN
            node.Value = listTemp.ID.ToString
            If SelectOrgFunction IsNot Nothing Then
                If node.Value = SelectOrgFunction Then
                    node.Selected = True
                End If
            End If
            tree.Nodes.Add(node)
            BuildTreeChildNode(node, list, bCheck)
            tree.ExpandAllNodes()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức tạo cây thư mục tổ chức cấp con
    ''' </summary>
    ''' <param name="nodeParent"></param>
    ''' <param name="list"></param>
    ''' <param name="bCheck"></param>
    ''' <remarks></remarks>
    Protected Sub BuildTreeChildNode(ByVal nodeParent As RadTreeNode, ByVal list As List(Of OrganizationDTO), ByVal bCheck As Boolean)
        Dim listTemp As List(Of OrganizationDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If bCheck Then
                listTemp = (From t In list Where t.PARENT_ID = Decimal.Parse(nodeParent.Value)).ToList
            Else
                listTemp = (From t In list Where t.PARENT_ID = Decimal.Parse(nodeParent.Value) And (t.DISSOLVE_DATE Is Nothing Or (t.DISSOLVE_DATE IsNot Nothing And t.DISSOLVE_DATE >= Date.Now))).ToList
            End If

            If listTemp.Count = 0 Then
                Exit Sub
            End If
            For index As Integer = 0 To listTemp.Count - 1
                Dim node As New RadTreeNode
                node.Text = listTemp(index).CODE & " - " & listTemp(index).NAME_VN
                node.ToolTip = listTemp(index).NAME_VN
                node.Value = listTemp(index).ID.ToString
                If bCheck Then
                    If listTemp(index).DISSOLVE_DATE IsNot Nothing Then
                        If listTemp(index).DISSOLVE_DATE < Date.Now Then
                            node.ForeColor = Drawing.Color.AliceBlue
                            node.BackColor = Drawing.Color.DarkGray
                        End If
                    End If

                End If

                nodeParent.Nodes.Add(node)
                BuildTreeChildNode(node, list, bCheck)
            Next
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức OnReceiveData của control ctrlOrgTitle
    ''' Thiết lập các trạng thái hiện tại theo giá trị của action
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub ctrlOrgTitle_OnReceiveData(ByVal sender As IViewListener(Of ViewBase), ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim mess As MessageDTO = CType(e.EventData, MessageDTO)
            Select Case e.FromViewID
                Case "ctrlOrgTitleNew"
                    Select Case mess.Mess
                        Case CommonMessage.ACTION_INSERTED
                            CurrentState = CommonMessage.STATE_NEW
                            Refresh("InsertView")
                            UpdateControlState()
                        Case CommonMessage.TOOLBARITEM_CANCEL
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                    End Select
                Case "ctrlOrgTitleEdit"
                    Select Case mess.Mess
                        Case CommonMessage.ACTION_UPDATED
                            CurrentState = CommonMessage.STATE_NORMAL
                            Refresh("UpdateView")
                            UpdateControlState()
                        Case CommonMessage.TOOLBARITEM_CANCEL
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                    End Select
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Yes/No của button command ctrlMessageBox
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
                UpdateControlState()
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState()
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Selected NodeChanged của control ctrlOrg 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            SelectOrgFunction = ctrlOrg.CurrentValue
            rgOrgTitle.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện click khi click cancel trên control ctrlFind
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFind_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindTitlePopup.CancelClicked
        Try
            isLoadPopup = 0
        Catch ex As Exception
        End Try

    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Title Selected cho control ctrlFindTitlePopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindTitlePopup_TitleSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindTitlePopup.TitleSelected
        Dim lstCommonTitle As New List(Of Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lstProfileTitle As New List(Of OrgTitleDTO)
        'Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonTitle = ctrlFindTitlePopup.SelectedTitle
            If lstCommonTitle IsNot Nothing Then
                If lstCommonTitle.Count <> 0 Then
                    For idx = 0 To lstCommonTitle.Count - 1
                        Dim item As Decimal = lstCommonTitle(idx)
                        lstProfileTitle.Add(New OrgTitleDTO With {.ACTFLG = "A",
                                                                  .ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                                                  .TITLE_ID = item})
                    Next
                End If
            End If
            isLoadPopup = 0
            If lstProfileTitle.Count <> 0 Then
                InsertOrgTitles = lstProfileTitle
                CurrentState = CommonMessage.STATE_NEW
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện needDataSource cho rad grid rgOrgTitle
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgOrgTitle.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 24/07/2017 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức cập nhật trạng thái các control trên trang theo trạng thái hiện tại:
    ''' Thêm mới, hủy kích hoạt, kích hoạt, xóa
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Dim gID As Decimal
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    Dim lstID As New List(Of Decimal)
                    'If (InsertOrgTitles.Count = 1 And InsertOrgTitles(0).TITLE_ID = 0) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                    '    Return
                    'End If
                    For Each item In InsertOrgTitles
                        If item IsNot Nothing Then
                            lstID.Add(item.TITLE_ID)
                        End If
                    Next
                    'If Not rep.CheckExistInDatabase(lstID, ProfileCommonTABLE_NAME.HU_TITLE) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                    '    Return
                    'End If
                    If rep.InsertOrgTitle(InsertOrgTitles, gID) Then
                        InsertOrgTitles = Nothing
                        CurrentState = CommonMessage.STATE_NORMAL
                        Refresh("UpdateView")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgOrgTitle.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgOrgTitle.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveOrgTitle(lstDeletes, "I") Then
                        Refresh("UpdateView")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgOrgTitle.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgOrgTitle.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveOrgTitle(lstDeletes, "A") Then
                        Refresh("UpdateView")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgOrgTitle.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgOrgTitle.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If Not rep.CheckExistInDatabase(lstDeletes, ProfileCommonTABLE_NAME.HU_TITLE) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                        Return
                    End If
                    If rep.DeleteOrgTitle(lstDeletes) Then
                        Refresh("UpdateView")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select

            Select Case isLoadPopup
                Case 1
                    If Not Me.Controls.Contains(ctrlFindTitlePopup) Then
                        ctrlFindTitlePopup = Me.Register("ctrlFindTitlePopup", "Common", "ctrlFindTitlePopup")
                        Me.Controls.Add(ctrlFindTitlePopup)
                    End If
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

#End Region

End Class