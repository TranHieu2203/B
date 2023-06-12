Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlTreelistEmp
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
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Seperator,
                                        ToolbarItem.Active,
                                        ToolbarItem.Deactive)
            CType(MainToolBar.Items(0), RadToolBarButton).Text = "Khai báo chi tiết"
            CType(MainToolBar.Items(2), RadToolBarButton).Text = "Kích hoạt"
            CType(MainToolBar.Items(3), RadToolBarButton).Text = "Ngừng kích hoạt"
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
        If cbDissolve.Checked = True Then
            lst1 = rep.GetOrgTreeEmp("")
        Else
            lst1 = rep.GetOrgTreeEmp("A")
        End If

        For Each t In lst1
            lst.Add(New OrgTreeDTO With {.ID = t.ID, .CODE = t.CODE,
                    .NAME_VN = t.NAME_VN, .NAME_EN = t.NAME_EN, .TITLE_NAME = t.TITLE_NAME, .POSITION_NAME = t.POSITION_NAME,
                    .PARENT_ID = t.PARENT_ID, .NLEVEL = t.NLEVEL,
                    .COST_CENTER_CODE = t.COST_CENTER_CODE, .EFFECT_DATE = t.EFFECT_DATE,
                    .GROUPPROJECT = t.GROUPPROJECT, .STATUS_NAME = t.STATUS_NAME, .COLOR = t.COLOR, .FILENAME = t.FILENAME, .ATTACH_FILE = t.ATTACH_FILE})
        Next

        rgOrgTitle.DataSource = lst
        OrgTree = lst1

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
        Dim orgItem As New OrganizationDTO
        Dim lst1 As New List(Of OrganizationDTO)
        lst1 = rep.GetOrgTreeEmp()
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
                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlTreeListEdit&group=Business&orgid=" & item.GetDataKeyValue("ID"), False)
                    Next
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgOrgTitle.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For idx = 0 To rgOrgTitle.SelectedItems.Count - 1
                        Dim item As TreeListDataItem = rgOrgTitle.SelectedItems(idx)
                        Dim ID_ORG = Decimal.Parse(item.GetDataKeyValue("ID"))

                        orgItem = (From p In lst1 Where p.ID = ID_ORG).SingleOrDefault
                        If orgItem.STATUS_NAME = "Kích hoạt" Then
                            ShowMessage(Translate("Bản ghi đang trong trạng thái Kích hoạt."), NotifyType.Warning)
                            Exit Sub
                        End If

                        Dim dtDataCheck = rep.GetDataByProcedures(4, ID_ORG, "", Common.Common.SystemLanguage.Name)
                        If dtDataCheck IsNot Nothing Then
                            If dtDataCheck.Rows.Count >= 1 Then
                                ShowMessage(Translate("Không thể Kích hoạt khi còn tổ chức cha đang Ngừng sử dụng"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If

                        Dim dtDataCheckOM = rep.GetDataByProcedures(11, ID_ORG, "", Common.Common.SystemLanguage.Name)
                        If dtDataCheckOM IsNot Nothing Then
                            If dtDataCheckOM.Rows.Count >= 1 Then
                                ShowMessage(Translate("Không thể Kích hoạt khi còn tổ chức cha Mới khởi tạo"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If

                    Next
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn kích hoạt dữ liệu này?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                    Session.Remove("OrganizationLocationDataCache")
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

                        Dim checkDeActive As Decimal
                        checkDeActive = (From p In lst1 Where p.PARENT_ID = ID_ORG And p.ACTFLG = "Sử dụng").Count

                        If checkDeActive >= 1 Then
                            ShowMessage(Translate("Không thể ngừng áp dụng khi còn tổ chức con đang sử dụng"), NotifyType.Warning)
                            Exit Sub
                        End If

                        If Not rep.CheckEmployeeInOrganization(GetAllChild(item.GetDataKeyValue("ID"))) Then
                            ShowMessage("Bạn phải điều chuyển toàn bộ nhân viên trước khi ngừng kích hoạt.", NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    For idx = 0 To rgOrgTitle.SelectedItems.Count - 1
                        Dim item As TreeListDataItem = rgOrgTitle.SelectedItems(idx)
                        Dim ID_ORG = Decimal.Parse(item.GetDataKeyValue("ID"))

                        orgItem = (From p In lst1 Where p.ID = ID_ORG).SingleOrDefault
                        If orgItem.STATUS_NAME = "Ngừng kích hoạt" Then
                            ShowMessage(Translate("Bản ghi đang trong trạng thái Ngừng kích hoạt."), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn ngừng kích hoạt dữ liệu này?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                    Session.Remove("OrganizationOMLocationDataCache")
                    Session.Remove("OrganizationLocationDataCache")
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


                item.ForeColor = Drawing.Color.FromArgb(Int32.Parse(color.Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier))
                If (CType(item.GetDataKeyValue("GROUPPROJECT"), Boolean)) Then
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
                Dim strName = CType(item("FILENAME").Text, String).ToUpper()
                If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Then
                    item("ViewCommandColumn").Visible = True
                Else
                    item("ViewCommandColumn").Visible = False
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
                Dim filepath As String = Server.MapPath("~/ReportTemplates/Profile/Organization/File/" & FileAttach & "/" & FileName)
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
                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgOrgTitle.SelectedItems.Count - 1
                        Dim item As TreeListDataItem = rgOrgTitle.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveOrgEmp(lstDeletes, 8526) Then
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
                    If rep.ActiveOrgEmp(lstDeletes, 8527) Then
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
    Public Sub Show(strfile As Object)
        Dim script As String
        script = "var oWnd = $find('" & popupId & "');"
        script &= "oWnd.add_close(" & Me.ClientID & "_OnClientClose);"
        script &= "oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlViewImage&group=Business&state=Normal&emp=" & strfile & "');"
        script &= "oWnd.show();"
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)

    End Sub
#End Region


End Class