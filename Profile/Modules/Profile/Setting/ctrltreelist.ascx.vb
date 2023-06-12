Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports ICSharpCode.SharpZipLib.Checksums
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrltreelist
    Inherits Common.CommonView
    Protected WithEvents OrganizationView As ViewBase
    Protected WithEvents ctrlFindTitlePopup As ctrlFindTitlePopup

    'EditDate: 21-06-2017
    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/Setting/" + Me.GetType().Name.ToString()
    Public Property popupId As String
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
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
    Public Property OrgTree As List(Of OrganizationDTO)
        Get
            Return ViewState(Me.ID & "_OrgTree")
        End Get
        Set(ByVal value As List(Of OrganizationDTO))
            ViewState(Me.ID & "_OrgTree") = value
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
            'rgOrgTitle.SetFilter()
            'rgOrgTitle.AllowCustomPaging = True
            InitControl()
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            popupId = CType(Me.Page, AjaxPage).PopupWindow.ClientID
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
            Me.MainToolBar = tbarOrg
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Active, ToolbarItem.Deactive)
            CType(MainToolBar.Items(0), RadToolBarButton).Text = Translate("Sử dụng")
            CType(MainToolBar.Items(1), RadToolBarButton).Text = Translate("Ngừng Sử dụng")
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
        Dim rep As New ProfileRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgOrgTitle.CurrentPageIndex = 0
                        'rgOrgTitle.MasterTableView.SortExpressions.Clear()
                        rgOrgTitle.Rebind()
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgOrgTitle.CurrentPageIndex = 0
                        'rgOrgTitle.MasterTableView.SortExpressions.Clear()
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
    ''' 24/04/2019 9:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện needDataSource cho rad grid rgOrgTitle
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As TreeListNeedDataSourceEventArgs)
        Dim rep As New ProfileRepository
        Dim lst As New List(Of OrgTreeDTO)
        Dim lst1 As New List(Of OrganizationDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If cbDissolve.Checked = True Then
                lst1 = rep.GetOrgTreeList("")

            Else
                lst1 = rep.GetOrgTreeList("A")
            End If

            If chkUyBan.Checked Then
                lst1 = lst1.Where(Function(f) f.ID = 1 OrElse (f.UY_BAN IsNot Nothing AndAlso f.UY_BAN = -1)).ToList
            Else
                lst1 = lst1.Where(Function(f) f.ID = 1 OrElse (f.UY_BAN Is Nothing OrElse (f.UY_BAN IsNot Nothing AndAlso f.UY_BAN = 0))).ToList
            End If

            For Each t In lst1
                lst.Add(New OrgTreeDTO With {.ID = t.ID, .CODE = t.CODE, .NAME_VN = t.NAME_VN, .NAME_EN = t.NAME_EN, .PARENT_ID = t.PARENT_ID, .ORG_LEVEL_NAME = t.ORG_LEVEL_NAME, .COST_CENTER_CODE = t.COST_CENTER_CODE, .FOUNDATION_DATE = t.FOUNDATION_DATE, .GROUPPROJECT = t.GROUPPROJECT, .ACTFLG = t.ACTFLG, .COLOR = t.COLOR, .STATUS_NAME = t.STATUS_NAME, .ATTACH_FILE = t.ATTACH_FILE, .FILENAME = t.FILENAME, .UY_BAN = t.UY_BAN, .ORG_LEVEL = t.ORG_LEVEL})
                Dim tree As New RadTreeNode
                tree.Text = t.NAME_VN
                tree.ToolTip = t.NAME_VN
                'If t.ACTFLG = "Ngừng sử dụng" Then
                '    tree.BackColor = Drawing.Color.Yellow
                'End If
            Next

            rgOrgTitle.DataSource = lst
            Dim a = rgOrgTitle.DataSource
            OrgTree = lst1
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            rgOrgTitle.ExpandToLevel(1)
        End If
    End Sub

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
        Dim lst1 As New List(Of OrganizationDTO)
        Dim orgItem As New OrganizationDTO
        lst1 = rep.GetOrgTreeList()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    If rgOrgTitle.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgOrgTitle.SelectedItems.Count - 1
                        Dim item As TreeListDataItem = rgOrgTitle.SelectedItems(idx)
                        Response.Redirect("/Default.aspx?mid=Organize&fid=ctrlTreeListEdit&group=Business&orgid=" & item.GetDataKeyValue("ID"), False)
                    Next
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgOrgTitle.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For idx = 0 To rgOrgTitle.SelectedItems.Count - 1
                        Dim item As TreeListDataItem = rgOrgTitle.SelectedItems(idx)
                        Dim ID_ORG = Decimal.Parse(item.GetDataKeyValue("ID"))

                        Dim dtDataCheck = rep.GetDataByProcedures(4, 0, "", Common.Common.SystemLanguage.Name)
                        If dtDataCheck IsNot Nothing Then
                            If dtDataCheck.Rows.Count >= 1 Then
                                ShowMessage(Translate("Không thể Sử dụng khi còn tổ chức cha đang Ngừng sử dụng"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If


                        orgItem = (From p In lst1 Where p.ID = ID_ORG).SingleOrDefault
                        If orgItem.ACTFLG = "Sử dụng" Then
                            ShowMessage(Translate("Bản ghi đang trong trạng thái Sử dụng."), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn Sử dụng dữ liệu này?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                    Session.Remove("OrganizationOMLocationDataCache")
                    'Click item hủy kích hoạt
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgOrgTitle.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For idx = 0 To rgOrgTitle.SelectedItems.Count - 1
                        Dim item As TreeListDataItem = rgOrgTitle.SelectedItems(idx)
                        Dim ID_ORG = Decimal.Parse(item.GetDataKeyValue("ID"))

                        'repHF = New HistaffFrameworkRepository
                        'Dim dtDataCheck = repHF.ExecuteToDataSet("PKG_PROFILE.CHECK_DEACTIVE_ORG", New List(Of Object)({ID_ORG})).Tables(0)

                        Dim checkDeActive As Decimal
                        checkDeActive = (From p In lst1 Where p.PARENT_ID = ID_ORG And p.ACTFLG = "Sử dụng").Count

                        If checkDeActive >= 1 Then
                            ShowMessage(Translate("Không thể ngừng sử dụng khi còn tổ chức con đang sử dụng"), NotifyType.Warning)
                            Exit Sub
                        End If

                        Dim orgTitleItem As New TitleDTO
                        Dim lstTitle As New List(Of TitleDTO)
                        Dim _param = New ParamDTO With {.ORG_ID = ID_ORG,
                                          .IS_DISSOLVE = 0}
                        lstTitle = rep.GetPossition(orgTitleItem, _param)
                        orgTitleItem = (From p In lstTitle Where p.ORG_ID = ID_ORG).FirstOrDefault
                        If orgTitleItem Is Nothing Then

                        Else
                            If orgTitleItem.ACTFLG = "Áp dụng" Then
                                ShowMessage(Translate("Không thể ngừng sử dụng khi còn vị trí đang sử dụng"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                    Next
                    For idx = 0 To rgOrgTitle.SelectedItems.Count - 1
                        Dim item As TreeListDataItem = rgOrgTitle.SelectedItems(idx)
                        Dim ID_ORG = Decimal.Parse(item.GetDataKeyValue("ID"))

                        lst1 = rep.GetOrgTreeList()
                        orgItem = (From p In lst1 Where p.ID = ID_ORG).SingleOrDefault
                        If orgItem.ACTFLG = "Ngừng sử dụng" Then
                            ShowMessage(Translate("Bản ghi đang trong trạng thái Ngừng sử dụng."), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn Ngừng sử dụng dữ liệu này?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                    Session.Remove("OrganizationOMLocationDataCache")
                    ' Click item Xóa

            End Select
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

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Protected Sub orgTreeList_InsertCommand(ByVal sender As Object, ByVal e As TreeListCommandEventArgs)
        Dim tab As New Hashtable()
        Dim item As TreeListEditableItem = TryCast(e.Item, TreeListEditableItem)
        Dim objOrganization As New OrganizationDTO
        Dim rep As New ProfileRepository
        item.ExtractValues(tab)

        ConvertEmptyValuesToDBNull(tab)
        objOrganization.NAME_VN = tab("NAME_VN")
        If tab("NAME_EN").ToString = "" Then
            objOrganization.NAME_EN = ""
        Else
            objOrganization.NAME_EN = tab("NAME_EN")
        End If

        objOrganization.CODE = tab("CODE")
        objOrganization.PARENT_ID = tab("PARENT_ID")
        'objOrganization.CAP = Decimal.Parse(tab("CAP"))
        'If tab("THUTU").ToString = "" Then
        '    objOrganization.THUTU = Nothing
        'Else
        '    objOrganization.THUTU = tab("THUTU")
        'End If
        If tab("EFFECT_DATE").ToString = "" Then
            objOrganization.EFFECT_DATE = Nothing
        Else
            objOrganization.EFFECT_DATE = tab("EFFECT_DATE")
        End If
        objOrganization.COST_CENTER_CODE = tab("COST_CENTER_CODE")
        objOrganization.GROUPPROJECT = If(tab("GROUPPROJECT"), -1, 0)
        Dim orgItem As New OrganizationDTO
        Dim checkCODEORRG As Decimal
        Dim lst1 As New List(Of OrganizationDTO)
        lst1 = rep.GetOrgTreeList("")
        orgItem = (From p In lst1 Where p.ID = Decimal.Parse(tab("PARENT_ID"))).SingleOrDefault
        objOrganization.HIERARCHICAL_PATH = orgItem.HIERARCHICAL_PATH
        objOrganization.DESCRIPTION_PATH = orgItem.DESCRIPTION_PATH
        If orgItem.ACTFLG = "Ngừng sử dụng" Then
            ShowMessage(Translate("Không thể thêm mới khi tổ chức cha đã ngừng sử dụng."), NotifyType.Warning)
            Exit Sub
        End If
        checkCODEORRG = (From p In lst1 Where p.CODE = tab("CODE")).Count
        If checkCODEORRG > 0 Then
            e.Canceled = True
            ShowMessage(Translate("Mã tổ chức đã tồn tại."), NotifyType.Warning)
            Exit Sub
        End If
        If rep.InsertOrgTreeList(objOrganization, 0) Then
        Else
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
        End If
        Common.Common.OrganizationLocationDataSession = Nothing
        Common.Common.OrganizationOMLocationDataSession = Nothing
    End Sub

    Protected Sub orgTreeList_UpdateCommand(ByVal sender As Object, ByVal e As TreeListCommandEventArgs)
        Dim tab As New Hashtable()
        Dim item As TreeListEditableItem = TryCast(e.Item, TreeListEditableItem)
        Dim objOrganization As New OrganizationDTO
        Dim rep As New ProfileRepository
        item.ExtractValues(tab)

        ConvertEmptyValuesToDBNull(tab)
        objOrganization.NAME_VN = tab("NAME_VN")
        If tab("NAME_EN").ToString = "" Then
            objOrganization.NAME_EN = ""
        Else
            objOrganization.NAME_EN = tab("NAME_EN")
        End If
        objOrganization.CODE = tab("CODE")
        objOrganization.ID = Decimal.Parse(tab("ID"))
        If IsNumeric(tab("NLEVEL")) Then
            objOrganization.NLEVEL = Decimal.Parse(tab("NLEVEL"))
        End If

        'objOrganization.THUTU = Decimal.Parse(tab("THUTU"))
        If IsDate(tab("EFFECT_DATE").ToString) Then
            objOrganization.EFFECT_DATE = tab("EFFECT_DATE")
        End If

        objOrganization.COST_CENTER_CODE = tab("COST_CENTER_CODE")
        objOrganization.GROUPPROJECT = If(tab("GROUPPROJECT"), -1, 0)

        Dim checkCODEORRG As Decimal
        Dim lst1 As New List(Of OrganizationDTO)
        lst1 = rep.GetOrgTreeList("")
        checkCODEORRG = (From p In lst1 Where p.CODE = tab("CODE") And p.ID <> Decimal.Parse(tab("ID"))).Count
        If checkCODEORRG > 0 Then
            e.Canceled = True
            ShowMessage(Translate("Mã tổ chức đã tồn tại."), NotifyType.Warning)
            Exit Sub
        End If
        If rep.ModifyOrgTreeList(objOrganization) Then

        Else
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
        End If

        Common.Common.OrganizationLocationDataSession = Nothing
        Common.Common.OrganizationOMLocationDataSession = Nothing
    End Sub


    Private Sub ConvertEmptyValuesToDBNull(ByVal values As Hashtable)
        Dim keysToDbNull As New List(Of Object)()

        For Each entry As DictionaryEntry In values
            If entry.Value Is Nothing OrElse (TypeOf entry.Value Is [String] AndAlso [String].IsNullOrEmpty(DirectCast(entry.Value, [String]))) Then
                keysToDbNull.Add(entry.Key)
            End If
        Next

        For Each key As Object In keysToDbNull
            values(key) = DBNull.Value
        Next
    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien CheckedChanged cua control cbDissolve
    ''' Cap nhat trang thai cua cac control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cbDissolve_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbDissolve.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            RadGrid_NeedDataSource(Nothing, Nothing)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")


        End Try
    End Sub

    Private Sub rgOrgTitle_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.TreeListItemDataBoundEventArgs) Handles rgOrgTitle.ItemDataBound
        Try
            If TypeOf e.Item Is TreeListDataItem Then
                Dim item As TreeListDataItem = CType(e.Item, TreeListDataItem)
                Dim color = CType(item("COLOR").Text, String)
                Dim tab As New Hashtable()
                item.ExtractValues(tab)
                ConvertEmptyValuesToDBNull(tab)

                'If (CType(item("IS_JOB").Text, Boolean)) Then
                '    If item.DataItem.ACTFLG = "Ngừng sử dụng" Then
                '        item.CssClass = "NodeJOB inactive-row"
                '        item("ORG_NAME").CssClass = "NodeJOB inactive-row"
                '    Else
                '        item("ORG_NAME").CssClass = "NodeJOB"
                '        e.Item.CssClass = "NodeJOB1"
                '    End If
                'Else
                Dim IsGroup As Boolean = False
                Try
                    IsGroup = CType(tab("GROUPPROJECT"), Boolean)
                Catch ex As Exception
                    IsGroup = False
                End Try
                If (IsGroup) Then
                    If item.DataItem.ACTFLG = "Ngừng sử dụng" Then
                        item.CssClass = "NodeNhomDuAn1 inactive-row"
                        item("NAME_VN").CssClass = "NodeNhomDuAn inactive-row"
                    Else
                        item("NAME_VN").CssClass = "NodeNhomDuAn"
                        e.Item.CssClass = "NodeNhomDuAn1"
                    End If
                Else
                    If item.DataItem.ACTFLG = "Ngừng sử dụng" Then
                        item.CssClass = "NodeTree1 inactive-row"
                        item("NAME_VN").CssClass = "NodeTree inactive-row"
                    Else
                        item("NAME_VN").CssClass = "NodeTree"
                        e.Item.CssClass = "NodeTree1"
                    End If
                End If
                'End If
                item("NAME_EN").CssClass = "custom-column"
                item("CODE").CssClass = "custom-column"
                If color.Trim <> "&nbsp;" Then
                    item.ForeColor = Drawing.Color.FromArgb(Int32.Parse(color.Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier))
                End If
                Dim strName = CType(item("FILENAME").Text, String).ToUpper()
                If strName = "&NBSP;" Then
                    item("DowloadCommandColumn").Visible = False
                Else
                    item("DowloadCommandColumn").Visible = True
                End If
                If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Then
                    item("ViewCommandColumn").Visible = True
                Else
                    item("ViewCommandColumn").Visible = False
                End If

                Dim ORG_LEVEL = CType(item("ORG_LEVEL").Text, String)
                If IsNumeric(ORG_LEVEL) AndAlso CDec(ORG_LEVEL) = 860 Then
                    item.ForeColor = Drawing.Color.FromName("#33af8a")
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub RadTreeList1_ItemCommand(ByVal sender As Object, ByVal e As TreeListCommandEventArgs) Handles rgOrgTitle.ItemCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim a = e.CommandName
            If e.CommandName = "Dowload" Then
                Dim item As TreeListDataItem = TryCast(e.Item, TreeListDataItem)
                Dim orgItem As OrganizationDTO
                Dim ID As String = item.GetDataKeyValue("ID").ToString()
                orgItem = (From p In OrgTree Where p.ID = Decimal.Parse(ID)).SingleOrDefault
                Dim FileName As String = orgItem.FILENAME
                Dim FileAttach As String = orgItem.ATTACH_FILE
                Dim path As String = Server.MapPath("~/ReportTemplates/Profile/OrganizationFileAttach/" & FileAttach & FileName)
                ' ZipFiles(path, FileName)
                Dim url As String = "Download.aspx?" & "ctrlTreelistEmp," & FileName & "," & FileAttach
                Dim str As String = "window.open('" & url + "');"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End If
            If e.CommandName = "View" Then
                Dim item As TreeListDataItem = TryCast(e.Item, TreeListDataItem)
                Dim orgItem As OrganizationDTO
                Dim ID As String = item.GetDataKeyValue("ID").ToString()
                orgItem = (From p In OrgTree Where p.ID = Decimal.Parse(ID)).SingleOrDefault
                Dim FileName As String = orgItem.FILENAME
                Dim FileAttach As String = orgItem.ATTACH_FILE
                Dim filepath As String = Server.MapPath("~/ReportTemplates/Profile/OrganizationFileAttach/" & FileAttach & "/" & FileName)
                Dim strName As String = IO.Path.GetExtension(filepath).ToUpper()

                If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Then
                    Show(FileAttach & "/" & FileName)
                Else
                    ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
                End If

            End If
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub chkUyBan_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkUyBan.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgOrgTitle.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")


        End Try
    End Sub

