Imports Common
Imports Framework.UI
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlDBEmployeeStatistic
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
    ''' <summary>title</summary>
    ''' <remarks></remarks>
    Public title As String = ""

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>name</summary>
    ''' <remarks></remarks>
    Public name As String = "Nhân viên"

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>data</summary>
    ''' <remarks></remarks>
    Public data As String ''{name: 'Microsoft Internet Explorer', y: 56.33},\n

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
            '    StatisticList = rep.GetListEmployeeStatistic()
            'End If
            StatisticList = rep.GetListEmployeeStatistic()
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
                StatisticData = rep.GetEmployeeStatistic(_orgID, cboType.SelectedValue)
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
            validate.CODE = "DB_EMP"
            bResult = rep.ValidateOtherList(validate)

            If Not bResult Then
                Dim repDashBoard As New ProfileDashboardRepository
                StatisticList = repDashBoard.GetListEmployeeStatistic()

                Utilities.FillDropDownList(cboType, StatisticList, "NAME_VN", "CODE", Common.Common.SystemLanguage, False)
                StatisticType = StatisticList(0).CODE.ToString
                cboType.SelectedValue = StatisticList(0).CODE.ToString
            End If
            rep.Dispose()
            StatisticType = e.Value
            Refresh(CommonMessage.ACTION_UPDATED)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
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