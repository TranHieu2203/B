Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Charting
Imports Telerik.Web.UI

Public Class ctrlChartWageByMonth
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Dim psp As New RecruitmentRepository
    Private store As New RecruitmentStoreProcedure()
    Dim cons As New Contant_OtherList_Iprofile
    Dim com As New CommonProcedureNew
    Dim log As New UserLog
#Region "Property"
    Private Property dsData As DataSet
        Get
            Return PageViewState(Me.ID & "_dsData")
        End Get
        Set(ByVal value As DataSet)
            PageViewState(Me.ID & "_dsData") = value
        End Set


    End Property
    Public Property StatisticType As String
        Get
            Return Session(Me.ID & "_StatisticType")
        End Get
        Set(ByVal value As String)
            Session(Me.ID & "_StatisticType") = value
        End Set
    End Property
    Public Property StatisticData As List(Of RecruitmentBusiness.StatisticDTO)
        Get
            Return Session(Me.ID & "_StatisticData")
        End Get
        Set(ByVal value As List(Of RecruitmentBusiness.StatisticDTO))
            Session(Me.ID & "_StatisticData") = value
        End Set
    End Property
    Public Property Data_Plan As List(Of HRPlaningDetailDTO)
        Get
            Return Session(Me.ID & "_Data_Plan")
        End Get
        Set(ByVal value As List(Of HRPlaningDetailDTO))
            Session(Me.ID & "_Data_Plan") = value
        End Set
    End Property
    Public Property StatisticList As List(Of Profile.ProfileBusiness.OtherListDTO)
        Get
            Return Session(Me.ID & "_StatisticList")
        End Get
        Set(ByVal value As List(Of Profile.ProfileBusiness.OtherListDTO))
            Session(Me.ID & "_StatisticList") = value
        End Set
    End Property
#End Region

#Region "Page"
    Private width As Integer
    Private height As Integer
    Public _year As Integer = Date.Now.Year
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            'Me.MainToolBar = tbarMain
            ''Common.Common.BuildToolbar(Me.MainToolBar,
            ''                           ToolbarItem.Export)

            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            'CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        'rgData.SetFilter()
        'rgData.AllowCustomPaging = True
        'rgData.PageSize = Common.Common.DefaultPageSize
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Try
            If Request("width") IsNot Nothing Then
                width = CInt(Request("width"))
            End If
            If Request("height") IsNot Nothing Then
                height = CInt(Request("height"))
            End If
            Dim resize As Integer = 0
            If Request("resize") IsNot Nothing Then
                resize = CInt(Request("resize"))
            End If
            If Not IsPostBack Then
                Refresh(CommonMessage.ACTION_UPDATED)
            Else
                Refresh()
            End If
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
        'Dim store As New RecruitmentStoreProcedure()
        'Try
        '    If ctrlOrg.CurrentValue Is Nothing Then
        '        Exit Sub
        '    End If
        '    Dim Data As New DataTable
        '    If StatisticData Is Nothing Or Message = CommonMessage.ACTION_UPDATED Then
        '        Data = store.GetStatisticGender(LogHelper.GetUserLog.Username.ToUpper, 2020)
        '    End If
        '    charData1.Width = CInt(If(width = 0, charData1.Width.Value, width))
        '    charData1.Height = CInt(If(height = 0, charData1.Height.Value, height))
        '    Dim DATEs = DateAdd("m", 1, DateSerial(Year(Today), Month(Today), 0))
        '    'Đặt title cho chart
        '    charData1.ChartTitle.TextBlock.Text = ""
        '    'charData1.ChartTitle.TextBlock.Text = "Thống kê theo giới tính"
        '    'charData1.ChartTitle.Visible = False

        '    'Load data
        '    charData1.Series(0).DefaultLabelValue = "#Y [#%]"
        '    charData1.Series(0).Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.ItemLabels

        '    If Data.Rows.Count > 0 Then
        '        charData1.DataSource = Data
        '    Else
        '        charData1.DataSource = Nothing
        '    End If
        '    charData1.Series(0).DataYColumn = "VALUE"
        '    charData1.DataBind()
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub
#End Region

