Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlSignWorkDelete
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()

#Region "Property"
    Dim emp_id As String
    Dim periodid As Decimal
    ''' <lastupdate>
    ''' 17/08/2017 09:55
    ''' </lastupdate>
    ''' <summary>
    ''' 0 - normal
    ''' 1 - Employee
    ''' 2 - Sign
    ''' 3 - Org
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 17/08/2017 09:55
    ''' </lastupdate>
    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 17/08/2017 09:55
    ''' </lastupdate>
    ''' <summary>
    ''' them danh sach xep ca lam viec
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Insertworksign As List(Of AT_WORKSIGNDTO)
        Get
            Return ViewState(Me.ID & "_AT_WORKSIGNDTO")
        End Get
        Set(ByVal value As List(Of AT_WORKSIGNDTO))
            ViewState(Me.ID & "_AT_WORKSIGNDTO") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 17/08/2017 09:55
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc viewload
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 09:55
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc khoi tao, goi phuong thuc thiet lap, khoi tao cac control tren trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 09:55
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc khoi tao, thiet lap cac thanh phan tren trang: main toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 09:55
    ''' </lastupdate>
    ''' <summary>
    ''' Lam moi, thiet lap cac control tren trang
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                If Request.Params("periodid") IsNot Nothing AndAlso Request.Params("periodid").ToString <> "" Then
                    periodid = Request.Params("periodid")
                    'Using rep As New AttendanceRepository
                    '    Dim period = rep.LOAD_PERIODByID(New AT_PERIODDTO With {.PERIOD_ID = periodid})
                    '    'rdFromdate.SelectedDate = period.START_DATE
                    '    'rdEnddate.SelectedDate = period.END_DATE
                    '    rdFromdate.MinDate = period.START_DATE
                    '    rdFromdate.MaxDate = period.END_DATE
                    '    rdEnddate.MinDate = period.START_DATE
                    '    rdEnddate.MaxDate = period.END_DATE
                    'End Using
                End If
            End If
            Dim psp As New AttendanceStoreProcedure
            Dim dt_ID As DataTable = psp.SELECT_ID_BY_DELETE_SIGN_WORK(113)
            If (dt_ID.Rows.Count > 0) Then
                'emp_id = Request.Params("ID")
                emp_id = dt_ID.Rows(0)(0).ToString()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 17/08/2017 09:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Command khi click cac item tren toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New AttendanceRepository
        Dim gCODE As String = ""
        Dim gstatus As Integer = 0
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_DELETE
                    Dim strArr() As String
                    strArr = emp_id.Split(",")

                    For idx = 0 To strArr.Length - 1
                        Dim _param = New ParamDTO With {.EMPLOYEE_ID = Convert.ToDecimal(strArr(idx)), _
                                                 .FROMDATE = rdFromdate.SelectedDate, _
                                                 .ENDDATE = rdEnddate.SelectedDate}
                        ' kiem tra ky cong da dong chua?
                        If rep.IS_PERIODSTATUS_BY_DATE(_param) Then
                            ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thêm/sửa"), NotifyType.Error)
                            Exit Sub
                        End If
                        If (rdFromdate.SelectedDate > rdEnddate.SelectedDate) Then
                            ShowMessage(Translate("Ngày kết thúc xếp ca phải lớn hơn ngày bắt đầu!"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    If emp_id <> "" Then
                        If rep.Del_WorkSign_ByEmp(emp_id, rdFromdate.SelectedDate, rdEnddate.SelectedDate) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    End If
                    'Dim str As String = "getRadWindow().close('1');"
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    ''POPUPTOLINK
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlSignWork&group=Business")
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlSignWork&group=Business")
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand

    End Sub

#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 17/08/2017 09:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien CancelClicked tren control ctrlFindPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 09:55
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai cac control tren trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = True
                        ctrlFindEmployeePopup.IsHideTerminate = False
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try

    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 09:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien EmployeeSelected tren control ctrlFindEmployeePopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New AttendanceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                If Insertworksign Is Nothing Then
                    Insertworksign = New List(Of AT_WORKSIGNDTO)
                End If
                For idx = 0 To lstCommonEmployee.Count - 1
                    Dim item As New AT_WORKSIGNDTO
                    item.EMPLOYEE_CODE = lstCommonEmployee(idx).EMPLOYEE_CODE
                    item.VN_FULLNAME = lstCommonEmployee(idx).FULLNAME_VN
                    item.TITLE_NAME = lstCommonEmployee(idx).TITLE_NAME
                    item.ORG_ID = lstCommonEmployee(idx).ORG_ID
                    item.ORG_NAME = lstCommonEmployee(idx).ORG_NAME
                    item.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                    Insertworksign.Add(item)
                Next
                'SetGridEditRow()
                isLoadPopup = 0
            Else
                isLoadPopup = 0
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region


End Class

