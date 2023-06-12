Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_ApproveEmployee_Edit
    Inherits Common.CommonView

    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
#Region "Property"
    Public Property popupId As String
    Public Property AjaxManagerId As String
    Property lstEmpEdit As List(Of EmployeeEditDTO)
        Get
            Return ViewState(Me.ID & "_lstEmpEdit")
        End Get
        Set(value As List(Of EmployeeEditDTO))
            ViewState(Me.ID & "_lstEmpEdit") = value
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
    ''' <summary>
    ''' Khởi tạo, load control, set thuộc tính cho grid
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            If Not IsPostBack Then
                ViewConfig(LeftPane)
                GirdConfig(rgData)
            End If
            InitControl()
            popupId = CType(Me.Page, AjaxPage).PopupWindow.ClientID
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo, load menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.MainToolBar = tbarWorkings
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Approve, ToolbarItem.Reject)
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' reload page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
        Try
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation

            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgData_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Try
            Using rep As New ProfileBusinessRepository
                Dim dic As Dictionary(Of String, String) = rep.GetChangedCVList(lstEmpEdit)
                For Each i As GridDataItem In rgData.Items
                    Dim colNames As String = dic(i.GetDataKeyValue("ID"))
                    If colNames <> "" Then
                        If colNames.Contains(",") Then
                            For Each colName As String In colNames.Split(",")
                                i(colName).ForeColor = Drawing.Color.Red
                            Next
                        Else
                            i(colNames).ForeColor = Drawing.Color.Red
                        End If
                    End If
                Next
            End Using
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                Dim strName = If(datarow.GetDataKeyValue("FILE_ADDRESS") IsNot Nothing, datarow.GetDataKeyValue("FILE_ADDRESS").ToString, "")
                If strName <> "" Then
                    datarow("DowloadCommandColumnAddress").Enabled = True
                Else
                    datarow("DowloadCommandColumnAddress").Enabled = False
                    datarow("DowloadCommandColumnAddress").CssClass = "hide-button"
                End If
                If strName.ToUpper.Contains(".JPG") Or strName.ToUpper.Contains(".GIF") Or strName.ToUpper.Contains(".PNG") Then
                    datarow("ViewCommandColumnAddress").Enabled = True
                Else
                    datarow("ViewCommandColumnAddress").Enabled = False
                    datarow("ViewCommandColumnAddress").CssClass = "hide-button"
                End If
                Dim strName2 = If(datarow.GetDataKeyValue("FILE_BANK") IsNot Nothing, datarow.GetDataKeyValue("FILE_BANK").ToString, "")
                If strName2 <> "" Then
                    datarow("DowloadCommandColumnBank").Enabled = True
                Else
                    datarow("DowloadCommandColumnBank").Enabled = False
                    datarow("DowloadCommandColumnBank").CssClass = "hide-button"
                End If
                If strName2.ToUpper.Contains(".JPG") Or strName2.ToUpper.Contains(".GIF") Or strName2.ToUpper.Contains(".PNG") Then
                    datarow("ViewCommandColumnBank").Enabled = True
                Else
                    datarow("ViewCommandColumnBank").Enabled = False
                    datarow("ViewCommandColumnBank").CssClass = "hide-button"
                End If
                Dim strName3 = If(datarow.GetDataKeyValue("IMAGE") IsNot Nothing, datarow.GetDataKeyValue("IMAGE").ToString, "")
                If strName3 <> "" Then
                    datarow("DowloadCommandColumnImage").Enabled = True
                Else
                    datarow("DowloadCommandColumnImage").Enabled = False
                    datarow("DowloadCommandColumnImage").CssClass = "hide-button"
                End If
                If strName3.ToUpper.Contains(".JPG") Or strName3.ToUpper.Contains(".GIF") Or strName3.ToUpper.Contains(".PNG") Then
                    datarow("ViewCommandColumnImage").Enabled = True
                Else
                    datarow("ViewCommandColumnImage").Enabled = False
                    datarow("ViewCommandColumnImage").CssClass = "hide-button"
                End If
                Dim strName4 = If(datarow.GetDataKeyValue("FILE_CMND") IsNot Nothing, datarow.GetDataKeyValue("FILE_CMND").ToString, "")
                If strName4 <> "" Then
                    datarow("DowloadCommandColumnCMND").Enabled = True
                Else
                    datarow("DowloadCommandColumnCMND").Enabled = False
                    datarow("DowloadCommandColumnCMND").CssClass = "hide-button"
                End If

                Dim strName4b = If(datarow.GetDataKeyValue("FILE_CMND_BACK") IsNot Nothing, datarow.GetDataKeyValue("FILE_CMND_BACK").ToString, "")
                If strName4b <> "" Then
                    datarow("DowloadCommandColumnCMNDBack").Enabled = True
                Else
                    datarow("DowloadCommandColumnCMNDBack").Enabled = False
                    datarow("DowloadCommandColumnCMNDBack").CssClass = "hide-button"
                End If
                If strName4b.ToUpper.Contains(".JPG") Or strName4.ToUpper.Contains(".GIF") Or strName4.ToUpper.Contains(".PNG") Then
                    datarow("ViewCommandColumnCMNDBack").Enabled = True
                Else
                    datarow("ViewCommandColumnCMNDBack").Enabled = False
                    datarow("ViewCommandColumnCMNDBack").CssClass = "hide-button"
                End If

                If strName4.ToUpper.Contains(".JPG") Or strName4.ToUpper.Contains(".GIF") Or strName4.ToUpper.Contains(".PNG") Then
                    datarow("ViewCommandColumnCMND").Enabled = True
                Else
                    datarow("ViewCommandColumnCMND").Enabled = False
                    datarow("ViewCommandColumnCMND").CssClass = "hide-button"
                End If
                Dim strName5 = If(datarow.GetDataKeyValue("FILE_OTHER") IsNot Nothing, datarow.GetDataKeyValue("FILE_OTHER").ToString, "")
                If strName5 <> "" Then
                    datarow("DowloadCommandColumnOther").Enabled = True
                Else
                    datarow("DowloadCommandColumnOther").Enabled = False
                    datarow("DowloadCommandColumnOther").CssClass = "hide-button"
                End If
                If strName5.ToUpper.Contains(".JPG") Or strName5.ToUpper.Contains(".GIF") Or strName5.ToUpper.Contains(".PNG") Then
                    datarow("ViewCommandColumnOther").Enabled = True
                Else
                    datarow("ViewCommandColumnOther").Enabled = False
                    datarow("ViewCommandColumnOther").CssClass = "hide-button"
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Reload, load data cho grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CreateDataFilter()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub


