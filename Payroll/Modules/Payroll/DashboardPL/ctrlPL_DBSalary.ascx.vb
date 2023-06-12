Imports Common
Imports Framework.UI.Utilities

Public Class ctrlPL_DBSalary
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"
    Public Property StatisticType As String
        Get
            Return Session(Me.ID & "_StatisticType")
        End Get
        Set(ByVal value As String)
            Session(Me.ID & "_StatisticType") = value
        End Set
    End Property
    'Public Property StatisticData As List(Of StatisticDTO)
    '    Get
    '        Return Session(Me.ID & "_StatisticData")
    '    End Get
    '    Set(ByVal value As List(Of StatisticDTO))
    '        Session(Me.ID & "_StatisticData") = value
    '    End Set
    'End Property

#End Region

#Region "Page"
    Private width As Integer
    Private height As Integer
    Public data As String ''{name: 'Tokyo',data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]},\n
    Public categories As String
    Public enabled As String
    Public type As String

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
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            LoadYear()
            cboYear.SelectedValue = Date.Now.Year

            If Common.Common.OrganizationLocationDataSession Is Nothing Then
                Dim rep1 As New CommonRepository
                rep1.GetOrganizationLocationTreeView()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim dsData As DataSet
        Using rep As New PayrollRepository
            Try
                dsData = rep.GetStatisticSalaryTracker(cboYear.SelectedValue)
                RefreshBar(dsData)
            Catch ex As Exception
                Throw ex
            End Try

        End Using
    End Sub

#End Region

#Region "Event"

    Protected Sub cboYear_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboYear.TextChanged
        Refresh(CommonMessage.ACTION_UPDATED)
    End Sub

    Public Sub RefreshBar(ByVal dsData As DataSet)
        Try
            'Đặt title cho chart
            'charData1.PlotArea.YAxis.Step = 1
            'charData1.PlotArea.XAxis.Step = 1

            'charData1.Series(0).Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.SeriesName
            'charData1.Series(0).DataYColumn = "TOTAL_DINHBIEN"
            'charData1.Series(0).DefaultLabelValue = "#Y{n0}"
            'charData1.Series(1).Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.SeriesName
            'charData1.Series(1).DataYColumn = "TOTAL_THUCTE"
            'charData1.Series(1).DefaultLabelValue = "#Y{n0}"
            'charData1.PlotArea.XAxis.DataLabelsColumn = "MONTH"
            'charData1.DataSource = dsData.Tables(0)
            'charData1.DataBind()
            data = "{name: '" & Translate("Tổng quỹ lương theo định biên") & "',data: ["
            For index As Integer = 0 To dsData.Tables(0).Rows.Count - 1
                If index = dsData.Tables(0).Rows.Count - 1 Then
                    data &= dsData.Tables(0).Rows(index)("TOTAL_DINHBIEN")
                Else
                    data &= dsData.Tables(0).Rows(index)("TOTAL_DINHBIEN") & ","
                End If
            Next
            data &= "]},"
            data &= " {name: '" & Translate("Tổng chi phí lương thực tế") & "',data: ["
            For index As Integer = 0 To dsData.Tables(0).Rows.Count - 1
                If index = dsData.Tables(0).Rows.Count - 1 Then
                    data &= dsData.Tables(0).Rows(index)("TOTAL_THUCTE")
                Else
                    data &= dsData.Tables(0).Rows(index)("TOTAL_THUCTE") & ","
                End If
            Next
            data &= "]}"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



#End Region

#Region "Custom"
    Private Sub LoadYear()
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
        Catch ex As Exception

        End Try
    End Sub


#End Region
End Class