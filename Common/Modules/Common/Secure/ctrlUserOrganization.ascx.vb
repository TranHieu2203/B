Imports Common.CommonBusiness
Imports Framework.UI
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlUserOrganization
    Inherits CommonView
    Public Property UserInfo As UserDTO
    Public Overrides Property MustAuthorize As Boolean = True


    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"
    ''' <summary>
    ''' UserOganization
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserOganization As List(Of Decimal)
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_UserOganization") = value
        End Set
        Get
            Return PageViewState(Me.ID & "_UserOganization")
        End Get
    End Property
    ''' <summary>
    ''' UserOganizationFunction
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserOganizationFunction As List(Of Decimal)
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_UserOganizationFunction") = value
        End Set
        Get
            Return PageViewState(Me.ID & "_UserOganizationFunction")
        End Get
    End Property
    ''' <summary>
    ''' UserID_Old
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserID_Old As Decimal
        Get
            Return PageViewState(Me.ID & "_UserID_Old")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_UserID_Old") = value
        End Set
    End Property
    ''' <summary>
    ''' lstOrgID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstOrgID As List(Of CommonBusiness.UserOrgAccessDTO)
        Get
            Return PageViewState(Me.ID & "_lstOrgID")
        End Get
        Set(ByVal value As List(Of CommonBusiness.UserOrgAccessDTO))
            PageViewState(Me.ID & "_lstOrgID") = value
        End Set
    End Property
    ''' <summary>
    ''' lstOrgIDFirst
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstOrgIDFirst As List(Of CommonBusiness.UserOrgAccessDTO)
        Get
            Return PageViewState(Me.ID & "_lstOrgIDFirst")
        End Get
        Set(ByVal value As List(Of CommonBusiness.UserOrgAccessDTO))
            PageViewState(Me.ID & "_lstOrgIDFirst") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo ViewInit
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarMain
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit,
                                        ToolbarItem.Seperator,
                                        ToolbarItem.Save,
                                        ToolbarItem.Cancel)
            orgLoca.AutoPostBack = False
            orgLoca.CheckBoxes = TreeNodeTypes.All
            orgLoca.CheckChildNodes = True
            orgLoca.CheckParentNodes = False
            orgLoca.ShowCommitee = True
            orgLoca.is_UYBAN = True
            orgLoca.build_UYBAN = True
            Refresh("ViewFirst")
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Hiển thị thông tin trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            UpdateControlStatus()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Cập nhật trạng thái các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UpdateControlStatus()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Then
                Me.MainToolBar.Items(0).Enabled = False

                Me.MainToolBar.Items(2).Enabled = True
                Me.MainToolBar.Items(3).Enabled = True

                orgLoca.Enabled = True
            Else
                If UserInfo IsNot Nothing Then
                    Me.MainToolBar.Items(0).Enabled = True
                Else
                    Me.MainToolBar.Items(0).Enabled = False
                End If
                Me.MainToolBar.Items(2).Enabled = False
                Me.MainToolBar.Items(3).Enabled = False

                orgLoca.Enabled = False
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Làm mới thiết lập, giá trị các control về mặc định
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If UserInfo Is Nothing Then Exit Sub

            If UserID_Old = Nothing Or UserID_Old <> UserInfo.ID Or
                Message = CommonMessage.ACTION_SAVED Or UserOganization Is Nothing Then
                Dim rep As New CommonRepository
                UserOganization = rep.GetUserOrganization(UserInfo.ID)
            End If
            If Message = "ViewFirst" Then
                lstOrgIDFirst = New List(Of UserOrgAccessDTO)
                For i = 0 To UserOganization.Count - 1
                    If lstOrgIDFirst IsNot Nothing AndAlso lstOrgIDFirst.Where(Function(f) f.ORG_ID = Decimal.Parse(UserOganization(i))).Count > 0 Then
                    Else
                        lstOrgIDFirst.Add(New CommonBusiness.UserOrgAccessDTO() With {.USER_ID = UserInfo.ID,
                                                                                 .ORG_ID = Decimal.Parse(UserOganization(i))})
                    End If
                Next
            End If
            If UserOganization IsNot Nothing Then
                lstOrgID = New List(Of UserOrgAccessDTO)
                For i = 0 To UserOganization.Count - 1
                    If lstOrgID IsNot Nothing AndAlso lstOrgID.Where(Function(f) f.ORG_ID = Decimal.Parse(UserOganization(i))).Count > 0 Then
                    Else
                        lstOrgID.Add(New CommonBusiness.UserOrgAccessDTO() With {.USER_ID = UserInfo.ID,
                                                                                 .ORG_ID = Decimal.Parse(UserOganization(i))})
                    End If
                Next
            End If
            'Đưa dữ liệu vào Grid
            orgLoca.CheckedValueKeys = UserOganization
            orgLoca.Bind_CheckedValueKeys = UserOganization

            'orgFunc.CheckedValueDecimals = UserOganizationFunction

            'Thay đổi trạng thái các control
            UpdateControlStatus()

            UserID_Old = UserInfo.ID
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' Xử lý sự kiện Command khi click item trên toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New CommonRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                    'Thay đổi trạng thái các control
                    UpdateControlStatus()
                    'Gửi thông điệp cho Parent View
                    Me.Send(CommonMessage.ACTION_UPDATING)
                Case CommonMessage.TOOLBARITEM_SAVE
                    If CurrentState = CommonMessage.STATE_EDIT Then
                        Dim lst As List(Of CommonBusiness.UserOrgAccessDTO)
                        'Dim lstFunc As List(Of CommonBusiness.SE_User_ORG_FUN_ACCESS)

                        lst = GetListOrgID()
                        'lstFunc = GetListOrgFuncID()
                        If lst.Count = 0 Then
                            If lstOrgID Is Nothing Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ORG), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                        If rep.UpdateUserOrganization(lst) Then
                            Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Refresh(CommonMessage.ACTION_SAVED)

                        Else
                            Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                        End If
                    End If
                    CurrentState = CommonMessage.STATE_NORMAL
                    'Thay đổi trạng thái các control
                    UpdateControlStatus()
                    'Gửi thông điệp cho Parent View
                    Me.Send(CommonMessage.ACTION_UPDATED)
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    'Thay đổi trạng thái các control
                    UpdateControlStatus()
                    Refresh()
                    'Gửi thông điệp cho Parent View
                    Me.Send(CommonMessage.ACTION_UPDATED)
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    ''' <summary>
    ''' Hàm lấy danh sách đơn vị của tài khoản
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetListOrgID() As List(Of CommonBusiness.UserOrgAccessDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lst As New List(Of CommonBusiness.UserOrgAccessDTO)
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If UserID_Old <> UserInfo.ID Then
                lstOrgID = New List(Of CommonBusiness.UserOrgAccessDTO)
            End If
            Dim lstOrg = orgLoca.CheckedValueGroups
            Dim orgList As List(Of Decimal) = orgLoca.GetAllOrgID()
            For i = 0 To lstOrg.Count - 1
                lst.Add(New CommonBusiness.UserOrgAccessDTO() With {.USER_ID = UserInfo.ID,
                                                                        .ORG_ID = Decimal.Parse(lstOrg(i))})
            Next

            Dim query = orgList.Except(lstOrg.ConvertAll(Of Decimal)(Function(i As Integer) i)).ToList
            If lstOrgID Is Nothing Then
                lstOrgID = lstOrgIDFirst
            End If
            Dim query2 = lstOrgID.Union(lst).ToList.Select(Function(o) o.ORG_ID).ToList.Except(query).ToList
            lstOrgID = (From p In query2 Select New CommonBusiness.UserOrgAccessDTO() With {.USER_ID = UserInfo.ID,
                                                                                           .ORG_ID = p}).Distinct.ToList
            Return lstOrgID
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
        Return lstOrgID
    End Function

#End Region

End Class