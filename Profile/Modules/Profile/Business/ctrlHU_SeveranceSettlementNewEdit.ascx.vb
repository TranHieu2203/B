Imports System.Globalization
Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_SeveranceSettlementNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True
    Public WithEvents AjaxManager As RadAjaxManager

    Private psp As New ProfileStoreProcedure

    Dim log As New UserLog
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _year As Decimal = Year(DateTime.Now)
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' IDDetailSelecting
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IDDebtSelecting As Decimal?
        Get
            Return ViewState(Me.ID & "_IDDebtSelecting")
        End Get

        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_IDDebtSelecting") = value
        End Set
    End Property

    Property lstHandoverContent As List(Of HandoverContentDTO)
        Get
            Return ViewState(Me.ID & "_lstHandoverContent")
        End Get
        Set(ByVal value As List(Of HandoverContentDTO))
            ViewState(Me.ID & "_lstHandoverContent") = value
        End Set
    End Property

    Property lstDebtForEdit As List(Of DebtDTO)
        Get
            Return ViewState(Me.ID & "_lstDebtForEdit")
        End Get
        Set(ByVal value As List(Of DebtDTO))
            ViewState(Me.ID & "_lstDebtForEdit") = value
        End Set
    End Property

    'Property lstReason As List(Of TerminateReasonDTO)
    '    Get
    '        Return ViewState(Me.ID & "_lstReason")
    '    End Get
    '    Set(ByVal value As List(Of TerminateReasonDTO))
    '        ViewState(Me.ID & "_lstReason") = value
    '    End Set
    'End Property

    Property Terminate As TerminateDTO
        Get
            Return ViewState(Me.ID & "_Terminate")
        End Get
        Set(ByVal value As TerminateDTO)
            ViewState(Me.ID & "_Terminate") = value
        End Set
    End Property

    Property Down_File As String
        Get
            Return ViewState(Me.ID & "_Down_File")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_File") = value
        End Set
    End Property

    Property FileOldName As String
        Get
            Return ViewState(Me.ID & "_FileOldName")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_FileOldName") = value
        End Set
    End Property

    Property objTerminate As TerminateDTO
        Get
            Return ViewState(Me.ID & "_objTerminate")
        End Get
        Set(ByVal value As TerminateDTO)
            ViewState(Me.ID & "_objTerminate") = value
        End Set
    End Property

    Property ListComboData As Profile.ProfileBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Profile.ProfileBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    Property dt_TRUYTHUBHYT As DataTable
        Get
            Return ViewState(Me.ID & "_dt_TRUYTHUBHYT")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dt_TRUYTHUBHYT") = value
        End Set
    End Property

    Property SelectOrg As String
        Get
            Return ViewState(Me.ID & "_SelectOrg")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_SelectOrg") = value
        End Set
    End Property

    '0 - normal
    '1 - Employee
    '2 - Sign
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    '0 - Declare
    '1 - Extent
    '2 - Details
    Dim FormType As Integer

    Dim IDSelect As Decimal?

#End Region

