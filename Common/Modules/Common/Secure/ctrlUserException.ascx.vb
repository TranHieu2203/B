Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlUserException
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Protected WithEvents ViewItem1 As ViewBase
    Protected WithEvents ViewItem2 As ViewBase
    Protected WithEvents ViewItem3 As ViewBase
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Secure/" + Me.GetType().Name.ToString()


#Region "Property"
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Thông tin tài khoản
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UserInfo As CommonBusiness.UserDTO
        Get
            Return PageViewState(Me.ID & "_UserInfo")
        End Get
        Set(ByVal value As CommonBusiness.UserDTO)
            PageViewState(Me.ID & "_UserInfo") = value
        End Set
    End Property
    Property GridIDSelect As Decimal
        Get
            Return PageViewState(Me.ID & "_GridIDSelect")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_GridIDSelect") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Current tab
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
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Danh sách tài khoản
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListUsers As List(Of CommonBusiness.UserDTO)
        Get
            Return PageViewState(Me.ID & "_ListUsers")
        End Get
        Set(ByVal value As List(Of CommonBusiness.UserDTO))
            PageViewState(Me.ID & "_ListUsers") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Hiển thị thông tin các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgGrid.SetFilter()
            rgGrid.AllowCustomPaging = True
            UpdateView()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewInit khởi tạo các thiết lập, giá trị cho các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ctrlOrg.AutoPostBack = True
            ctrlOrg.CheckBoxes = TreeNodeTypes.All
            ctrlOrg.CheckChildNodes = True
            ctrlOrg.CheckParentNodes = False

            ViewItem1 = Me.Register("ctrlUserFunctionException", "Common", "ctrlUserFunctionException", "Secure")
            ViewItem2 = Me.Register("ctrlUserOrganization", "Common", "ctrlUserOrganization", "Secure")
            ViewItem3 = Me.Register("ctrlUserReport", "Common", "ctrlUserReport", "Secure")
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái của trang tương ứng với từng tab
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
            End Select
            TabView.Controls.Clear()
            TabView.Controls.Add(ViewItem)
            ViewItem.Visible = True
            ViewItem.SetProperty("ViewShowed", True)
            ViewItem.SetProperty("UserInfo", UserInfo)
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
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Thay đổi trạng thái các control trên trang
    ''' </summary>
    ''' <param name="bEnabled"></param>
    ''' <remarks></remarks>
    Protected Sub ChangeControlStatus(ByVal bEnabled As Boolean)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            EnabledGrid(rgGrid, bEnabled)
            rtabTab.Enabled = bEnabled
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
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện OnReceiveData của user control ctrlUser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub ctrlUser_OnReceiveData(ByVal sender As ViewBase, ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
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
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Click khi click vào tab rtabTab
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
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedIndexChanged của grid rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgGrid.SelectedIndexChanged
        Dim item As GridDataItem
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            item = CType(rgGrid.SelectedItems(0), GridDataItem)
            If ListUsers IsNot Nothing Then
                UserInfo = (From p In ListUsers
                            Where p.ID = item.GetDataKeyValue("ID")
                            Select p).FirstOrDefault


                If UserInfo IsNot Nothing Then
                    hidID.Value = UserInfo.ID
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
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource của rad grid rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        Dim MaximumRows As Integer
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New CommonRepository
                Dim Sorts As String = rgGrid.MasterTableView.SortExpressions.GetSortString()
                Dim _filter As New UserDTO
                SetValueObjectByRadGrid(rgGrid, _filter)
                '_filter.ACTFLG = "A"
                '_filter.IS_APP = True
                If Sorts IsNot Nothing Then
                    Me.ListUsers = rep.GetUser(_filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows, Sorts)
                Else
                    Me.ListUsers = rep.GetUser(_filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows)
                End If
                'Đưa dữ liệu vào Grid
                rgGrid.VirtualItemCount = MaximumRows
                rgGrid.DataSource = Me.ListUsers
            End Using
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

    Private Sub ctrlOrg_CheckedNodeChanged(sender As Object, e As EventArgs) Handles ctrlOrg.CheckedNodeChanged
        Try
            Dim lstOrgID As List(Of String) = ctrlOrg.CheckedValueGroups

            If UserInfo IsNot Nothing Then
                Dim lstOrg As New List(Of Decimal)
                UserInfo.LST_ORG = lstOrg
                For i = 0 To lstOrgID.Count - 1
                    UserInfo.LST_ORG.Add(Decimal.Parse(lstOrgID(i)))
                Next
                UpdateView(CommonMessage.ACTION_UPDATED)
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
End Class