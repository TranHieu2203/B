Imports System.Drawing
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI

Public Class ctrlPA_PortalSalary
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
    Public IDSelect As Integer
    Public Property vPAHoldSalary As List(Of PAHoldSalaryDTO)
        Get
            Return ViewState(Me.ID & "_HoldSalary")
        End Get
        Set(ByVal value As List(Of PAHoldSalaryDTO))
            ViewState(Me.ID & "_HoldSalary") = value
        End Set
    End Property
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    Public Property EmployeeID As Decimal
#End Region

#Region "Page"
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()


    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Public Overrides Sub BindData()
        Try

            Dim rep As New PayrollRepository
            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2015 To Date.Now.Year + 1
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year

            If cboYear.Text.Length = 4 Then
                rcboPeriod.ClearSelection()
                Dim data = rep.GetPeriodbyYear(cboYear.SelectedValue)
                ' Dim query = From p In data Where p.YEAR <> 2020 Or (p.YEAR = 2020 And p.MONTH <> 1 And p.MONTH <> 2 And p.MONTH <> 3 And p.MONTH <> 4 And p.MONTH <> 5 And p.MONTH <> 6 And p.MONTH <> 7)
                rcboPeriod.DataSource = data.OrderBy(Function(x) x.MONTH).ToList
                rcboPeriod.DataValueField = "ID"
                rcboPeriod.DataTextField = "PERIOD_NAME"
                rcboPeriod.DataBind()
                rcboPeriod.SelectedIndex = 0
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Event"


    Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim rep As New PayrollRepository
        Try
            If cboYear.Text.Length = 4 Then
                rcboPeriod.ClearSelection()
                Dim data = rep.GetPeriodbyYear(cboYear.SelectedValue)
                Dim query = From p In data Order By p.MONTH Ascending
                'Dim query = From p In data Where p.YEAR <> 2020 Or (p.YEAR = 2020 And p.MONTH <> 1 And p.MONTH <> 2 And p.MONTH <> 3 And p.MONTH <> 4 And p.MONTH <> 5 And p.MONTH <> 6 And p.MONTH <> 7)
                rcboPeriod.DataSource = data.OrderBy(Function(x) x.MONTH).ToList
                rcboPeriod.DataValueField = "ID"
                rcboPeriod.DataTextField = "PERIOD_NAME"
                rcboPeriod.DataBind()
                rcboPeriod.SelectedIndex = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rcboPeriod_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rcboPeriod.SelectedIndexChanged
        Dim rep As New PayrollRepository
        Try
            If rcboPeriod.SelectedValue <> "" Then
                btnSearch_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            Dim dtable As DataTable
            Dim dtableCheck As DataTable
            Dim rp As New PayrollRepository

            If rcboPeriod.SelectedValue = "" Then
                ShowMessage(Translate("Chưa chọn kỳ lương ."), NotifyType.Warning)
                Exit Sub
            End If
            'kiem tra ky luong phai dong moi xem duoc

            dtableCheck = rp.CHECK_OPEN_CLOSE(rcboPeriod.SelectedValue, EmployeeID)

            If dtableCheck.Rows(0)("ID").ToString() = "0" Then
                ShowMessage(Translate("Kỳ lương đang mở, không xem được."), NotifyType.Warning)
                Exit Sub
            End If

            Dim hfr As New HistaffFrameworkRepository
            'Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_BUSINESS.GET_PHIEULUONG_PORTAL", New List(Of Object)(New Object() {LogHelper.CurrentUser.EMPLOYEE_ID,
            '                                                                                                             rcboPeriod.SelectedValue,
            '                                                                                                             FrameworkUtilities.OUT_CURSOR}))
            'Dim dt As DataTable
            Dim tabSource As DataTable
            Dim tabSource1 As DataTable
            'If ds.Tables(0).Rows.Count > 0 Then
            '    dt = ds.Tables(0)
            'Else
            '    ShowMessage("Không có thông tin lương tháng này", NotifyType.Warning)
            '    Exit Sub
            'End If
            'Dim rData = dt
            'rData.TableName = "DATA"
            'Dim filePath = System.AppDomain.CurrentDomain.BaseDirectory + "ReportTemplates\Payroll\Report\PhieuLuongNV.xlsx"
            'Using xls As New ExcelCommon
            '    tabSource = xls.ExportExcelToTable(filePath, "PhieuLuong", rData, Response)
            'End Using
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_BUSINESS.GET_PAYSLIP_PORTAL1", New List(Of Object)(New Object() {LogHelper.CurrentUser.EMPLOYEE_ID,
                                                                                                                         rcboPeriod.SelectedValue,
                                                                                                                       FrameworkUtilities.OUT_CURSOR}))
            tabSource = ds.Tables(0)
            tabSource1 = ds.Tables(1)
            rgMain.PageSize = 30
            rgMain.Columns(1).HeaderText = "a"
            rgMain.Columns(2).HeaderText = "b"
            rgMain.DataSource = tabSource
            rgMain.ShowHeader = False
            rgMain.MasterTableView.AllowPaging = False
            rgMain.MasterTableView.AllowSorting = False
            rgMain.MasterTableView.AllowCustomPaging = False
            rgMain.MasterTableView.AllowCustomSorting = False
            rgMain.MasterTableView.NoMasterRecordsText = ""
            rgMain.MasterTableView.NoDetailRecordsText = ""
            rgMain.DataBind()
            rgMain1.PageSize = 30
            rgMain1.Columns(1).HeaderText = "a"
            rgMain1.Columns(2).HeaderText = "b"
            rgMain1.DataSource = tabSource1
            rgMain1.ShowHeader = False
            rgMain1.MasterTableView.AllowPaging = False
            rgMain1.MasterTableView.AllowSorting = False
            rgMain1.MasterTableView.AllowCustomPaging = False
            rgMain1.MasterTableView.AllowCustomSorting = False
            rgMain1.MasterTableView.NoMasterRecordsText = ""
            rgMain1.MasterTableView.NoDetailRecordsText = ""
            rgMain1.DataBind()
            rgMain1.Enabled = False
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        'If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
        '    'CurrentState = CommonMessage.STATE_DELETE
        '    'UpdateControlState()
        'End If
    End Sub


