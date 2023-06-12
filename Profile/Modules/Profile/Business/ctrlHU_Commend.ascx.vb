Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_Commend
    Inherits Common.CommonView

    ''' <summary>
    ''' psp
    ''' </summary>
    ''' <remarks></remarks>
    Dim psp As New ProfileStoreProcedure

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Commends As List(Of CommendDTO)
        Get
            Return ViewState(Me.ID & "_Commends")
        End Get

        Set(ByVal value As List(Of CommendDTO))
            ViewState(Me.ID & "_Commends") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeleteCommend As CommendDTO
        Get
            Return ViewState(Me.ID & "_DeleteCommend")
        End Get

        Set(ByVal value As CommendDTO)
            ViewState(Me.ID & "_DeleteCommend") = value
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

    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get

        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    ''' <summary>
    ''' IsLoad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get

        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

    ''' <summary>
    ''' ApproveCommend
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ApproveCommend As CommendDTO
        Get
            Return ViewState(Me.ID & "_ApproveCommend")
        End Get

        Set(ByVal value As CommendDTO)
            ViewState(Me.ID & "_ApproveCommend") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If Not IsPostBack Then
                Refresh()
            End If

            UpdateControlState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            rgCommend.AllowCustomPaging = True
            rgCommend.PageSize = Common.Common.DefaultPageSize
            rgCommend.SetFilter()
            btnSearch.CausesValidation = False
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            InitControl()
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                GirdConfig(rgCommend)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarCommends

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Export,
                                       ToolbarItem.Delete, ToolbarItem.Print,
                                       ToolbarItem.Approve, ToolbarItem.Unlock)

            CType(MainToolBar.Items(6), RadToolBarButton).Text = "Mở phê duyệt"
            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_CREATE_BATCH,
            '                                                      ToolbarIcons.Add,
            '                                                      ToolbarAuthorize.Special1,
            '                                                      "Phê duyệt hàng loạt"))
            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_UNLOCK,
            '                                                      ToolbarIcons.Unlock,
            '                                                      ToolbarAuthorize.Special1,
            '                                                      "Mở phê duyệt"))

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Fill du lieu len combobox</summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            GetDataCombo()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Cập nhật trạng thái control</summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            UpdateControlEnabled(False)
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstCommend As New List(Of CommendDTO)
                    For Each item As GridDataItem In rgCommend.SelectedItems
                        lstCommend.Add(New CommendDTO With {.ID = item.GetDataKeyValue("ID")})
                    Next

                    Dim isSuccess As Boolean = True
                    For Each item In lstCommend
                        If Not rep.DeleteCommend(item) Then
                            isSuccess = False
                            Exit For
                        End If
                    Next

                    If isSuccess Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If

                Case CommonMessage.STATE_APPROVE
                    Dim lst As New List(Of Decimal)
                    For Each item As GridDataItem In rgCommend.SelectedItems
                        lst.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ApproveListCommend(lst, "PD") Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If

                Case CommonMessage.ACTION_UNLOCKED
                    Dim lst As New List(Of Decimal)
                    For Each item As GridDataItem In rgCommend.SelectedItems
                        lst.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ApproveListCommend(lst, "CPD") Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Cập nhật lại dữ liệu của grid</summary>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If Not IsPostBack Then
                rgCommend.Rebind()
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgCommend.Rebind()

                        SelectedItemDataGridByKey(rgCommend, IDSelect, , rgCommend.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgCommend.CurrentPageIndex = 0
                        rgCommend.MasterTableView.SortExpressions.Clear()
                        rgCommend.Rebind()
                        SelectedItemDataGridByKey(rgCommend, IDSelect, )
                    Case "Cancel"
                        rgCommend.MasterTableView.ClearSelectedItems()
                End Select
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EDIT
                    'Dim rep As New ProfileBusinessRepository
                    Dim gId = Utilities.ObjToDecima(rgCommend.SelectedValue)
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgCommend.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each item As GridDataItem In rgCommend.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") IsNot Nothing AndAlso item.GetDataKeyValue("STATUS_ID") = ProfileCommon.COMMEND_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Khen thưởng đã phê duyệt. Không thể xóa."), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_PRINT
                    'Dim repval As New ProfileBusinessRepository
                    'Dim listID As New List(Of Decimal)
                    'listID.Add(rgCommend.SelectedValue)
                    'If repval.ValidateBusiness("HU_COMMEND", "ID", listID) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                    '    Exit Sub
                    'End If

                    'If rgCommend.SelectedItems.Count = 0 Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    'If rgCommend.SelectedItems.Count > 1 Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    'Dim dtData As DataTable
                    'Dim folderName As String = ""
                    'Dim filePath As String = ""
                    'Dim extension As String = ""
                    'Dim iError As Integer = 0
                    'Dim item As GridDataItem = rgCommend.SelectedItems(0)

                    '' Kiểm tra + lấy thông tin trong database
                    'Using rep As New ProfileRepository
                    '    dtData = rep.GetHU_DataDynamic(item.GetDataKeyValue("ID"),
                    '                                   ProfileCommon.HU_TEMPLATE_TYPE.TERMINATE_ID,
                    '                                   folderName)
                    '    If dtData.Rows.Count = 0 Then
                    '        ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
                    '        Exit Sub
                    '    End If
                    '    If folderName = "" Then
                    '        ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                    '        Exit Sub
                    '    End If
                    'End Using

                    '' Kiểm tra file theo thông tin trong database
                    'If Not Utilities.GetTemplateLinkFile(2,
                    '                                     folderName,
                    '                                     filePath,
                    '                                     extension,
                    '                                     iError) Then
                    '    Select Case iError
                    '        Case 1
                    '            ShowMessage("Biểu mẫu không tồn tại", NotifyType.Warning)
                    '            Exit Sub
                    '    End Select
                    'End If

                    '' Export file mẫu
                    'Using word As New WordCommon
                    '    word.ExportMailMerge(filePath,
                    '                         item.GetDataKeyValue("ID") & "_" & _
                    '                         Format(Date.Now, "yyyyMMddHHmmss"),
                    '                         dtData,
                    '                         Response)
                    'End Using

                Case CommonMessage.TOOLBARITEM_APPROVE
                    If rgCommend.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each item As GridDataItem In rgCommend.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") IsNot Nothing AndAlso item.GetDataKeyValue("STATUS_ID") = ProfileCommon.COMMEND_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Khen thưởng đã phê duyệt. Vui lòng kiểm tra lại."), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_APPROVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgCommend.ExportExcel(Server, Response, dtData, "Commend")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgCommend.ExportExcel(Server, Response, dtData, "Commend")
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_CREATE_BATCH
                    BatchApproveCommend("P")
                Case CommonMessage.TOOLBARITEM_UNLOCK
                    If rgCommend.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each item As GridDataItem In rgCommend.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") IsNot Nothing AndAlso item.GetDataKeyValue("STATUS_ID") = ProfileCommon.COMMEND_STATUS.WAIT_APPROVE_ID Then
                            ShowMessage(Translate("Khen thưởng đang chờ phê duyệt. Vui lòng kiểm tra lại."), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = "Bạn có chắc chắn muốn mở phê duyệt ?"
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_UNLOCK
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
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
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_APPROVE
                UpdateControlState()
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_UNLOCK And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.ACTION_UNLOCKED
                UpdateControlState()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Event button tim kiem theo dieu kien tu ngay den ngay</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            rgCommend.CurrentPageIndex = 0
            rgCommend.MasterTableView.SortExpressions.Clear()
            rgCommend.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            CreateDataFilter()
            Refresh()
            rgCommend.CurrentPageIndex = 0
            rgCommend.MasterTableView.SortExpressions.Clear()
            rgCommend.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgCommend.NeedDataSource
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

    'Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCommend.ItemDataBound
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
    '            'If CommonConfig.APP_SETTING_11() Then
    '            '    datarow("SIGNER_NAME").Visible = False
    '            '    datarow("SIGNER_TITLE").Visible = False
    '            '    datarow("SIGN_DATE").Visible = False
    '            'End If
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    Private Sub rgCommend_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCommend.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Cập nhật trạng thái các control</summary>
    ''' <param name="bCheck"></param>
    ''' <remarks></remarks>
    Private Sub UpdateControlEnabled(ByVal bCheck As Boolean)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            ctrlOrg.Enabled = Not bCheck
            Utilities.EnabledGrid(rgCommend, Not bCheck, False)
            rdFromDate.Enabled = Not bCheck
            rdToDate.Enabled = Not bCheck
            btnSearch.Enabled = Not bCheck

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý phê duyệt khen thưởng</summary>
    ''' <remarks></remarks>
    Private Sub BatchApproveCommend(ByVal acti As String)
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lstID As New List(Of Decimal)

        Try
            '1. Check có rows nào được select hay không
            If rgCommend Is Nothing OrElse rgCommend.SelectedItems.Count <= 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If

            Dim listCon As New List(Of ContractDTO)

            For Each dr As Telerik.Web.UI.GridDataItem In rgCommend.SelectedItems
                Dim ID As New Decimal
                If dr("STATUS_ID").Text.Equals("714") And acti = "P" Then ' da phe duyet
                    ID = dr.GetDataKeyValue("ID")
                    lstID.Add(ID)
                Else
                    ID = dr.GetDataKeyValue("ID")
                    lstID.Add(ID)
                End If
            Next

            If lstID.Count > 0 Then
                'Dim bCheckHasfile = rep.CheckHasFileComend(lstID)
                For Each item As GridDataItem In rgCommend.SelectedItems
                    If item.GetDataKeyValue("STATUS_ID") = 714 And acti = "P" Then
                        ShowMessage(Translate("Bản ghi đã phê duyệt."), NotifyType.Warning)
                        Exit Sub
                    End If
                    If item.GetDataKeyValue("STATUS_ID") = 715 And acti = "M" Then
                        ShowMessage(Translate("Bản ghi đang chờ phê duyệt."), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                'If bCheckHasfile = 1 Then
                '    ShowMessage(Translate("Duyệt khi tất cả các record đã có tập tin đính kèm,bạn kiểm tra lại"), NotifyType.Warning)
                '    Exit Sub
                'End If
                If rep.ApproveListCommend(lstID, acti) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgCommend.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            Else
                If acti = "P" Then
                    ShowMessage("Các khen thưởng được chọn đã được phê duyệt", NotifyType.Information)
                Else
                    ShowMessage("Các khen thưởng được chọn đang chờ phê duyệt", NotifyType.Information)
                End If
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load dữ liệu cho combobox</summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_COMMEND_OBJ = True
                ListComboData.GET_COMMEND_STATUS = True
                rep.GetComboList(ListComboData)
            End If

            FillDropDownList(cboStatus, ListComboData.LIST_COMMEND_STATUS, "NAME_VN", "ID", Common.Common.SystemLanguage, False)
            FillDropDownList(cboCommendObj, ListComboData.LIST_COMMEND_OBJ, "NAME_VN", "ID", Common.Common.SystemLanguage, True)

            cboCommendObj.SelectedIndex = 1 'default: khen thuong ca nhan 
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Load data len grid</summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        CreateDataFilter = Nothing
        Dim _filter As New CommendDTO
        _filter.param = New ParamDTO
        Dim rep As New ProfileBusinessRepository
        Dim bCheck As Boolean = False
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.param.ORG_ID = Utilities.ObjToDecima(ctrlOrg.CurrentValue)
            Else
                rgCommend.DataSource = New List(Of CommendDTO)
                '_filter.param.ORG_ID = psp.GET_ID_ORG()
                Exit Function
            End If

            _filter.param.IS_DISSOLVE = ctrlOrg.IsDissolve

            If txtEmployee.Text <> "" Then
                _filter.EMPLOYEE_CODE = txtEmployee.Text
            End If

            If rdFromDate.SelectedDate IsNot Nothing Then
                _filter.EFFECT_DATE = rdFromDate.SelectedDate
            End If

            If rdToDate.SelectedDate IsNot Nothing Then
                _filter.EXPIRE_DATE = rdToDate.SelectedDate
            End If

            If cboCommendObj.SelectedValue <> "" Then
                _filter.COMMEND_OBJ = cboCommendObj.SelectedValue
            End If

            If cboStatus.SelectedValue <> "" Then
                _filter.STATUS_ID = cboStatus.SelectedValue
            End If

            _filter.IS_TERMINATE = chkChecknghiViec.Checked

            _filter.FID = Me.ViewName

            'If chkCheckNotEmp.Checked Then
            '    _filter.NotEmp = 1
            'Else
            '    _filter.NotEmp = 0
            'End If

            SetValueObjectByRadGrid(rgCommend, _filter)

            Dim MaximumRows As Integer
            Dim Sorts As String = rgCommend.MasterTableView.SortExpressions.GetSortString()

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetCommend(_filter, Sorts).ToTable()
                Else
                    Return rep.GetCommend(_filter).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.Commends = rep.GetCommend(_filter, rgCommend.CurrentPageIndex, rgCommend.PageSize, MaximumRows, Sorts)
                Else
                    Me.Commends = rep.GetCommend(_filter, rgCommend.CurrentPageIndex, rgCommend.PageSize, MaximumRows)
                End If
                rep.Dispose()
                rgCommend.VirtualItemCount = MaximumRows
                rgCommend.DataSource = Me.Commends
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    'Private Sub rgCommend_ColumnCreated(sender As Object, e As GridColumnCreatedEventArgs) Handles rgCommend.ColumnCreated
    '    Try
    '        If CommonConfig.APP_SETTING_11() Then
    '            If e.Column.UniqueName = "SIGNER_NAME" Or e.Column.UniqueName = "SIGNER_TITLE" Or e.Column.UniqueName = "SIGN_DATE" Then
    '                e.Column.Visible = False
    '            End If
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub rgCommend_PreRender(sender As Object, e As EventArgs) Handles rgCommend.PreRender
        Try
            If CommonConfig.APP_SETTING_11() Then
                rgCommend.MasterTableView.GetColumn("SIGNER_NAME").Visible = False
                rgCommend.MasterTableView.GetColumn("SIGNER_TITLE").Visible = False
                rgCommend.MasterTableView.GetColumn("SIGN_DATE").Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class