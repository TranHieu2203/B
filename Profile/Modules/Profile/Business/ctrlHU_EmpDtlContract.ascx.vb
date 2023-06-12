Imports Common
Imports Framework.UI
Imports Profile.ProfileBusiness

Public Class ctrlHU_EmpDtlContract
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    ' Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
    Public Property GridList As List(Of ContractDTO)
        Get
            Return PageViewState(Me.ID & "_GridList")
        End Get
        Set(ByVal value As List(Of ContractDTO))
            PageViewState(Me.ID & "_GridList") = value
        End Set
    End Property
    Public Property EmployeeID As Decimal
    'Thông tin cơ bản của nhân viên.
    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
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

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Try
            If EmployeeInfo IsNot Nothing Then
                EmployeeID = EmployeeInfo.ID
                GridList = rep.GetContractProccess(EmployeeID)
            End If
            'Đưa dữ liệu vào Grid
            If Me.GridList IsNot Nothing Then
                rgGrid.DataSource = Me.GridList
                rgGrid.DataBind()
            Else
                rgGrid.DataSource = New List(Of ContractDTO)
                rgGrid.DataBind()
            End If
            rep.Dispose()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Private Sub rgGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        Try
            If IsPostBack Then Exit Sub

            Dim rep As New ProfileBusinessRepository
            GridList = rep.GetContractProccess(EmployeeID)
            rgGrid.DataSource = GridList
            rep.Dispose()
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

#End Region
End Class