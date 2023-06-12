Imports Framework.UI
Imports WebAppLog

Public Class ctrlGroup
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Protected WithEvents ViewItem1 As ViewBase
    Protected WithEvents ViewItem2 As ViewBase
    Protected WithEvents ViewItem3 As ViewBase
    Protected WithEvents ViewItem4 As ViewBase
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()


#Region "Property"

    ''' <summary>
    ''' Obj GroupInfo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property GroupInfo As CommonBusiness.GroupDTO
        Get
            Return PageViewState(Me.ID & "_GroupInfo")
        End Get
        Set(ByVal value As CommonBusiness.GroupDTO)
            PageViewState(Me.ID & "_GroupInfo") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj CurrentTab
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CurrentTab As Integer
        Get
            Return PageViewState(Me.ID & "_CurrentTaab")
        End Get
        Set(ByVal value As Integer)
            PageViewState(Me.ID & "_CurrentTaab") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj ListGroups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListGroups As List(Of CommonBusiness.GroupDTO)
        Get
            Return PageViewState(Me.ID & "_ListGroups")
        End Get
        Set(ByVal value As List(Of CommonBusiness.GroupDTO))
            PageViewState(Me.ID & "_ListGroups") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateView()
            If Not IsPostBack Then
                rtabTab.SelectedIndex = 1
                rtabTab_TabClick(Nothing, Nothing)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Gọi đến các view để hiển thị các tab
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ViewItem1 = Me.Register("ctrlGroupUser", "Common", "ctrlGroupUser", "Secure")
            ViewItem2 = Me.Register("ctrlGroupFunction", "Common", "ctrlGroupFunction", "Secure")
            ViewItem3 = Me.Register("ctrlGroupOrganization", "Common", "ctrlGroupOrganization", "Secure")
            ViewItem4 = Me.Register("ctrlGroupReport", "Common", "ctrlGroupReport", "Secure")
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý việc ẩn hiện các tab và Refresh lại view khi có message
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Sub UpdateView(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ViewItem IsNot Nothing Then
                ViewItem.Visible = False
                ViewItem.SetProperty("ViewShowed", False)
            End If
            Select Case CurrentTab
                Case 0
                    ViewItem = ViewItem1
                Case 1
                    ViewItem = ViewItem2
                Case 2
                    ViewItem = ViewItem3
                Case 4
                    ViewItem = ViewItem4
            End Select

            TabView.Controls.Clear()
            TabView.Controls.Add(ViewItem)
            ViewItem.Visible = True
            ViewItem.SetProperty("ViewShowed", True)
            ViewItem.SetProperty("GroupInfo", GroupInfo)
            If Message = CommonMessage.ACTION_UPDATED Then
                ViewItem.UpdateControlState()
                ViewItem.Refresh()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý việc enable các nút theo params
    ''' </summary>
    ''' <param name="bEnabled"></param>
    ''' <remarks></remarks>
    Protected Sub ChangeControlStatus(ByVal bEnabled As Boolean)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            lstGroup.Enabled = bEnabled
            rtabTab.Enabled = bEnabled
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub DataBind()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim log = HistaffFrameworkPublic.UserLogHelper.GetCurrentLogUser()
            Dim rep As New CommonRepository
            ListGroups = rep.GetGroupListToComboListBox()
            Dim query = ListGroups
            If log.Username.ToUpper <> "ADMIN" Then
                query = (From p In ListGroups Where p.IS_HR_ADMIN Is Nothing OrElse p.IS_HR_ADMIN = 0 Select p).ToList()
            End If

            'Đưa dữ liệu vào Grid
            If query IsNot Nothing Then
                lstGroup.DataValueField = "ID"
                lstGroup.DataTextField = "NAME"
                lstGroup.DataSource = query
                lstGroup.DataBind()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien OnReceiveData cua control ctrlGroup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub ctrlGroup_OnReceiveData(ByVal sender As ViewBase, ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.EventData = CommonMessage.ACTION_UPDATED Then
                ChangeControlStatus(True)
            ElseIf e.EventData = CommonMessage.ACTION_UPDATING Then
                ChangeControlStatus(False)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien click cua control rtabTab
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rtabTab_TabClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTabStripEventArgs) Handles rtabTab.TabClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CurrentTab = rtabTab.SelectedIndex
            UpdateView(CommonMessage.ACTION_UPDATED)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien chọn item của control lstGroup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lstGroup_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstGroup.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ListGroups IsNot Nothing Then
                Dim query = (From p In ListGroups Where p.ID.ToString() = lstGroup.SelectedItem.Value Select p).SingleOrDefault
                If query IsNot Nothing Then
                    GroupInfo = query
                    UpdateView(CommonMessage.ACTION_UPDATED)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

#End Region

End Class