#Region "Event"

    'Private Sub rgContract_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgData.ItemCommand
    '    Dim sv_sID As String = String.Empty
    '    If e.CommandName = Telerik.Web.UI.RadGrid.ExportToExcelCommandName Then
    '        Using xls As New ExcelCommon
    '            If dsData IsNot Nothing AndAlso dsData.Tables(1).Rows.Count > 0 Then
    '                rgData.ExportExcel(Server, Response, dsData.Tables(1), "DanhSachNhanVien")
    '            Else
    '                ShowMessage(Translate("Không có dữ liệu để xuất báo cáo"), Utilities.NotifyType.Warning)
    '                Exit Sub
    '            End If
    '        End Using
    '        'ExportToExcel(rgData)
    '        e.Canceled = True
    '    End If
    'End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            'If dsData IsNot Nothing Then
            '    Dim dtData = dsData.Tables(1)
            '    If Not IsPostBack Then
            '        DesignGrid(dtData)
            '    End If
            'Dim lstData As List(Of HRYearPlaningDTO)
            'Dim A = rnYear.Text

            'aaaa.Month
            Dim Month = Today.Month
            Dim Year = Today.Year
            If IsDate(rdTuThang.SelectedDate) Then
                Dim Dates As DateTime = rdTuThang.SelectedDate
                Month = Dates.Month
                Year = Dates.Year
            End If
            Dim lstData = psp.REPORT_RANK_SAL_BY_MONTH(Year, Month, ctrlOrg.CurrentValue)
            rgData.DataSource = lstData
            rgData.VirtualItemCount = lstData.Count
            Dim ACTUAL_WAGE_FUND = lstData.Sum(Function(m) m.ACTUAL_WAGE_FUND)
            Dim WAGE_FUNDS = lstData.Sum(Function(m) m.WAGE_FUNDS)
            Dim Data As New DataTable()
            Data.Columns.AddRange(New DataColumn(1) {New DataColumn("NAME", GetType(String)),
                                                New DataColumn("VALUE", GetType(Decimal))})
            Data.Rows.Add("Chi phí thực tế", ACTUAL_WAGE_FUND)
            Data.Rows.Add("Quỹ lương còn lại", WAGE_FUNDS)

            charData1.Width = CInt(If(width = 0, charData1.Width.Value, width))
            charData1.Height = CInt(If(height = 0, charData1.Height.Value, height))
            'Đặt title cho chart
            charData1.ChartTitle.TextBlock.Text = ""
            'charData1.ChartTitle.TextBlock.Text = "Thống kê theo giới tính"
            'charData1.ChartTitle.Visible = False

            'Load data
            charData1.Series(0).DefaultLabelValue = "#Y [#%]"
            charData1.Series(0).Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.ItemLabels

            If Data.Rows.Count > 0 Then
                charData1.DataSource = Data
            Else
                charData1.DataSource = Nothing
            End If
            charData1.Series(0).DataYColumn = "VALUE"
            charData1.DataBind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged

        Try
            Refresh()
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub charData1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Charting.ChartItemDataBoundEventArgs) Handles charData1.ItemDataBound
        Dim data As DataRowView = DirectCast(e.DataItem, DataRowView)
        e.SeriesItem.Name = data("NAME")

        For i As Integer = 0 To charData1.Series(0).Items.Count - 1
            Select Case i Mod 9
                Case 0
                    charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
                    charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(78, 149, 197)
                    charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(32, 98, 162)
                Case 1
                    charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
                    charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(159, 218, 239)
                    charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(212, 245, 255)
                Case 2
                    charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
                    charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(157, 209, 54)
                    charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(210, 247, 114)
                Case 3
                    charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
                    charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(255, 126, 97)
                    charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(238, 171, 151)
                Case 4
                    charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
                    charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(255, 193, 103)
                    charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(255, 232, 204)
                Case 5
                    charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
                    charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(76, 171, 205)
                    charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(134, 219, 244)
                Case 6
                    charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
                    charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(255, 255, 255)
                    charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(255, 84, 84)
                Case 7
                    charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
                    charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(252, 255, 0)
                    charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(254, 255, 130)
                Case 8
                    charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
                    charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(8, 178, 0)
                    charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(103, 248, 95)
            End Select
        Next
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            Refresh()
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case Common.CommonMessage.TOOLBARITEM_EXPORT
                    ExportToExcel()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    Protected Sub DesignGrid(ByVal dt As DataTable)
        Dim rCol As GridBoundColumn
        Dim rColCheck As GridClientSelectColumn
        Dim rColFile As GridButtonColumn
        Dim rColDate As GridDateTimeColumn
        rgData.MasterTableView.Columns.Clear()
        For Each column As DataColumn In dt.Columns
            If column.ColumnName = "CBSTATUS" Then
                rColCheck = New GridClientSelectColumn()
                rgData.MasterTableView.Columns.Add(rColCheck)
                rColCheck.HeaderStyle.Width = 30
                rColCheck.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            End If
            If Not column.ColumnName.Contains("ID") And column.ColumnName <> "ExtensionData" And
             column.ColumnName <> "CBSTATUS" And Not column.ColumnName.Contains("DATE") Then
                rCol = New GridBoundColumn()
                rgData.MasterTableView.Columns.Add(rCol)
                rCol.DataField = column.ColumnName
                rCol.HeaderText = Translate(column.ColumnName)
                rCol.HeaderStyle.Width = 150
                rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rCol.AutoPostBackOnFilter = True
                rCol.CurrentFilterFunction = GridKnownFunction.Contains
                rCol.ShowFilterIcon = False
                rCol.FilterControlWidth = 130
                rCol.HeaderTooltip = Translate(column.ColumnName)
                rCol.FilterControlToolTip = Translate(column.ColumnName)
            End If
            If column.ColumnName.Contains("FILE_BYTE_ID") Then
                rCol = New GridBoundColumn()
                rgData.MasterTableView.Columns.Add(rCol)
                rCol.DataField = column.ColumnName
                rCol.HeaderText = Translate(column.ColumnName)
                rCol.Visible = False
            End If
            If column.ColumnName.Contains("DATE") Then
                rColDate = New GridDateTimeColumn()
                rgData.MasterTableView.Columns.Add(rColDate)
                rColDate.DataField = column.ColumnName
                rColDate.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
                rColDate.HeaderText = Translate(column.ColumnName)
                rColDate.HeaderStyle.Width = 150
                rColDate.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rColDate.AutoPostBackOnFilter = True
                rColDate.CurrentFilterFunction = GridKnownFunction.EqualTo
                rColDate.ShowFilterIcon = False
                rColDate.FilterControlWidth = 130
                rColDate.HeaderTooltip = Translate(column.ColumnName)
                rColDate.FilterControlToolTip = Translate(column.ColumnName)
            End If
        Next
    End Sub

    Public Sub ExportToExcel()
        Dim _error As Integer = 0
        Using xls As New ExcelCommon
            If dsData IsNot Nothing AndAlso dsData.Tables(1).Rows.Count > 0 Then
                Dim bCheck = xls.ExportExcelTemplate(
                Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/Empls_By_Gender.xlsx"),
                "Co_Cau_Nhan_Su_Theo_Gioi_Tinh", dsData.Tables(1), Response, _error, ExcelCommon.ExportType.Excel)
                If Not bCheck Then
                    Select Case _error
                        Case 1
                            ShowMessage(Translate("Mẫu báo cáo không tồn tại"), NotifyType.Warning)
                        Case 2
                            ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
                    End Select
                    Exit Sub
                End If
            Else
                ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
            End If

        End Using
    End Sub

#End Region

End Class