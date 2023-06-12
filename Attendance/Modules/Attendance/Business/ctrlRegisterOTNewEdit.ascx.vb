Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlRegisterOTNewEdit
    Inherits CommonView

    ''' <summary>
    ''' ctrlFindEmployeePopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

    ''' <summary>
    ''' ctrlFindSigner
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup

    ''' <summary>
    ''' MustAuthorize
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Property MustAuthorize As Boolean = False

    ''' <summary>
    ''' AjaxManager
    ''' </summary>
    ''' <remarks></remarks>
    Public WithEvents AjaxManager As RadAjaxManager

    ''' <summary>
    ''' AjaxManagerId
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AjaxManagerId As String

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As Attendance.AttendanceBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Attendance.AttendanceBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    ''' <summary>
    ''' isLoadPopup
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
    ''' isEdit
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property isEdit As Boolean
        Get
            Return ViewState(Me.ID & "_isEdit")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isEdit") = value
        End Set
    End Property

    ''' <summary>
    ''' Employee_id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Employee_id As Integer
        Get
            Return ViewState(Me.ID & "_Employee_id")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_Employee_id") = value
        End Set
    End Property

    ''' <summary>
    ''' Employee_Code
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Employee_Code As String
        Get
            Return ViewState(Me.ID & "_Employee_Code")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Employee_Code") = value
        End Set
    End Property

    ''' <summary>
    ''' _Value
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property _Value As Decimal?
        Get
            Return ViewState(Me.ID & "_Value")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_Value") = value
        End Set
    End Property

    ''' <summary>
    ''' RegisterOT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RegisterOT As AT_REGISTER_OTDTO
        Get
            Return ViewState(Me.ID & "_objRegisterOT")
        End Get
        Set(ByVal value As AT_REGISTER_OTDTO)
            ViewState(Me.ID & "_objRegisterOT") = value
        End Set
    End Property

    ''' <summary>
    ''' RegisterOTList
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RegisterOTList As List(Of AT_REGISTER_OTDTO)
        Get
            Return ViewState(Me.ID & "_objRegisterOTList")
        End Get
        Set(ByVal value As List(Of AT_REGISTER_OTDTO))
            ViewState(Me.ID & "_objRegisterOTList") = value
        End Set
    End Property

    ''' <summary>
    ''' ValueHS_OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueHS_OT As Decimal
        Get
            Return ViewState(Me.ID & "_ValueHS_OT")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueHS_OT") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView, la ham load du lieu, control states cua usercontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'isplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            SetGridFilter(rgWorkschedule)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgWorkschedule.AllowCustomPaging = True
            rgWorkschedule.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            GetDataCombo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel)

            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Using rep As New AttendanceRepository
                Message = Request.Params("VIEW")

                If Not IsPostBack Then
                    If Request.Params("periodid") IsNot Nothing AndAlso Request.Params("periodid").ToString <> "" Then
                        Dim periodid = Request.Params("periodid")
                        Dim period = rep.LOAD_PERIODByID(New AT_PERIODDTO With {.PERIOD_ID = periodid})
                        'rdregisterDate.SelectedDate = period.START_DATE
                        rdregisterDate.MinDate = period.START_DATE
                        rdregisterDate.MaxDate = period.END_DATE
                    End If
                End If

                Select Case Message
                    Case "TRUE"
                        Dim obj As New AT_REGISTER_OTDTO
                        obj.ID = Decimal.Parse(Request.Params("ID"))
                        CurrentState = CommonMessage.STATE_EDIT
                        obj = rep.GetRegisterById(obj.ID)

                        If obj IsNot Nothing Then
                            rgWorkschedule.Enabled = False
                            RegisterOTList = New List(Of AT_REGISTER_OTDTO)
                            Dim item As New AT_REGISTER_OTDTO
                            item.EMPLOYEE_CODE = obj.EMPLOYEE_CODE
                            item.VN_FULLNAME = obj.VN_FULLNAME
                            item.TITLE_NAME = obj.TITLE_NAME
                            item.ORG_ID = obj.ORG_ID
                            item.ORG_NAME = obj.ORG_NAME
                            item.EMPLOYEE_ID = obj.EMPLOYEE_ID
                            RegisterOTList.Add(item)

                            Employee_id = obj.EMPLOYEE_ID
                            Employee_Code = obj.EMPLOYEE_CODE
                            rdregisterDate.SelectedDate = obj.WORKINGDAY
                            txtTuGio.SelectedDate = obj.FROM_HOUR
                            txtDenGio.SelectedDate = obj.TO_HOUR
                            txtGhiChu.Text = obj.NOTE
                            cbohs_ot.SelectedValue = obj.HS_OT
                            _Value = obj.ID
                        End If
                End Select
            End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly load tat ca thong tin ve to chức khi click button [hủy] o popup ctrlFindPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim obj As AT_REGISTER_OTDTO
        Dim rep As New AttendanceRepository
        Dim gCODE As String = ""
        Dim gstatus As Integer = 0
        Dim sAction As String
        Dim lstEmp As New List(Of EmployeeDTO)
        Dim lstEmployeeCode As String = ""

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE

                    If Not Page.IsValid Then
                        ExcuteScript("Resize", "setDefaultSize()")
                        Exit Sub
                    End If

                    If RegisterOTList() Is Nothing Then
                        ShowMessage(Translate("Vui lòng chọn nhân viên cần đăng ký làm thêm!"), NotifyType.Error)
                        Exit Sub
                    End If

                    For index = 0 To RegisterOTList.Count - 1
                        lstEmp.Add(New EmployeeDTO With {.ID = Employee_id,
                                                    .EMPLOYEE_CODE = RegisterOTList(index).EMPLOYEE_CODE,
                                                    .FULLNAME_VN = RegisterOTList(index).VN_FULLNAME})
                    Next

                    If Not AttendanceRepositoryStatic.Instance.CheckPeriodClose(lstEmp.Select(Function(f) f.ID).ToList,
                                                                                rdregisterDate.SelectedDate, rdregisterDate.SelectedDate, sAction) Then
                        ShowMessage(Translate(sAction), NotifyType.Warning)
                        Exit Sub
                    End If

                    If _Value.HasValue Then
                        obj = New AT_REGISTER_OTDTO
                        obj.ID = _Value.Value
                        obj.EMPLOYEE_CODE = Employee_Code
                        obj.WORKINGDAY = rdregisterDate.SelectedDate
                        obj.FROM_HOUR = rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtTuGio.SelectedTime.Value.TotalHours))
                        obj.TO_HOUR = rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtDenGio.SelectedTime.Value.TotalHours))

                        If obj.TO_HOUR < obj.FROM_HOUR Then
                            obj.TO_HOUR = rdregisterDate.SelectedDate.Value.AddDays(1).AddHours(Double.Parse(txtDenGio.SelectedTime.Value.TotalHours))
                        Else
                            obj.TO_HOUR = rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtDenGio.SelectedTime.Value.TotalHours))
                        End If

                        If _Value <= 0 Then
                            obj.ORG_ID = Me.RegisterOT.ORG_ID
                        End If

                        obj.TYPE_INPUT = True
                        obj.TYPE_OT = 0
                        obj.NOTE = txtGhiChu.Text.Trim
                        obj.HS_OT = Decimal.Parse(cbohs_ot.SelectedValue)
                        obj.IS_NB = cboIs_nb.Checked

                        If rep.CheckImporAddNewtOT(obj) Then
                            If rep.ModifyRegisterOT(obj, gstatus) Then
                                'Dim str As String = "getRadWindow().Close('1');"
                                'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                ''POPUPTOLINK
                                Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlRegisterOT&group=Business")
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                            End If
                        Else
                            ShowMessage("Bạn đang đăng ký có thời gian bị trùng", NotifyType.Warning)
                            Exit Sub
                        End If
                    Else
                        obj = New AT_REGISTER_OTDTO
                        obj.WORKINGDAY = rdregisterDate.SelectedDate
                        obj.FROM_HOUR = rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtTuGio.SelectedTime.Value.TotalHours))
                        obj.TO_HOUR = rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtDenGio.SelectedTime.Value.TotalHours))

                        If obj.TO_HOUR < obj.FROM_HOUR Then
                            obj.TO_HOUR = rdregisterDate.SelectedDate.Value.AddDays(1).AddHours(Double.Parse(txtDenGio.SelectedTime.Value.TotalHours))
                        Else
                            obj.TO_HOUR = rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtDenGio.SelectedTime.Value.TotalHours))
                        End If

                        If _Value <= 0 Then
                            obj.ORG_ID = Me.RegisterOT.ORG_ID
                        End If

                        obj.TYPE_INPUT = True
                        obj.TYPE_OT = 0
                        obj.NOTE = txtGhiChu.Text.Trim
                        obj.HS_OT = Decimal.Parse(cbohs_ot.SelectedValue)
                        obj.IS_NB = cboIs_nb.Checked

                        If rep.CheckDataListImportAddNew(RegisterOTList, obj, lstEmployeeCode) Then
                            If rep.InsertDataRegisterOT(RegisterOTList, obj, gstatus) Then
                                'Dim str As String = "getRadWindow().Close('1');"
                                'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                ''POPUPTOLINK
                                Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlRegisterOT&group=Business")
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                            End If
                        Else
                            ShowMessage("Nhân viên có thời gian đăng ký bị ghi đè:" & lstEmployeeCode, NotifyType.Warning)
                            Exit Sub
                        End If
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlRegisterOT&group=Business")
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event ItemCommand cho Grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgWorkschedule_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgWorkschedule.ItemCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
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
                                    Dim at_item As AT_REGISTER_OTDTO = RegisterOTList.Where(Function(s) s.EMPLOYEE_CODE = item.GetDataKeyValue("EMPLOYEE_CODE")).FirstOrDefault
                                    If (at_item IsNot Nothing) Then
                                        RegisterOTList.Remove(at_item)
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
                    rgWorkschedule.VirtualItemCount = RegisterOTList.Count
                    rgWorkschedule.DataSource = RegisterOTList()
                    rgWorkschedule.Rebind()
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgWorkschedule_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgWorkschedule.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If RegisterOTList() Is Nothing Then
                rgWorkschedule.VirtualItemCount = 0
                rgWorkschedule.DataSource = New List(Of String)
            Else
                'check employee exist in the rad gird
                If (rgWorkschedule.VirtualItemCount > 0) Then
                    For idx = 0 To rgWorkschedule.VirtualItemCount - 1
                        Dim item As GridDataItem = rgWorkschedule.Items(idx)
                        Dim at_item As List(Of AT_REGISTER_OTDTO) = RegisterOTList.Where(Function(s) s.EMPLOYEE_CODE = item.GetDataKeyValue("EMPLOYEE_CODE")).ToList()
                        If (at_item IsNot Nothing) Then
                            If (at_item.Count = 2) Then
                                RegisterOTList.Remove(at_item.Item(0))
                            End If
                        End If
                    Next
                End If

                rgWorkschedule.VirtualItemCount = RegisterOTList.Count
                rgWorkschedule.DataSource = RegisterOTList()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Kiem tra gia tri dang đươc chọn cua combox HỆ SỐ LÀM THÊM có tồn tại hoặc bị ngừng ap dụng hay không?
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cval_cbohs_ot_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cval_cbohs_ot.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository

        Try
            If String.IsNullOrEmpty(cbohs_ot.SelectedValue) Then
                args.IsValid = False
                ClearControlValue(cbohs_ot)
                Exit Sub
            End If

            ValueHS_OT = cbohs_ot.SelectedValue
            ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
            Dim dto As New OT_OTHERLIST_DTO
            Dim list As New List(Of OT_OTHERLIST_DTO)

            dto.ID = Convert.ToDecimal(cbohs_ot.SelectedValue)
            list.Add(dto)

            ListComboData.GET_LIST_HS_OT = True
            ListComboData.LIST_LIST_HS_OT = list

            If rep.ValidateCombobox(ListComboData) Then
                args.IsValid = True
            Else
                args.IsValid = False
                cbohs_ot.ClearSelection()
                rep.GetComboboxData(ListComboData)
                FillRadCombobox(cbohs_ot, ListComboData.LIST_LIST_HS_OT, "CODE", "ID", True)
                cbohs_ot.SelectedIndex = 0
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = True
                        ctrlFindEmployeePopup.IsHideTerminate = True
                    End If
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly load tat ca thong tin ve nhân viên khi click button [chọn] o popup ctrlFindEmployeePopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New AttendanceBusinessClient

        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))

            If lstCommonEmployee.Count <> 0 Then
                If RegisterOTList Is Nothing Then
                    RegisterOTList = New List(Of AT_REGISTER_OTDTO)
                End If
                'RegisterOTList = New List(Of AT_REGISTER_OTDTO)
                For idx = 0 To lstCommonEmployee.Count - 1
                    Dim item As New AT_REGISTER_OTDTO
                    item.EMPLOYEE_CODE = lstCommonEmployee(idx).EMPLOYEE_CODE
                    item.VN_FULLNAME = lstCommonEmployee(idx).FULLNAME_VN
                    item.TITLE_NAME = lstCommonEmployee(idx).TITLE_NAME
                    item.ORG_ID = lstCommonEmployee(idx).ORG_ID
                    item.ORG_NAME = lstCommonEmployee(idx).ORG_NAME
                    item.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                    RegisterOTList.Add(item)
                Next
                isLoadPopup = 0
            Else
                isLoadPopup = 0
            End If

            rgWorkschedule.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' load data cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository

        Try
            If ListComboData Is Nothing Then
                ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
                ListComboData.GET_LIST_HS_OT = True
                rep.GetComboboxData(ListComboData)
            End If

            FillRadCombobox(cbohs_ot, ListComboData.LIST_LIST_HS_OT, "CODE", "ID", True)

            If ListComboData.LIST_LIST_HS_OT.Count > 0 Then
                cbohs_ot.SelectedIndex = 0
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class

