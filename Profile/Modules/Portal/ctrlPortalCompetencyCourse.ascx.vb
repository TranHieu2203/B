Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness

Public Class ctrlPortalCompetencyCourse
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase

#Region "Property"
    Public Property GridList As List(Of EmployeeCriteriaRecordDTO)
        Get
            Return PageViewState(Me.ID & "_GridList")
        End Get
        Set(ByVal value As List(Of EmployeeCriteriaRecordDTO))
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

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Try
            SetValueObjectByRadGrid(rgData, New EmployeeCriteriaRecordDTO)

            If Not IsPostBack Then
                GridList = rep.GetPortalCompetencyCourse(Session("_EmployeeID"))
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                If Message = CommonMessage.ACTION_SAVED Then
                    GridList = rep.GetPortalCompetencyCourse(EmployeeID)
                End If
            End If

            'Đưa dữ liệu vào Grid
            If Me.GridList IsNot Nothing Then
                rgData.DataSource = Me.GridList
                rgData.DataBind()
            Else
                rgData.DataSource = New List(Of EmployeeCriteriaRecordDTO)
                rgData.DataBind()
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            'If IsPostBack Then Exit Sub
            'SetValueObjectByRadGrid(rgData, New EmployeeCriteriaRecordDTO)

            'Dim rep As New ProfileBusinessRepository
            'GridList = rep.GetAssessmentDirect(EmployeeID)
            'rgData.DataSource = GridList
            Refresh()
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


#End Region

#Region "Custom"


#End Region
End Class