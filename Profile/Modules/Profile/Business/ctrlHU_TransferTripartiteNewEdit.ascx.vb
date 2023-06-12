Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_TransferTripartiteNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindDirectPopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Public Overrides Property MustAuthorize As Boolean = False

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
#Region "Property"
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' OBJect Working
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Working As WorkingDTO
        Get
            Return ViewState(Me.ID & "_Working")
        End Get
        Set(ByVal value As WorkingDTO)
            ViewState(Me.ID & "_Working") = value
        End Set
    End Property

    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' '0 - normal
    '1 - Employee
    '2 - Sign
    '3 - Org
    '4 - Direct
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
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy giá trị parentID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property parentID As String
        Get
            Return ViewState(Me.ID & "_parentID")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_parentID") = value
        End Set
    End Property
    Property empID As Integer
        Get
            Return ViewState(Me.ID & "_empID")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_empID") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức VIewLoad
    ''' lấy giá trị các parametter
    ''' Làm mới trạng thái, các thiết lập trên trang
    ''' Cập nhật trạng thái các control trên trang
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
            ''DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgAllowCur.AllowSorting = False
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức bind dữ liệu cho các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Dim dtData
            Using rep As New ProfileRepository
                dtData = rep.GetOT_TripartiteTypeList()
            End Using
            FillRadCombobox(cboDecisionType, dtData, "NAME", "ID")
            If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                cboDecisionType.SelectedValue = dtData.Rows(0)("ID")
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMassTransferSalary
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Làm mới các trạng thái, thiết lập các control trên trang
    ''' với 2 trạng thái của view là UpdateView và NormalView
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
                    Working = rep.GetWorkingByID(Working)
                    hidID.Value = Working.ID.ToString
                    hidEmp.Value = Working.EMPLOYEE_ID
                    If Working.WORKING_OLD IsNot Nothing Then
                        With Working.WORKING_OLD
                            txtTitleNameOld.Text = .TITLE_NAME
                            txtTitleGroupOld.Text = .TITLE_GROUP_NAME
                            txtDecisionNoOld.Text = .DECISION_NO
                            txtDecisionTypeOld.Text = .DECISION_TYPE_NAME
                            rdEffectDateOld.SelectedDate = .EFFECT_DATE
                            rdExpireDateOld.SelectedDate = .EXPIRE_DATE
                            txtStaffRankOld.Text = .STAFF_RANK_NAME
                            If .STAFF_RANK_ID IsNot Nothing Then
                                hidStaffRank.Value = .STAFF_RANK_ID
                            End If
                            txtOrgNameOld.Text = .ORG_NAME
                        End With
                    End If
                    txtEmployeeCode.Text = Working.EMPLOYEE_CODE
                    txtEmployeeName.Text = Working.EMPLOYEE_NAME
                    If Working.TITLE_ID IsNot Nothing Then
                        cboTitle.SelectedValue = Working.TITLE_ID
                        cboTitle.Text = Working.TITLE_NAME
                    End If
                    txtTitleGroup.Text = Working.TITLE_GROUP_NAME
                    If Working.DIRECT_MANAGER IsNot Nothing Then
                        hidDirect.Value = Working.DIRECT_MANAGER
                        txtDirectManager.Text = Working.DIRECT_MANAGER_NAME
                    End If
                    hidOrg.Value = Working.ORG_ID
                    txtOrgName.Text = Working.ORG_NAME
                    If Working.STATUS_ID IsNot Nothing Then
                        cboStatus.SelectedValue = Working.STATUS_ID
                        cboStatus.Text = Working.STATUS_NAME
                    End If
                    rdEffectDate.SelectedDate = Working.EFFECT_DATE
                    rdExpireDate.SelectedDate = Working.EXPIRE_DATE
                    If Working.DECISION_TYPE_ID IsNot Nothing Then
                        cboDecisionType.SelectedValue = Working.DECISION_TYPE_ID
                        cboDecisionType.Text = Working.DECISION_TYPE_NAME
                    End If
                    If Working.STAFF_RANK_ID IsNot Nothing Then
                        cboStaffRank.SelectedValue = Working.STAFF_RANK_ID
                        cboStaffRank.Text = Working.STAFF_RANK_NAME
                    End If
                    txtDecisionNo.Text = Working.DECISION_NO
                    If Working.SAL_GROUP_ID IsNot Nothing Then
                        cboSalGroup.SelectedValue = Working.SAL_GROUP_ID
                        cboSalGroup.Text = Working.SAL_GROUP_NAME
                    End If
                    If Working.SAL_LEVEL_ID IsNot Nothing Then
                        cboSalLevel.SelectedValue = Working.SAL_LEVEL_ID
                        cboSalLevel.Text = Working.SAL_LEVEL_NAME
                    End If
                    If Working.SAL_RANK_ID IsNot Nothing Then
                        cboSalRank.SelectedValue = Working.SAL_RANK_ID
                        cboSalRank.Text = Working.SAL_RANK_NAME
                    End If
                    rntxtSalBasic.Value = Working.SAL_BASIC
                    rntxtCostSupport.Value = Working.COST_SUPPORT
                    rntxtPercentSalary.Value = Working.PERCENT_SALARY
                    rdSignDate.SelectedDate = Working.SIGN_DATE
                    If Working.SIGN_ID IsNot Nothing Then
                        hidSign.Value = Working.SIGN_ID
                    End If
                    txtSignName.Text = Working.SIGN_NAME
                    txtSignTitle.Text = Working.SIGN_TITLE
                    txtRemark.Text = Working.REMARK
                    rntxtPercentSalary.Value = Working.PERCENT_SALARY
                    'lstAllow = Working.lstAllowance
                    'rgAllow.Rebind()

                    'Dim total As Decimal = 0
                    'If rntxtCostSupport.Value IsNot Nothing Then
                    '    total = total + rntxtCostSupport.Value
                    'End If
                    'If rntxtSalBasic.Value IsNot Nothing Then
                    '    total = total + rntxtSalBasic.Value
                    'End If
                    'For Each item As GridDataItem In rgAllow.Items
                    '    If item.GetDataKeyValue("AMOUNT") IsNot Nothing Then
                    '        total = total + item.GetDataKeyValue("AMOUNT")
                    '    End If
                    'Next
                    'rntxtTotal.Value = total

                    If Working.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Or _
                        Working.STATUS_ID = ProfileCommon.DECISION_STATUS.NOT_APPROVE_ID Then
                        LeftPane.Enabled = False
                        MainToolBar.Items(0).Enabled = False
                    End If

                Case "NormalView"
                    CurrentState = CommonMessage.STATE_NEW
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện khi click Command OnMainToolbar
    ''' Các command Lưu, thêm mới, Hủy 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objWorking As New WorkingDTO
        Dim rep As New ProfileBusinessRepository
        'Dim dtData As DataTable
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE

                    If Page.IsValid Then
                        If cboStatus.SelectedValue <> "" Then
                            If cboStatus.SelectedValue = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                                If txtDecisionNo.Text = "" Then
                                    ShowMessage(Translate("Bạn phải nhập số tờ trình"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If
                        End If
                        If cboTitle.SelectedValue = "" Or cboTitle.SelectedValue = "-1" Then
                            ShowMessage(Translate("Bạn phải chọn chức danh"), NotifyType.Warning)
                            cboTitle.Focus()
                            Exit Sub
                        End If
                        If cboDecisionType.SelectedValue = "" Or cboDecisionType.SelectedValue = "-1" Then
                            ShowMessage(Translate("Bạn phải chọn loại tờ trình"), NotifyType.Warning)
                            cboDecisionType.Focus()
                            Exit Sub
                        End If
                        If cboStaffRank.SelectedValue = "" Or cboStaffRank.SelectedValue = "-1" Then
                            ShowMessage(Translate("Bạn phải chọn cấp nhân sự"), NotifyType.Warning)
                            cboStaffRank.Focus()
                            Exit Sub
                        End If
                        If cboStatus.SelectedValue = "" Or cboStatus.SelectedValue = "-1" Then
                            ShowMessage(Translate("Bạn phải chọn trạng thái"), NotifyType.Warning)
                            cboStatus.Focus()
                            Exit Sub
                        End If
                        If rdEffectDate.SelectedDate Is Nothing Then
                            ShowMessage(Translate("Bạn phải chọn ngày hiệu lực"), NotifyType.Warning)
                            rdEffectDate.Focus()
                            Exit Sub
                        End If

                        Dim gID As Decimal
                        With objWorking
                            .EMPLOYEE_ID = hidEmp.Value
                            If cboTitle.SelectedValue <> "" Then
                                .TITLE_ID = cboTitle.SelectedValue
                            End If

                            .ORG_ID = hidOrg.Value
                            If cboStatus.SelectedValue <> "" Then
                                .STATUS_ID = cboStatus.SelectedValue
                            End If
                            .EFFECT_DATE = rdEffectDate.SelectedDate
                            .EXPIRE_DATE = rdExpireDate.SelectedDate
                            If cboDecisionType.SelectedValue <> "" Then
                                .DECISION_TYPE_ID = cboDecisionType.SelectedValue
                            End If
                            .DECISION_NO = txtDecisionNo.Text.Trim
                            If cboSalGroup.SelectedValue <> "" Then
                                .SAL_GROUP_ID = cboSalGroup.SelectedValue
                            End If
                            If cboSalLevel.SelectedValue <> "" Then
                                .SAL_LEVEL_ID = cboSalLevel.SelectedValue
                            End If
                            If cboSalRank.SelectedValue <> "" Then
                                .SAL_RANK_ID = cboSalRank.SelectedValue
                            End If
                            If cboStaffRank.SelectedValue <> "" Then
                                .STAFF_RANK_ID = cboStaffRank.SelectedValue
                            End If
                            .SAL_BASIC = rntxtSalBasic.Value
                            .COST_SUPPORT = rntxtCostSupport.Value
                            .SIGN_DATE = rdSignDate.SelectedDate
                            If hidSign.Value <> "" Then
                                .SIGN_ID = hidSign.Value
                            End If
                            If hidDirect.Value <> "" Then
                                .DIRECT_MANAGER = hidDirect.Value
                            End If
                            .SIGN_NAME = txtSignName.Text
                            .SIGN_TITLE = txtSignTitle.Text
                            .REMARK = txtRemark.Text.Trim
                            .IS_PROCESS = False
                            .IS_MISSION = True
                            .IS_WAGE = False
                            .IS_3B = True
                            .PERCENT_SALARY = rntxtPercentSalary.Value

                            .lstAllowance = New List(Of WorkingAllowanceDTO)
                        End With

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertWorking3B(objWorking, gID) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    ''POPUPTOLINK
                                    Select Case parentID
                                        Case "Emp"
                                            Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=" + empID.ToString + "&state=Normal")
                                        Case "Transfer"
                                            Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_TransferTripartiteMng&group=Business")
                                    End Select
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objWorking.ID = Decimal.Parse(hidID.Value)
                                If rep.ModifyWorking3B(objWorking, gID) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    ''POPUPTOLINK
                                    Select Case parentID
                                        Case "Emp"
                                            Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=" + empID.ToString + "&state=Normal")
                                        Case "Transfer"
                                            Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_TransferTripartiteMng&group=Business")
                                    End Select

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                        If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                            ctrlMessageBox.MessageText = Translate("Đi tới nghiệp vụ nghỉ việc cho nhân viên?")
                            ctrlMessageBox.ActionName = "CALL_TERMINATE"
                            ctrlMessageBox.DataBind()
                            ctrlMessageBox.Show()
                        Else
                            ''POPUPTOLINK
                            Select Case parentID
                                Case "Emp"
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=" + empID.ToString + "&state=Normal")
                                Case "Transfer"
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_TransferTripartiteMng&group=Business")
                            End Select

                        End If
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    'Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtlProfile&group=Business")
                    'Select Case parentID
                    'Case "Transfer"
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_TransferTripartiteMng&group=Business")
                    'End Select
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện button command control ctrlMessageBox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.ActionName = "CALL_TERMINATE" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                ExcuteScript("RedirectTer", "RedirectTerminate(" & hidEmp.Value & ")")
            Else
                Dim str As String = "getRadWindow().close('1');"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Click khi click vào btnFindCommon
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFindCommon_Click(ByVal sender As Object,
                                    ByVal e As EventArgs) Handles _
                                btnFindEmployee.Click,
                                btnFindSign.Click,
                                btnFindDirect.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
                Case btnFindSign.ID
                    isLoadPopup = 2
                Case btnFindDirect.ID
                    isLoadPopup = 4
            End Select

            UpdateControlState()
            Select Case sender.ID
                Case btnFindEmployee.ID
                    ctrlFindEmployeePopup.Show()
                Case btnFindSign.ID
                    ctrlFindSigner.MustHaveContract = True
                    ctrlFindSigner.Show()
                Case btnFindDirect.ID
                    ctrlFindDirectPopup.MustHaveContract = True
                    ctrlFindDirectPopup.Show()
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Cancel Click của control ctrlFind
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFind_CancelClick(ByVal sender As Object,
                                     ByVal e As System.EventArgs) Handles _
                                 ctrlFindEmployeePopup.CancelClicked,
                                 ctrlFindSigner.CancelClicked,
                                 ctrlFindOrgPopup.CancelClicked,
                                 ctrlFindOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Selected của control ctrlFindEmployeePopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim empID = ctrlFindEmployeePopup.SelectedEmployeeID(0)
            FillData(empID)
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Selected của control ctrlFindSigner
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindSigner_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner.EmployeeSelected
        Dim objEmployee As CommonBusiness.EmployeePopupFindDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            objEmployee = ctrlFindSigner.SelectedEmployee(0)
            txtSignName.Text = objEmployee.FULLNAME_VN
            txtSignTitle.Text = objEmployee.TITLE_NAME
            hidSign.Value = objEmployee.EMPLOYEE_ID
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Selected của control ctrlFindDirectPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindDirectPopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindDirectPopup.EmployeeSelected
        Dim objEmployee As CommonBusiness.EmployeePopupFindDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            objEmployee = ctrlFindDirectPopup.SelectedEmployee(0)
            txtDirectManager.Text = objEmployee.FULLNAME_VN
            hidDirect.Value = objEmployee.EMPLOYEE_ID
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện ItemRequested của cboComon
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
    Handles cboSalGroup.ItemsRequested, cboSalLevel.ItemsRequested,
    cboSalRank.ItemsRequested, cboStatus.ItemsRequested, cboTitle.ItemsRequested,
    cboStaffRank.ItemsRequested
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Using rep As New ProfileRepository
                Dim dtData As DataTable
                Dim sText As String = e.Text
                Dim dValue As Decimal
                Dim dateValue As Date
                Select Case sender.ID
                    Case cboDecisionType.ID
                        dtData = rep.GetOT_TripartiteTypeList(True)
                    Case cboSalGroup.ID
                        If e.Context("valueCustom") Is Nothing Then
                            dateValue = Date.Now
                        Else
                            dateValue = Date.Parse(e.Context("valueCustom"))
                        End If
                        dtData = rep.GetSalaryGroupList(dateValue, True)
                    Case cboSalLevel.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetSalaryLevelList(dValue, True)
                    Case cboSalRank.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetSalaryRankList(dValue, True)
                    Case cboStatus.ID
                        dtData = rep.GetOtherList("DECISION_STATUS", True)
                    Case cboTitle.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetTitleByOrgID(dValue, True)
                    Case cboStaffRank.ID
                        dtData = rep.GetStaffRankList(True)
                End Select

                If sText <> "" Then
                    Dim dtExist = (From p In dtData
                                   Where p("NAME") IsNot DBNull.Value AndAlso _
                                   p("NAME").ToString.ToUpper = sText.ToUpper)

                    If dtExist.Count = 0 Then
                        Dim dtFilter = (From p In dtData
                                        Where p("NAME") IsNot DBNull.Value AndAlso _
                                        p("NAME").ToString.ToUpper.Contains(sText.ToUpper))

                        If dtFilter.Count > 0 Then
                            dtData = dtFilter.CopyToDataTable
                        Else
                            dtData = dtData.Clone
                        End If

                        Dim itemOffset As Integer = e.NumberOfItems
                        Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                        e.EndOfItems = endOffset = dtData.Rows.Count

                        For i As Integer = itemOffset To endOffset - 1
                            Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                            Select Case sender.ID
                                Case cboSalRank.ID
                                    radItem.Attributes("SALARY_BASIC") = dtData.Rows(i)("SALARY_BASIC").ToString()
                                Case cboTitle.ID
                                    radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                            End Select
                            sender.Items.Add(radItem)
                        Next
                    Else

                        Dim itemOffset As Integer = e.NumberOfItems
                        Dim endOffset As Integer = dtData.Rows.Count
                        e.EndOfItems = True

                        For i As Integer = itemOffset To endOffset - 1
                            Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                            Select Case sender.ID
                                Case cboSalRank.ID
                                    radItem.Attributes("SALARY_BASIC") = dtData.Rows(i)("SALARY_BASIC").ToString()
                                Case cboTitle.ID
                                    radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                            End Select
                            sender.Items.Add(radItem)
                        Next
                    End If
                Else
                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count

                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        Select Case sender.ID
                            Case cboSalRank.ID
                                radItem.Attributes("SALARY_BASIC") = dtData.Rows(i)("SALARY_BASIC").ToString()
                            Case cboTitle.ID
                                radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                        End Select
                        sender.Items.Add(radItem)
                    Next
                End If
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Selected của control ctrlOrgPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrg.Value = e.CurrentValue
                txtOrgName.Text = orgItem.NAME_VN
                'txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
            End If
            cboTitle.ClearValue()
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Click khi click vào btnFindOrg
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindOrg.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 3
            UpdateControlState()
            ctrlFindOrgPopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện server validate của control cusExistEffectDate
    ''' với các trạng thái thêm mới, sứa
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>

    Private Sub cusExistEffectDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusExistEffectDate.ServerValidate
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If hidEmp.Value = "" Then
                args.IsValid = True
                Exit Sub
            End If
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    Using rep As New ProfileBusinessRepository
                        args.IsValid = rep.ValidateWorking("EXIST_EFFECT_DATE",
                                                            New WorkingDTO With {
                                                                .EMPLOYEE_ID = hidEmp.Value,
                                                                .EFFECT_DATE = rdEffectDate.SelectedDate})
                    End Using
                Case CommonMessage.STATE_EDIT
                    Using rep As New ProfileBusinessRepository
                        args.IsValid = rep.ValidateWorking("EXIST_EFFECT_DATE",
                                                            New WorkingDTO With {
                                                                .ID = hidID.Value,
                                                                .EMPLOYEE_ID = hidEmp.Value,
                                                                .EFFECT_DATE = rdEffectDate.SelectedDate})
                    End Using
                Case Else
                    args.IsValid = True
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource của rad grid rgAllowCur
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgAllowCur_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgAllowCur.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If rdEffectDateOld.SelectedDate Is Nothing Or hidEmp.Value = "" Then
                rgAllowCur.DataSource = New List(Of WorkingAllowanceDTO)
                Exit Sub
            End If
            Dim rep As New ProfileBusinessRepository
            Dim obj = New WorkingAllowanceDTO With {
                .EMPLOYEE_ID = hidEmp.Value,
                .EFFECT_DATE = rdEffectDateOld.SelectedDate}

            If hidID.Value <> "" Then
                obj.HU_WORKING_ID = hidID.Value
            End If
            rgAllowCur.DataSource = rep.GetAllowanceByDate(obj)
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' cvalOrgName server validate -- Đơn vị chuyển đến
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalOrgName_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalOrgName.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New OrganizationDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.NAME_VN = txtOrgName.Text.Trim
                validate.NAME_EN = txtOrgName.Text.Trim
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateOrganization(validate)
            End If
            If Not args.IsValid Then
                txtOrgName.Text = String.Empty
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' cvalDecisionType server validate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalDecisionType_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalDecisionType.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New OtherListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboDecisionType.SelectedValue
                validate.ACTFLG = "A"
                validate.CODE = ProfileCommon.DECISION_TYPE.Name
                args.IsValid = rep.ValidateOtherList(validate)
            End If
            If Not args.IsValid Then
                LoadCombo("DecisionType")
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' cvalTitle server validate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalTitle_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalTitle.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New TitleDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboTitle.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateTitle(validate)
            End If
            If Not args.IsValid Then
                LoadCombo("Title")
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' cvalStaffRank server validate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalStaffRank_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalStaffRank.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New StaffRankDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboStaffRank.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateStaffRank(validate)
            End If
            If Not args.IsValid Then
                LoadCombo("StaffRank")
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' cusStatus server validate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cusStatus_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusStatus.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New OtherListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboStatus.SelectedValue
                validate.ACTFLG = "A"
                validate.CODE = ProfileCommon.DECISION_STATUS.Name
                args.IsValid = rep.ValidateOtherList(validate)
            End If
            If Not args.IsValid Then
                LoadCombo("DecisionStatus")
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    ''' <summary>
    ''' Load combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadCombo(ByVal name As String)
        Dim rep As New ProfileRepository
        Dim comboBoxDataDTO As New ComboBoxDataDTO
        'Dim dValue As Decimal

        rep.GetComboList(comboBoxDataDTO) 'Lấy danh sách các Combo.
        If (name.ToUpper.Trim = "DecisionType".Trim.ToUpper) Then
            comboBoxDataDTO.GET_DECISION_TYPE = True
            If Not comboBoxDataDTO Is Nothing Then
                FillDropDownList(cboDecisionType, comboBoxDataDTO.LIST_DECISION_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, True)
            End If
        ElseIf (name.ToUpper.Trim = "Title".Trim.ToUpper) Then
            cboTitle.ClearValue()
        ElseIf (name.ToUpper.Trim = "StaffRank".Trim.ToUpper) Then
            cboStaffRank.ClearValue()
        ElseIf (name.ToUpper.Trim = "DecisionStatus".Trim.ToUpper) Then
            comboBoxDataDTO.GET_DECISION_STATUS = True
            If Not comboBoxDataDTO Is Nothing Then
                FillDropDownList(cboTitle, comboBoxDataDTO.LIST_DECISION_STATUS, "NAME_VN", "ID", Common.Common.SystemLanguage, True)
            End If
        End If
        rep.Dispose()
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức cập nhật trạng thái của các control trên page
    ''' trạng thái sửa với isPopup = 1,2,3,4
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW

                Case CommonMessage.STATE_EDIT
                    EnableControlAll(False, btnFindEmployee)
            End Select
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.LoadAllOrganization = False
                        ctrlFindEmployeePopup.MustHaveContract = True
                    End If
                Case 2
                    If Not phFindSign.Controls.Contains(ctrlFindSigner) Then
                        ctrlFindSigner = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                        phFindSign.Controls.Add(ctrlFindSigner)
                        ctrlFindSigner.MultiSelect = False
                        ctrlFindSigner.LoadAllOrganization = True
                        ctrlFindSigner.MustHaveContract = False
                    End If
                Case 3
                    If Not phFindOrg.Controls.Contains(ctrlFindOrgPopup) Then
                        ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                        ctrlFindOrgPopup.LoadAllOrganization = True
                        phFindOrg.Controls.Add(ctrlFindOrgPopup)
                    End If
                Case 4
                    If Not phFindDirect.Controls.Contains(ctrlFindDirectPopup) Then
                        ctrlFindDirectPopup = Me.Register("ctrlFindDirectPopup", "Common", "ctrlFindEmployeePopup")
                        phFindDirect.Controls.Add(ctrlFindDirectPopup)
                        ctrlFindDirectPopup.MultiSelect = False
                        ctrlFindDirectPopup.LoadAllOrganization = True
                    End If
            End Select
            EnableControlAll(False, cboSalGroup, cboSalLevel, cboSalRank, rntxtCostSupport, rntxtPercentSalary, rntxtSalBasic)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức lấy danh sách các parametter trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    Dim ID As String = Request.Params("ID")
                    If Working Is Nothing Then
                        Working = New WorkingDTO With {.ID = Decimal.Parse(ID)}
                    End If
                    Refresh("UpdateView")
                    Exit Sub
                End If
                If Request.Params("empID") IsNot Nothing Then
                    empID = Request.Params("empID")
                    FillData(empID)
                End If
                If Request.Params("parentID") IsNot Nothing Then
                    parentID = Request.Params("parentID")
                End If
                Refresh("NormalView")
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Đổ dữ liệu cho các control trên page
    ''' </summary>
    ''' <param name="empid"></param>
    ''' <remarks></remarks>
    Private Sub FillData(ByVal empid As Decimal)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Using rep As New ProfileBusinessRepository
                Dim obj = rep.GetEmployeCurrentByID(New WorkingDTO With {.EMPLOYEE_ID = empid})
                If obj.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                    Exit Sub
                End If
                hidID.Value = obj.ID.ToString
                hidEmp.Value = obj.EMPLOYEE_ID
                txtEmployeeCode.Text = obj.EMPLOYEE_CODE
                txtEmployeeName.Text = obj.EMPLOYEE_NAME
                txtTitleNameOld.Text = obj.TITLE_NAME
                txtTitleGroupOld.Text = obj.TITLE_GROUP_NAME
                txtDecisionNoOld.Text = obj.DECISION_NO
                txtDecisionTypeOld.Text = obj.DECISION_TYPE_NAME
                txtOrgNameOld.Text = obj.ORG_NAME
                rdEffectDateOld.SelectedDate = obj.EFFECT_DATE
                rdExpireDateOld.SelectedDate = obj.EXPIRE_DATE
                txtStaffRankOld.Text = obj.STAFF_RANK_NAME
                If obj.STAFF_RANK_ID IsNot Nothing Then
                    hidStaffRank.Value = obj.STAFF_RANK_ID
                End If
                If obj.SAL_GROUP_ID IsNot Nothing Then
                    cboSalGroup.SelectedValue = obj.SAL_GROUP_ID
                    cboSalGroup.Text = obj.SAL_GROUP_NAME
                End If
                If obj.SAL_LEVEL_ID IsNot Nothing Then
                    cboSalLevel.SelectedValue = obj.SAL_LEVEL_ID
                    cboSalLevel.Text = obj.SAL_LEVEL_NAME
                End If
                If obj.SAL_RANK_ID IsNot Nothing Then
                    cboSalRank.SelectedValue = obj.SAL_RANK_ID
                    cboSalRank.Text = obj.SAL_RANK_NAME
                End If
                rntxtSalBasic.Value = obj.SAL_BASIC
                rntxtCostSupport.Value = obj.COST_SUPPORT
                rntxtPercentSalary.Value = obj.PERCENT_SALARY
                rgAllowCur.Rebind()
                'Dim total As Decimal = 0
                'If rntxtCostSupport.Value IsNot Nothing Then
                '    total = total + rntxtCostSupport.Value
                'End If
                'If rntxtSalBasic.Value IsNot Nothing Then
                '    total = total + rntxtSalBasic.Value
                'End If
                'For Each item As GridDataItem In rgAllow.Items
                '    If item.GetDataKeyValue("AMOUNT") IsNot Nothing Then
                '        total = total + item.GetDataKeyValue("AMOUNT")
                '    End If
                'Next
                'rntxtTotal.Value = total
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub


#End Region

End Class