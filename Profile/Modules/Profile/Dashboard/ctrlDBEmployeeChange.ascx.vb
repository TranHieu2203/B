Imports Common
Imports Framework.UI
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlDBEmployeeChange
    Inherits Common.CommonView

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>MustAuthorize</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Property MustAuthorize As Boolean = False

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>width</summary>
    ''' <remarks></remarks>
    Private width As Integer

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>height</summary>
    ''' <remarks></remarks>
    Private height As Integer

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>data</summary>
    ''' <remarks></remarks>
    Public data As String ''{name: 'Tokyo',data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]},\n

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>categories</summary>
    ''' <remarks></remarks>
    Public categories As String

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>enabled</summary>
    ''' <remarks></remarks>
    Public enabled As String

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>type</summary>
    ''' <remarks></remarks>
    Public type As String

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Dashboard" + Me.GetType().Name.ToString()
    Dim _orgID As Decimal

#Region "Property"

    ''' <summary>
    ''' StatisticType
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StatisticType As String
        Get
            Return Session(Me.ID & "_StatisticType")
        End Get

        Set(ByVal value As String)
            Session(Me.ID & "_StatisticType") = value
        End Set
    End Property

    ''' <summary>
    ''' StatisticData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StatisticData As List(Of StatisticDTO)
        Get
            Return Session(Me.ID & "_StatisticData")
        End Get

        Set(ByVal value As List(Of StatisticDTO))
            Session(Me.ID & "_StatisticData") = value
        End Set
    End Property

    ''' <summary>
    ''' StatisticList
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

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

            _orgID = 1
            If Request("orgid") IsNot Nothing Then
                _orgID = CInt(Request("orgid"))
            End If
            If Not IsPostBack Then
                Refresh(CommonMessage.ACTION_UPDATED)
            Else
                Refresh()
            End If

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>Fill du lieu len combobox</summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim rep As New ProfileDashboardRepository
            'If StatisticList Is Nothing Then
            '    StatisticList = rep.GetListChangeStatistic()
            'End If
            StatisticList = rep.GetListChangeStatistic()
            Utilities.FillDropDownList(cboType, StatisticList, "NAME_VN", "CODE", Common.Common.SystemLanguage, False, StatisticType)

            If cboType.Items.Count > 0 And StatisticType = "" Then
                cboType.SelectedIndex = 0
            End If

            If Common.Common.OrganizationLocationDataSession Is Nothing Then
                Dim rep1 As New CommonRepository
                rep1.GetOrganizationLocationTreeView()
                rep1.Dispose()
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileDashboardRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If width > 1000 Then
                btnMax.Text = "Restore"
            Else
                btnMax.Text = "Maximize"
            End If

            If cboType.SelectedValue = "" Then Exit Sub

            StatisticData = rep.GetChangeStatistic(_orgID, cboType.SelectedValue)
            StatisticData = StatisticData.OrderBy(Function(p) CInt(p.NAME)).ToList

            If StatisticData.Count > 0 Then
                data = String.Empty
                Select Case cboType.SelectedValue
                    Case "EMP_COUNT_YEAR", "EMP_COUNT_MON"
                        type = "column"
                        enabled = "false"
                        categories = "xAxis: { type: 'category', labels: { rotation: 0 }},"
                        data = "{name: 'Tổng số nhân viên',data: ["
                        For index As Integer = 0 To StatisticData.Count - 1
                            If index = StatisticData.Count - 1 Then
                                data &= "['" & StatisticData(index).NAME & "', " & StatisticData(index).VALUE & "]"
                            Else
                                data &= "['" & StatisticData(index).NAME & "', " & StatisticData(index).VALUE & "],"
                            End If
                        Next
                        data &= "]}"

                    Case "EMP_CHANGE_MON", "TER_CHANGE_MON"
                        type = "line"
                        enabled = "true"
                        categories = " xAxis: { categories: ['1', '2', '3', '4', '5', '6','7', '8', '9', '10', '11', '12'] },"
                        Select Case cboType.SelectedValue
                            Case "EMP_CHANGE_MON"
                                data = "{name: 'Tuyển mới',data: ["
                            Case "TER_CHANGE_MON"
                                data = "{name: 'Nghỉ việc',data: ["
                        End Select
                        For index As Integer = 0 To StatisticData.Count - 1
                            If index = StatisticData.Count - 1 Then
                                data &= StatisticData(index).VALUE
                            Else
                                data &= StatisticData(index).VALUE & ","
                            End If
                        Next
                        data &= "]}"
                End Select
            End If

            'Select Case cboType.SelectedValue
            '    Case "EMP_COUNT_YEAR", "EMP_COUNT_MON"
            '        charData1.Width = CInt(If(width = 0, charData1.Width.Value, width))
            '        charData1.Height = CInt(If(height = 0, charData1.Height.Value, height))

            '        'Đặt title cho chart
            '        'charData1.ChartTitle.TextBlock.Text = "Thống kê " & cboType.Text
            '        charData1.ChartTitle.Visible = False
            '        charData1.PlotArea.YAxis.Step = 1
            '        charData1.PlotArea.XAxis.Step = 1

            '        'Load data
            '        'charData1.Series(0).DefaultLabelValue = "#Y, #%"
            '        charData1.Series(0).Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.Nothing
            '        charData1.Series(0).DataYColumn = "VALUE"
            '        charData1.PlotArea.XAxis.DataLabelsColumn = "NAME"
            '        If (From p In StatisticData Where p.VALUE <> 0).Any Then
            '            charData1.DataSource = StatisticData
            '        Else
            '            charData1.DataSource = Nothing
            '        End If
            '        charData1.DataBind()
            '        charData1.Visible = True

            '        charData2.DataSource = Nothing
            '        charData2.DataBind()
            '        charData2.Visible = False
            '    Case "EMP_CHANGE_MON", "TER_CHANGE_MON"
            '        charData2.Width = CInt(If(width = 0, charData2.Width.Value, width))
            '        charData2.Height = CInt(If(height = 0, charData2.Height.Value, height))

            '        'Đặt title cho chart
            '        charData2.ChartTitle.Visible = False
            '        charData2.PlotArea.YAxis.Step = 1
            '        charData2.PlotArea.XAxis.Step = 1
            '        Select Case cboType.SelectedValue
            '            Case "EMP_CHANGE_MON"
            '                charData2.Series(0).Name = "Tuyển mới"
            '            Case "TER_CHANGE_MON"
            '                charData2.Series(0).Name = "Nghỉ việc"
            '        End Select
            '        charData2.Series(0).Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.SeriesName
            '        charData2.Series(0).DataYColumn = "VALUE"
            '        charData2.PlotArea.XAxis.DataLabelsColumn = "NAME"
            '        If (From p In StatisticData Where p.VALUE <> 0).Any Then
            '            charData2.DataSource = StatisticData
            '        Else
            '            charData2.DataSource = Nothing
            '        End If

            '        charData2.DataBind()
            '        charData2.Visible = True

            '        charData1.DataSource = Nothing
            '        charData1.DataBind()
            '        charData1.Visible = False
            'End Select

            If cboType.SelectedValue = "EMP_COUNT_YEAR" Or cboType.SelectedValue = "EMP_COUNT_MON" Then

            Else

            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>Event thay đổi combobox cboType</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboType.SelectedIndexChanged
        Dim rep As New ProfileRepository
        Dim validate As New OtherListDTO
        Dim dtData As DataTable = Nothing
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim bResult As Boolean = False

        Try
            For i As Integer = 0 To StatisticList.Count - 1
                If e.Value = StatisticList(i).CODE.ToString Then
                    validate.ID = Convert.ToDecimal(StatisticList(i).ID.ToString)
                End If
            Next

            validate.ACTFLG = "A"
            validate.CODE = "DB_CHANGE"
            bResult = rep.ValidateOtherList(validate)

            If Not bResult Then
                Dim repDashBoard As New ProfileDashboardRepository
                StatisticList = repDashBoard.GetListChangeStatistic()

                Utilities.FillDropDownList(cboType, StatisticList, "NAME_VN", "CODE", Common.Common.SystemLanguage, False)
                StatisticType = StatisticList(0).CODE.ToString
                cboType.SelectedValue = StatisticList(0).CODE.ToString
            End If

            StatisticType = e.Value
            Refresh(CommonMessage.ACTION_UPDATED)
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

#End Region

End Class