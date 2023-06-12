Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlManageCache
    Inherits CommonView
    Protected WithEvents UserView As ViewBase
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"

    Property lstFunctions As List(Of FunctionDTO)
        Get
            Return ViewState(Me.ID & "_lstFunctions")
        End Get
        Set(ByVal value As List(Of FunctionDTO))
            ViewState(Me.ID & "_lstFunctions") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Refresh()

            'Insert logs
            Dim startTime As DateTime = DateTime.UtcNow
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
            rgListManageCaches.SetFilter()
            rgListManageCaches.AllowCustomPaging = True
            InitControl()

            'Insert logs
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = rtbMainToolBar
            Common.BuildToolbar(Me.MainToolBar,
                                ToolbarItem.Delete)
            Me.MainToolBar.OnClientButtonClicking = "clientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            'Insert logs
            Dim startTime As DateTime = DateTime.UtcNow
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
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                rgListManageCaches.Rebind()
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgListManageCaches.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                End Select
            End If

            'Insert logs
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim _count As New Decimal
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgListManageCaches.SelectedItems.Count = 0 Then
                        ctrlMessageBox.MessageText = "Bạn có muốn xóa tất cả Cache không?"
                    Else
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    End If
                    ctrlMessageBox.ActionName = "DELETE"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select

            'Insert logs
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Hiển thị dữ liệu cho lưới
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgListManageCaches_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgListManageCaches.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CreateDataFilter()

            'Insert logs
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Hiển thị Yes/No button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(sender As Object, e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim lstActiveFunctions As New List(Of FunctionDTO)
            Dim rep As New CommonRepository
            If e.ActionName = "DELETE" AndAlso e.ButtonID = MessageBoxButtonType.ButtonYes Then
                If rgListManageCaches.SelectedItems.Count = 0 Then
                    CacheManager.ClearAll()
                Else
                    For Each _item As GridDataItem In rgListManageCaches.SelectedItems
                        CacheManager.ClearValue(_item.GetDataKeyValue("Key"))
                    Next
                End If
                Refresh("UpdateView")
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Custom"

    ''' <summary>
    ''' Lấy dữ liệu từ DB
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CreateDataFilter()
        Dim rep As New CommonRepository
        Dim obj As New FunctionDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgListManageCaches, obj)
            Dim getData = CacheManager.GetAll()
            Dim Sorts As String = rgListManageCaches.MasterTableView.SortExpressions.GetSortString()

            rgListManageCaches.VirtualItemCount = getData.Rows.Count
            rgListManageCaches.DataSource = getData

            'Insert logs
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region
End Class