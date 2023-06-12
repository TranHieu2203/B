Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI

Public Class ctrlPortalIETDetailHistory
    Inherits Common.CommonView
    Dim _classPath As String = "Performance/Module/Portal/" + Me.GetType().Name.ToString()

#Region "Property"

    Property dtDetail As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        Get
            Return ViewState(Me.ID & "dtDetail")
        End Get
        Set(ByVal value As List(Of PE_KPI_ASSESMENT_DETAIL_DTO))
            ViewState(Me.ID & "dtDetail") = value
        End Set
    End Property
    Public Property Criteria As String
    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get

        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar)
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New PerformanceRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                Case CommonMessage.TOOLBARITEM_CANCEL
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Dim rep As New PerformanceRepository
        Try
            Dim _filter As New PE_KPI_ASSESMENT_DETAIL_DTO
            Dim str As String = Request.Params("Criteria")
            Dim goal = Request.Params("GoalID")
            Dim id = 0
            _filter.GOAL_ID = Decimal.Parse(goal)
            Dim lstID As New List(Of String)
            lstID = str.Split(",").ToList
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim _param As New ParamDTO

            Dim MaximumRows As Integer
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If dtDetail IsNot Nothing Then
            Else
                If Sorts IsNot Nothing Then
                    dtDetail = rep.GetKpiAssessmentDetailHistory(lstID, _filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, _param, Sorts)
                Else
                    dtDetail = rep.GetKpiAssessmentDetailHistory(lstID, _filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, _param)
                End If
            End If
            rgMain.DataSource = dtDetail
            rgMain.VirtualItemCount = MaximumRows
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    Protected Sub RebindGrid()
        Try
            rgMain.Rebind()
            For Each items As GridDataItem In rgMain.MasterTableView.Items
                items.Edit = True
            Next
            rgMain.MasterTableView.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region
End Class