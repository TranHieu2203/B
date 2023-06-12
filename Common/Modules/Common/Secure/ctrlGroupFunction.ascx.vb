Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlGroupFunction
    Inherits CommonView
    Property GroupInfo As CommonBusiness.GroupDTO
    Protected WithEvents ViewItem As ViewBase
    Protected WithEvents groupCopy As ctrlGroupCopy
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/List/" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' Obj ComboBoxData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ComboBoxData As ComboBoxDataDTO
        Get
            Return PageViewState(Me.ID & "_ComboBoxData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            PageViewState(Me.ID & "_ComboBoxData") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj ViewShowed
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewShowed As Boolean
        Get
            Return PageViewState(Me.ID & "_ViewShowed")
        End Get
        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_ViewShowed") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj SelectedItem
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SelectedItem() As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_SelectedItem") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj DeleteItemList
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeleteItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_DeleteItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_DeleteItemList") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj GroupFunction
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GroupFunction As List(Of GroupFunctionDTO)
        Get
            Return PageViewState(Me.ID & "_GroupFunction")
        End Get
        Set(ByVal value As List(Of GroupFunctionDTO))
            PageViewState(Me.ID & "_GroupFunction") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj GroupID_Old
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GroupID_Old As Decimal
        Get
            Return PageViewState(Me.ID & "_GroupID_Old")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_GroupID_Old") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj CheckState
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CheckState As GroupFunctionDTO
        Get
            If PageViewState(Me.ID & "_CheckState") Is Nothing Then
                PageViewState(Me.ID & "_CheckState") = New GroupFunctionDTO
            End If
            Return PageViewState(Me.ID & "_CheckState")
        End Get
        Set(ByVal value As GroupFunctionDTO)
            PageViewState(Me.ID & "CheckState") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj MaximumRows
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Obj isLoadPopup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' 1 - group
    ''' 0 - normal
    ''' </remarks>
    Property isLoadPopup As Integer
        Get
            Return PageViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            PageViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    ''' <summary>
    ''' dtbModule
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property dtbModule As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbModule")
        End Get

        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbModule") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Khởi tạo các command trên toolbar
    ''' Xét các trạng thái của grid rgGrid
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = rtbMain
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, _
                                                ToolbarItem.Edit, _
                                                ToolbarItem.Seperator, _
                                                ToolbarItem.Save, _
                                                ToolbarItem.Cancel, _
                                                ToolbarItem.Seperator, _
                                                ToolbarItem.Export, _
                                                ToolbarItem.Seperator, _
                                                ToolbarItem.Delete)

            Me.MainToolBar.Items.Add(Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_SYNC,
                                                          ToolbarIcons.Sync,
                                                          ToolbarAuthorize.None,
                                                        Translate("Sao chép")))

            ViewItem = Me.Register("ctrlGroupFunctionAddEdit", "Common", "ctrlGroupFunctionAddEdit", "Secure")


            rgGrid.AllowCustomPaging = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateControlState()
            UpdatePageViewState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        dtbModule = New DataTable
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'Load Combobox
            Dim rep As New CommonRepository
            If ComboBoxData Is Nothing Then
                ComboBoxData = New ComboBoxDataDTO
                ComboBoxData.GET_MODULE = True
                ComboBoxData.GET_FUNCTION_GROUP = True
                rep.GetComboList(ComboBoxData)
            End If
            'Dim dtbModule = (New CommonProgramsRepository).FillComboboxModules()
            'ComboBoxData.LIST_MODULE.Clear()
            'For Each item As DataRow In dtbModule.Rows
            '    Dim mo As New ModuleDTO
            '    mo.ID = item("ID")
            '    mo.NAME = item("NAME")
            '    ComboBoxData.LIST_MODULE.Add(mo)
            'Next
            FillDropDownList(cboMODULE, ComboBoxData.LIST_MODULE, "NAME", "ID", Common.SystemLanguage, True, cboMODULE.SelectedValue)
            FillDropDownList(cboFunctionGroup, ComboBoxData.LIST_FUNCTION_GROUP, "NAME", "ID", Common.SystemLanguage, True)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Gọi phương thức chuyển đổi state của toolbar và enable các control
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UpdateToolbarStatus()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ChangeToolbarState()
            If GroupInfo Is Nothing Then
                rtbMain.Enabled = False
            Else
                rtbMain.Enabled = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Gọi phương thức chuyển đổi trạng thái của các control
    ''' Khi state là new sẽ hiển thị ra các control cho việc insert
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UpdatePageViewState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateControlStatus()
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    If Not ViewPlaceHolder.Controls.Contains(ViewItem) Then
                        ViewPlaceHolder.Controls.Add(ViewItem)
                    End If
                    If Not GroupInfo Is Nothing Then
                        ViewItem.SetProperty("GroupID", GroupInfo.ID)
                    End If
                    ViewItem.SetProperty("ViewShowed", True)
                    ViewItem.CurrentState = CurrentState
                    ViewItem.Refresh()
                    GridPlaceHolder.Visible = False
                    ViewPlaceHolder.Visible = True
                Case Else
                    ViewPlaceHolder.Controls.Clear()
                    GridPlaceHolder.Visible = True
                    ViewPlaceHolder.Visible = False
                    ViewItem.SetProperty("ViewShowed", False)
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load ra các checkbox theo chức năng và set enable hay disable
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UpdateControlStatus()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateToolbarStatus()
            Select Case CurrentState
                Case CommonMessage.STATE_EDIT
                    For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.Items
                        For i = 3 To dr.Cells.Count - 1
                            If dr.Cells(i).Controls.Count > 0 Then
                                Dim chk As CheckBox = DirectCast(dr.Cells(i).Controls(1), CheckBox)
                                If chk IsNot Nothing Then
                                    chk.Enabled = True
                                End If
                            End If
                        Next
                    Next
                Case Else
                    For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.Items
                        For i = 3 To dr.Cells.Count - 1
                            If dr.Cells(i).Controls.Count > 0 Then
                                Dim chk As CheckBox = DirectCast(dr.Cells(i).Controls(1), CheckBox)
                                If chk IsNot Nothing Then
                                    chk.Enabled = False
                                End If
                            End If
                        Next
                    Next
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Bind lai du lieu cho grid rgGrid
    ''' Gọi hàm chuyển đổi trạng thái control
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If GroupInfo Is Nothing Then
                rgGrid.DataSource = New List(Of GroupFunctionDTO)
                Exit Sub
            End If
            If Not ViewShowed Then Exit Sub
            Dim Sorts As String = rgGrid.MasterTableView.SortExpressions.GetSortString()
            Dim filter As New GroupFunctionDTO
            filter.FUNCTION_NAME = txtFUNCTION_NAME.Text.Trim
            filter.MODULE_NAME = cboMODULE.Items(cboMODULE.SelectedIndex).Text
            filter.FUNCTION_GROUP_NAME = cboFunctionGroup.Items(cboFunctionGroup.SelectedIndex).Text
            If GroupID_Old = Nothing Or GroupID_Old <> GroupInfo.ID _
                Or Message = CommonMessage.ACTION_SAVED Or Message = CommonMessage.ACTION_UPDATED _
                Or GroupFunction Is Nothing Then
                Dim rep As New CommonRepository
                If Sorts IsNot Nothing Then
                    GroupFunction = rep.GetGroupFunctionPermision(GroupInfo.ID, filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows, Sorts)
                Else
                    GroupFunction = rep.GetGroupFunctionPermision(GroupInfo.ID, filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows)
                End If
            End If

            'Đưa dữ liệu vào Grid
            rgGrid.VirtualItemCount = MaximumRows
            rgGrid.DataSource = GroupFunction
            If Message <> CommonMessage.ACTION_UPDATED Then rgGrid.DataBind()

            'Thay đổi trạng thái các control
            UpdateControlStatus()

            GroupID_Old = GroupInfo.ID
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Chuyển đổi trạng thái các control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Me.Controls.Contains(groupCopy) Then
                Me.Controls.Remove(groupCopy)
                'Me.Views.Remove(groupCopy.ID.ToUpper)
            End If
            Select Case isLoadPopup
                Case 1
                    groupCopy = Me.Register("ctrlGroupCopy", "Common", "ctrlGroupCopy", "Secure")
                    groupCopy.GroupID = GroupID_Old
                    groupCopy.LabelName = "Sao chép đến"
                    groupCopy.BindData()
                    Me.Controls.Add(groupCopy)
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command thêm, sửa, lưu, hủy, copy, xóa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New CommonRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_NEW Then
                CType(ViewItem, ctrlGroupFunctionAddEdit).OnToolbar_Command(sender, e)
            Else
                Select Case CType(e.Item, Telerik.Web.UI.RadToolBarButton).CommandName
                    Case CommonMessage.TOOLBARITEM_CREATE
                        CurrentState = CommonMessage.STATE_NEW
                        'Update View
                        UpdatePageViewState()
                        'Gửi thông điệp cho Parent View
                        Me.Send(CommonMessage.ACTION_UPDATING)
                    Case CommonMessage.TOOLBARITEM_EDIT
                        CurrentState = CommonMessage.STATE_EDIT
                        'Update grid
                        rgGrid.Rebind()
                        'Update View
                        UpdatePageViewState()
                        'Gửi thông điệp cho Parent View
                        Me.Send(CommonMessage.STATE_NORMAL)
                    Case CommonMessage.TOOLBARITEM_SAVE
                        'Kiểm tra đã select Group chưa
                        If Not CheckRowSelected() Then
                            Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If CurrentState = CommonMessage.STATE_EDIT Then
                            Dim lst As List(Of CommonBusiness.GroupFunctionDTO)
                            lst = RepareDataForUpdate()
                            Dim lstID As List(Of Decimal) = (From p In lst Select p.ID).ToList
                            Using ret As New CommonRepository
                                If ret.CheckExistIDTable(lstID, "SE_GROUP_PERMISSION", "ID") Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                    Exit Sub
                                End If
                            End Using
                            If rep.UpdateGroupFunction(lst) Then
                                Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Else
                                Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                            End If
                        End If
                        CurrentState = CommonMessage.STATE_NORMAL
                        'Update grid
                        SelectedItem = Nothing
                        Refresh(CommonMessage.ACTION_SAVED)
                        'Update View
                        UpdatePageViewState()
                        'Gửi thông điệp cho Parent View
                        Me.Send(CommonMessage.ACTION_UPDATED)
                    Case CommonMessage.TOOLBARITEM_DELETE
                        'Kiểm tra đã select Group chưa
                        If Not CheckRowSelected() Then
                            Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        DeleteItemList = RepareDataForDelete()
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                        ctrlMessageBox.MessageTitle = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE_TITLE)
                        ctrlMessageBox.ActionName = CommonMessage.ACTION_DELETED
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Case CommonMessage.TOOLBARITEM_CANCEL
                        CurrentState = CommonMessage.STATE_NORMAL
                        'Update grid
                        SelectedItem = Nothing
                        rgGrid.Rebind()
                        'Update View
                        UpdatePageViewState()
                        'Gửi thông điệp cho Parent View
                        Me.Send(CommonMessage.ACTION_UPDATED)
                    Case CommonMessage.TOOLBARITEM_SYNC
                        isLoadPopup = 1
                        UpdateControlState()
                        groupCopy.Show()
                    Case CommonMessage.TOOLBARITEM_EXPORT
                        Dim dtData As DataTable
                        Dim Sorts As String = rgGrid.MasterTableView.SortExpressions.GetSortString()
                        Dim filter As New GroupFunctionDTO
                        SetValueObjectByRadGrid(rgGrid, filter)
                        Using xls As New ExcelCommon
                            If Sorts IsNot Nothing Then
                                dtData = rep.GetGroupFunctionPermision(GroupInfo.ID, filter, 0, Integer.MaxValue, 0, Sorts).ToTable
                            Else
                                dtData = rep.GetGroupFunctionPermision(GroupInfo.ID, filter, 0, Integer.MaxValue, 0).ToTable
                            End If
                            If dtData.Rows.Count = 0 Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                                Exit Sub
                            ElseIf dtData.Rows.Count > 0 Then
                                rgGrid.ExportExcel(Server, Response, dtData, "Function")
                                Exit Sub
                            End If
                        End Using
                End Select
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien OnReceiveData cua control ctrlGroupFunction
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub ctrlGroupFunction_OnReceiveData(ByVal sender As ViewBase, ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.FromViewID = "ctrlGroupFunctionAddEdit" Then
                CurrentState = CommonMessage.STATE_NORMAL
                UpdatePageViewState()
                If e.EventData = CommonMessage.ACTION_SUCCESS Then
                    Refresh(CommonMessage.ACTION_SAVED)
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                ElseIf e.EventData = CommonMessage.ACTION_UNSUCCESS Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                Else
                    Refresh()
                End If
                Me.Send(CommonMessage.ACTION_UPDATED)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien CheckChanged cua control CheckBox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub CheckBox_CheckChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Then
                Dim chk = DirectCast(sender, CheckBox)
                Select Case chk.ID
                    Case "chkCREATE_ALL"
                        CheckState.ALLOW_CREATE = chk.Checked
                        DoCheck("ALLOW_CREATE", "chkCREATE", chk.Checked)
                    Case "chkMODIFY_ALL"
                        CheckState.ALLOW_MODIFY = chk.Checked
                        DoCheck("ALLOW_MODIFY", "chkMODIFY", chk.Checked)
                    Case "chkDELETE_ALL"
                        CheckState.ALLOW_DELETE = chk.Checked
                        DoCheck("ALLOW_DELETE", "chkDELETE", chk.Checked)
                    Case "chkPRINT_ALL"
                        CheckState.ALLOW_PRINT = chk.Checked
                        DoCheck("ALLOW_PRINT", "chkPRINT", chk.Checked)
                    Case "chkIMPORT_ALL"
                        CheckState.ALLOW_IMPORT = chk.Checked
                        DoCheck("ALLOW_IMPORT", "chkIMPORT", chk.Checked)
                    Case "chkEXPORT_ALL"
                        CheckState.ALLOW_EXPORT = chk.Checked
                        DoCheck("ALLOW_EXPORT", "chkEXPORT", chk.Checked)
                    Case "chkSPECIAL1_ALL"
                        CheckState.ALLOW_SPECIAL1 = chk.Checked
                        DoCheck("ALLOW_SPECIAL1", "chkSPECIAL1", chk.Checked)
                    Case "chkSPECIAL2_ALL"
                        CheckState.ALLOW_SPECIAL2 = chk.Checked
                        DoCheck("ALLOW_SPECIAL2", "chkSPECIAL2", chk.Checked)
                    Case "chkSPECIAL3_ALL"
                        CheckState.ALLOW_SPECIAL3 = chk.Checked
                        DoCheck("ALLOW_SPECIAL3", "chkSPECIAL3", chk.Checked)
                    Case "chkSPECIAL4_ALL"
                        CheckState.ALLOW_SPECIAL4 = chk.Checked
                        DoCheck("ALLOW_SPECIAL4", "chkSPECIAL4", chk.Checked)
                    Case "chkSPECIAL5_ALL"
                        CheckState.ALLOW_SPECIAL5 = chk.Checked
                        DoCheck("ALLOW_SPECIAL5", "chkSPECIAL5", chk.Checked)
                End Select
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command button cua control ctrkMessageBox
    ''' Cap nhat trang thai cua cac control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New CommonRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ButtonID = MessageBoxButtonType.ButtonYes And e.ActionName = CommonMessage.ACTION_DELETED Then
                If rep.DeleteGroupFunction(DeleteItemList) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Refresh(CommonMessage.ACTION_SAVED)
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien ItemDataBound cua control rgGrid để show ra các checkbox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgGrid.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Header Then
                Dim chkCREATE As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_CREATE") _
                    .FindControl("chkCREATE_ALL"), CheckBox)
                Dim chkMODIFY As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_MODIFY") _
                    .FindControl("chkMODIFY_ALL"), CheckBox)
                Dim chkDELETE As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_DELETE") _
                    .FindControl("chkDELETE_ALL"), CheckBox)
                Dim chkPRINT As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_PRINT") _
                    .FindControl("chkPRINT_ALL"), CheckBox)
                Dim chkIMPORT As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_IMPORT") _
                    .FindControl("chkIMPORT_ALL"), CheckBox)
                Dim chkEXPORT As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_EXPORT") _
                    .FindControl("chkEXPORT_ALL"), CheckBox)
                Dim chkSPECIAL1 As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_SPECIAL1") _
                    .FindControl("chkSPECIAL1_ALL"), CheckBox)
                Dim chkSPECIAL2 As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_SPECIAL2") _
                    .FindControl("chkSPECIAL2_ALL"), CheckBox)
                Dim chkSPECIAL3 As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_SPECIAL3") _
                    .FindControl("chkSPECIAL3_ALL"), CheckBox)
                Dim chkSPECIAL4 As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_SPECIAL4") _
                    .FindControl("chkSPECIAL4_ALL"), CheckBox)
                Dim chkSPECIAL5 As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("ALLOW_SPECIAL5") _
                    .FindControl("chkSPECIAL5_ALL"), CheckBox)
                If chkCREATE IsNot Nothing Then
                    chkCREATE.Checked = CheckState.ALLOW_CREATE
                    chkCREATE.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkMODIFY IsNot Nothing Then
                    chkMODIFY.Checked = CheckState.ALLOW_MODIFY
                    chkMODIFY.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkDELETE IsNot Nothing Then
                    chkDELETE.Checked = CheckState.ALLOW_DELETE
                    chkDELETE.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkPRINT IsNot Nothing Then
                    chkPRINT.Checked = CheckState.ALLOW_PRINT
                    chkPRINT.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkIMPORT IsNot Nothing Then
                    chkIMPORT.Checked = CheckState.ALLOW_IMPORT
                    chkIMPORT.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkEXPORT IsNot Nothing Then
                    chkEXPORT.Checked = CheckState.ALLOW_EXPORT
                    chkEXPORT.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkSPECIAL1 IsNot Nothing Then
                    chkSPECIAL1.Checked = CheckState.ALLOW_SPECIAL1
                    chkSPECIAL1.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkSPECIAL2 IsNot Nothing Then
                    chkSPECIAL2.Checked = CheckState.ALLOW_SPECIAL2
                    chkSPECIAL2.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkSPECIAL3 IsNot Nothing Then
                    chkSPECIAL3.Checked = CheckState.ALLOW_SPECIAL3
                    chkSPECIAL3.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkSPECIAL4 IsNot Nothing Then
                    chkSPECIAL4.Checked = CheckState.ALLOW_SPECIAL4
                    chkSPECIAL4.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
                If chkSPECIAL5 IsNot Nothing Then
                    chkSPECIAL5.Checked = CheckState.ALLOW_SPECIAL5
                    chkSPECIAL5.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                End If
            ElseIf e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                If SelectedItem IsNot Nothing Then
                    If SelectedItem.Contains(Decimal.Parse(datarow("ID").Text)) Then
                        e.Item.Selected = True
                    End If
                End If
                For i = 3 To datarow.Cells.Count - 1
                    If datarow.Cells(i).Controls.Count > 0 Then
                        Dim chk As CheckBox = DirectCast(datarow.Cells(i).Controls(1), CheckBox)
                        If chk IsNot Nothing Then
                            chk.Enabled = If(CurrentState = CommonMessage.STATE_EDIT, True, False)
                        End If
                    End If
                Next
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cua control rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh(CommonMessage.ACTION_UPDATED)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien clicked cua control btnFIND
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFIND_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFIND.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.GroupFunction = Nothing
            rgGrid.CurrentPageIndex = 0
            Refresh()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien chọn nhân viên của control groupCopy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub groupCopy_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles groupCopy.GroupCopySelected
        Dim rep As New CommonRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien nhấn nút cancel của control groupCopy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles groupCopy.CancelClicked
        isLoadPopup = 0
    End Sub
