Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Public Class ctrlHU_EmpDtlConcurrently
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    'Public Overrides Property MustAuthorize As Boolean = False
    Dim log As New UserLog

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
    'Thông tin cơ bản của nhân viên.
    Property EmployeeInfo As ProfileBusiness.EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As ProfileBusiness.EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property
    'Lưu lại đang ở View nào để load khi dùng nút Next và Previous để chuyển sang xem thông tin nhân viên khác.
    Public Property CurrentPlaceHolder As String
        Get
            Return PageViewState(Me.ID & "_CurrentPlaceHolder")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_CurrentPlaceHolder") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlEmpBasicInfo.SetProperty("EmployeeInfo", EmployeeInfo)
            Refresh()
            Me.CurrentPlaceHolder = Me.ViewName
            ctrlEmpBasicInfo.SetProperty("CurrentPlaceHolder", Me.CurrentPlaceHolder)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            If Not IsPostBack Then
                'GirdConfig(rgGrid)
            End If
            rgGrid.SetFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Try
            Dim _filter As New Temp_ConcurrentlyDTO
            SetValueObjectByRadGrid(rgGrid, _filter)
            Dim startTime As DateTime = DateTime.UtcNow
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Dim MaximumRows As Integer
            Dim Sorts As String = rgGrid.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                Me.Concurrently = rep.GET_LIST_CONCURRENTLY_BY_ID(_filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows, EmployeeInfo.ID, Sorts)
            Else
                Me.Concurrently = rep.GET_LIST_CONCURRENTLY_BY_ID(_filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows, EmployeeInfo.ID)
            End If
            rep.Dispose()
            rgGrid.VirtualItemCount = MaximumRows
            rgGrid.DataSource = Me.Concurrently
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Private Sub rgGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        Try
            'CreateDataFilter(False)
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

#End Region
End Class