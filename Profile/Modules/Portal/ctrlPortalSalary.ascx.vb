Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlPortalSalary
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase

#Region "Property"
    Public Property GridList As List(Of WorkingDTO)
        Get
            Return PageViewState(Me.ID & "_GridList")
        End Get
        Set(ByVal value As List(Of WorkingDTO))
            PageViewState(Me.ID & "_GridList") = value
        End Set
    End Property
    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
#End Region

#Region "Page"
    Public Overrides Property MustAuthorize As Boolean = False
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            rgSalary.SetFilter()
            rgSalary.ClientSettings.EnablePostBackOnRowClick = True
            rgAllow.SetFilter()
            Refresh()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/07/2017 18:11
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgContract
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Not IsPostBack Then
                GirdConfig(rgSalary)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Try
            SetValueObjectByRadGrid(rgSalary, New WorkingDTO)

            If Not IsPostBack Then
                GridList = rep.GetSalaryProccess(EmployeeID)
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                If Message = CommonMessage.ACTION_SAVED Then
                    GridList = rep.GetSalaryProccess(EmployeeID)
                End If
            End If

            'Đưa dữ liệu vào Grid
            If Me.GridList IsNot Nothing Then

                rgSalary.DataSource = Me.GridList
                rgSalary.DataBind()

            Else
                rgSalary.DataSource = New List(Of WorkingDTO)
                rgSalary.DataBind()
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Private Sub rgSalary_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgSalary.NeedDataSource
        Try

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgSalary_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgSalary.SelectedIndexChanged
        rgAllow.Rebind()
    End Sub

    Private Sub rgAllow_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgAllow.NeedDataSource
        'Dim _filter = New WorkingAllowanceDTO
        'Try
        '    If rgSalary.SelectedItems.Count = 0 Then
        '        rgAllow.DataSource = New List(Of WorkingAllowanceDTO)
        '        Exit Sub
        '    End If
        '    SetValueObjectByRadGrid(rgAllow, _filter)
        '    Dim item As GridDataItem = rgSalary.SelectedItems(0)
        '    Dim rep As New ProfileBusinessRepository
        '    _filter.HU_WORKING_ID = item.GetDataKeyValue("ID")
        '    rgAllow.DataSource = rep.GetAllowanceByWorkingID(_filter)
        'Catch ex As Exception
        '    DisplayException(Me.ViewName, Me.ID, ex)
        'End Try
        Dim _filter = New WorkingAllowanceDTO
        Try
            If rgSalary.SelectedItems.Count = 0 Then
                rgAllow.DataSource = New List(Of WorkingAllowanceDTO)
                Exit Sub
            End If
            SetValueObjectByRadGrid(rgAllow, _filter)
            Dim item As GridDataItem = rgSalary.SelectedItems(0)
            Dim rep As New ProfileBusinessRepository
            _filter.HU_WORKING_ID = item.GetDataKeyValue("ID")
            _filter.EFFECT_DATE = item.GetDataKeyValue("EFFECT_DATE")
            _filter.EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
            rgAllow.DataSource = rep.GetAllowanceByDate(_filter)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region


#Region "Custom"

#End Region
End Class