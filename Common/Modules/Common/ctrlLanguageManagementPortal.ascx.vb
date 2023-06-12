Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlLanguageManagementPortal
    Inherits CommonView

    ''' <summary>
    ''' MustAuthorize
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Property MustAuthorize As Boolean = False

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"

#End Region

#Region "Page"

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.MainToolBar = Me.tbarLanguage
            Common.BuildToolbar(Me.tbarLanguage, ToolbarItem.Search, ToolbarItem.Seperator,
                                ToolbarItem.Edit, ToolbarItem.Seperator,
                                ToolbarItem.Save, ToolbarItem.Seperator,
                                ToolbarItem.Cancel, ToolbarItem.Seperator,
                                ToolbarItem.Export, ToolbarItem.Delete)

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Refresh()
            UpdateControlState()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                UpdateControlState()
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles grvLanguage.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim langMgr As New LanguageManager

        Try
            grvLanguage.DataSource = langMgr.Dictionary
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary> Set Trang thai control </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If CurrentState = CommonMessage.STATE_NORMAL Then
                Me.tbarLanguage.Items(0).Enabled = True
                Me.tbarLanguage.Items(2).Enabled = True
                Me.tbarLanguage.Items(4).Enabled = False
                Me.tbarLanguage.Items(6).Enabled = False
                grvLanguage.MasterTableView.ClearEditItems()
                grvLanguage.Rebind()

            ElseIf Me.CurrentState = CommonMessage.STATE_EDIT Then
                Me.tbarLanguage.Items(0).Enabled = True
                Me.tbarLanguage.Items(2).Enabled = False
                Me.tbarLanguage.Items(4).Enabled = True
                Me.tbarLanguage.Items(6).Enabled = True

                For Each item As GridItem In grvLanguage.SelectedItems
                    item.Edit = True
                Next

                grvLanguage.Rebind()
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub MainToolBarClick(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim CommandName As String = CType(e.Item, RadToolBarButton).CommandName
            Select Case CommandName
                Case CommonMessage.TOOLBARITEM_SEARCH
                    CurrentState = CommonMessage.STATE_NORMAL

                    For Each column As GridColumn In grvLanguage.MasterTableView.Columns
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter
                        column.CurrentFilterValue = String.Empty
                    Next

                    grvLanguage.MasterTableView.FilterExpression = String.Empty
                    grvLanguage.MasterTableView.ClearEditItems()
                    grvLanguage.Rebind()
                    Refresh("Update")

                Case CommonMessage.TOOLBARITEM_EDIT
                    If grvLanguage.SelectedItems.Count > 0 Then
                        CurrentState = CommonMessage.STATE_EDIT
                    Else
                        ShowMessage(Me.Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), Utilities.NotifyType.Warning)
                    End If

                Case CommonMessage.TOOLBARITEM_DELETE
                    If grvLanguage.SelectedItems.Count > 0 Then
                        Dim langMgr As New LanguageManager
                        For Each item As GridDataItem In grvLanguage.SelectedItems
                            langMgr.RemoveItem(item.GetDataKeyValue("Key"))
                        Next
                        langMgr.SerializeToFile()
                    Else
                        ShowMessage(Me.Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), Utilities.NotifyType.Warning)
                    End If

                Case CommonMessage.TOOLBARITEM_SAVE
                    If grvLanguage.EditItems.Count > 0 Then
                        Try
                            Dim langMgr As New LanguageManager
                            For Each item As GridDataItem In grvLanguage.EditItems
                                langMgr.Item(CType(item("Key").Controls(0), TextBox).Text) = CType(item("Value").Controls(0), TextBox).Text
                            Next
                            langMgr.SerializeToFile()
                        Catch ex As Exception
                            DisplayException(Me.ViewName, Me.ID, ex)
                        End Try
                        CurrentState = CommonMessage.STATE_NORMAL
                    Else
                        ShowMessage(Me.Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), Utilities.NotifyType.Warning)
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim langMgr As New LanguageManager
                    grvLanguage.AllowCustomPaging = False
                    grvLanguage.DataSource = langMgr.Dictionary
                    grvLanguage.DataBind()

                    If langMgr.Dictionary.Count > 0 Then
                        grvLanguage.ExportSettings.ExportOnlyData = True
                        grvLanguage.ExportSettings.IgnorePaging = True
                        grvLanguage.ExportSettings.OpenInNewWindow = True
                        grvLanguage.ExportSettings.FileName = "Language"
                        'grvLanguage.ExportSettings.IgnorePaging = False
                        grvLanguage.MasterTableView.UseAllDataFields = True
                        grvLanguage.MasterTableView.ExportToExcel()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                    End If
                    Exit Sub
            End Select

            UpdateControlState()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

#End Region

End Class