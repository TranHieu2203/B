Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog


Public Class ctrlOtherListGroup
    Inherits CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/List/" + Me.GetType().Name.ToString()

#Region "Property"

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

    Property DeleteItemList As List(Of OtherListGroupDTO)
        Get
            Return PageViewState(Me.ID & "_DeleteItemList")
        End Get
        Set(ByVal value As List(Of OtherListGroupDTO))
            PageViewState(Me.ID & "_DeleteItemList") = value
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

    Property OtherListGroup As String
        Get
            Return PageViewState(Me.ID & "_OtherListType")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_OtherListType") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            UpdatePageViewState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarOtherListGroups
            rgOtherListGroup.SetFilter()
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Active,
                                       ToolbarItem.Deactive,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Delete
                                       )
            CType(tbarOtherListGroups.Items(3), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            'rgOtherListGroup.AllowCustomPaging = True
            'rgOtherListGroup.PageSize = Common.DefaultPageSize
            'rgOtherListGroup.ClientSettings.EnablePostBackOnRowClick = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub UpdatePageViewState(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If

            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    If rgOtherListGroup.SelectedValue IsNot Nothing Then
                        Dim item = (From p In OtherListGroups Where p.ID = rgOtherListGroup.SelectedValue).SingleOrDefault
                        If item IsNot Nothing Then
                            txtOtherListGroupName.Text = item.NAME
                        End If
                    End If
                    txtOtherListGroupName.ReadOnly = True
                    EnabledGridNotPostback(rgOtherListGroup, True)

                Case CommonMessage.STATE_EDIT
                    If rgOtherListGroup.SelectedValue IsNot Nothing Then
                        Dim item = (From p In OtherListGroups Where p.ID = rgOtherListGroup.SelectedValue).SingleOrDefault
                        txtOtherListGroupName.Text = item.NAME

                    End If
                    txtOtherListGroupName.ReadOnly = False
                    EnabledGridNotPostback(rgOtherListGroup, False)

                Case CommonMessage.STATE_NEW
                    txtOtherListGroupName.ReadOnly = False
                    txtOtherListGroupName.Text = ""
                    EnabledGridNotPostback(rgOtherListGroup, False)

                Case CommonMessage.STATE_DELETE
                    Dim rep As New CommonRepository
                    If rep.DeleteOtherListGroup(DeleteItemList) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        DeleteItemList = Nothing
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                    End If
                    Refresh(CommonMessage.ACTION_UPDATED)
                    UpdatePageViewState(CurrentState)
            End Select
            ChangeToolbarState()
            txtOtherListGroupName.Focus()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New CommonRepository

            If Not IsPostBack Or Message = CommonMessage.ACTION_UPDATED Then

                Me.OtherListGroups = rep.GetOtherListGroup()
                MaximumRows = OtherListGroups.Count()
                rgOtherListGroup.DataSource = Me.OtherListGroups
                rgOtherListGroup.DataBind()
            Else
                rgOtherListGroup.DataSource = Me.OtherListGroups
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New CommonRepository
            Me.OtherListTypes = rep.GetOtherListType("A")
            Me.OtherListGroups = rep.GetOtherListGroup()
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("NAME", txtOtherListGroupName)
            Utilities.OnClientRowSelectedChanged(rgOtherListGroup, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
#End Region

#Region "Event"
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objOtherListGroup As New OtherListGroupDTO
        Dim rep As New CommonRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdatePageViewState(CommonMessage.ACTION_UPDATED)
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgOtherListGroup.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgOtherListGroup.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdatePageViewState(CommonMessage.ACTION_UPDATED)
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgOtherListGroup.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgOtherListGroup.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Utilities.GridExportExcel(rgOtherListGroup, "OtherList")
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objOtherListGroup.NAME = txtOtherListGroupName.Text.Trim

                        'objOtherListGroup.ID = Decimal.Parse(OtherListGroup)

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objOtherListGroup.ACTFLG = "A"
                                If rep.InsertOtherListGroup(objOtherListGroup) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Refresh(CommonMessage.ACTION_UPDATED)
                                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                Else
                                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objOtherListGroup.ID = rgOtherListGroup.SelectedValue
                                If rep.ModifyOtherListGroup(objOtherListGroup) Then
                                    CurrentState = CommonMessage.STATE_NORMAL

                                    Refresh(CommonMessage.ACTION_UPDATED)
                                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                Else
                                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                                End If
                        End Select
                        UpdatePageViewState(CommonMessage.ACTION_UPDATED)
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE

                    Dim lstDeletes As New List(Of OtherListGroupDTO)
                    For idx = 0 To rgOtherListGroup.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgOtherListGroup.SelectedItems(idx)
                        lstDeletes.Add(New OtherListGroupDTO With {.ID = Decimal.Parse(item("ID").Text)})
                    Next

                    DeleteItemList = lstDeletes
                    If DeleteItemList.Count > 0 Then
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdatePageViewState(CommonMessage.ACTION_UPDATED)
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New CommonRepository
        Dim _error As String = ""
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Select Case e.ActionName
                    Case CommonMessage.ACTION_ACTIVE
                        If rep.ActiveOtherListGroup(DeleteItemList, "A") Then
                            Refresh(CommonMessage.ACTION_UPDATED)
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)

                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & "<br />" & Translate(_error), NotifyType.Error)
                        End If
                    Case CommonMessage.ACTION_DEACTIVE
                        If rep.ActiveOtherListGroup(DeleteItemList, "I") Then
                            Refresh(CommonMessage.ACTION_UPDATED)
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)

                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If
                    Case CommonMessage.TOOLBARITEM_DELETE
                        CurrentState = CommonMessage.STATE_DELETE
                        UpdatePageViewState(CommonMessage.ACTION_DELETED)
                End Select

            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgOtherListGroup_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgOtherListGroup.DataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If SelectedID = 0 Then
                If (rgOtherListGroup.MasterTableView.Items.Count > 0) Then
                    rgOtherListGroup.MasterTableView.Items(0).Selected = True
                End If
            End If
            Dim gdiItem As GridDataItem = rgOtherListGroup.MasterTableView.FindItemByKeyValue("ID", SelectedID)
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

#End Region

#Region "Custom"

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

    Protected Function RepareDataForDelete() As List(Of OtherListGroupDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lstDeleteOtherListGroups As New List(Of OtherListGroupDTO)
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim sActive As String = Nothing
            Dim bCheck As Boolean = True
            For idx = 0 To rgOtherListGroup.SelectedItems.Count - 1
                Dim item As GridDataItem = rgOtherListGroup.SelectedItems(idx)
                If sActive Is Nothing Then
                    sActive = item("ACTFLG").Text
                ElseIf sActive <> item("ACTFLG").Text Then
                    bCheck = False
                    Exit For
                End If
                lstDeleteOtherListGroups.Add(New OtherListGroupDTO With {.ID = Decimal.Parse(item("ID").Text),
                                                                         .ACTFLG = sActive})
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
        Return lstDeleteOtherListGroups
    End Function
#End Region

End Class