Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI
Imports Common.Common
Imports Common.CommonMessage
Imports Profile.ProfileBusiness
Imports Newtonsoft.Json.Linq
Imports Aspose.Cells
Imports System.IO

Public Class ctrlHU_ReportList
    Inherits Common.CommonView

#Region "Properties"
    Public name As String = "Số lượng nhân sự"
    Public name2 As String = "Bậc lao động"
    Public name3 As String = "Thâm niên công tác"
    Public name4 As String = "Thống kê số lượng nhân viên tại nơi làm việc"
    Public name5 As String = "Thống kê lao động theo độ tuổi lao động"
    Public name6 As String = "Thống kê trình độ học vấn"
    Public data As String = ""
    Public data2 As String = ""
    Public data3 As String = ""
    Public data4 As String = ""
    Public data5 As String = ""
    Public data6 As String = ""
    Public title As String = ""
    Public category As String = "['Tháng 1','Tháng 2','Tháng 3','Tháng 4','Tháng 5','Tháng 6','Tháng 7','Tháng 8','Tháng 9','Tháng 10','Tháng 11','Tháng 12']"
    Public category_age As String = "['Độ tuổi']"
    Public category_tnct As String = ""
    Public category_work_place As String = "['Nơi làm việc']"
    Public category_learning_lv As String = "['Trình độ']"
    Public series As String = ""
    Public series_tnct As String = ""
    Public series_work_place As String = ""
    Public series_age As String = ""
    Public series_learing_lv As String = ""
    Public colors As String = "'#F1C40F','#00536b', '#E67E22','#95A5A6','#E74C3C','#ECF0F1','#9B59B6', '#2FCC71', '#3498DB', '#1ABC9C','#8fbc8f','#8b0000','#ff2bf8','#9966CC','#FF3333','#8B4513','#009ACD','#EE30A7','#C71585','#008080'"
    Public type As String = "pie"
    Public smonth As String = "no"
    Public Property StatisticData As List(Of StatisticDTO)
        Get
            Return Session(Me.ID & "_StatisticData")
        End Get

        Set(ByVal value As List(Of StatisticDTO))
            Session(Me.ID & "_StatisticData") = value
        End Set
    End Property
    Public Property isExport As Decimal?
        Get
            Return Session(Me.ID & "_isExport")
        End Get

        Set(ByVal value As Decimal?)
            Session(Me.ID & "_isExport") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)

        Try
            If Not IsPostBack Then
                isExport = 0
                ctrlOrganization.AutoPostBack = False
                ctrlOrganization.LoadDataAfterLoaded = True
                ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
                ctrlOrganization.CheckBoxes = TreeNodeTypes.None
                ctrlOrganization.CheckParentNodes = False
                ctrlOrganization.CheckChildNodes = True

                If cboType.SelectedValue = "" Then
                    Dim dt As New DataTable
                    dt.Columns.Add("ID")
                    dt.Columns.Add("NAME")
                    dt.Rows.Add("1", "Thống kê số lượng nhân sự")
                    dt.Rows.Add("2", "Thống kê lao động theo độ tuổi lao động")
                    dt.Rows.Add("3", "Thống kê bậc lao động")
                    dt.Rows.Add("4", "Thống kê thâm niên công tác")
                    dt.Rows.Add("5", "Thống kê số lượng nhân viên tại nơi làm việc")
                    dt.Rows.Add("6", "Thống kê nhân sự theo giới tính")
                    dt.Rows.Add("7", "Thống kê trình độ học vấn")
                    dt.Rows.Add("8", "Thống kê trình độ ngoại ngữ")
                    dt.Rows.Add("9", "Thống kê tuyển dụng mới theo tháng")
                    dt.Rows.Add("10", "Thống kê số lượng nhân viên nghỉ việc theo tháng")
                    dt.Rows.Add("11", "Thống kê số lượng ký hợp đồng trong tháng")
                    dt.Rows.Add("12", "Thống kê kết quả đạt xuất sắc trong năm")
                    dt.Rows.Add("13", "Thống kê số lượng bổ nhiệm chính thức trong tháng")
                    dt.Rows.Add("14", "Báo cáo tổng hợp lao động")
                    FillRadCombobox(cboType, dt, "NAME", "ID")
                End If

                If cboReportType.SelectedValue = "" Then
                    Dim dt2 As New DataTable
                    dt2.Columns.Add("ID")
                    dt2.Columns.Add("NAME")
                    dt2.Rows.Add("pie", "Biểu đồ tròn")
                    dt2.Rows.Add("column", "Biểu đồ cột xếp chồng")
                    dt2.Rows.Add("column1", "Biểu đồ cột")
                    FillRadCombobox(cboReportType, dt2, "NAME", "ID")
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            'Me.MainToolBar = tbarMainToolBar
            'Common.Common.BuildToolbar(Me.MainToolBar)
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
            Dim rep As New ProfileRepository
            Dim dtData As New DataTable
            dtData = rep.GetOtherList("WORK_PLACE", True)
            FillRadCombobox(cboNoiLamViec, dtData, "NAME", "ID")
            dtData = rep.GetOtherList("GENDER", True)
            FillRadCombobox(cboGender, dtData, "NAME", "ID")
            dtData = rep.GetOtherList("JOB_BAND", True)
            dtData.DefaultView.Sort = "NAME ASC"
            FillRadCombobox(cboJobBand, dtData, "NAME", "ID")

            Dim dtAge As New DataTable
            dtAge.Columns.Add("ID")
            dtAge.Columns.Add("NAME")
            dtAge.Rows.Add("", "")
            dtAge.Rows.Add("1", "Dưới 30 tuổi")
            dtAge.Rows.Add("2", "Từ 30-35 tuổi")
            dtAge.Rows.Add("3", "Từ 36-40 tuổi")
            dtAge.Rows.Add("4", "Từ 41-50 tuổi")
            dtAge.Rows.Add("5", "Từ 51-59 tuổi")
            dtAge.Rows.Add("6", "Trên 59")
            FillRadCombobox(cboDoTuoi, dtAge, "NAME", "ID")

            Dim dtThamNiem As New DataTable
            dtThamNiem.Columns.Add("ID")
            dtThamNiem.Columns.Add("NAME")
            dtThamNiem.Rows.Add("", "")
            dtThamNiem.Rows.Add("1", "Dưới 1 năm")
            dtThamNiem.Rows.Add("2", "Trên 1-2 năm")
            dtThamNiem.Rows.Add("3", "trên 2-3 năm")
            dtThamNiem.Rows.Add("4", "Trên 3-5 năm")
            dtThamNiem.Rows.Add("5", "Trên 5 năm")
            FillRadCombobox(cboThamNien, dtThamNiem, "NAME", "ID")

            dtData = rep.GetOtherList("LEARNING_LEVEL", True)
            FillRadCombobox(cboTDHocVan, dtData, "NAME", "ID")

        Catch ex As Exception

            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    Private Sub cboReportType_SelectIndexChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboReportType.SelectedIndexChanged
        Try
            If cboReportType.SelectedValue <> "" Then
                type = cboReportType.SelectedValue
                If type = "pie" Then
                    colors = "'#F1C40F', '#00536b', '#E67E22','#95A5A6','#E74C3C','#ECF0F1','#9B59B6', '#2FCC71', '#3498DB', '#1ABC9C'"
                Else
                    colors = "'#dba510','#00536b'"
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboType_SelectIndexChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged
        Try
            data = String.Empty
            data2 = String.Empty
            data3 = String.Empty
            data4 = String.Empty
            data5 = String.Empty
            type = "pie"
            colors = "'#F1C40F', '#E67E22','#95A5A6','#E74C3C','#ECF0F1','#9B59B6', '#2FCC71', '#3498DB', '#1ABC9C'"
            lblMonthMin.Visible = True
            cboMonth.Visible = True
            lblReportType.Visible = True
            cboReportType.Visible = True
            Select Case cboType.SelectedValue
                Case "1"
                    type = "pie"
                    smonth = "no"
                    tdLbReportType.Visible = True
                    tdCboReportType.Visible = True
                    lblMonthMin.Visible = True
                    cboMonth.Visible = True
                Case "2"
                    type = "pie"
                    smonth = "no"
                    tdLbReportType.Visible = True
                    tdCboReportType.Visible = True
                    lblMonthMin.Visible = True
                    cboMonth.Visible = True
                Case "3"
                    type = "pie"
                    smonth = "no"
                    tdLbReportType.Visible = True
                    tdCboReportType.Visible = True
                    lblMonthMin.Visible = True
                    cboMonth.Visible = True
                Case "4"
                    type = "pie"
                    smonth = "no"
                    tdLbReportType.Visible = True
                    tdCboReportType.Visible = True
                    lblMonthMin.Visible = True
                    cboMonth.Visible = True
                Case "5"
                    type = "pie"
                    smonth = "no"
                    tdLbReportType.Visible = True
                    tdCboReportType.Visible = True
                    lblMonthMin.Visible = True
                    cboMonth.Visible = True
                Case "6"
                    type = "pie"
                    smonth = "no"
                    tdLbReportType.Visible = True
                    tdCboReportType.Visible = True
                    lblMonthMin.Visible = True
                    cboMonth.Visible = True
                Case "7"
                    type = "pie"
                    smonth = "no"
                    tdLbReportType.Visible = True
                    tdCboReportType.Visible = True
                    lblMonthMin.Visible = True
                    cboMonth.Visible = True
                Case "8"
                    type = "pie"
                    smonth = "no"
                    tdLbReportType.Visible = True
                    tdCboReportType.Visible = True
                    lblMonthMin.Visible = True
                    cboMonth.Visible = True
                Case "9"
                    type = "column1"
                    lblMonthMin.Visible = False
                    cboMonth.Visible = False
                    smonth = "yes"
                Case "9"
                    type = "column1"
                    lblMonthMin.Visible = False
                    cboMonth.Visible = False
                    smonth = "yes"
                Case "10"
                    type = "column1"
                    lblMonthMin.Visible = False
                    cboMonth.Visible = False
                    smonth = "yes"
                Case "11"
                    type = "column"
                    lblMonthMin.Visible = False
                    cboMonth.Visible = False
                    smonth = "yes"
                Case "12"
                    type = "pie"
                    smonth = "no"
                    lblMonthMin.Visible = True
                    cboMonth.Visible = True
                    tdLbReportType.Visible = False
                    tdCboReportType.Visible = False
                Case "13"
                    type = "column"
                    lblMonthMin.Visible = False
                    cboMonth.Visible = False
                    smonth = "yes"
                Case "14"
                    type = "multi"
                    lblMonthMin.Visible = True
                    cboMonth.Visible = True
                    tdLbReportType.Visible = False
                    tdCboReportType.Visible = False
            End Select
            If cboReportType.Visible Then
                cboReportType.SelectedValue = type
            End If

            ClearControlValue(cboGender, cboDoTuoi, cboTDHocVan, cboNoiLamViec, cboJobBand, cboThamNien)
            If cboType.SelectedValue = "1" Then
                tdLbGender.Visible = True
                tdCboGender.Visible = True
                tdLbAge.Visible = True
                tdCboAge.Visible = True
                tdLbLearningLevel.Visible = True
                tdCboLearningLevel.Visible = True
                tdLbWorkPlace.Visible = True
                tdCboWorkPlace.Visible = True
                tdLbJobBand.Visible = True
                tdCboJobBand.Visible = True
                tdLbExp.Visible = True
                tdCboExp.Visible = True
                ctrlOrganization.CheckBoxes = TreeNodeTypes.None
            ElseIf cboType.SelectedValue = "3" Then
                tdLbGender.Visible = True
                tdCboGender.Visible = True
                tdLbAge.Visible = False
                tdCboAge.Visible = False
                tdLbLearningLevel.Visible = False
                tdCboLearningLevel.Visible = False
                tdLbWorkPlace.Visible = False
                tdCboWorkPlace.Visible = False
                tdLbJobBand.Visible = False
                tdCboJobBand.Visible = False
                tdLbExp.Visible = False
                tdCboExp.Visible = False
                ctrlOrganization.CheckBoxes = TreeNodeTypes.All
            ElseIf cboType.SelectedValue = "4" Then
                tdLbGender.Visible = False
                tdCboGender.Visible = False
                tdLbAge.Visible = False
                tdCboAge.Visible = False
                tdLbLearningLevel.Visible = False
                tdCboLearningLevel.Visible = False
                tdLbWorkPlace.Visible = False
                tdCboWorkPlace.Visible = False
                tdLbJobBand.Visible = True
                tdCboJobBand.Visible = True
                tdLbExp.Visible = False
                tdCboExp.Visible = False
                ctrlOrganization.CheckBoxes = TreeNodeTypes.All
            ElseIf cboType.SelectedValue = "2" OrElse cboType.SelectedValue = "5" OrElse cboType.SelectedValue = "6" OrElse cboType.SelectedValue = "7" _
                     OrElse cboType.SelectedValue = "8" OrElse cboType.SelectedValue = "9" OrElse cboType.SelectedValue = "10" Then
                tdLbGender.Visible = False
                tdCboGender.Visible = False
                tdLbAge.Visible = False
                tdCboAge.Visible = False
                tdLbLearningLevel.Visible = False
                tdCboLearningLevel.Visible = False
                tdLbWorkPlace.Visible = False
                tdCboWorkPlace.Visible = False
                tdLbJobBand.Visible = False
                tdCboJobBand.Visible = False
                tdLbExp.Visible = False
                tdCboExp.Visible = False
                ctrlOrganization.CheckBoxes = TreeNodeTypes.All
            ElseIf cboType.SelectedValue = "11" OrElse cboType.SelectedValue = "12" OrElse cboType.SelectedValue = "13" Then
                tdLbGender.Visible = False
                tdCboGender.Visible = False
                tdLbAge.Visible = False
                tdCboAge.Visible = False
                tdLbLearningLevel.Visible = False
                tdCboLearningLevel.Visible = False
                tdLbWorkPlace.Visible = True
                tdCboWorkPlace.Visible = True
                tdLbJobBand.Visible = True
                tdCboJobBand.Visible = True
                tdLbExp.Visible = False
                tdCboExp.Visible = False
                ctrlOrganization.CheckBoxes = TreeNodeTypes.All
            ElseIf cboType.SelectedValue = "14" Then
                tdLbGender.Visible = False
                tdCboGender.Visible = False
                tdLbAge.Visible = False
                tdCboAge.Visible = False
                tdLbLearningLevel.Visible = False
                tdCboLearningLevel.Visible = False
                tdLbWorkPlace.Visible = False
                tdCboWorkPlace.Visible = False
                tdLbJobBand.Visible = False
                tdCboJobBand.Visible = False
                tdLbExp.Visible = False
                tdCboExp.Visible = False
                ctrlOrganization.CheckBoxes = TreeNodeTypes.None
            End If
        Catch
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            data = String.Empty
            data2 = String.Empty
            data3 = String.Empty
            data4 = String.Empty
            data5 = String.Empty
            title = cboType.SelectedItem.Text
            If cboReportType.Visible Then
                type = cboReportType.SelectedValue
            End If
            Select Case cboType.SelectedValue
                Case "1"
                    Chart_Employee_Num()
                Case "2"
                    Chart_Age()
                Case "3"
                    Chart_BAC_LAO_DONG()
                Case "4"
                    Chart_TNCT()
                Case "5"
                    Chart_WorkPlace()
                Case "6"
                    Chart_GENDER()
                Case "7"
                    Chart_TRINHDO_HOCVAN()
                Case "8"
                    Chart_TRINHDO_NGOAINGU()
                Case "9"
                    Chart_NEW_EMPLOYEE()
                Case "10"
                    Chart_TER_EMPLOYEE()
                Case "11"
                    Chart_HDLD()
                Case "12"
                    Chart12()
                Case "13"
                    Chart_BO_NHIEM()
                Case "14"
                    type = "multi"
                    Chart_Total()
            End Select

        Catch ex As Exception
            isExport = 0
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New ProfileDashboardRepository
        Try
            ''Bo log
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Chart_HDLD()
        Dim rep As New ProfileRepository
        Dim _filter As New ParamDTO
        _filter.ORG_ID = ctrlOrganization.CurrentValue
        _filter.MONTH = cboMonth.SelectedValue
        _filter.YEAR = cboYear.SelectedValue
        Dim _filterStr As New JObject
        If cboNoiLamViec.SelectedValue <> "" Then
            _filterStr.Add("work_place", cboNoiLamViec.SelectedValue)
        End If
        If cboJobBand.SelectedValue <> "" Then
            _filterStr.Add("job_band", cboJobBand.SelectedValue)
        End If
        Dim lstOrg = ctrlOrganization.CheckedValueKeys
        Dim dt As DataTable = rep.Chart_HDLD(_filter, lstOrg, _filterStr.ToString(), isExport)
        If isExport = 1 Then
            ExportTemplate("Profile\ExportData.xlsx",
                                      dt, Nothing,
                            "Thongkesoluongkyhopdong_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
        Else

            Dim lstData As New List(Of String)
            If type = "pie" Then
                For i = 0 To dt.Rows.Count - 1
                    If i = dt.Rows.Count - 1 Then
                        data &= "{name: '" & dt(i).Item("NAME") & " (" & dt(i).Item("TOTAL") & ")" & "', y: " & dt(i).Item("TOTAL") & ",sliced: true,selected: true}"
                    Else
                        data &= "{name: '" & dt(i).Item("NAME") & " (" & dt(i).Item("TOTAL") & ")" & "', y: " & dt(i).Item("TOTAL") & "}," & vbNewLine
                    End If
                Next
                Dim itemData As String = ""
                itemData = "{name: 'name', colorByPoint: true, data:[" + data + "]}"
                lstData.Add(itemData)
                For i = 0 To lstData.Count - 1
                    If i = lstData.Count - 1 Then
                        series &= lstData(i)
                    Else
                        series &= lstData(i) & ","
                    End If
                Next
            ElseIf type = "column1" Then
                category = "['Hợp đồng']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            Else
                category = "['Hợp đồng']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            End If
        End If
    End Sub

    Private Sub Chart_Age()
        Dim rep As New ProfileRepository
        Dim _filter As New ParamDTO
        _filter.ORG_ID = ctrlOrganization.CurrentValue
        _filter.MONTH = cboMonth.SelectedValue
        _filter.YEAR = cboYear.SelectedValue
        Dim lstOrg = ctrlOrganization.CheckedValueKeys
        Dim dt As DataTable = rep.Chart_Age(_filter, lstOrg, "", isExport)
        If isExport = 1 Then
            ExportTemplate("Profile\ExportData.xlsx",
                                      dt, Nothing,
                            "Thongkelaodongtheodotuoilaodong_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
        Else

            Dim dtEmp As New DataTable
            dtEmp.Columns.Add("TOTAL")
            dtEmp.Columns.Add("NAME")

            Dim lstData As New List(Of String)
            If type = "pie" Then
                If dt.Rows(0).Item("AGE29").ToString() > "0" Then
                    dtEmp.Rows.Add(dt.Rows(0).Item("AGE29").ToString(), "<30 (" & dt.Rows(0).Item("AGE29").ToString() & ")")
                End If
                If dt.Rows(0).Item("AGE30").ToString() > "0" Then
                    dtEmp.Rows.Add(dt.Rows(0).Item("AGE30").ToString(), "30-35 (" & dt.Rows(0).Item("AGE30").ToString() & ")")
                End If
                If dt.Rows(0).Item("AGE40").ToString() > "0" Then
                    dtEmp.Rows.Add(dt.Rows(0).Item("AGE40").ToString(), "36 - 40 (" & dt.Rows(0).Item("AGE40").ToString() & ")")

                End If
                If dt.Rows(0).Item("AGE50").ToString() > "0" Then
                    dtEmp.Rows.Add(dt.Rows(0).Item("AGE50").ToString(), "41 - 50 (" & dt.Rows(0).Item("AGE50").ToString() & ")")

                End If
                If dt.Rows(0).Item("AGE58").ToString() > "0" Then
                    dtEmp.Rows.Add(dt.Rows(0).Item("AGE58").ToString(), "51 - 58 (" & dt.Rows(0).Item("AGE58").ToString() & ")")

                End If
                If dt.Rows(0).Item("AGE59").ToString() > "0" Then
                    dtEmp.Rows.Add(dt.Rows(0).Item("AGE59").ToString(), ">=59 (" & dt.Rows(0).Item("AGE59").ToString() & ")")

                End If

                For i = 0 To dtEmp.Rows.Count - 1
                    If dtEmp.Rows(i).Item("TOTAL") > "0" Then
                        If i = dtEmp.Rows.Count - 1 Then
                            data &= "{name: '" & dtEmp(i).Item("NAME") & "', y: " & dtEmp(i).Item("TOTAL") & ",sliced: true,selected: true}"
                            'ElseIf i = 0 Then
                            '    data &= "{name: 'Nhỏ hơn hoặc bằng 30 (" & dtEmp(i).Item("TOTAL") & ")', y: " & dtEmp(i).Item("TOTAL") & "}," & vbNewLine
                        Else
                            data &= "{name: '" & dtEmp(i).Item("NAME") & "', y: " & dtEmp(i).Item("TOTAL") & "}," & vbNewLine
                        End If
                    End If
                Next
                Dim itemData As String = ""
                itemData = "{name: 'name', colorByPoint: true, data:[" + data + "]}"
                lstData.Add(itemData)
                For i = 0 To lstData.Count - 1
                    If i = lstData.Count - 1 Then
                        series &= lstData(i)
                    Else
                        series &= lstData(i) & ","
                    End If
                Next
            ElseIf type = "column1" Then
                dtEmp.Rows.Add(dt.Rows(0).Item("AGE29").ToString(), "<30")
                dtEmp.Rows.Add(dt.Rows(0).Item("AGE30").ToString(), "30-35")
                dtEmp.Rows.Add(dt.Rows(0).Item("AGE40").ToString(), "36 - 40")
                dtEmp.Rows.Add(dt.Rows(0).Item("AGE50").ToString(), "41 - 50")
                dtEmp.Rows.Add(dt.Rows(0).Item("AGE58").ToString(), "51 - 58")
                dtEmp.Rows.Add(dt.Rows(0).Item("AGE59").ToString(), ">=59")
                category = "['Độ tuổi']"
                For i = 0 To dtEmp.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dtEmp(i).Item("NAME") & "', data: [" & dtEmp(i).Item("TOTAL") & "]},"
                    ElseIf i = dtEmp.Rows.Count - 1 Then
                        itemData &= "{name: '" & dtEmp(i).Item("NAME") & "', data: [" & dtEmp(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dtEmp(i).Item("NAME") & "', data: [" & dtEmp(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            Else
                dtEmp.Rows.Add(dt.Rows(0).Item("AGE29").ToString(), "<30")
                dtEmp.Rows.Add(dt.Rows(0).Item("AGE30").ToString(), "30-35")
                dtEmp.Rows.Add(dt.Rows(0).Item("AGE40").ToString(), "36 - 40")
                dtEmp.Rows.Add(dt.Rows(0).Item("AGE50").ToString(), "41 - 50")
                dtEmp.Rows.Add(dt.Rows(0).Item("AGE58").ToString(), "51 - 58")
                dtEmp.Rows.Add(dt.Rows(0).Item("AGE59").ToString(), ">=59")
                category = "['Độ tuổi']"
                For i = 0 To dtEmp.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dtEmp(i).Item("NAME") & "', data: [" & dtEmp(i).Item("TOTAL") & "]},"
                    ElseIf i = dtEmp.Rows.Count - 1 Then
                        itemData &= "{name: '" & dtEmp(i).Item("NAME") & "', data: [" & dtEmp(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dtEmp(i).Item("NAME") & "', data: [" & dtEmp(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            End If
        End If
    End Sub

    Private Sub Chart_TNCT()
        Dim rep As New ProfileRepository
        Dim _filter As New ParamDTO
        _filter.ORG_ID = ctrlOrganization.CurrentValue
        _filter.MONTH = cboMonth.SelectedValue
        _filter.YEAR = cboYear.SelectedValue
        Dim _filterStr As New JObject
        If cboJobBand.SelectedValue <> "" Then
            _filterStr.Add("job_band", cboJobBand.SelectedValue)
        End If
        Dim lstOrg = ctrlOrganization.CheckedValueKeys
        Dim dt As DataTable = rep.Chart_TNCT(_filter, lstOrg, _filterStr.ToString(), isExport)
        If isExport = 1 Then
            ExportTemplate("Profile\ExportData.xlsx",
                                      dt, Nothing,
                            "Thongkethamniencongtac_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
        Else

            Dim dtEmp As New DataTable
            dtEmp.Columns.Add("TOTAL")
            dtEmp.Columns.Add("NAME")
            Dim lstData As New List(Of String)
            If type = "pie" Then
                If dt.Rows(0).Item("NUMYEAR1").ToString() > "0" Then
                    dtEmp.Rows.Add(dt.Rows(0).Item("NUMYEAR1").ToString(), "< 1 năm (" & dt.Rows(0).Item("NUMYEAR1").ToString() & ")")
                End If
                If dt.Rows(0).Item("NUMYEAR2").ToString() > "0" Then
                    dtEmp.Rows.Add(dt.Rows(0).Item("NUMYEAR2").ToString(), "1-2 năm (" & dt.Rows(0).Item("NUMYEAR2").ToString() & ")")
                End If
                If dt.Rows(0).Item("NUMYEAR3").ToString() > "0" Then
                    dtEmp.Rows.Add(dt.Rows(0).Item("NUMYEAR3").ToString(), "2-3 năm (" & dt.Rows(0).Item("NUMYEAR3").ToString() & ")")
                End If
                If dt.Rows(0).Item("NUMYEAR4").ToString() > "0" Then
                    dtEmp.Rows.Add(dt.Rows(0).Item("NUMYEAR4").ToString(), "3-5 năm (" & dt.Rows(0).Item("NUMYEAR4").ToString() & ")")

                End If
                If dt.Rows(0).Item("NUMYEAR5").ToString() > "0" Then
                    dtEmp.Rows.Add(dt.Rows(0).Item("NUMYEAR5").ToString(), ">5 năm (" & dt.Rows(0).Item("NUMYEAR5").ToString() & ")")

                End If


                For i = 0 To dtEmp.Rows.Count - 1
                    If dtEmp.Rows(i).Item("TOTAL") > "0" Then
                        If i = dtEmp.Rows.Count - 1 Then
                            data &= "{name: '" & dtEmp(i).Item("NAME") & "', y: " & dtEmp(i).Item("TOTAL") & ",sliced: true,selected: true}"
                            'ElseIf i = 0 Then
                            '    data &= "{name: 'Nhỏ hơn hoặc bằng 30 (" & dtEmp(i).Item("TOTAL") & ")', y: " & dtEmp(i).Item("TOTAL") & "}," & vbNewLine
                        Else
                            data &= "{name: '" & dtEmp(i).Item("NAME") & "', y: " & dtEmp(i).Item("TOTAL") & "}," & vbNewLine
                        End If
                    End If
                Next
                Dim itemData As String = ""
                itemData = "{name: 'name', colorByPoint: true, data:[" + data + "]}"
                lstData.Add(itemData)
                For i = 0 To lstData.Count - 1
                    If i = lstData.Count - 1 Then
                        series &= lstData(i)
                    Else
                        series &= lstData(i) & ","
                    End If
                Next
            ElseIf type = "column1" Then
                dtEmp.Rows.Add(dt.Rows(0).Item("NUMYEAR1").ToString(), "<1 năm")
                dtEmp.Rows.Add(dt.Rows(0).Item("NUMYEAR2").ToString(), "1-2 năm")
                dtEmp.Rows.Add(dt.Rows(0).Item("NUMYEAR3").ToString(), "2-3 năm")
                dtEmp.Rows.Add(dt.Rows(0).Item("NUMYEAR4").ToString(), "3-5 năm")
                dtEmp.Rows.Add(dt.Rows(0).Item("NUMYEAR5").ToString(), "> 5 năm")

                category = "['Thâm niên công tác']"
                For i = 0 To dtEmp.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dtEmp(i).Item("NAME") & "', data: [" & dtEmp(i).Item("TOTAL") & "]},"
                    ElseIf i = dtEmp.Rows.Count - 1 Then
                        itemData &= "{name: '" & dtEmp(i).Item("NAME") & "', data: [" & dtEmp(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dtEmp(i).Item("NAME") & "', data: [" & dtEmp(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            Else
                dtEmp.Rows.Add(dt.Rows(0).Item("NUMYEAR1").ToString(), "<1 năm")
                dtEmp.Rows.Add(dt.Rows(0).Item("NUMYEAR2").ToString(), "1-2 năm")
                dtEmp.Rows.Add(dt.Rows(0).Item("NUMYEAR3").ToString(), "2-3 năm")
                dtEmp.Rows.Add(dt.Rows(0).Item("NUMYEAR4").ToString(), "3-5 năm")
                dtEmp.Rows.Add(dt.Rows(0).Item("NUMYEAR5").ToString(), "> 5 năm")
                category = "['Thâm niên công tác']"
                For i = 0 To dtEmp.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dtEmp(i).Item("NAME") & "', data: [" & dtEmp(i).Item("TOTAL") & "]},"
                    ElseIf i = dtEmp.Rows.Count - 1 Then
                        itemData &= "{name: '" & dtEmp(i).Item("NAME") & "', data: [" & dtEmp(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dtEmp(i).Item("NAME") & "', data: [" & dtEmp(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            End If
        End If
    End Sub

    Private Sub Chart_WorkPlace()
        Dim rep As New ProfileRepository
        Dim _filter As New ParamDTO
        _filter.ORG_ID = ctrlOrganization.CurrentValue
        _filter.MONTH = cboMonth.SelectedValue
        _filter.YEAR = cboYear.SelectedValue
        Dim lstOrg = ctrlOrganization.CheckedValueKeys
        Dim dt As DataTable = rep.Chart_WorkPlace(_filter, lstOrg, "", isExport)
        If isExport = 1 Then
            ExportTemplate("Profile\ExportData.xlsx",
                                      dt, Nothing,
                            "Thongkesoluongnhanvientainoilamviec_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
        Else
            Dim lstData As New List(Of String)
            If type = "pie" Then
                For i = 0 To dt.Rows.Count - 1
                    If i = dt.Rows.Count - 1 Then
                        data &= "{name: '" & dt(i).Item("NAME") & " (" & dt(i).Item("TOTAL") & ")" & "', y: " & dt(i).Item("TOTAL") & ",sliced: true,selected: true}"
                    Else
                        data &= "{name: '" & dt(i).Item("NAME") & " (" & dt(i).Item("TOTAL") & ")" & "', y: " & dt(i).Item("TOTAL") & "}," & vbNewLine
                    End If
                Next
                Dim itemData As String = ""
                itemData = "{name: 'name', colorByPoint: true, data:[" + data + "]}"
                lstData.Add(itemData)
                For i = 0 To lstData.Count - 1
                    If i = lstData.Count - 1 Then
                        series &= lstData(i)
                    Else
                        series &= lstData(i) & ","
                    End If
                Next
            ElseIf type = "column1" Then
                category = "['Nơi làm việc']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            Else
                category = "['Nơi làm việc']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            End If

        End If
    End Sub

    Private Sub Chart_GENDER()
        Dim rep As New ProfileRepository
        Dim _filter As New ParamDTO
        _filter.ORG_ID = ctrlOrganization.CurrentValue
        _filter.MONTH = cboMonth.SelectedValue
        _filter.YEAR = cboYear.SelectedValue
        Dim _filterStr As New JObject
        If cboJobBand.SelectedValue <> "" Then
            _filterStr.Add("job_band", cboJobBand.SelectedValue)
        End If
        Dim lstOrg = ctrlOrganization.CheckedValueKeys
        Dim dt As DataTable = rep.Chart_GENDER(_filter, lstOrg, _filterStr.ToString(), isExport)
        If isExport = 1 Then
            ExportTemplate("Profile\ExportData.xlsx",
                                      dt, Nothing,
                            "Thongkenhansutheogioitinh_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
        Else

            Dim lstData As New List(Of String)
            If type = "pie" Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item("TOTAL") > "0" Then
                        If i = dt.Rows.Count - 1 Then
                            data &= "{name: '" & dt(i).Item("NAME") & "', y: " & dt(i).Item("TOTAL") & ",sliced: true,selected: true}"
                        Else
                            data &= "{name: '" & dt(i).Item("NAME") & "', y: " & dt(i).Item("TOTAL") & "}," & vbNewLine
                        End If
                    End If
                Next
                Dim itemData As String = ""
                itemData = "{name: 'name', colorByPoint: true, data:[" + data + "]}"
                lstData.Add(itemData)
                For i = 0 To lstData.Count - 1
                    If i = lstData.Count - 1 Then
                        series &= lstData(i)
                    Else
                        series &= lstData(i) & ","
                    End If
                Next
            ElseIf type = "column1" Then
                category = "['Giới tính']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            Else
                category = "['Giới tính']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            End If
        End If
    End Sub

    Private Sub Chart_TRINHDO_HOCVAN()
        Dim rep As New ProfileRepository
        Dim _filter As New ParamDTO
        _filter.ORG_ID = ctrlOrganization.CurrentValue
        _filter.MONTH = cboMonth.SelectedValue
        _filter.YEAR = cboYear.SelectedValue
        Dim lstOrg = ctrlOrganization.CheckedValueKeys
        Dim dt As DataTable = rep.Chart_TRINHDO_HOCVAN(_filter, lstOrg, "", isExport)
        If isExport = 1 Then
            ExportTemplate("Profile\ExportData.xlsx",
                                      dt, Nothing,
                            "Thongketrinhdohocvan_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
        Else

            Dim lstData As New List(Of String)
            If type = "pie" Then
                For i = 0 To dt.Rows.Count - 1
                    If i = dt.Rows.Count - 1 Then
                        data &= "{name: '" & dt(i).Item("NAME") & " (" & dt(i).Item("TOTAL") & ")" & "', y: " & dt(i).Item("TOTAL") & ",sliced: true,selected: true}"
                    Else
                        data &= "{name: '" & dt(i).Item("NAME") & " (" & dt(i).Item("TOTAL") & ")" & "', y: " & dt(i).Item("TOTAL") & "}," & vbNewLine
                    End If
                Next
                Dim itemData As String = ""
                itemData = "{name: 'name', colorByPoint: true, data:[" + data + "]}"
                lstData.Add(itemData)
                For i = 0 To lstData.Count - 1
                    If i = lstData.Count - 1 Then
                        series &= lstData(i)
                    Else
                        series &= lstData(i) & ","
                    End If
                Next
            ElseIf type = "column1" Then
                category = "['Trình độ']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            Else
                category = "['Trình độ']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            End If
        End If
    End Sub

    Private Sub Chart_TRINHDO_NGOAINGU()
        Dim rep As New ProfileRepository
        Dim _filter As New ParamDTO
        _filter.ORG_ID = ctrlOrganization.CurrentValue
        _filter.MONTH = cboMonth.SelectedValue
        _filter.YEAR = cboYear.SelectedValue
        Dim lstOrg = ctrlOrganization.CheckedValueKeys
        Dim dt As DataTable = rep.Chart_TRINHDO_NGOAINGU(_filter, lstOrg, "", isExport)
        If isExport = 1 Then
            ExportTemplate("Profile\ExportData.xlsx",
                                      dt, Nothing,
                            "Thongketrinhdongoaingu_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
        Else
            Dim lstData As New List(Of String)
            If type = "pie" Then
                For i = 0 To dt.Rows.Count - 1
                    If i = dt.Rows.Count - 1 Then
                        data &= "{name: '" & dt(i).Item("NAME") & " (" & dt(i).Item("TOTAL") & ")" & "', y: " & dt(i).Item("TOTAL") & ",sliced: true,selected: true}"
                    Else
                        data &= "{name: '" & dt(i).Item("NAME") & " (" & dt(i).Item("TOTAL") & ")" & "', y: " & dt(i).Item("TOTAL") & "}," & vbNewLine
                    End If
                Next
                Dim itemData As String = ""
                itemData = "{name: 'name', colorByPoint: true, data:[" + data + "]}"
                lstData.Add(itemData)
                For i = 0 To lstData.Count - 1
                    If i = lstData.Count - 1 Then
                        series &= lstData(i)
                    Else
                        series &= lstData(i) & ","
                    End If
                Next
            ElseIf type = "column1" Then
                category = "['Ngôn ngữ']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            Else
                category = "['Ngôn ngữ']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            End If
        End If
    End Sub

    Private Sub Chart_NEW_EMPLOYEE()
        Dim rep As New ProfileRepository
        Dim _filter As New ParamDTO
        _filter.ORG_ID = ctrlOrganization.CurrentValue
        _filter.MONTH = cboMonth.SelectedValue
        _filter.YEAR = cboYear.SelectedValue
        Dim _filterStr As New JObject
        Dim lstOrg = ctrlOrganization.CheckedValueKeys
        Dim dt As DataTable = rep.Chart_NEW_EMPLOYEE(_filter, lstOrg, _filterStr.ToString(), isExport)
        If isExport = 1 Then
            ExportTemplate("Profile\ExportData.xlsx",
                                      dt, Nothing,
                            "Thongketuyendungmoi_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
        Else
            Dim lstData As New List(Of String)
            If type = "pie" Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item("TOTAL") > "0" Then
                        If i = dt.Rows.Count - 1 Then
                            data &= "{name: '" & dt(i).Item("NAME") & "', y: " & dt(i).Item("TOTAL") & ",sliced: true,selected: true}"
                        Else
                            data &= "{name: '" & dt(i).Item("NAME") & "', y: " & dt(i).Item("TOTAL") & "}," & vbNewLine
                        End If
                    End If
                Next
                Dim itemData As String = ""
                itemData = "{name: 'name', colorByPoint: true, data:[" + data + "]}"
                lstData.Add(itemData)
                For i = 0 To lstData.Count - 1
                    If i = lstData.Count - 1 Then
                        series &= lstData(i)
                    Else
                        series &= lstData(i) & ","
                    End If
                Next
            ElseIf type = "column1" Then
                category = "['Tháng']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            Else
                category = "['Tháng']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            End If
        End If
    End Sub

    Private Sub Chart_TER_EMPLOYEE()
        Dim rep As New ProfileRepository
        Dim _filter As New ParamDTO
        _filter.ORG_ID = ctrlOrganization.CurrentValue
        _filter.MONTH = cboMonth.SelectedValue
        _filter.YEAR = cboYear.SelectedValue
        Dim _filterStr As New JObject
        Dim lstOrg = ctrlOrganization.CheckedValueKeys
        Dim dt As DataTable = rep.Chart_TER_EMPLOYEE(_filter, lstOrg, _filterStr.ToString())
        If isExport = 1 Then
            ExportTemplate("Profile\ExportData.xlsx",
                                      dt, Nothing,
                            "Thongketnhanviennghiviec_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
        Else
            Dim lstData As New List(Of String)
            If type = "pie" Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item("TOTAL") > "0" Then
                        If i = dt.Rows.Count - 1 Then
                            data &= "{name: '" & dt(i).Item("NAME") & "', y: " & dt(i).Item("TOTAL") & ",sliced: true,selected: true}"
                        Else
                            data &= "{name: '" & dt(i).Item("NAME") & "', y: " & dt(i).Item("TOTAL") & "}," & vbNewLine
                        End If
                    End If
                Next
                Dim itemData As String = ""
                itemData = "{name: 'name', colorByPoint: true, data:[" + data + "]}"
                lstData.Add(itemData)
                For i = 0 To lstData.Count - 1
                    If i = lstData.Count - 1 Then
                        series &= lstData(i)
                    Else
                        series &= lstData(i) & ","
                    End If
                Next
            ElseIf type = "column1" Then
                category = "['Tháng']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            Else
                category = "['Tháng']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            End If
        End If
    End Sub

    Private Sub Chart12()
        Dim rep As New ProfileRepository
        Dim _filter As New ParamDTO
        _filter.ORG_ID = ctrlOrganization.CurrentValue
        _filter.MONTH = cboMonth.SelectedValue
        _filter.YEAR = cboYear.SelectedValue
        Dim _filterStr As New JObject
        If cboJobBand.SelectedValue <> "" Then
            _filterStr.Add("job_band", cboJobBand.SelectedValue)
        End If
        Dim lstOrg = ctrlOrganization.CheckedValueKeys
        Dim dt As DataTable = New DataTable
        If isExport = 1 Then
            ExportTemplate("Profile\ExportData.xlsx",
                                      dt, Nothing,
                            "Thongkeketquadatxuatsac_nam" & _filter.YEAR.ToString)
        Else

            Dim lstData As New List(Of String)
            If type = "pie" Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item("TOTAL") > "0" Then
                        If i = dt.Rows.Count - 1 Then
                            data &= "{name: '" & dt(i).Item("NAME") & "', y: " & dt(i).Item("TOTAL") & ",sliced: true,selected: true}"
                        Else
                            data &= "{name: '" & dt(i).Item("NAME") & "', y: " & dt(i).Item("TOTAL") & "}," & vbNewLine
                        End If
                    End If
                Next
                Dim itemData As String = ""
                itemData = "{name: 'name', colorByPoint: true, data:[" + data + "]}"
                lstData.Add(itemData)
                For i = 0 To lstData.Count - 1
                    If i = lstData.Count - 1 Then
                        series &= lstData(i)
                    Else
                        series &= lstData(i) & ","
                    End If
                Next
            ElseIf type = "column1" Then
                category = "['Giới tính']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            Else
                category = "['Giới tính']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            End If
        End If
    End Sub

    Private Sub Chart_Employee_Num()
        Dim rep As New ProfileRepository
        Dim _filter As New ParamDTO
        _filter.ORG_ID = ctrlOrganization.CurrentValue
        _filter.MONTH = cboMonth.SelectedValue
        _filter.YEAR = cboYear.SelectedValue

        Dim _filterStr As New JObject
        If cboNoiLamViec.SelectedValue <> "" Then
            _filterStr.Add("work_place", cboNoiLamViec.SelectedValue)
        End If
        If cboGender.SelectedValue <> "" Then
            _filterStr.Add("gender", cboGender.SelectedValue)
        End If
        If cboJobBand.SelectedValue <> "" Then
            _filterStr.Add("job_band", cboJobBand.SelectedValue)
        End If
        If cboDoTuoi.SelectedValue <> "" Then
            _filterStr.Add("age", cboDoTuoi.SelectedValue)
        End If
        If cboThamNien.SelectedValue <> "" Then
            _filterStr.Add("exp", cboThamNien.SelectedValue)
        End If
        If cboTDHocVan.SelectedValue <> "" Then
            _filterStr.Add("learning_level", cboTDHocVan.SelectedValue)
        End If

        Dim lstOrg = ctrlOrganization.CheckedValueKeys
        Dim dt As DataTable = rep.Chart_Employee_Num(_filter, _filterStr.ToString(), isExport)
        If isExport = 1 Then
            ExportTemplate("Profile\ExportData.xlsx",
                                      dt, Nothing,
                            "Thongkesoluongnhansu_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
        Else
            Dim lstData As New List(Of String)
            If type = "pie" Then
                For i = 0 To dt.Rows.Count - 1
                    If i = dt.Rows.Count - 1 Then
                        data &= "{name: '" & dt(i).Item("ORG_NAME") & " (" & dt(i).Item("TOTAL") & ")" & "', y: " & dt(i).Item("TOTAL") & ",sliced: true,selected: true}"
                    Else
                        data &= "{name: '" & dt(i).Item("ORG_NAME") & " (" & dt(i).Item("TOTAL") & ")" & "', y: " & dt(i).Item("TOTAL") & "}," & vbNewLine
                    End If
                Next
                Dim itemData As String = ""
                itemData = "{name: 'org_name', colorByPoint: true, data:[" + data + "]}"
                lstData.Add(itemData)
                For i = 0 To lstData.Count - 1
                    If i = lstData.Count - 1 Then
                        series &= lstData(i)
                    Else
                        series &= lstData(i) & ","
                    End If
                Next
            ElseIf type = "column1" Then
                category = "['Phòng ban']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("ORG_NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("ORG_NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("ORG_NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            Else
                category = "['Phòng ban']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("ORG_NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("ORG_NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("ORG_NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            End If
        End If
    End Sub

    Private Sub Chart_BO_NHIEM()
        Dim rep As New ProfileRepository
        Dim _filter As New ParamDTO
        _filter.ORG_ID = ctrlOrganization.CurrentValue
        _filter.MONTH = cboMonth.SelectedValue
        _filter.YEAR = cboYear.SelectedValue
        Dim _filterStr As New JObject
        If cboNoiLamViec.SelectedValue <> "" Then
            _filterStr.Add("work_place", cboNoiLamViec.SelectedValue)
        End If
        If cboJobBand.SelectedValue <> "" Then
            _filterStr.Add("job_band", cboJobBand.SelectedValue)
        End If
        Dim lstOrg = ctrlOrganization.CheckedValueKeys
        Dim dt As DataTable = rep.Chart_BO_NHIEM(_filter, lstOrg, _filterStr.ToString(), isExport)
        If isExport = 1 Then
            ExportTemplate("Profile\ExportData.xlsx",
                                      dt, Nothing,
                            "Thongksoluongbonhiemchinhthuc_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
        Else
            Dim lstData As New List(Of String)
            If type = "pie" Then
                For i = 0 To dt.Rows.Count - 1
                    If i = dt.Rows.Count - 1 Then
                        data &= "{name: '" & dt(i).Item("NAME") & " (" & dt(i).Item("TOTAL") & ")" & "', y: " & dt(i).Item("TOTAL") & ",sliced: true,selected: true}"
                    Else
                        data &= "{name: '" & dt(i).Item("NAME") & " (" & dt(i).Item("TOTAL") & ")" & "', y: " & dt(i).Item("TOTAL") & "}," & vbNewLine
                    End If
                Next
                Dim itemData As String = ""
                itemData = "{name: 'name', colorByPoint: true, data:[" + data + "]}"
                lstData.Add(itemData)
                For i = 0 To lstData.Count - 1
                    If i = lstData.Count - 1 Then
                        series &= lstData(i)
                    Else
                        series &= lstData(i) & ","
                    End If
                Next
            ElseIf type = "column1" Then
                category = "['Tháng']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            Else
                category = "['Tháng']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            End If
        End If
    End Sub

    Private Sub Chart_BAC_LAO_DONG()
        Dim rep As New ProfileRepository
        Dim _filter As New ParamDTO
        _filter.ORG_ID = ctrlOrganization.CurrentValue
        _filter.MONTH = cboMonth.SelectedValue
        _filter.YEAR = cboYear.SelectedValue
        Dim _filterStr As New JObject
        If cboGender.SelectedValue <> "" Then
            _filterStr.Add("gender", cboGender.SelectedValue)
        End If
        Dim lstOrg = ctrlOrganization.CheckedValueKeys
        Dim dt As DataTable = rep.Chart_BAC_LAO_DONG(_filter, lstOrg, _filterStr.ToString(), isExport)
        If isExport = 1 Then
            ExportTemplate("Profile\ExportData.xlsx",
                                      dt, Nothing,
                            "Thongkebaclaodong_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
        Else
            Dim lstData As New List(Of String)
            If type = "pie" Then
                For i = 0 To dt.Rows.Count - 1
                    If i = dt.Rows.Count - 1 Then
                        data &= "{name: '" & dt(i).Item("NAME") & " (" & dt(i).Item("TOTAL") & ")" & "', y: " & dt(i).Item("TOTAL") & ",sliced: true,selected: true}"
                    Else
                        data &= "{name: '" & dt(i).Item("NAME") & " (" & dt(i).Item("TOTAL") & ")" & "', y: " & dt(i).Item("TOTAL") & "}," & vbNewLine
                    End If
                Next
                Dim itemData As String = ""
                itemData = "{name: 'name', colorByPoint: true, data:[" + data + "]}"
                lstData.Add(itemData)
                For i = 0 To lstData.Count - 1
                    If i = lstData.Count - 1 Then
                        series &= lstData(i)
                    Else
                        series &= lstData(i) & ","
                    End If
                Next
            ElseIf type = "column1" Then
                category = "['Bậc lao động']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            Else
                category = "['Bậc lao động']"
                For i = 0 To dt.Rows.Count - 1
                    Dim itemData As String = ""
                    If i = 0 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    ElseIf i = dt.Rows.Count - 1 Then
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]}"
                    Else
                        itemData &= "{name: '" & dt(i).Item("NAME") & "', data: [" & dt(i).Item("TOTAL") & "]},"
                    End If
                    lstData.Add(itemData)
                Next
                For i = 0 To lstData.Count - 1
                    series &= lstData(i)
                Next
            End If
        End If
    End Sub

    Private Sub Chart_Total()
        Dim rep As New ProfileRepository
        Dim _filter As New ParamDTO
        _filter.ORG_ID = ctrlOrganization.CurrentValue
        _filter.MONTH = cboMonth.SelectedValue
        _filter.YEAR = cboYear.SelectedValue
        Dim lstOrg = ctrlOrganization.GetAllChild(ctrlOrganization.CurrentValue)

        ' Báo cáo số lượng nhân sụ
        Dim _filterStr As New JObject

        Dim dt1 As DataTable = rep.Chart_Employee_Num(_filter, _filterStr.ToString())
        For i = 0 To dt1.Rows.Count - 1
            If i = dt1.Rows.Count - 1 Then
                data &= "{name: '" & dt1(i).Item("ORG_NAME") & " (" & dt1(i).Item("TOTAL") & ")" & "', y: " & dt1(i).Item("TOTAL") & ",sliced: true,selected: true}"
            Else
                data &= "{name: '" & dt1(i).Item("ORG_NAME") & " (" & dt1(i).Item("TOTAL") & ")" & "', y: " & dt1(i).Item("TOTAL") & "}," & vbNewLine
            End If
        Next

        'Báo cáo bậc ld
        Dim dt2 As DataTable = rep.Chart_BAC_LAO_DONG(_filter, lstOrg, _filterStr.ToString())
        For i = 0 To dt2.Rows.Count - 1
            If i = dt2.Rows.Count - 1 Then
                data2 &= "{name: '" & dt2(i).Item("NAME") & " (" & dt2(i).Item("TOTAL") & ")" & "', y: " & dt2(i).Item("TOTAL") & ",sliced: true,selected: true}"
            Else
                data2 &= "{name: '" & dt2(i).Item("NAME") & " (" & dt2(i).Item("TOTAL") & ")" & "', y: " & dt2(i).Item("TOTAL") & "}," & vbNewLine
            End If
        Next

        'Thâm niên công tác
        Dim dtData3 As DataTable = rep.Chart_TNCT(_filter, lstOrg, _filterStr.ToString())
        Dim dt3 As New DataTable
        dt3.Columns.Add("TOTAL")
        dt3.Columns.Add("NAME")
        Dim lstData As New List(Of String)
        If dtData3.Rows(0).Item("NUMYEAR1").ToString() > "0" Then
            dt3.Rows.Add(dtData3.Rows(0).Item("NUMYEAR1").ToString(), "< 1 năm (" & dtData3.Rows(0).Item("NUMYEAR1").ToString() & ")")
        End If
        If dtData3.Rows(0).Item("NUMYEAR2").ToString() > "0" Then
            dt3.Rows.Add(dtData3.Rows(0).Item("NUMYEAR2").ToString(), "1-2 năm (" & dtData3.Rows(0).Item("NUMYEAR2").ToString() & ")")
        End If
        If dtData3.Rows(0).Item("NUMYEAR3").ToString() > "0" Then
            dt3.Rows.Add(dtData3.Rows(0).Item("NUMYEAR3").ToString(), "3-5 năm (" & dtData3.Rows(0).Item("NUMYEAR3").ToString() & ")")

        End If
        If dtData3.Rows(0).Item("NUMYEAR4").ToString() > "0" Then
            dt3.Rows.Add(dtData3.Rows(0).Item("NUMYEAR4").ToString(), ">5 năm (" & dtData3.Rows(0).Item("NUMYEAR4").ToString() & ")")

        End If

        For i = 0 To dt3.Rows.Count - 1
            If dt3.Rows(i).Item("TOTAL") > "0" Then
                If i = dt3.Rows.Count - 1 Then
                    data3 &= "{name: '" & dt3(i).Item("NAME") & "', y: " & dt3(i).Item("TOTAL") & ",sliced: true,selected: true}"
                Else
                    data3 &= "{name: '" & dt3(i).Item("NAME") & "', y: " & dt3(i).Item("TOTAL") & "}," & vbNewLine
                End If
            End If
        Next
        lstData = New List(Of String)
        Dim itemTNCT As String = ""
        itemTNCT = "{name: 'name', colorByPoint: true, data:[" + data3 + "]}"
        lstData.Add(itemTNCT)
        For i = 0 To lstData.Count - 1
            If i = lstData.Count - 1 Then
                series_tnct &= lstData(i)
            Else
                series_tnct &= lstData(i) & ","
            End If
        Next

        'Thống kê số lượng nhân viên tại nơi làm việc
        Dim dt4 As DataTable = rep.Chart_WorkPlace(_filter, lstOrg, "")
        lstData = New List(Of String)
        For i = 0 To dt4.Rows.Count - 1
            Dim itemData As String = ""
            If i = 0 Then
                itemData &= "{name: '" & dt4(i).Item("NAME") & "', data: [" & dt4(i).Item("TOTAL") & "]},"
            ElseIf i = dt4.Rows.Count - 1 Then
                itemData &= "{name: '" & dt4(i).Item("NAME") & "', data: [" & dt4(i).Item("TOTAL") & "]}"
            Else
                itemData &= "{name: '" & dt4(i).Item("NAME") & "', data: [" & dt4(i).Item("TOTAL") & "]},"
            End If
            lstData.Add(itemData)
        Next
        For i = 0 To lstData.Count - 1
            series_work_place &= lstData(i)
        Next

        ' Báo cáo theo độ tuổi
        Dim dt5 As DataTable = rep.Chart_Age(_filter, lstOrg, "")
        lstData = New List(Of String)
        Dim dtAge As New DataTable
        dtAge.Columns.Add("TOTAL")
        dtAge.Columns.Add("NAME")
        dtAge.Rows.Add(dt5.Rows(0).Item("AGE29").ToString(), "<30")
        dtAge.Rows.Add(dt5.Rows(0).Item("AGE30").ToString(), "30-35")
        dtAge.Rows.Add(dt5.Rows(0).Item("AGE40").ToString(), "36 - 40")
        dtAge.Rows.Add(dt5.Rows(0).Item("AGE50").ToString(), "41 - 50")
        dtAge.Rows.Add(dt5.Rows(0).Item("AGE58").ToString(), "51 - 58")
        dtAge.Rows.Add(dt5.Rows(0).Item("AGE59").ToString(), ">=59")
        For i = 0 To dtAge.Rows.Count - 1
            Dim itemData As String = ""
            If i = 0 Then
                itemData &= "{name: '" & dtAge(i).Item("NAME") & "', data: [" & dtAge(i).Item("TOTAL") & "]},"
            ElseIf i = dtAge.Rows.Count - 1 Then
                itemData &= "{name: '" & dtAge(i).Item("NAME") & "', data: [" & dtAge(i).Item("TOTAL") & "]}"
            Else
                itemData &= "{name: '" & dtAge(i).Item("NAME") & "', data: [" & dtAge(i).Item("TOTAL") & "]},"
            End If
            lstData.Add(itemData)
        Next
        For i = 0 To lstData.Count - 1
            series_age &= lstData(i)
        Next

        'Thống kê trình độ học vấn
        Dim dt6 As DataTable = rep.Chart_TRINHDO_HOCVAN(_filter, lstOrg, "")
        lstData = New List(Of String)
        For i = 0 To dt6.Rows.Count - 1
            Dim itemData As String = ""
            If i = 0 Then
                itemData &= "{name: '" & dt6(i).Item("NAME") & "', data: [" & dt6(i).Item("TOTAL") & "]},"
            ElseIf i = dt6.Rows.Count - 1 Then
                itemData &= "{name: '" & dt6(i).Item("NAME") & "', data: [" & dt6(i).Item("TOTAL") & "]}"
            Else
                itemData &= "{name: '" & dt6(i).Item("NAME") & "', data: [" & dt6(i).Item("TOTAL") & "]},"
            End If
            lstData.Add(itemData)
        Next
        For i = 0 To lstData.Count - 1
            series_learing_lv &= lstData(i)
        Next
    End Sub
    Private Sub Chart_Total_Export(ByVal type As Decimal)
        Dim rep As New ProfileRepository
        Dim _filter As New ParamDTO
        _filter.ORG_ID = ctrlOrganization.CurrentValue
        _filter.MONTH = cboMonth.SelectedValue
        _filter.YEAR = cboYear.SelectedValue
        Dim lstOrg = ctrlOrganization.GetAllChild(ctrlOrganization.CurrentValue)

        ' Báo cáo số lượng nhân sụ
        Dim _filterStr As New JObject
        Select Case type
            Case 1
                Dim dt1 As DataTable = rep.Chart_Employee_Num(_filter, _filterStr.ToString(), isExport)
                ExportTemplate("Profile\ExportData.xlsx",
                                      dt1, Nothing,
                            "Thongkesoluongnhansu_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
            Case 2
                'Báo cáo bậc ld
                Dim dt2 As DataTable = rep.Chart_BAC_LAO_DONG(_filter, lstOrg, _filterStr.ToString(), isExport)
                ExportTemplate("Profile\ExportData.xlsx",
                                      dt2, Nothing,
                            "Thongkebaclaodong_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
            Case 3
                'Thâm niên công tác
                Dim dt3 As DataTable = rep.Chart_TNCT(_filter, lstOrg, _filterStr.ToString(), isExport)
                ExportTemplate("Profile\ExportData.xlsx",
                                      dt3, Nothing,
                            "Thongkethamniencongtac_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
            Case 4
                'Thống kê số lượng nhân viên tại nơi làm việc
                Dim dt4 As DataTable = rep.Chart_WorkPlace(_filter, lstOrg, "", isExport)
                ExportTemplate("Profile\ExportData.xlsx",
                                      dt4, Nothing,
                            "Thongkesoluongnhanvientainoilamviec_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
            Case 5
                ' Báo cáo theo độ tuổi
                Dim dt5 As DataTable = rep.Chart_Age(_filter, lstOrg, "", isExport)
                ExportTemplate("Profile\ExportData.xlsx",
                                      dt5, Nothing,
                            "Thongkelaodongtheodotuoilaodong_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
            Case 6
                'Thống kê trình độ học vấn
                Dim dt6 As DataTable = rep.Chart_TRINHDO_HOCVAN(_filter, lstOrg, "", isExport)
                ExportTemplate("Profile\ExportData.xlsx",
                                      dt6, Nothing,
                            "Thongketrinhdohocvan_thang" & _filter.MONTH.ToString & "nam" & _filter.YEAR.ToString)
        End Select

    End Sub
    Private Sub btnData_Click(sender As Object, e As ImageButtonClickEventArgs) Handles btnData.Click
        Try
            isExport = 1
            Select Case cboType.SelectedValue
                Case "1"
                    Chart_Employee_Num()
                Case "2"
                    Chart_Age()
                Case "3"
                    Chart_BAC_LAO_DONG()
                Case "4"
                    Chart_TNCT()
                Case "5"
                    Chart_WorkPlace()
                Case "6"
                    Chart_GENDER()
                Case "7"
                    Chart_TRINHDO_HOCVAN()
                Case "8"
                    Chart_TRINHDO_NGOAINGU()
                Case "9"
                    Chart_NEW_EMPLOYEE()
                Case "10"
                    Chart_TER_EMPLOYEE()
                Case "11"
                    Chart_HDLD()
                Case "12"
                    'Chart12()
                Case "13"
                    Chart_BO_NHIEM()
                Case "14"
                    'Chart_Total()
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnDataCol1_Click(sender As Object, e As ImageButtonClickEventArgs) Handles btnDataCol1.Click
        Try
            isExport = 1
            Chart_Total_Export(1)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnDataCol2_Click(sender As Object, e As ImageButtonClickEventArgs) Handles btnDataCol2.Click
        Try
            isExport = 1
            Chart_Total_Export(2)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnDataCol3_Click(sender As Object, e As ImageButtonClickEventArgs) Handles btnDataCol3.Click
        Try
            isExport = 1
            Chart_Total_Export(3)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnDataCol4_Click(sender As Object, e As ImageButtonClickEventArgs) Handles btnDataCol4.Click
        Try
            isExport = 1
            Chart_Total_Export(4)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnDataCol5_Click(sender As Object, e As ImageButtonClickEventArgs) Handles btnDataCol5.Click
        Try
            isExport = 1
            Chart_Total_Export(5)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnDataCol6_Click(sender As Object, e As ImageButtonClickEventArgs) Handles btnDataCol6.Click
        Try
            isExport = 1
            Chart_Total_Export(6)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dtData As DataTable,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dtData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
            isExport = 0
        Catch ex As Exception
            isExport = 0
            Return False
        End Try
        Return True
    End Function
#End Region

End Class