Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlSignWorkNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()

#Region "Property"

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
    ''' <summary>
    ''' periodid
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property periodid As Integer
        Get
            Return ViewState(Me.ID & "_periodid")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_periodid") = value
        End Set
    End Property
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
    ''' <summary>
    ''' Insertworksign
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
    ''' <summary>
    ''' ghi de phuong thuc viewload
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
    ''' <summary>
    ''' Ghi de phuong thuc khoi tao
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SetGridFilter(rgWorkschedule)
            'AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            'AjaxManagerId = AjaxManager.ClientID
            rgWorkschedule.AllowCustomPaging = True
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Ghi de phuong thuc BindData bind du lieu cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Phuong thuc khoi tao, thiet lap cac control tren trang: toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Lam moi, thiet lap cac thanh phan tren trang
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
                    Using rep As New AttendanceRepository
                        Dim period = rep.LOAD_PERIODByID(New AT_PERIODDTO With {.PERIOD_ID = periodid})
                        'rdFromdate.SelectedDate = period.START_DATE
                        'rdEnddate.SelectedDate = period.END_DATE
                        rdFromdate.MinDate = period.START_DATE
                        rdFromdate.MaxDate = period.END_DATE
                        rdEnddate.MinDate = period.START_DATE
                        rdEnddate.MaxDate = period.END_DATE
                    End Using
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

