Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Portal.PortalBusiness
Imports Telerik.Web.UI

Public Class ctrlEventManager
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase

#Region "Property"
    Property EventInfo As EventDTO
        Get
            Return PageViewState(Me.ID & "_EventInfo")
        End Get
        Set(ByVal value As EventDTO)
            PageViewState(Me.ID & "_EventInfo") = value
        End Set
    End Property

    Public Property ListEvents As List(Of EventDTO)
        Get
            Return PageViewState(Me.ID & "_ListEvents")
        End Get
        Set(ByVal value As List(Of EventDTO))
            PageViewState(Me.ID & "_ListEvents") = value
        End Set
    End Property

    Public Property DeleteItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_DeleteItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_DeleteItemList") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            LoadAllControlDefault()
            Refresh()
            UpdateViewState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
        InitControl()
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New PortalRepository
        Try
            If Not IsPostBack Then
                ListEvents = rep.GetListEventInformation()
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                If Message = CommonMessage.ACTION_SAVED Then
                    ListEvents = rep.GetListEventInformation()
                End If
            End If

            'Đưa dữ liệu vào Grid
            If Me.ListEvents IsNot Nothing Then
                rGrid.DataSource = Me.ListEvents
                rGrid.DataBind()
            Else
                rGrid.DataSource = New List(Of EventDTO)
                rGrid.DataBind()
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Seperator, _
                                       ToolbarItem.Delete,
                                       ToolbarItem.Seperator, _
                                       ToolbarItem.Active)

            ViewItem = Me.Register("ctrlEventNewEdit", "Portal", "ctrlEventNewEdit")
            ViewPlaceHolder.Controls.Add(ViewItem)
            ViewItem.DataBind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateViewState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rGrid.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstEvent As List(Of EventDTO) = rGrid.DataSource
                    CurrentState = CommonMessage.STATE_EDIT
                    EventInfo = (From p In lstEvent Where p.ID = rGrid.SelectedValue).SingleOrDefault
                    UpdateViewState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.MessageTitle = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE_TITLE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DELETED
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rGrid.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim id As Decimal = Decimal.Parse(rGrid.SelectedValue.ToString)
                    Dim rep As New PortalRepository
                    rep.ActiveEventInformation(id)
                    Refresh(CommonMessage.ACTION_SAVED)
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Sub ctrlUser_OnReceiveData(ByVal sender As IViewListener(Of ViewBase), ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Try
            If e.FromViewID = "ctrlEventNewEdit" Then
                CurrentState = CommonMessage.STATE_NORMAL
                UpdateViewState()
                If e.EventData = CommonMessage.ACTION_SUCCESS Then
                    Refresh(CommonMessage.ACTION_SAVED)
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                ElseIf e.EventData = CommonMessage.ACTION_UNSUCCESS Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New PortalRepository
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes And e.ActionName = CommonMessage.ACTION_DELETED Then
                If rep.DeleteEventInformation(DeleteItemList) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Refresh(CommonMessage.ACTION_SAVED)
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rGrid.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = e.Item
            Dim myCell As TableCell = dataItem("IS_SHOW")
            If myCell.Text.ToUpper = "TRUE" Then
                dataItem.BackColor = Drawing.Color.Orange
                dataItem.ForeColor = Drawing.Color.White
            End If
        End If
    End Sub

    Private Sub rGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rGrid.NeedDataSource
        Try
            If IsPostBack Then Exit Sub

            Dim rep As New PortalRepository
            ListEvents = rep.GetListEventInformation()
            rGrid.DataSource = ListEvents

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    Protected Sub UpdateControlStatus()
        Select Case CurrentState
            Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                MainToolBar.Enabled = False
                rGrid.Visible = False
            Case CommonMessage.STATE_NORMAL, ""
                MainToolBar.Enabled = True
                rGrid.Visible = True
        End Select
    End Sub

    Protected Sub UpdateViewState()
        Try
            UpdateControlStatus()
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    If Not Me.ViewPlaceHolder.Controls.Contains(ViewItem) Then
                        ViewPlaceHolder.Controls.Add(ViewItem)
                    End If
                    ViewItem.CurrentState = CurrentState
                    ViewItem.Refresh()
                Case CommonMessage.STATE_NORMAL
                    Me.ViewPlaceHolder.Controls.Clear()
                Case CommonMessage.STATE_EDIT
                    If Not Me.ViewPlaceHolder.Controls.Contains(ViewItem) Then
                        ViewPlaceHolder.Controls.Add(ViewItem)
                    End If
                    ViewItem.CurrentState = CurrentState
                    ViewItem.SetProperty("EventInfo", EventInfo)
                    ViewItem.Refresh()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub LoadAllControlDefault()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Function RepareDataForDelete() As List(Of Decimal)
        Dim lst As New List(Of Decimal)
        For Each dr As Telerik.Web.UI.GridDataItem In rGrid.SelectedItems
            lst.Add(Decimal.Parse(dr("ID").Text))
        Next
        Return lst
    End Function
#End Region
End Class