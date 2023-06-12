Imports Common
Imports Common.CommonBusiness
Imports Common.CommonMessage
Imports Framework.UI
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_EmpDtl
    Inherits CommonView
    Public WithEvents CurrentView As ViewBase
    Public WithEvents ViewImage As ViewBase

    Public Overrides Property MustAuthorize As Boolean = True

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

#Region "Properties"
    Property isChangeImage As Boolean
        Get
            Return PageViewState(Me.ID & "_isChangeImage")
        End Get
        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_isChangeImage") = value
        End Set
    End Property

    Property LoadDefaultImage As Boolean
        Get
            Return PageViewState(Me.ID & "_LoadDefaultImage")
        End Get
        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_LoadDefaultImage") = value
        End Set
    End Property

    'Lưu view hiện tại.
    'Thông tin cơ bản của nhân viên.
    Property EmployeeInfo As Profile.ProfileBusiness.EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As Profile.ProfileBusiness.EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property

    Property EmployeeID As String
        Get
            Return PageViewState(Me.ID & "_EmployeeID")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_EmployeeID") = value
        End Set
    End Property


#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 06/07/2017 11:09
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm khởi tạo các dữ liệu ban đầu cho các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim ViewName As String = Request.QueryString("Place")
            If ViewName Is Nothing OrElse ViewName.Trim = "" Then
                ViewName = "ctrlHU_EmpDtlProfile"
            End If
            AccessPanelBar()

            Dim item = rpbProfile.FindItemByValue(ViewName)
            If item IsNot Nothing Then
                item.Selected = True
            End If
            CurrentView = Me.Register(ViewName, "Profile", ViewName, "Business")
            If CurrentView Is Nothing Then
                Throw New NullReferenceException("Error when Register view")
            End If
            phProfile.Controls.Add(CurrentView)

            If ViewImage Is Nothing Then ViewImage = Me.Register("ctrlImageUpload", "Profile", "ctrlImageUpload", "Business")
            ViewPlaceHoderImage.Controls.Add(ViewImage)
            CurrentView.SetProperty("ImageFile", ViewImage.GetProperty("ImageFile"))
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 11:10
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm hiển thị các control trên trang theo các param truyền trên request
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                Dim strMessage As String = Request.QueryString("message")
                If strMessage IsNot Nothing AndAlso strMessage.ToUpper = "SUCCESS" Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                End If
                EmployeeID = Request.QueryString("emp")
                Dim strCurrentTabView As String = Request.QueryString("tab")
                If strCurrentTabView Is Nothing Then
                    strCurrentTabView = ""
                End If
                Dim State As String = Request.QueryString("state")
                If EmployeeID IsNot Nothing AndAlso EmployeeID.Trim <> "" Then
                    Dim rep As New ProfileBusinessRepository
                    If State = CommonMessage.STATE_EDIT OrElse State = CommonMessage.STATE_NORMAL Then
                        If EmployeeInfo Is Nothing Then
                            EmployeeInfo = rep.GetEmployeeByEmployeeID(EmployeeID) 'Lưu vào viewStates để truyền vào các view con.
                        End If
                        'ShowMessage(EmployeeID.Count.ToString, Utilities.NotifyType.Information)
                        CurrentView.SetProperty("EmployeeInfo", EmployeeInfo)

                        If EmployeeInfo IsNot Nothing And State = CommonMessage.STATE_EDIT Then
                            If EmployeeInfo.WORK_STATUS Is Nothing Or
                                (EmployeeInfo.WORK_STATUS IsNot Nothing AndAlso
                                 (EmployeeInfo.WORK_STATUS <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or
                                  (EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And
                                   EmployeeInfo.TER_EFFECT_DATE > Date.Now.Date))) Then
                                State = CommonMessage.STATE_EDIT
                            Else
                                ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                                State = CommonMessage.STATE_NORMAL
                            End If
                            lblFullName.Text = EmployeeInfo.FULLNAME_VN
                        End If
                    End If

                    If State = "Copy" Then
                        Dim list_panel = rpbProfile.GetAllItems()
                        For Each item In list_panel
                            If item.Value <> "" And item.Value <> "ctrlHU_EmpDtlProfile" Then
                                item.Visible = False
                            End If
                        Next

                        If EmployeeInfo Is Nothing Then
                            EmployeeInfo = rep.GetEmployeeByEmployeeID(EmployeeID) 'Lưu vào viewStates để truyền vào các view con.
                        End If
                        CurrentView.SetProperty("EmployeeInfo", EmployeeInfo)
                        If EmployeeInfo IsNot Nothing Then
                            If EmployeeInfo.WORK_STATUS IsNot Nothing Then
                                State = "Copy"
                            End If
                            lblFullName.Text = EmployeeInfo.FULLNAME_VN
                        End If
                    End If
                    rep.Dispose()

                End If

                CurrentState = IIf(State <> "", State, CommonMessage.STATE_NORMAL)
                CurrentView.CurrentState = CurrentState
                CurrentView.DataBind()
                Me.DataBind()
                ViewImage.SetProperty("EmployeeInfo", EmployeeInfo)
            End If
            UpdateControlState()
            CurrentView.UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Events"
    ''' <lastupdate>
    ''' 06/07/2017 11:12
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm sử lý sự kiện nhận dữ liệu của usercontrol ctrlHU_EmpDtl
    ''' Lấy lại trạng thái hiện tại của trang
    ''' Cập nhật trạng thái của các control trên page
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub ctrlHU_EmpDtl_OnReceiveData(ByVal sender As ViewBase, ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case e.EventData
                Case STATE_NEW
                    CurrentState = e.EventData
                    EmployeeInfo = Nothing
                Case STATE_EDIT, STATE_NORMAL
                    CurrentState = e.EventData
                Case Else
            End Select
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Customer"
    ''' <lastupdate>
    ''' 06/07/2017 11:25
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xét trạng thái truy cập panelbar theo nhóm quyền của user
    ''' Hiển thị profile của user theo nhóm quyền đó
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AccessPanelBar()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim i As Integer = 0
            Using rep As New CommonRepository
                Dim GroupAdmin As Boolean = rep.CheckGroupAdmin(Common.Common.GetUsername)
                Dim permissions As List(Of PermissionDTO) = rep.GetUserPermissions(Common.Common.GetUsername)
                While (i < rpbProfile.Items(0).Items.Count - 1)
                    Dim itm As RadPanelItem = rpbProfile.Items(0).Items(i)
                    Dim user = LogHelper.CurrentUser
                    If GroupAdmin = False Then
                        If permissions IsNot Nothing Then
                            Dim isPermissions = (From p In permissions Where p.FID = itm.Value And p.IS_REPORT = False).Any
                            If Not isPermissions Then
                                rpbProfile.Items(0).Items.RemoveAt(i)
                                Continue While
                            End If
                        End If
                    Else
                        If user.MODULE_ADMIN = "*" OrElse (user.MODULE_ADMIN IsNot Nothing AndAlso user.MODULE_ADMIN.Contains(Me.ModuleName)) Then
                        Else
                            rpbProfile.Items(0).Items.RemoveAt(i)
                            Continue While
                        End If
                    End If
                    i = i + 1
                End While
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 11:28
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức cập nhật trạng thái các control trên page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT OrElse CurrentState = CommonMessage.STATE_NEW Then
                rpbProfile.Enabled = False
            Else
                rpbProfile.Enabled = True
            End If

            'View Image
            ViewImage.CurrentState = CurrentState
            If LoadDefaultImage AndAlso isChangeImage = False Then
                ViewImage.SetProperty("LoadDefaultImage", True)
                isChangeImage = False
                LoadDefaultImage = False
            Else
                ViewImage.SetProperty("LoadDefaultImage", False)
            End If
            ViewImage.UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
#End Region

End Class