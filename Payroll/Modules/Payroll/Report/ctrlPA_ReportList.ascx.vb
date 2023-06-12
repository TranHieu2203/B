Imports Common
Imports Framework.UI
Imports Telerik.Web.UI
Public Class ctrlPA_ReportList
    Inherits Common.CommonView

#Region "Properties"

#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlOrganization.AutoPostBack = False
            ctrlOrganization.LoadDataAfterLoaded = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckBoxes = TreeNodeTypes.None

            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try

            rgEmployeeList.AllowCustomPaging = True
            rgEmployeeList.PageSize = Common.Common.DefaultPageSize
            rgEmployeeList.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export)

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Message = CommonMessage.ACTION_UPDATED Then
                rgEmployeeList.Rebind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_EXPORT
                    If rgEmployeeList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn báo cáo"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    Dim item As GridDataItem = rgEmployeeList.SelectedItems(0)
                    Dim _error As String = ""
                    Using xls As New ExcelCommon
                        xls.ExportExcelTemplate(
                            Server.MapPath("~/ReportTemplates//" & "Payroll" & "/" & "Report" & "/" & item.GetDataKeyValue("NAME") & ".xls "),
                            item.GetDataKeyValue("NAME"), New DataSet, Nothing, Response, _error, ExcelCommon.ExportType.Excel)

                    End Using
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEmployeeList.NeedDataSource
        Try
            Dim dtData = New DataTable
            dtData.Columns.Add("ID")
            dtData.Columns.Add("CODE")
            dtData.Columns.Add("NAME")
            Dim row As DataRow = dtData.NewRow
            row(0) = 1
            row(1) = "PA01"
            row(2) = "Bao cao so sanh luong"
            dtData.Rows.Add(row)
            row = dtData.NewRow
            row(0) = 2
            row(1) = "PA02"
            row(2) = "Bao cao tong hop luong"
            dtData.Rows.Add(row)
            row = dtData.NewRow
            row(0) = 3
            row(1) = "PA03"
            row(2) = "Bao cao thu nhap theo tung tung chuc danh"
            dtData.Rows.Add(row)
            row = dtData.NewRow
            row(0) = 4
            row(1) = "PA04"
            row(2) = "Bao cao tien luong thu nhap theo don vi"
            dtData.Rows.Add(row)

            rgEmployeeList.VirtualItemCount = dtData.Rows.Count
            rgEmployeeList.DataSource = dtData
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgEmployeeList.PageIndexChanged
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

#End Region

End Class