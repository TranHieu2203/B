Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Telerik.Charting

Public Class ctrlDBEmployeeStatisticEducation
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
    Public Property StatisticData As List(Of StatisticDTO)
        Get
            Return Session(Me.ID & "_StatisticData")
        End Get
        Set(ByVal value As List(Of StatisticDTO))
            Session(Me.ID & "_StatisticData") = value
        End Set
    End Property
    Public Property StatisticList As List(Of OtherListDTO)
        Get
            Return Session(Me.ID & "_StatisticList")
        End Get
        Set(ByVal value As List(Of OtherListDTO))
            Session(Me.ID & "_StatisticList") = value
        End Set
    End Property
#End Region

#Region "Page"
    Private width As Integer
    Private height As Integer
    Public title As String = ""
    Public name As String = "Nhân viên"
    Public data As String ''{name: 'Microsoft Internet Explorer', y: 56.33},\n
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
            Dim rep As New ProfileDashboardRepository
            If StatisticList Is Nothing Then
                StatisticList = rep.GetListEmployeeStatistic()
            End If
            Utilities.FillDropDownList(cboType, StatisticList, "NAME_VN", "CODE", Common.Common.SystemLanguage, False, StatisticType)
            If cboType.Items.Count > 0 And StatisticType = "" Then
                cboType.SelectedIndex = 0
            End If
            If Common.Common.OrganizationLocationDataSession Is Nothing Then
                Dim rep1 As New CommonRepository
                rep1.GetOrganizationLocationTreeView()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileDashboardRepository
        Try
            'If width > 1000 Then
            '    btnMax.Text = "Restore"
            'Else
            '    btnMax.Text = "Maximize"
            'End If
            'If cboType.SelectedValue = "" Then Exit Sub

            'If StatisticData Is Nothing Or Message = CommonMessage.ACTION_UPDATED Then
            '    StatisticData = rep.GetEmployeeStatistic(cboType.SelectedValue)
            'End If

            'charData1.Width = CInt(If(width = 0, charData1.Width.Value, width))
            'charData1.Height = CInt(If(height = 0, charData1.Height.Value, height))

            ''Đặt title cho chart
            ''charData1.ChartTitle.TextBlock.Text = "Thống kê " & cboType.Text
            'charData1.ChartTitle.Visible = False

            ''Load data
            'charData1.Series(0).DefaultLabelValue = "#Y [#%]"
            'charData1.Series(0).Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.ItemLabels

            'If (From p In StatisticData Where p.VALUE <> 0).Any Then
            '    charData1.DataSource = StatisticData
            'Else
            '    charData1.DataSource = Nothing
            'End If
            'charData1.Series(0).DataYColumn = "VALUE"
            'charData1.DataBind()
            title = "Thống kê " & cboType.Text
            data = String.Empty
            If StatisticData Is Nothing Or Message = CommonMessage.ACTION_UPDATED Then
                StatisticData = rep.GetEmployeeStatistic(cboType.SelectedValue)
            End If
            If (From p In StatisticData Where p.VALUE <> 0).Any Then
                For index As Integer = 0 To StatisticData.Count - 1
                    If index = StatisticData.Count - 1 Then
                        data &= "{name: '" & StatisticData(index).NAME & "', y: " & StatisticData(index).VALUE & "}"
                    Else
                        data &= "{name: '" & StatisticData(index).NAME & "', y: " & StatisticData(index).VALUE & "}," & vbNewLine
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"

    Private Sub cboType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboType.SelectedIndexChanged
        StatisticType = e.Value
        Refresh(CommonMessage.ACTION_UPDATED)
    End Sub
    'Private Sub charData1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Charting.ChartItemDataBoundEventArgs) Handles charData1.ItemDataBound
    '    Dim data As DataRowView = DirectCast(e.DataItem, DataRowView)
    '    e.SeriesItem.Name = data("NAME")

    '    For i As Integer = 0 To charData1.Series(0).Items.Count - 1
    '        Select Case i Mod 9
    '            Case 0
    '                charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
    '                charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(78, 149, 197)
    '                charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(32, 98, 162)
    '            Case 1
    '                charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
    '                charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(159, 218, 239)
    '                charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(212, 245, 255)
    '            Case 2
    '                charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
    '                charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(157, 209, 54)
    '                charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(210, 247, 114)
    '            Case 3
    '                charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
    '                charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(255, 126, 97)
    '                charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(238, 171, 151)
    '            Case 4
    '                charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
    '                charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(255, 193, 103)
    '                charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(255, 232, 204)
    '            Case 5
    '                charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
    '                charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(76, 171, 205)
    '                charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(134, 219, 244)
    '            Case 6
    '                charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
    '                charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(255, 255, 255)
    '                charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(255, 84, 84)
    '            Case 7
    '                charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
    '                charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(252, 255, 0)
    '                charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(254, 255, 130)
    '            Case 8
    '                charData1.Series(0).Items(i).Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
    '                charData1.Series(0).Items(i).Appearance.FillStyle.MainColor = Drawing.Color.FromArgb(8, 178, 0)
    '                charData1.Series(0).Items(i).Appearance.FillStyle.SecondColor = Drawing.Color.FromArgb(103, 248, 95)
    '        End Select
    '    Next
    'End Sub
#End Region

#Region "Custom"
#End Region

End Class