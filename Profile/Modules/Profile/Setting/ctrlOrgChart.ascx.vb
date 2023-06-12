Imports Common
Imports Profile.ProfileBusiness
Imports WebAppLog

Public Class ctrlOrgChart
    Inherits Common.CommonView
#Region "Property"
    Property OrgChart As List(Of OrgChartDTO)
        Get
            Return ViewState(Me.ID & "_Organization")
        End Get
        Set(ByVal value As List(Of OrgChartDTO))
            ViewState(Me.ID & "_Organization") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <summary>
    ''' Khởi tạo, load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        GetOrgChart()
    End Sub
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/Setting/" + Me.GetType().Name.ToString()
    ''' <summary>
    ''' Load data from DB to RadOrgChart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetOrgChart()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'Dim rep As New CommonRepository
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep2 As New ProfileBusinessRepository

                Using rep As New CommonRepository
                    If OrgChart Is Nothing Then
                        OrgChart = rep2.GetEmployeeOrgChart
                    End If
                    chartData1.Nodes.Clear()
                    chartData1.DataSource = OrgChart
                    chartData1.DataBind()
                    chartData1.CollapseAllNodes()
                    chartData1.CollapseAllGroups()

                End Using
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "") 'hsdx
        End Try
    End Sub
#End Region

#Region "Event"

#End Region

#Region "Custom"
#End Region

End Class