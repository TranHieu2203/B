Imports Common
Imports Common.CommonBusiness
Imports Common.CommonMessage
Imports Framework.UI
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_CanDtl
    Inherits CommonView
    Protected WithEvents CurrentView As ViewBase
    Public WithEvents ViewImage As ViewBase

    Public Overrides Property MustAuthorize As Boolean = True

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
    Property CandidateInfo As CandidateDTO
        Get
            Return PageViewState(Me.ID & "_CandidateInfo")
        End Get
        Set(ByVal value As CandidateDTO)
            PageViewState(Me.ID & "_CandidateInfo") = value
        End Set
    End Property

    Property CandidateCode As String
        Get
            Return PageViewState(Me.ID & "_CandidateCode")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_CandidateCode") = value
        End Set
    End Property

    Property ProgramID As Decimal
        Get
            Return PageViewState(Me.ID & "_ProgramID")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_ProgramID") = value
        End Set
    End Property

    Property EmpHasOnlyNewHireDecision As Boolean?
        Get
            Return PageViewState(Me.ID & "_EmpHasOnlyNewHireDecision")
        End Get
        Set(ByVal value As Boolean?)
            PageViewState(Me.ID & "_EmpHasOnlyNewHireDecision") = value
        End Set
    End Property


#End Region

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim ViewName As String = Request.QueryString("Place")
        Dim State As String = Request.QueryString("state")
        If ViewName Is Nothing OrElse ViewName.Trim = "" Then
            ViewName = "ctrlRC_CanDtlProfile"
        End If
        AccessPanelBar()
        Dim item = rpbRecruitment.FindItemByValue(ViewName)
        If item IsNot Nothing Then
            item.Selected = True
        End If
        CurrentView = Me.Register(ViewName, "Recruitment", ViewName, "Business")

        phRecruitment.Controls.Add(CurrentView)

        If ViewImage Is Nothing Then ViewImage = Me.Register("ctrlRC_ImageUpload", "Recruitment", "ctrlRC_ImageUpload", "Business")
        ViewPlaceHoderImage.Controls.Add(ViewImage)
        CurrentView.SetProperty("ImageFile", ViewImage.GetProperty("ImageFile"))
    End Sub

    Public Sub AccessPanelBar()
        Try
            Dim i As Integer = 0
            ' lặp item của RadPanelBar
            While (i < rpbRecruitment.Items(0).Items.Count - 1)
                ' lấy item của đang lặp
                Dim itm As RadPanelItem = rpbRecruitment.Items(0).Items(i)
                Using rep As New CommonRepository
                    ' lấy thông tin user đang đăng nhập
                    Dim user = LogHelper.CurrentUser
                    ' check xem có phải admin không
                    Dim GroupAdmin As Boolean = rep.CheckGroupAdmin(Utilities.GetUsername)
                    ' nếu không phải admin
                    If GroupAdmin = False Then
                        ' lấy quyền của user
                        Dim permissions As List(Of PermissionDTO) = rep.GetUserPermissions(Utilities.GetUsername)
                        ' nếu có quyền
                        If permissions IsNot Nothing Then
                            ' kiểm tra các chức năng ngoại trừ chức năng là báo cáo
                            Dim isPermissions = (From p In permissions Where p.FID = itm.Value And p.IS_REPORT = False).Any
                            ' nếu không tồn tại --> xóa item
                            If Not isPermissions Then
                                rpbRecruitment.Items(0).Items.RemoveAt(i)
                                Continue While
                            End If
                        End If
                    Else
                        ' nếu là admin + có quyền
                        If user.MODULE_ADMIN = "*" OrElse (user.MODULE_ADMIN IsNot Nothing AndAlso user.MODULE_ADMIN.Contains(Me.ModuleName)) Then
                        Else
                            ' xóa nếu admin không có quyền
                            rpbRecruitment.Items(0).Items.RemoveAt(i)
                            Continue While
                        End If
                    End If
                End Using
                i = i + 1
            End While
            ' làm tưởng tự tabstrip
        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ProgramID = Request.Params("PROGRAM_ID")
            If Not IsPostBack Then
                Dim strMessage As String = Request.QueryString("message")
                If strMessage IsNot Nothing AndAlso strMessage.ToUpper = "SUCCESS" Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                End If
                CandidateCode = Request.QueryString("Can")
                Dim strCurrentTabView As String = Request.QueryString("tab")
                If strCurrentTabView Is Nothing Then
                    strCurrentTabView = ""
                End If
                Dim State As String = Request.QueryString("state")
                If CandidateCode IsNot Nothing AndAlso CandidateCode.Trim <> "" Then
                    hidCandidateCode.Value = CandidateCode

                    Dim rep As New RecruitmentRepository
                    If State = CommonMessage.STATE_EDIT OrElse State = CommonMessage.STATE_NORMAL Then
                        If CandidateInfo Is Nothing Then
                            CandidateInfo = rep.GetCandidateInfo(CandidateCode) 'Lưu vào viewStates để truyền vào các view con.
                        End If

                        CurrentView.SetProperty("CandidateInfo", CandidateInfo)

                        If CandidateInfo IsNot Nothing And State = CommonMessage.STATE_EDIT Then
                            If (CandidateInfo.STATUS_ID <> RecruitmentCommon.RC_CANDIDATE_STATUS.LANHANVIEN_ID) Then
                                CurrentState = CommonMessage.STATE_EDIT
                            Else
                                ShowMessage(Translate("Ứng viên đã là nhân viên. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                                CurrentState = CommonMessage.STATE_NORMAL
                            End If
                        End If

                    End If
                End If

                CurrentState = IIf(State <> "", State, CommonMessage.STATE_NORMAL)
                CurrentView.CurrentState = CurrentState
                CurrentView.DataBind()
                Me.DataBind()
                ViewImage.SetProperty("CandidateInfo", CandidateInfo)
            End If
            UpdateControlState()
            CurrentView.UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()

        If CurrentState = CommonMessage.STATE_EDIT OrElse CurrentState = CommonMessage.STATE_NEW Then
            rpbRecruitment.Enabled = False
        Else
            rpbRecruitment.Enabled = True
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
    End Sub

    'Lấy thông tin cơ bản của nhân viên.
    Private Sub CheckCandidateStatus(ByVal strEmpCode As String, ByRef State As String)


    End Sub

    Public Sub ctrlRC_CanDtl_OnReceiveData(ByVal sender As ViewBase, ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Try
            Select Case e.EventData
                Case STATE_NEW
                    CurrentState = e.EventData
                    CandidateInfo = Nothing
                Case STATE_EDIT, STATE_NORMAL
                    CurrentState = e.EventData
                Case Else
            End Select
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


End Class