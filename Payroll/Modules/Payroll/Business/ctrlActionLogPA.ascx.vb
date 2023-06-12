Imports Common.CommonBusiness
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports Framework.UI
Imports Common
Imports Payroll.PayrollRepository
Imports Payroll.PayrollBusiness

Public Class ctrlActionLogPA
    Inherits Common.CommonView

#Region "Property"
    Public Property ActionLogs As List(Of PA_ACTION_LOGDTO)
        Get
            Return ViewState(Me.ID & "_ActionLogs")
        End Get
        Set(ByVal value As List(Of PA_ACTION_LOGDTO))
            ViewState(Me.ID & "_ActionLogs") = value
        End Set
    End Property

    Public Property DeleteIds As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_DeleteIds")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_DeleteIds") = value
        End Set
    End Property

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

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
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)

        Me.MainToolBar = rtbActionLog
        Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Delete, ToolbarItem.Export)
        SetGridFilter(rgActionLog)
        rgActionLog.AllowCustomPaging = True
        Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)

    End Sub

    Public Overrides Sub BindData()

    End Sub

    Protected Sub rgActionLog_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgActionLog.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New PayrollRepository
        Dim filter As New PA_ACTION_LOGDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgActionLog, filter)
            Dim Sorts As String = rgActionLog.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetActionLog(filter, Sorts).ToTable()
                Else
                    Return rep.GetActionLog(filter).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.ActionLogs = rep.GetActionLog(filter, MaximumRows, rgActionLog.CurrentPageIndex, rgActionLog.PageSize, Sorts)
                Else
                    Me.ActionLogs = rep.GetActionLog(filter, MaximumRows, rgActionLog.CurrentPageIndex, rgActionLog.PageSize)
                End If

                rgActionLog.VirtualItemCount = MaximumRows
                rgActionLog.DataSource = Me.ActionLogs
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub UpdateControlState()
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Public Sub MainToolbarClick(ByVal sender As Object, ByVal events As Telerik.Web.UI.RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(events.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgActionLog.ExportExcel(Server, Response, dtData, "ActionLog")
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

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New PayrollRepository
                If rep.DeleteActionLogsAT(DeleteIds) Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    rgActionLog.Rebind()
                Else
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    
#End Region

#Region "Custom"

#End Region

End Class