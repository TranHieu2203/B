Imports Common
Imports Framework.UI
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI

Public Class ctrlListClassifi
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
    Public Property InfoList As List(Of StatisticDTO)
        Get
            Return ViewState(Me.ID & "_InfoList")
        End Get
        Set(ByVal value As List(Of StatisticDTO))
            ViewState(Me.ID & "_InfoList") = value
        End Set
    End Property

    Public Property ClassificationList As List(Of ClassificationDTO)
        Get
            Return PageViewState(Me.ID & "_ClassificationList")
        End Get
        Set(ByVal value As List(Of ClassificationDTO))
            PageViewState(Me.ID & "_ClassificationList") = value
        End Set
    End Property

#End Region

#Region "Page"
    Private width As Integer
    Private height As Integer

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Dim resize As Integer = 0
            If Request("resize") IsNot Nothing Then
                resize = CInt(Request("resize"))
            End If
            If Not IsPostBack Or resize = 0 Then
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
            rgContract.Rebind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgContract.SetFilter()
            rgContract.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


#End Region

#Region "Event"

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgContract.NeedDataSource
        Using rep As New PerformanceRepository
            Try
                ClassificationList = rep.GetStatisticClassification()
                rgContract.DataSource = ClassificationList
            Catch ex As Exception
                rep.Dispose()
                DisplayException(Me.ViewName, Me.ID, ex)
            End Try
        End Using
    End Sub

#End Region

#Region "Custom"

#End Region

End Class