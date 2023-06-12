Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ctrlHU_TerminateNewEdit
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_Terminate3BNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
#Region "Property"

    ''' <summary>
    ''' Obj Terminate3B
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Terminate3B As Terminate3BDTO
        Get
            Return ViewState(Me.ID & "_Terminate3B")
        End Get
        Set(ByVal value As Terminate3BDTO)
            ViewState(Me.ID & "_Terminate3B") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj objTerminate3B
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property objTerminate3B As Terminate3BDTO
        Get
            Return ViewState(Me.ID & "_objTerminate3B")
        End Get
        Set(ByVal value As Terminate3BDTO)
            ViewState(Me.ID & "_objTerminate3B") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj ListComboData
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
    ''' Obj SelectOrg
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SelectOrg As String
        Get
            Return ViewState(Me.ID & "_SelectOrg")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_SelectOrg") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj isLoadPopup
    ''' 0 - normal
    ''' 1 - Employee
    ''' 2 - Sign
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

    Dim FormType As Integer
    Dim IDSelect As Decimal?
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
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
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Gọi phương thức khởi tạo 
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

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức bind dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            GetDataCombo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo giá trị cho các control trên page
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.MainToolBar = tbarTerminate
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)

            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Reset Page theo trạng thái
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Try
            EnableControlAll(False, rdContractExpireDate, rdContractEffectDate)
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Terminate3B = rep.GetTerminate3BByID(New Terminate3BDTO With {.ID = IDSelect})

                    If Terminate3B.WORK_STATUS IsNot Nothing Then
                        hidWorkStatus.Value = Terminate3B.WORK_STATUS
                    End If
                    txtSignerName.Text = Terminate3B.SIGN_NAME
                    txtSignerTitle.Text = Terminate3B.SIGN_TITLE
                    txtSeniority.Text = Terminate3B.EMPLOYEE_SENIORITY

                    cboStatus.SelectedValue = Terminate3B.STATUS_ID.ToString

                    rdSignDate.SelectedDate = Terminate3B.SIGN_DATE
                    rdEffectDate3B.SelectedDate = Terminate3B.EFFECT_DATE
                    rdSignDate.SelectedDate = Terminate3B.SIGN_DATE


                    hidDecisionID.Value = Terminate3B.DECISION_ID.ToString
                    hidEmpID.Value = Terminate3B.EMPLOYEE_ID
                    hidTitleID.Value = Terminate3B.TITLE_ID
                    hidOrgID.Value = Terminate3B.ORG_ID
                    hidID.Value = Terminate3B.ID.ToString
                    FillDataByEmployeeID(Terminate3B.EMPLOYEE_ID)

                    chkIdenitifiCard.Checked = Utilities.ObjToDecima(Terminate3B.IDENTIFI_CARD, -1)
                    rdIdentifiDate.SelectedDate = Terminate3B.IDENTIFI_RDATE
                    txtIdentifiStatus.Text = Terminate3B.IDENTIFI_STATUS
                    chkSunCard.Checked = Utilities.ObjToDecima(Terminate3B.SUN_CARD, -1)
                    rdSunDate.SelectedDate = Terminate3B.SUN_RDATE
                    txtSunStatus.Text = Terminate3B.SUN_STATUS
                    chkInsuranceCard.Checked = Utilities.ObjToDecima(Terminate3B.INSURANCE_CARD, -1)
                    rdInsuranceDate.SelectedDate = Terminate3B.INSURANCE_RDATE
                    txtInsuranceStatus.Text = Terminate3B.INSURANCE_STATUS
                    rntxtIdentifiMoney.Value = Terminate3B.IDENTIFI_MONEY
                    rntxtSunMoney.Value = Terminate3B.SUN_MONEY
                    rntxtInsuranceMoney.Value = Terminate3B.INSURANCE_MONEY
                    rntxtRemainingLeave.Value = Terminate3B.REMAINING_LEAVE
                    chkIsRemainingLeave.Checked = Terminate3B.IS_REMAINING_LEAVE
                    chkIsCompensatoryLeave.Checked = Terminate3B.IS_COMPENSATORY_LEAVE
                    rntxtCompensatoryLeave.Value = Terminate3B.COMPENSATORY_LEAVE

                    ' phê duyệt và ko phê duyêt
                    If Terminate3B.STATUS_ID = 262 Or _
                        Terminate3B.STATUS_ID = 264 Then
                        RadPane2.Enabled = False
                        MainToolBar.Items(0).Enabled = False
                    End If

                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
                Case "NormalView"

                    Refresh("UpdateView")
                    btnFindEmployee.Enabled = False
                    btnFindSinger.Enabled = False
                    CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    txtSignerName.ReadOnly = True
                    txtSignerTitle.ReadOnly = True
                    txtSeniority.ReadOnly = True

                    cboStatus.Enabled = False
                    rdSignDate.Enabled = False

                    txtContractNo.ReadOnly = True
                    txtEmployeeCode.ReadOnly = True
                    txtEmployeeName.ReadOnly = True
                    txtTitleName.ReadOnly = True
                    txtStaffRankName.ReadOnly = True
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

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện command tren toolbar khi click vao cac item cua no
    ''' Cac command la luu, huy 
    ''' Cập nhật lại trạng thái các control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim _filter As New Terminate3BDTO
        Dim dtData As New DataTable
        Dim _objfilter As New Terminate3BDTO

        Dim gid As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If CurrentState = Common.CommonMessage.STATE_EDIT Then
                            _objfilter.ID = hidID.Value
                        End If
                        objTerminate3B = New Terminate3BDTO
                        objTerminate3B.EMPLOYEE_ID = hidEmpID.Value
                        objTerminate3B.ORG_ID = hidOrgID.Value
                        objTerminate3B.TITLE_ID = hidTitleID.Value
                        objTerminate3B.STATUS_ID = cboStatus.SelectedValue
                        If CurrentState = Common.CommonMessage.STATE_EDIT Then
                            objTerminate3B.ID = Decimal.Parse(hidDecisionID.Value)
                        End If

                        objTerminate3B.EMPLOYEE_SENIORITY = txtSeniority.Text

                        objTerminate3B.EFFECT_DATE = rdEffectDate3B.SelectedDate
                        objTerminate3B.SIGN_NAME = txtSignerName.Text
                        objTerminate3B.SIGN_DATE = rdSignDate.SelectedDate

                        objTerminate3B.IDENTIFI_CARD = chkIdenitifiCard.Checked
                        objTerminate3B.IDENTIFI_RDATE = rdIdentifiDate.SelectedDate
                        objTerminate3B.IDENTIFI_STATUS = txtIdentifiStatus.Text
                        objTerminate3B.SUN_CARD = chkSunCard.Checked
                        objTerminate3B.SUN_RDATE = rdSunDate.SelectedDate
                        objTerminate3B.SUN_STATUS = txtSunStatus.Text
                        objTerminate3B.INSURANCE_CARD = chkInsuranceCard.Checked
                        objTerminate3B.INSURANCE_RDATE = rdInsuranceDate.SelectedDate
                        objTerminate3B.INSURANCE_STATUS = txtInsuranceStatus.Text

                        objTerminate3B.IDENTIFI_MONEY = rntxtIdentifiMoney.Value
                        objTerminate3B.SUN_MONEY = rntxtSunMoney.Value
                        objTerminate3B.INSURANCE_MONEY = rntxtInsuranceMoney.Value
                        objTerminate3B.IS_REMAINING_LEAVE = chkIsRemainingLeave.Checked
                        objTerminate3B.IS_COMPENSATORY_LEAVE = chkIsCompensatoryLeave.Checked
                        objTerminate3B.REMAINING_LEAVE = rntxtRemainingLeave.Value
                        objTerminate3B.COMPENSATORY_LEAVE = rntxtCompensatoryLeave.Value

                        objTerminate3B.ORG_ABBR = hidOrgAbbr.Value

                        objTerminate3B.SIGN_TITLE = txtSignerTitle.Text

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertTerminate3B(objTerminate3B, gid) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Terminate3B&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                If hidWorkStatus.Value = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                                    ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), NotifyType.Warning)
                                    Exit Sub
                                End If
                                objTerminate3B.ID = Decimal.Parse(hidID.Value)
                                objTerminate3B.DECISION_ID = Decimal.Parse(hidDecisionID.Value)
                                Dim lstID As New List(Of Decimal)
                                lstID.Add(objTerminate3B.ID)
                                If rep.ValidateBusiness("HU_TERMINATE_3B", "ID", lstID) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                    Exit Sub
                                End If
                                If rep.ModifyTerminate3B(objTerminate3B, gid) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Terminate3B&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select


                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Terminate3B&group=Business")
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien clicked cua control ctrlMessageBox_Button
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
                        If rep.InsertTerminate3B(objTerminate3B, gID) Then
                            Dim str As String = "getRadWindow().close('1');"
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    Case CommonMessage.STATE_EDIT
                        objTerminate3B.ID = Decimal.Parse(hidID.Value)
                        objTerminate3B.DECISION_ID = Decimal.Parse(hidDecisionID.Value)
                        If rep.ModifyTerminate3B(objTerminate3B, gID) Then
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

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button btnFindSinger
    ''' Hien thi popup co isLoadPopup = 2 khi click vao button
    ''' Cap nhat lai trang thai của cac control tren page
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
            ctrlFindSigner.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button btnFindEmployee
    ''' Hien thi popup co isLoadPopup = 1 khi click vao button
    ''' Cap nhat lai trang thai của cac control tren page
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

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien selected cua control ctrlFindEmployeePopup_Employee
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
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                txtEmployeeCode.Text = item.EMPLOYEE_CODE
                FillDataByEmployeeID(item.ID)
                If rdJoinDateState.SelectedDate IsNot Nothing And rdEffectDate3B.SelectedDate IsNot Nothing Then
                    If rdJoinDateState.SelectedDate < rdEffectDate3B.SelectedDate.Value.AddDays(-1) Then
                        txtSeniority.Text = CalculateSeniority(rdJoinDateState.SelectedDate, rdEffectDate3B.SelectedDate.Value.AddDays(-1))
                    Else
                        txtSeniority.Text = vbNullString
                    End If
                End If
                rgLabourProtect.Rebind()
                rgAsset.Rebind()
                GetTerminate3BCalculate()
            End If
            rep.Dispose()
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien fill data cho cac textbox theo nhan da chon 
    ''' </summary>
    ''' <param name="gID"></param>
    ''' <remarks></remarks>
    Private Sub FillDataByEmployeeID(ByVal gID As Decimal)
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim emp = rep.GetTerminate3bEmployeeByID(gID)
            If emp IsNot Nothing Then
                txtContractNo.Text = emp.CONTRACT_NO
                txtEmployeeCode.Text = emp.EMPLOYEE_CODE
                txtEmployeeName.Text = emp.FULLNAME_VN
                txtTitleName.Text = emp.TITLE_NAME_VN
                txtStaffRankName.Text = emp.STAFF_RANK_NAME
                txtOrgName.Text = emp.ORG_NAME
                rdJoinDateState.SelectedDate = emp.JOIN_DATE_STATE
                rdContractEffectDate.SelectedDate = emp.CONTRACT_EFFECT_DATE
                rdContractExpireDate.SelectedDate = emp.CONTRACT_EXPIRE_DATE
                rntxtCostSupport.Value = emp.COST_SUPPORT
                rntxtSalBasic.Value = emp.SAL_BASIC
                rdEffectDate3B.SelectedDate = emp.EFFECT_DATE
                hidOrgAbbr.Value = emp.ORG_ID
                hidEmpID.Value = emp.ID
                hidTitleID.Value = emp.TITLE_ID
                hidOrgID.Value = emp.ORG_ID
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Tính toán thâm niên làm việc của nhân viên
    ''' Thâm niên công tác = ngày làm việc cuối - ngày vào làm
    ''' ngày nhỏ hơn 15 = 0.5 ngày
    ''' ngày lớn hơn hoặc bằng 15 = 1 tháng
    ''' </summary>
    ''' <param name="dStart"></param>
    ''' <param name="dEnd"></param>
    ''' <returns name="str"></returns>
    ''' <remarks></remarks>
    Private Function CalculateSeniority(ByVal dStart As Date, ByVal dEnd As Date) As String
        Dim dSoNam As Double
        Dim dSoThang As Double
        Dim dNgayDuThang As Double
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim str As String = ""
        Try

            Dim clsDate As DateDifference = New DateDifference(dStart, dEnd)
            dSoNam = clsDate.Years
            dSoThang = clsDate.Months
            dNgayDuThang = clsDate.Days

            If dNgayDuThang < 15 Then
                dSoThang = dSoThang + 0.5
            Else
                dSoThang = dSoThang + 1
                If dSoThang = 12 Then
                    dSoNam = dSoNam + 1
                    dSoThang = 0
                End If
            End If

            If dSoNam = 0 And dSoThang = 0 Then
                str = ""
            End If

            If dSoNam = 0 And dSoThang <> 0 Then
                str = dSoThang & " tháng"
            End If

            If dSoNam <> 0 And dSoThang = 0 Then
                str = dSoNam & " năm"
            End If

            If dSoNam <> 0 And dSoThang <> 0 Then
                str = dSoNam & " năm " & dSoThang & " tháng"
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
        Return str
    End Function

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien selected cua control ctrlFindSigner_Employee
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

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button btnFindEmployee
    ''' Dong popup co isLoadPopup = 0 khi click vao button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked, ctrlFindSigner.CancelClicked
        isLoadPopup = 0
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rgLabourProtect 
    ''' Tao du lieu filter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgLabourProtect_NeedDataSource(sender As Object, e As System.EventArgs) Handles rgLabourProtect.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Using rep As New ProfileBusinessRepository
                Dim lstData As List(Of LabourProtectionMngDTO)
                If hidEmpID.Value <> "" Then
                    lstData = rep.GetLabourProtectByTerminate(hidEmpID.Value)
                Else
                    lstData = New List(Of LabourProtectionMngDTO)
                End If
                rgLabourProtect.DataSource = lstData
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rgAsset 
    ''' Tao du lieu filter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgAsset_NeedDataSource(sender As Object, e As System.EventArgs) Handles rgAsset.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Using rep As New ProfileBusinessRepository
                Dim lstData As List(Of AssetMngDTO)
                If hidEmpID.Value <> "" Then
                    lstData = rep.GetAssetByTerminate(hidEmpID.Value)
                Else
                    lstData = New List(Of AssetMngDTO)
                End If
                rgAsset.DataSource = lstData
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:10
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện validation ngừng sử dụng cho combobox cboWELFARE_ID
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cusValStatus_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusValStatus.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New OtherListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboStatus.SelectedValue
                validate.ACTFLG = "A"
                validate.CODE = ProfileCommon.TER_STATUS.Code
                args.IsValid = rep.ValidateOtherList(validate)
            End If
            If Not args.IsValid Then
                GetDataCombo()
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc load và tính toán lương cơ bản và ngày phép cho nhân viên
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetTerminate3BCalculate()
        Dim dt As DataTable
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If hidEmpID.Value Is Nothing Or rdEffectDate3B.SelectedDate Is Nothing Then
                rntxtRemainingLeave.ClearValue()
                rntxtCompensatoryLeave.ClearValue()
                hiSalbasic.ClearValue()
                Exit Sub
            End If
            Using rep As New ProfileBusinessRepository
                dt = rep.CalculateTerminate(hidEmpID.Value, rdEffectDate3B.SelectedDate)
            End Using
            If dt IsNot Nothing Then
                rntxtRemainingLeave.Value = Utilities.ObjToDecima(dt.Rows(0)("LEAVE"))
                rntxtCompensatoryLeave.Value = Utilities.ObjToDecima(dt.Rows(0)("COMP_LEAVE"))
                hiSalbasic.Value = Utilities.ObjToDecima(dt.Rows(0)("SAL_BASIC"))
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc đăng ký sự kiện click cho ctrlFindEmployeePopup hoặc ctrlFindSigner
    ''' isLoadPopup = 1 gán cho button ctrlFindEmployeePopup
    ''' isLoadPopup = 1 gán cho button ctrlFindSigner
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
                    ctrlFindEmployeePopup.IsOnlyWorkingWithoutTer = True
                    ctrlFindEmployeePopup.IS_3B = 1
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
                Case 2
                    ctrlFindSigner = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                    ctrlFindSigner.CurrentValue = SelectOrg
                    ctrlFindSigner.MustHaveContract = True
                    ctrlFindSigner.LoadAllOrganization = True
                    phFindSign.Controls.Add(ctrlFindSigner)
                    ctrlFindSigner.MultiSelect = False
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ListComboData = New ComboBoxDataDTO
            ListComboData.GET_TER_STATUS = True
            rep.GetComboList(ListComboData)
            rep.Dispose()
            FillDropDownList(cboStatus, ListComboData.LIST_TER_STATUS, "NAME_VN", "ID", Common.Common.SystemLanguage, True)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:50
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc lấy về parameter "FormType" và "ID"
    ''' Làm mới View hiện thời
    ''' Fill du lieu cho View nếu parameter "empid" có dữ liệu và parameter "FormType" = 3
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
                        If rdJoinDateState.SelectedDate IsNot Nothing And rdEffectDate3B.SelectedDate IsNot Nothing Then
                            If rdJoinDateState.SelectedDate < rdEffectDate3B.SelectedDate.Value.AddDays(-1) Then
                                txtSeniority.Text = CalculateSeniority(rdJoinDateState.SelectedDate, rdEffectDate3B.SelectedDate.Value.AddDays(-1))
                            Else
                                txtSeniority.Text = vbNullString
                            End If
                        End If
                        hidEmpID.Value = empID
                        rgLabourProtect.Rebind()
                        GetTerminate3BCalculate()
                        Refresh("InsertView")
                End Select
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class
