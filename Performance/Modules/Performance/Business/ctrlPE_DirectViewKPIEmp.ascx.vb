Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI

Public Class ctrlPE_DirectViewKPIEmp
    Inherits Common.CommonView
    Protected WithEvents RequestView As ViewBase

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

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgData.SetFilter()
            rgData.AllowCustomPaging = True

            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            rgData.PageSize = Common.Common.DefaultPageSize
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Print)
            CType(MainToolBar.Items(0), RadToolBarButton).Text = "In đánh giá"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            'Me.MainToolBar = tbarMain
            'Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Delete)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New PerformanceRepository
        Try
            'Select Case CurrentState
            '    Case CommonMessage.STATE_DELETE
            '        Dim lstDeletes As New List(Of Decimal)
            '        For idx = 0 To rgData.SelectedItems.Count - 1
            '            Dim item As GridDataItem = rgData.SelectedItems(idx)
            '            lstDeletes.Add(item.GetDataKeyValue("ID"))
            '        Next
            '        CurrentState = CommonMessage.STATE_NORMAL
            '        If rep.DeletePlans(lstDeletes) Then
            '            Refresh("UpdateView")
            '            UpdateControlState()
            '        Else
            '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
            '            UpdateControlState()
            '        End If
            'End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New PerformanceRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                rntYear.Value = Date.Now.Year
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

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objAssessment As New AssessmentDTO
        Dim rep As New PerformanceRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_PRINT
                    Dim dsData As DataSet
                    Dim sPath As String = ""

                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi in ."), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate("Chỉ được chọn 1 bản ghi để in ."), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If

                    'Kiểm tra các điều kiện trước khi in
                    Dim lstEmpID As String = ""
                    Dim srtImage As String = ""

                    For Each dr As Telerik.Web.UI.GridDataItem In rgData.SelectedItems
                        dsData = rep.PRINT_PE_ASSESS(dr.GetDataKeyValue("EMPLOYEE_ID"), dr.GetDataKeyValue("PE_PERIO_ID"), dr.GetDataKeyValue("OBJECT_GROUP_ID"))
                    Next

                    sPath = Server.MapPath("~/ReportTemplates/Performance/Report/BC_DANHGIA.xls")

                    Using xls As New ExcelCommon
                        xls.ExportExcelTemplate(sPath,
                            "InDanhGia", Nothing, dsData, Nothing, Response, "", ExcelCommon.ExportType.Excel)
                    End Using
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
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

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        rgData.Rebind()
    End Sub

#End Region

#Region "Custom"

    Protected Sub CreateDataFilter(Optional ByVal isFull As Boolean = False)
        Dim _filter As New AssessmentDirectDTO
        Dim rep As New PerformanceRepository
        Dim total As Integer = 0
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgData.DataSource = New List(Of AssessmentDirectDTO)
                Exit Sub
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), _
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve}
            _filter.PE_PERIO_YEAR = rntYear.Value
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of AssessmentDirectDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetDirectKPIEmployee(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
            Else
                lstData = rep.GetDirectKPIEmployee(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
            End If
            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class