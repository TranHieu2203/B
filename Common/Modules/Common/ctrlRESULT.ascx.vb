'Imports Framework.UI.Utilities
Imports System.IO
Imports System.Threading
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
'Imports Excel
Imports WebAppLog
Public Class ctrlRESULT
    Inherits CommonView

    Private XMLFileThread As Thread
    Dim rep As New HistaffFrameworkRepository
    Public isPhysical As Decimal = Decimal.Parse(ConfigurationManager.AppSettings("PHYSICAL_PATH"))
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Module/Common/" + Me.GetType().Name.ToString()
    Dim com As New CommonProcedureNew
#Region "Property"

    Private Property requestId As Decimal
        Get
            Return PageViewState(Me.ID & "_requestId")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_requestId") = value
        End Set
    End Property

    Private Property typeLog As Decimal
        Get
            Return PageViewState(Me.ID & "_typeLog")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_typeLog") = value
        End Set
    End Property

    Public Property pathImport As String
        Get
            Return ViewState(Me.ID & "_pathImport")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_pathImport") = value
        End Set
    End Property

#End Region


#Region "Page"
    ''' <summary>
    ''' Khởi tạo page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' Load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If requestId = 0 Then
                ShowMessage("Không tìm thấy request id !!!", Framework.UI.Utilities.NotifyType.Warning)
                Exit Sub
            Else
                If isPhysical = 1 Then 'Nếu là 1 : lấy đường dẫn theo cấu hình ||    0: lấy đường dẫn theo web server                    
                    If typeLog = 0 Then   'Hiển thị log file của request Import
                        Dim pathFileLog As String = System.IO.Path.Combine(pathImport, "log_" & requestId & ".log")
                        If File.Exists(pathFileLog) Then
                            Dim rs = File.ReadAllText(pathFileLog)
                            rtbLog.Text = rs
                        Else
                            ShowMessage("Không tìm thấy file .log !!!", Framework.UI.Utilities.NotifyType.Warning)
                        End If
                    Else
                        '    Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.PRO_GET_LOG_FILE", New List(Of Object)(New Object() {requestId, OUT_STRINGBUDER}))
                        '    Dim logDoc As String = ""
                        '    If obj Is Nothing Then
                        '        logDoc = "Không có thông tin gì về log của request này!!!"
                        '    Else
                        '        logDoc = obj(0).ToString
                        '    End If
                        '    rtbLog.Text = logDoc
                        'End If

                        Dim dtLog As New DataTable
                        dtLog = com.GET_LOG_CACULATOR(requestId)
                        Dim rowLog As String = ""
                        rowLog = dtLog.Rows(0)(0).ToString()
                        If rowLog Is Nothing Then
                            rtbLog.Text = "Không có thông tin gì về log của request này!!!"
                        Else
                            rtbLog.Text = rowLog
                        End If
                    End If

                Else
                    If typeLog = 0 Then   'Hiển thị log file của request Import
                        Dim pathFileLog As String = System.IO.Path.Combine(Server.MapPath(pathImport), "log_" & requestId & ".log")
                        If File.Exists(pathFileLog) Then
                            Dim rs = File.ReadAllText(pathFileLog)
                            rtbLog.Text = rs
                        Else
                            ShowMessage("Không tìm thấy file .log !!!", Framework.UI.Utilities.NotifyType.Warning)
                        End If
                    Else
                        'Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.PRO_GET_LOG_FILE", New List(Of Object)(New Object() {requestId, OUT_STRING}))
                        'Dim logDoc = obj(0).ToString
                        'rtbLog.Text = logDoc
                        Dim dtLog As New DataTable
                        dtLog = com.GET_LOG_CACULATOR(requestId)
                        Dim rowLog As String = ""
                        rowLog = dtLog.Rows(0)(0).ToString()
                        If rowLog Is Nothing Then
                            rtbLog.Text = "Không có thông tin gì về log của request này!!!"
                        Else
                            rtbLog.Text = rowLog
                        End If
                    End If
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Load data
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            GetParams()
            'lấy đường dẫn lưu file sau khi convert sang csv xong
            Dim objImport As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.READ_URL_CONTROL_FOLDER",
                                                       New List(Of Object)(New Object() {"PATH_IMPORT_FOLDER", OUT_STRING}))
            If objImport IsNot Nothing Then
                pathImport = objImport(0).ToString
            Else
                pathImport = ""
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    'Protected Sub rgLoadData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgLoadData.NeedDataSource
    '    Try
    '        FillDataToGridView()
    '    Catch ex As Exception
    '        ShowMessage(ex.Message, Utilities.NotifyType.Error)
    '    End Try
    'End Sub

#End Region

#Region "Custom"
    ''' <summary>
    ''' Fill data to grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillDataToGridView()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim ds As New DataSet

            '3. Fill DataSoure vào GridView
            If ds IsNot Nothing Then
                'rgLoadData.DataSource = ds
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Get param from url
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If CurrentState Is Nothing Then

                If Request.Params("requestID") IsNot Nothing Then
                    requestId = Decimal.Parse(Request.Params("requestID"))
                End If

                If Request.Params("typeLog") IsNot Nothing Then
                    typeLog = Decimal.Parse(Request.Params("typeLog"))
                End If

            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
#End Region
End Class