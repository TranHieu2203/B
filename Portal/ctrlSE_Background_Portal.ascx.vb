Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlSE_Background_Portal
    Inherits CommonView

#Region "Property"
    Public Property ListEvents As List(Of SE_BACKGROUND_PORTALDTO)
        Get
            Return PageViewState(Me.ID & "_ListEvents")
        End Get
        Set(ByVal value As List(Of SE_BACKGROUND_PORTALDTO))
            PageViewState(Me.ID & "_ListEvents") = value
        End Set
    End Property
    Public Property popupId As String
    Public Property AjaxManagerId As String
    Public WithEvents AjaxManager As RadAjaxManager
#End Region

#Region "Page"
    Public Overrides Sub ViewInit(e As System.EventArgs)
        Try
            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Delete)
            popupId = CType(Me.Page, AjaxPage).PopupWindow.ClientID
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub ViewLoad(e As System.EventArgs)
        Try
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New CommonRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    ClearControlValue(rtFromdate, rtTodate, rtNote, txtUploadFile, rtBackground, hidID)
                    EnableControlAll(True, rtFromdate, rtTodate, rtNote, btnUpload)
                    'EnabledGridNotPostback(rGrid, False)

                Case CommonMessage.STATE_NORMAL
                    ClearControlValue(rtFromdate, rtTodate, rtNote, txtUploadFile, rtBackground, hidID)
                    EnableControlAll(False, rtFromdate, rtTodate, rtNote, btnUpload)
                    'EnabledGridNotPostback(rGrid, True)

                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, rtFromdate, rtTodate, rtNote, btnUpload)
                    'EnabledGridNotPostback(rGrid, False)
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rGrid.SelectedItems.Count - 1
                        Dim item As GridDataItem = rGrid.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteSE_BACKGROUND_PORTAL(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If

            End Select

            UpdateToolbarState()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rGrid.Rebind()
                        ClearControlValue(rtFromdate, rtTodate, rtNote, rtBackground, txtUploadFile, hidID)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rGrid.CurrentPageIndex = 0
                        rGrid.MasterTableView.SortExpressions.Clear()
                        rGrid.Rebind()
                        ClearControlValue(rtFromdate, rtTodate, rtNote, rtBackground, txtUploadFile, hidID)
                    Case "Cancel"
                        rGrid.MasterTableView.ClearSelectedItems()
                End Select
            End If

            UpdateControlState()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim rep As New PortalRepository
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If (rGrid.SelectedItems.Count > 1) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rGrid.SelectedItems.Count - 1
                        Dim item As GridDataItem = rGrid.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item("ID").Text))
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    'If Page.IsValid Then
                    Dim obj As New SE_BACKGROUND_PORTALDTO
                    obj.FROM_DATE = rtFromdate.Text
                    obj.TO_DATE = rtTodate.Text
                    obj.NOTE = rtNote.Text
                    obj.BACKGROUND = rtBackground.Text
                    obj.FILEPATH = txtUploadFile.Text

                    Select Case CurrentState
                        Case CommonMessage.STATE_NEW
                            If rep.InsertBACKGROUND_PORTAL(obj) Then
                                CurrentState = CommonMessage.STATE_NORMAL
                                Refresh("UpdateView")
                                UpdateControlState()

                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                        Case CommonMessage.STATE_EDIT
                            obj.ID = hidID.Value
                            If rep.ModifyBACKGROUND_PORTAL(obj) Then
                                CurrentState = CommonMessage.STATE_NORMAL
                                Refresh("UpdateView")
                                UpdateControlState()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                    End Select
                    'End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select

        Catch ex As Exception
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New CommonRepository
        Try

            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rGrid.NeedDataSource
        Try
            'If IsPostBack Then Exit Sub

            Dim rep As New PortalRepository
            ListEvents = rep.GetListBackgroud()
            rGrid.DataSource = ListEvents

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ctrlUpload2.AllowedExtensions = "png,jpg,bitmap,jpeg,gif"
            ctrlUpload2.Show()
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    Protected Sub btnView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnView.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim filepath As String = Server.MapPath("~/Static/Images/" & txtUploadFile.Text & "/" & rtBackground.Text)
            Dim strName As String = IO.Path.GetExtension(filepath).ToUpper()

            If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Then
                Show(txtUploadFile.Text & "/" & rtBackground.Text)
            Else
                ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ctrlUpload2_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload2.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rtBackground.Text = ""
            Dim listExtension = New List(Of String)
            listExtension.Add(".png")
            listExtension.Add(".jpg")
            'listExtension.Add(".bitmap")
            listExtension.Add(".jpeg")
            listExtension.Add(".gif")

            Dim fileName As String

            Dim strPath As String = Server.MapPath("~/Static/Images/")
            If ctrlUpload2.UploadedFiles.Count >= 1 Then
                'For i = 0 To ctrlUpload2.UploadedFiles.Count - 1
                Dim file As UploadedFile = ctrlUpload2.UploadedFiles(0)
                Dim Newguid = Guid.NewGuid.ToString()
                Dim str_Filename = Newguid + "\"
                If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                    System.IO.Directory.CreateDirectory(strPath + str_Filename)
                    strPath = strPath + str_Filename
                    fileName = System.IO.Path.Combine(strPath, file.FileName)
                    file.SaveAs(fileName, True)
                    rtBackground.Text = file.FileName
                    txtUploadFile.Text = Newguid.ToString
                Else
                    ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file PNG,JPG,JPEG,GIF"), NotifyType.Warning)
                    Exit Sub
                End If
                'Next
                'loadDatasource(txtUpload.Text)
            End If
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    Protected Sub rGrid_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rGrid.SelectedIndexChanged
        ClearControlValue(rtFromdate, rtTodate, rtBackground, rtNote, txtUploadFile)
        Dim item = CType(rGrid.SelectedItems(rGrid.SelectedItems.Count - 1), GridDataItem)
        rtFromdate.Text = item.GetDataKeyValue("FROM_DATE")
        rtTodate.Text = item.GetDataKeyValue("TO_DATE")
        rtNote.Text = item.GetDataKeyValue("NOTE")
        hidID.Value = item.GetDataKeyValue("ID")
        rtBackground.Text = item.GetDataKeyValue("BACKGROUND")
        txtUploadFile.Text = item.GetDataKeyValue("FILEPATH")
    End Sub
    Private Sub rgEmployee_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rGrid.ItemCommand
        Dim fileName As String = ""
        Dim chuoi As String = ""
        If e.CommandName = "ViewImage" Then
            If e.CommandArgument <> "" Then
                Dim filepath As String = Server.MapPath("~/Static/Images/" & e.CommandArgument)
                Dim strName As String = IO.Path.GetExtension(filepath).ToUpper()

                If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Then
                    Show(e.CommandArgument)
                Else
                    ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
                End If

            Else
                ShowMessage(Translate("Chưa có file đính kèm."), NotifyType.Warning)
            End If

        End If

    End Sub
#End Region

#Region "Custom"
    Public Sub Show(strfile As Object)
        Dim script As String
        script = "var oWnd = $find('" & popupId & "');"
        script &= "oWnd.add_close(" & Me.ClientID & "_OnClientClose);"
        script &= "oWnd.setUrl('Dialog.aspx?mid=Portal&fid=ctrlViewImage&&emp=" & strfile & "');"
        script &= "oWnd.show();"
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)

    End Sub
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ChangeToolbarState()

        Catch ex As Exception

            Throw ex
        End Try
    End Sub
#End Region

End Class