#Region "Page"

    ''' <summary>
    ''' Khởi tạo, Load page, load info cho control theo ID
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            GetParams()
            Refresh()
            UpdateControlState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Khởi tạo load control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Load data cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileRepository
        Try
            GetDataCombo()
            txtPeriod.Visible = False
            lbPeriod.Visible = False
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Khởi tạo, Load menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.MainToolBar = tbarTerminate
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Cancel)

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Reset, Load page theo trạng thái page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        log = LogHelper.GetUserLog
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Terminate = rep.GetTerminateByID(New TerminateDTO With {.ID = IDSelect})
                    If log.Username.ToUpper = "ADMIN" Or Terminate.IS_JOB_LOSS_ALLOWANCE = -1 Then
                        rtxtYear_Job_loss_Allowance.Visible = True
                        txtMonth_Job_loss_Allowance.Visible = True
                        rtxtMoney_Job_loss_allowance.Visible = True
                        Label5.Visible = True
                        Label14.Visible = True
                        Label6.Visible = True
                    Else
                        rtxtYear_Job_loss_Allowance.Visible = False
                        txtMonth_Job_loss_Allowance.Visible = False
                        rtxtMoney_Job_loss_allowance.Visible = False
                        Label5.Visible = False
                        Label14.Visible = False
                        Label6.Visible = False
                    End If


                    If Terminate.WORK_STATUS IsNot Nothing Then
                        hidWorkStatus.Value = Terminate.WORK_STATUS
                    End If
                    rdTerDate.SelectedDate = Terminate.TER_DATE
                    'If Terminate.TRUYTHU_BHYT IsNot Nothing Then
                    '    cboTruyThu_BHYT.SelectedValue = Terminate.TRUYTHU_BHYT
                    'End If
                    'rtxtO_HI_EMP.Value = Terminate.O_HI_EMP
                    rtxtYear_Job_loss_Allowance.Value = Terminate.YEAR_JOB_LOSS_ALLOWANCE
                    txtMonth_Job_loss_Allowance.Text = Terminate.MONTH_JOB_LOSS_ALLOWANCE
                    rtxtMoney_Job_loss_allowance.Value = Terminate.MONEY_JOB_LOSS_ALLOWANCE
                    'rtxtPaybak_Uniform.Value = Terminate.PAYPACK_UNIFORM
                    'rtxtTraining_Costs.Value = Terminate.TRAINING_COSTS
                    'rtxtAmount_Violations.Value = Terminate.AMOUNT_VIOLATIONS
                    'rtxtOther_Compensation.Value = Terminate.OTHER_COMPENSATION

                    If Terminate.IS_RETURN_INSAL IsNot Nothing Then
                        chkIsReturnInSal.Checked = Terminate.IS_RETURN_INSAL
                    End If
                    txtPeriod.Text = Terminate.PERIOD_TEXT
                    txtSeniority.Text = Terminate.EMPLOYEE_SENIORITY
                    rdEffectDate.SelectedDate = Terminate.EFFECT_DATE
                    rdLastDate.SelectedDate = Terminate.LAST_DATE

                    hidDecisionID.Value = Terminate.DECISION_ID.ToString
                    hidEmpID.Value = Terminate.EMPLOYEE_ID
                    hidTitleID.Value = Terminate.TITLE_ID
                    hidOrgID.Value = Terminate.ORG_ID
                    hidID.Value = Terminate.ID.ToString
                    FillDataByEmployeeID(Terminate.EMPLOYEE_ID)
                    rntxtRemainingLeave.Value = Terminate.REMAINING_LEAVE
                    rntxtPaymentLeave.Value = Terminate.PAYMENT_LEAVE
                    rntxtAllowanceTerminate.Value = Terminate.ALLOWANCE_TERMINATE

                    rntxtSalaryMedium_loss.Value = Terminate.SALARYMEDIUM
                    ' MO RA
                    txtTimeAccidentIns_loss.Text = Terminate.SALARYMEDIUM_LOSS

                    rntxtMoneyReturn.Value = Terminate.MONEY_RETURN
                    rntxtyearforallow_loss.Value = Terminate.YEARFORALLOW
                    txtNote.Text = Terminate.REMARK_QT
                    'rtxtUniform.Value = Terminate.UNIFORM

                    checked_IsDeDuct_Salary()

                Case "InsertView"

                    lstHandoverContent = New List(Of HandoverContentDTO)
                    For Each obj In ListComboData.LIST_HANDOVER_CONTENT
                        Dim objHandover As New HandoverContentDTO
                        objHandover.CONTENT_ID = obj.ID
                        objHandover.CONTENT_NAME = obj.NAME_VN
                        lstHandoverContent.Add(objHandover)
                    Next

                    CurrentState = CommonMessage.STATE_NEW
                Case "NormalView"

                    Refresh("UpdateView")

                    txtSeniority.ReadOnly = True
                    rdEffectDate.Enabled = False
                    rdLastDate.Enabled = False

                    txtContractNo.ReadOnly = True
                    txtEmployeeCode.ReadOnly = True
                    txtEmployeeName.ReadOnly = True
                    txtTitleName.ReadOnly = True
                    txtOrgName.ReadOnly = True
                    rdJoinDateState.Enabled = False
                    rdContractEffectDate.Enabled = False
                    rdContractExpireDate.Enabled = False
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

