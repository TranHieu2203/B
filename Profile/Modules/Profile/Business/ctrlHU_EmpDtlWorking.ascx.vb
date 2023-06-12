Imports Common
Imports Framework.UI
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlHU_EmpDtlWorking
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    'Public Overrides Property MustAuthorize As Boolean = False


#Region "Property"
    ''' <summary>
    ''' EmployeeInfo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <summary>
    ''' Hien thi cac control tren page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlEmpBasicInfo.SetProperty("EmployeeInfo", EmployeeInfo)
            Refresh()
            rgGrid.SetFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Phuong thuc khoi tao cac control 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        'Try
        '    If Not IsPostBack Then
        '        GirdConfig(rgGrid)
        '    End If
        'Catch ex As Exception
        '    DisplayException(Me.ViewName, Me.ID, ex)
        'End Try

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")

        Try
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' Xu ly su kien ItemDataBound cua grid rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgGrid.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
        End If
    End Sub
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cua grid rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        Try
            'Dim rep As New ProfileBusinessRepository
            'Dim lst = rep.GetWorkingProccess(If(EmployeeInfo Is Nothing, Nothing, EmployeeInfo.ID))
            Dim rep1 As New ProfileStoreProcedure
            rgGrid.DataSource = rep1.get_current_work_history(EmployeeInfo.ID)
            'rep.Dispose()
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

#End Region
End Class