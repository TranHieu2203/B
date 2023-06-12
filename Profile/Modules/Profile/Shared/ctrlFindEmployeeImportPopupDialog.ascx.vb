Imports Common
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Public Class ctrlFindEmployeeImportPopupDialog
    Inherits CommonView

    Delegate Sub EmployeeSelectedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Event EmployeeSelected As EmployeeSelectedDelegate
    Delegate Sub CancelClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CancelClicked As CancelClick
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
    Public Property is_CheckNode As Boolean
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
    Public Property CommendDate As Date
        Get
            Return ViewState(Me.ID & "_CommendDate")
        End Get
        Set(value As Date)
            ViewState(Me.ID & "_CommendDate") = value
        End Set
    End Property
    Public Property CommendList_Code As String
        Get
            Return ViewState(Me.ID & "_CommendList_Code")
        End Get
        Set(value As String)
            ViewState(Me.ID & "_CommendList_Code") = value
        End Set
    End Property
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
            rgEmployeeInfo.PageSize = Common.Common.DefaultPageSize
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            CurrentValue = If(Request.Params("CurrentValue") Is Nothing, "", Request.Params("Enabled"))
            MultiSelect = If(Request.Params("MultiSelect") Is Nothing, True, Request.Params("MultiSelect"))
            is_CheckNode = If(Request.Params("is_CheckNode") Is Nothing, False, Request.Params("is_CheckNode"))
            rgEmployeeInfo.AllowMultiRowSelection = MultiSelect
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Overrides Sub BindData()
        Try
            Dim psp As New ProfileStoreProcedure

            Dim data As New DataTable
            data = psp.Get_Commend_Title(True, 388)

            FillRadCombobox(cbCommenList, data, "NAME", "CODE", False)
        Catch ex As Exception
            Throw
        End Try

    End Sub
#End Region

#Region "Event"
    Private Sub rgEmployeeInfo_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployeeInfo.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            'datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
            Dim id = datarow.GetDataKeyValue("EMPLOYEE_ID")
            If SelectedItem IsNot Nothing AndAlso SelectedItem.Contains(id) Then
                datarow.Selected = True
            End If
        End If
    End Sub
    Protected Sub rgEmployeeInfo_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEmployeeInfo.NeedDataSource
        Try
            Dim rep As New ProfileBusinessRepository
            Dim listEmpImport As New List(Of ImportCommendDTO)
            Dim importCommend As New ImportCommendDTO

            importCommend.COMMEND_OBJ = 388

            If rdCommendDate.SelectedDate IsNot Nothing Then
                importCommend.COMMEND_DATE = rdCommendDate.SelectedDate
            End If

            If cbCommenList.SelectedValue <> "" Then
                importCommend.FIELD_DATA_CODE = cbCommenList.SelectedValue
            End If

            If txtEmployee_Code.Text <> "" Then
                importCommend.EMPLOYEE_CODE = txtEmployee_Code.Text
            End If

            listEmpImport = rep.GetImportCommend(importCommend)
            rep.Dispose()
            rgEmployeeInfo.DataSource = listEmpImport
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        SelectedItem = Nothing
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
        SelectedItem = New List(Of String)
        For Each dr As Telerik.Web.UI.GridDataItem In rgEmployeeInfo.Items
            Dim id As String = dr.GetDataKeyValue("EMPLOYEE_ID")
            If dr.Selected Then
                SelectedItem.Add(id)
            End If
        Next
    End Sub

#End Region

End Class