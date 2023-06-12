Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness
Public Class ctrlTR_ReportList
    Inherits Common.CommonView

#Region "Properties"
    Dim dtable As DataTable
    Dim dsData As DataSet

    Dim rp As New TrainingRepository

    Dim wbook As Aspose.Cells.Workbook
    Dim sPath As String = ""
    Dim _error As String = ""
    Dim sOrgID As String

#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlOrganization.AutoPostBack = False
            ctrlOrganization.LoadDataAfterLoaded = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckBoxes = TreeNodeTypes.All
            ctrlOrganization.CheckParentNodes = False
            ctrlOrganization.CheckChildNodes = True

            If rgEmployeeList.SelectedItems.Count > 0 Then
                Dim item As GridDataItem = rgEmployeeList.SelectedItems(0)
                UpdateControlState(item.GetDataKeyValue("CODE"))
            Else
                rdFromDate.Visible = False
                rdToDate.Visible = False
                rdMonth.Visible = True

                cboMonth.Visible = False
                cboYear.Visible = False

                lblFromDate.Visible = False
                lblToDate.Visible = False
                lblMonth.Visible = True
                lblMonthMin.Visible = False
                lblYear.Visible = False

                rdFromDate.SelectedDate = Nothing
                rdToDate.SelectedDate = Nothing
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try

            rgEmployeeList.AllowCustomPaging = True
            'rgEmployeeList.ClientSettings.EnablePostBackOnRowClick = False
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
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
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

            Dim tableMonth As New DataTable
            tableMonth.Columns.Add("MONTH", GetType(String))
            tableMonth.Columns.Add("ID", GetType(String))
            Dim rowMonth As DataRow
            For index = 0 To 12
                rowMonth = tableMonth.NewRow

                If index = 0 Then
                    rowMonth("ID") = 0
                    rowMonth("MONTH") = ""
                    tableMonth.Rows.Add(rowMonth)
                Else
                    rowMonth("ID") = index
                    rowMonth("MONTH") = index
                    tableMonth.Rows.Add(rowMonth)
                End If
            Next
            FillRadCombobox(cboMonth, tableMonth, "MONTH", "ID")
            cboMonth.SelectedValue = Date.Now.Month

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try

            If rgEmployeeList.SelectedItems.Count <= 0 Then
                ShowMessage(Translate("Bạn phải chọn báo cáo"), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            If ctrlOrganization.CheckedValues.Count = 0 Then
                ShowMessage("Bạn chưa tích chọn đơn vị", NotifyType.Warning)
                Exit Sub
            End If
            Dim item As GridDataItem = rgEmployeeList.SelectedItems(0)

            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_EXPORT

                    sOrgID = ctrlOrganization.CheckedValues.Aggregate(Function(x, y) x & "," & y)

                    If rgEmployeeList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn báo cáo"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If

                    Select Case item.GetDataKeyValue("CODE")
                        Case "TR01" ' Bao cao dao tao
                            If rdFromDate.SelectedDate Is Nothing And rdToDate.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            TR01(item.GetDataKeyValue("NAME"))

                        Case "TR02" '  Bao cáo qua trinh dao tao
                            If rdFromDate.SelectedDate Is Nothing And rdToDate.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            TR02(item.GetDataKeyValue("NAME"))

                        Case "TR03" 'Bao cao thong ke chi phi
                            If rdFromDate.SelectedDate Is Nothing And rdToDate.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            TR03(item.GetDataKeyValue("NAME"))

                        Case "TR04" ' KE HOACH DAO TAO
                            If rdFromDate.SelectedDate Is Nothing And rdToDate.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            TR04(item.GetDataKeyValue("NAME"))
                    End Select



            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEmployeeList.NeedDataSource
        Try
            Dim rep As New TrainingRepository
            Dim _filter As New Se_ReportDTO
            Dim listReport As List(Of Se_ReportDTO)

            Try
                Dim MaximumRows As Integer
                SetValueObjectByRadGrid(rgEmployeeList, _filter)
                Dim Sorts As String = rgEmployeeList.MasterTableView.SortExpressions.GetSortString()
                _filter.MODULE_ID = 7

                If Sorts IsNot Nothing Then
                    listReport = rep.GetReportById(_filter, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows, Sorts)
                Else
                    listReport = rep.GetReportById(_filter, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows)
                End If

                rgEmployeeList.VirtualItemCount = MaximumRows
                rgEmployeeList.DataSource = listReport

            Catch ex As Exception
                Throw ex
            End Try


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

    Private Sub rgEmployeeList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgEmployeeList.SelectedIndexChanged
        Try
            If rgEmployeeList.SelectedItems.Count = 0 Then Exit Sub

            Dim item As GridDataItem = rgEmployeeList.SelectedItems(0)
            UpdateControlState(item.GetDataKeyValue("CODE"))

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    Private Sub TR01(ByVal strName As String)
        Try
            dsData = rp.ExportReport("PKG_TRAINING_REPORT.TR01",
                                     rdFromDate.SelectedDate,
                                     rdToDate.SelectedDate,
                                     sOrgID,
                                     ctrlOrganization.IsDissolve, "")
            sPath = Server.MapPath("~/ReportTemplates/Training/Report" & "/Bao cao dao tao.xls")

            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel, False)
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub TR02(ByVal strName As String)
        Try
            dsData = rp.ExportReport("PKG_TRAINING_REPORT.TR02", rdFromDate.SelectedDate, rdToDate.SelectedDate, sOrgID, ctrlOrganization.IsDissolve, "")
            sPath = Server.MapPath("~/ReportTemplates/Training/Report" & "/Bao cao qua trinh dao tao.xls")


            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using
            'Using xls As New ExcelCommon
            '    xls.ExportExcelTemplate(sPath, strName, New DataSet, Nothing, Response, _error, ExcelCommon.ExportType.Excel, False)
            'End Using

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub TR03(ByVal strName As String)
        Try
            dsData = rp.ExportReport("PKG_TRAINING_REPORT.TR03", rdFromDate.SelectedDate, rdToDate.SelectedDate, sOrgID, ctrlOrganization.IsDissolve, "")
            sPath = Server.MapPath("~/ReportTemplates/Training/Report" & "/Bao cao thong ke chi phi.xls")

            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub TR04(ByVal strName As String)
        Try

            dsData = rp.ExportReport("PKG_TRAINING_REPORT.TR04", rdFromDate.SelectedDate, rdToDate.SelectedDate, sOrgID, ctrlOrganization.IsDissolve, "")
            sPath = Server.MapPath("~/ReportTemplates/Training/Report" & "/Ke hoach dao tao.xls")

            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Sub UpdateControlState(ByVal strReportID As String)
        Try
            rdFromDate.Visible = False
            rdToDate.Visible = False
            rdMonth.Visible = False
            cboMonth.Visible = False
            cboYear.Visible = False

            lblFromDate.Visible = False
            lblToDate.Visible = False
            lblMonth.Visible = False
            lblMonthMin.Visible = False
            lblYear.Visible = False

            rdFromDate.SelectedDate = Nothing
            rdToDate.SelectedDate = Nothing

            Select Case strReportID
                Case "TR01" 'Bao cao dao tao
                    rdFromDate.Visible = True
                    rdToDate.Visible = True
                    lblFromDate.Visible = True
                    lblToDate.Visible = True
                Case "TR02" ' Bao cáo qua trinh dao tao
                    rdFromDate.Visible = True
                    rdToDate.Visible = True
                    lblFromDate.Visible = True
                    lblToDate.Visible = True
                Case "TR03" ' Bao cao thong ke chi phi
                    rdFromDate.Visible = True
                    rdToDate.Visible = True
                    lblFromDate.Visible = True
                    lblToDate.Visible = True
                Case "TR04" ' Ke hoach dao tao
                    rdFromDate.Visible = True
                    rdToDate.Visible = True
                    lblFromDate.Visible = True
                    lblToDate.Visible = True
            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

End Class