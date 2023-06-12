Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog


Public Class ctrlOtherListType
    Inherits CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/List/" + Me.GetType().Name.ToString()

#Region "Property"



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

    Property DeleteItemList As List(Of OtherListTypeDTO)
        Get
            Return PageViewState(Me.ID & "_DeleteItemList")
        End Get
        Set(ByVal value As List(Of OtherListTypeDTO))
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
            Me.MainToolBar = tbarOtherListTypes
            rgOtherListType.SetFilter()
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
            CType(tbarOtherListTypes.Items(3), RadToolBarButton).CausesValidation = True
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
            Dim rep As New CommonRepository
            Utilities.FillDropDownList(cboOtherListTypeGroup, rep.GetOtherListGroup("A"), "Name", "ID", Common.SystemLanguage, False)
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    If rgOtherListType.SelectedValue IsNot Nothing Then
                        Dim item = (From p In OtherListTypes Where p.ID = rgOtherListType.SelectedValue).SingleOrDefault
                        If item IsNot Nothing Then
                            txtOtherListTypeName.Text = item.NAME
                            txtOtherListTypeCode.Text = item.CODE

                            Dim oListItem As RadComboBoxItem = cboOtherListTypeGroup.Items.FindItemByValue(item.GROUP_ID.ToString)
                            If oListItem IsNot Nothing Then
                                cboOtherListTypeGroup.SelectedValue = oListItem.Value
                            End If
                        End If
                    End If
                    txtOtherListTypeCode.ReadOnly = True
                    txtOtherListTypeName.ReadOnly = True
                    cboOtherListTypeGroup.Enabled = True
                    EnabledGridNotPostback(rgOtherListType, True)

                Case CommonMessage.STATE_EDIT
                    If rgOtherListType.SelectedValue IsNot Nothing Then
                        Dim item = (From p In OtherListTypes Where p.ID = rgOtherListType.SelectedValue).SingleOrDefault
                        txtOtherListTypeCode.Text = item.CODE
                        txtOtherListTypeName.Text = item.NAME
                        Dim oListItem As RadComboBoxItem = cboOtherListTypeGroup.Items.FindItemByValue(item.GROUP_ID.ToString)
                        If oListItem IsNot Nothing Then
                            cboOtherListTypeGroup.SelectedValue = oListItem.Value
                        End If
                    End If
                    txtOtherListTypeCode.ReadOnly = False
                    txtOtherListTypeName.ReadOnly = False
                    cboOtherListTypeGroup.Enabled = True
                    EnabledGridNotPostback(rgOtherListType, False)

                Case CommonMessage.STATE_NEW
                    txtOtherListTypeCode.Text = ""
                    txtOtherListTypeName.Text = ""
                    cboOtherListTypeGroup.SelectedIndex = 0
                    txtOtherListTypeCode.ReadOnly = False
                    txtOtherListTypeName.ReadOnly = False
                    cboOtherListTypeGroup.Enabled = True
                    EnabledGridNotPostback(rgOtherListType, False)
                Case CommonMessage.STATE_DELETE
                    If rep.DeleteOtherListType(DeleteItemList) Then
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
            txtOtherListTypeName.Focus()
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
                Dim systemName As String = Request.Params("mid").Trim()
                Me.OtherListTypes = rep.GetOtherListTypeSystem(systemName)
                OtherListTypes = (From p In OtherListTypes Where p.ACTFLG_DB = "A").ToList
                rgOtherListType.DataSource = Me.OtherListTypes
                rgOtherListType.DataBind()
            Else
                rgOtherListType.DataSource = Me.OtherListTypes
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New CommonRepository

            Me.OtherListTypes = rep.GetOtherListType()
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", txtOtherListTypeCode)
            dic.Add("NAME", txtOtherListTypeName)
            dic.Add("GROUP_ID", cboOtherListTypeGroup)
            Utilities.OnClientRowSelectedChanged(rgOtherListType, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
#End Region

#Region "Event"
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objOtherListType As New OtherListTypeDTO
        Dim rep As New CommonRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdatePageViewState(CommonMessage.ACTION_UPDATED)
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgOtherListType.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgOtherListType.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdatePageViewState(CommonMessage.ACTION_UPDATED)
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgOtherListType.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgOtherListType.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Utilities.GridExportExcel(rgOtherListType, "OtherList")
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then

                        objOtherListType.NAME = txtOtherListTypeName.Text.Trim
                        objOtherListType.CODE = txtOtherListTypeCode.Text.Trim
                        If cboOtherListTypeGroup.SelectedValue <> "" Then
                            objOtherListType.GROUP_ID = Decimal.Parse(cboOtherListTypeGroup.SelectedValue)
                        End If
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objOtherListType.ACTFLG = "A"
                                If rep.InsertOtherListType(objOtherListType) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Refresh(CommonMessage.ACTION_UPDATED)
                                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                Else
                                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objOtherListType.ID = rgOtherListType.SelectedValue
                                If rep.ModifyOtherListType(objOtherListType) Then
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

                    Dim lstDeletes As New List(Of OtherListTypeDTO)
                    For idx = 0 To rgOtherListType.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgOtherListType.SelectedItems(idx)
                        lstDeletes.Add(New OtherListTypeDTO With {.ID = Decimal.Parse(item("ID").Text)})
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
                        If rep.ActiveOtherListType(DeleteItemList, "A") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            Refresh(CommonMessage.ACTION_UPDATED)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & "<br />" & Translate(_error), NotifyType.Error)
                        End If
                    Case CommonMessage.ACTION_DEACTIVE
                        If rep.ActiveOtherListType(DeleteItemList, "I") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            Refresh(CommonMessage.ACTION_UPDATED)
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

    Private Sub rgOtherListType_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgOtherListType.DataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If SelectedID = 0 Then
                If (rgOtherListType.MasterTableView.Items.Count > 0) Then
                    rgOtherListType.MasterTableView.Items(0).Selected = True
                End If
            End If
            Dim gdiItem As GridDataItem = rgOtherListType.MasterTableView.FindItemByKeyValue("ID", SelectedID)
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


    Protected Function RepareDataForDelete() As List(Of OtherListTypeDTO)

        Dim lstDeleteOtherListTypes As New List(Of OtherListTypeDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim sActive As String = Nothing
            Dim bCheck As Boolean = True
            For idx = 0 To rgOtherListType.SelectedItems.Count - 1
                Dim item As GridDataItem = rgOtherListType.SelectedItems(idx)
                If sActive Is Nothing Then
                    sActive = item("ACTFLG").Text
                ElseIf sActive <> item("ACTFLG").Text Then
                    bCheck = False
                    Exit For
                End If
                lstDeleteOtherListTypes.Add(New OtherListTypeDTO With {.ID = Decimal.Parse(item("ID").Text),
                                                                         .ACTFLG = sActive})
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

        Return lstDeleteOtherListTypes
    End Function
#End Region

End Class