#End Region

#Region "Event"
    ''' <summary>
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_APPROVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If txtRemark.Text.Length > 0 Then
                        ShowMessage(Translate("Không được nhập lý do không phê duyệt!"), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_APPROVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_REJECT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If txtRemark.Text.Trim = "" Then
                        ShowMessage(Translate("Bạn phải nhập lý do không phê duyệt. "), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_UNAPPROVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_UNAPPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Event selected Node trên treeview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgData.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Event Yes.No message popup hỏi phê duyệt, không phê duyệt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim sContent As String = ""
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Using rep As New ProfileBusinessRepository
                    Dim lstID As New List(Of Decimal)
                    For Each item As GridDataItem In rgData.SelectedItems
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next
                    sContent = "2:" & txtRemark.Text.Trim

                    rep.UpdateStatusEmployeeEdit(lstID, sContent)
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgData.Rebind()
                    txtRemark.Text = ""
                End Using
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_UNAPPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Using rep As New ProfileBusinessRepository
                    Dim lstID As New List(Of Decimal)
                    For Each item As GridDataItem In rgData.SelectedItems
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next
                    sContent = "3:" & txtRemark.Text.Trim

                    rep.UpdateStatusEmployeeEdit(lstID, sContent)
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgData.Rebind()
                    txtRemark.Text = ""
                End Using
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

#End Region

#Region "Custom"
    ''' <summary>
    ''' Set trạng thái control theo trạng thai page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_APPROVE
                Case CommonMessage.STATE_REJECT
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Load data cho grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileBusinessRepository
        Dim _filter As New EmployeeEditDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If ctrlOrg.CurrentValue Is Nothing Then
                rgData.DataSource = New List(Of EmployeeEditDTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

            SetValueObjectByRadGrid(rgData, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetApproveEmployeeEdit(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetApproveEmployeeEdit(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.lstEmpEdit = rep.GetApproveEmployeeEdit(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
                Else
                    Me.lstEmpEdit = rep.GetApproveEmployeeEdit(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
                End If
                rgData.DataSource = Me.lstEmpEdit
                rgData.VirtualItemCount = MaximumRows
            End If
            'rep.Dispose()
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
        script &= "oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlViewImage&group=Business&state=Normal&imgUrl=" & strfile & "');"
        script &= "oWnd.show();"
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)

    End Sub
    Private Sub rgData_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgData.ItemCommand
        Try
            If e.CommandName = "DowloadAddress" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_ADDRESS").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link = file.LINK
                Dim name = file.FILE_NAME
                ' ZipFiles(path, FileName)
                link = link.Replace("\", "(slash)")
                Dim url As String = "Download.aspx?" & "ctrlHU_ApproveEmployee_Edit," & link & "," & name
                Dim str As String = "window.open('" & url + "');"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End If
            If e.CommandName = "ViewAddress" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_ADDRESS").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link As String = file.LINK & "\" & file.FILE_NAME
                Dim strName As String = IO.Path.GetExtension(link).ToUpper()

                link = link.Replace("\", "(slash)")
                If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Then
                    Show(link)
                Else
                    ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
                End If
            End If
            If e.CommandName = "DowloadBank" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_BANK").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link = file.LINK
                Dim name = file.FILE_NAME
                ' ZipFiles(path, FileName)
                link = link.Replace("\", "(slash)")
                Dim url As String = "Download.aspx?" & "ctrlHU_ApproveEmployee_Edit," & link & "," & name
                Dim str As String = "window.open('" & url + "');"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End If
            If e.CommandName = "ViewBank" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_BANK").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link As String = file.LINK & "\" & file.FILE_NAME
                Dim strName As String = IO.Path.GetExtension(link).ToUpper()

                link = link.Replace("\", "(slash)")
                If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Then
                    Show(link)
                Else
                    ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
                End If
            End If
            If e.CommandName = "DowloadImage" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_IMAGE").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link = file.LINK
                Dim name = file.FILE_NAME
                ' ZipFiles(path, FileName)
                link = link.Replace("\", "(slash)")
                Dim url As String = "Download.aspx?" & "ctrlHU_ApproveEmployee_Edit," & link & "," & name
                Dim str As String = "window.open('" & url + "');"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End If
            If e.CommandName = "ViewImage" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_IMAGE").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link As String = file.LINK & "\" & file.FILE_NAME
                Dim strName As String = IO.Path.GetExtension(link).ToUpper()

                link = link.Replace("\", "(slash)")
                If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Then
                    Show(link)
                Else
                    ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
                End If
            End If

            If e.CommandName = "DowloadCMND" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_CMND").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link = file.LINK
                Dim name = file.FILE_NAME
                ' ZipFiles(path, FileName)
                link = link.Replace("\", "(slash)")
                Dim url As String = "Download.aspx?" & "ctrlHU_ApproveEmployee_Edit," & link & "," & name
                Dim str As String = "window.open('" & url + "');"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End If
            If e.CommandName = "ViewCMND" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_CMND").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link As String = file.LINK & "\" & file.FILE_NAME
                Dim strName As String = IO.Path.GetExtension(link).ToUpper()

                link = link.Replace("\", "(slash)")
                If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Then
                    Show(link)
                Else
                    ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
                End If
            End If

            If e.CommandName = "DowloadCMNDBack" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_CMND_BACK").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link = file.LINK
                Dim name = file.FILE_NAME
                ' ZipFiles(path, FileName)
                link = link.Replace("\", "(slash)")
                Dim url As String = "Download.aspx?" & "ctrlHU_ApproveEmployee_Edit," & link & "," & name
                Dim str As String = "window.open('" & url + "');"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End If
            If e.CommandName = "ViewCMNDBack" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_CMND_BACK").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link As String = file.LINK & "\" & file.FILE_NAME
                Dim strName As String = IO.Path.GetExtension(link).ToUpper()

                link = link.Replace("\", "(slash)")
                If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Then
                    Show(link)
                Else
                    ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
                End If
            End If




            If e.CommandName = "DowloadOther" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_OTHER").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link = file.LINK
                Dim name = file.FILE_NAME
                ' ZipFiles(path, FileName)
                link = link.Replace("\", "(slash)")
                Dim url As String = "Download.aspx?" & "ctrlHU_ApproveEmployee_Edit," & link & "," & name
                Dim str As String = "window.open('" & url + "');"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End If
            If e.CommandName = "ViewOther" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_OTHER").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link As String = file.LINK & "\" & file.FILE_NAME
                Dim strName As String = IO.Path.GetExtension(link).ToUpper()

                link = link.Replace("\", "(slash)")
                If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Then
                    Show(link)
                Else
                    ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

End Class