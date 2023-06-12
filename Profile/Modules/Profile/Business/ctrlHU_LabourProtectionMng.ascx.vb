Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_LabourProtectionMng
    Inherits Common.CommonView
    Protected WithEvents LabourProtectionMng As ViewBase
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
#Region "Property"
#End Region

#Region "Page"
    ''' <summary>
    ''' Khoi tao, load page, set trang thai page, trang thai control
    ''' set thuoc tinh filter cho grid
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Me.MainToolBar = tbarMenu
            Me.ctrlMessageBox.Listener = Me
            Me.rgLabourProtection.SetFilter()
            If Not IsPostBack Then
                Call Refresh()
            End If
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Khoi tao control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            ctrlOrg.SelectFirstNode = True
            rgLabourProtection.AllowCustomPaging = True
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Khoi tao, load menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Export, ToolbarItem.Seperator,
                                       ToolbarItem.Delete)

            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Reload lai grid khi thay doi trang thai page
    ''' </summary>
    ''' <param name="Message">Khong su dung</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        'Dim startTime As DateTime = DateTime.UtcNow 
        'Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() 
        'Dim rep As New ProfileBusinessRepository
        'Try
        '    ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
        '    CurrentState = CommonMessage.STATE_NORMAL
        '    rgLabourProtection.Rebind()
        '    _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "") 
        'Catch ex As Exception
        '    DisplayException(Me.ViewName, Me.ID, ex)
        '    _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "") 
        'End Try
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgLabourProtection.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgLabourProtection.CurrentPageIndex = 0
                        rgLabourProtection.MasterTableView.SortExpressions.Clear()
                        rgLabourProtection.Rebind()

                    Case "Cancel"
                        rgLabourProtection.MasterTableView.ClearSelectedItems()
                End Select
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <summary>
    ''' Event click item tren menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objOrgFunction As New OrganizationDTO
        Dim sError As String = ""
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    'Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_LabourProtectionMngNewEdit&group=Business")
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                    If rgLabourProtection.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgLabourProtection.SelectedItems.Count > 1 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgLabourProtection.SelectedItems
                        ID = item.GetDataKeyValue("ID")
                    Next
                    'Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_LabourProtectionMngNewEdit&group=Business&gUID=" + ID)
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgLabourProtection.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each item As GridDataItem In rgLabourProtection.SelectedItems
                        If item.GetDataKeyValue("WORK_STATUS") = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                            ShowMessage(Translate("Nhân viên nghỉ việc. Không được xóa thông tin."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT

                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgLabourProtection.ExportExcel(Server, Response, dtData, "Labour Protection")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Event selected node tren treeview-> load lai thong tin tren grid theo node
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgLabourProtection.CurrentPageIndex = 0
            rgLabourProtection.MasterTableView.SortExpressions.Clear()
            rgLabourProtection.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Event Yes/No message hoi xoa du lieu-> process xoa du lieu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New ProfileBusinessRepository
                Dim objD As New List(Of LabourProtectionMngDTO)
                Dim lst As New List(Of Decimal)
                For Each item As GridDataItem In rgLabourProtection.SelectedItems
                    Dim obj As New LabourProtectionMngDTO
                    obj.ID = Utilities.ObjToDecima(item.GetDataKeyValue("ID"))
                    objD.Add(obj)
                Next
                If rep.DeleteLabourProtectionMng(objD) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    Refresh("UpdateView")
                End If
                rep.Dispose()
                UpdateControlState()

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Set, reload datasource cho grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgLabourProtection.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Event click button tim kiem
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        If IIf(rdFrom.SelectedDate Is Nothing, Now, rdFrom.SelectedDate) > IIf(rdTo.SelectedDate Is Nothing, Now, rdTo.SelectedDate) Then
            ShowMessage(Translate("Từ ngày không được lớn hơn đến ngày!"), NotifyType.Warning)
            Exit Sub
        End If
        Try
            rgLabourProtection.CurrentPageIndex = 0
            rgLabourProtection.MasterTableView.SortExpressions.Clear()
            rgLabourProtection.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    'Private Sub rgLabourProtection_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgLabourProtection.ItemDataBound
    '    Dim startTime As DateTime = DateTime.UtcNow 
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() 
    '    Try
    '        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
    '            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "") 
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "") 
    '    End Try

    'End Sub
#End Region

#Region "Custom"
    ''' <summary>
    ''' Load data from DB to grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New LabourProtectionMngDTO
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgLabourProtection.DataSource = New List(Of WorkingDTO)
                Exit Function
            End If
            SetValueObjectByRadGrid(rgLabourProtection, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgLabourProtection.MasterTableView.SortExpressions.GetSortString()
            Dim LabourProtectionMng As New List(Of LabourProtectionMngDTO)
            _filter.IS_TERMINATE = chkChecknghiViec.Checked
            If rdFrom.SelectedDate IsNot Nothing Then
                _filter.FROM_DATE_SEARCH = rdFrom.SelectedDate
            End If
            If rdTo.SelectedDate IsNot Nothing Then
                _filter.TO_DATE_SEARCH = rdTo.SelectedDate
            End If

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetLabourProtectionMng(_filter, ctrlOrg.IsDissolve, Sorts).ToTable()
                Else
                    Return rep.GetLabourProtectionMng(_filter, ctrlOrg.IsDissolve).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    LabourProtectionMng = rep.GetLabourProtectionMng(_filter, ctrlOrg.IsDissolve, rgLabourProtection.CurrentPageIndex, rgLabourProtection.PageSize, MaximumRows, Sorts)
                Else
                    LabourProtectionMng = rep.GetLabourProtectionMng(_filter, ctrlOrg.IsDissolve, rgLabourProtection.CurrentPageIndex, rgLabourProtection.PageSize, MaximumRows)
                End If
                rgLabourProtection.VirtualItemCount = MaximumRows
                rgLabourProtection.DataSource = LabourProtectionMng
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