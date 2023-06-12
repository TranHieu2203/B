Imports Common
Imports Framework.UI
Imports HistaffFrameworkPublic
'Imports Common.CommonBusiness
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlFindProgramPopupView
    Inherits CommonView
    Private Property psp As New RecruitmentRepository
    Delegate Sub OrgTitleSelectedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Event OrgTitleSelected As OrgTitleSelectedDelegate
    Delegate Sub CancelClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CancelClicked As CancelClick
    Public Overrides Property MustAuthorize As Boolean = False
    Public Property CurrentValue As String
    Public Property IDSelect As String
#Region "Property"
    Property SelectOrgFunction As String
        Get
            Return PageViewState(Me.ID & "_SelectOrgFunction")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_SelectOrgFunction") = value
        End Set
    End Property

    Public Property MaximumRows As Integer
        Get
            If PageViewState(Me.ID & "_MaximumRows") Is Nothing Then
                Return 0
            End If
            Return PageViewState(Me.ID & "_MaximumRows")
        End Get
        Set(ByVal value As Integer)
            PageViewState(Me.ID & "_MaximumRows") = value
        End Set
    End Property
    Public Property PageSize As Integer
        Get
            If PageViewState(Me.ID & "_PageSize") Is Nothing Then
                Return rgOrgTitle.PageSize
            End If
            Return PageViewState(Me.ID & "_PageSize")
        End Get
        Set(ByVal value As Integer)
            PageViewState(Me.ID & "_PageSize") = value
        End Set
    End Property
    Public Property SelectedItem As List(Of String)
        Get
            If PageViewState(Me.ID & "_SelectedItem") Is Nothing Then
                PageViewState(Me.ID & "_SelectedItem") = New List(Of String)
            End If
            Return PageViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of String))
            PageViewState(Me.ID & "_SelectedItem") = value
        End Set
    End Property
    Public Property LoadAllOrganization As Boolean
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgOrgTitle.AllowCustomPaging = True
            rgOrgTitle.PageSize = 10
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            CurrentValue = If(Request.Params("CurrentValue") Is Nothing, "", Request.Params("Enabled"))
            LoadAllOrganization = If(Request.Params("LoadAllOrganization") = "0", False, True)
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrg.LoadAllOrganization = LoadAllOrganization
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New CommonRepository
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub CreateDataFilter()
        Dim _filter As New ProgramDTO
        Dim rep As New RecruitmentRepository
        Dim bCheck As Boolean = False
        Try

            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgOrgTitle.DataSource = New List(Of ProgramDTO)
                Exit Sub
            End If
            _filter.STATUS_ID = "4060"

            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), _
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve}


            Dim MaximumRows As Integer
            Dim Sorts As String = rgOrgTitle.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of ProgramDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetProgramSearch(_filter, rgOrgTitle.CurrentPageIndex, rgOrgTitle.PageSize, MaximumRows, _param, Sorts)
            Else
                lstData = rep.GetProgramSearch(_filter, rgOrgTitle.CurrentPageIndex, rgOrgTitle.PageSize, MaximumRows, _param)
            End If


            rgOrgTitle.VirtualItemCount = MaximumRows
            rgOrgTitle.DataSource = lstData
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

    Private Sub rgOrgTitle_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgOrgTitle.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try
            SelectOrgFunction = ctrlOrg.CurrentValue
            CurrentValue = ctrlOrg.CurrentValue
            rgOrgTitle.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnYES_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYES.Click
        'anhvn
        If rgOrgTitle.SelectedItems.Count > 1 Then
            ShowMessage("Vui lòng chỉ chọn 1 chức danh !", Utilities.NotifyType.Warning)
            Exit Sub
        End If
        '-----
        GetOrgTitleSelected()
        hidSelected.Value = ""
        Dim ID_PROGRAM As Integer = 0



        For Each sId As String In SelectedItem
            hidSelected.Value &= ";" & sId
        Next
        If hidSelected.Value <> "" Then
            hidSelected.Value = hidSelected.Value.Substring(1)
        End If
        For Each dr As GridDataItem In rgOrgTitle.SelectedItems
            ID_PROGRAM = dr.GetDataKeyValue("ID")
            Exit For
        Next

        'XU LY THAY DOI THONG TIN TUYEN DUNG CHO UNG VIEN 
        '@P_ID_CANDIDATE 
        '@ID_PROGRAM 
        Dim P_RET As Integer = 0
        Dim rep As New HistaffFrameworkRepository
        Dim P_PROGRAM_ID_OLD = Request.Params("P_PROGRAM_ID_OLD") ' Decimal.Parse(Session("P_PROGRAM_ID_OLD"))
        Dim ID_CANDIDATE = Request.Params("ID_CANDIDATE")
        Dim ID_PROGRAM_CANDIDATE = Request.Params("ID_PROGRAM_CANDIDATE")
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_COMMON_LIST.INS_CHANGE_REQUEST_PROGRAM", New List(Of Object)(New Object() {ID_CANDIDATE, ID_PROGRAM, "OUT_NUMBER"}))
        Dim obj_program_can As Object = rep.ExecuteStoreScalar("PKG_COMMON_LIST.INS_CHANGE_REQUEST_PROGRAM_CANDIDATE", New List(Of Object)(New Object() {ID_CANDIDATE, ID_PROGRAM_CANDIDATE, P_PROGRAM_ID_OLD, ID_PROGRAM, "OUT_NUMBER"}))
        Dim strJS As String
        If Int32.Parse(obj(0).ToString()) = 0 Then
            strJS = "btnYesClick(1);"
        Else
            strJS = "btnYesClick(0);"
        End If
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", strJS, True)
        RaiseEvent OrgTitleSelected(sender, e)
        Session.Remove("CallAllOrg")
    End Sub

#Region "Custom"
    Private Sub GetOrgTitleSelected()
        If rgOrgTitle.SelectedItems.Count > 1 Then
            ShowMessage("Vui vòng chỉ chọn 1 chức danh !", Utilities.NotifyType.Warning)
            Exit Sub
        End If
        For Each dr As Telerik.Web.UI.GridDataItem In rgOrgTitle.Items
            Dim id As String = dr("ID").Text.ToUpper
            If dr.Selected Then
                IDSelect = id
                If Not SelectedItem.Contains(id) Then SelectedItem.Add(id)
            Else
                If SelectedItem.Contains(id) Then SelectedItem.Remove(id)
            End If
        Next
    End Sub
#End Region

    Private Sub btnNO_Click(sender As Object, e As System.EventArgs) Handles btnNO.Click
        Session.Remove("CallAllOrg")
    End Sub
End Class