Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlLeaveApproveAPI
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Dim psp As New AttendanceStoreProcedure
#Region "Constant"
    Private Const CONST_DUYET_NGHI_CODE As String = "DUYETNGHI"
    Private Const CONST_GROUP_MAIL As String = "Attendance"
    Private Const FIELD_APPROVER As String = "{APPROVER}"
    Private Const FIELD_EMPLOYEE As String = "{Full_name}"
    Private Const FIELD_LEAVE_FROM As String = "{LEAVE_FROM}"
    Private Const FIELD_LEAVE_TO As String = "{LEAVE_TO}"
    Private Const FIELD_DAY_NUM As String = "{DAY_NUM}"
    Private Const FIELD_LOAI_NGHI As String = "{LOAI_NGHI}"
    Private Const FIELD_GENDER As String = "{GENDER}"
    Private Const FIELD_EMP_ID As String = "{EMP_ID}"
    Private Const FIELD_ID As String = "ID"
#End Region
#Region "Property"
    Public Property IDCtrl As String
        Get
            Return ViewState(Me.ID & "_IDCtrl")
        End Get
        Set(value As String)
            ViewState(Me.ID & "_IDCtrl") = value
        End Set
    End Property
    Public ReadOnly Property CurrentUser As UserDTO
        Get
            Return LogHelper.CurrentUser
        End Get
    End Property
    Public ReadOnly Property ApproveProcess As String
        Get
            Return ATConstant.GSIGNCODE_LEAVE
        End Get
    End Property

    Protected Property EmployeeDto As DataTable
        Get
            Return PageViewState(Me.ID & "_EmployeeDto")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_EmployeeDto") = value
        End Set
    End Property
    Protected Property ListManual As List(Of AT_TIME_MANUALDTO)
        Get
            Return PageViewState(Me.ID & "_ListFML")
        End Get
        Set(ByVal value As List(Of AT_TIME_MANUALDTO))
            PageViewState(Me.ID & "_ListFML") = value
        End Set
    End Property
    Property ListComboData As Attendance.AttendanceBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Attendance.AttendanceBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    'EmployeeID
    Property EmployeeID As Decimal
        Get
            Return ViewState(Me.ID & "_EmployeeID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_EmployeeID") = value
        End Set
    End Property

    Property Status As Decimal
        Get
            Return ViewState(Me.ID & "_Status")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Status") = value
        End Set
    End Property

    Property userType As String
        Get
            Return ViewState(Me.ID & "_userType")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_userType") = value
        End Set
    End Property

    Property dtDetail As DataTable
        Get
            Return ViewState(Me.ID & "_dtDetail")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDetail") = value
        End Set
    End Property
    Property rPH As DataRow
        Get
            Return ViewState(Me.ID & "_rPH")
        End Get
        Set(ByVal value As DataRow)
            ViewState(Me.ID & "_rPH") = value
        End Set
    End Property
    Property isFlag As Boolean
        Get
            Return ViewState(Me.ID & "_isFlag")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isFlag") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Submit, ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dsLeaveSheet As New DataSet()
        Try
            'If IsPostBack Then
            '    rtEmployee_id.Text = LogHelper.CurrentUser.EMPLOYEE_ID
            '    Using rep As New AttendanceRepository
            '        EmployeeDto = rep.GetEmployeeInfor(EmployeeID, Nothing)
            '    End Using
            'End If
            rtEmployee_id.Text = LogHelper.CurrentUser.EMPLOYEE_ID
            Dim startTime As DateTime = DateTime.UtcNow
            IDCtrl = Request.Params("id")
            Status = Request.Params("status")
            EmployeeID = Request.Params("empid")
            Message = Request.Params("VIEW")
            Dim Struct As Decimal = 1
            Dim ID_PH As Decimal = 0
            If IsNumeric(Request.Params("ID")) Then
                Struct = 0
                ID_PH = Decimal.Parse(Request.Params("ID"))
            End If
            Using rep As New AttendanceRepository
                dsLeaveSheet = rep.GetLeaveSheet_ById(ID_PH, Struct)
            End Using
            If dsLeaveSheet IsNot Nothing Then
                If dsLeaveSheet.Tables(0) IsNot Nothing Then
                    rPH = dsLeaveSheet.Tables(0).NewRow
                    If dsLeaveSheet.Tables(0).Rows.Count > 0 Then
                        rPH = dsLeaveSheet.Tables(0).Rows(0)
                    End If
                End If
                If dsLeaveSheet.Tables(1) IsNot Nothing AndAlso dtDetail Is Nothing Then
                    dtDetail = dsLeaveSheet.Tables(1).Clone()
                    dtDetail = dsLeaveSheet.Tables(1)
                End If
            End If
            FillData(Decimal.Parse(rtEmployee_id.Text.Trim))
            Select Case Message
                Case "TRUE"
                    CreateDataBinDing(1)
            End Select
        Catch ex As Exception
            Throw ex
        Finally
            dsLeaveSheet.Dispose()
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            If Utilities.ObjToString(rPH("S_CODE")) = "R" Or Utilities.ObjToString(rPH("S_CODE")) = "U" Or Utilities.ObjToString(rPH("S_CODE")) = "" Then
                RadPane1.Enabled = True
                CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
            Else
                RadPane1.Enabled = False
                CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
            End If
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    Protected Sub cbSTATUS_SHIFT_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            Dim edit = CType(sender, RadComboBox)
            If edit.Enabled = False Then
                Exit Sub
            End If
            Dim item = CType(edit.NamingContainer, GridEditableItem)
            ' If Not IsNumeric(edit.SelectedValue) Then Exit Sub
            Dim EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
            Dim LEAVE_DAY = item.GetDataKeyValue("LEAVE_DAY")
            For Each rows In dtDetail.Rows
                If rows("LEAVE_DAY") = LEAVE_DAY Then
                    rows("STATUS_SHIFT") = If(IsNumeric(edit.SelectedValue), edit.SelectedValue, 0)
                    If edit.SelectedValue IsNot Nothing AndAlso edit.SelectedValue <> "" Then
                        'rows("DAY_NUM") = 0.5
                        rows("DAY_NUM") = Decimal.Parse(rows("SHIFT_DAY")) / 2
                    Else
                        'rows("DAY_NUM") = 1
                        rows("DAY_NUM") = Decimal.Parse(rows("SHIFT_DAY"))
                    End If
                    Exit For
                End If
            Next
            rgData.Rebind()
            For Each items As GridDataItem In rgData.MasterTableView.Items
                items.Edit = True
            Next
            rgData.MasterTableView.Rebind()
            Cal_DayLeaveSheet()

            ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "text", "IsBlock()", True)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim objValidate As New AT_LEAVESHEETDTO
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        'check so ngay dang ky nghi
                        If Not IsNumeric(rnDAY_NUM.Value) OrElse rnDAY_NUM.Value <= 0 Then
                            ShowMessage(Translate("Số ngày đăng ký nghỉ phải lơn hơn 0"), NotifyType.Warning)
                            Exit Sub
                        End If

                        If (isFlag = True) Then
                            Dim intBalance As Double = If(rnBALANCE.Text.Trim = "", 0, Double.Parse(rnBALANCE.Text.Trim))
                            If (rnDAY_NUM.Value > intBalance) Then
                                ShowMessage(Translate("Đã vượt quá số phép qui định, vui lòng điều chỉnh lại dữ liệu"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If

                        CreateDataBinDing(0)
                        objValidate.LEAVE_FROM = rdLEAVE_FROM.SelectedDate
                        objValidate.LEAVE_TO = rdLEAVE_TO.SelectedDate
                        objValidate.ID = Utilities.ObjToDecima(rPH("ID"))
                        objValidate.EMPLOYEE_ID = CDec(rtEmployee_id.Text)
                        If (New AttendanceBusinessClient).ValidateLeaveSheetDetail(objValidate) = False Then
                            ShowMessage(Translate("Ngày đăng ký nghỉ đã bị trùng"), NotifyType.Warning)
                            Exit Sub
                        End If
                        'If valSum.Page.IsValid Then
                        If Utilities.ObjToString(rPH("S_CODE")) = "A" Then 'TRANG THAI approve
                            ShowMessage(Translate("Đơn đã Phê duyệt. Không thể chỉnh sửa !"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If SaveDB() Then
                            Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistration")
                        Else
                            ShowMessage(Translate("Xảy ra lỗi"), NotifyType.Error)
                        End If
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule')")
                    End If
                Case CommonMessage.TOOLBARITEM_SUBMIT
                    'Select Case Utilities.ObjToString(rPH("S_CODE"))
                    '    Case "W"
                    '        ShowMessage(Translate("Đơn đang Chờ phê duyệt. Vui lòng thử lại !"), NotifyType.Warning)
                    '        Exit Sub
                    '    Case "A"
                    '        ShowMessage(Translate("Đơn đã Phê duyệt. Vui lòng thử lại !"), NotifyType.Warning)
                    '        Exit Sub
                    'End Select
                    If rdLEAVE_FROM.SelectedDate IsNot Nothing Then
                        If IS_PERIOD_CLOSE(rdLEAVE_FROM.SelectedDate) = False Then
                            ShowMessage(Translate("Kì công đã đóng, xin kiểm tra lại !"), NotifyType.Warning)
                            Exit Sub
                        End If
                    End If
                    If rdLEAVE_TO.SelectedDate IsNot Nothing Then
                        If IS_PERIOD_CLOSE(rdLEAVE_TO.SelectedDate) = False Then
                            ShowMessage(Translate("Kì công đã đóng, xin kiểm tra lại !"), NotifyType.Warning)
                            Exit Sub
                        End If
                    End If

                    If Utilities.ObjToString(rPH("S_CODE")) = "R" Then
                        ctrlMessageBox.MessageText = Translate("Bạn có muốn gửi phê duyệt?")
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_SUBMIT
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate("Chỉ gửi đơn ở trạng thái Chưa gửi duyệt. Vui lòng thử lại !"), NotifyType.Warning)
                        Exit Sub
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=" + IDCtrl)
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim objValidate As New AT_LEAVESHEETDTO
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_SUBMIT And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                SetData_Controls(objValidate, 0)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub rgData_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub rgData_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemCreated
        If TypeOf e.Item Is GridDataItem Then
            Dim control As GridDataItem = CType(e.Item, GridDataItem)
            Dim combo As RadComboBox = CType(control.FindControl("cboLeaveValue"), RadComboBox) ' CType(insertItem("cboLeaveValue").FindControl("radComboBoxOccCode"), RadComboBox)

            If combo IsNot Nothing Then

                combo.AutoPostBack = True
                AddHandler combo.SelectedIndexChanged, AddressOf combo_SelectedIndexChanged
                combo.Enabled = False
            End If
        End If
        If Not String.IsNullOrEmpty(hidID.Value) AndAlso hidID.Value > 0 Then
            Dim cmdItem As GridItem = rgData.MasterTableView.GetItems(GridItemType.CommandItem)(0)
            Dim editDetail As RadButton = CType(cmdItem.FindControl("btnEditDetail"), RadButton)
            editDetail.Enabled = False
        End If
    End Sub

    Protected Sub combo_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)
        Try

            ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "showDetail('block');", True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rgData_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rgData.ItemDataBound
        Dim cbo As New RadComboBox
        Dim arr As New ArrayList()
        Try
            If e.Item.Edit Then
                Dim edit = CType(e.Item, GridEditableItem)
                Dim item As GridDataItem = CType(e.Item, GridDataItem)
                cbo = CType(edit.FindControl("cbSTATUS_SHIFT"), RadComboBox)
                arr.Add(New DictionaryEntry("", Nothing))
                arr.Add(New DictionaryEntry("Đầu ca", 1))
                arr.Add(New DictionaryEntry("Cuối ca", 2))
                With cbo
                    .DataSource = arr
                    .DataValueField = "Value"
                    .DataTextField = "Key"
                    cbo.DataBind()
                    .SelectedIndex = 0
                End With
                SetDataToGrid(edit)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            cbo.Dispose()
            arr = Nothing
        End Try
    End Sub

    Private Sub rgData_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgData.ItemCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case e.CommandName
                Case "EditDetail"
                    For Each item As GridDataItem In rgData.MasterTableView.Items
                        item.Edit = True
                    Next
                    rgData.MasterTableView.Rebind()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "showDetail('');", True)

            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rdLEAVE_FROM_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdLEAVE_FROM.SelectedDateChanged, rdLEAVE_TO.SelectedDateChanged
        Try
            If (Not IsDate(rdLEAVE_FROM.SelectedDate) OrElse Not IsDate(rdLEAVE_TO.SelectedDate) OrElse dtDetail Is Nothing OrElse Not IsNumeric(rtEmployee_id.Text)) Then Exit Sub
            GetLeaveSheet_Detail()
            Cal_DayLeaveSheet()
            Cal_DayEntitlement()

            Dim store As New AttendanceStoreProcedure
            Dim dtSourceNB = store.GET_INFO_NGHIBU(Decimal.Parse(rtEmployee_id.Text.Trim), rdLEAVE_FROM.SelectedDate)
            If dtSourceNB.Rows.Count > 0 Then
                txtCUR_HAVE.Text = If(dtSourceNB.Rows(0)("CUR_HAVE") Is Nothing, 0, CDec(dtSourceNB.Rows(0)("CUR_HAVE").ToString()))
            Else
                txtCUR_HAVE.Text = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cbMANUAL_ID_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbMANUAL_ID.SelectedIndexChanged
        Try
            If (Not IsDate(rdLEAVE_FROM.SelectedDate) OrElse Not IsDate(rdLEAVE_TO.SelectedDate) OrElse dtDetail Is Nothing OrElse Not IsNumeric(rtEmployee_id.Text)) Then Exit Sub
            GetLeaveSheet_Detail()
            Cal_DayLeaveSheet()
            Cal_DayEntitlement()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal fromDate As Date? = Nothing, Optional ByVal toDate As Date? = Nothing, Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_LEAVESHEETDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgData.VirtualItemCount = dtDetail.Rows.Count
            rgData.DataSource = dtDetail
            Cal_DayLeaveSheet()
        Catch ex As Exception
        End Try
        Return New DataTable()
    End Function

    Private Sub Cal_DayLeaveSheet()
        Try
            Dim sumDay As Decimal = dtDetail.Compute("SUM(DAY_NUM)", "1=1")
            rnDAY_NUM.NumberFormat.AllowRounding = False
            rnDAY_NUM.NumberFormat.DecimalDigits = 2
            rnDAY_NUM.Value = sumDay

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Cal_DayEntitlement()
        Try
            If rtEmployee_id.Text = "" Or rdLEAVE_FROM.SelectedDate Is Nothing Then
                Exit Sub
            End If

            Dim dtSourceEntitlement As New DataTable()
            Dim dtManual As New DataTable()
            Dim employee_id As Decimal = Decimal.Parse(rtEmployee_id.Text.Trim)

            Try
                Using rep As New AttendanceRepository
                    Dim manualID As Decimal = Decimal.Parse(cbMANUAL_ID.SelectedValue)
                    dtManual = rep.GET_MANUAL_BY_ID(manualID)
                End Using

                If dtManual.Rows.Count > 0 Then
                    Dim strCode As String = dtManual.Rows(0)("CODE").ToString()
                    Dim strMorning As String = If(dtManual.Rows(0)("MORNING_ID") IsNot Nothing, dtManual.Rows(0)("MORNING_ID").ToString(), Nothing)
                    Dim strAfternoon As String = If(dtManual.Rows(0)("AFTERNOON_ID") IsNot Nothing, dtManual.Rows(0)("AFTERNOON_ID").ToString(), Nothing)

                    If (strCode.ToUpper() = "P" And (strMorning = "251" Or strAfternoon = "251")) Then
                        Using rep As New AttendanceRepository
                            dtSourceEntitlement = rep.GET_INFO_PHEPNAM(employee_id, rdLEAVE_FROM.SelectedDate)
                            If dtSourceEntitlement.Rows.Count > 0 Then
                                isFlag = True
                                rnCUR_HAVE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CHE_DO") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CHE_DO").ToString())
                                rnSENIORITYHAVE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_THAM_NIEN") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_THAM_NIEN").ToString())
                                rnPREV_HAVE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CU_CHUYEN_QUA") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CU_CHUYEN_QUA").ToString())
                                rnCUR_USED.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CU_DA_NGHI") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CU_DA_NGHI").ToString())
                                rnCUR_HAVE_INMONTH.Text = If(dtSourceEntitlement.Rows(0)("PHEP_THANG_LVIEC") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_THANG_LVIEC").ToString())
                                rnPREVTOTAL_HAVE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CU_CON_HLUC") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CU_CON_HLUC").ToString())
                                rtCUR_USED_INMONTH.Text = If(dtSourceEntitlement.Rows(0)("PHEP_DA_NGHI") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_DA_NGHI").ToString())
                                rnCUR_DANGKY.Text = If(dtSourceEntitlement.Rows(0)("PHEP_DA_DANGKY") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_DA_DANGKY").ToString())
                                rnBALANCE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CONLAI") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CONLAI").ToString())
                            Else
                                Clearn__DayEntitlement()
                            End If
                        End Using
                    Else
                        Clearn__DayEntitlement()
                    End If
                Else
                    Clearn__DayEntitlement()
                End If
            Catch ex As Exception
                Throw ex
            Finally
                dtSourceEntitlement.Dispose()
                dtManual.Dispose()
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Clearn__DayEntitlement()
        isFlag = False
        rnCUR_HAVE.Text = ""
        rnSENIORITYHAVE.Text = ""
        rnPREV_HAVE.Text = ""
        rnCUR_USED.Text = ""
        rnCUR_HAVE_INMONTH.Text = ""
        rnPREVTOTAL_HAVE.Text = ""
        rtCUR_USED_INMONTH.Text = ""
        rnCUR_DANGKY.Text = ""
        rnBALANCE.Text = ""
    End Sub

    Public Sub CreateDataBinDing(ByVal Mode As Decimal)
        '1 set data in list control
        '0 get data to list control
        Dim id As String
        Dim suffixId As String
        Select Case Mode
            Case 1
                For Each ctrs As Control In RadPane1.Controls
                    If ctrs.ID Is Nothing Then
                        Continue For
                    End If
                    Try
                        id = ctrs.ID
                        suffixId = id.ToUpper.Substring(0, 2)
                        Select Case suffixId
                            Case "cb".ToUpper
                                CType(ctrs, RadComboBox).SelectedValue = rPH(id.ToUpper.Substring(2))
                            Case "rt".ToUpper
                                CType(ctrs, RadTextBox).Text = rPH(id.ToUpper.Substring(2))
                            Case "rn".ToUpper
                                If TypeOf (ctrs) Is RadNumericTextBox Then
                                    CType(ctrs, RadNumericTextBox).Value = rPH(id.ToUpper.Substring(2))
                                ElseIf TypeOf (ctrs) Is RadTextBox Then
                                    CType(ctrs, RadTextBox).Text = rPH(id.ToUpper.Substring(2))
                                End If
                            Case "rd".ToUpper
                                CType(ctrs, RadDatePicker).SelectedDate = rPH(id.ToUpper.Substring(2))
                            Case Else
                                Continue For
                        End Select
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
            Case 0
                For Each ctrs As Control In RadPane1.Controls
                    If ctrs.ID Is Nothing Then
                        Continue For
                    End If
                    Try
                        id = ctrs.ID
                        suffixId = id.ToUpper.Substring(0, 2)
                        Select Case suffixId
                            Case "cb".ToUpper
                                If Not String.IsNullOrEmpty(CType(ctrs, RadComboBox).SelectedValue) Then
                                    rPH(id.ToUpper.Substring(2)) = CType(ctrs, RadComboBox).SelectedValue
                                End If
                            Case "rt".ToUpper
                                If Not String.IsNullOrEmpty(CType(ctrs, RadTextBox).Text) Then
                                    rPH(id.ToUpper.Substring(2)) = CType(ctrs, RadTextBox).Text
                                End If
                            Case "rn".ToUpper
                                If TypeOf (ctrs) Is RadNumericTextBox Then
                                    If CType(ctrs, RadNumericTextBox).Value.HasValue Then
                                        rPH(id.ToUpper.Substring(2)) = CType(ctrs, RadNumericTextBox).Value
                                    End If
                                ElseIf TypeOf (ctrs) Is RadTextBox Then
                                    If Not String.IsNullOrEmpty(CType(ctrs, RadTextBox).Text) Then
                                        rPH(id.ToUpper.Substring(2)) = CType(ctrs, RadTextBox).Text
                                    End If
                                End If
                            Case "rd".ToUpper
                                If CType(ctrs, RadDatePicker).SelectedDate.HasValue Then
                                    rPH(id.ToUpper.Substring(2)) = CType(ctrs, RadDatePicker).SelectedDate.Value
                                End If
                            Case Else
                                Continue For
                        End Select
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
        End Select

    End Sub

    Private Sub SetDataToGrid(ByVal EditItem As GridEditableItem)
        Dim cbo As New RadComboBox
        Dim arr As New ArrayList()
        Try
            Dim LEAVE_DAY = EditItem.GetDataKeyValue("LEAVE_DAY")
            Dim STATUS_SHIFT As New Object
            Dim MANUAL_ID As New Object
            Dim IS_DEDUCT_SHIFT As New Object
            Dim SHIFT_DAY As New Object
            For Each rows As DataRow In dtDetail.Rows
                If rows("LEAVE_DAY") = LEAVE_DAY Then
                    STATUS_SHIFT = rows("STATUS_SHIFT")
                    MANUAL_ID = rows("MANUAL_ID")
                    IS_DEDUCT_SHIFT = rows("IS_DEDUCT_SHIFT")
                    SHIFT_DAY = rows("SHIFT_DAY")
                    Exit For
                End If
            Next
            cbo = CType(EditItem.FindControl("cbSTATUS_SHIFT"), RadComboBox)
            arr.Add(New DictionaryEntry("", Nothing))
            arr.Add(New DictionaryEntry("Đầu ca", 1))
            arr.Add(New DictionaryEntry("Cuối ca", 2))
            With cbo
                .DataSource = arr
                .DataValueField = "Value"
                .DataTextField = "Key"
                cbo.DataBind()
                .SelectedIndex = 0
            End With
            If IsNumeric(STATUS_SHIFT) Then
                cbo.SelectedValue = STATUS_SHIFT
                cbo.Enabled = If(Not IsNumeric(MANUAL_ID), False, True)
            End If
            If IS_DEDUCT_SHIFT = 0 Then
                cbo.Enabled = False
            Else
                If IsDBNull(SHIFT_DAY) = True Then
                    cbo.Enabled = False
                Else
                    If SHIFT_DAY <= 0.5 Or SHIFT_DAY > 1 Then
                        cbo.Enabled = False
                    Else
                        cbo.Enabled = True
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function SaveDB() As Boolean
        Dim rep As New AttendanceRepository
        Dim PH As DataTable = New DataTable()
        Dim dr As DataRow() = New DataRow() {rPH}
        dr(0)("STATUS") = 3 'Chờ phê duyệt
        PH = dr.CopyToDataTable()
        PH.TableName = "PH"
        Dim dsLeaveSheet As New DataSet("DATA")
        Dim CT As New DataTable()
        dsLeaveSheet.Tables.Add(PH)
        CT = dtDetail
        CT.TableName = "CT"
        'dsLeaveSheet.Tables.Remove("CT")
        dsLeaveSheet.Tables.Add(CT.Copy())
        Try
            Return rep.SaveLeaveSheet(dsLeaveSheet)
        Catch ex As Exception
            Return False
        Finally
            rep.Dispose()
            CT.Dispose()
            PH.Dispose()
        End Try
    End Function

    Private Sub GetDataCombo()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ListComboData Is Nothing Then
                ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO

                'Điều chỉnh Loại nghỉ (thêm điều kiện Loại xử lý Kiểu công: Đăng ký)
                ListComboData.GET_LIST_TYPE_MANUAL_LEAVE = True
                rep.GetComboboxData(ListComboData)
                FillRadCombobox(cbMANUAL_ID, ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE, "NAME_VN", "ID", True)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub GetLeaveSheet_Detail()
        Dim dtSource As New DataTable()
        Try
            Using rep As New AttendanceRepository
                Dim employee_id As Decimal = Decimal.Parse(rtEmployee_id.Text.Trim)
                Dim manualID As Decimal = Decimal.Parse(cbMANUAL_ID.SelectedValue)
                dtSource = rep.GetLeaveSheet_Detail_ByDate(employee_id, rdLEAVE_FROM.SelectedDate, rdLEAVE_TO.SelectedDate, manualID)
            End Using
            dtDetail = dtSource
            rgData.Rebind()
            For Each item As GridDataItem In rgData.MasterTableView.Items
                item.Edit = True
            Next
            rgData.MasterTableView.Rebind()
        Catch ex As Exception
            Throw ex
        Finally
            dtSource.Dispose()
        End Try
    End Sub

    ''' <summary>
    ''' Fill data len control theo ID
    ''' </summary>
    ''' <param name="empid">Ma nhan vien</param>
    ''' <remarks></remarks>
    Private Sub FillData(ByVal empid As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dtData As New DataTable()
        Dim dateValue = Date.Now
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New Profile.ProfileBusinessRepository
                Dim obj = rep.GetEmployeCurrentByID(New WorkingDTO With {.EMPLOYEE_ID = empid})
                If IsNumeric(obj.ID) Then
                    rtEmployee_id.Text = obj.EMPLOYEE_ID.ToString()
                End If
                rtEmployee_Name.Text = obj.EMPLOYEE_NAME
                rtEmployee_Code.Text = obj.EMPLOYEE_CODE
                rtOrg_Name.Text = obj.ORG_NAME
                If IsNumeric(obj.ORG_ID) Then
                    rtOrg_id.Text = obj.ORG_ID.ToString()
                End If
                rtTitle_Name.Text = obj.TITLE_NAME
                If IsNumeric(obj.TITLE_ID) Then
                    rtTitle_Id.Text = obj.TITLE_ID.ToString()
                End If

            End Using

        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetData_Controls(ByVal atLeave As AT_LEAVESHEETDTO, ByVal id_state As Decimal)
        Try

            'check so ngay dang ky nghi
            If Not IsNumeric(rnDAY_NUM.Value) OrElse rnDAY_NUM.Value < 1 Then
                ShowMessage(Translate("Số ngày đăng ký nghỉ phải lơn hơn 0"), NotifyType.Warning)
                Exit Sub
            End If
            rnSTATUS.Text = id_state.ToString
            CreateDataBinDing(0)
            atLeave.LEAVE_FROM = rdLEAVE_FROM.SelectedDate
            atLeave.LEAVE_TO = rdLEAVE_TO.SelectedDate
            atLeave.ID = Utilities.ObjToDecima(rPH("ID"))
            atLeave.EMPLOYEE_ID = CDec(rtEmployee_id.Text)
            If (New AttendanceBusinessClient).ValidateLeaveSheetDetail(atLeave) = False Then
                ShowMessage(Translate("Ngày đăng ký nghỉ đã bị trùng"), NotifyType.Warning)
                Exit Sub
            End If

            Dim dtCheckSendApprove As DataTable = psp.CHECK_APPROVAL(atLeave.ID)
            Dim period_id As Integer = Utilities.ObjToDecima(dtCheckSendApprove.Rows(0)("PERIOD_ID"))
            Dim sign_id As Integer = Utilities.ObjToDecima(dtCheckSendApprove.Rows(0)("SIGN_ID"))
            Dim id_group As Integer = Utilities.ObjToDecima(dtCheckSendApprove.Rows(0)("ID_REGGROUP"))
            Dim sumday As Integer = Utilities.ObjToDecima(dtCheckSendApprove.Rows(0)("SUMDAY"))

            Dim outNumber As Decimal
            Try
                Dim IAttendance As IAttendanceBusiness = New AttendanceBusinessClient()
                outNumber = IAttendance.PRI_PROCESS_APP(EmployeeID, period_id, "LEAVE", 0, sumday, sign_id, id_group, "")
            Catch ex As Exception
                ShowMessage(ex.ToString, NotifyType.Error)
            End Try

            If outNumber = 0 Then
                'sau khi hoàn thành update nội dung status thì tiến hành email
                SendEmail(atLeave.EMPLOYEE_ID, atLeave.ID)
                Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistration")
            ElseIf outNumber = 1 Then
                ShowMessage(Translate("Quy trình phê duyệt chưa được thiết lập"), NotifyType.Error)
            ElseIf outNumber = 2 Then
                ShowMessage(Translate("Thao tác xảy ra lỗi,bạn kiểm tra lại quy trình"), NotifyType.Error)
            ElseIf outNumber = 3 Then
                ShowMessage(Translate("Nhân viên chưa có thiết lập nhóm chức danh"), NotifyType.Error)
            End If

            'If SaveDB("SUBMIT") Then
            '    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistration")
            'Else
            '    ShowMessage(Translate("Xảy ra lỗi"), NotifyType.Error)
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Xử lý và kiểm tra Kỳ công đóng mở theo ngày đăng ký nghỉ
    ''' </summary>
    ''' <param name="_date"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IS_PERIOD_CLOSE(ByVal _date As Date?) As Boolean
        Dim dtData As DataTable
        Dim period_id As Integer
        Dim rs As Boolean = False
        Try
            dtData = psp.GET_PERIOD_BYDATE(_date)
            If dtData.Rows.Count > 0 Then
                period_id = Utilities.ObjToDecima(dtData.Rows(0)("PERIOD_ID"))
                Using rep As New AttendanceRepository
                    Dim check = rep.CHECK_PERIOD_CLOSE(period_id)
                    If check = 0 Then
                        rs = False
                    Else
                        rs = True
                    End If
                End Using
            End If
            Return rs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Gửi mail sau khi gửi thông tin email phê duyệt
    ''' </summary>
    ''' <param name="EmpId"></param>
    ''' <param name="Id"></param>
    Private Function SendEmail(ByVal EmpId As Decimal, ByVal Id As Decimal) As Boolean
        Dim sent As Boolean = False
        Try
            '1. Lấy thông tin approver
            Dim rep As New AttendanceRepository
            Dim objAttendance = rep.GetProcessApprovedStatusByEmpAndId(EmpId, Id)               ' lấy thông tin approve process
            Dim objApprover = rep.GetEmpId(objAttendance.EMPLOYEE_APPROVED)                     ' lấy thông tin approve
            Dim strGioiTinh = IIf(objApprover.GENDER = 565, Translate("Ông"), Translate("Bà"))

            'kiểm tra thông tin Approver
            If objApprover Is Nothing Then
                ShowMessage(Translate("Thông tin người phê duyệt không chính xác"), NotifyType.Warning)
                Exit Function
            End If

            'kiểm tra email của approver
            If String.IsNullOrEmpty(objApprover.WORK_EMAIL) Then
                ShowMessage(Translate("Email của người phê duyệt không chính xác"), NotifyType.Warning)
                Exit Function
            End If

            '2. Lấy email template
            Using repCommon As New CommonRepository
                Dim mailTemplate = repCommon.GetMailTemplateBaseCode(CONST_DUYET_NGHI_CODE, CONST_GROUP_MAIL)   'lấy thông tin mail template
                Dim leaveSheetInfo = rep.GetLeaveSheetByEmpAndLeave(EmpId, Id)                                  'lấy thông tin đơn xin nghỉ việc

                'nếu không có thông tin
                If leaveSheetInfo Is Nothing Then
                    ShowMessage(Translate("Thông tin đơn xin nghỉ việc không chính xác"), NotifyType.Warning)
                    Exit Function
                End If

                'nếu không có mấu email nào tồn tại
                If mailTemplate Is Nothing Then
                    ShowMessage(Translate("Email Template không tồn tại"), NotifyType.Warning)
                    Exit Function
                End If

                Dim repo As New CommonRepository
                Dim data = repo.GetEmployeeID(EmpId)

                'tiến hành điền mẫu email vào nội dung
                Dim content As String = mailTemplate.CONTENT

                'điền thông tin data sau khi thêm vào
                content.Replace("{ORG_NAME}", data.ORG_NAME)
                content.Replace("{TITLE_NAME}", data.TITLE_NAME)
                content.Replace(FIELD_EMPLOYEE, rtEmployee_Name.Text)
                content.Replace(FIELD_LEAVE_FROM, leaveSheetInfo.LEAVE_FROM)
                content.Replace(FIELD_LEAVE_TO, leaveSheetInfo.LEAVE_TO)
                content.Replace(FIELD_LOAI_NGHI, cbMANUAL_ID.Text)
                content.Replace(FIELD_APPROVER, objApprover.FULLNAME_VN)
                content.Replace(FIELD_GENDER, strGioiTinh)
                content.Replace(FIELD_DAY_NUM, leaveSheetInfo.DAY_NUM)
                content.Replace(FIELD_ID, Id)
                content.Replace(FIELD_EMP_ID, EmpId)

                'gửi mail
                sent = sendEmailByServerMail(objApprover.WORK_EMAIL, Nothing, mailTemplate.TITLE, content, Nothing)

            End Using
        Catch ex As Exception
            Throw ex
        End Try

        Return sent
    End Function
#End Region

End Class