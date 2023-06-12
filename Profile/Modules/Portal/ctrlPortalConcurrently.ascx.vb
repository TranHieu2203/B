Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness

Public Class ctrlPortalConcurrently
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase

#Region "Property"
    Public Property Concurrently As List(Of Temp_ConcurrentlyDTO)
        Get
            Return ViewState(Me.ID & "_Concurrently")
        End Get

        Set(ByVal value As List(Of Temp_ConcurrentlyDTO))
            ViewState(Me.ID & "_Concurrently") = value
        End Set
    End Property

    Public Property GridList As List(Of TitleConcurrentDTO)
        Get
            Return PageViewState(Me.ID & "_GridList")
        End Get
        Set(ByVal value As List(Of TitleConcurrentDTO))
            PageViewState(Me.ID & "_GridList") = value
        End Set
    End Property
    Public Property EmployeeID As Decimal
#End Region

#Region "Page"
    Public Overrides Property MustAuthorize As Boolean = False
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        'Try
        '    If Not IsPostBack Then
        '        GirdConfig(rgDiscipline)
        '    End If
        'Catch ex As Exception
        '    DisplayException(Me.ViewName, Me.ID, ex)
        'End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        'Dim rep As New ProfileBusinessRepository
        'Try
        '    rgDiscipline.SetFilter()
        '    SetValueObjectByRadGrid(rgDiscipline, New TitleConcurrentDTO)

        '    If Not IsPostBack Then
        '        GridList = rep.GetConcurrentlyProccess(EmployeeID)
        '        CurrentState = CommonMessage.STATE_NORMAL
        '    Else
        '        If Message = CommonMessage.ACTION_SAVED Then
        '            GridList = rep.GetConcurrentlyProccess(EmployeeID)
        '        End If
        '    End If

        '    'Đưa dữ liệu vào Grid
        '    If Me.GridList IsNot Nothing Then
        '        rgDiscipline.DataSource = Me.GridList
        '        rgDiscipline.DataBind()
        '    Else
        '        rgDiscipline.DataSource = New List(Of TitleConcurrentDTO)
        '        rgDiscipline.DataBind()
        '    End If

        'Catch ex As Exception
        '    DisplayException(Me.ViewName, Me.ID, ex)
        'End Try

        Dim rep As New ProfileBusinessRepository
        Try
            Dim _filter As New Temp_ConcurrentlyDTO
            SetValueObjectByRadGrid(rgDiscipline, _filter)
            Dim startTime As DateTime = DateTime.UtcNow
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Dim MaximumRows As Integer
            Dim Sorts As String = rgDiscipline.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                Me.Concurrently = rep.GET_LIST_CONCURRENTLY_BY_ID(_filter, rgDiscipline.CurrentPageIndex, rgDiscipline.PageSize, MaximumRows, EmployeeID, Sorts)
            Else
                Me.Concurrently = rep.GET_LIST_CONCURRENTLY_BY_ID(_filter, rgDiscipline.CurrentPageIndex, rgDiscipline.PageSize, MaximumRows, EmployeeID)
            End If
            rep.Dispose()
            rgDiscipline.VirtualItemCount = MaximumRows
            rgDiscipline.DataSource = Me.Concurrently
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Private Sub rgDiscipline_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgDiscipline.NeedDataSource
        Try
            If IsPostBack Then Exit Sub
            SetValueObjectByRadGrid(rgDiscipline, New TitleConcurrentDTO)

            Dim rep As New ProfileBusinessRepository
            GridList = rep.GetConcurrentlyProccess(EmployeeID)
            rgDiscipline.DataSource = GridList

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

#End Region
End Class