#End Region

#Region "Custom"
    Private Sub ZipFiles(ByVal path As String, ByVal strName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New Crc32()
            Dim fileNameZip As String = strName

            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            'Response.ContentType = "application/octet-stream"
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Public Sub Show(strfile As Object)
        Dim script As String
        script = "var oWnd = $find('" & popupId & "');"
        script &= "oWnd.add_close(" & Me.ClientID & "_OnClientClose);"
        script &= "oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlViewImage&group=Business&state=Normal&emp=" & strfile & "');"
        script &= "oWnd.show();"
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)

    End Sub
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
                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgOrgTitle.SelectedItems.Count - 1
                        Dim item As TreeListDataItem = rgOrgTitle.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveOrgTreeList(lstDeletes, "I") Then
                        Refresh("UpdateView")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgOrgTitle.SelectedItems.Count - 1
                        Dim item As TreeListDataItem = rgOrgTitle.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveOrgTreeList(lstDeletes, "A") Then
                        Refresh("UpdateView")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lay tat ca cac con cua treeview
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetAllChild(ByVal _orgId As Decimal) As List(Of Decimal)
        Dim query As List(Of Decimal)
        Dim list As List(Of Decimal)
        Dim result As New List(Of Decimal)
        Dim repp As New ProfileRepository
        Dim rep As New CommonRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try

            result.Add(_orgId)
            Dim lstOrgs As List(Of OrganizationDTO) = repp.GetOrganization()

            query = (From p In lstOrgs Where p.PARENT_ID = _orgId
                     Select p.ID).ToList
            For Each q As Decimal In query
                list = GetAllChild(q)
                result.InsertRange(0, list)
            Next
            Return result
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
#End Region

