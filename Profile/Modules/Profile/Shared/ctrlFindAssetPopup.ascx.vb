Imports Common
Imports Framework.UI
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlFindAssetPopup
    Inherits CommonView

    Delegate Sub AssetDeclareSelectedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Event AssetDeclareSelected As AssetDeclareSelectedDelegate
    Delegate Sub CancelClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CancelClicked As CancelClick

#Region "Property"

    Public Overrides Property MustAuthorize As Boolean = False

    Private Property Opened As Boolean
        Get
            Return ViewState(Me.ID & "_Opened")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_Opened") = value
        End Set
    End Property

    Private Property AssetList As List(Of AssetDTO)
        Get
            Return ViewState(Me.ID & "_AssetList")
        End Get
        Set(ByVal value As List(Of AssetDTO))
            ViewState(Me.ID & "_AssetList") = value
        End Set
    End Property
    Public Property MultiSelect() As Boolean
        Get
            Return rgAssetDeclare.AllowMultiRowSelection
        End Get
        Set(ByVal value As Boolean)
            rgAssetDeclare.AllowMultiRowSelection = value
        End Set
    End Property

    Public ReadOnly Property SelectedAssetDeclare() As List(Of AssetDTO)
        Get
            Dim lst As New List(Of AssetDTO)
            For Each dr As Telerik.Web.UI.GridDataItem In rgAssetDeclare.SelectedItems
                Dim _new As New AssetDTO
                _new.ID = Decimal.Parse(dr("ID").Text)
                _new.CODE = dr.GetDataKeyValue("CODE")
                _new.NAME = dr("NAME").Text
                _new.GROUP_NAME = dr("GROUP_NAME").Text
                lst.Add(_new)
            Next
            Return lst
        End Get
    End Property

#End Region

#Region "Page"


    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            RadAjaxPanel1.LoadingPanelID = CType(Me.Page, AjaxPage).AjaxLoading.ID
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            rgAssetDeclare.AllowCustomPaging = True
            If Opened Then
                rwMessage.VisibleOnPageLoad = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Show()
        Opened = True
        rwMessage.VisibleOnPageLoad = True
        rwMessage.Visible = True
    End Sub

    Public Sub Hide()
        Opened = False
        rwMessage.VisibleOnPageLoad = False
        rwMessage.Visible = False
    End Sub

    Protected Sub CreateDataFilter()
        Dim _filter As New AssetDTO
        Dim rep As New ProfileRepository

        Try
            Dim MaximumRows As Integer
            Dim Sorts As String = rgAssetDeclare.MasterTableView.SortExpressions.GetSortString()
            _filter.ACTFLG2 = "A"
            If Sorts IsNot Nothing Then
                Me.AssetList = rep.GetAsset(_filter, rgAssetDeclare.CurrentPageIndex, rgAssetDeclare.PageSize, MaximumRows, Sorts)
            Else
                Me.AssetList = rep.GetAsset(_filter, rgAssetDeclare.CurrentPageIndex, rgAssetDeclare.PageSize, MaximumRows)
            End If
            rep.Dispose()
            rgAssetDeclare.VirtualItemCount = MaximumRows
            rgAssetDeclare.DataSource = Me.AssetList

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgAssetDeclare.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Event"

    Private Sub btnYES_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYES.Click
        If rgAssetDeclare.SelectedItems.Count = 0 Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), Utilities.NotifyType.Warning)
            Exit Sub
        End If
        'Dim asset = (From p In AssetDeclareList Where p.ID = rgAssetDeclare.SelectedValue).FirstOrDefault
        'If asset.QUANTITY_LEFT <= 0 Then
        '    ShowMessage(Translate("Số lượng tài sản trong kho không đủ để cấp phát"), Utilities.NotifyType.Warning)
        '    Exit Sub
        'End If
        RaiseEvent AssetDeclareSelected(sender, e)
        Hide()
    End Sub


    Private Sub btnNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNO.Click
        RaiseEvent CancelClicked(sender, e)
        Hide()
    End Sub

    Private Sub rgAssetDeclare_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgAssetDeclare.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


#End Region

End Class