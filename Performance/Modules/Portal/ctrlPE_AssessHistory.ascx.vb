Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities

Public Class ctrlPE_AssessHistory
    Inherits Common.CommonView
#Region "Property"
    'Public Property ID As Decimal
    Public Property EmployeeCode As String
    Public Overrides Property MustAuthorize As Boolean = False

    Public ReadOnly Property CurrentUser As UserDTO
        Get
            Return LogHelper.CurrentUser
        End Get
    End Property
#End Region

#Region "View"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        If Not IsPostBack Then
            Refresh()
        End If
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

    Public Overrides Sub BindData()
    End Sub

#End Region

#Region "Events"

    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            Dim rep As New PerformanceRepository
            Dim id = Decimal.Parse(Request.QueryString("ID"))

            Dim DATA = rep.GET_PE_ASSESSMENT_HISTORY(id)

            rgData.DataSource = DATA
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

#End Region

#Region "Custom"
    Private Sub LoadComboData()
        Try

        Catch ex As Exception
            ShowMessage(ex.Message, NotifyType.Error)
        End Try
    End Sub
#End Region
End Class