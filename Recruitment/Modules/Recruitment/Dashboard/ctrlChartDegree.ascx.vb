Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI


Public Class ctrlChartDegree
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Dim psp As New RecruitmentRepository
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
    Public Property StatisticData As List(Of StatisticDTO)
        Get
            Return Session(Me.ID & "_StatisticData")
        End Get
        Set(ByVal value As List(Of StatisticDTO))
            Session(Me.ID & "_StatisticData") = value
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


    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Export)

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
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
        Dim store As New Recruitment.RecruitmentStoreProcedure
        Try
            If ctrlOrg.CurrentValue Is Nothing Then
                Exit Sub
            End If
            'Dim _filter As New HRPlaningDetailDTO
            'log = LogHelper.GetUserLog
            '_filter.USER_NAME = log.Username.ToUpper
            '_filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            '_filter.IS_DISSOLVE = ctrlOrg.IsDissolve
            'If StatisticData Is Nothing Or Message = CommonMessage.ACTION_UPDATED Then
            Dim lstData = psp.REPORT_RANK_SAL_BY_YEAR_SAL(2020, ctrlOrg.CurrentValue)

            Dim MONTH_1 = lstData.Sum(Function(m) m.MONTH_1)
            Dim MONTH_2 = lstData.Sum(Function(m) m.MONTH_2)
            Dim MONTH_3 = lstData.Sum(Function(m) m.MONTH_3)
            Dim MONTH_4 = lstData.Sum(Function(m) m.MONTH_4)
            Dim MONTH_5 = lstData.Sum(Function(m) m.MONTH_5)
            Dim MONTH_6 = lstData.Sum(Function(m) m.MONTH_6)
            Dim MONTH_7 = lstData.Sum(Function(m) m.MONTH_7)
            Dim MONTH_8 = lstData.Sum(Function(m) m.MONTH_8)
            Dim MONTH_9 = lstData.Sum(Function(m) m.MONTH_9)
            Dim MONTH_10 = lstData.Sum(Function(m) m.MONTH_10)
            Dim MONTH_11 = lstData.Sum(Function(m) m.MONTH_11)
            Dim MONTH_12 = lstData.Sum(Function(m) m.MONTH_12)

            charData1.Series(0).AddItem(MONTH_1, 1)
            charData1.Series(0).AddItem(MONTH_2, 2)
            charData1.Series(0).AddItem(MONTH_3, 3)
            charData1.Series(0).AddItem(MONTH_4, 4)
            charData1.Series(0).AddItem(MONTH_5, 5)
            charData1.Series(0).AddItem(MONTH_6, 6)
            charData1.Series(0).AddItem(MONTH_7, 7)
            charData1.Series(0).AddItem(MONTH_8, 8)
            charData1.Series(0).AddItem(MONTH_9, 9)
            charData1.Series(0).AddItem(MONTH_10, 10)
            charData1.Series(0).AddItem(MONTH_11, 11)
            charData1.Series(0).AddItem(MONTH_12, 12)

            charData1.Series(1).AddItem(store.REPORT_RANK_SAL_BY_YEAR_SAL1(LogHelper.CurrentUser.USERNAME.ToUpper, 2020, 1, CDec(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve))
            charData1.Series(1).AddItem(MONTH_2, 2)
            charData1.Series(1).AddItem(MONTH_3, 3)
            charData1.Series(1).AddItem(MONTH_4, 4)
            charData1.Series(1).AddItem(MONTH_5, 5)
            charData1.Series(1).AddItem(MONTH_6, 6)
            charData1.Series(1).AddItem(MONTH_7, 7)
            charData1.Series(1).AddItem(MONTH_8, 8)
            charData1.Series(1).AddItem(MONTH_9, 9)
            charData1.Series(1).AddItem(MONTH_10, 10)
            charData1.Series(1).AddItem(MONTH_11, 11)
            charData1.Series(1).AddItem(MONTH_12, 12)
            'Load data
            'charData1.Series(0).DefaultLabelValue = "#Y [#%]"
            'charData1.Series(0).Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.ItemLabels
            'charData1.Series(1).DefaultLabelValue = "#Y [#%]"
            'charData1.Series(1).Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.ItemLabels

            'If Data.Rows.Count > 0 Then
            '    charData1.DataSource = Data
            'Else
            '    charData1.DataSource = Nothing
            'End If
            'charData1.Series(0).DataYColumn = "VALUE"
            'charData1.Series(1).DataYColumn = "VALUE"
            'charData1.Series(1).DataYColumn = "VALUE"
            'charData1.Series(0).AddItem(33, "VALUE")

            'charData1.Series(1).AddItem(44, "VALUEs")
            charData1.ChartTitle.TextBlock.Text = "Quỹ lương"
            charData1.Series(0).Name = "Tổng quỹ lương theo định biên"
            charData1.Series(1).Name = "Tổng chi phí lương thực tế"
            charData1.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
        ''Dim radChart As New RadChart()
        ''RadChart.ChartTitle.TextBlock.Text = "My RadChart"
        '' Create a ChartSeries and assign its name and chart type
        'Dim chartSeries As New ChartSeries()
        'chartSeries.Name = "Sales"
        'chartSeries.Type = ChartSeriesType.Bar
        '' add new items to the series,
        '' passing a value and a label string
        'chartSeries.AddItem(120, "Internet")
        'chartSeries.AddItem(140, "Retail")
        'chartSeries.AddItem(35, "Wholesale")
        '' add the series to the RadChart Series collection
        'charData1.Series.Add(chartSeries)
        '' add the RadChart to the page.
        'Me.Page.Controls.Add(charData1)
        ''Dim chartSeries As ChartSeries = radChart.CreateSeries("Sales", System.Drawing.Color.RoyalBlue, System.Drawing.Color.LightSteelBlue, ChartSeriesType.Bar)
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

    'Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
    '    Try
    '        If dsData IsNot Nothing Then
    '            Dim dtData = dsData.Tables(1)
    '            If Not IsPostBack Then
    '                DesignGrid(dtData)
    '            End If
    '            rgData.DataSource = dtData
    '            rgData.VirtualItemCount = dtData.Rows.Count
    '        End If

    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try

    'End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged

        Try
            Refresh()
            'rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub charData1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Charting.ChartItemDataBoundEventArgs) Handles charData1.ItemDataBound
        'Dim data As DataRowView = DirectCast(e.DataItem, DataRowView)
        'e.SeriesItem.Name = data("NAME")

        'Dim i As Int32 = charData1.Series(0).Items.Count - 1

        'Select Case i
        '    Case 0
        '        charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
        '        charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(25, 55, 0)
        '        charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(54, 25, 30)
        '    Case 1
        '        charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
        '        charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(159, 218, 239)
        '        charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(212, 245, 255)
        '    Case 2
        '        charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
        '        charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(157, 209, 54)
        '        charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(210, 247, 114)
        '    Case 3
        '        charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
        '        charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(255, 126, 97)
        '        charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(238, 171, 151)
        '    Case 4
        '        charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
        '        charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(255, 193, 103)
        '        charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(255, 232, 204)
        '    Case 5
        '        charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
        '        charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(76, 171, 205)
        '        charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(134, 219, 244)
        '    Case 6
        '        charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
        '        charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(255, 255, 255)
        '        charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(255, 84, 84)
        '    Case 7
        '        charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
        '        charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(78, 149, 197)
        '        charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(32, 98, 162)
        '    Case 8
        '        charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
        '        charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(8, 178, 0)
        '        charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(204, 0, 0)
        '    Case 9
        '        charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
        '        charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(8, 178, 0)
        '        charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(0, 255, 153)
        'End Select
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
    'Protected Sub DesignGrid(ByVal dt As DataTable)
    '    Dim rCol As GridBoundColumn
    '    Dim rColCheck As GridClientSelectColumn
    '    Dim rColFile As GridButtonColumn
    '    Dim rColDate As GridDateTimeColumn
    '    rgData.MasterTableView.Columns.Clear()
    '    For Each column As DataColumn In dt.Columns
    '        If column.ColumnName = "CBSTATUS" Then
    '            rColCheck = New GridClientSelectColumn()
    '            rgData.MasterTableView.Columns.Add(rColCheck)
    '            rColCheck.HeaderStyle.Width = 30
    '            rColCheck.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
    '        End If
    '        If Not column.ColumnName.Contains("ID") And column.ColumnName <> "ExtensionData" And _
    '         column.ColumnName <> "CBSTATUS" And Not column.ColumnName.Contains("DATE") Then
    '            rCol = New GridBoundColumn()
    '            rgData.MasterTableView.Columns.Add(rCol)
    '            rCol.DataField = column.ColumnName
    '            rCol.HeaderText = Translate(column.ColumnName)
    '            rCol.HeaderStyle.Width = 150
    '            rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
    '            rCol.AutoPostBackOnFilter = True
    '            rCol.CurrentFilterFunction = GridKnownFunction.Contains
    '            rCol.ShowFilterIcon = False
    '            rCol.FilterControlWidth = 130
    '            rCol.HeaderTooltip = Translate(column.ColumnName)
    '            rCol.FilterControlToolTip = Translate(column.ColumnName)
    '        End If
    '        If column.ColumnName.Contains("FILE_BYTE_ID") Then
    '            rCol = New GridBoundColumn()
    '            rgData.MasterTableView.Columns.Add(rCol)
    '            rCol.DataField = column.ColumnName
    '            rCol.HeaderText = Translate(column.ColumnName)
    '            rCol.Visible = False
    '        End If
    '        If column.ColumnName.Contains("DATE") Then
    '            rColDate = New GridDateTimeColumn()
    '            rgData.MasterTableView.Columns.Add(rColDate)
    '            rColDate.DataField = column.ColumnName
    '            rColDate.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
    '            rColDate.HeaderText = Translate(column.ColumnName)
    '            rColDate.HeaderStyle.Width = 150
    '            rColDate.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
    '            rColDate.AutoPostBackOnFilter = True
    '            rColDate.CurrentFilterFunction = GridKnownFunction.EqualTo
    '            rColDate.ShowFilterIcon = False
    '            rColDate.FilterControlWidth = 130
    '            rColDate.HeaderTooltip = Translate(column.ColumnName)
    '            rColDate.FilterControlToolTip = Translate(column.ColumnName)
    '        End If
    '    Next
    'End Sub

    Public Sub ExportToExcel()
        Dim _error As Integer = 0
        Using xls As New ExcelCommon
            If dsData IsNot Nothing AndAlso dsData.Tables(1).Rows.Count > 0 Then
                Dim bCheck = xls.ExportExcelTemplate(
                Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/Empls_By_Knowledge.xlsx"),
                "Co_Cau_Nhan_Su_Theo_Trinh_Do", dsData.Tables(1), Response, _error)
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