Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlControlsManage
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
            rgListControls.SetFilter()
            rgListControls.AllowCustomPaging = True
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
                                ToolbarItem.Create, _
                                ToolbarItem.Edit, _
                                ToolbarItem.Active, _
                                ToolbarItem.Deactive)
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
                rgListControls.Rebind()
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
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    _count = 0
                    If rgListControls.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each _item As GridDataItem In rgListControls.SelectedItems
                        If _item.GetDataKeyValue("ACTFLG").ToString.ToUpper.Contains("Áp dụng".ToUpper) = True Then
                            _count += 1
                        End If
                    Next
                    If _count > 0 Then
                        Me.ShowMessage(Translate("Đã được áp dụng, kiểm tra lại ! "), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    _count = 0
                    If rgListControls.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each _item As GridDataItem In rgListControls.SelectedItems
                        If _item.GetDataKeyValue("ACTFLG").ToString.ToUpper.Contains("Không áp dụng".ToUpper) = True Then
                            _count += 1
                        End If
                    Next
                    If _count > 0 Then
                        Me.ShowMessage(Translate("Đã ngưng áp dụng, kiểm tra lại ! "), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE
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
    Private Sub rgListControls_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgListControls.NeedDataSource
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

            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE AndAlso e.ButtonID = MessageBoxButtonType.ButtonYes Then
                For Each _item As GridDataItem In rgListControls.SelectedItems
                    lstActiveFunctions.Add(New FunctionDTO With {.ID = Decimal.Parse(_item.GetDataKeyValue("ID").ToString)})
                Next
                If rep.ActiveFunctions(lstActiveFunctions, "A") Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgListControls.Rebind()
                Else
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                End If
            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE AndAlso e.ButtonID = MessageBoxButtonType.ButtonYes Then
                For Each _item As GridDataItem In rgListControls.SelectedItems
                    lstActiveFunctions.Add(New FunctionDTO With {.ID = Decimal.Parse(_item.GetDataKeyValue("ID").ToString)})
                Next
                If rep.ActiveFunctions(lstActiveFunctions, "I") Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgListControls.Rebind()
                Else
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                End If
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
            SetValueObjectByRadGrid(rgListControls, obj)
            Dim Sorts As String = rgListControls.MasterTableView.SortExpressions.GetSortString()

            If Sorts IsNot Nothing Then
                Me.lstFunctions = rep.Get_FunctionWithControl_List(obj, rgListControls.CurrentPageIndex, rgListControls.PageSize, MaximumRows, Sorts)
            Else
                Me.lstFunctions = rep.Get_FunctionWithControl_List(obj, rgListControls.CurrentPageIndex, rgListControls.PageSize, MaximumRows)
            End If
            rgListControls.VirtualItemCount = MaximumRows
            rgListControls.DataSource = Me.lstFunctions

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