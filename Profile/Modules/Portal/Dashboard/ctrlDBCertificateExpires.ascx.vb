Imports Common
Imports Framework.UI

Public Class ctrlDBCertificateExpires
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase

#Region "Property"
    'Public Property GridList As DataTable
    '    Get
    '        Return PageViewState(Me.ID & "_GridList")
    '    End Get
    '    Set(ByVal value As DataTable)
    '        PageViewState(Me.ID & "_GridList") = value
    '    End Set
    'End Property
    Public Property EmployeeID As Decimal
#End Region

#Region "Page"
    Public Overrides Property MustAuthorize As Boolean = False
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            If Not IsPostBack Then
                Refresh(CommonMessage.ACTION_UPDATED)
            Else
                Refresh()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            Dim StatisticData As DataTable
            Using rep As New ProfileDashboardRepository
                'get competency group
                StatisticData = rep.GetCertificateExpires(Session("_EmployeeID"))
                If StatisticData IsNot Nothing AndAlso StatisticData.Rows.Count > 0 Then
                    rgData.DataSource = StatisticData
                    'rgData.DataBind()
                Else
                    rgData.DataSource = Nothing
                End If
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            Refresh()
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


#End Region

#Region "Custom"


#End Region
End Class