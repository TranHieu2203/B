Imports Common.CommonBusiness
Imports Telerik.Web.UI

Public Class ctrlFindTitlePopupDialog
    Inherits CommonView

    Delegate Sub TitleSelectedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Event TitleSelected As TitleSelectedDelegate
    Delegate Sub CancelClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CancelClicked As CancelClick
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Private Property TitleList As List(Of TitleDTO)
        Get
            Return PageViewState(Me.ID & "_TitleList")
        End Get
        Set(ByVal value As List(Of TitleDTO))
            PageViewState(Me.ID & "_TitleList") = value
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
                Return rgTitle.PageSize
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

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(e As System.EventArgs)
        Try
            rgTitle.AllowCustomPaging = True
            rgTitle.PageSize = 10
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional Message As String = "")
        Try
            Dim rep As New CommonRepository
            If Not IsPostBack Or Message = CommonMessage.ACTION_UPDATED Then
                Dim Sorts As String = rgTitle.MasterTableView.SortExpressions.GetSortString()
                Dim _filter As New TitleDTO
                _filter.CODE = rgTitle.MasterTableView.GetColumn("CODE").CurrentFilterValue
                _filter.ORG_ID_NAME = rgTitle.MasterTableView.GetColumn("ORG_ID_NAME").CurrentFilterValue
                _filter.NAME_VN = rgTitle.MasterTableView.GetColumn("NAME_VN").CurrentFilterValue
                _filter.REMARK = rgTitle.MasterTableView.GetColumn("REMARK").CurrentFilterValue
                _filter.TITLE_GROUP_NAME = rgTitle.MasterTableView.GetColumn("TITLE_GROUP_NAME").CurrentFilterValue
                If Sorts IsNot Nothing Then
                    Me.TitleList = rep.GetTitle(_filter, rgTitle.CurrentPageIndex, rgTitle.PageSize, MaximumRows, Sorts)
                Else
                    Me.TitleList = rep.GetTitle(_filter, rgTitle.CurrentPageIndex, rgTitle.PageSize, MaximumRows)
                End If
                'Đưa dữ liệu vào Grid
                rgTitle.MasterTableView.FilterExpression = ""
                rgTitle.VirtualItemCount = MaximumRows
                rgTitle.DataSource = Me.TitleList
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub rgTitle_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgTitle.NeedDataSource
        Try
            Refresh(CommonMessage.ACTION_UPDATED)
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgTitle_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgTitle.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim id = datarow("ID").Text.ToUpper
            If SelectedItem IsNot Nothing AndAlso SelectedItem.Contains(id) Then
                datarow.Selected = True
            End If
        End If
    End Sub

    Private Sub rgTitle_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgTitle.PageIndexChanged
        GetEmployeeSelected()
    End Sub

    Private Sub btnYES_Click(sender As Object, e As System.EventArgs) Handles btnYES.Click
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
        For Each dr As Telerik.Web.UI.GridDataItem In rgTitle.Items
            Dim id As String = dr("ID").Text.ToUpper
            If dr.Selected Then
                If Not SelectedItem.Contains(id) Then SelectedItem.Add(id)
            Else
                If SelectedItem.Contains(id) Then SelectedItem.Remove(id)
            End If
        Next
    End Sub
#End Region

End Class