#End Region

#Region "Event"

    ''' <summary>
    ''' Event Click item trên menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim rep_Store As New ProfileStoreProcedure
        Dim _filter As New TerminateDTO
        Dim dtData As New DataTable
        Dim _objfilter As New TerminateDTO
        Dim gid As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If rep_Store.CHECK_TER_EMPEXIST(Decimal.Parse(If(hidID.Value = "", 0, hidID.Value)), Decimal.Parse(hidEmpID.Value)) = True Then
                            ShowMessage(Translate("Nhân viên có mã số {0} đã có đơn được phê duyệt. Vui lòng kiểm tra lại !", txtEmployeeCode.Text), NotifyType.Warning)
                            Exit Sub
                        End If
                        Dim startDate As Date
                        If chkIsReturnInSal.Checked Then
                            If txtPeriod.Text.Trim = "" Then
                                ShowMessage(Translate("Bạn phải nhập tháng lương"), NotifyType.Warning)
                                Exit Sub
                            Else
                                If CheckDate(txtPeriod.Text, startDate) = False Then
                                    ShowMessage(Translate("Tháng lương không đúng định dạng!"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If
                        Else
                            txtPeriod.Text = ""
                        End If

                        If CurrentState = Common.CommonMessage.STATE_EDIT Then
                            _objfilter.ID = hidID.Value
                        End If

                        objTerminate = New TerminateDTO
                        objTerminate.TER_DATE = rdTerDate.SelectedDate
                        'If cboTruyThu_BHYT.SelectedValue <> "" Then
                        '    objTerminate.TRUYTHU_BHYT = Decimal.Parse(cboTruyThu_BHYT.SelectedValue)
                        'End If
                        'objTerminate.O_HI_EMP = rtxtO_HI_EMP.Value
                        objTerminate.YEAR_JOB_LOSS_ALLOWANCE = rtxtYear_Job_loss_Allowance.Value
                        objTerminate.MONTH_JOB_LOSS_ALLOWANCE = txtMonth_Job_loss_Allowance.Text
                        objTerminate.MONEY_JOB_LOSS_ALLOWANCE = rtxtMoney_Job_loss_allowance.Value
                        'objTerminate.PAYPACK_UNIFORM = rtxtPaybak_Uniform.Value
                        'objTerminate.TRAINING_COSTS = rtxtTraining_Costs.Value
                        'objTerminate.AMOUNT_VIOLATIONS = rtxtAmount_Violations.Value
                        'objTerminate.OTHER_COMPENSATION = rtxtOther_Compensation.Value

                        objTerminate.EMPLOYEE_ID = hidEmpID.Value
                        objTerminate.ORG_ID = hidOrgID.Value
                        objTerminate.TITLE_ID = hidTitleID.Value
                        If CurrentState = Common.CommonMessage.STATE_EDIT Then
                            objTerminate.ID = Decimal.Parse(hidDecisionID.Value)
                        End If
                        objTerminate.LAST_DATE = rdLastDate.SelectedDate
                        objTerminate.EMPLOYEE_SENIORITY = txtSeniority.Text

                        objTerminate.EFFECT_DATE = rdEffectDate.SelectedDate

                        objTerminate.IS_RETURN_INSAL = chkIsReturnInSal.Checked
                        objTerminate.PERIOD_TEXT = txtPeriod.Text
                        objTerminate.PAYMENT_LEAVE = rntxtPaymentLeave.Value
                        objTerminate.ALLOWANCE_TERMINATE = rntxtAllowanceTerminate.Value
                        objTerminate.ORG_ABBR = hidOrgAbbr.Value

                        objTerminate.SALARYMEDIUM = rntxtSalaryMedium_loss.Value
                        objTerminate.MONEY_RETURN = rntxtMoneyReturn.Value
                        objTerminate.YEARFORALLOW = rntxtyearforallow_loss.Value
                        objTerminate.SALARYMEDIUM_LOSS = txtTimeAccidentIns_loss.Text

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertTerminate(objTerminate, gid) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_TerminateNewEdit&group=Business&FormType=0")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objTerminate.ID = Decimal.Parse(hidID.Value)
                                objTerminate.DECISION_ID = Decimal.Parse(hidDecisionID.Value)
                                Dim listID As New List(Of Decimal)
                                listID.Add(hidID.Value)
                                If rep.ValidateBusiness("HU_TERMINATE", "ID", listID) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                    Exit Sub
                                End If
                                If rep.ModifyTerminate(objTerminate, gid) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Terminate&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_SeveranceSettlement&group=Business")
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Private Sub btnClose_Click(sender As Object, e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_SeveranceSettlement&group=Business")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        Dim rep As New ProfileBusinessRepository
        Dim gid As Decimal

        Dim startDate As Date
        If chkIsReturnInSal.Checked Then
            If txtPeriod.Text.Trim = "" Then
                ShowMessage(Translate("Bạn phải nhập tháng lương"), NotifyType.Warning)
                Exit Sub
            Else
                If CheckDate(txtPeriod.Text, startDate) = False Then
                    ShowMessage(Translate("Tháng lương không đúng định dạng!"), NotifyType.Warning)
                    Exit Sub
                End If
            End If
        Else
            txtPeriod.Text = ""
        End If

        objTerminate = New TerminateDTO
        objTerminate.ID = hidID.Value
        'If cboTruyThu_BHYT.SelectedValue <> "" Then
        '    objTerminate.TRUYTHU_BHYT = Decimal.Parse(cboTruyThu_BHYT.SelectedValue)
        'End If

        'If rtxtO_HI_EMP.Value IsNot Nothing Then
        '    objTerminate.O_HI_EMP = rtxtO_HI_EMP.Value
        'End If

        'If rtxtUniform.Value IsNot Nothing Then
        '    objTerminate.UNIFORM = rtxtUniform.Value
        'End If

        'If rtxtPaybak_Uniform.Value IsNot Nothing Then
        '    objTerminate.PAYPACK_UNIFORM = rtxtPaybak_Uniform.Value
        'End If

        'If rtxtTraining_Costs.Value IsNot Nothing Then
        '    objTerminate.TRAINING_COSTS = rtxtTraining_Costs.Value
        'End If

        'If rtxtAmount_Violations.Value IsNot Nothing Then
        '    objTerminate.AMOUNT_VIOLATIONS = rtxtAmount_Violations.Value
        'End If

        'If rtxtOther_Compensation.Value IsNot Nothing Then
        '    objTerminate.OTHER_COMPENSATION = rtxtOther_Compensation.Value
        'End If

        If rntxtSalaryMedium_loss.Value IsNot Nothing Then
            objTerminate.SALARYMEDIUM = rntxtSalaryMedium_loss.Value
        End If

        If rntxtPaymentLeave.Value IsNot Nothing Then
            objTerminate.PAYMENT_LEAVE = rntxtPaymentLeave.Value
        End If

        objTerminate.SALARYMEDIUM_LOSS = txtTimeAccidentIns_loss.Text

        If rntxtyearforallow_loss.Value IsNot Nothing Then
            objTerminate.YEARFORALLOW = rntxtyearforallow_loss.Value
        End If

        If rntxtAllowanceTerminate.Value IsNot Nothing Then
            objTerminate.ALLOWANCE_TERMINATE = rntxtAllowanceTerminate.Value
        End If

        If rtxtYear_Job_loss_Allowance.Value IsNot Nothing Then
            objTerminate.YEAR_JOB_LOSS_ALLOWANCE = rtxtYear_Job_loss_Allowance.Value
        End If

        objTerminate.MONTH_JOB_LOSS_ALLOWANCE = txtMonth_Job_loss_Allowance.Text

        If rtxtMoney_Job_loss_allowance.Value IsNot Nothing Then
            objTerminate.MONEY_JOB_LOSS_ALLOWANCE = rtxtMoney_Job_loss_allowance.Value
        End If

        If rntxtMoneyReturn.Value IsNot Nothing Then
            objTerminate.MONEY_RETURN = rntxtMoneyReturn.Value
        End If

        objTerminate.IS_RETURN_INSAL = chkIsReturnInSal.Checked
        objTerminate.PERIOD_TEXT = txtPeriod.Text
        objTerminate.REMARK_QT = txtNote.Text

        If rntxtRemainingLeave.Value IsNot Nothing Then
            objTerminate.REMAINING_LEAVE = rntxtRemainingLeave.Value
        End If

        If rep.ModifyTerminate_TV(objTerminate, gid) Then
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
        Else
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click
        Dim rep As New ProfileBusinessRepository
        Dim gid As Decimal
        objTerminate = New TerminateDTO
        objTerminate.ID = hidID.Value
        If rep.ModifyTerminate_TV(objTerminate, gid) Then
            'ClearControlValue(cboTruyThu_BHYT, rtxtO_HI_EMP, rtxtUniform, rtxtPaybak_Uniform, rtxtTraining_Costs, rtxtAmount_Violations, rtxtOther_Compensation, rntxtSalaryMedium_loss, rntxtPaymentLeave, txtTimeAccidentIns_loss, rntxtyearforallow_loss, rntxtAllowanceTerminate, rtxtYear_Job_loss_Allowance, txtMonth_Job_loss_Allowance, rntxtMoneyReturn, chkIsReturnInSal, txtPeriod, txtNote, rntxtRemainingLeave)
            ClearControlValue(rntxtSalaryMedium_loss, rntxtPaymentLeave, txtTimeAccidentIns_loss, rntxtyearforallow_loss, rntxtAllowanceTerminate, rtxtYear_Job_loss_Allowance, txtMonth_Job_loss_Allowance, rntxtMoneyReturn, chkIsReturnInSal, txtPeriod, txtNote, rntxtRemainingLeave)

            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
        Else
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
        End If
    End Sub

    Private Sub btnCal_Click(sender As Object, e As System.EventArgs) Handles btnCal.Click
        Dim dt As DataTable = psp.CAL_QT(hidEmpID.Value)
        'BOI HOAN DAO TAO
        'rtxtTraining_Costs.Value = dt(0)("TRAINING_COSTS").ToString
        'TIEN DONG PHUC DUOC CAP
        'rtxtUniform.Value = dt(0)("UNIFORM").ToString
        'TIEN DONG PHUC HOAN LAI
        'rtxtPaybak_Uniform.Value = dt(0)("PAYBAK_UNIFORM").ToString
        'LUONG TB 6 THANG
        rntxtSalaryMedium_loss.Value = dt(0)("SALARY_MEDIUM_LOSS").ToString
        'PHEP NAM CON LAI
        rntxtRemainingLeave.Value = dt(0)("TOTAL_HAVE").ToString
        'TIEN PHEP CON LAI  
        rntxtPaymentLeave.Value = dt(0)("MONEY_HAVE").ToString
        'THOI GIAN TRO CAP THOI VIEC
        txtTimeAccidentIns_loss.Text = dt(0)("YEAR_TV").ToString
        'TIEN TRO CAP THOI VIEC QUY DOI
        rntxtyearforallow_loss.Value = dt(0)("YEAR_TV_QD").ToString
        'TIEN TRO CAP THOI VIEC
        rntxtAllowanceTerminate.Value = dt(0)("SAL_TV").ToString
        txtMonth_Job_loss_Allowance.Text = 0
        rtxtYear_Job_loss_Allowance.Value = 0
        rtxtMoney_Job_loss_allowance.Value = 0
        'rntxtMoneyReturn.Value = If(rntxtPaymentLeave.Value Is Nothing, 0, rntxtPaymentLeave.Value) + If(rntxtAllowanceTerminate.Value Is Nothing, 0, rntxtAllowanceTerminate.Value) _
        '                        + If(rtxtMoney_Job_loss_allowance.Value Is Nothing, 0, rtxtMoney_Job_loss_allowance.Value) - If(rtxtO_HI_EMP.Value Is Nothing, 0, rtxtO_HI_EMP.Value) _
        '                        - If(rtxtUniform.Value Is Nothing, 0, rtxtUniform.Value) - If(rtxtPaybak_Uniform.Value Is Nothing, 0, rtxtPaybak_Uniform.Value) - If(rtxtTraining_Costs.Value Is Nothing, 0, rtxtTraining_Costs.Value) _
        '                        - If(rtxtAmount_Violations.Value Is Nothing, 0, rtxtAmount_Violations.Value) - If(rtxtOther_Compensation.Value Is Nothing, 0, rtxtOther_Compensation.Value)
        rntxtMoneyReturn.Value = If(rntxtPaymentLeave.Value Is Nothing, 0, rntxtPaymentLeave.Value) + If(rntxtAllowanceTerminate.Value Is Nothing, 0, rntxtAllowanceTerminate.Value) _
                                + If(rtxtMoney_Job_loss_allowance.Value Is Nothing, 0, rtxtMoney_Job_loss_allowance.Value)
    End Sub

    Private Sub text_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rntxtPaymentLeave.TextChanged, rntxtAllowanceTerminate.TextChanged, rtxtMoney_Job_loss_allowance.TextChanged
        Try
            rntxtMoneyReturn.Value = If(rntxtPaymentLeave.Value Is Nothing, 0, rntxtPaymentLeave.Value) + If(rntxtAllowanceTerminate.Value Is Nothing, 0, rntxtAllowanceTerminate.Value) _
                                + If(rtxtMoney_Job_loss_allowance.Value Is Nothing, 0, rtxtMoney_Job_loss_allowance.Value)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub rntxtRemainingLeave_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rntxtRemainingLeave.TextChanged, rntxtSalaryMedium_loss.TextChanged
        Try
            rntxtPaymentLeave.Value = (If(rntxtRemainingLeave.Value Is Nothing, 0, rntxtRemainingLeave.Value) * If(rntxtSalaryMedium_loss.Value Is Nothing, 0, rntxtSalaryMedium_loss.Value)) / 26

            rntxtMoneyReturn.Value = If(rntxtPaymentLeave.Value Is Nothing, 0, rntxtPaymentLeave.Value) + If(rntxtAllowanceTerminate.Value Is Nothing, 0, rntxtAllowanceTerminate.Value) _
                                + If(rtxtMoney_Job_loss_allowance.Value Is Nothing, 0, rtxtMoney_Job_loss_allowance.Value)
        Catch ex As Exception

        End Try
    End Sub


    ''' <summary>
    ''' Event Yes/No trên Message popup hỏi áp dụng, ngừng áp dụng
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New ProfileBusinessRepository
        Dim gID As Decimal
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Select Case CurrentState
                    Case CommonMessage.STATE_NEW
                        If rep.InsertTerminate(objTerminate, gID) Then
                            Dim str As String = "getRadWindow().close('1');"
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    Case CommonMessage.STATE_EDIT
                        objTerminate.ID = Decimal.Parse(hidID.Value)
                        objTerminate.DECISION_ID = Decimal.Parse(hidDecisionID.Value)
                        If rep.ModifyTerminate(objTerminate, gID) Then
                            Dim str As String = "getRadWindow().close('1');"
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                End Select
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Fill data lên các control theo ID truyền đến
    ''' </summary>
    ''' <param name="gID"></param>
    ''' <remarks></remarks>
    Private Sub FillDataByEmployeeID(ByVal gID As Decimal)
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim store As New ProfileStoreProcedure
        Try
            Dim emp = rep.GetEmployeeByID(gID)
            txtContractNo.Text = emp.CONTRACT_NO
            txtEmployeeCode.Text = emp.EMPLOYEE_CODE
            txtEmployeeName.Text = emp.FULLNAME_VN
            txtTitleName.Text = emp.TITLE_NAME_VN
            txtOrgName.Text = emp.ORG_NAME
            rdJoinDateState.SelectedDate = emp.JOIN_DATE
            rdContractEffectDate.SelectedDate = emp.CONTRACT_EFFECT_DATE
            rdContractExpireDate.SelectedDate = emp.CONTRACT_EXPIRE_DATE

            hidOrgAbbr.Value = emp.ORG_ID
            hidEmpID.Value = emp.ID
            hidTitleID.Value = emp.TITLE_ID
            hidOrgID.Value = emp.ORG_ID

            Dim dt = store.CAL_DEBT_EMP(Decimal.Parse(hidEmpID.Value))
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

#End Region

#Region "Custom"

    ''' <summary>
    ''' Khoi tao, load popup list ma Nhan vien
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Get data cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Try
            'dt_TRUYTHUBHYT = rep.GetOtherList("TRUYTHUBHYT", True)
            'FillRadCombobox(cboTruyThu_BHYT, dt_TRUYTHUBHYT, "NAME", "ID", True)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Get data theo ID Ma nhan vien
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("FormType") IsNot Nothing Then
                    FormType = Request.Params("FormType")
                End If
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = Decimal.Parse(Request.Params("ID"))
                End If
                Select Case FormType
                    Case 1
                        Refresh("UpdateView")
                End Select
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub checked_IsDeDuct_Salary()
        If chkIsReturnInSal.Checked Then
            txtPeriod.Visible = True
            lbPeriod.Visible = True
        Else
            txtPeriod.Visible = False
            lbPeriod.Visible = False
        End If
    End Sub

#End Region

#Region "Caculate"
    'Private Sub cboTruyThu_BHYT_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboTruyThu_BHYT.SelectedIndexChanged

    '    Try
    '        If cboTruyThu_BHYT.SelectedValue = "" Or hidEmpID.Value Is Nothing Then
    '            Exit Sub
    '        End If

    '        Dim store As New ProfileStoreProcedure
    '        Dim dtdata = store.GET_TRUYTHU_BHXH(Decimal.Parse(hidEmpID.Value), cboTruyThu_BHYT.SelectedValue)
    '        rtxtO_HI_EMP.Value = dtdata(0)("SALARY_BHYT").ToString

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub cusvalPeriod_ServerValidate(ByVal source As Object, ByVal args As ServerValidateEventArgs) Handles cusvalPeriod.ServerValidate
        Dim startDate As Date
        Try
            args.IsValid = CheckDate(txtPeriod.Text, startDate)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function CheckDate(ByVal value As String, ByRef result As Date) As Boolean
        Dim dateCheck As Boolean
        If value = "" Or value = "&nbsp;" Then
            value = ""
            Return True
        End If

        Try
            dateCheck = DateTime.TryParseExact(value, "MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result)
            Return dateCheck
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub chkIsReturnInSal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkIsReturnInSal.CheckedChanged
        Try
            checked_IsDeDuct_Salary()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtPeriod_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtPeriod.TextChanged
        Try
            Dim startDate As Date
            If CheckDate(txtPeriod.Text, startDate) = False Then
                ShowMessage(Translate("Tháng lương không đúng định dạng(MM/YYYY)!"), NotifyType.Warning)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region
End Class
