Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_PlanSummary
    Inherits Common.CommonView
    Protected WithEvents PlanRegView As ViewBase
    Dim repStore As New RecruitmentStoreProcedure

#Region "Property"

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
            rgData.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            'rntxtYear.Value = Date.Now.Year
            If ctrlOrg.CurrentValue IsNot Nothing Then
                Dim listYear As DataTable = repStore.LoadComboboxYear_PlanReg(Int32.Parse(ctrlOrg.CurrentValue))
                FillRadCombobox(cboYear, listYear, "YEAR", "YEAR")
                cboYear.SelectedIndex = 0
            End If

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

    Public Overrides Sub UpdateControlState()


    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")

    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    'Using xls As New ExcelCommon
                    '    Dim dtData = CreateDataFilter(True)
                    '    If dtData.Rows.Count > 0 Then
                    '        rgData.ExportExcel(Server, Response, dtData, "Recruitment")
                    '    End If
                    'End Using
                    Dim rep As New HistaffFrameworkRepository
                    Dim template_URL As String = String.Format("~/ReportTemplates/Recruitment/Report/BM 01 Ke hoach tuyen dung nam.xls")
                    Dim fileName As String = String.Format("Ke hoach tuyen dung nam - {0}.xls", Now.Date.ToString("yyyy"))
                    Dim _error As String = ""
                    Dim ds As New DataSet
                    Dim dTable0 As DataTable = New DataTable()
                    dTable0.Columns.Add("Ngay")
                    dTable0.Columns.Add("Thang")
                    dTable0.Columns.Add("Nam")
                    dTable0.Rows.Add(Now.Date.ToString("dd"), Now.Date.ToString("MM"), Now.Date.ToString("yyyy"))
                    Dim dTable1 As DataTable = CreateDataFilter()
                    ds.Tables.Add(dTable0)
                    ds.Tables(0).TableName = "TABLE0"
                    ds.Tables.Add(dTable1)
                    ds.Tables(1).TableName = "TABLE1"

                    Using xls As New ExcelCommon
                        xls.ExportExcelTemplate(Server.MapPath(template_URL), fileName, ds, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
                    End Using
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try
            Dim listYear As DataTable = repStore.LoadComboboxYear_PlanReg(Int32.Parse(ctrlOrg.CurrentValue))
            FillRadCombobox(cboYear, listYear, "YEAR", "YEAR")
            cboYear.SelectedIndex = 0

            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
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
            rgData.DataSource = CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New PlanRegDTO
        Dim rep As New RecruitmentRepository
        Dim dData As New DataTable
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgData.DataSource = New DataTable
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), _
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve}

            dData = rep.GetPlanSummary(If(cboYear.SelectedValue = "", 0, Decimal.Parse(cboYear.SelectedValue)), _param)
            Return dData
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

    Protected Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
        End If
    End Sub
End Class