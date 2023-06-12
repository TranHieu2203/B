Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlActionLog
    Inherits CommonView

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' ActionLogs
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ActionLogs As List(Of ActionLog)
        Get
            Return ViewState(Me.ID & "_ActionLogs")
        End Get

        Set(ByVal value As List(Of ActionLog))
            ViewState(Me.ID & "_ActionLogs") = value
        End Set
    End Property

    ''' <summary>
    ''' DeleteIds
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeleteIds As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_DeleteIds")
        End Get

        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_DeleteIds") = value
        End Set
    End Property

    ''' <summary>
    ''' isLoadPopup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' 0 - normal
    ''' 1 - Content
    ''' </remarks>
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get

        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
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

#End Region

#Region "Page"

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgActionLog.AllowCustomPaging = True
            rgActionLog.SetFilter()

            Me.MainToolBar = rtbActionLog
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Delete, ToolbarItem.Export)
            rgActionLog.AllowCustomPaging = True

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary> Load data cho combobox </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository

        Try
            Dim cbo As New ComboBoxDataDTO With {.GET_FUNCTION_GROUP = True,
                                                 .GET_FUNCTION = True,
                                                 .GET_ACTION_NAME = True,
                                                 .GET_USER_GROUP = True}
            rep.GetComboList(cbo)

            Utilities.FillDropDownList(cboFunctionGroup, cbo.LIST_FUNCTION_GROUP, "NAME", "NAME", Common.SystemLanguage, True)
            Utilities.FillDropDownList(cboFunctionName, cbo.LIST_FUNCTION, "NAME", "FID", Common.SystemLanguage, True)
            Utilities.FillDropDownList(cboActionName, cbo.LIST_ACTION_NAME, "NAME_VN", "CODE", Common.SystemLanguage, True)
            Utilities.FillDropDownList(cboUserGroup, cbo.LIST_USER_GROUP, "NAME", "CODE", Common.SystemLanguage, True)

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgActionLog_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgActionLog.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        CreateDataFilter = Nothing
        Dim rep As New CommonRepository
        Dim filter As New ActionLogFilter
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            filter.ViewGroup = cboFunctionGroup.SelectedValue
            filter.ViewName = cboFunctionName.SelectedValue
            filter.ActionName = cboActionName.SelectedValue
            filter.UserGroup = cboUserGroup.SelectedValue
            filter.UserName = txtUser.Text.Trim.ToUpper
            filter.IP = txtIP.Text.Trim
            filter.FromDate = rdFromDate.SelectedDate
            filter.ToDate = rdToDate.SelectedDate
            filter.ComputerName = txtComputerName.Text.Trim
            filter.username_login = LogHelper.CurrentUser.USERNAME.ToUpper
            filter.Platform = cboPlatform.SelectedValue

            SetValueObjectByRadGrid(rgActionLog, filter)
            'If filter.ActionDate IsNot Nothing Then
            '    filter.ActionDate = Format(CDate(filter.ActionDate), "dd/MM/yyyy")
            'End If

            Dim MaximumRows As Integer
            Dim Sorts As String = rgActionLog.MasterTableView.SortExpressions.GetSortString()

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetActionLog(filter, Sorts).ToTable()
                Else
                    Return rep.GetActionLog(filter).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.ActionLogs = rep.GetActionLog(filter, rgActionLog.CurrentPageIndex, rgActionLog.PageSize, MaximumRows, Sorts)
                Else
                    Me.ActionLogs = rep.GetActionLog(filter, rgActionLog.CurrentPageIndex, rgActionLog.PageSize, MaximumRows)
                End If

                rgActionLog.VirtualItemCount = MaximumRows

                'Dim re = New List(Of ActionLog)
                'If filter.ActionDate IsNot Nothing Then
                '    Dim date1 = Format(CDate(filter.ActionDate), "dd/MM/yyyy")
                '    re = Me.ActionLogs.Where(Function(f) Format(CDate(f.ActionDate.Date), "dd/MM/yyyy") = date1).ToList
                'Else
                '    re = Me.ActionLogs
                'End If

                rgActionLog.DataSource = Me.ActionLogs
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

#End Region

#Region "Event"

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="events"></param>
    ''' <remarks></remarks>
    Public Sub MainToolbarClick(ByVal sender As Object, ByVal events As Telerik.Web.UI.RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case CType(events.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgActionLog.ExportExcel(Server, Response, dtData, "ActionLog")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_DELETE
                    Dim lstDeleteIds As New List(Of Decimal)
                    Dim sActive As String = ""
                    Dim bCheck As Boolean = True

                    If rgActionLog.SelectedItems.Count > 0 Then
                        For idx As Integer = 0 To rgActionLog.SelectedItems.Count - 1
                            Dim item As GridDataItem = rgActionLog.SelectedItems(idx)
                            lstDeleteIds.Add(Decimal.Parse(item("Id").Text))
                        Next
                        DeleteIds = lstDeleteIds
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    End If
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Sự kiện xử lý khi click với popup Warning</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New CommonRepository
                If rep.DeleteActionLogs(DeleteIds) Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    rgActionLog.Rebind()
                Else
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Sự kiện xử lý khi click button Find</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFIND_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFIND.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgActionLog.CurrentPageIndex = 0
            rgActionLog.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

#End Region

End Class