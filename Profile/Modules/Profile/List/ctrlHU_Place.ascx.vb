Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports WebAppLog
''' <summary>
''' Class thuc hien xu ly chung quan ly dia chi: quoc gia, thanh pho, huyen, xa
''' </summary>
''' <remarks></remarks>
Public Class ctrlHU_Place
    Inherits CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Modules/Profile/Report/" + Me.GetType().Name.ToString()
    Protected WithEvents ViewItem As ViewBase

#Region "Properties"
    Property Nation As NationDTO
        Get
            Return ViewState(Me.ID & "_Nation")
        End Get
        Set(ByVal value As NationDTO)
            ViewState(Me.ID & "_Nation") = value
        End Set
    End Property
#End Region

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
    'Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        InitControl()
    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

    '    End Try

    'End Sub
    'Protected Sub InitControl()
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow


    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    ''' <summary>
    ''' cap nhat trang thai cua cac control trong usercontrol
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim currentTab As String = ""
            currentTab = Request.QueryString("tab") & ""
            CurrentState = currentTab
            Select Case CurrentState
                Case "Province"
                    RadTabStrip1.SelectedIndex = 1
                    rpvProvince.Selected = True
                    ViewItem = Me.Register("Province", "Profile", "ctrlHU_Province", "List")
                    If Not rpvProvince.Controls.Contains(ViewItem) Then
                        rpvProvince.Controls.Add(ViewItem)
                    End If
                Case "District"
                    RadTabStrip1.SelectedIndex = 2
                    rpvDistrict.Selected = True
                    ViewItem = Me.Register("District", "Profile", "ctrlHU_District", "List")
                    If Not rpvDistrict.Controls.Contains(ViewItem) Then
                        rpvDistrict.Controls.Add(ViewItem)
                    End If
                Case "Ward"
                    RadTabStrip1.SelectedIndex = 3
                    rpvWard.Selected = True
                    ViewItem = Me.Register("Ward", "Profile", "ctrlHU_Ward", "List")
                    If Not rpvWard.Controls.Contains(ViewItem) Then
                        rpvWard.Controls.Add(ViewItem)
                    End If
                Case Else
                    RadTabStrip1.SelectedIndex = 0
                    rpvNations.Selected = True
                    ViewItem = Me.Register("Nation", "Profile", "ctrlHU_Nation", "List")
                    If Not rpvNations.Controls.Contains(ViewItem) Then
                        rpvNations.Controls.Add(ViewItem)
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
                    dir.Add("tab", "Nation")
                    ' Response.Redirect("?mid=Profile&fid=ctrlPlace&group=List&tab=Nation")
                Case 1
                    dir.Add("tab", "Province")
                    'Response.Redirect("?mid=Profile&fid=ctrlPlace&group=List&tab=Province")
                Case 2
                    dir.Add("tab", "District")
                    'Response.Redirect("?mid=Profile&fid=ctrlPlace&group=List&tab=District")
                Case 3
                    dir.Add("tab", "Ward")
                    'Response.Redirect("?mid=Profile&fid=ctrlPlace&group=List&tab=District")
            End Select
            Redirect("Profile", "ctrlHU_Place", "List", dir)
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