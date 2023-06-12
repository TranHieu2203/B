Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_CandidateTransferList2

    Inherits Common.CommonView
    Private Property psp As New RecruitmentRepository
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
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            hidProgramID.Value = Request.Params("PROGRAM_ID")
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgCandidateList.AllowCustomPaging = True
            rgCandidateList.PageSize = Common.Common.DefaultPageSize
            rgCandidateList.SetFilter()
            rgResult.AllowCustomPaging = True
            rgResult.PageSize = Common.Common.DefaultPageSize
            rgResult.SetFilter()
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me

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
                Dim rep As New RecruitmentRepository
                Dim objPro = rep.GetProgramByID(New ProgramDTO With {.ID = Decimal.Parse(hidProgramID.Value)})

                hidOrg.Value = objPro.ORG_ID

                hidTitle.Value = objPro.TITLE_ID

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    'Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
    '    Try
    '        Me.sender = "btnSearch"
    '        rgCandidateList.CurrentPageIndex = 0
    '        rgCandidateList.Rebind()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim objCandidate As New CandidateDTO
            Dim rep As New RecruitmentRepository

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_EXPORT

                    Using xls As New ExcelCommon
                        Dim dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgCandidateList.ExportExcel(Server, Response, dtData, "CandidateList")
                        End If
                    End Using

            End Select
            'UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub





    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgCandidateList.NeedDataSource
        Try
            If hidProgramID.Value <> "" Then
                'rgCandidateList.DataSource = psp.GET_LIST_EMPLOYEE_ELECT(Decimal.Parse(hidProgramID.Value))
            End If
            'CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub rgResult_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgResult.NeedDataSource
        Try
            If rgCandidateList.SelectedValues IsNot Nothing Then
                rgResult.DataSource = Nothing
                Dim idProGram = rgCandidateList.SelectedValues.Item("ID")
                Dim Candidate_CODE = rgCandidateList.SelectedValues.Item("CANDIDATE_CODE")
                rgResult.DataSource = psp.GET_LIST_EMPLOYEE_EXAMS(idProGram, Candidate_CODE)
            Else
                rgResult.DataSource = psp.GET_LIST_EMPLOYEE_EXAMS(3, "000005")
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    'Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCandidateList.PageIndexChanged
    '    Try
    '        CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    Private Sub rgCandidateList_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgCandidateList.SelectedIndexChanged
        Try
            'If rgCandidateList.SelectedValues IsNot Nothing Then
            '    Dim idProGram = rgCandidateList.SelectedValues.Item("ID")
            '    Dim Candidate_CODE = rgCandidateList.SelectedValues.Item("CANDIDATE_CODE")
            '    rgResult.DataSource = psp.GET_LIST_EMPLOYEE_EXAMS(idProGram, Candidate_CODE)
            '    rgResult.DataBind()
            'End If
            rgResult.Rebind()
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
            If hidProgramID.Value <> "" Then
                _filter.RC_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)

            End If
            Dim MaximumRows As Integer

            Dim Sorts As String = rgCandidateList.MasterTableView.SortExpressions.GetSortString()

            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.CandidateList = rep.GetListCandidateTransferPaging(rgCandidateList.CurrentPageIndex, _
                                                                rgCandidateList.PageSize, _
                                                                MaximumRows, _
                                                                _filter, _
                                                                Sorts)
                Else
                    Me.CandidateList = rep.GetListCandidateTransferPaging(rgCandidateList.CurrentPageIndex, _
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