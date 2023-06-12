Imports System.IO
Imports Common.CommonMessage
Imports Recruitment.RecruitmentBusiness
Public Class ctrlRC_ImageUpload
    Inherits Common.CommonView

#Region "Properties"
    ''' <summary>
    ''' CandidateInfo truyền từ view Cha vào.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CandidateInfo As CandidateDTO
        Get
            Return PageViewState(Me.ID & "_CandidateInfo")
        End Get
        Set(ByVal value As CandidateDTO)
            PageViewState(Me.ID & "_CandidateInfo") = value
        End Set
    End Property

    Property ImageFile As Telerik.Web.UI.UploadedFile
        Get
            Return PageViewState(Me.ID & "_ImageFile")
        End Get
        Set(ByVal value As Telerik.Web.UI.UploadedFile)
            PageViewState(Me.ID & "_ImageFile") = value
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
    Property isLoad As Boolean
        Get
            Return PageViewState(Me.ID & "_isLoad")
        End Get
        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_isLoad") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Property MustAuthorize As Boolean = False
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        UpdateControlState()
        Refresh()
    End Sub
#End Region

#Region "Event"
    Protected Sub btnSaveImage_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveImage.Click
        Try
            If _radAsynceUpload.UploadedFiles.Count = 0 Then
                Exit Sub
            End If
            Dim file = _radAsynceUpload.UploadedFiles(0)
            Dim fileContent As Byte() = New Byte(file.ContentLength) {}
            Using str As Stream = file.InputStream
                str.Read(fileContent, 0, file.ContentLength)
            End Using
            'Lưu  image vào PageViewStates , khi insert Candidate vào Database mới lưu image này lên folder trên server.
            ImageFile = file
            'Hiển thị ảnh mới.
            rbiCandidateImage.DataValue = fileContent
            Me.Send("UpdatedImage")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
#End Region

#Region "Custom"
    Public Overrides Sub UpdateControlState()
        If CurrentState = STATE_NORMAL Then
            _radAsynceUpload.Enabled = False
        ElseIf CurrentState = STATE_NEW Then
            _radAsynceUpload.Enabled = False
            If Me.AllowCreate Then
                _radAsynceUpload.Enabled = True
            End If
        Else
            _radAsynceUpload.Enabled = False
            If Me.AllowModify Then
                _radAsynceUpload.Enabled = True
            End If
        End If
        If LoadDefaultImage Then
            CandidateInfo = Nothing
            DisplayImage()
        End If
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not isLoad Then
                DisplayImage()
                isLoad = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub DisplayImage()
        Try
            Dim rep As New RecruitmentRepository
            Dim sError As String = ""
            If CandidateInfo IsNot Nothing Then
                If CandidateInfo.IMAGE IsNot Nothing Then
                    rbiCandidateImage.DataValue = rep.GetCandidateImage(CandidateInfo.ID, sError) 'Lấy ảnh của nhân viên.
                Else
                    rbiCandidateImage.DataValue = rep.GetCandidateImage(0, sError) 'Lấy ảnh mặc định (NoImage.jpg)
                End If
            Else
                rbiCandidateImage.DataValue = rep.GetCandidateImage(0, sError) 'Lấy ảnh mặc định (NoImage.jpg)
            End If
            Exit Sub
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region
End Class