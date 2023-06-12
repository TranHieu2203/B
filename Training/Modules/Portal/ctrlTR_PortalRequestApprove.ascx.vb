Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_PortalRequestApprove
    Inherits Common.CommonView
    Protected WithEvents RequestView As ViewBase
    Protected WithEvents ctrlCommon_Reject As ctrlCommon_Reject

    Dim rep As New TrainingRepository

#Region "Property"

    Public Property EmployeeID As Decimal

    Property note As String
        Get
            Return ViewState(Me.ID & "_note")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_note") = value
        End Set
    End Property
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

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgData.SetFilter()
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
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Approve, ToolbarItem.Reject, ToolbarItem.Export)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

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
            If Request.Headers("NoSearch") IsNot Nothing Then
                RadPane4.Visible = False
            End If
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
                Case "ACTIVE"
                    'Dim store As New AttendanceStoreProcedure
                    Dim ID_REGGROUP As Integer
                    Dim IS_SUCCESS As Integer
                    Dim EMP_ID As Integer
                    For Each dr As GridDataItem In rgData.SelectedItems
                        ID_REGGROUP = dr.GetDataKeyValue("ID")
                        EMP_ID = dr.GetDataKeyValue("REQUEST_SENDER_ID")
                        Dim result = rep.PRI_PROCESS(LogHelper.CurrentUser.EMPLOYEE_ID, EMP_ID, 0, 1, "TRAINING", "", ID_REGGROUP)

                        If result = 0 Then
                            IS_SUCCESS += 1
                        Else
                            IS_SUCCESS -= 1
                        End If
                    Next

                    If IS_SUCCESS >= 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Refresh("UpdateView")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            MyBase.BindData()
            Dim dtData As New DataTable
            'dtData = rep.GetOtherList("PROCESS_STATUS", True)
            dtData.Columns.Add("ID")
            dtData.Columns.Add("NAME")
            Dim R As DataRow = dtData.NewRow
            Dim R2 As DataRow = dtData.NewRow
            Dim R3 As DataRow = dtData.NewRow
            R("NAME") = "Chờ phê duyệt"
            R("ID") = "0"
            dtData.Rows.Add(R)
            R2("NAME") = "Phê duyệt"
            R2("ID") = "1"
            dtData.Rows.Add(R2)
            R3("NAME") = "Không phê duyệt"
            R3("ID") = "2"
            dtData.Rows.Add(R3)
            FillRadCombobox(cboStatus, dtData, "NAME", "ID")
            cboStatus.SelectedValue = 0
            rntYear.Value = Today.Date.Year
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try

            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_APPROVE
                    Dim lstApprove As New List(Of RequestDTO)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        If item.GetDataKeyValue("IS_APPROVE") <> 0 Then
                            ShowMessage(Translate("Chỉ thực hiện không phê duyệt trên các đơn đang trạng thái chờ phê duyệt"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    CurrentState = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.MessageText = "Bạn có muốn phê duyệt"
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_REJECT
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("IS_APPROVE") <> 0 Then
                            ShowMessage(Translate("Chỉ thực hiện không phê duyệt trên các đơn đang trạng thái chờ phê duyệt"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlCommon_Reject.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "TR_PortalRequestApprove")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlCommon_Reject_ButtonCommand(ByVal sender As Object, ByVal e As CommandSaveEventArgs) Handles ctrlCommon_Reject.ButtonCommand
        Try
            Dim strComment As String = e.Comment
            Dim ID_REGGROUP As Integer
            Dim IS_SUCCESS As Integer
            Dim EMP_ID As Integer
            For Each dr As GridDataItem In rgData.SelectedItems
                ID_REGGROUP = dr.GetDataKeyValue("ID")
                EMP_ID = dr.GetDataKeyValue("REQUEST_SENDER_ID")
                Dim result = rep.PRI_PROCESS(LogHelper.CurrentUser.EMPLOYEE_ID, EMP_ID, 0, 2, "TRAINING", strComment, ID_REGGROUP)

                If result = 0 Then
                    IS_SUCCESS += 1
                Else
                    IS_SUCCESS -= 1
                End If
            Next

            If IS_SUCCESS >= 1 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                Refresh("UpdateView")
            Else
                CurrentState = CommonMessage.STATE_NORMAL
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = "ACTIVE"
                UpdateControlState()
            End If
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Select Case e.ActionName
                    Case CommonMessage.TOOLBARITEM_DELETE
                        Dim lstDeletes As New List(Of Decimal)
                        For idx = 0 To rgData.SelectedItems.Count - 1
                            Dim item As GridDataItem = rgData.SelectedItems(idx)
                            lstDeletes.Add(item.GetDataKeyValue("ID"))
                        Next
                        If rep.DeleteTrainingRequests(lstDeletes) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                End Select
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
            rwPopup.VisibleOnPageLoad = False
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New RequestDTO
        Dim rep As New TrainingRepository
        Dim total As Integer = 0
        Dim bCheck As Boolean = False
        Try
            Dim _param = New ParamDTO With {.ORG_ID = Nothing, _
                                               .IS_DISSOLVE = 0}
            If Request.Headers("NoSearch") IsNot Nothing Then
                _filter.STATUS_ID = 0
            Else
                If cboStatus.SelectedValue <> String.Empty Then
                    _filter.STATUS_ID = cboStatus.SelectedValue
                End If
                _filter.YEAR = rntYear.Value
            End If
            _filter.EMPLOYEE_ID = LogHelper.CurrentUser.EMPLOYEE_ID
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of RequestDTO)
            If isFull Then
                Return rep.GetTrainingRequestPortalApprove(_filter, 0, Integer.MaxValue, MaximumRows, _param).ToTable()
            Else
                If Sorts IsNot Nothing Then
                    lstData = rep.GetTrainingRequestPortalApprove(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
                Else
                    lstData = rep.GetTrainingRequestPortalApprove(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
                End If
            End If
            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                'datarow("COM_NAME").ToolTip = Utilities.DrawTreeByString(datarow("COM_DESC").Text)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)

            End If

            'If TypeOf (e.Item) Is GridPagerItem Then
            '    Dim myPageSizeCombo As RadComboBox = e.Item.FindControl("PageSizeComboBox")
            '    myPageSizeCombo.Items.Clear()
            '    Dim arrPageSizes() As String = {"10", "20", "50", "100", "200", "500", "1000"}
            '    For x As Integer = 0 To UBound(arrPageSizes)
            '        Dim myRadComboBoxItem As New RadComboBoxItem(arrPageSizes(x))
            '        myPageSizeCombo.Items.Add(myRadComboBoxItem)
            '        'add the following line
            '        myRadComboBoxItem.Attributes.Add("ownerTableViewId", rgWorking.MasterTableView.ClientID)
            '    Next
            '    myPageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = True
            'End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        rgData.Rebind()
    End Sub

#End Region

#Region "Custom"

#End Region

End Class