End Class
Public Class MyData
    Public Shared Function GetData() As List(Of MyItem)
        Dim list As New Generic.List(Of MyItem)()
        list.Add(New MyItem("A", "Appetizers", "", Nothing, True, ""))
        list.Add(New MyItem("B", "Beverages", "", Nothing, True, ""))
        list.Add(New MyItem("C", "Cheese", "", Nothing, False, ""))
        list.Add(New MyItem("A1", "Southwestern Twisted Chips", "150 gr.", 6.79D, False, "A"))
        list.Add(New MyItem("A2", "Top Shelf Combo Appetizer", "300 gr.", 9.49D, True, "A"))
        list.Add(New MyItem("B1", "Sangria", "90 ml.", 6.49D, True, "B"))
        list.Add(New MyItem("B2", "Margarita", "60 ml.", 7.39D, False, "B"))
        list.Add(New MyItem("B3", "Red Cherry Boost", "200 ml.", 6.99D, False, "B"))
        list.Add(New MyItem("B4", "Mojito", "180 ml.", 7.59D, True, "B"))
        list.Add(New MyItem("C1", "Blue Cheese and Hazelnut Shortbread", "220 gr.", 10.69D, False, "C"))
        list.Add(New MyItem("C2", "Avocado Feta Salsa", "240 gr.", 7.19D, False, "C"))
        Return list
    End Function
