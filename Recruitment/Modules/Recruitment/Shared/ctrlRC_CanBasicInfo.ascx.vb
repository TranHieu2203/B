Imports Common
Imports Framework.UI
Imports Recruitment.RecruitmentBusiness
Public Class ctrlRC_CanBasicInfo
    Inherits CommonView
    Dim CandidateCode As String
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Properties"
    Property CandidateInfo As CandidateDTO
        Get
            Return PageViewState(Me.ID & "_CandidateInfo")
        End Get
        Set(ByVal value As CandidateDTO)
            PageViewState(Me.ID & "_CandidateInfo") = value
        End Set
    End Property

    Public Property CurrentPlaceHolder As String
        Get
            Return PageViewState(Me.ID & "_CurrentPlaceHolder")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_CurrentPlaceHolder") = value
        End Set
    End Property

#End Region
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        LoadCandidateInfo()
        UpdateControlState()
    End Sub

    ''' <summary>
    ''' Lấy thông tin chi tiết của nhân viên từ CandidateCode
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadCandidateInfo()
        Try
            Dim rep As New RecruitmentRepository
            Dim strError As String = ""

            If CandidateInfo Is Nothing Then 'Lấy property set ở view cha, nếu ko có thì query để lấy.
                Dim strCanCode = Request.QueryString("Can") & ""
                If strCanCode <> "" Then
                    CandidateInfo = rep.GetCandidateInfo(strCanCode)
                End If
            End If
            If CurrentPlaceHolder Is Nothing Then
                CurrentPlaceHolder = Request.QueryString("Place") & ""
            End If
            If CandidateInfo IsNot Nothing Then
                txtCandidateCODE1.Text = CandidateInfo.CANDIDATE_CODE
                txtCandidateName.Text = CandidateInfo.FULLNAME_VN
                hidID.Value = CandidateInfo.ID.ToString()
                txtCandidateCODE1.ReadOnly = True
                txtCandidateName.ReadOnly = True

            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub btnSearchCan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchCan1.Click
        Dim rep As New RecruitmentRepository
        Dim strCanCode = txtCandidateCODE1.Text
        If rep.CheckExistCandidate(strCanCode) Then
            If CurrentPlaceHolder = "" Then
                CurrentPlaceHolder = Me.ViewName
            End If
            Page.Response.Redirect("Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&Can=" & HttpContext.Current.Server.UrlEncode(strCanCode) & "&state=Normal&Place=" & HttpContext.Current.Server.UrlEncode(CurrentPlaceHolder) & "&noscroll=1&reload=1")
        Else
            ShowMessage(Translate("Ứng viên không tồn tại."), Utilities.NotifyType.Error)
        End If
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            If CandidateInfo IsNot Nothing Then
                btnSearchCan1.Enabled = False
                txtCandidateCODE1.ReadOnly = False
            End If
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    txtCandidateCODE1.ReadOnly = True
                    btnSearchCan1.Enabled = False
                Case CommonMessage.STATE_EDIT
                    txtCandidateCODE1.ReadOnly = True
                    btnSearchCan1.Enabled = False
                Case Else
                    txtCandidateCODE1.ReadOnly = False
                    btnSearchCan1.Enabled = True
            End Select

        Catch ex As Exception

        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        UpdateControlState()
    End Sub
End Class