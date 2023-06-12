Imports Common
Imports Framework.UI
Imports Profile.ProfileBusiness
Public Class ctrlHU_EmpDtlWorkingBefore
    Inherits CommonView
    Dim employeeCode As String
    'Public Overrides Property MustAuthorize As Boolean = False
#Region "Properties"
    Property SelectedItem As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItem") = value
        End Set
    End Property
    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property

    Property isLoad As Boolean
        Get
            Return ViewState(Me.ID & "_isLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isLoad") = value
        End Set
    End Property

    Property lstWorkingBefore As List(Of WorkingBeforeDTO)
        Get
            Return ViewState(Me.ID & "_lstWorkingBefore")
        End Get
        Set(ByVal value As List(Of WorkingBeforeDTO))
            ViewState(Me.ID & "_lstWorkingBefore") = value
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
            rgGrid.SetFilter()
            rgGrid.ClientSettings.EnablePostBackOnRowClick = True
            Me.CurrentPlaceHolder = Me.ViewName
            ctrlEmpBasicInfo.SetProperty("CurrentPlaceHolder", Me.CurrentPlaceHolder)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgGrid.SetFilter()
            rgGrid.AllowCustomPaging = True
            If Not IsPostBack Then
                GirdConfig(rgGrid)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    Private Sub rgGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        Try
            'Dim rep As New ProfileBusinessRepository
            'Dim _param = New ParamDTO With {.ORG_ID = 1,
            '.IS_DISSOLVE = True}
            'If (EmployeeID <> 0) Then
            '    rgSalary.DataSource = rep.GetWorking(New WorkingDTO With {.EMPLOYEE_ID = EmployeeID,
            '                                                          .STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID,
            '                                                          .IS_WAGE = -1}, _param, "EFFECT_DATE desc")
            'GridList = rep.GetWorking(New WorkingDTO With {.EMPLOYEE_ID = EmployeeID,
            '                                                          .STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID,
            '                                                          .IS_WAGE = -1}, _param, "EFFECT_DATE desc")
            'rgGrid.DataSource = GridList
            'rep.Dispose()
            Dim rep1 As New ProfileStoreProcedure
            rgGrid.DataSource = rep1.GET_WORKING_BEFORE(EmployeeInfo.ID)
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    Protected Sub SetStatusToolBar()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class