#Region "Event"
    ''' <summary>
    ''' Xu ly su kien Command khi click cac item tren toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objWorksign As AT_WORKSIGNDTO
        Dim rep As New AttendanceRepository
        Dim gCODE As String = ""
        Dim gstatus As Integer = 0
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim startdate As New Date
                    Dim enddate As New Date
                    If Insertworksign Is Nothing Then
                        ShowMessage(Translate("Vui lòng chọn nhân viên cần xếp ca!"), NotifyType.Error)
                        Exit Sub
                    End If
                    ' For index = 0 To Insertworksign.Count - 1
                    If (rdFromdate.SelectedDate.HasValue) Then
                        startdate = rdFromdate.SelectedDate
                    Else
                        ShowMessage(Translate("Vui lòng chọn ngày bắt đầu xếp ca!"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If (rdEnddate.SelectedDate.HasValue) Then
                        enddate = rdEnddate.SelectedDate
                    Else
                        ShowMessage(Translate("Vui lòng chọn ngày kết thúc xếp ca!"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If (startdate > enddate) Then
                        ShowMessage(Translate("Ngày kết thúc xếp ca phải lớn hơn ngày bắt đầu!"), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim _param = New ParamDTO With {.EMPLOYEE_ID = Insertworksign(0).EMPLOYEE_ID, _
                                                    .FROMDATE = startdate, _
                                                    .ENDDATE = enddate}
                    ' kiem tra ky cong da dong chua?
                    If rep.IS_PERIODSTATUS_BY_DATE(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thêm/sửa"), NotifyType.Error)
                        Exit Sub
                    End If

                    objWorksign = New AT_WORKSIGNDTO
                    objWorksign.PERIOD_ID = periodid
                    objWorksign.WORKINGDAY = startdate
                    objWorksign.SHIFT_ID = cboSign.SelectedValue
                    Dim shift_code = (From p In ListComboData.LIST_LIST_SHIFT Where p.ID = cboSign.SelectedValue Select p.CODE).FirstOrDefault
                    objWorksign.SHIFT_CODE = shift_code
                    Dim at_shift As New AT_SHIFTDTO
                    at_shift.ID = cboSign.SelectedValue
                    at_shift.ACTFLG = "A"
                    'Check tồn tại và áp dụng
                    If (Not rep.ValidateAT_SHIFT(at_shift)) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                        GetDataCombo()
                        Exit Sub
                    End If
                    rep.InsertWorkSign(Insertworksign, objWorksign, startdate, rdEnddate.SelectedDate, gstatus)
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
    ''' <lastupdate>
    ''' 17/08/2017 09:55
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien ItemCommand cua rgWorkschedule
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgWorkschedule_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgWorkschedule.ItemCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case e.CommandName
                Case "FindEmployee"
                    isLoadPopup = 1
                    UpdateControlState()
                    ctrlFindEmployeePopup.Show()
                Case "DeleteEmployee"
                    'Lấy bản ghi đã chọn để xóa
                    If (rgWorkschedule.VirtualItemCount > 0) Then
                        If (rgWorkschedule.SelectedItems.Count = 0) Then
                            'thông báo chưa chọn bản ghi nào
                            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Else
                            'Xóa bản ghi khỏi rag grid và List Insert
                            For idx = 0 To rgWorkschedule.SelectedItems.Count - 1
                                Dim item As GridDataItem = rgWorkschedule.SelectedItems(idx)
                                If (item IsNot Nothing) Then
                                    Dim at_item As AT_WORKSIGNDTO = Insertworksign.Where(Function(s) s.EMPLOYEE_CODE = item.GetDataKeyValue("EMPLOYEE_CODE")).FirstOrDefault
                                    If (at_item IsNot Nothing) Then
                                        Insertworksign.Remove(at_item)
                                    End If
                                End If
                            Next
                            rgWorkschedule.MasterTableView.FilterExpression = ""
                            Dim column1 As GridColumn = rgWorkschedule.MasterTableView.GetColumnSafe("EMPLOYEE_CODE")
                            column1.CurrentFilterFunction = GridKnownFunction.NoFilter
                            column1.CurrentFilterValue = ""
                            Dim column2 As GridColumn = rgWorkschedule.MasterTableView.GetColumnSafe("VN_FULLNAME")
                            column2.CurrentFilterFunction = GridKnownFunction.NoFilter
                            column2.CurrentFilterValue = ""
                            Dim column3 As GridColumn = rgWorkschedule.MasterTableView.GetColumnSafe("TITLE_NAME")
                            column3.CurrentFilterFunction = GridKnownFunction.NoFilter
                            column3.CurrentFilterValue = ""
                            Dim column4 As GridColumn = rgWorkschedule.MasterTableView.GetColumnSafe("ORG_NAME")
                            column4.CurrentFilterFunction = GridKnownFunction.NoFilter
                            column4.CurrentFilterValue = ""
                        End If
                    End If
                    rgWorkschedule.VirtualItemCount = Insertworksign.Count
                    rgWorkschedule.DataSource = Insertworksign
                    rgWorkschedule.Rebind()
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
    ''' <lastupdate>
    ''' 17/08/2017 09:55
    ''' </lastupdate>
    ''' <summary>
    ''' Khi click Huy popup thi gan isLoadPopup = 0
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
    ''' Lay danh sach combobox
    ''' fill du lieu cho combobox tren trang
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_LIST_SHIFT = True
                rep.GetComboboxData(ListComboData)
            End If
            FillRadCombobox(cboSign, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", True)
            If ListComboData.LIST_LIST_SHIFT.Count > 0 Then
                cboSign.SelectedIndex = 0
            End If
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
    ''' Xu ly su kien EmployeeSelected cho control ctrlEmployeePopup
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
                    item.ORG_DESC = lstCommonEmployee(idx).ORG_DESC
                    item.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                    Insertworksign.Add(item)
                Next
                'SetGridEditRow()
                isLoadPopup = 0
            Else
                isLoadPopup = 0
            End If
            rgWorkschedule.Rebind()
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
    ''' Xu ly su kien NeedDataSource cho rad grid rgWorkschedule
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgWorkschedule.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ' SearchData()
            If Insertworksign Is Nothing Then
                rgWorkschedule.VirtualItemCount = 0
                rgWorkschedule.DataSource = New List(Of String)
            Else
                'check employee exist in the rad gird?

                If (rgWorkschedule.VirtualItemCount > 0) Then
                    For idx = 0 To rgWorkschedule.VirtualItemCount - 1
                        Dim item As GridDataItem = rgWorkschedule.Items(idx)
                        Dim at_item As List(Of AT_WORKSIGNDTO) = Insertworksign.Where(Function(s) s.EMPLOYEE_CODE = item.GetDataKeyValue("EMPLOYEE_CODE")).ToList()
                        If (at_item IsNot Nothing) Then
                            If (at_item.Count = 2) Then
                                Insertworksign.Remove(at_item.Item(0))
                            End If
                        End If
                    Next
                End If
                rgWorkschedule.VirtualItemCount = Insertworksign.Count
                rgWorkschedule.DataSource = Insertworksign
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện bắt validation cho combobox cboSign </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cusCboSign_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusCboSign.ServerValidate
        Dim rep As New AttendanceRepository
        Dim validate As New AT_SHIFTDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If (cboSign.SelectedValue = "") Then
                args.IsValid = False
            Else
                If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                    validate.ID = cboSign.SelectedItem.Value.Trim
                    validate.ACTFLG = "A"
                    args.IsValid = rep.ValidateAT_SHIFT(validate)
                End If
            End If

            If Not args.IsValid Then
                If ListComboData Is Nothing Then
                    ListComboData = New ComboBoxDataDTO
                    ListComboData.GET_LIST_SHIFT = True
                    rep.GetComboboxData(ListComboData)
                End If
                FillRadCombobox(cboSign, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", True)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgWorkschedule_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgWorkschedule.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC").ToString)
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region


End Class

