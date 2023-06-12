Imports Common.CommonBusiness
Imports Telerik.Web.UI

Public Class ctrlFindEmployeePopupDialog
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
    Public Property is_All As Boolean
    Public Property IS_DM As Boolean
    Public Property IsHideTerminate As Boolean
    Public Property IsShowKiemNhiem As Boolean
    Public Property IsOnlyWorkingWithoutTer As Boolean
    Public Property IS_3B As String
    Public Property Enabled As Boolean
    Public Property CurrentValue As String
    Public Property MultiSelect As Boolean
    Public Property popupId As String
    Public WithEvents AjaxManager As RadAjaxManager
    Public AjaxLoading As RadAjaxLoadingPanel
    Public Property AjaxManagerId As String
    Public Property AjaxLoadingId As String
    Public Property EMP_CODE_OR_NAME As String
    Public Property FunctionName As String
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(e As System.EventArgs)
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
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            MustHaveContract = If(Request.Params("MustHaveContract") Is Nothing, True, Request.Params("MustHaveContract"))
            is_All = If(Request.Params("is_All") Is Nothing, True, Request.Params("is_All"))
            IS_DM = If(Request.Params("IS_DM") Is Nothing, False, Request.Params("IS_DM"))
            CurrentValue = If(Request.Params("CurrentValue") Is Nothing, "", Request.Params("Enabled"))
            MultiSelect = If(Request.Params("MultiSelect") Is Nothing, True, Request.Params("MultiSelect"))
            LoadAllOrganization = If(Request.Params("LoadAllOrganization") Is Nothing, False, Request.Params("LoadAllOrganization"))
            IsHideTerminate = If(Request.Params("IsHideTerminate") Is Nothing, False, Request.Params("IsHideTerminate"))
            'Update By : Tran Ngoc Hung
            'Update Date : 12/12/2022
            'Description : BCG-874
            IsShowKiemNhiem = If(Request.Params("IsShowKiemNhiem") Is Nothing, False, Request.Params("IsShowKiemNhiem"))
            If IsShowKiemNhiem Then
                chk_kiemnhiem.Visible = True
            Else
                chk_kiemnhiem.Visible = False
            End If
            IsOnlyWorkingWithoutTer = If(Request.Params("IsOnlyWorkingWithoutTer") Is Nothing, False, Request.Params("IsOnlyWorkingWithoutTer"))
            IS_3B = If(Request.Params("IS_3B") Is Nothing, 0, Request.Params("IS_3B"))
            EMP_CODE_OR_NAME = If(Request.Params("EMP_CODE_OR_NAME") Is Nothing, "", Request.Params("EMP_CODE_OR_NAME"))
            FunctionName = If(Request.Params("FunctionName") Is Nothing, "", Request.Params("FunctionName"))
            'If IsHideTerminate Then
            'cbTerminate.Visible = False
            'End If
            rgEmployeeInfo.AllowMultiRowSelection = MultiSelect
            ctrlOrganization.AutoPostBack = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckChildNodes = True
            ctrlOrganization.CurrentValue = CurrentValue
            ctrlOrganization.LoadAllOrganization = LoadAllOrganization
            If EMP_CODE_OR_NAME IsNot Nothing Then
                txtEmployee_Code.Text = EMP_CODE_OR_NAME
            End If
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
            Dim _filter As New EmployeePopupFindListDTO
            Dim Sorts As String
            'Dim bShowTerminate As Boolean
            Dim orgID As Decimal? = 0
            Dim EmployeeList As List(Of EmployeePopupFindListDTO)
            If ctrlOrganization.CurrentValue <> "" Then
                orgID = Decimal.Parse(ctrlOrganization.CurrentValue)
            End If
            Dim _param = New ParamDTO With {.ORG_ID = orgID, _
                                               .IS_DISSOLVE = ctrlOrganization.IsDissolve}


            _filter.EMPLOYEE_CODE = txtEmployee_Code.Text
            _filter.IsOnlyWorkingWithoutTer = IsOnlyWorkingWithoutTer
            _filter.IS_3B = IS_3B
            _filter.IS_TER = cbTerminate.Checked
            _filter.IS_SHOW_KIEMNHIEM = IsShowKiemNhiem
            _filter.IS_KIEMNHIEM = chk_kiemnhiem.Checked
            _filter.LoadAllOrganization = LoadAllOrganization
            _filter.MustHaveContract = MustHaveContract
            _filter.is_All = is_All
            _filter.DM_ID = IS_DM
            _filter.FUNCTION_NAME = FunctionName


            Sorts = rgEmployeeInfo.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                EmployeeList = rep.GetEmployeeToPopupFind(_filter, rgEmployeeInfo.CurrentPageIndex, PageSize,
                                                          MaximumRows, Sorts, _param)
            Else
                EmployeeList = rep.GetEmployeeToPopupFind(_filter, rgEmployeeInfo.CurrentPageIndex, PageSize,
                                                          MaximumRows, "EMPLOYEE_CODE asc", _param)
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


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GetEmployeeSelected()
        rgEmployeeInfo.CurrentPageIndex = 0
        rgEmployeeInfo.Rebind()
    End Sub

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
            If dr.Selected Then
                If Not SelectedItem.Contains(id) Then SelectedItem.Add(id)
            Else
                If SelectedItem.Contains(id) Then SelectedItem.Remove(id)
            End If
        Next
    End Sub

#End Region

    Private Sub chk_kiemnhiem_CheckedChanged(sender As Object, e As System.EventArgs) Handles chk_kiemnhiem.CheckedChanged
        Try
            rgEmployeeInfo.Rebind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cbTerminate_CheckedChanged(sender As Object, e As System.EventArgs) Handles cbTerminate.CheckedChanged
        Try
            rgEmployeeInfo.Rebind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class