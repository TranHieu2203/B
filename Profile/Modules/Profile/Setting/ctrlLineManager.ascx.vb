Imports Common
Imports Profile.ProfileBusiness

Public Class ctrlLineManager
    Inherits Common.CommonView
#Region "Property"

    Private Property EMPLOYEE_LIST As List(Of EmployeeDTO)
        Get
            Return ViewState(Me.ID & "_OrgList")
        End Get
        Set(value As List(Of EmployeeDTO))
            ViewState(Me.ID & "_OrgList") = value
        End Set
    End Property

#End Region

#Region "Page"
    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim rep As New ProfileRepository

            If EMPLOYEE_LIST Is Nothing Then
                EMPLOYEE_LIST = rep.GetLineManager(LogHelper.CurrentUser.USERNAME)
            End If
            rep.Dispose()
            If IsPostBack Then
                Return
            End If

            Refresh()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        'Dim rep As New CommonRepository
        'Dim rep2 As New ProfileRepository
        Try
            'Dim orgId = rep2.GetOrgFromUsername(LogHelper.CurrentUser.USERNAME)
            Dim employees = EMPLOYEE_LIST

            'chartData1.LoadOnDemand = OrgChartLoadOnDemand.NodesAndGroups
            chartData1.DataFieldID = "ID"
            chartData1.DataFieldParentID = "DIRECT_MANAGER"
            chartData1.DataTextField = "FULLNAME_VN"
            chartData1.DataSource = employees
            chartData1.DataBind()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Public Overrides Sub BindData()
        Try


        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        If chartData1.Nodes.Count > 0 Then
            chartData1.Nodes(0).Collapsed = True
            'chartData1.LoadOnDemand = OrgChartLoadOnDemand.NodesAndGroups
            Me.ViewDescription = Translate("Line manager chart")
        End If
    End Sub

#End Region

#Region "Custom"


#End Region

End Class