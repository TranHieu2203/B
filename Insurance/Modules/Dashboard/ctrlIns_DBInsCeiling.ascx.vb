Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlIns_DBInsCeiling
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Insurance/Module/Insurance/Dashboard/" + Me.GetType().Name.ToString()

#Region "Property"
    ''' <summary>
    ''' InsDataRateList
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InsDataRateList As List(Of StatisticInsCeilingDTO)
        Get
            Return PageViewState(Me.ID & "_StatisticInsCeilingDTO")
        End Get
        Set(ByVal value As List(Of StatisticInsCeilingDTO))
            PageViewState(Me.ID & "_StatisticInsCeilingDTO") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <summary>
    ''' ViewLoad
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Refresh()
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Làm mới các thành phần trên trang
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            LoadDataGrid()
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            'rgGridDataRate.SetFilter()
            'rgGridDataRate.PageSize = Common.Common.DefaultPageSize
            'CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource cho rad grid rgGridDataRate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgGridDataRate.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            LoadDataGrid()
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' load dữ liệu cho rad grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LoadDataGrid(Optional ByVal isFull As Boolean = False)
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            InsDataRateList = rep.GetStatisticInsCeiling()

            rgGridDataRate.DataSource = InsDataRateList
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

#End Region

#Region "Custom"

#End Region

End Class