Imports WebAppLog

Public Class ctrlDashboard
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Dashboard/" + Me.GetType().Name.ToString()

#Region "Property"
#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 22/08/2017 14:12
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo thiết lập dữ liệu ban đầu
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            IsShowDenyMessage = False
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 22/08/2017 14:12
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "SizeToFit(0);", True)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
#End Region

#Region "Event"

#End Region

#Region "Custom"

#End Region
End Class