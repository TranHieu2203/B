Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_FindCandidate

    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = True

#Region "Properties"
    Private Property CandidateList As List(Of CandidateDTO)
        Get
            Return ViewState(Me.ID & "_CandidateList")
        End Get
        Set(ByVal value As List(Of CandidateDTO))
            ViewState(Me.ID & "_CandidateList") = value
        End Set
    End Property
    Private Property sender As String
        Get
            Return ViewState(Me.ID & "_sender")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_sender") = value
        End Set
    End Property
    Dim str As Integer
    Public Property _filter As CandidateDTO
        Get
            If PageViewState(Me.ID & "_filter") Is Nothing Then
                PageViewState(Me.ID & "_filter") = New CandidateDTO
            End If
            Return PageViewState(Me.ID & "_filter")
        End Get
        Set(ByVal value As CandidateDTO)
            PageViewState(Me.ID & "_filter") = value
        End Set
    End Property

    Private Property lstData As List(Of CandidateDTO)
        Get
            Return ViewState(Me.ID & "_lstData")
        End Get
        Set(ByVal value As List(Of CandidateDTO))
            ViewState(Me.ID & "_lstData") = value
        End Set
    End Property

    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    '0 - normal
    '1 - Employee
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property


#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgCandidateList.AllowCustomPaging = True
            rgCandidateList.PageSize = Common.Common.DefaultPageSize
            rgCandidateList.ClientSettings.EnablePostBackOnRowClick = False
            rgCandidateList.SetFilter()
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Public Overrides Sub UpdateControlState()
        Try

        Catch ex As Exception
            Throw ex
        End Try
        ChangeToolbarState()
    End Sub


#End Region

#Region "Event"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            Me.sender = "btnSearch"
            rgCandidateList.CurrentPageIndex = 0
            rgCandidateList.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim objCandidate As New CandidateDTO
            Dim rep As New RecruitmentRepository

            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case TOOLBARITEM_DELETE
                    'Kiểm tra các điều kiện để xóa.
                    If rgCandidateList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
            'UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgCandidateList.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCandidateList.PageIndexChanged
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Try
            Dim rep As New RecruitmentRepository

            'Mã nhân viên
            _filter.CANDIDATE_CODE = txtCandidateCode.Text.Trim()
            _filter.IS_PONTENTIAL = True
            Dim MaximumRows As Integer

            Dim Sorts As String = rgCandidateList.MasterTableView.SortExpressions.GetSortString()

            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.CandidateList = rep.GetFindCandidatePaging(rgCandidateList.CurrentPageIndex, _
                                                                rgCandidateList.PageSize, _
                                                                MaximumRows, _
                                                                _filter, _
                                                                Sorts)
                Else
                    Me.CandidateList = rep.GetFindCandidatePaging(rgCandidateList.CurrentPageIndex, _
                                                                rgCandidateList.PageSize, _
                                                                MaximumRows, _filter)
                End If

                rgCandidateList.VirtualItemCount = MaximumRows

                If CandidateList IsNot Nothing Then
                    rgCandidateList.DataSource = CandidateList
                Else
                    rgCandidateList.DataSource = New List(Of CandidateDTO)
                End If
            Else
                If Sorts IsNot Nothing Then
                    Return rep.GetListCandidate(_filter, Sorts).ToTable
                Else
                    Return rep.GetListCandidate(_filter).ToTable
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

#End Region

End Class