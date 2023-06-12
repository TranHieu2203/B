Imports Common.CommonBusiness
Imports Framework.UI
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlAccessLog
    Inherits CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"

#End Region

#Region "Page"
    ''' <summary>
    ''' Khởi tạo, load menu toolbar
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = rtbAccessLog
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export)
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        rgAccessLog.AllowCustomPaging = True
    End Sub
    ''' <summary>
    ''' Load combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim lstStatus As New List(Of LogoutStatus)
            lstStatus.Add(New LogoutStatus With {.Text = "Logout", .Value = "Logout"})
            lstStatus.Add(New LogoutStatus With {.Text = "Timeout", .Value = "Timeout"})
            lstStatus.Add(New LogoutStatus With {.Text = "Killed", .Value = "Killed"})
            Utilities.FillDropDownList(cboStatus, lstStatus, "Text", "Value", Common.SystemLanguage, True, cboStatus.SelectedValue)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load datasource cho grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgAccessLog_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgAccessLog.NeedDataSource
        CreateDataFilter()
    End Sub
    ''' <summary>
    ''' Load data from DB to grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name
        Dim filter As New AccessLogFilter
        Using rep As New CommonRepository
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                filter.Status = cboStatus.SelectedValue
                filter.Username = txtUSER.Text.Trim.ToUpper
                filter.IP = txtIP.Text.Trim
                filter.FromDate = rdFromDate.SelectedDate
                filter.ToDate = rdToDate.SelectedDate
                filter.ComputerName = txtComputerName.Text.Trim
                filter.username_login = LogHelper.CurrentUser.USERNAME.ToUpper
                Dim MaximumRows As Integer
                Dim Sorts As String = rgAccessLog.MasterTableView.SortExpressions.GetSortString()

                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetAccessLog(filter, Sorts).ToTable()
                    Else
                        Return rep.GetAccessLog(filter).ToTable()
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        rgAccessLog.DataSource = rep.GetAccessLog(filter, rgAccessLog.CurrentPageIndex, rgAccessLog.PageSize, MaximumRows, Sorts)
                    Else
                        rgAccessLog.DataSource = rep.GetAccessLog(filter, rgAccessLog.CurrentPageIndex, rgAccessLog.PageSize, MaximumRows)
                    End If
                    rgAccessLog.VirtualItemCount = MaximumRows
                End If
                _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
                DisplayException(Me.ViewName, Me.ID, ex)

            End Try
        End Using
    End Function
#End Region

#Region "Event"
    ''' <summary>
    ''' Event click button tìm kiếm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFIND_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFIND.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgAccessLog.CurrentPageIndex = 0
            rgAccessLog.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="events"></param>
    ''' <remarks></remarks>
    Public Sub MainToolbarClick(ByVal sender As Object, ByVal events As Telerik.Web.UI.RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(events.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgAccessLog.ExportExcel(Server, Response, dtData, "AccessLog")
                        End If
                    End Using
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


#End Region

#Region "Custom"
    ''' <summary>
    ''' Khai báo lớp logoutStatus
    ''' </summary>
    ''' <remarks></remarks>
    Public Class LogoutStatus
        Public Property Text As String
        Public Property Value As String
    End Class
#End Region

End Class

