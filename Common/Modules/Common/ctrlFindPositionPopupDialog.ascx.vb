Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlFindPositionPopupDialog
    Inherits CommonView

    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"



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
                Return rgEmployeeInfo.PageSize
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
    Public Property MustHaveContract As Boolean
    Public Property IsHideTerminate As Boolean
    Public Property IsOnlyWorkingWithoutTer As Boolean
    Public Property IS_3B As String
    Public Property IS_COMMITEE As String
    Public Property Enabled As Boolean
    Public Property CurrentValue As String
    Public Property MultiSelect As Boolean
    Public Property popupId As String
    Public WithEvents AjaxManager As RadAjaxManager
    Public AjaxLoading As RadAjaxLoadingPanel
    Public Property AjaxManagerId As String
    Public Property AjaxLoadingId As String
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            Dim popup As RadWindow
            popup = CType(Me.Page, AjaxPage).PopupWindow

            popup.Height = 400
            popup.Width = 500
            popupId = popup.ClientID
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxLoading = CType(Me.Page, AjaxPage).AjaxLoading
            AjaxManagerId = AjaxManager.ClientID
            AjaxLoadingId = AjaxLoading.ClientID

            rgEmployeeInfo.AllowCustomPaging = True
            rgEmployeeInfo.SetFilter()
            ctrlOrganization.IsOM = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            MustHaveContract = If(Request.Params("MustHaveContract") Is Nothing, True, Request.Params("MustHaveContract"))
            CurrentValue = If(Request.Params("CurrentValue") Is Nothing, "", Request.Params("Enabled"))
            MultiSelect = If(Request.Params("MultiSelect") Is Nothing, True, Request.Params("MultiSelect"))
            LoadAllOrganization = If(Request.Params("LoadAllOrganization") Is Nothing, False, Request.Params("LoadAllOrganization"))
            IsHideTerminate = If(Request.Params("IsHideTerminate") Is Nothing, False, Request.Params("IsHideTerminate"))
            IsOnlyWorkingWithoutTer = If(Request.Params("IsOnlyWorkingWithoutTer") Is Nothing, False, Request.Params("IsOnlyWorkingWithoutTer"))
            IS_3B = If(Request.Params("IS_3B") Is Nothing, 0, Request.Params("IS_3B"))
            IS_COMMITEE = If(Request.Params("IS_COMMITEE") Is Nothing, 0, Request.Params("IS_COMMITEE"))
            'If IsHideTerminate Then
            '    cbTerminate.Visible = False
            'End If
            rgEmployeeInfo.AllowMultiRowSelection = MultiSelect
            ctrlOrganization.AutoPostBack = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckChildNodes = True
            ctrlOrganization.CurrentValue = CurrentValue
            ctrlOrganization.LoadAllOrganization = LoadAllOrganization
            ctrlOrganization.ShowCommitee_static_check = IS_COMMITEE
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        SelectedItem = Nothing
        rgEmployeeInfo.CurrentPageIndex = 0
        rgEmployeeInfo.Rebind()
    End Sub

    'Private Sub rgEmployeeInfo_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployeeInfo.ItemDataBound
    '    If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '        Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
    '        datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
    '        Dim id = datarow.GetDataKeyValue("ID")
    '        If SelectedItem IsNot Nothing AndAlso SelectedItem.Contains(id) Then
    '            datarow.Selected = True
    '        End If
    '    End If
    'End Sub

    Private Sub rgEmployeeInfo_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgEmployeeInfo.PageIndexChanged

        GetEmployeeSelected()

    End Sub

    Protected Sub rgEmployeeInfo_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEmployeeInfo.NeedDataSource
        Try
            Dim rep As New CommonRepository

            Dim _filter As New PositionPopupFindListDTO
            Dim Sorts As String
            Dim bShowTerminate As Boolean
            Dim orgID As Decimal? = 0
            Dim EmployeeList As List(Of PositionPopupFindListDTO)
            If ctrlOrganization.CurrentValue <> "" Then
                orgID = Decimal.Parse(ctrlOrganization.CurrentValue)
            End If
            Dim _param = New ParamDTO With {.ORG_ID = orgID,
                                               .IS_DISSOLVE = ctrlOrganization.IsDissolve}
            _filter.IS_UYBAN = IS_COMMITEE
            SetValueObjectByRadGrid(rgEmployeeInfo, _filter)
            Sorts = rgEmployeeInfo.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                EmployeeList = rep.GetPositionToPopupFind(_filter, rgEmployeeInfo.CurrentPageIndex, PageSize,
                                                          MaximumRows, Sorts, _param)
                'EmployeeList = rep.GetEmployeeToPopupFind(_filter, rgEmployeeInfo.CurrentPageIndex, PageSize,
                '                                          MaximumRows, Sorts, _param)
            Else
                EmployeeList = rep.GetPositionToPopupFind(_filter, rgEmployeeInfo.CurrentPageIndex, PageSize,
                                                          MaximumRows, "ID asc", _param)
            End If

            rgEmployeeInfo.VirtualItemCount = MaximumRows

            rgEmployeeInfo.DataSource = EmployeeList
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub rgEmployeeInfo_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles rgEmployeeInfo.SortCommand

        GetEmployeeSelected()

    End Sub


    'Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
    '    GetEmployeeSelected()
    '    rgEmployeeInfo.CurrentPageIndex = 0
    '    rgEmployeeInfo.Rebind()
    'End Sub

    Private Sub btnYES_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYES.Click
        GetEmployeeSelected()
        hidSelected.Value = ""
        For Each sId As String In SelectedItem
            hidSelected.Value &= ";" & sId
        Next
        If hidSelected.Value <> "" Then
            hidSelected.Value = hidSelected.Value.Substring(1)
        End If
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "btnYesClick();", True)
    End Sub

