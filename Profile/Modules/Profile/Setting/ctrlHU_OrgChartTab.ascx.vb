Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports WebAppLog
''' <summary>
''' Class thuc hien xu ly chung quan ly dia chi: quoc gia, thanh pho, huyen, xa
''' </summary>
''' <remarks></remarks>
Public Class ctrlHU_OrgChartTab
    Inherits CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Modules/Profile/Setting/" + Me.GetType().Name.ToString()
    Protected WithEvents ViewItem As ViewBase


#Region "Page"
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView,load trang thai cac control cua usercontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            UpdateControlState()

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' cap nhat trang thai cua cac control trong usercontrol
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim currentTab As String = "OrgChart"
            Dim MyArray() As String = {"OrgChart", "JobPosition"}
            If (MyArray.ToList().IndexOf(Request.QueryString("tab")) > 0) Then currentTab = Request.QueryString("tab") & ""
            CurrentState = currentTab
            Select Case CurrentState
                Case "JobPosition"
                    RadTabStrip1.SelectedIndex = 1
                    rpvJobPosition.Selected = True
                    ViewItem = Me.Register("JobPosition", "Organize", "ctrlHU_JobPosition", "Dashboard")
                    If Not rpvJobPosition.Controls.Contains(ViewItem) Then
                        rpvJobPosition.Controls.Add(ViewItem)
                    End If
                Case Else
                    RadTabStrip1.SelectedIndex = 0
                    rpvOrgChart.Selected = True
                    ViewItem = Me.Register("OrgChart", "Organize", "ctrlHU_OrgChart", "Dashboard")
                    If Not rpvOrgChart.Controls.Contains(ViewItem) Then
                        rpvOrgChart.Controls.Add(ViewItem)
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
#End Region

#Region "Event"
    '''<editby>HongDX</editby>
    ''' <contentEdit>Khai bao dir 1 lan, redirect 1 lan=> ngan gon code </contentEdit>
    ''' <summary>
    ''' event click tab => Redirect sang usercontrol quoc gia, tinh thanh, quan huyen, phuong xa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadTabStrip1_TabClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTabStripEventArgs) Handles RadTabStrip1.TabClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim dir As New Dictionary(Of String, Object)
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case sender.SelectedIndex
                Case 0
                    dir.Add("tab", "OrgChart")
                    ' Response.Redirect("?mid=Organize&fid=ctrlHU_OrgChartTab&group=Report&tab=OrgChart")
                Case 1
                    dir.Add("tab", "JobPosition")
                    'Response.Redirect("?mid=Organize&fid=ctrlHU_OrgChartTab&group=Report&tab=JobPosition")
            End Select
            Redirect("Organize", "ctrlHU_OrgChartTab", "Dashboard", dir)
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <summary>
    ''' event OnReceiveData thay doi trang thai tabStrip
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub ctrlPlace_OnReceiveData(ByVal sender As IViewListener(Of ViewBase), ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case e.EventData
                Case STATE_EDIT
                    RadTabStrip1.Enabled = False
                Case Else
                    RadTabStrip1.Enabled = True
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class