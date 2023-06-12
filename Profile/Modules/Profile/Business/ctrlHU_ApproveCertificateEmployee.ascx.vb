Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_ApproveCertificateEmployee
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
    Property lstEmpCertificateEdit As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        Get
            Return ViewState(Me.ID & "_lstEmpCertificateEdit")
        End Get
        Set(value As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT))
            ViewState(Me.ID & "_lstEmpCertificateEdit") = value
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

                    rep.UpdateStatusEmployeeCetificateEdit(lstID, sContent)
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

                    rep.UpdateStatusEmployeeCetificateEdit(lstID, sContent)
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
            If e.CommandName = "Dowload" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link = file.LINK
                Dim name = file.FILE_NAME
                ' ZipFiles(path, FileName)
                link = link.Replace("\", "(slash)")
                Dim url As String = "Download.aspx?" & "ctrlHU_ApproveWorkingBefore_Edit," & link & "," & name
                Dim str As String = "window.open('" & url + "');"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End If
            If e.CommandName = "View" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE").ToString()
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
    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Using rep As New ProfileBusinessRepository
            Dim dic As Dictionary(Of String, String) = rep.GetChangedEmployeeCertificateList(lstEmpCertificateEdit)
            For Each i As GridDataItem In rgData.Items
                If dic.Keys.Contains(i.GetDataKeyValue("ID")) Then
                    Dim colNames As String = dic(i.GetDataKeyValue("ID"))
                    If colNames <> "" Then
                        If colNames.Contains(",") Then
                            For Each colName As String In colNames.Split(",")
                                If colName = "IS_MAIN" Then
                                    i(colName).BackColor = Drawing.Color.Red
                                Else
                                    i(colName).ForeColor = Drawing.Color.Red
                                End If
                            Next
                        Else
                            If colNames = "IS_MAIN" Then
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
            Dim strName = If(datarow.GetDataKeyValue("FILE_NAME") IsNot Nothing, datarow.GetDataKeyValue("FILE_NAME").ToString, "")
            If strName <> "" Then
                datarow("DowloadCommandColumn").Visible = True
            Else
                datarow("DowloadCommandColumn").Visible = False
            End If
            If strName.ToUpper.Contains(".JPG") Or strName.ToUpper.Contains(".GIF") Or strName.ToUpper.Contains(".PNG") Then
                datarow("ViewCommandColumn").Visible = True
            Else
                datarow("ViewCommandColumn").Visible = False
            End If
        End If
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
        Dim _filter As New HU_PRO_TRAIN_OUT_COMPANYDTOEDIT
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If ctrlOrg.CurrentValue Is Nothing Then
                rgData.DataSource = New List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

            SetValueObjectByRadGrid(rgData, _filter)

            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetApproveEmployeeCertificateEdit(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetApproveEmployeeCertificateEdit(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.lstEmpCertificateEdit = rep.GetApproveEmployeeCertificateEdit(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
                Else
                    Me.lstEmpCertificateEdit = rep.GetApproveEmployeeCertificateEdit(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
                End If
                rgData.DataSource = lstEmpCertificateEdit
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