#End Region

#Region "Custom"

    Private Sub GetEmployeeSelected()
        For Each dr As Telerik.Web.UI.GridDataItem In rgEmployeeInfo.Items
            Dim id As String = dr.GetDataKeyValue("ID")
            Dim name As String = dr.GetDataKeyValue("FULLNAME_VN")
            Dim code As String = dr.GetDataKeyValue("POSITION_CODE")
            Dim EMP_LM As String = dr.GetDataKeyValue("EMP_LM")
            Dim EMP_CSM As String = dr.GetDataKeyValue("EMP_CSM")
            If SelectedItem Is Nothing Then
                SelectedItem = New List(Of String)
            End If
            If dr.Selected Then
                If Not SelectedItem.Contains(id) Then SelectedItem.Add(id)
                If Not SelectedItem.Contains(name) Then SelectedItem.Add(name)
                If Not SelectedItem.Contains(code) Then SelectedItem.Add(code)
                If Not SelectedItem.Contains(EMP_LM) Then SelectedItem.Add(EMP_LM)
                If Not SelectedItem.Contains(EMP_CSM) Then SelectedItem.Add(EMP_CSM)
                'Else
                '    If SelectedItem.Contains(id) Then SelectedItem.Remove(id)
                '    If SelectedItem.Contains(name) Then SelectedItem.Remove(name)
            End If
        Next

        'If rgEmployeeInfo.Items.Count > 0 Then
        '    Dim dr As Telerik.Web.UI.GridDataItem = rgEmployeeInfo.Items(0)
        '    Dim id As String = dr.GetDataKeyValue("ID")
        '    Dim name As String = dr.GetDataKeyValue("FULLNAME_VN")
        '    If dr.Selected Then
        '        If Not SelectedItem.Contains(id) Then
        '            SelectedItem.Add(id)
        '            SelectedItem.Add(name)
        '        End If

        '    Else
        '        If SelectedItem.Contains(id) Then
        '            SelectedItem.Remove(id)
        '            SelectedItem.Remove(name)
        '        End If
        '    End If

        'End If

    End Sub

#End Region

End Class