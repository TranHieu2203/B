Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_SearchTranningRecord
    Inherits Common.CommonView
    Protected WithEvents RequestView As ViewBase

    Dim rep As New TrainingRepository

#Region "Property"

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New TrainingRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            'rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub BindData()
        MyBase.BindData()
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "EmployeeRecord")
                        End If
                    End Using
            End Select
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Select Case e.ActionName
                    Case CommonMessage.TOOLBARITEM_DELETE
                        Dim lstDeletes As New List(Of Decimal)
                        For idx = 0 To rgData.SelectedItems.Count - 1
                            Dim item As GridDataItem = rgData.SelectedItems(idx)
                            lstDeletes.Add(item.GetDataKeyValue("ID"))
                        Next
                        If rep.DeletePlans(lstDeletes) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    Case CommonMessage.TOOLBARITEM_APPROVE
                        Dim lstDeletes As New List(Of Decimal)
                        For idx = 0 To rgData.SelectedItems.Count - 1
                            Dim item As GridDataItem = rgData.SelectedItems(idx)
                            lstDeletes.Add(item.GetDataKeyValue("ID"))
                        Next
                        If rep.UpdateStatusTrainingRequests(lstDeletes, TrainingCommon.TR_REQUEST_STATUS.APPROVE_ID) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If

                    Case CommonMessage.TOOLBARITEM_REJECT
                        Dim lstDeletes As New List(Of Decimal)
                        For idx = 0 To rgData.SelectedItems.Count - 1
                            Dim item As GridDataItem = rgData.SelectedItems(idx)
                            lstDeletes.Add(item.GetDataKeyValue("ID"))
                        Next
                        If rep.UpdateStatusTrainingRequests(lstDeletes, TrainingCommon.TR_REQUEST_STATUS.NOT_APPROVE_ID) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If
                End Select
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        rgData.Rebind()
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New RecordEmployeeDTO
        Dim rep As New TrainingRepository
        Dim total As Integer = 0
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If ctrlOrg.CurrentValue Is Nothing Then
                rgData.DataSource = New List(Of RecordEmployeeDTO)
                Exit Function
            End If

            _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            _filter.START_DATE = rdFromDate.SelectedDate
            _filter.END_DATE = rdToDate.SelectedDate
            _filter.EMPLOYEE_CODE = txtEmployee.Text 'tim kiem theo ma nhan vien

            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), .IS_DISSOLVE = ctrlOrg.IsDissolve}

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetEmployeeRecord(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetEmployeeRecord(_filter, _param).ToTable()
                End If
            Else
                Dim MaximumRows As Integer
                Dim lstData As List(Of RecordEmployeeDTO)
                If Sorts IsNot Nothing Then
                    lstData = rep.GetEmployeeRecord(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
                Else
                    lstData = rep.GetEmployeeRecord(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
                End If
                rgData.VirtualItemCount = MaximumRows

                If (lstData IsNot Nothing) Then
                    For idx = 0 To lstData.Count - 1
                        If (lstData(idx).CERTIFICATE_DATE IsNot Nothing) Then
                            lstData(idx).CER_EXPIRED_DATE = CaculationDate(lstData(idx).CER_RECEIVE_DATE, lstData(idx).CERTIFICATE_DURATION)
                        End If
                    Next
                End If

                rgData.DataSource = lstData
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Custom"

    Function CaculationDate(ByVal dateTime As Date, ByVal addmonth As Decimal) As Date
        Dim result As Date
        Try
            result = DateAdd(DateInterval.Month, addmonth, dateTime)
        Catch ex As Exception
            Throw ex
        End Try
        Return result
    End Function
#End Region

End Class