End Class
Public Class MyItem
    Public Property ID() As String
        Get
            Return m_ID
        End Get
        Set(ByVal value As String)
            m_ID = value
        End Set
    End Property
    Private m_ID As String
    Public Property ProductName() As String
        Get
            Return m_ProductName
        End Get
        Set(ByVal value As String)
            m_ProductName = value
        End Set
    End Property
    Private m_ProductName As String
    Public Property Quantity() As String
        Get
            Return m_Quantity
        End Get
        Set(ByVal value As String)
            m_Quantity = value
        End Set
    End Property
    Private m_Quantity As String
    Public Property Price() As System.Nullable(Of Decimal)
        Get
            Return m_Price
        End Get
        Set(ByVal value As System.Nullable(Of Decimal))
            m_Price = value
        End Set
    End Property
    Private m_Price As System.Nullable(Of Decimal)
    Public Property InStock() As Boolean
        Get
            Return m_InStock
        End Get
        Set(ByVal value As Boolean)
            m_InStock = value
        End Set
    End Property
    Private m_InStock As Boolean
    Public Property ParentID() As String
        Get
            Return m_ParentID
        End Get
        Set(ByVal value As String)
            m_ParentID = value
        End Set
    End Property
    Private m_ParentID As String
    Public Sub New(ByVal id__1 As String, ByVal productName__2 As String, ByVal quantity__3 As String, ByVal price__4 As System.Nullable(Of Decimal), ByVal inStock__5 As Boolean, ByVal parentID__6 As String)
        ID = id__1
        ProductName = productName__2
        Quantity = quantity__3
        Price = price__4
        InStock = inStock__5
        ParentID = parentID__6
    End Sub
End Class
Public Class OrgTreeDTO
    Public Property ID As Decimal
    Public Property CODE As String
    Public Property NAME_VN As String
    Public Property NAME_EN As String
    Public Property PARENT_ID As Decimal?
    Public Property PARENT_NAME As String

    Public Property EFFECT_DATE As Date?
    Public Property FOUNDATION_DATE As Date?
    Public Property NLEVEL As Decimal?
    Public Property STATUS_ID As Decimal
    Public Property GROUPPROJECT As Boolean?
    Public Property UY_BAN As Boolean?
    Public Property COST_CENTER_CODE As String
    Public Property ACTFLG As String
    Public Property STATUS_NAME As String
    Public Property COLOR As String
    Public Property TITLE_NAME As String
    Public Property POSITION_NAME As String
    Public Property ATTACH_FILE As String
    Public Property FILENAME As String
    Public Property ORG_LEVEL As Decimal?
    Public Property ORG_LEVEL_NAME As String

End Class