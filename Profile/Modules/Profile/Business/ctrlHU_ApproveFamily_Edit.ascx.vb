Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_ApproveFamily_Edit
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
    Property lstFamilyEdit As List(Of FamilyEditDTO)
        Get
            Return ViewState(Me.ID & "_lstFamilyEdit")
        End Get
        Set(value As List(Of FamilyEditDTO))
            ViewState(Me.ID & "_lstFamilyEdit") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 12/07/2017 11:24
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de hien thi thong tin trang
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
    ''' 12/07/2017 11:24
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de khoi tao cac thiet lap ban dau cho cac control tren page
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
    ''' <lastupdate>
    ''' 12/07/2017 11:24
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc khoi tao cac thiet lap cho cac control tren page
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
    ''' <lastupdate>
    ''' 12/07/2017 11:24
    ''' </lastupdate>
    ''' <summary>
    ''' Lam moi thong tin, thiet lap cac control tren page
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
    ''' <lastupdate>
    ''' 12/07/2017 11:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho radgrid rgData
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

    Private Sub rgData_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Try
            Using rep As New ProfileBusinessRepository
                Dim dic As Dictionary(Of String, String) = rep.GetChangedFamilyList(lstFamilyEdit)
                For Each i As GridDataItem In rgData.Items
                    If dic.Keys.Contains(i.GetDataKeyValue("ID")) Then
                        Dim colNames As String = dic(i.GetDataKeyValue("ID"))
                        If colNames <> "" Then
                            If colNames.Contains(",") Then
                                For Each colName As String In colNames.Split(",")
                                    If colName = "IS_OWNER" Or colName = "IS_PASS" Or colName = "IS_DEDUCT" Then
                                        i(colName).BackColor = Drawing.Color.Red
                                    Else
                                        i(colName).ForeColor = Drawing.Color.Red
                                    End If
                                Next
                            Else
                                If colNames = "IS_OWNER" Or colNames = "IS_PASS" Or colNames = "IS_DEDUCT" Then
                                    i(colNames).BackColor = Drawing.Color.Red
                                Else
                                    i(colNames).ForeColor = Drawing.Color.Red
                                End If
                            End If
                        End If
                    End If
                Next
            End Using
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                Dim strName = If(datarow.GetDataKeyValue("FILE_FAMILY") IsNot Nothing, datarow.GetDataKeyValue("FILE_FAMILY").ToString, "")
                If strName <> "" Then
                    datarow("DowloadCommandColumnFamily").Enabled = True
                Else
                    datarow("DowloadCommandColumnFamily").Enabled = False
                    datarow("DowloadCommandColumnFamily").CssClass = "hide-button"
                End If
                If strName.ToUpper.Contains(".JPG") Or strName.ToUpper.Contains(".GIF") Or strName.ToUpper.Contains(".PNG") Or strName.ToUpper.Contains(".JPEG") Then
                    datarow("ViewCommandColumnFamily").Enabled = True
                Else
                    datarow("ViewCommandColumnFamily").Enabled = False
                    datarow("ViewCommandColumnFamily").CssClass = "hide-button"
                End If
                Dim strName2 = If(datarow.GetDataKeyValue("FILE_NPT") IsNot Nothing, datarow.GetDataKeyValue("FILE_NPT").ToString, "")
                If strName2 <> "" Then
                    datarow("DowloadCommandColumnNPT").Enabled = True
                Else
                    datarow("DowloadCommandColumnNPT").Enabled = False
                    datarow("DowloadCommandColumnNPT").CssClass = "hide-button"
                End If
                If strName2.ToUpper.Contains(".JPG") Or strName2.ToUpper.Contains(".GIF") Or strName2.ToUpper.Contains(".PNG") Or strName2.ToUpper.Contains(".JPEG") Then
                    datarow("ViewCommandColumnNPT").Enabled = True
                Else
                    datarow("ViewCommandColumnNPT").Enabled = False
                    datarow("ViewCommandColumnNPT").CssClass = "hide-button"
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rgData_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgData.ItemCommand
        Try
            If e.CommandName = "DowloadFamily" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_FAMILY").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link = file.LINK
                Dim name = file.FILE_NAME
                ' ZipFiles(path, FileName)
                link = link.Replace("\", "(slash)")
                Dim url As String = "Download.aspx?" & "ctrlHU_ApproveFamily_Edit," & link & "," & name
                Dim str As String = "window.open('" & url + "');"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End If
            If e.CommandName = "ViewFamily" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_FAMILY").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link As String = file.LINK & "\" & file.FILE_NAME
                Dim strName As String = IO.Path.GetExtension(link).ToUpper()

                link = link.Replace("\", "(slash)")
                If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Or strName.Contains(".JPEG") Then
                    Show(link)
                Else
                    ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
                End If

            End If
            If e.CommandName = "DowloadNPT" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_NPT").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link = file.LINK
                Dim name = file.FILE_NAME
                ' ZipFiles(path, FileName)
                link = link.Replace("\", "(slash)")
                Dim url As String = "Download.aspx?" & "ctrlHU_ApproveFamily_Edit," & link & "," & name
                Dim str As String = "window.open('" & url + "');"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End If
            If e.CommandName = "ViewNPT" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE_NPT").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link As String = file.LINK & "\" & file.FILE_NAME
                Dim strName As String = IO.Path.GetExtension(link).ToUpper()

                link = link.Replace("\", "(slash)")
                If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Or strName.Contains(".JPEG") Then
                    Show(link)
                Else
                    ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
                End If

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Sub Show(strfile As Object)
        Dim script As String
        script = "var oWnd = $find('" & popupId & "');"
        script &= "oWnd.add_close(" & Me.ClientID & "_OnClientClose);"
        script &= "oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlViewImage&group=Business&state=Normal&imgUrl=" & strfile & "');"
        script &= "oWnd.show();"
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)

    End Sub
#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 12/07/2017 11:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Command khi click toolbar item duyet, huy duyet
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
    ''' <lastupdate>
    ''' 12/07/2017 11:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Selected Node Changed cua ctrlOrg
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
    ''' <lastupdate>
    ''' 12/07/2017 11:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly button command cua control ctrlMessageBox, duyet, huy duyet
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

                    rep.UpdateStatusEmployeeFamilyEdit(lstID, sContent)
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

                    rep.UpdateStatusEmployeeFamilyEdit(lstID, sContent)
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
    ''' <lastupdate>
    ''' 12/07/2017 11:24
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai cac control tren page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
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
    ''' <lastupdate>
    ''' 12/07/2017 11:24
    ''' </lastupdate>
    ''' <summary>
    ''' Tao du lieu cho filte
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileBusinessRepository
        Dim _filter As New FamilyEditDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If ctrlOrg.CurrentValue Is Nothing Then
                rgData.DataSource = New List(Of FamilyEditDTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

            SetValueObjectByRadGrid(rgData, _filter)

            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetApproveFamilyEdit(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetApproveFamilyEdit(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.lstFamilyEdit = rep.GetApproveFamilyEdit(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
                Else
                    Me.lstFamilyEdit = rep.GetApproveFamilyEdit(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
                End If
                rgData.DataSource = lstFamilyEdit
                rgData.VirtualItemCount = MaximumRows
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

#End Region

End Class