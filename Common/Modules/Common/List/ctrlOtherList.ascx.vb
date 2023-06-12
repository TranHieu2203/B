Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlOtherList
    Inherits CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/List/" + Me.GetType().Name.ToString()

#Region "Property"

    Public Property OtherLists As List(Of OtherListDTO)
        Get
            If PageViewState(Me.ID & "_OtherLists") Is Nothing Then
                PageViewState(Me.ID & "_OtherLists") = New List(Of OtherListDTO)
            End If
            Return PageViewState(Me.ID & "_OtherLists")
        End Get
        Set(ByVal value As List(Of OtherListDTO))
            PageViewState(Me.ID & "_OtherLists") = value
        End Set
    End Property

    Public Property OtherListGroups As List(Of OtherListGroupDTO)
        Get
            Return PageViewState(Me.ID & "_OtherListGroups")
        End Get
        Set(ByVal value As List(Of OtherListGroupDTO))
            PageViewState(Me.ID & "_OtherListGroups") = value
        End Set
    End Property

    Public Property SelectNode As String
        Get
            Return PageViewState(Me.ID & "_SelectNode")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_SelectNode") = value
        End Set
    End Property

    Public Property OtherListTypes As List(Of OtherListTypeDTO)
        Get
            Return PageViewState(Me.ID & "_OtherListTypes")
        End Get
        Set(ByVal value As List(Of OtherListTypeDTO))
            PageViewState(Me.ID & "_OtherListTypes") = value
        End Set
    End Property

    Property DeleteItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_DeleteItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_DeleteItemList") = value
        End Set
    End Property

    Property DeleteItemOtherList As List(Of OtherListDTO)
        Get
            Return PageViewState(Me.ID & "_DeleteItemOtherList")
        End Get
        Set(ByVal value As List(Of OtherListDTO))
            PageViewState(Me.ID & "_DeleteItemOtherList") = value
        End Set
    End Property

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

    Public Property SelectedID As Decimal
        Get
            Return PageViewState(Me.ID & "_SelectedID")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_SelectedID") = value
        End Set
    End Property

    Property OtherListType As String
        Get
            Return PageViewState(Me.ID & "_OtherListType")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_OtherListType") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <summary>
    ''' load page, set trang thai control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdatePageViewState()
            If Not IsPostBack Then
                'Load left tree view
                If Not treeOtherListType.Nodes(0).Nodes(0) Is Nothing Then
                    treeOtherListType.Nodes(0).Nodes(0).Selected = True
                    SelectNode = treeOtherListType.Nodes(0).Nodes(0).Value
                    OtherListType = treeOtherListType.Nodes(0).Nodes(0).Value
                End If

            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' khoi tao page, menu toolbar
    ''' set cac action duoc su dung: edit, create, save, cancel, active, deactive, export, delete
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarOtherLists
            rgOtherList.SetFilter()
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Active,
                                       ToolbarItem.Deactive,
                                       ToolbarItem.Export,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Delete
                                       )
            CType(tbarOtherLists.Items(1), RadToolBarButton).CausesValidation = False
            CType(tbarOtherLists.Items(0), RadToolBarButton).CausesValidation = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            rgOtherList.AllowCustomPaging = True
            'rgOtherList.ClientSettings.EnablePostBackOnRowClick = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' set trang thai control theo trang thai cua page
    ''' Process event xoa, ap dung, ngung ap dung
    ''' thay doi trang thai cua menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UpdatePageViewState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If

            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    If rgOtherList.SelectedValue IsNot Nothing Then
                        Dim item = (From p In OtherLists Where p.ID = rgOtherList.SelectedValue).SingleOrDefault
                        If item IsNot Nothing Then
                            txtOtherListNameVN.Text = item.NAME_VN
                            If item.ORDERBYID IsNot Nothing Then
                                txtNum.Text = item.ORDERBYID
                            End If

                            txtRemark.Text = item.REMARK
                            txtCode.Text = item.CODE
                        End If
                    Else
                        ClearControlValue(txtOtherListNameVN, txtNum, txtCode, txtNum, txtRemark)
                    End If
                    txtOtherListNameVN.ReadOnly = True
                    txtCode.ReadOnly = True
                    txtNum.ReadOnly = True
                    txtCode.BackColor = Drawing.Color.White
                    txtRemark.ReadOnly = True
                    EnabledGridNotPostback(rgOtherList, True)
                    treeOtherListType.Enabled = True
                Case CommonMessage.STATE_EDIT
                    txtOtherListNameVN.ReadOnly = False
                    txtCode.ReadOnly = True
                    txtNum.ReadOnly = False
                    txtCode.BackColor = Drawing.Color.Yellow
                    txtRemark.ReadOnly = False
                    EnabledGridNotPostback(rgOtherList, False)
                    treeOtherListType.Enabled = False
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgOtherList, False)
                    treeOtherListType.Enabled = False
                    txtRemark.ReadOnly = False
                    txtOtherListNameVN.ReadOnly = False
                    txtCode.ReadOnly = False
                    txtNum.ReadOnly = False
                    txtCode.BackColor = Drawing.Color.White
                Case CommonMessage.STATE_DELETE
                    Dim rep As New CommonRepository
                    If rep.DeleteOtherList(DeleteItemOtherList, (From p In OtherListTypes Where p.ID = Decimal.Parse(OtherListType) Select p.CODE).FirstOrDefault) Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        rgOtherList.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                        DeleteItemList = Nothing
                    Else
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                    End If
                    UpdatePageViewState()
            End Select
            ChangeToolbarState()
            txtCode.Focus()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load lai grid khi thuc hien xong action xoa, them moi, sua, luu, view page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New CommonRepository
            If SelectNode <> "" Then
                If Not IsPostBack Or Message = CommonMessage.ACTION_UPDATED Then
                    Dim Sorts As String = rgOtherList.MasterTableView.SortExpressions.GetSortString()
                    Dim _filter As New OtherListDTO
                    _filter.CODE = rgOtherList.MasterTableView.GetColumn("CODE").CurrentFilterValue
                    _filter.NAME_VN = rgOtherList.MasterTableView.GetColumn("NAME_VN").CurrentFilterValue
                    If rgOtherList.MasterTableView.GetColumn("ORDERBYID").CurrentFilterValue IsNot Nothing AndAlso rgOtherList.MasterTableView.GetColumn("ORDERBYID").CurrentFilterValue <> "" Then
                        _filter.ORDERBYID = CDec(rgOtherList.MasterTableView.GetColumn("ORDERBYID").CurrentFilterValue)
                    End If
                    ' _filter.REMARK = rgOtherList.MasterTableView.GetColumn("REMARK").CurrentFilterValue
                    '_filter.NAME_EN = rgOtherList.MasterTableView.GetColumn("NAME_EN").CurrentFilterValue
                    _filter.ACTFLG = rgOtherList.MasterTableView.GetColumn("ACTFLG").CurrentFilterValue
                    '_filter.TYPE_ID =
                    If Sorts IsNot Nothing Then
                        Me.OtherLists = rep.GetOtherListByType(Decimal.Parse(SelectNode), _filter, rgOtherList.CurrentPageIndex, rgOtherList.PageSize, MaximumRows, Sorts)
                    Else
                        Me.OtherLists = rep.GetOtherListByType(Decimal.Parse(SelectNode), _filter, rgOtherList.CurrentPageIndex, rgOtherList.PageSize, MaximumRows)
                    End If
                End If
            End If
            'Đưa dữ liệu vào Grid
            rgOtherList.MasterTableView.FilterExpression = ""
            rgOtherList.VirtualItemCount = MaximumRows
            rgOtherList.DataSource = Me.OtherLists
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load data len treeview
    ''' set ngon ngu cho cac control lable, column name
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New CommonRepository
            Dim tSystemCode As String = Request.Params("mid").Trim()
            Me.OtherListTypes = rep.GetOtherListType("A")
            Me.OtherListGroups = rep.GetOtherListGroupBySystem(tSystemCode)
            BuildTreeOtherListGroup(treeOtherListType, OtherListGroups, OtherListTypes)
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", txtCode)
            dic.Add("NAME_VN", txtOtherListNameVN)
            dic.Add("ORDERBYID", txtNum)
            dic.Add("REMARK", txtRemark)
            Utilities.OnClientRowSelectedChanged(rgOtherList, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' event click nut: them, sua, xoa, luu, export, ap dung, ngung ap dung
    ''' Load lai trang thai control theo trang thai page
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objOtherList As New OtherListDTO
        Dim rep As New CommonRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    If SelectNode = "" Then
                        ShowMessage(Translate("Bạn chưa chọn tham số"), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtCode, txtNum, txtOtherListNameVN)
                    txtCode.ReadOnly = False
                    UpdatePageViewState()
                    rgOtherList.Rebind()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgOtherList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgOtherList.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    txtOtherListNameVN.Focus()
                    txtCode.ReadOnly = True

                    txtNum.ReadOnly = True
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdatePageViewState()
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgOtherList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgOtherList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim _lstExcel As New List(Of OtherListDTO)
                    If SelectNode <> "" Then
                        Dim Sorts As String = rgOtherList.MasterTableView.SortExpressions.GetSortString()
                        Dim _filter As New OtherListDTO
                        _filter.CODE = rgOtherList.MasterTableView.GetColumn("CODE").CurrentFilterValue
                        _filter.NAME_VN = rgOtherList.MasterTableView.GetColumn("NAME_VN").CurrentFilterValue
                        _filter.REMARK = rgOtherList.MasterTableView.GetColumn("REMARK").CurrentFilterValue
                        _filter.ACTFLG = rgOtherList.MasterTableView.GetColumn("ACTFLG").CurrentFilterValue
                        If Sorts IsNot Nothing Then
                            _lstExcel = rep.GetOtherListByType(Decimal.Parse(SelectNode), _filter, 0, Integer.MaxValue, MaximumRows, Sorts)
                        Else
                            _lstExcel = rep.GetOtherListByType(Decimal.Parse(SelectNode), _filter, 0, Integer.MaxValue, MaximumRows)
                        End If
                    End If
                    If _lstExcel.ToTable().Rows.Count > 0 Then
                        rgOtherList.ExportExcel(Server, Response, _lstExcel.ToTable(), "OtherList")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        Exit Sub
                    End If

                Case CommonMessage.TOOLBARITEM_SAVE
                    Page.Validate()
                    If Page.IsValid Then
                        objOtherList.NAME_VN = txtOtherListNameVN.Text.Trim
                        objOtherList.ORDERBYID = CDec(Val(txtNum.Text))
                        'objOtherList.NAME_EN = txtOtherListNameVN.Text.Trim
                        objOtherList.CODE = txtCode.Text.Trim
                        objOtherList.REMARK = txtRemark.Text.Trim
                        objOtherList.TYPE_ID = Decimal.Parse(OtherListType)
                        objOtherList.TYPE_CODE = (From p In OtherListTypes Where p.ID = Decimal.Parse(OtherListType) Select p.CODE).FirstOrDefault

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objOtherList.ACTFLG = "A"
                                Dim validate As New OtherListDTO
                                validate.CODE = objOtherList.CODE
                                validate.TYPE_ID = objOtherList.TYPE_ID
                                validate.ACTFLG = "A"
                                If Not rep.ValidateOtherList(validate) Then
                                    ShowMessage(Translate("MÃ KÝ HIỆU ĐÃ TỒN TẠI."), NotifyType.Error)
                                    Exit Sub
                                End If
                                If rep.InsertOtherList(objOtherList) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    rgOtherList.Rebind()
                                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    ClearControlValue(txtCode, txtNum, txtOtherListNameVN, txtRemark)
                                    rgOtherList.SelectedIndexes.Clear()
                                Else
                                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                                End If
                            Case CommonMessage.STATE_EDIT
                                Dim validate As New OtherListDTO
                                validate.ID = rgOtherList.SelectedValue
                                validate.CODE = txtCode.Text.Trim.ToString
                                validate.ACTFLG = "A"
                                If Not rep.ValidateOtherList(validate) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                    Exit Sub
                                End If
                                objOtherList.ID = rgOtherList.SelectedValue
                                If rep.ModifyOtherList(objOtherList) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    rgOtherList.Rebind()
                                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    ClearControlValue(txtCode, txtNum, txtOtherListNameVN, txtRemark)
                                    rgOtherList.SelectedIndexes.Clear()
                                Else
                                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                                End If
                        End Select
                        UpdatePageViewState()
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgOtherList')")
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE

                    If rgOtherList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstDeletes As New List(Of OtherListDTO)
                    Dim lstDeletesID As New List(Of Decimal)
                    For idx = 0 To rgOtherList.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgOtherList.SelectedItems(idx)
                        lstDeletes.Add(New OtherListDTO With {.ID = Decimal.Parse(item("ID").Text)})
                        lstDeletesID.Add(Decimal.Parse(item("ID").Text))
                    Next

                    If rep.CheckOtherListExistInDatabase(lstDeletesID, Integer.Parse(treeOtherListType.SelectedNode.Value)) = False Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                        Return
                    End If

                    DeleteItemOtherList = lstDeletes
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    txtCode.ReadOnly = False
                    rgOtherList.SelectedIndexes.Clear()
                    UpdatePageViewState()
                    ClearControlValue(txtCode, txtNum, txtOtherListNameVN)
                    rgOtherList.Rebind()
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' event Yes/No cua message popup
    ''' process xoa, ap dung, ngung ap dung
    ''' load lai trang thai control, page khi thuc hien xong process
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
                    Case CommonMessage.ACTION_ACTIVE
                        If rep.ActiveOtherList(DeleteItemList, "A", (From p In OtherListTypes Where p.ID = Decimal.Parse(OtherListType) Select p.CODE).FirstOrDefault) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgOtherList.Rebind()
                            ClearControlValue(txtCode, txtNum, txtOtherListNameVN)
                            rgOtherList.SelectedIndexes.Clear()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & "<br />" & Translate(_error), NotifyType.Error)
                        End If
                    Case CommonMessage.ACTION_DEACTIVE
                        If rep.ActiveOtherList(DeleteItemList, "I", (From p In OtherListTypes Where p.ID = Decimal.Parse(OtherListType) Select p.CODE).FirstOrDefault) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgOtherList.Rebind()
                            ClearControlValue(txtCode, txtNum, txtOtherListNameVN)
                            rgOtherList.SelectedIndexes.Clear()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If
                    Case CommonMessage.TOOLBARITEM_DELETE
                        CurrentState = CommonMessage.STATE_DELETE
                        UpdatePageViewState()
                        ClearControlValue(txtCode, txtNum, txtOtherListNameVN)
                        rgOtherList.SelectedIndexes.Clear()
                End Select
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' event click item treeview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub treeOtherListType_SelectedNodeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles treeOtherListType.NodeClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If TypeOf CType(sender, RadTreeView).SelectedNode.Parent Is RadTreeView Then
                SelectNode = ""
            Else
                SelectNode = treeOtherListType.SelectedNode.Value
            End If
            Dim rep As New CommonRepository
            Dim is_admin As Boolean = rep.CheckGroupAdmin(Common.GetUsername)
            If treeOtherListType.SelectedNode.ForeColor = Drawing.Color.Red And is_admin = False Then
                tbarOtherLists.Visible = False
            Else
                tbarOtherLists.Visible = True
            End If
            If SelectNode IsNot Nothing Then
                SelectedID = 0
                OtherListType = treeOtherListType.SelectedValue
                ClearControlValue(txtCode, txtNum, txtOtherListNameVN)
                rgOtherList.CurrentPageIndex = 0
                Utilities.ClearRadGridFilter(rgOtherList)
                rgOtherList.Rebind()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgOtherList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgOtherList.DataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim gdiItem As GridDataItem = rgOtherList.MasterTableView.FindItemByKeyValue("ID", SelectedID)
            If gdiItem IsNot Nothing Then
                gdiItem.Selected = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load lai page, control sau khi update
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgOtherList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgOtherList.NeedDataSource
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
    ''' <summary>
    ''' Validate du lieu nhap vao cvalcode
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>

    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New CommonRepository
        Dim _validate As New OtherListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.TYPE_ID = OtherListType
                _validate.ID = rgOtherList.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateOtherList(_validate)
            Else
                If CurrentState = CommonMessage.STATE_NEW Then
                    _validate.CODE = txtCode.Text.Trim
                    _validate.TYPE_ID = OtherListType
                    args.IsValid = rep.ValidateOtherList(_validate)
                End If

            End If
            If Not args.IsValid Then

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
    ''' Phuong thuc lam moi rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgOtherList_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rgOtherList.SelectedItems.Count = 0 Or rgOtherList.SelectedItems.Count > 1 Then
                ClearControlValue(txtCode, txtNum, txtOtherListNameVN)
            ElseIf rgOtherList.SelectedItems.Count = 1 Then
                Dim item As GridDataItem = rgOtherList.SelectedItems.Item(0)
                txtCode.Text = item("CODE").Text
                txtOtherListNameVN.Text = item("NAME_VN").Text
                txtNum.Text = item("ORDERBYID").Text
                txtRemark.Text = item("REMARK").Text.ToString.Replace("&nbsp;", "")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub rgOtherList_ItemCreated(ByVal sender As Object, ByVal e As GridItemEventArgs)
        If TypeOf e.Item Is GridHeaderItem Then
            Dim headerItem As GridHeaderItem = CType(e.Item, GridHeaderItem)
            Dim headerChkBox As CheckBox = CType(headerItem("cbStatus").Controls(0), CheckBox)
            headerChkBox.AutoPostBack = True
            AddHandler headerChkBox.CheckedChanged, AddressOf headerChkBox_CheckedChanged
        End If
    End Sub

    Private Sub headerChkBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        ClearControlValue(txtCode, txtNum, txtOtherListNameVN)
    End Sub

#End Region

#Region "Custom"
    ''' <summary>
    ''' Load du lieu len left treeView
    ''' </summary>
    ''' <param name="tree"></param>
    ''' <param name="lstGroup"></param>
    ''' <param name="lstType"></param>
    ''' <remarks></remarks>
    Private Sub BuildTreeOtherListGroup(ByVal tree As RadTreeView, ByVal lstGroup As List(Of OtherListGroupDTO), ByVal lstType As List(Of OtherListTypeDTO))
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            tree.Nodes.Clear()
            For index = 0 To lstGroup.Count - 1
                Dim node As New RadTreeNode
                node.Text = lstGroup(index).NAME
                node.Value = lstGroup(index).ID.ToString
                tree.Nodes.Add(node)
                BuildTreeOtherListType(node, lstGroup, lstType)
            Next
            tree.ExpandAllNodes()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load node cho treeview
    ''' </summary>
    ''' <param name="nodeParent"></param>
    ''' <param name="lstGroup"></param>
    ''' <param name="lstType"></param>
    ''' <remarks></remarks>
    Private Sub BuildTreeOtherListType(ByVal nodeParent As RadTreeNode, ByVal lstGroup As List(Of OtherListGroupDTO), ByVal lstType As List(Of OtherListTypeDTO))
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            For index = 0 To lstType.Count - 1
                If lstType(index).GROUP_ID.ToString.ToUpper = nodeParent.Value.ToUpper Then
                    Dim node As New RadTreeNode
                    node.Text = lstType(index).NAME
                    node.Value = lstType(index).ID.ToString
                    If lstType(index).IS_SYSTEM Then
                        node.ForeColor = Drawing.Color.Red
                    End If
                    If SelectNode IsNot Nothing Then
                        If SelectNode = node.Value Then
                            node.Selected = True
                        End If
                    End If
                    nodeParent.Nodes.Add(node)
                End If
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' list cac row duoc chon tren grid khi boi den chon nhieu dong
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function RepareDataForDelete() As List(Of Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lst As New List(Of Decimal)
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            For Each dr As Telerik.Web.UI.GridDataItem In rgOtherList.SelectedItems
                lst.Add(dr.GetDataKeyValue("ID"))
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