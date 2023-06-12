Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Public Class ctrlHU_EmpDtlSalary
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    ' Public Overrides Property MustAuthorize As Boolean = False


#Region "Property"
    Public Overrides Property IsShowDenyMessage As Boolean = False
    Public Property GridList As List(Of WorkingDTO)
        Get
            Return PageViewState(Me.ID & "_GridList")
        End Get
        Set(ByVal value As List(Of WorkingDTO))
            PageViewState(Me.ID & "_GridList") = value
        End Set
    End Property
    ReadOnly Property EmployeeID As Decimal
        Get
            Return EmployeeInfo.ID
        End Get
    End Property
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
            rgGrid.SetFilter()
            rgGrid.ClientSettings.EnablePostBackOnRowClick = True
            rgAllow.SetFilter()
            Me.CurrentPlaceHolder = Me.ViewName
            ctrlEmpBasicInfo.SetProperty("CurrentPlaceHolder", Me.CurrentPlaceHolder)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub ViewInit(e As EventArgs)
        MyBase.ViewInit(e)
        If Not IsPostBack Then
            GirdConfig(rgGrid)
        End If
        'rgGrid.MasterTableView.GetColumn("SAL_TYPE_NAME").HeaderText = UI.Wage_WageGRoup
        'rgGrid.MasterTableView.GetColumn("SAL_BASIC").HeaderText = UI.Wage_BasicSalary
        'rgGrid.MasterTableView.GetColumn("TAX_TABLE_Name").HeaderText = UI.Wage_TaxTable
        'rgGrid.MasterTableView.GetColumn("SAL_INS").HeaderText = UI.Wage_Sal_Ins
        'rgGrid.MasterTableView.GetColumn("SAL_TOTAL").HeaderText = UI.Wage_Salary_Total
    End Sub
#End Region

#Region "Event"

    Private Sub rgGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgGrid.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            'Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            'DataRow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
        End If
    End Sub

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
            rgGrid.DataSource = rep1.GET_CURRENT_SALARY_HISTORY(EmployeeInfo.ID)
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgGrid_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgGrid.SelectedIndexChanged
        rgAllow.Rebind()
    End Sub

    Private Sub rgAllow_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgAllow.NeedDataSource
        Dim _filter = New WorkingAllowanceDTO
        Try
            If rgGrid.SelectedItems.Count = 0 Then
                rgAllow.DataSource = New List(Of WorkingAllowanceDTO)
                Exit Sub
            End If
            SetValueObjectByRadGrid(rgAllow, _filter)
            Dim item As GridDataItem = rgGrid.SelectedItems(0)
            Dim rep As New ProfileBusinessRepository
            _filter.HU_WORKING_ID = item.GetDataKeyValue("ID")
            _filter.EFFECT_DATE = item.GetDataKeyValue("EFFECT_DATE")
            _filter.EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
            rgAllow.DataSource = rep.GetAllowanceByDate(_filter)
            rep.Dispose()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

#End Region

End Class