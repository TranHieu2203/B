Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_AssetMng
    Inherits Common.CommonView

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' AssetMng
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AssetMng As AssetMngDTO
        Get
            Return ViewState(Me.ID & "_AssetMng")
        End Get

        Set(ByVal value As AssetMngDTO)
            ViewState(Me.ID & "_AssetMng") = value
        End Set
    End Property

    ''' <summary>
    ''' AssetMngs
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AssetMngs As List(Of AssetMngDTO)
        Get
            Return ViewState(Me.ID & "_AssetMngs")
        End Get

        Set(ByVal value As List(Of AssetMngDTO))
            ViewState(Me.ID & "_AssetMngs") = value
        End Set
    End Property

    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get

        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Me.MainToolBar = tbarAssetMngs
            Me.ctrlMessageBox.Listener = Me
            Me.rgAssetMng.SetFilter()

            If Not IsPostBack Then
                Call Refresh()
            End If

            UpdateControlState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgAssetMng.AllowCustomPaging = True

            InitControl()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Fill du lieu len combobox</summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        'Try
        'Catch ex As Exception
        '    DisplayException(Me.ViewName, Me.ID, ex)
        '    _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex,"") 
        'End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarAssetMngs

            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create, ToolbarItem.Edit,
                                       ToolbarItem.Delete, ToolbarItem.Export)

            Refresh("")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgAssetMng.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgAssetMng.CurrentPageIndex = 0
                        rgAssetMng.MasterTableView.SortExpressions.Clear()
                        rgAssetMng.Rebind()

                    Case "Cancel"
                        rgAssetMng.MasterTableView.ClearSelectedItems()

                    Case "DeleteView"
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgAssetMng.CurrentPageIndex = 0
                        rgAssetMng.MasterTableView.SortExpressions.Clear()
                        rgAssetMng.Rebind()
                End Select
            End If

            UpdateControlState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objOrgFunction As New OrganizationDTO
        Dim sError As String = ""
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW

                Case CommonMessage.TOOLBARITEM_EDIT
                    For Each item As GridDataItem In rgAssetMng.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") <> ProfileCommon.HU_ASSET_MNG_STATUS.ASSET_WAIT Then
                            ShowMessage(Translate("Không thể thực hiện thao tác sửa với bản ghi này."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    CurrentState = CommonMessage.STATE_EDIT

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgAssetMng.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgAssetMng.SelectedItems.Count = 1 Then
                        For Each item As GridDataItem In rgAssetMng.SelectedItems
                            If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.HU_ASSET_MNG_STATUS.ASSET_TRANSFER Then
                                ShowMessage(Translate("Tài sản đã bàn giao không được phép xóa. Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.HU_ASSET_MNG_STATUS.ASSET_NEW Then
                                ShowMessage(Translate("Tài sản đã cấp không được phép xóa. Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                        Next
                    End If

                    Dim intCountWait As Integer = 0
                    Dim intCountTranfer As Integer = 0
                    Dim intCountNew As Integer = 0
                    For Each item As GridDataItem In rgAssetMng.SelectedItems
                        If item.GetDataKeyValue("WORK_STATUS") = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                            ShowMessage(Translate("Nhân viên nghỉ việc. Không được xóa thông tin."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.HU_ASSET_MNG_STATUS.ASSET_WAIT Then
                            intCountWait = intCountWait + 1
                        End If

                        If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.HU_ASSET_MNG_STATUS.ASSET_TRANSFER Then
                            intCountTranfer = intCountTranfer + 1
                        End If

                        If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.HU_ASSET_MNG_STATUS.ASSET_NEW Then
                            intCountNew = intCountNew + 1
                        End If
                    Next

                    If intCountWait = 0 Then
                        If intCountNew = 0 Then
                            If intCountTranfer <> 0 Then
                                ShowMessage(Translate("Tài sản đã bàn giao không được phép xóa. Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                        Else
                            If intCountTranfer = 0 Then
                                ShowMessage(Translate("Tài sản đã cấp không được phép xóa. Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                        End If

                        ShowMessage(Translate("Tài sản đã cấp, đã bàn giao không được phép xóa. Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)

                        If dtDatas.Rows.Count > 0 Then
                            rgAssetMng.ExportExcel(Server, Response, dtDatas, "AssetMng")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            rgAssetMng.CurrentPageIndex = 0
            rgAssetMng.MasterTableView.SortExpressions.Clear()
            rgAssetMng.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Event button tim kiem theo dieu kien tu ngay den ngay</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgAssetMng.CurrentPageIndex = 0
            rgAssetMng.MasterTableView.SortExpressions.Clear()
            rgAssetMng.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary> 
    ''' Event Yes/No messagebox hoi Xoa du lieu
    ''' Update lai trang thai control sau khi process Xoa
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
                Dim objD As New List(Of OccupationalSafetyDTO)
                Dim objAssetMngD As New List(Of AssetMngDTO)
                Dim lst As New List(Of Decimal)

                For Each item As GridDataItem In rgAssetMng.SelectedItems
                    Dim obj As New OccupationalSafetyDTO
                    Dim objAssetMng As New AssetMngDTO
                    If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.HU_ASSET_MNG_STATUS.ASSET_WAIT Then
                        obj.ID = Utilities.ObjToDecima(item.GetDataKeyValue("ID"))
                        objAssetMng.ID = Utilities.ObjToDecima(item.GetDataKeyValue("ID"))

                        objD.Add(obj)
                        objAssetMngD.Add(objAssetMng)
                    End If
                Next

                If rep.DeleteOccupationalSafety(objD) And rep.DeleteAssetMng(objAssetMngD) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    Refresh("DeleteView")
                End If
                rep.Dispose()
                UpdateControlState()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgAssetMng.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            CreateDataFilter()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>  Load data len grid </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileBusinessRepository
        Dim _filter As New AssetMngDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim _param = New ParamDTO With {.ORG_ID = ctrlOrg.CurrentValue, _
                                           .IS_DISSOLVE = ctrlOrg.IsDissolve, _
                                           .IS_FULL = True}

            SetValueObjectByRadGrid(rgAssetMng, _filter)

            If rdFrom.SelectedDate.HasValue Then
                _filter.FROM_DATE_SEARCH = rdFrom.SelectedDate
            End If

            If rdTo.SelectedDate.HasValue Then
                _filter.TO_DATE_SEARCH = rdTo.SelectedDate
            End If

            _filter.IS_TERMINATE = chkChecknghiViec.Checked

            Dim MaximumRows As Integer
            Dim Sorts As String = rgAssetMng.MasterTableView.SortExpressions.GetSortString()

            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.AssetMngs = rep.GetAssetMng(_filter, _param, rgAssetMng.CurrentPageIndex, rgAssetMng.PageSize, MaximumRows, Sorts)
                Else
                    Me.AssetMngs = rep.GetAssetMng(_filter, _param, rgAssetMng.CurrentPageIndex, rgAssetMng.PageSize, MaximumRows)
                End If
            Else
                If Sorts IsNot Nothing Then
                    Return rep.GetAssetMng(_filter, _param, 0, Integer.MaxValue, 0, Sorts).ToTable
                Else
                    Return rep.GetAssetMng(_filter, _param, 0, Integer.MaxValue, 0).ToTable
                End If
            End If
            rep.Dispose()
            rgAssetMng.VirtualItemCount = MaximumRows
            rgAssetMng.DataSource = Me.AssetMngs

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

#End Region

End Class