#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý suwk kiện check hay không check cho checkbox theo param truyền vào
    ''' </summary>
    ''' <param name="UniqueName"></param>
    ''' <param name="CheckBoxName"></param>
    ''' <param name="checked"></param>
    ''' <remarks></remarks>
    Private Sub DoCheck(ByVal UniqueName As String, ByVal CheckBoxName As String, ByVal checked As Boolean)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SelectedItem = New List(Of Decimal)
            For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.SelectedItems
                SelectedItem.Add(Decimal.Parse(dr.GetDataKeyValue("ID").ToString))
                Dim _chk As CheckBox = DirectCast(dr(UniqueName).FindControl(CheckBoxName), CheckBox)
                If _chk IsNot Nothing Then
                    _chk.Checked = checked
                End If
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện check checkbox khi row được selected
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckRowSelected()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Return rgGrid.SelectedItems.Count > 0
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return False
        End Try

    End Function

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện load trạng thái của checkbox để chuẩn bị update
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function RepareDataForUpdate() As List(Of CommonBusiness.GroupFunctionDTO)
        Dim lst As New List(Of CommonBusiness.GroupFunctionDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.SelectedItems
                Dim item As New CommonBusiness.GroupFunctionDTO
                item.ALLOW_CREATE = If(DirectCast(dr("ALLOW_CREATE").FindControl("chkCREATE"), CheckBox).Checked, 1, 0)
                item.ALLOW_MODIFY = If(DirectCast(dr("ALLOW_MODIFY").FindControl("chkMODIFY"), CheckBox).Checked, 1, 0)
                item.ALLOW_DELETE = If(DirectCast(dr("ALLOW_DELETE").FindControl("chkDELETE"), CheckBox).Checked, 1, 0)
                item.ALLOW_PRINT = If(DirectCast(dr("ALLOW_PRINT").FindControl("chkPRINT"), CheckBox).Checked, 1, 0)
                item.ALLOW_IMPORT = If(DirectCast(dr("ALLOW_IMPORT").FindControl("chkIMPORT"), CheckBox).Checked, 1, 0)
                item.ALLOW_EXPORT = If(DirectCast(dr("ALLOW_EXPORT").FindControl("chkEXPORT"), CheckBox).Checked, 1, 0)
                item.ALLOW_SPECIAL1 = If(DirectCast(dr("ALLOW_SPECIAL1").FindControl("chkSPECIAL1"), CheckBox).Checked, 1, 0)
                item.ALLOW_SPECIAL2 = If(DirectCast(dr("ALLOW_SPECIAL2").FindControl("chkSPECIAL2"), CheckBox).Checked, 1, 0)
                item.ALLOW_SPECIAL3 = If(DirectCast(dr("ALLOW_SPECIAL3").FindControl("chkSPECIAL3"), CheckBox).Checked, 1, 0)
                item.ALLOW_SPECIAL4 = If(DirectCast(dr("ALLOW_SPECIAL4").FindControl("chkSPECIAL4"), CheckBox).Checked, 1, 0)
                item.ALLOW_SPECIAL5 = If(DirectCast(dr("ALLOW_SPECIAL5").FindControl("chkSPECIAL5"), CheckBox).Checked, 1, 0)
                item.GROUP_ID = GroupID_Old
                item.FUNCTION_ID = Decimal.Parse(dr.GetDataKeyValue("FUNCTION_ID").ToString)
                item.ID = Decimal.Parse(dr.GetDataKeyValue("ID").ToString)
                lst.Add(item)
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

        Return lst
    End Function

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện add các ID vào list để chuẩn bị delete
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function RepareDataForDelete() As List(Of Decimal)
        Dim lst As New List(Of Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.SelectedItems
                lst.Add(Decimal.Parse(dr.GetDataKeyValue("ID").ToString))
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