Imports System.Globalization
Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports ICSharpCode.SharpZipLib.Checksums
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Calendar
Imports WebAppLog

Public Class ctrlHU_TerminateNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True
    Public WithEvents AjaxManager As RadAjaxManager
    Dim log As New UserLog
    Private psp As New ProfileStoreProcedure
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
            'If CType(CommonConfig.dicConfig("APP_SETTING_14"), Boolean) Then
            'lbSignerName.Visible = False
            'txtSignerName.Visible = False
            'btnFindSinger.Visible = False
            'lbSignerTitle.Visible = False
            'txtSignerTitle.Visible = False

            'End If
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
            'rgHandoverContent.AllowSorting = False
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
        log = LogHelper.GetUserLog
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileRepository
        Try
            If log.Username.ToUpper = "ADMIN" Then
                Label1.Visible = True
                txtNotifyNo.Visible = True
            Else
                Label1.Visible = False
                txtNotifyNo.Visible = False
            End If

            GetDataCombo()
            'txtDecisionNo.ReadOnly = True
            'txtDecisionNo.Text = "/QĐ-TLSG"
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
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Unlock)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            CType(MainToolBar.Items(3), RadToolBarButton).Text = Translate("Mở phê duyệt")
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
        Try

            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Terminate = rep.GetTerminateByID(New TerminateDTO With {.ID = IDSelect})
                    If Terminate.WORK_STATUS IsNot Nothing Then
                        hidWorkStatus.Value = Terminate.WORK_STATUS
                    End If
                    txtNotifyNo.Text = Terminate.NOTIFY_NO
                    rdTerDate.SelectedDate = Terminate.TER_DATE
                    cbIsBlackList.Checked = Terminate.IS_BLACK_LIST
                    txtReason_BlackList.Text = Terminate.REASON_BLACK_LIST
                    If Terminate.TRUYTHU_BHYT IsNot Nothing Then
                        cboTruyThu_BHYT.SelectedValue = Terminate.TRUYTHU_BHYT
                    End If
                    chkIs_Job_loss_Allowance.Checked = Terminate.IS_JOB_LOSS_ALLOWANCE
                    txtDecisionNo.Text = Terminate.DECISION_NO

                    txtSignerName.Text = Terminate.SIGN_NAME
                    txtSignerTitle.Text = Terminate.SIGN_TITLE
                    txtSeniority.Text = Terminate.EMPLOYEE_SENIORITY
                    txtTerReasonDetail.Text = Terminate.TER_REASON_DETAIL
                    'txtRemark.Text = Terminate.REMARK

                    cboStatus.SelectedValue = Terminate.STATUS_ID.ToString

                    rdSignDate.SelectedDate = Terminate.SIGN_DATE
                    rdEffectDate.SelectedDate = Terminate.EFFECT_DATE
                    rdLastDate.SelectedDate = Terminate.LAST_DATE
                    rdSendDate.SelectedDate = Terminate.SEND_DATE
                    rdSignDate.SelectedDate = Terminate.SIGN_DATE

                    hidDecisionID.Value = Terminate.DECISION_ID.ToString
                    hidEmpID.Value = Terminate.EMPLOYEE_ID
                    hidTitleID.Value = Terminate.TITLE_ID
                    hidOrgID.Value = Terminate.ORG_ID
                    hidID.Value = Terminate.ID.ToString
                    FillDataByEmployeeID(Terminate.EMPLOYEE_ID)
                    If Terminate.TER_REASON.HasValue Then
                        cboTerReason.SelectedValue = Terminate.TER_REASON
                    End If
                    If Terminate.DECISION_TYPE.HasValue Then
                        cboDecisionType.SelectedValue = Terminate.DECISION_TYPE.ToString
                    End If
                    cbIsAllowForTer.Checked = Terminate.IS_ALLOW
                    lstHandoverContent = Terminate.lstHandoverContent
                    'rgHandoverContent.Rebind()
                    'For Each i As GridItem In rgHandoverContent.Items
                    '    i.Edit = True
                    'Next
                    'rgHandoverContent.Rebind()
                    txtUploadFile.Text = Terminate.FILENAME
                    txtRemindLink.Text = If(Terminate.UPLOADFILE Is Nothing, "", Terminate.UPLOADFILE)
                    loadDatasource(txtUploadFile.Text)
                    FileOldName = If(FileOldName = "", txtUpload.Text, FileOldName)
                    ' phê duyệt và ko phê duyêt
                    If Terminate.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Or
                        Terminate.STATUS_ID = ProfileCommon.DECISION_STATUS.NOT_APPROVE_ID Then
                        EnableControlAll_Cus(False, RadPane2)
                        btnDownload.Enabled = True
                        btnUploadFile.Enabled = True
                        'rgHandoverContent.Enabled = True
                        rgDebt.Enabled = True
                        'rgHandoverContent.Enabled = True
                        EnableRadCombo(cboDebtStatus, True)
                        MainToolBar.Items(0).Enabled = True
                        EnableControlAll(True, cboDebtType, rntxtDebtMoney, cboDebtStatus, txtDebtNote, rgDebt)
                    End If
                    If Terminate.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        MainToolBar.Items(0).Enabled = False
                    End If

                Case "InsertView"

                    lbReason_BlackList.Visible = False
                    txtReason_BlackList.Visible = False


                    lstHandoverContent = New List(Of HandoverContentDTO)
                    For Each obj In ListComboData.LIST_HANDOVER_CONTENT
                        Dim objHandover As New HandoverContentDTO
                        objHandover.CONTENT_ID = obj.ID
                        objHandover.CONTENT_NAME = obj.NAME_VN
                        lstHandoverContent.Add(objHandover)
                    Next
                    rgDebt.Rebind()
                    For Each i As GridItem In rgDebt.Items
                        i.Edit = True
                    Next
                    rgDebt.Rebind()
                    'rgHandoverContent.Rebind()
                    'For Each i As GridItem In rgHandoverContent.Items
                    '    i.Edit = True
                    'Next
                    'rgHandoverContent.Rebind()
                    cboStatus.SelectedValue = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID
                    cboDecisionType.SelectedIndex = 0
                    CurrentState = CommonMessage.STATE_NEW
                Case "NormalView"

                    Refresh("UpdateView")
                    cbIsBlackList.Checked = False

                    btnFindEmployee.Enabled = False
                    btnFindSinger.Enabled = False
                    CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    txtSignerName.ReadOnly = True
                    txtSignerTitle.ReadOnly = True
                    txtSeniority.ReadOnly = True
                    txtTerReasonDetail.ReadOnly = True
                    'txtRemark.ReadOnly = True

                    cboStatus.Enabled = False
                    rdSignDate.Enabled = False
                    rdEffectDate.Enabled = False
                    rdLastDate.Enabled = False
                    rdSendDate.Enabled = False
                    rdSignDate.Enabled = False

                    txtContractNo.ReadOnly = True
                    txtEmployeeCode.ReadOnly = True
                    txtEmployeeName.ReadOnly = True
                    txtTitleName.ReadOnly = True
                    txtOrgName.ReadOnly = True
                    rdJoinDateState.Enabled = False
                    rdContractEffectDate.Enabled = False
                    rdContractExpireDate.Enabled = False
                    cbIsAllowForTer.Enabled = False
                    cboTerReason.Enabled = False
                    cboDecisionType.Enabled = False
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Private Sub cboDecisionType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboDecisionType.SelectedIndexChanged
        Try
            AutoCreate_DecisionNo()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub rdEffectDate_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdEffectDate.SelectedDateChanged
        Dim item As New ContractTypeDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ClearControlValue(txtSignerName, txtSignerTitle)
            Dim startTime As DateTime = DateTime.UtcNow
            If rdEffectDate.SelectedDate IsNot Nothing Then

                Dim dExpire As Date = rdEffectDate.SelectedDate
                dExpire = dExpire.AddDays(CType(-1, Double))
                rdLastDate.SelectedDate = dExpire
            End If
            GetSigner()
            AutoCreate_DecisionNo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
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

                        If cbIsBlackList.Checked Then
                            If txtReason_BlackList.Text.Trim = "" Then
                                ShowMessage(Translate("Bạn phải nhập lý do danh sách đen"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If

                        _objfilter.DECISION_NO = txtDecisionNo.Text
                        If CurrentState = Common.CommonMessage.STATE_EDIT Then
                            _objfilter.ID = hidID.Value
                        End If

                        objTerminate = New TerminateDTO
                        objTerminate.NOTIFY_NO = txtNotifyNo.Text
                        objTerminate.TER_DATE = rdTerDate.SelectedDate
                        objTerminate.IS_BLACK_LIST = cbIsBlackList.Checked
                        objTerminate.REASON_BLACK_LIST = txtReason_BlackList.Text
                        If cboTruyThu_BHYT.SelectedValue <> "" Then
                            objTerminate.TRUYTHU_BHYT = Decimal.Parse(cboTruyThu_BHYT.SelectedValue)
                        End If
                        objTerminate.IS_JOB_LOSS_ALLOWANCE = chkIs_Job_loss_Allowance.Checked
                        objTerminate.DECISION_NO = txtDecisionNo.Text
                        objTerminate.EMPLOYEE_ID = hidEmpID.Value
                        objTerminate.ORG_ID = hidOrgID.Value
                        objTerminate.TITLE_ID = hidTitleID.Value
                        objTerminate.STATUS_ID = cboStatus.SelectedValue
                        If CurrentState = Common.CommonMessage.STATE_EDIT Then
                            objTerminate.ID = Decimal.Parse(hidDecisionID.Value)
                        End If
                        objTerminate.FILENAME = txtUpload.Text.Trim
                        objTerminate.UPLOADFILE = If(Down_File Is Nothing, "", Down_File)
                        If objTerminate.UPLOADFILE = "" Then
                            objTerminate.UPLOADFILE = If(txtRemindLink.Text Is Nothing, "", txtRemindLink.Text)
                        Else
                            objTerminate.UPLOADFILE = If(objTerminate.UPLOADFILE Is Nothing, "", objTerminate.UPLOADFILE)
                        End If
                        objTerminate.LAST_DATE = rdLastDate.SelectedDate
                        objTerminate.SEND_DATE = rdSendDate.SelectedDate
                        objTerminate.TER_REASON_DETAIL = txtTerReasonDetail.Text

                        'objTerminate.REMARK = txtRemark.Text
                        objTerminate.EMPLOYEE_SENIORITY = txtSeniority.Text

                        objTerminate.EFFECT_DATE = rdEffectDate.SelectedDate
                        objTerminate.SIGN_NAME = txtSignerName.Text
                        objTerminate.SIGN_DATE = rdSignDate.SelectedDate
                        objTerminate.ORG_ABBR = hidOrgAbbr.Value

                        objTerminate.SIGN_TITLE = txtSignerTitle.Text
                        If cboTerReason.Text <> "" Then
                            objTerminate.TER_REASON = cboTerReason.SelectedValue
                        End If
                        objTerminate.DECISION_TYPE = cboDecisionType.SelectedValue.ToString
                        lstHandoverContent = New List(Of HandoverContentDTO)
                        objTerminate.IS_ALLOW = cbIsAllowForTer.Checked
                        'For Each item As GridDataItem In rgHandoverContent.Items
                        '    Dim handover As New HandoverContentDTO
                        '    handover.CONTENT_ID = item.GetDataKeyValue("CONTENT_ID")
                        '    handover.CONTENT_NAME = item.GetDataKeyValue("CONTENT_NAME")
                        '    Dim chk As CheckBox = CType(item("IS_FINISH").Controls(0), CheckBox)
                        '    handover.IS_FINISH = chk.Checked
                        '    lstHandoverContent.Add(handover)
                        'Next
                        objTerminate.lstHandoverContent = lstHandoverContent
                        Dim debts As New List(Of DebtDTO)
                        For Each row As GridDataItem In rgDebt.Items
                            Dim debt As New DebtDTO With {.ID = ConvertTo(row.GetDataKeyValue("ID")),
                                                          .MONEY = row.GetDataKeyValue("MONEY"),
                                                          .REMARK = row.GetDataKeyValue("REMARK"),
                                                          .DEBT_STATUS = row.GetDataKeyValue("DEBT_STATUS"),
                                                          .DEBT_TYPE_ID = row.GetDataKeyValue("DEBT_TYPE_ID")}
                            debts.Add(debt)
                        Next
                        objTerminate.lstDebts = debts
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                Dim checkCon = rep.CheckConcurrentlyExpireDate(objTerminate)
                                If checkCon = 1 Then
                                    ShowMessage(Translate("Quyết định kiêm nhiệm đang còn hiệu lực. Vui lòng kiểm tra lại"), Utilities.NotifyType.Warning)
                                    Exit Sub
                                ElseIf checkCon = 2 Then
                                    ShowMessage(Translate("Ngày hiệu lực thôi kiêm nhiệm đang lớn hơn ngày hiệu lực QĐ nghỉ việc. Vui lòng kiểm tra lại"), Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.InsertTerminate(objTerminate, gid) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Terminate&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                'If hidWorkStatus.Value = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                                '    ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), NotifyType.Warning)
                                '    Exit Sub
                                'End If

                                objTerminate.ID = Decimal.Parse(hidID.Value)
                                objTerminate.DECISION_ID = Decimal.Parse(hidDecisionID.Value)
                                Dim listID As New List(Of Decimal)
                                listID.Add(hidID.Value)
                                If rep.ValidateBusiness("HU_TERMINATE", "ID", listID) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                    Exit Sub
                                End If
                                If rep.ModifyTerminate(objTerminate, gid) Then
                                    If cboStatus.SelectedValue = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID Then
                                        If hidEmpID.Value <> "" Then
                                            If rep_Store.UPDATE_STATUS_UNLOCK_TERMINATE(Decimal.Parse(hidEmpID.Value)) Then
                                            End If
                                        End If
                                    End If
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Terminate&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select


                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    txtRemindLink.Text = ""
                    FileOldName = ""
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Terminate&group=Business")
                Case CommonMessage.TOOLBARITEM_UNLOCK
                    If Terminate.STATUS_ID = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID Then
                        ShowMessage(Translate("Trạng thái hiện tại đang là Chờ Phê Duyệt"), NotifyType.Warning)
                        Exit Sub
                    Else

                        If rep_Store.CHECK_SETTLENMENT(Decimal.Parse(hidEmpID.Value)) Then
                            ShowMessage(Translate("Đã tồn tại thông tin quyết toán thôi việc, không thể mở phê duyệt"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        ClearControlValue(cboStatus)
                        cboStatus.SelectedValue = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID

                        If hidEmpID.Value <> "" Then
                            If rep_Store.UPDATE_STATUS_UNLOCK_TERMINATE(Decimal.Parse(hidEmpID.Value)) And rep.Delete_Ins_Arising_While_Unapprove(Decimal.Parse(hidEmpID.Value), rdEffectDate.SelectedDate) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
                            End If
                        End If
                    End If

            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Private Sub rgDebt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgDebt.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            For Each _item As GridDataItem In rgDebt.SelectedItems
                cboDebtType.SelectedValue = _item.GetDataKeyValue("DEBT_TYPE_ID")
                rntxtDebtMoney.Value = Decimal.Parse(_item.GetDataKeyValue("MONEY"))
                cboDebtStatus.SelectedValue = _item.GetDataKeyValue("DEBT_STATUS")
                txtDebtNote.Text = _item.GetDataKeyValue("REMARK")
                hidCheckDelete.Value = _item.GetDataKeyValue("DEBT_TYPE_ID")
            Next
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim bCheck As Boolean = False
        Try
            If txtUpload.Text <> "" Then
                Dim strPath_Down As String
                If FileOldName = txtUpload.Text.Trim Or FileOldName Is Nothing Then
                    If txtRemindLink.Text IsNot Nothing Then
                        If txtRemindLink.Text <> "" Then
                            strPath_Down = Server.MapPath("~/ReportTemplates/Profile/TerminateInfo/" + txtRemindLink.Text)
                            'bCheck = True
                            ZipFiles(strPath_Down)
                        End If
                    End If
                Else
                    If Down_File <> "" Then
                        strPath_Down = Server.MapPath("~/ReportTemplates/Profile/TerminateInfo/" + Down_File)
                        'bCheck = True
                        ZipFiles(strPath_Down)
                    End If
                End If
                'If bCheck Then
                '    ZipFiles(strPath_Down)
                'End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ZipFiles(ByVal path As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New Crc32()
            'Dim pathZip As String = AppDomain.CurrentDomain.BaseDirectory & "Zip\"
            'Dim fileNameZip As String = "ThongTinKhenThuong.zip"
            Dim fileNameZip As String = txtUploadFile.Text.Trim

            'If Not Directory.Exists(pathZip) Then
            '    Directory.CreateDirectory(pathZip)
            'Else
            '    For Each deleteFile In Directory.GetFiles(pathZip, "*.*", SearchOption.TopDirectoryOnly)
            '        File.Delete(deleteFile)
            '    Next
            'End If

            'Dim s As New ZipOutputStream(File.Create(pathZip & fileNameZip))
            's.SetLevel(0)
            '' 0 - store only to 9 - means best compression
            'For i As Integer = 0 To Directory.GetFiles(path).Length - 1
            '    ' Must use a relative path here so that files show up in the Windows Zip File Viewer
            '    ' .. hence the use of Path.GetFileName(...)
            '    Dim fileName As String = System.IO.Path.GetFileName(Directory.GetFiles(path)(i))

            '    Dim entry As New ZipEntry(fileName)
            '    entry.DateTime = DateTime.Now

            '    ' Read in the 
            '    Using fs As FileStream = File.Open(Directory.GetFiles(path)(i), FileMode.Open)
            '        Dim buffer As Byte() = New Byte(fs.Length - 1) {}
            '        fs.Read(buffer, 0, buffer.Length)
            '        entry.Size = fs.Length
            '        fs.Close()
            '        crc.Reset()
            '        crc.Update(buffer)
            '        entry.Crc = crc.Value
            '        s.PutNextEntry(entry)
            '        s.Write(buffer, 0, buffer.Length)
            '    End Using
            'Next
            's.Finish()
            's.Close()

            'Using FileStream = File.Open(path & fileNameZip, FileMode.Open)
            '    Dim buffer As Byte() = New Byte(FileStream.Length - 1) {}
            '    FileStream.Read(buffer, 0, buffer.Length)
            '    Dim rEx As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_\-\.]+")
            '    Response.Clear()
            '    Response.AddHeader("Content-Disposition", "attachment; filename=" + rEx.Replace(fileNameZip, "_"))
            '    Response.AddHeader("Content-Length", FileStream.Length.ToString())
            '    Response.ContentType = "application/octet-stream"
            '    Response.BinaryWrite(buffer)
            '    FileStream.Close()
            'End Using

            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & fileNameZip)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            'Response.ContentType = "application/octet-stream"
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            txtUploadFile.Text = ""
            Dim listExtension = New List(Of String)
            listExtension.Add(".xls")
            listExtension.Add(".xlsx")
            listExtension.Add(".txt")
            listExtension.Add(".ctr")
            listExtension.Add(".doc")
            listExtension.Add(".docx")
            listExtension.Add(".xml")
            listExtension.Add(".png")
            listExtension.Add(".jpg")
            listExtension.Add(".bitmap")
            listExtension.Add(".jpeg")
            listExtension.Add(".gif")
            listExtension.Add(".pdf")
            listExtension.Add(".rar")
            listExtension.Add(".zip")
            listExtension.Add(".ppt")
            listExtension.Add(".pptx")

            Dim fileName As String
            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/TerminateInfo/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        'If Commend IsNot Nothing Then
                        '    If Commend.UPLOADFILE IsNot Nothing Then
                        '        strPath += Commend.UPLOADFILE
                        '    Else
                        '        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        '        strPath = strPath + str_Filename
                        '    End If
                        '    fileName = System.IO.Path.Combine(strPath, file.FileName)
                        '    file.SaveAs(fileName, True)
                        '    Commend.UPLOADFILE = str_Filename
                        '    txtUploadFile.Text = file.FileName
                        'Else
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        txtUploadFile.Text = file.FileName
                        'End If
                        Down_File = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                loadDatasource(txtUploadFile.Text)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub loadDatasource(ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            'Dim data As New DataTable
            'data.Columns.Add("FileName")
            'Dim row As DataRow
            'Dim str() As String

            If strUpload <> "" Then
                txtUploadFile.Text = strUpload
                FileOldName = txtUpload.Text
                txtUpload.Text = strUpload
                'str = strUpload.Split(";")

                'For Each s As String In str
                '    If s <> "" Then
                '        row = data.NewRow
                '        row("FileName") = s
                '        data.Rows.Add(row)
                '    End If
                'Next

                'cboUpload.DataSource = data
                'cboUpload.DataTextField = "FileName"
                'cboUpload.DataValueField = "FileName"
                'cboUpload.DataBind()
            Else
                'cboUpload.DataSource = Nothing
                'cboUpload.ClearSelection()
                'cboUpload.ClearCheckedItems()
                'cboUpload.Items.Clear()
                strUpload = String.Empty
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnUploadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload1.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Event click button tìm mã nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFindSinger_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindSinger.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindSigner.MustHaveContract = True
            ctrlFindSigner.LoadAllOrganization = False
            ctrlFindSigner.IsOnlyWorkingWithoutTer = True
            ctrlFindSigner.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Event button tìm mã nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFindEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Event OK chọn mã nhân viên (giao diện 1)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ClearControlValue(txtSignerTitle, txtSignerName)
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                Using rep1 As New ProfileBusinessRepository
                    Dim check = rep.Check_has_Ter(item.ID)
                    If check = 1 Then
                        ShowMessage(Translate("Nhân viên đã có quyết định nghỉ việc"), NotifyType.Warning)
                        Exit Sub
                    End If
                End Using
                txtEmployeeCode.Text = item.EMPLOYEE_CODE
                'txtDecisionNo.Text = item.EMPLOYEE_CODE.Substring(1) + " / QDTV-KSF"
                FillDataByEmployeeID(item.ID)
                If rdJoinDateState.SelectedDate IsNot Nothing And rdLastDate.SelectedDate IsNot Nothing Then

                    If rdJoinDateState.SelectedDate < rdLastDate.SelectedDate Then
                        txtSeniority.Text = CalculateSeniority1(rdJoinDateState.SelectedDate, rdLastDate.SelectedDate)
                    Else
                        txtSeniority.Text = vbNullString
                    End If
                End If
                rgDebt.Rebind()
            End If

            isLoadPopup = 0
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
            'txtStaffRankName.Text = emp.STAFF_RANK_NAME
            rdJoinDateState.SelectedDate = emp.JOIN_DATE
            rdContractEffectDate.SelectedDate = emp.CONTRACT_EFFECT_DATE
            rdContractExpireDate.SelectedDate = emp.CONTRACT_EXPIRE_DATE
            'rntxtCostSupport.Value = emp.COST_SUPPORT
            'rnt.Value = emp.SAL_BASIC

            hidOrgAbbr.Value = emp.ORG_ID
            hidEmpID.Value = emp.ID
            hidTitleID.Value = emp.TITLE_ID
            hidOrgID.Value = emp.ORG_ID

            GenerateNofityNo()
            'If IsPostBack Then
            '    AutoCreate_DecisionNo()
            'End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    'Thâm niên công tác = ngày làm việc cuối - ngày vào làm
    'ngày < 15 = 0.5 ngày
    'ngày >= 15 = 1 tháng
    Private Function CalculateSeniority(ByVal dStart As Date, ByVal dEnd As Date) As String
        Dim dSoNam As Double
        Dim dSoThang As Double
        Dim dNgayDuThang As Double
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'Dim clsDate As Framework.Data.DateDifference = New Framework.Data.DateDifference(dStart, Date.Now)
            'Dim clsDate As DateDifference = New DateDifference(dStart, dEnd)
            'dSoNam = Math.Round(((dEnd - dStart).Days + 1) / 365, 0)
            Dim sonam = ((dEnd - dStart).Days + 1) / 365
            'dSoThang = clsDate.Months
            Dim pos = sonam.ToString().IndexOf(",")
            If pos = -1 Then
                dSoNam = sonam

                dSoThang = 0
            Else
                dSoNam = sonam.ToString().Substring(0, 2)
                sonam = (sonam - dSoNam) * 12
                Dim sonamdu As Decimal = "0" + sonam.ToString().Substring(pos + 1, 2)
                dSoThang = Math.Round(sonam, 2)
            End If


            'If dNgayDuThang < 15 Then
            '    dSoThang = dSoThang + 0.5
            'Else
            '    dSoThang = dSoThang + 1
            '    If dSoThang = 12 Then
            '        dSoNam = dSoNam + 1
            '        dSoThang = 0
            '    End If
            'End If
            Dim str As String

            If dSoNam = 0 And dSoThang = 0 Then
                str = ""
            End If

            If dSoNam = 0 And dSoThang <> 0 Then
                str = dSoThang & " Tháng"
            End If

            If dSoNam <> 0 And dSoThang = 0 Then
                str = dSoNam & " Năm"
            End If

            If dSoNam <> 0 And dSoThang <> 0 Then
                str = dSoNam & " Năm " & dSoThang & " Tháng"
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Return str
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
    Private Function CalculateSeniority1(ByVal dStart As Date, ByVal dEnd As Date) As String
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileBusinessRepository
        Dim Cal_Month_Emp As Int32 = 0
        Dim str As String = ""
        Dim Cal1 As Integer = 0
        Dim Cal2 As Integer = 0
        Dim lastDayOfMonth As Integer = 0
        Dim dSoNam As Double = 0
        Dim dSoThang As Double = 0
        Dim Total_Month As Decimal = 0
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'Dim Cal_Day_Emp = Math.Round((CDate(dEnd).Subtract(CDate(dStart)).TotalDays) + 1, 2)
            'Dim Cal_Month_Emp = Math.Round(Cal_Day_Emp / 365 * 12, 2) 


            ' Tinh tham nien lam viec truoc
            Dim Month_Work = rep.Get_Month_Work_Before(hidEmpID.Value)
            ' tham nien nhan vien tai HSV
            Cal_Month_Emp = (DateDiff(DateInterval.Month, CDate(dStart), CDate(dEnd))) + 1
            If CDate(dStart).Day <= 5 Then
                Cal1 = 1
            Else
                Cal1 = 0
            End If
            lastDayOfMonth = (DateTime.DaysInMonth(CDate(dEnd).Year, CDate(dEnd).Month)) - 5
            If CDate(dEnd).Day >= lastDayOfMonth Then
                Cal2 = 1
            Else
                Cal2 = 0
            End If
            Total_Month = Math.Round(((Cal_Month_Emp - 2 + Cal1 + Cal2) + Month_Work), 2)
            If IsNumeric(Total_Month) Then
                dSoNam = Total_Month \ 12
                dSoThang = Math.Round(CDec(Total_Month) Mod 12, 2)
                str = If(dSoNam > 0, dSoNam.ToString + " Năm ", "") + If(Math.Round(CDec(dSoThang) Mod 12, 2) > 0, Math.Round(CDec(dSoThang) Mod 12, 2).ToString + " Tháng", "")
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Return str
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
    Private Sub GetYearOrMonthByText(ByVal str As String, ByRef dSoNam As Double, ByRef dSoThang As Double)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'str = "11,5 Tháng"
            If str = "" Then
                dSoNam = 0
                dSoThang = 0
            End If
            Dim findYear = str.IndexOf("Năm")
            Dim findMonth = str.IndexOf("Tháng")
            If findYear <> -1 And findMonth <> -1 Then
                dSoNam = Double.Parse(str.Substring(0, 2))
                dSoThang = Double.Parse(str.Substring(findYear + 3, findMonth - findYear - 3).Trim)
            ElseIf findYear = -1 And findMonth <> -1 Then
                dSoNam = 0
                dSoThang = Double.Parse(str.Substring(0, findMonth))
            ElseIf findYear <> -1 And findMonth = -1 Then
                dSoNam = Double.Parse(str.Substring(0, findYear))
                dSoThang = 0
            Else
                dSoNam = 0
                dSoThang = 0
            End If
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Event OK chọn mã nhân viên (giao diện 2)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindSigner_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            lstCommonEmployee = CType(ctrlFindSigner.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                txtSignerName.Text = item.FULLNAME_VN
                txtSignerTitle.Text = item.TITLE_NAME
            End If
            isLoadPopup = 0
            'rgLabourProtect.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Cancel Popup list mã nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked, ctrlFindSigner.CancelClicked
        isLoadPopup = 0
    End Sub

    ''' <summary>
    ''' Event click radio button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rd_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdLastDate.SelectedDateChanged, rdJoinDateState.SelectedDateChanged, rdEffectDate.SelectedDateChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If rdJoinDateState.SelectedDate IsNot Nothing Then
                If rdLastDate.SelectedDate IsNot Nothing Then
                    If rdJoinDateState.SelectedDate < rdLastDate.SelectedDate Then

                        txtSeniority.Text = CalculateSeniority1(rdJoinDateState.SelectedDate, rdLastDate.SelectedDate)
                    Else
                        txtSeniority.Text = vbNullString
                    End If
                End If
                If rdEffectDate.SelectedDate IsNot Nothing Then
                    If rdJoinDateState.SelectedDate > rdLastDate.SelectedDate Then
                        ShowMessage(Translate("Ngày thôi việc phải lớn hơn hoặc bằng ngày vào công ty"), NotifyType.Warning)
                    End If
                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Validate cval_lastdate_sendDate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Protected Sub cval_LastDate_SendDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cval_LastDate_SendDate.ServerValidate
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        If rdLastDate.SelectedDate IsNot Nothing And rdEffectDate.SelectedDate IsNot Nothing Then
    '            If rdLastDate.SelectedDate > rdEffectDate.SelectedDate Then
    '                args.IsValid = True
    '                Exit Sub
    '            End If
    '        End If

    '        args.IsValid = False
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    ''' <summary>
    ''' Validate cvaldpApproveDatejoinDate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Protected Sub cvaldpApproveDateJoinDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvaldpApproveDateJoinDate.ServerValidate
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        If rdJoinDateState.SelectedDate IsNot Nothing And rdApprovalDate.SelectedDate IsNot Nothing Then
    '            If rdApprovalDate.SelectedDate < rdJoinDateState.SelectedDate Then
    '                args.IsValid = False
    '                Exit Sub
    '            End If
    '        End If

    '        args.IsValid = True
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    ''' <summary>
    ''' Validate cval_LastDate_JoinDate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Protected Sub cval_LastDate_JoinDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cval_LastDate_SendDate.ServerValidate
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        If rdJoinDateState.SelectedDate IsNot Nothing And rdLastDate.SelectedDate IsNot Nothing Then
    '            If rdLastDate.SelectedDate < rdJoinDateState.SelectedDate Then
    '                args.IsValid = False
    '                Exit Sub
    '            End If
    '        End If

    '        args.IsValid = True
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    ''' <summary>
    ''' Validate cvaldpSendDate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cvaldpSendDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvaldpSendDate.ServerValidate
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If rdJoinDateState.SelectedDate IsNot Nothing And rdSendDate.SelectedDate IsNot Nothing Then
                If rdSendDate.SelectedDate < rdJoinDateState.SelectedDate Then
                    args.IsValid = False
                    Exit Sub
                End If
            End If

            args.IsValid = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ' ''' <summary>
    ' ''' Validate cval_LastDate
    ' ''' </summary>
    ' ''' <param name="source"></param>
    ' ''' <param name="args"></param>
    ' ''' <remarks></remarks>
    'Protected Sub cval_LastDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cval_LastDate_JoinDate.ServerValidate
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        'ngày nghỉ thực tế bắt nhập khi trạng thái là phê duyệt
    '        If cboStatus.SelectedValue = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
    '            If rdLastDate.SelectedDate Is Nothing Then
    '                args.IsValid = False
    '                Exit Sub
    '            End If
    '        End If



    '        args.IsValid = True
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    ''' <summary>
    ''' Reload, load datasource cho grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub rgLabourProtect_NeedDataSource(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgLabourProtect.NeedDataSource
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Using rep As New ProfileBusinessRepository
    '            Dim lstData As List(Of LabourProtectionMngDTO)
    '            If hidEmpID.Value <> "" Then
    '                lstData = rep.GetLabourProtectByTerminate(hidEmpID.Value)
    '            Else
    '                lstData = New List(Of LabourProtectionMngDTO)
    '            End If
    '            'rgLabourProtect.DataSource = lstData
    '        End Using
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '    End Try

    'End Sub

    ''' <summary>
    ''' Reload, load datasource cho grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgDebt_NeedDataSource(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgDebt.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lstData As List(Of DebtDTO)
        Try
            Using rep As New ProfileBusinessRepository
                If hidEmpID.Value <> "" Then
                    'lstData = rep.GetDebt(hidEmpID.Value, rgDebt.CurrentPageIndex, rgDebt.PageSize, 10)
                Else
                    lstData = New List(Of DebtDTO)
                End If
            End Using
            'For index = 0 To lstData.Count - 1
            '    lstData(0).EMPLOYEE_CODE = index + 1
            'Next
            rgDebt.DataSource = lstData
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Event thay doi gia tri cua rdApprovalDate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub rdApprovalDate_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdApprovalDate.SelectedDateChanged, rdLastDate.SelectedDateChanged
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        GetTerminateCalculate()
    '        If (rdApprovalDate.SelectedDate Is Nothing Or rdLastDate.SelectedDate Is Nothing Or hiSalbasic.Value Is Nothing) Then
    '            rntxtAmountWrongful.ClearValue()
    '            rntxtAmountViolations.ClearValue()
    '        Else
    '            If rdApprovalDate.SelectedDate > rdLastDate.SelectedDate Then
    '                Dim day = (rdApprovalDate.SelectedDate.Value - rdLastDate.SelectedDate.Value).Days
    '                rntxtAmountWrongful.Value = Decimal.Round(Utilities.ObjToDecima(day * (hiSalbasic.Value / 26)), 2)
    '                rntxtAmountViolations.Value = hiSalbasic.Value / 2
    '            Else
    '                rntxtAmountWrongful.Value = 0
    '                rntxtAmountViolations.Value = 0
    '            End If
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub


    'Private Sub rgHandoverContent_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgHandoverContent.NeedDataSource
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        CreateDataHandoverContent(lstHandoverContent)
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub


    'Private Sub rgHandoverContent_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgHandoverContent.ItemDataBound
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        'If e.Item.Edit Then
    '        '    Dim edit = CType(e.Item, GridEditableItem)
    '        '    Dim rntxt As RadNumericTextBox
    '        '    rntxt = CType(edit("DENSITY").Controls(0), RadNumericTextBox)
    '        '    rntxt.MinValue = 0
    '        '    rntxt.MaxValue = 100
    '        '    rntxt.Width = Unit.Percentage(100)
    '        'End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try

    'End Sub

    Private Sub RgDebts_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgDebt.ItemCommand
        Dim dataSource = GetDebtsSource()
        Select Case e.CommandName
            Case "btnAddDebt"
                If cboDebtType.SelectedValue = "" Then
                    ShowMessage(Translate("Loại công nợ chưa được chọn. Vui lòng kiểm tra lại !"), NotifyType.Warning)
                    Exit Sub
                End If
                If rntxtDebtMoney.Value.HasValue = False Then
                    ShowMessage(Translate("Số tiền không để trống. Vui lòng kiểm tra lại !"), NotifyType.Warning)
                    Exit Sub
                End If
                If cboDebtStatus.SelectedValue = "" Then
                    ShowMessage(Translate("Trạng thái công nợ chưa được chọn. Vui lòng kiểm tra lại !"), NotifyType.Warning)
                    Exit Sub
                End If
                AddDebt(dataSource)
                ClearControlValue(cboDebtType, rntxtDebtMoney, txtDebtNote, cboDebtStatus)
                rgDebt.DataSource = dataSource
                rgDebt.DataBind()
                'Case "btnEditDebt"
                '    EditDebt(lstDebtForEdit, IDDebtSelecting)
                '    rgDebt.DataSource = lstDebtForEdit
            Case "btnDeleteDebts"
                DeleteDebts(dataSource)
                ClearControlValue(cboDebtType, rntxtDebtMoney, txtDebtNote, cboDebtStatus)
                rgDebt.DataSource = dataSource
                If dataSource.Count = 0 Then
                    rgDebt.DataSource = New List(Of DebtDTO)
                End If
                rgDebt.DataBind()
        End Select
        If rgDebt.DataSource Is Nothing Then
            rgDebt.DataSource = dataSource
        End If
    End Sub

    Private Function GetDebtsSource() As List(Of DebtDTO)
        Dim dataSource = New List(Of DebtDTO)

        For Each item As GridDataItem In rgDebt.Items
            dataSource.Add(New DebtDTO With {.ID = item.GetDataKeyValue("ID"),
                           .DEBT_TYPE_ID = item.GetDataKeyValue("DEBT_TYPE_ID"),
                           .DEBT_TYPE_NAME = item.GetDataKeyValue("DEBT_TYPE_NAME"),
                           .DEBT_STATUS = item.GetDataKeyValue("DEBT_STATUS"),
                           .DEBT_STATUS_NAME = item.GetDataKeyValue("DEBT_STATUS_NAME"),
                           .MONEY = item.GetDataKeyValue("MONEY"),
                           .REMARK = item.GetDataKeyValue("REMARK")})
        Next
        Return dataSource
    End Function

    Private Function EditDebt(ByVal dataSource As List(Of DebtDTO), ByVal ID As Decimal) As List(Of DebtDTO)
        Try
            Dim status = dataSource.Find(Function(f) f.ID = ID).DEBT_STATUS
            If status = ProfileCommon.DEBT_STATUS.NOT_COMPLETE_ID Then
                dataSource.Where(Function(f) f.ID = ID).FirstOrDefault().DEBT_STATUS = cboDebtStatus.SelectedValue
                dataSource.Where(Function(f) f.ID = ID).FirstOrDefault().DEBT_STATUS_NAME = cboDebtStatus.Text
            End If
            Return dataSource
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function AddDebt(ByVal dataSource As List(Of DebtDTO)) As List(Of DebtDTO)
        Try
            'Kiểm tra (hoặc xóa) loại công nợ đã tồn tại hay chưa
            Dim item_Ck = (From p In dataSource.AsEnumerable Where p.DEBT_TYPE_ID = cboDebtType.SelectedValue).FirstOrDefault
            If item_Ck IsNot Nothing Then
                dataSource.Remove(item_Ck)
            End If

            Dim rowId = dataSource.Count + 1 'dung de delete row

            dataSource.Add(New DebtDTO With {.ID = Nothing,
                           .DEBT_TYPE_ID = cboDebtType.SelectedValue,
                           .DEBT_TYPE_NAME = cboDebtType.Text,
                           .MONEY = If(IsNumeric(rntxtDebtMoney.Value), Decimal.Parse(rntxtDebtMoney.Value), Nothing),
                           .REMARK = txtDebtNote.Text,
                           .DEBT_STATUS = cboDebtStatus.SelectedValue,
                           .DEBT_STATUS_NAME = cboDebtStatus.Text})
            Return dataSource
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function DeleteDebts(ByVal dataSource As List(Of DebtDTO)) As List(Of DebtDTO)
        Try
            'For Each item As GridDataItem In rgDebt.Items
            '    Dim item_Ck = (From p In dataSource.AsEnumerable Where p.DEBT_TYPE_ID = item.GetDataKeyValue("DEBT_TYPE_ID")).FirstOrDefault
            '    If item_Ck IsNot Nothing Then
            '        dataSource.Remove(item_Ck)
            '    End If
            'Next
            If hidCheckDelete.Value <> "" Then
                Dim item_Ck = (From p In dataSource.AsEnumerable Where p.DEBT_TYPE_ID = hidCheckDelete.Value).FirstOrDefault
                If item_Ck IsNot Nothing Then
                    dataSource.Remove(item_Ck)
                End If
            End If
            Return dataSource

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Event Click checkbox Tra the BHYT
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub chkInsuranceCard_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkInsuranceCard.CheckedChanged
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim rep As New ProfileBusinessRepository
    '        Dim dtData As New DataTable
    '        Dim salary As New Decimal
    '        '1. kiểm tra nếu tích trả thẻ bảo hiểm
    '        If chkInsuranceCard.Checked Then
    '            rdInsuranceDate.Enabled = True
    '            '2. nếu ngày thẻ và ngày làm việc cuối cùng có dữ liệu
    '            If rdInsuranceDate.SelectedDate IsNot Nothing And rdLastDate.SelectedDate IsNot Nothing Then
    '                '3. nếu tháng trả thẻ bhyt khác với tháng của ngày làm việc cuối cùng thì sẽ tính số tiền phải trả
    '                If rdInsuranceDate.SelectedDate.Value.Month <> rdLastDate.SelectedDate.Value.AddDays(-1).Month Then
    '                    dtData = rep.GetSalaryNew(hidEmpID.Value)
    '                    Dim month As Decimal = rdInsuranceDate.SelectedDate.Value.Month - rdLastDate.SelectedDate.Value.Month
    '                    Dim tileNV As Decimal = Utilities.ObjToDecima(rep.GetTyleNV().Rows(0)("HI_EMP"))
    '                    If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
    '                        salary = Utilities.ObjToDecima(rep.GetSalaryNew(hidEmpID.Value).Rows(0)("NEWSALARY"))
    '                    End If
    '                    rntxtInsuranceMoney.Value = Utilities.ObjToDecima(month * salary * tileNV / 100)
    '                End If
    '            End If
    '        Else '4. nếu trường hợp không tích chọn thì sẽ tính số tháng bằng cuối năm trừ đi ngày làm việc cuối cùng.
    '            rdInsuranceDate.SelectedDate = Nothing
    '            rdInsuranceDate.Enabled = False
    '            '5. kiểm tra nếu ngày làm việc cuối cùng có dữ liệu
    '            If rdLastDate.SelectedDate IsNot Nothing Then
    '                dtData = rep.GetSalaryNew(hidEmpID.Value)
    '                Dim month As Decimal = 12 - rdLastDate.SelectedDate.Value.Month
    '                Dim tileNV As Decimal = Utilities.ObjToDecima(rep.GetTyleNV().Rows(0)("HI_EMP"))
    '                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
    '                    salary = Utilities.ObjToDecima(rep.GetSalaryNew(hidEmpID.Value).Rows(0)("NEWSALARY"))
    '                End If
    '                rntxtInsuranceMoney.Value = Utilities.ObjToDecima(month * salary * tileNV / 100)
    '            End If
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '    End Try

    'End Sub
    ''' <summary>
    ''' Event thay doi value date cua rdInsuranceDate: Ngay tra the BHYT
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub rdInsuranceDate_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdInsuranceDate.SelectedDateChanged
    '    chkInsuranceCard_CheckedChanged(Nothing, Nothing)
    'End Sub
    ''' <summary>
    ''' Validate combobox trạng thái
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cvalStatus_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalStatus.ServerValidate
    '    Dim rep As New ProfileRepository
    '    Dim validate As New OtherListDTO
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
    '            validate.ID = cboStatus.SelectedValue
    '            validate.ACTFLG = "A"
    '            validate.CODE = ProfileCommon.OT_TER_STATUS.Name
    '            args.IsValid = rep.ValidateOtherList(validate)
    '        End If
    '        If Not args.IsValid Then
    '            GetDataCombo()
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method,
    '                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    Private Sub txtEmployeeCode_TextChanged(sender As Object, e As EventArgs) Handles txtEmployeeCode.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If txtEmployeeCode.Text <> "" Then
                    Reset_Find_Emp()
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = txtEmployeeCode.Text
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        txtEmployeeCode.Text = ""
                    ElseIf Count = 1 Then
                        Dim empID = EmployeeList(0).EMPLOYEE_ID
                        If empID IsNot Nothing Then
                            Using rep1 As New ProfileBusinessRepository
                                Dim check = rep1.Check_has_Ter(empID)
                                If check = 1 Then
                                    ShowMessage(Translate("Nhân viên đã có quyết định nghỉ việc"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End Using
                            'txtDecisionNo.Text = item.EMPLOYEE_CODE.Substring(1) + " / QDTV-KSF"
                            FillDataByEmployeeID(EmployeeList(0).ID)
                            If rdJoinDateState.SelectedDate IsNot Nothing And rdLastDate.SelectedDate IsNot Nothing Then

                                If rdJoinDateState.SelectedDate < rdLastDate.SelectedDate Then
                                    txtSeniority.Text = CalculateSeniority1(rdJoinDateState.SelectedDate, rdLastDate.SelectedDate)
                                Else
                                    txtSeniority.Text = vbNullString
                                End If
                            End If
                            'rgLabourProtect.Rebind()
                            rgDebt.Rebind()
                        End If

                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmployeeCode.Text
                            ctrlFindEmployeePopup.CurrentValue = SelectOrg
                            ctrlFindEmployeePopup.MustHaveContract = True
                            ctrlFindEmployeePopup.IS_3B = 2
                            phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                            ctrlFindEmployeePopup.MultiSelect = False
                            ctrlFindEmployeePopup.Show()
                            isLoadPopup = 1
                        End If
                    End If
                Else
                    Reset_Find_Emp()
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub Reset_Find_Emp()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ClearControlValue(txtContractNo, txtEmployeeName, txtTitleName, txtOrgName, rdJoinDateState, rdContractEffectDate, rdContractExpireDate, hidOrgAbbr, hidEmpID, hidTitleID,
                                hidOrgID, txtNotifyNo, txtSeniority, hiSalbasic)
            rgDebt.Rebind()
        Catch ex As Exception
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
            If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If
            If phFindSign.Controls.Contains(ctrlFindSigner) Then
                phFindSign.Controls.Remove(ctrlFindSigner)
                'Me.Views.Remove(ctrlFindSigner.ID.ToUpper)
            End If

            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.CurrentValue = SelectOrg
                    ctrlFindEmployeePopup.MustHaveContract = True
                    ctrlFindEmployeePopup.IS_3B = 2
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
                Case 2
                    ctrlFindSigner = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                    ctrlFindSigner.CurrentValue = SelectOrg
                    'ctrlFindSigner.MustHaveContract = True
                    ctrlFindSigner.LoadAllOrganization = True
                    ctrlFindSigner.FunctionName = "ctrlHU_TerminateNewEdit"
                    ctrlFindSigner.EmployeeOrg = If(hidOrgID.Value <> "", CDec(hidOrgID.Value), 0)
                    ctrlFindSigner.EffectDate = If(rdEffectDate.SelectedDate IsNot Nothing, CDbl(rdEffectDate.SelectedDate.Value.ToOADate), CDbl(New Date().ToOADate))
                    phFindSign.Controls.Add(ctrlFindSigner)
                    ctrlFindSigner.MultiSelect = False
            End Select
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
            If ListComboData Is Nothing Then
                ListComboData = New Profile.ProfileBusiness.ComboBoxDataDTO
                ListComboData.GET_TER_REASON = True
                ListComboData.GET_TER_STATUS = True
                ListComboData.GET_TYPE_NGHI = True
                ListComboData.GET_TYPE_INS_STATUS = True
                ListComboData.GET_DEBT_STATUS = True
                ListComboData.GET_DEBT_TYPE = True
                'ListComboData.GET_DECISION_TYPE = True
                ListComboData.GET_TER_DECISION_TYPE = True
                ListComboData.GET_HANDOVER_CONTENT = True
                rep.GetComboList(ListComboData)
            End If
            rep.Dispose()

            dt_TRUYTHUBHYT = rep.GetOtherList("TRUYTHUBHYT", True)
            FillRadCombobox(cboTruyThu_BHYT, dt_TRUYTHUBHYT, "NAME", "ID", True)

            Dim dtData As New DataTable
            dtData = rep.GetOtherList(OtherTypes.DecisionStatus, True)
            FillRadCombobox(cboStatus, dtData, "NAME", "ID", True)
            FillDropDownList(cboTerReason, ListComboData.LIST_TER_REASON, "NAME_VN", "ID", Common.Common.SystemLanguage, True)
            FillDropDownList(cboDebtStatus, ListComboData.LIST_DEBT_STATUS, "NAME_VN", "ID", Common.Common.SystemLanguage, True)
            FillDropDownList(cboDebtType, ListComboData.LIST_DEBT_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, True)
            FillRadCombobox(cboDecisionType, ListComboData.LIST_TER_DECISION_TYPE, "NAME_VN", "ID", True)

            cboStatus.SelectedValue = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID

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
                    Case 0
                        Refresh("InsertView")
                    Case 1
                        Refresh("UpdateView")
                    Case 2
                        Refresh("NormalView")
                    Case 3
                        Dim empID = Request.Params("empid")
                        FillDataByEmployeeID(empID)
                        If rdJoinDateState.SelectedDate IsNot Nothing And rdLastDate.SelectedDate IsNot Nothing Then
                            If rdJoinDateState.SelectedDate < rdLastDate.SelectedDate Then
                                txtSeniority.Text = CalculateSeniority1(rdJoinDateState.SelectedDate, rdLastDate.SelectedDate)
                            Else
                                txtSeniority.Text = vbNullString
                            End If

                        End If
                        hidEmpID.Value = empID
                        Refresh("InsertView")
                End Select
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Load dataSource cho grid Ly Do Nghi Viec
    ''' </summary>
    ''' <param name="lstReason"></param>
    ''' <remarks></remarks>
    Private Sub CreateDataHandoverContent(Optional ByVal lstHandoverContent As List(Of HandoverContentDTO) = Nothing)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If lstHandoverContent Is Nothing Then
                lstHandoverContent = New List(Of HandoverContentDTO)
            End If
            'rgHandoverContent.DataSource = lstHandoverContent
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Private Sub GenerateNofityNo()
        Dim store As New ProfileStoreProcedure
        Try
            If rdEffectDate.SelectedDate Is Nothing Or hidEmpID.Value = "" Then
                Exit Sub
            End If
            Dim dt = store.AUTOCREATE_NOTIFY_NO(Decimal.Parse(hidEmpID.Value), rdEffectDate.SelectedDate)
            txtNotifyNo.Text = dt.Rows(0)("NOTIFY_NO")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetSigner()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim store As New ProfileStoreProcedure
        Try
            ClearControlValue(txtSignerName, txtSignerTitle)
            If IsDate(rdEffectDate.SelectedDate) AndAlso cboDecisionType.SelectedValue <> "" Then
                Dim signer = store.GET_SIGNER_BY_FUNC(Me.ViewName, rdEffectDate.SelectedDate, cboDecisionType.SelectedValue)
                If signer.Rows.Count > 0 Then
                    txtSignerName.Text = signer.Rows(0)("EMPLOYEE_NAME")
                    txtSignerTitle.Text = signer.Rows(0)("TITLE_NAME")
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub AutoCreate_DecisionNo()
        Dim store As New ProfileStoreProcedure
        Try
            If hidEmpID.Value = "" Then
                Exit Sub
            End If

            If rdEffectDate.SelectedDate Is Nothing Then
                Exit Sub
            End If

            If cboDecisionType.SelectedValue Is Nothing Then
                Exit Sub
            End If


            ClearControlValue(txtDecisionNo)
            Dim contract_no = store.AUTOCREATE_DECISIONNO2(LogHelper.CurrentUser.EMPLOYEE_ID,
                                                           CDec(hidEmpID.Value),
                                                           If(IsNumeric(cboDecisionType.SelectedValue), cboDecisionType.SelectedValue, 0),
                                                           rdEffectDate.SelectedDate)

            txtDecisionNo.Text = contract_no
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Caculate"

    Private Sub cbIsBlackList_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbIsBlackList.CheckedChanged
        Try
            If cbIsBlackList.Checked Then
                lbReason_BlackList.Visible = True
                txtReason_BlackList.Visible = True
            Else
                lbReason_BlackList.Visible = False
                txtReason_BlackList.Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rdTerDate_SelectedDateChanged(ByVal sender As Object, ByVal e As SelectedDateChangedEventArgs) Handles rdTerDate.SelectedDateChanged
        Try
            If rdTerDate.SelectedDate IsNot Nothing Then
                rdLastDate.SelectedDate = rdTerDate.SelectedDate.Value.AddDays(-1)
                If CType(CommonConfig.dicConfig("APP_SETTING_3"), Boolean) Then
                    rdEffectDate.SelectedDate = rdTerDate.SelectedDate
                End If
                If rdJoinDateState.SelectedDate IsNot Nothing Then
                    If rdLastDate.SelectedDate IsNot Nothing Then
                        If rdJoinDateState.SelectedDate < rdLastDate.SelectedDate Then

                            txtSeniority.Text = CalculateSeniority1(rdJoinDateState.SelectedDate, rdLastDate.SelectedDate)
                        Else
                            txtSeniority.Text = vbNullString
                        End If
                    End If
                    If rdEffectDate.SelectedDate IsNot Nothing Then
                        If rdJoinDateState.SelectedDate > rdLastDate.SelectedDate Then
                            ShowMessage(Translate("Ngày thôi việc phải lớn hơn hoặc bằng ngày vào công ty"), NotifyType.Warning)
                        End If
                    End If
                End If
                GenerateNofityNo()
                AutoCreate_DecisionNo()
            End If
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
#End Region

#Region "DateDifference"

    Public Class DateDifference
        ''' <summary>
        ''' defining Number of days in month; index 0=> january and 11=> December
        ''' february contain either 28 or 29 days, that's why here value is -1
        ''' which wil be calculate later.
        ''' </summary>
        Private monthDay() As Integer = {31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}

        ''' <summary>
        ''' từ ngày
        ''' </summary>
        Private fromDate As Date

        ''' <summary>
        ''' đến ngày
        ''' </summary>
        Private toDate As Date

        ''' <summary>
        ''' 3 tham số trả về
        ''' </summary>
        Private year As Integer
        Private month As Integer
        Private day As Integer
        Dim _mylog As New MyLog()
        Dim _pathLog As String = _mylog._pathLog
        Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

        Public Sub New(ByVal d1 As Date, ByVal d2 As Date)
            Dim startTime As DateTime = DateTime.UtcNow
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try
                Dim increment As Integer

                If d1 > d2 Then
                    Me.fromDate = d2
                    Me.toDate = d1
                Else
                    Me.fromDate = d1
                    Me.toDate = d2
                End If

                ' Tính toán ngày
                increment = 0

                If Me.fromDate.Day > Me.toDate.Day Then
                    increment = Me.monthDay(Me.fromDate.Month - 1)
                End If

                ' Nếu là tháng 2
                If increment = -1 Then
                    ' kiểm tra năm nhuận
                    If Date.IsLeapYear(Me.fromDate.Year) Then
                        ' nếu là năm nhuận tháng 2 có 29 ngày
                        increment = 29
                    Else
                        increment = 28
                    End If
                End If
                ' Nếu không phải tháng 2
                If increment <> 0 Then
                    day = (Me.toDate.Day + increment) - Me.fromDate.Day
                    increment = 1
                Else
                    day = Me.toDate.Day - Me.fromDate.Day
                End If

                ' tính ra số tháng
                If (Me.fromDate.Month + increment) > Me.toDate.Month Then
                    Me.month = (Me.toDate.Month + 12) - (Me.fromDate.Month + increment)
                    increment = 1
                Else
                    Me.month = (Me.toDate.Month) - (Me.fromDate.Month + increment)
                    increment = 0
                End If

                ' tính ra số năm
                Me.year = Me.toDate.Year - (Me.fromDate.Year + increment)
                _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            End Try


        End Sub

        Public ReadOnly Property Years() As Integer
            Get
                Return Me.year
            End Get
        End Property

        Public ReadOnly Property Months() As Integer
            Get
                Return Me.month
            End Get
        End Property

        Public ReadOnly Property Days() As Integer
            Get
                Return Me.day
            End Get
        End Property

    End Class
#End Region

End Class
