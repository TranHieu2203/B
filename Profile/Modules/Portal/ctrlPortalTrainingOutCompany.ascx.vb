Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness

Public Class ctrlPortalTrainingOutCompany
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase

#Region "Property"
    Public Property GridList As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)
        Get
            Return PageViewState(Me.ID & "_GridList")
        End Get
        Set(ByVal value As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO))
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
        Try
            If Not IsPostBack Then
                GirdConfig(rgTrainingOutCompany)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Try
            rgTrainingOutCompany.SetFilter()
            SetValueObjectByRadGrid(rgTrainingOutCompany, New HU_PRO_TRAIN_OUT_COMPANYDTO)
            Dim objEmployeeTrain As New HU_PRO_TRAIN_OUT_COMPANYDTO
            objEmployeeTrain.EMPLOYEE_ID = EmployeeID
            If Not IsPostBack Then
                GridList = rep.GetProcessTraining(objEmployeeTrain)
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                If Message = CommonMessage.ACTION_SAVED Then
                    GridList = rep.GetProcessTraining(objEmployeeTrain)
                End If
            End If

            'Đưa dữ liệu vào Grid
            If Me.GridList IsNot Nothing Then
                rgTrainingOutCompany.DataSource = Me.GridList
                rgTrainingOutCompany.DataBind()
            Else
                rgTrainingOutCompany.DataSource = New List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)
                rgTrainingOutCompany.DataBind()
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Private Sub rgTrainingOutCompany_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgTrainingOutCompany.NeedDataSource
        Try
            If IsPostBack Then Exit Sub
            SetValueObjectByRadGrid(rgTrainingOutCompany, New HU_PRO_TRAIN_OUT_COMPANYDTO)

            Dim rep As New ProfileBusinessRepository
            Dim objEmployeeTrain As New HU_PRO_TRAIN_OUT_COMPANYDTO
            objEmployeeTrain.EMPLOYEE_ID = EmployeeID
            GridList = rep.GetProcessTraining(objEmployeeTrain)
            rgTrainingOutCompany.DataSource = GridList

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

#End Region
End Class