#End Region

#Region "Custom"
#End Region

    Private Function lblCL2() As Object
        Throw New NotImplementedException
    End Function

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Try
            If Not IsNumeric(rcboPeriod.SelectedValue) Then
                ShowMessage("Bạn phải chọn kỳ lương", NotifyType.Warning)
                Exit Sub
            End If
            Dim hfr As New HistaffFrameworkRepository
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_BUSINESS.GET_PHIEULUONG_PORTAL", New List(Of Object)(New Object() {LogHelper.CurrentUser.EMPLOYEE_ID,
                                                                                                                         rcboPeriod.SelectedValue,
                                                                                                                         FrameworkUtilities.OUT_CURSOR}))


            Dim dt As DataTable
            If ds.Tables(0).Rows.Count > 0 Then
                dt = ds.Tables(0)
            Else
                ShowMessage("Không có thông tin lương tháng này", NotifyType.Warning)
                Exit Sub
            End If
            Dim rData = dt
            rData.TableName = "DATA"
            Dim filePath = System.AppDomain.CurrentDomain.BaseDirectory + "ReportTemplates\Payroll\Report\PhieuLuongNV.xlsx"
            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(filePath, "PhieuLuong", rData, Response)
            End Using

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgMain_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgMain.ItemDataBound
        Try
            For Each item As GridDataItem In rgMain.Items
                Dim strColor = item("COLOR").Text
                Dim strBool = CBool(item("IS_BOLD").Text)
                If strColor <> "&nbsp;" Then

                    item("COLUMN2").ForeColor = Color.FromName(strColor)
                    item("COLUMN3").ForeColor = Color.FromName(strColor)
                End If
                If strBool = True Then
                    item("COLUMN2").Style.Add("Font-weight", "bold !important")
                    item("COLUMN2").Font.Bold = True
                    item("COLUMN3").Style.Add("Font-weight", "bold !important")
                    item("COLUMN3").Font.Bold = True
                End If
            Next
            e.Canceled = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rgMain1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgMain1.ItemDataBound
        Try
            For Each item As GridDataItem In rgMain1.Items
                Dim strColor = item("COLOR").Text
                Dim strBool = CBool(item("IS_BOLD").Text)
                If strColor <> "&nbsp;" Then

                    item("COLUMN2").ForeColor = Color.FromName(strColor)
                    item("COLUMN3").ForeColor = Color.FromName(strColor)
                End If
                If strBool = True Then
                    item("COLUMN2").Style.Add("Font-weight", "bold !important")
                    item("COLUMN2").Font.Bold = True
                    item("COLUMN3").Style.Add("Font-weight", "bold !important")
                    item("COLUMN3").Font.Bold = True
                End If
            Next
            e.Canceled = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub rgMain_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
    '    rgMain.DataSource = New DataTable
    'End Sub
End Class