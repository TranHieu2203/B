Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports ICSharpCode.SharpZipLib.Checksums
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlPortalDisciplineNewEditMng
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Portal" + Me.GetType().Name.ToString()

#Region "Property"
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Object Disciptline
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Discipline As DisciplineDTO
        Get
            Return ViewState(Me.ID & "_Discipline")
        End Get
        Set(ByVal value As DisciplineDTO)
            ViewState(Me.ID & "_Discipline") = value
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
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' List combo data
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
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Danh sach nhan vien bi ky luat
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Employee_Discipline As List(Of DisciplineEmpDTO)
        Get
            Return ViewState(Me.ID & "_Employee_Discipline")
        End Get
        Set(ByVal value As List(Of DisciplineEmpDTO))
            ViewState(Me.ID & "_Employee_Discipline") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' '0 - normal
    '1 - Employee
    '2 - Org
    '3 - Sign
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
    Property Down_File As String
        Get
            Return ViewState(Me.ID & "_Down_File")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_File") = value
        End Set
    End Property
    Dim FormType As Integer
    Dim IDSelect As Decimal?
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' 0 - Declare
    ''' 1 - Extent
    ''' 2 - Details
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DesciplineID As Decimal
        Get
            Return ViewState(Me.ID & "_DesciplineID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_DesciplineID") = value
        End Set
    End Property

    Property State_Id As Decimal
        Get
            Return ViewState(Me.ID & "_State_Id")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_State_Id") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc ViewLoad
    ''' Lay ve cac parameter, lam moi lai trang, Cap nhat trang thai cac control tren page
    ''' 0 - Declare
    ''' 1 - Extent
    ''' 2 - Details
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetParams()
            Refresh()
            UpdateControlState()
            rgEmployee.AllowSorting = False
            'rntxtMoney.Enabled = False
            If CType(CommonConfig.dicConfig("APP_SETTING_12"), Boolean) Then
                lbSignerName.Visible = False
                txtSignerName.Display = False
                btnFindSinger.Visible = False
                lbSignerTitle.Visible = False
                txtSignerTitle.Visible = False
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
    ''' Khoi tao cac control tren page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarDiscipline
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Seperator,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            If Request.Params("FormType") IsNot Nothing Then
                FormType = Request.Params("FormType")
                Select Case FormType
                    Case 0
                        Me.ViewDescription = Translate("Tạo mới kỷ luật")
                    Case 1
                        Me.ViewDescription = Translate("Sửa kỷ luật")
                End Select
            End If
            If Not IsPostBack Then
                'ViewConfig(RadPane2)
                'GirdConfig(rgEmployee)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc bind du lieu nguoi dung
    ''' Bind dua lieu cho data combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
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
    ''' Phuong thuc khoi tao cac control tren trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

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
    ''' Phuong thuc lam moi cacs trang thai theo update, insert cua view
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim objOther As OtherListDTO
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try

            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Discipline = rep.GetDisciplineByID(New DisciplineDTO With {.ID = DesciplineID})
                    txtDecisionNo.Text = Discipline.NO
                    txtDecisionName.Text = Discipline.NAME
                    txtRemark.Text = Discipline.REMARK
                    txtSignerName.Text = Discipline.SIGNER_NAME
                    txtSignerTitle.Text = Discipline.SIGNER_TITLE
                    chkDeductFromSalary.Checked = Discipline.DEDUCT_FROM_SALARY

                    If Discipline.DEDUCT_FROM_SALARY = True Then
                        nmYear.Enabled = True
                        cboPeriod.Enabled = True
                        nmYear.Value = Discipline.YEAR_PERIOD
                        nmYear_TextChanged(Nothing, Nothing)

                        If Discipline.PERIOD_ID Is Nothing Then
                            cboPeriod.ClearValue()
                        Else
                            cboPeriod.SelectedValue = Discipline.PERIOD_ID
                        End If

                        rnAmountSalaryMonth.Enabled = True
                        If Discipline.PERIOD_ID Is Nothing Then
                            rnAmountSalaryMonth.Value = Nothing
                        Else
                            rnAmountSalaryMonth.Value = Discipline.AMOUNT_SAL_MONTH
                        End If

                        rnAmountInMonth.Enabled = True
                        If Discipline.AMOUNT_IN_MONTH Is Nothing Then
                            rnAmountInMonth.Value = Nothing
                        Else
                            rnAmountInMonth.Value = Discipline.AMOUNT_IN_MONTH
                        End If

                        rnAmountDeductedMonth.Enabled = True
                        If Discipline.AMOUNT_DEDUCT_AMOUNT Is Nothing Then
                            rnAmountDeductedMonth.Value = Nothing
                        Else
                            rnAmountDeductedMonth.Value = Discipline.AMOUNT_DEDUCT_AMOUNT
                        End If

                        chkAmountInMonth.Enabled = True
                        chkAmountInMonth.Checked = Discipline.IS_AMOUNT_IN_MONTH

                        'If chkAmountInMonth.Checked Then
                        '    rnAmountInMonth.Enabled = False
                        '    rnAmountDeductedMonth.Enabled = True
                        'Else
                        '    rnAmountInMonth.Enabled = True
                        '    rnAmountDeductedMonth.Enabled = False
                        'End If

                        If chkDeductFromSalary.Checked Then
                            If chkAmountInMonth.Checked Then
                                rnAmountInMonth.Enabled = True

                                rnAmountDeductedMonth.Text = ""
                                rnAmountDeductedMonth.Enabled = False
                            Else
                                rnAmountInMonth.Text = ""
                                rnAmountInMonth.Enabled = False

                                rnAmountDeductedMonth.Enabled = True

                            End If
                        End If


                    Else
                        nmYear.Enabled = False
                        cboPeriod.Enabled = False
                        nmYear.ClearValue()
                        cboPeriod.ClearValue()

                        rnAmountSalaryMonth.Enabled = False
                        rnAmountSalaryMonth.Value = Nothing

                        rnAmountInMonth.Enabled = False
                        rnAmountInMonth.Value = Nothing

                        rnAmountDeductedMonth.Enabled = False
                        rnAmountDeductedMonth.Value = Nothing

                        chkAmountInMonth.Enabled = False
                        chkAmountInMonth.Checked = False

                        rnAmountInMonth.Enabled = False
                        rnAmountInMonth.Value = Nothing

                        rnAmountDeductedMonth.Enabled = False
                        rnAmountDeductedMonth.Value = Nothing

                    End If
                    rntxtIndemnifyMoney.Value = Discipline.INDEMNIFY_MONEY
                    rdEffectDate.SelectedDate = Discipline.EFFECT_DATE
                    rdExpireDate.SelectedDate = Discipline.EXPIRE_DATE
                    'rdIssueDate.SelectedDate = Discipline.ISSUE_DATE
                    rdSignDate.SelectedDate = Discipline.SIGN_DATE
                    txtPerformDiscipline.Text = Discipline.PERFORM_TIME

                    cboStatus.SelectedValue = Discipline.STATUS_ID.ToString
                    If Discipline.STATUS_ID = 447 Then
                        CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    End If

                    cboDisciplineLevel.SelectedValue = Discipline.DISCIPLINE_LEVEL.ToString
                    cboDisciplineObj.SelectedValue = Discipline.DISCIPLINE_OBJ.ToString
                    cboDisciplineType.SelectedValue = Discipline.DISCIPLINE_TYPE.ToString
                    cboDisciplineReason.SelectedValue = Discipline.DISCIPLINE_REASON.ToString

                    If Discipline.LEVEL_ID IsNot Nothing Then
                        cbolevel.SelectedValue = Discipline.LEVEL_ID.ToString
                        cbolevel.Text = Discipline.LEVEL_NAME
                    End If
                    If Discipline.YEAR IsNot Nothing Then
                        cboYear.SelectedValue = Discipline.YEAR.ToString
                    End If


                    rdDelDisciplineDate.SelectedDate = Discipline.DEL_DISCIPLINE_DATE
                    txtNoteDiscipline.Text = Discipline.NOTE_DISCIPLINE
                    txtDisciplineReasonDetail.Text = Discipline.DISCIPLINE_REASON_DETAIL
                    rdViolationDate.SelectedDate = Discipline.VIOLATION_DATE
                    rdDetectViolationDate.SelectedDate = Discipline.DECTECT_VIOLATION_DATE
                    txtExplain.Text = Discipline.EXPLAIN
                    txtRemarkDiscipline.Text = Discipline.RERARK_DISCIPLINE
                    rnPaidIMoeny.Value = Discipline.PAIDMONEY
                    rdAmountPaidCash.Value = Discipline.AMOUNT_PAID_CASH
                    rnAmountToPaid.Value = Discipline.AMOUNT_TO_PAID
                    txtDecisionNo_Discipline.Text = Discipline.NO_DISCIPLINE

                    State_Id = Discipline.STATUS_ID

                    rntxtMoney.Value = Discipline.MONEY
                    hidID.Value = Discipline.ID.ToString
                    txtUploadFile.Text = Discipline.FILENAME
                    txtRemindLink.Text = If(Discipline.UPLOADFILE Is Nothing, "", Discipline.UPLOADFILE)
                    loadDatasource(txtUploadFile.Text)
                    FileOldName = If(FileOldName = "", txtUpload.Text, FileOldName)
                    GetEmployeeDiscipline()
                    rgEmployee.Rebind()
                    For Each i As GridItem In rgEmployee.Items
                        i.Edit = True
                    Next
                    rgEmployee.Rebind()
                    If Discipline.STATUS_ID = ProfileCommon.DISCIPLINE_STATUS.APPROVE_ID Then

                        EnableControlAll_Cus(False, RadPane2)
                        EnableControlAll(True, chkPhatTien, rntxtMoney, rntxtIndemnifyMoney, rnPaidIMoeny, rdAmountPaidCash, rnAmountToPaid)
                        EnableControlAll(True, chkDeductFromSalary, rnAmountSalaryMonth, nmYear, cboPeriod, chkAmountInMonth, rnAmountInMonth, rnAmountDeductedMonth)

                        If cboDisciplineObj.SelectedValue <> 401 Or cboDisciplineObj.Text <> "Kỷ luật cá nhân" Then
                            EnableControlAll(False, chkDeductFromSalary, rnAmountSalaryMonth, nmYear, cboPeriod, chkAmountInMonth, rnAmountInMonth, rnAmountDeductedMonth)
                        End If

                        btnDownload.Enabled = True
                        btnUploadFile.Enabled = True

                        Dim btnEmployee As RadButton = rgEmployee.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnEmployee")
                        btnEmployee.Enabled = False
                        Dim btnQD As RadButton = rgEmployee.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnQD")
                        btnQD.Enabled = True
                        Dim btnHSL As RadButton = rgEmployee.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnHSL")
                        btnHSL.Enabled = True
                        Dim btnDeleteEmp As RadButton = rgEmployee.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnDeleteEmp")
                        btnDeleteEmp.Enabled = False

                        rgEmployee.Items(0).Enabled = False
                        'For Each item As GridDataItem In rgEmployee.Items

                        '    Dim cbStatus = CType(item("cbStatus").Controls(0), CheckBox)
                        '    cbStatus.Enabled = False

                        '    Dim txtINDEMNIFY_MONEY = CType(item("INDEMNIFY_MONEY").Controls(0), RadNumericTextBox)
                        '    txtINDEMNIFY_MONEY.Enabled = False

                        '    Dim chkNO_PROCESS = CType(item("NO_PROCESS").Controls(0), CheckBox)
                        '    chkNO_PROCESS.Enabled = False

                        'Next

                        CurrentState = CommonMessage.STATE_EDIT

                    Else
                        'For Each item As GridDataItem In rgEmployee.Items
                        '    Dim cbStatus = CType(item("cbStatus").Controls(0), CheckBox)
                        '    cbStatus.Enabled = True

                        '    Dim txtINDEMNIFY_MONEY = CType(item("INDEMNIFY_MONEY").Controls(0), RadNumericTextBox)
                        '    txtINDEMNIFY_MONEY.Enabled = True

                        '    Dim chkNO_PROCESS = CType(item("NO_PROCESS").Controls(0), CheckBox)
                        '    chkNO_PROCESS.Enabled = True
                        'Next

                        Dim btnEmployee As RadButton = rgEmployee.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnEmployee")
                        btnEmployee.Enabled = True

                        Dim btnQD As RadButton = rgEmployee.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnQD")
                        btnQD.Enabled = False

                        Dim btnHSL As RadButton = rgEmployee.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnHSL")
                        btnHSL.Enabled = False

                        Dim btnDeleteEmp As RadButton = rgEmployee.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnDeleteEmp")
                        btnDeleteEmp.Enabled = True

                    End If
                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
                    rntxtMoney.Enabled = False
                    Employee_Discipline = New List(Of DisciplineEmpDTO)
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
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Commandd khi click control OnMainToolbar
    ''' Cac trang thai command: luu, huy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim objDiscipline As New DisciplineDTO
        Dim lstDisciplineEmp As New List(Of DisciplineEmpDTO)
        'Dim objOther As OtherListDTO
        Dim gID As Decimal
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        'HongDX thêm checkbox chọn xử phạt tiền, bỏ trường hợp check ID = 462
                        'Utilities.ObjToDecima(cboDisciplineType.SelectedValue) <> 462 ' nếu kỷ luật xa thải ko bắt nhập số tiền
                        If chkPhatTien.Checked = True Then
                            If Not Utilities.ObjToDecima(rntxtMoney.Value) > 0 Then
                                ShowMessage(Translate("Số tiền phải lớn hơn 0"), NotifyType.Warning)

                                Exit Sub
                            End If
                        End If

                        'If cboStatus.SelectedValue = 447 Then
                        '    If txtUpload.Text = "" Then
                        '        ShowMessage(Translate("Bạn phải đính kèm tập tin khi phê duyệt"), NotifyType.Warning)
                        '        Exit Sub
                        '    End If
                        'End If

                        If rgEmployee.Items.Count = 0 Then
                            ShowMessage(Translate("Bạn chưa chọn nhân viên"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If rnAmountDeductedMonth.Value > rnAmountToPaid.Value Then
                            ShowMessage(Translate("Bạn phải nhập Số tiền trừ mỗi tháng nhỏ hơn Số tiền còn phải nộp"), NotifyType.Warning)
                            Exit Sub
                        End If
                        Try
                            Dim totalSumIndemMoney As Decimal = 0
                            Dim totalAmountToPaid As Decimal = 0
                            'For Each item As GridDataItem In rgEmployee.Items
                            '    Employee_Discipline(item.ItemIndex).INDEMNIFY_MONEY = CType(item("INDEMNIFY_MONEY").Controls(0), RadNumericTextBox).Value
                            '    If Employee_Discipline(item.ItemIndex).INDEMNIFY_MONEY IsNot Nothing Then
                            '        totalSumIndemMoney += Employee_Discipline(item.ItemIndex).INDEMNIFY_MONEY
                            '    End If
                            'Next
                            If rnAmountToPaid.Value IsNot Nothing Then
                                totalAmountToPaid += rnAmountToPaid.Value
                            End If

                            'so sanh chenh lech tien
                            'If (System.Math.Abs(totalSumIndemMoney - totalAmountToPaid) > 50) Then
                            '    ShowMessage(Translate("Tổng số tiền bồi thường đang bị chênh lệch"), NotifyType.Warning)
                            '    Exit Sub
                            'End If

                        Catch ex As Exception
                            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
                            Throw ex
                        End Try

                        Try
                            objDiscipline.DISCIPLINE_LEVEL = Decimal.Parse(cboDisciplineLevel.SelectedValue)
                        Catch ex As Exception
                            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
                            Throw ex
                        End Try
                        Try
                            objDiscipline.DISCIPLINE_OBJ = Decimal.Parse(cboDisciplineObj.SelectedValue)
                        Catch ex As Exception
                            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
                            Throw ex
                        End Try
                        objDiscipline.LEVEL_ID = CDec(Val(cbolevel.SelectedValue))
                        objDiscipline.YEAR = CDec(Val(cboYear.SelectedValue))
                        Try
                            objDiscipline.DISCIPLINE_TYPE = Decimal.Parse(cboDisciplineType.SelectedValue)
                        Catch ex As Exception
                            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
                            Throw ex
                        End Try
                        Try
                            objDiscipline.DISCIPLINE_REASON = Decimal.Parse(cboDisciplineReason.SelectedValue)
                        Catch ex As Exception
                            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
                            Throw ex
                        End Try

                        objDiscipline.DISCIPLINE_EMP = Employee_Discipline
                        objDiscipline.MONEY = rntxtMoney.Value
                        Try
                            objDiscipline.STATUS_ID = Decimal.Parse(cboStatus.SelectedValue)
                        Catch ex As Exception
                            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
                            Throw ex
                        End Try
                        objDiscipline.DEDUCT_FROM_SALARY = chkDeductFromSalary.Checked
                        If chkDeductFromSalary.Checked Then
                            objDiscipline.IS_AMOUNT_IN_MONTH = chkAmountInMonth.Checked
                            objDiscipline.PERIOD_ID = cboPeriod.SelectedValue
                            objDiscipline.AMOUNT_SAL_MONTH = rnAmountSalaryMonth.Value
                            objDiscipline.AMOUNT_IN_MONTH = rnAmountInMonth.Value
                            objDiscipline.AMOUNT_DEDUCT_AMOUNT = rnAmountDeductedMonth.Value
                        End If
                        If (rdEffectDate.SelectedDate.HasValue And rdExpireDate.SelectedDate.HasValue) Then
                            If (CLng(rdExpireDate.SelectedDate.Value.Subtract(rdEffectDate.SelectedDate.Value).TotalSeconds()) < 0) Then
                                ShowMessage(Translate("Ngày hết hiệu lực phải sau ngày có hiệu lực"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If

                        If (rdViolationDate.SelectedDate.HasValue And rdDetectViolationDate.SelectedDate.HasValue) Then
                            If (CLng(rdDetectViolationDate.SelectedDate.Value.Subtract(rdViolationDate.SelectedDate.Value).TotalSeconds()) < 0) Then
                                ShowMessage(Translate("Ngày phát hiện vi phạm phải sau ngày vi phạm"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                        'If (rdIssueDate.SelectedDate.HasValue And rdSignDate.SelectedDate.HasValue) Then
                        '    If (CLng(rdIssueDate.SelectedDate.Value.Subtract(rdSignDate.SelectedDate.Value).TotalSeconds()) < 0) Then
                        '        ShowMessage(Translate("Ngày ban hành phải sau ngày ký"), NotifyType.Error)
                        '        Exit Sub
                        '    End If
                        'End If
                        'If (rdSignDate.SelectedDate.HasValue And cboStatus.SelectedValue = ProfileCommon.DISCIPLINE_STATUS.APPROVE_ID) Then
                        '    If (CLng(rdSignDate.SelectedDate.Value.Subtract(DateTime.Now).TotalSeconds()) > 0) Then
                        '        ShowMessage(Translate("Chưa ký không được duyệt bản ghi"), NotifyType.Error)
                        '        Exit Sub
                        '    End If
                        'End If
                        If (rdSignDate.SelectedDate.HasValue And rdEffectDate.SelectedDate.HasValue) Then
                            If (CLng(rdSignDate.SelectedDate.Value.Subtract(rdEffectDate.SelectedDate.Value).TotalSeconds()) > 0) Then
                                ShowMessage(Translate("Ngày có hiệu lực phải sau ngày ký"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                        If (rdSignDate.SelectedDate.HasValue And rdExpireDate.SelectedDate.HasValue) Then
                            If (CLng(rdSignDate.SelectedDate.Value.Subtract(rdExpireDate.SelectedDate.Value).TotalSeconds()) > 0) Then
                                ShowMessage(Translate("Ngày ký phải trước ngày hết hiệu lực"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                        'If (rdIssueDate.SelectedDate.HasValue And rdExpireDate.SelectedDate.HasValue) Then
                        '    If (CLng(rdIssueDate.SelectedDate.Value.Subtract(rdExpireDate.SelectedDate.Value).TotalSeconds()) > 0) Then
                        '        ShowMessage(Translate("Ngày ban hành phải trước ngày hết hiệu lực"), NotifyType.Error)
                        '        Exit Sub
                        '    End If
                        'End If
                        objDiscipline.FILENAME = txtUpload.Text.Trim
                        objDiscipline.UPLOADFILE = If(Down_File Is Nothing, "", Down_File)
                        If objDiscipline.UPLOADFILE = "" Then
                            objDiscipline.UPLOADFILE = If(txtRemindLink.Text Is Nothing, "", txtRemindLink.Text)
                        Else
                            objDiscipline.UPLOADFILE = If(objDiscipline.UPLOADFILE Is Nothing, "", objDiscipline.UPLOADFILE)
                        End If
                        objDiscipline.INDEMNIFY_MONEY = rntxtIndemnifyMoney.Value
                        objDiscipline.REMARK = txtRemark.Text.Trim
                        objDiscipline.EFFECT_DATE = rdEffectDate.SelectedDate
                        objDiscipline.EXPIRE_DATE = rdExpireDate.SelectedDate
                        'objDiscipline.ISSUE_DATE = rdIssueDate.SelectedDate
                        objDiscipline.SIGNER_NAME = txtSignerName.Text.Trim
                        objDiscipline.SIGN_DATE = rdSignDate.SelectedDate
                        objDiscipline.NAME = txtDecisionName.Text.Trim
                        objDiscipline.NO = txtDecisionNo.Text.Trim
                        objDiscipline.PERFORM_TIME = txtPerformDiscipline.Text.Trim

                        objDiscipline.DEL_DISCIPLINE_DATE = rdDelDisciplineDate.SelectedDate
                        objDiscipline.NOTE_DISCIPLINE = txtNoteDiscipline.Text
                        objDiscipline.DISCIPLINE_REASON_DETAIL = txtDisciplineReasonDetail.Text
                        objDiscipline.VIOLATION_DATE = rdViolationDate.SelectedDate
                        objDiscipline.DECTECT_VIOLATION_DATE = rdDetectViolationDate.SelectedDate
                        objDiscipline.EXPLAIN = txtExplain.Text
                        objDiscipline.RERARK_DISCIPLINE = txtRemarkDiscipline.Text
                        objDiscipline.PAIDMONEY = rnPaidIMoeny.Value
                        objDiscipline.AMOUNT_PAID_CASH = rdAmountPaidCash.Value
                        objDiscipline.AMOUNT_TO_PAID = rnAmountToPaid.Value
                        objDiscipline.NO_DISCIPLINE = txtDecisionNo_Discipline.Text

                        objDiscipline.SIGNER_TITLE = txtSignerTitle.Text
                        For Each i As GridDataItem In rgEmployee.Items
                            Dim o As New DisciplineEmpDTO
                            o.HU_EMPLOYEE_ID = i.GetDataKeyValue("HU_EMPLOYEE_ID")
                            'o.MONEY = CType(i("MONEY").Controls(0), RadNumericTextBox).Value
                            'o.INDEMNIFY_MONEY = CType(i("INDEMNIFY_MONEY").Controls(0), RadNumericTextBox).Value
                            'Dim money As String = CType(i("MONEY").Controls(0), TextBox).Text.Trim
                            'o.MONEY = If(money <> "", Decimal.Parse(money), Nothing)
                            ' Dim iden_money As String = CType(i("INDEMNIFY_MONEY").Controls(0), RadNumericTextBox).Value
                            'o.INDEMNIFY_MONEY = If(iden_money <> "", Decimal.Parse(iden_money), Nothing)


                            'Dim is_NoPro As Boolean = CType(i("NO_PROCESS").Controls(0), CheckBox).Checked
                            'o.NO_PROCESS = If(is_NoPro = False, 0, 1)

                            lstDisciplineEmp.Add(o)
                        Next
                        objDiscipline.DISCIPLINE_EMP = lstDisciplineEmp
                        objDiscipline.IS_PORTAL = 1
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertDiscipline(objDiscipline, gID) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlPortalDisciplineNewEditMng&FormType=0&noscroll=1")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objDiscipline.ID = Decimal.Parse(DesciplineID)
                                If rep.ModifyDiscipline(objDiscipline, gID) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                        'Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Discipline&group=Business")

                        If objDiscipline.DISCIPLINE_TYPE = ProfileCommon.DISCIPLINE_TYPE.LAYOFF_ID And
                            cboDisciplineObj.SelectedValue = 401 Or cboDisciplineObj.Text = "Kỷ luật cá nhân" And
                            rgEmployee.Items.Count > 0 And
                            cboStatus.SelectedValue = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                            'ctrlMessageBox.MessageText = Translate("Đi tới nghiệp vụ nghỉ việc cho nhân viên?")
                            'ctrlMessageBox.ActionName = "CALL_TERMINATE"
                            'ctrlMessageBox.DataBind()
                            'ctrlMessageBox.Show()
                        Else
                            'Dim str As String = "getRadWindow().close('1');"
                            'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                            ''POPUPTOLINK
                            Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlPortalDisciplineMng")
                        End If
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    txtRemindLink.Text = ""
                    FileOldName = ""
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlPortalDisciplineMng")
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
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

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/DisciplineInfo/")
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
                            strPath_Down = Server.MapPath("~/ReportTemplates/Profile/DisciplineInfo/" + txtRemindLink.Text)
                            'bCheck = True
                            ZipFiles(strPath_Down)
                        End If
                    End If
                Else
                    If Down_File <> "" Then
                        strPath_Down = Server.MapPath("~/ReportTemplates/Profile/DisciplineInfo/" + Down_File)
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
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command button khi click vao control ctrlMessageBox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = "CALL_TERMINATE" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim empID = CType(rgEmployee.Items(0), GridDataItem).GetDataKeyValue("HU_EMPLOYEE_ID").ToString
                Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlPortalTerminateNewEdit&FormType=3&empid=" & empID)
            Else
                'Dim str As String = "getRadWindow().close('1');"
                'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                ''POPUPTOLINK
                Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlPortalDisciplineMng")
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
    ''' Xu ly su kien click khi click vao btnFindSigner
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFindSinger_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindSinger.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 3
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
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Selected cua control ctrlFindEmployeePopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count > 0 Then
                If Employee_Discipline Is Nothing Then
                    Employee_Discipline = New List(Of DisciplineEmpDTO)
                End If
                If cboDisciplineObj.SelectedValue = 401 Or cboDisciplineObj.Text = "Kỷ luật cá nhân" Then ' Ca nhan
                    Employee_Discipline.Clear()
                    chkDeductFromSalary.Enabled = True
                End If

                For Each emp As CommonBusiness.EmployeePopupFindDTO In lstCommonEmployee
                    Dim employee As New DisciplineEmpDTO
                    employee.EMPLOYEE_CODE = emp.EMPLOYEE_CODE
                    employee.HU_EMPLOYEE_ID = emp.ID
                    employee.FULLNAME = emp.FULLNAME_VN
                    employee.ORG_NAME = emp.ORG_NAME
                    employee.TITLE_NAME = emp.TITLE_NAME

                    Dim checkEmployeeCode As DisciplineEmpDTO = Employee_Discipline.Find(Function(p) p.EMPLOYEE_CODE = emp.EMPLOYEE_CODE)
                    If (Not checkEmployeeCode Is Nothing) Then
                        Continue For
                    End If

                    If cboDisciplineObj.SelectedValue = 401 Or cboDisciplineObj.Text = "Kỷ luật cá nhân" Then ' Ca nhan
                        'Employee_Discipline.Clear()
                        hidEmpCode.Value = emp.EMPLOYEE_CODE
                        hidOrg_Name_2.Value = emp.ORG_NAME_2
                        If IsDate(rdEffectDate.SelectedDate) Then
                            txtDecisionNo.Text = hidEmpCode.Value + "/" + rdEffectDate.SelectedDate.Value.Month.ToString + "/" + rdEffectDate.SelectedDate.Value.Year.ToString + "/QĐKL-" + hidOrg_Name_2.Value
                        End If
                        Employee_Discipline.Add(employee)
                    Else
                        Employee_Discipline.Add(employee)
                    End If
                Next

                Dim intMoney As Decimal = 0
                Dim intAmountToPaid As Decimal = 0
                If (Employee_Discipline.Count > 0) Then
                    If rnAmountToPaid.Value IsNot Nothing Then
                        intAmountToPaid += rnAmountToPaid.Value
                    End If
                    intMoney = Utilities.ObjToDecima(intAmountToPaid / Employee_Discipline.Count)
                End If

                For index = 0 To Employee_Discipline.Count - 1
                    Employee_Discipline.Item(index).INDEMNIFY_MONEY = intMoney
                Next

                rgEmployee.Rebind()
                For Each i As GridItem In rgEmployee.Items
                    i.Edit = True
                Next
                rgEmployee.Rebind()
            End If

            If nmYear.Text.Length = 4 Then
                Using re As New ProfileRepository
                    FillDropDownList(cboPeriod, re.GetPeriodbyYear(nmYear.Value), "PERIOD_NAME", "ID", Common.Common.SystemLanguage, False, cboPeriod.SelectedValue)
                End Using
            Else
                cboPeriod.ClearValue()
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
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Selected cua control ctrlFindSigner
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindSigner_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindSigner.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                txtSignerName.Text = item.FULLNAME_VN
                txtSignerTitle.Text = item.TITLE_NAME
            End If
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
    ''' Xu ly su kien khi click Cancel cua ctrlFindPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked, ctrlFindOrgPopup.CancelClicked, ctrlFindSigner.CancelClicked
        isLoadPopup = 0
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Server Validate cua control cvalDisciplineObj
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cvalDisciplineObj_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalDisciplineObj.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileRepository
            Dim validate As New OtherListDTO
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboDisciplineObj.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateOtherList(validate)
            End If
            If Not args.IsValid Then
                Dim dtData = rep.GetOtherList(validate.ID)
                FillRadCombobox(cboDisciplineObj, dtData, "NAME", "ID")
            End If
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
    ''' Xu ly su kien Server Validate cua control cvalDisciplineType
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cvalDisciplineType_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalDisciplineType.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileRepository
            Dim validate As New OtherListDTO
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboDisciplineType.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateOtherList(validate)
            End If
            If Not args.IsValid Then
                Dim dtData = rep.GetOtherList(validate.ID)
                FillRadCombobox(cboDisciplineType, dtData, "NAME", "ID")
            End If
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
    ''' Xu ly su kien Server Validate cua control cvalDisciplineLevel
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cvalDisciplineLevel_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalDisciplineLevel.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileRepository
            Dim validate As New OtherListDTO
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboDisciplineLevel.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateOtherList(validate)
            End If
            If Not args.IsValid Then
                Dim dtData = rep.GetOtherList(validate.ID)
                FillRadCombobox(cboDisciplineLevel, dtData, "NAME", "ID")
            End If
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
    ''' Xu ly su kien Server Validate cua control cvalDisciplineLevel
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Protected Sub cvalDisciplineReason_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalDisciplineReason.ServerValidate
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        Dim rep As New ProfileRepository
    '        Dim validate As New OtherListDTO
    '        If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
    '            validate.ID = cboDisciplineReason.SelectedValue
    '            validate.ACTFLG = "A"
    '            args.IsValid = rep.ValidateOtherList(validate)
    '        End If
    '        If Not args.IsValid Then
    '            Dim dtData = rep.GetOtherList(validate.ID)
    '            FillRadCombobox(cboDisciplineReason, dtData, "NAME", "ID")
    '        End If
    '        rep.Dispose()
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien item command cua item rgEmployee
    ''' Xu ly tinh toan tien ky luat theo tong nhan vien: Cong tong phat so sanh voi tong phat trc do;
    ''' Chia deu so phat cho tat ca cac nhan vien
    ''' Tim, xoa nhan vien tren luoi ky luat
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmployee_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgEmployee.ItemCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case e.CommandName
                Case "CalcEmployee"
                    'Dim totalSumMoney As Decimal = 0
                    Dim totalSumIndemMoney As Decimal = 0

                    For Each item As GridDataItem In rgEmployee.Items
                        'Employee_Discipline(item.ItemIndex).MONEY = CType(item("MONEY").Controls(0), RadNumericTextBox).Value
                        'If Employee_Discipline(item.ItemIndex).MONEY IsNot Nothing Then
                        '    totalSumMoney += Employee_Discipline(item.ItemIndex).MONEY
                        'End If
                        Employee_Discipline(item.ItemIndex).INDEMNIFY_MONEY = CType(item("INDEMNIFY_MONEY").Controls(0), RadNumericTextBox).Value
                        If Employee_Discipline(item.ItemIndex).INDEMNIFY_MONEY IsNot Nothing Then
                            totalSumIndemMoney += Employee_Discipline(item.ItemIndex).INDEMNIFY_MONEY
                        End If
                    Next
                    If Employee_Discipline.Count > 0 Then
                        'Dim totalMoneyEmp As Decimal = 0
                        Dim totalMoneyIndemEmp As Decimal = 0
                        'If rntxtMoney.Value IsNot Nothing Then
                        '    Try
                        '        totalMoneyEmp += rntxtMoney.Value
                        '    Catch ex As Exception
                        '        ShowMessage("Tổng tiền vượt giới hạn cho phép", NotifyType.Error)
                        '        Exit Sub
                        '        Throw ex
                        '    End Try

                        'End If
                        If rntxtIndemnifyMoney.Value IsNot Nothing Then
                            Try
                                totalMoneyIndemEmp += rntxtIndemnifyMoney.Value
                            Catch ex As Exception
                                ShowMessage("Tổng tiền vượt giới hạn cho phép", NotifyType.Error)
                                Exit Sub
                            End Try

                        End If
                        'Dim colMoney = CType(rgEmployee.Columns.FindByUniqueName("MONEY"), GridNumericColumn)
                        Dim colMoneyIndem = CType(rgEmployee.Columns.FindByUniqueName("INDEMNIFY_MONEY"), GridNumericColumn)
                        'colMoney.FooterText = ""
                        colMoneyIndem.FooterText = ""
                        'If totalSumMoney <> totalMoneyEmp Then
                        '    colMoney.Aggregate = GridAggregateFunction.None
                        '    rgEmployee.Columns.FindByUniqueName("MONEY").FooterText = "Lệch: " & Format((totalSumMoney - totalMoneyEmp), "n0")
                        'Else
                        '    colMoney.Aggregate = GridAggregateFunction.Sum
                        'End If
                        If totalSumIndemMoney <> totalMoneyIndemEmp Then
                            colMoneyIndem.Aggregate = GridAggregateFunction.None
                            colMoneyIndem.FooterText = "Lệch: " & Format((totalSumIndemMoney - totalMoneyIndemEmp), "n0")
                        Else
                            colMoneyIndem.Aggregate = GridAggregateFunction.Sum
                        End If
                    End If
                    rgEmployee.Rebind()
                Case "ShareEmployee"
                    Dim countEmp = rgEmployee.Items.Count
                    If countEmp = 0 Then
                        Exit Sub
                    End If
                    Dim totalMoney = 0
                    Dim totalIndemnyfiMoney = 0
                    If rntxtMoney.Value IsNot Nothing Then
                        Try
                            totalMoney += rntxtMoney.Value
                        Catch ex As Exception
                            ShowMessage("Tổng tiền vượt giới hạn cho phép", NotifyType.Error)
                            Exit Sub
                        End Try

                    End If
                    If rntxtIndemnifyMoney.Value IsNot Nothing Then
                        Try
                            totalIndemnyfiMoney += rntxtIndemnifyMoney.Value
                        Catch ex As Exception
                            ShowMessage("Tổng tiền vượt giới hạn cho phép", NotifyType.Error)
                            Exit Sub
                        End Try

                    End If
                    Dim shareEmp = Decimal.Round(totalMoney / countEmp)
                    Dim shareEmpIndemnyfi = Decimal.Round(totalIndemnyfiMoney / countEmp)

                    Dim totalSumMoney As Decimal = 0
                    Dim totalSumIndemMoney As Decimal = 0

                    For Each item In Employee_Discipline
                        Try
                            totalSumMoney += shareEmp
                            totalSumIndemMoney += shareEmpIndemnyfi
                            item.MONEY = shareEmp
                            item.INDEMNIFY_MONEY = shareEmpIndemnyfi
                        Catch ex As Exception
                            ShowMessage("Tổng tiền vượt giới hạn cho phép", NotifyType.Error)
                            Exit Sub
                        End Try
                    Next

                    If Employee_Discipline.Count > 0 Then
                        Dim totalMoneyEmp As Decimal = 0
                        Dim totalMoneyIndemEmp As Decimal = 0
                        If rntxtMoney.Value IsNot Nothing Then
                            Try
                                totalMoneyEmp += rntxtMoney.Value
                            Catch ex As Exception
                                ShowMessage("Tổng tiền vượt giới hạn cho phép", NotifyType.Error)
                                Exit Sub
                            End Try

                        End If
                        If rntxtIndemnifyMoney.Value IsNot Nothing Then
                            Try
                                totalMoneyIndemEmp += rntxtIndemnifyMoney.Value
                            Catch ex As Exception
                                ShowMessage("Tổng tiền vượt giới hạn cho phép", NotifyType.Error)
                                Exit Sub
                            End Try

                        End If
                        Dim colMoney = CType(rgEmployee.Columns.FindByUniqueName("MONEY"), GridNumericColumn)
                        Dim colMoneyIndem = CType(rgEmployee.Columns.FindByUniqueName("INDEMNIFY_MONEY"), GridNumericColumn)
                        colMoney.FooterText = ""
                        colMoneyIndem.FooterText = ""
                        If totalSumMoney <> totalMoneyEmp Then
                            colMoney.Aggregate = GridAggregateFunction.None
                            rgEmployee.Columns.FindByUniqueName("MONEY").FooterText = "Lệch: " & Format((totalSumMoney - totalMoneyEmp), "n0")
                        Else
                            colMoney.Aggregate = GridAggregateFunction.Sum
                        End If
                        If totalSumIndemMoney <> totalMoneyIndemEmp Then
                            colMoneyIndem.Aggregate = GridAggregateFunction.None
                            colMoneyIndem.FooterText = "Lệch: " & Format((totalSumIndemMoney - totalMoneyIndemEmp), "n0")
                        Else
                            colMoneyIndem.Aggregate = GridAggregateFunction.Sum
                        End If
                    End If
                    rgEmployee.Rebind()
                Case "FindEmployee"
                    isLoadPopup = 1
                    UpdateControlState()
                    ctrlFindEmployeePopup.Show()
                Case "DeleteEmployee"
                    For Each i As GridDataItem In rgEmployee.SelectedItems
                        Dim s = (From q In Employee_Discipline Where
                                 q.HU_EMPLOYEE_ID = i.GetDataKeyValue("HU_EMPLOYEE_ID")).FirstOrDefault
                        Employee_Discipline.Remove(s)
                    Next

                    Dim intMoney As Decimal = 0
                    Dim intAmountToPaid As Decimal = 0
                    If (Employee_Discipline.Count > 0) Then
                        If rnAmountToPaid.Value IsNot Nothing Then
                            intAmountToPaid += rnAmountToPaid.Value
                        End If
                        intMoney = Utilities.ObjToDecima(intAmountToPaid / Employee_Discipline.Count)
                    End If

                    For index = 0 To Employee_Discipline.Count - 1
                        Employee_Discipline.Item(index).INDEMNIFY_MONEY = intMoney
                    Next

                    rgEmployee.Rebind()
                Case "CreateQD"
                    Dim EmpID As Decimal
                    If State_Id = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        If cboDisciplineObj.SelectedValue = 401 Or cboDisciplineObj.Text = "Kỷ luật cá nhân" Then
                            'Ca nhan
                            For Each dr As GridDataItem In rgEmployee.Items
                                EmpID = dr.GetDataKeyValue("HU_EMPLOYEE_ID")
                            Next
                            chkDeductFromSalary.Enabled = False
                        End If
                        ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "text", "openQDtab('" & EmpID & "')", True)
                    Else
                        ShowMessage("Quyết định Kỷ luật chưa được phê duyệt. Vui lòng kiểm tra lại !", NotifyType.Warning)
                        Exit Sub
                    End If
                Case "CreateHSL"
                    Dim EmpID As Decimal
                    If State_Id = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        If cboDisciplineObj.SelectedValue = 401 Or cboDisciplineObj.Text = "Kỷ luật cá nhân" Then
                            'Ca nhan
                            For Each dr As GridDataItem In rgEmployee.Items
                                EmpID = dr.GetDataKeyValue("HU_EMPLOYEE_ID")
                            Next
                            chkDeductFromSalary.Enabled = False
                        End If
                        ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "text", "openHSLtab('" & EmpID & "')", True)
                    Else
                        ShowMessage("Quyết định Kỷ luật chưa được phê duyệt. Vui lòng kiểm tra lại !", NotifyType.Warning)
                        Exit Sub
                    End If
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
    ''' Xu ly su kien ItemDataBound cua control rgEmployee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmployee_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployee.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.Edit Then
                Dim edit = CType(e.Item, GridEditableItem)
                Dim rntxt As TextBox

                'rntxt = CType(edit("MONEY").Controls(0), RadNumericTextBox)
                'rntxt.Width = Unit.Percentage(100)        
                Dim arntxt = CType(rgEmployee.Columns.FindByUniqueName("INDEMNIFY_MONEY"), GridNumericColumn)
                arntxt.DataFormatString = "{0:#,##0.##}"

                _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            End If
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rgEmployee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmployee_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEmployee.NeedDataSource
        Try
            rgEmployee.DataSource = Employee_Discipline
        Catch ex As Exception

        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Kiem tra trich ky luat tu luong
    ''' Fill du lieu cho combobox cboPeriod
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkDeductFromSalary_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDeductFromSalary.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkDeductFromSalary.Checked Then
                nmYear.Enabled = True
                nmYear.Value = Date.Now.Year
                cboPeriod.Enabled = True
                rnAmountSalaryMonth.Enabled = True
                rnAmountInMonth.Enabled = True
                rnAmountDeductedMonth.Enabled = True
                chkAmountInMonth.Enabled = True

                If chkAmountInMonth.Checked Then
                    rnAmountInMonth.Enabled = True
                    rnAmountDeductedMonth.Enabled = False
                Else
                    rnAmountInMonth.Enabled = False
                    rnAmountDeductedMonth.Enabled = True
                End If

                Using rep As New ProfileRepository
                    FillDropDownList(cboPeriod, rep.GetPeriodbyYear(nmYear.Value), "PERIOD_NAME", "ID", Common.Common.SystemLanguage, False, cboPeriod.SelectedValue)
                End Using
            Else
                'rdPayDate.Enabled = False
                nmYear.Enabled = False
                nmYear.ClearValue()
                cboPeriod.Enabled = False
                cboPeriod.ClearValue()

                rnAmountSalaryMonth.Enabled = False
                rnAmountSalaryMonth.Value = Nothing

                rnAmountInMonth.Enabled = False
                rnAmountInMonth.Value = Nothing

                rnAmountDeductedMonth.Enabled = False
                rnAmountDeductedMonth.Value = Nothing

                chkAmountInMonth.Enabled = False
                chkAmountInMonth.Checked = False

                rnAmountInMonth.Enabled = False
                rnAmountInMonth.Value = Nothing

                rnAmountDeductedMonth.Enabled = False
                rnAmountDeductedMonth.Value = Nothing
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Private Sub chkAmountInMonth_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAmountInMonth.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkAmountInMonth.Checked Then
                rnAmountInMonth.Enabled = True
                rnAmountInMonth.Value = 0

                rnAmountDeductedMonth.Text = ""
                rnAmountDeductedMonth.Enabled = False
            Else
                rnAmountInMonth.Text = ""
                rnAmountInMonth.Enabled = False

                rnAmountDeductedMonth.Enabled = True
                rnAmountDeductedMonth.Value = 0
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien server validate cho control cvalMoney
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Protected Sub cvalMoney_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalMoney.ServerValidate
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If rgEmployee.Items.Count = 0 Then
    '            args.IsValid = True
    '            Exit Sub
    '        End If
    '        If Utilities.ObjToDecima(cboDisciplineType.SelectedValue) <> 462 Then ' nếu kỷ luật xa thải ko bắt nhập số tiền
    '            If rntxtMoney.Value Is Nothing Then
    '                args.IsValid = True
    '                Exit Sub
    '            Else
    '                If rntxtMoney.Value <= 0 Then
    '                    args.IsValid = False
    '                    Exit Sub
    '                End If
    '            End If
    '            args.IsValid = True
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub


    'Private Sub cvalAmountToPaid_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalAmountToPaid.ServerValidate
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        Dim totalMoney = 0
    '        If rnAmountToPaid.Value IsNot Nothing Then
    '            totalMoney += rnAmountToPaid.Value
    '        End If

    '        Dim TotalMoneyEmp As Decimal = 0
    '        If rnAmountToPaid.Value IsNot Nothing Then
    '            TotalMoneyEmp += rnPaidIMoeny.Value
    '        End If

    '        If totalMoney > TotalMoneyEmp Then
    '            args.IsValid = False
    '        Else
    '            args.IsValid = True
    '        End If

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien server validate cho control cvalIndemnifyTotal
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cvalIndemnifyTotal_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalIndemnifyTotal.ServerValidate
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        Dim totalMoney = 0
    '        If rntxtIndemnifyMoney.Value IsNot Nothing Then
    '            totalMoney += rnAmountToPaid.Value
    '        End If



    '        Dim TotalMoneyEmp As Decimal = 0
    '        For Each i As GridDataItem In rgEmployee.Items
    '            TotalMoneyEmp += Utilities.ObjToDecima(CType(i("INDEMNIFY_MONEY").Controls(0), RadNumericTextBox).Value)
    '        Next
    '        args.IsValid = totalMoney = TotalMoneyEmp
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien TextChanged cho control nmYear
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub nmYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nmYear.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If nmYear.Text.Length = 4 Then
                Using rep As New ProfileRepository
                    FillDropDownList(cboPeriod, rep.GetPeriodbyYear(nmYear.Value), "PERIOD_NAME", "ID", Common.Common.SystemLanguage, False, cboPeriod.SelectedValue)
                End Using
            Else
                cboPeriod.ClearValue()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien server validate cho control cval_EffectDate_EpireDate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cval_EffectDate_ExpireDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cval_EffectDate_ExpireDate.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rdExpireDate.SelectedDate IsNot Nothing Then
                If rdExpireDate.SelectedDate < rdEffectDate.SelectedDate Then
                    args.IsValid = False
                Else
                    args.IsValid = True
                End If
            Else
                args.IsValid = True
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
    ''' Xu ly su kien server validate cho control cusDecisionNo doi voi trang thai them moi, sua
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cusDecisionNo_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusDecisionNo.ServerValidate

    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        Select Case CurrentState
    '            Case CommonMessage.STATE_NEW
    '                Using rep As New ProfileBusinessRepository
    '                    args.IsValid = rep.ValidateDiscipline("EXIST_DECISION_NO",
    '                                                        New DisciplineDTO With {
    '                                                            .NO = txtDecisionNo.Text.Trim})
    '                End Using
    '            Case CommonMessage.STATE_EDIT
    '                Using rep As New ProfileBusinessRepository
    '                    args.IsValid = rep.ValidateDiscipline("EXIST_DECISION_NO",
    '                                                        New DisciplineDTO With {
    '                                                            .ID = hidID.Value,
    '                                                            .NO = txtDecisionNo.Text.Trim})
    '                End Using
    '            Case Else
    '                args.IsValid = True
    '        End Select
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try

    'End Sub

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
                validate.CODE = "DECISION_STATUS"
                args.IsValid = rep.ValidateOtherList(validate)
            End If
            If Not args.IsValid Then
                GetDataCombo()
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub chkPhatTien_CheckedChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPhatTien.CheckedChanged
        If chkPhatTien.Checked Then
            rntxtMoney.Enabled = True
        Else
            rntxtMoney.Enabled = False
            rntxtMoney.Text = String.Empty
        End If
    End Sub

    Protected Sub RadAjaxPanel1_AjaxRequest(ByVal sender As Object, ByVal e As AjaxRequestEventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Dim intMoney As Decimal = 0
            Dim intIndemnifyMoney As Decimal = 0
            Dim intAmountPaidCash As Decimal = 0
            Dim totalPaidIMoeny As Decimal = 0
            Dim totalAmountToPaid As Decimal = 0

            If rntxtMoney.Value IsNot Nothing Then
                intMoney += rntxtMoney.Value
            End If
            If rntxtIndemnifyMoney.Value IsNot Nothing Then
                intIndemnifyMoney += rntxtIndemnifyMoney.Value
            End If

            If rdAmountPaidCash.Value IsNot Nothing Then
                intAmountPaidCash += rdAmountPaidCash.Value
            End If

            totalPaidIMoeny = Utilities.ObjToDecima(intMoney + intIndemnifyMoney)
            rnPaidIMoeny.Value = totalPaidIMoeny

            totalAmountToPaid = Utilities.ObjToDecima(totalPaidIMoeny - intAmountPaidCash)
            rnAmountToPaid.Value = totalAmountToPaid

            Dim intMoneyDetail As Decimal = 0
            Dim intAmountToPaid As Decimal = 0
            If (Employee_Discipline.Count > 0) Then
                If rnAmountToPaid.Value IsNot Nothing Then
                    intAmountToPaid += rnAmountToPaid.Value
                End If
                intMoneyDetail = Utilities.ObjToDecima(intAmountToPaid / Employee_Discipline.Count)
            End If

            For index = 0 To Employee_Discipline.Count - 1
                Employee_Discipline.Item(index).INDEMNIFY_MONEY = intMoneyDetail
            Next
            rgEmployee.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    'Private Sub rntxtMoney_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rntxtMoney.TextChanged, rntxtIndemnifyMoney.TextChanged
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow

    '        Dim intMoney As Decimal = 0
    '        Dim intIndemnifyMoney As Decimal = 0
    '        Dim intAmountPaidCash As Decimal = 0
    '        Dim totalPaidIMoeny As Decimal = 0
    '        Dim totalAmountToPaid As Decimal = 0

    '        If rntxtMoney.Value IsNot Nothing Then
    '            intMoney += rntxtMoney.Value
    '        End If
    '        If rntxtIndemnifyMoney.Value IsNot Nothing Then
    '            intIndemnifyMoney += rntxtIndemnifyMoney.Value
    '        End If

    '        If rdAmountPaidCash.Value IsNot Nothing Then
    '            intAmountPaidCash += rdAmountPaidCash.Value
    '        End If

    '        totalPaidIMoeny = Utilities.ObjToDecima(intMoney + intIndemnifyMoney)
    '        rnPaidIMoeny.Value = totalPaidIMoeny

    '        totalAmountToPaid = Utilities.ObjToDecima(totalPaidIMoeny - intAmountPaidCash)
    '        rnAmountToPaid.Value = totalAmountToPaid

    '        Dim intMoneyDetail As Decimal = 0
    '        Dim intAmountToPaid As Decimal = 0
    '        If (Employee_Discipline.Count > 0) Then
    '            If rnAmountToPaid.Value IsNot Nothing Then
    '                intAmountToPaid += rnAmountToPaid.Value
    '            End If
    '            intMoneyDetail = Utilities.ObjToDecima(intAmountToPaid / Employee_Discipline.Count)
    '        End If

    '        For index = 0 To Employee_Discipline.Count - 1
    '            Employee_Discipline.Item(index).INDEMNIFY_MONEY = intMoneyDetail
    '        Next
    '        rgEmployee.Rebind()

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    'Private Sub cvalAmountSalaryMonth_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalAmountSalaryMonth.ServerValidate
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow

    '        Dim totalMoney As Decimal = 0
    '        Dim totalAmountToPaid As Decimal = 0

    '        If rnAmountSalaryMonth.Value IsNot Nothing Then
    '            totalMoney += rnAmountSalaryMonth.Value
    '        End If

    '        If rnAmountToPaid.Value IsNot Nothing Then
    '            totalAmountToPaid += rnAmountToPaid.Value
    '        End If

    '        If (totalMoney > totalAmountToPaid) Then
    '            args.IsValid = False
    '        Else
    '            args.IsValid = True
    '        End If

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    Private Sub rdAmountPaidCash_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdAmountPaidCash.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Dim totalPaidIMoeny As Decimal = 0
            Dim totalrdAmountPaidCash As Decimal = 0

            Dim totalAmountToPaid As Decimal = 0

            If rnPaidIMoeny.Value IsNot Nothing Then
                totalPaidIMoeny += rnPaidIMoeny.Value
            End If

            If rdAmountPaidCash.Value IsNot Nothing Then
                totalrdAmountPaidCash += rdAmountPaidCash.Value
            End If

            totalAmountToPaid = Utilities.ObjToDecima(totalPaidIMoeny - totalrdAmountPaidCash)
            rnAmountToPaid.Value = totalAmountToPaid

            Dim intMoneyDetail As Decimal = 0
            Dim intAmountToPaid As Decimal = 0
            If (Employee_Discipline.Count > 0) Then
                If rnAmountToPaid.Value IsNot Nothing Then
                    intAmountToPaid += rnAmountToPaid.Value
                End If
                intMoneyDetail = Utilities.ObjToDecima(intAmountToPaid / Employee_Discipline.Count)
            End If

            For index = 0 To Employee_Discipline.Count - 1
                Employee_Discipline.Item(index).INDEMNIFY_MONEY = intMoneyDetail
            Next
            rgEmployee.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rnPaidIMoeny_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rnPaidIMoeny.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            rdAmountPaidCash.Value = 0
            rnAmountToPaid.Value = 0

            Dim intMoneyDetail As Decimal = 0
            Dim intAmountToPaid As Decimal = 0
            If (Employee_Discipline.Count > 0) Then
                If rnAmountToPaid.Value IsNot Nothing Then
                    intAmountToPaid += rnAmountToPaid.Value
                End If
                intMoneyDetail = Utilities.ObjToDecima(intAmountToPaid / Employee_Discipline.Count)
            End If

            For index = 0 To Employee_Discipline.Count - 1
                Employee_Discipline.Item(index).INDEMNIFY_MONEY = intMoneyDetail
            Next
            rgEmployee.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region
    Private Sub ZipFiles(ByVal path As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New Crc32()
            'Dim pathZip As String = AppDomain.CurrentDomain.BaseDirectory & "Zip\"
            'Dim fileNameZip As String = "ThongTinKyLuat.zip"
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
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub loadDatasource(ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If strUpload <> "" Then
                txtUploadFile.Text = strUpload
                FileOldName = txtUpload.Text
                txtUpload.Text = strUpload
            Else
                strUpload = String.Empty
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#Region "Custom"
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Lay thong tin nhung nhan vien bi ky luat
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetEmployeeDiscipline()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New ProfileBusinessRepository
                Employee_Discipline = rep.GetEmployeeDesciplineID(Utilities.ObjToDecima(DesciplineID))
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai cac control tren paged
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If
            If phFindOrg.Controls.Contains(ctrlFindOrgPopup) Then
                phFindOrg.Controls.Remove(ctrlFindOrgPopup)
                'Me.Views.Remove(ctrlFindOrgPopup.ID.ToUpper)
            End If
            If phFindSign.Controls.Contains(ctrlFindSigner) Then
                phFindSign.Controls.Remove(ctrlFindSigner)
                'Me.Views.Remove(ctrlFindSigner.ID.ToUpper)
            End If

            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = True
                    'ctrlFindEmployeePopup.IsOnlyWorkingWithoutTer = True
                    ctrlFindEmployeePopup.IsHideTerminate = False
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    If cboDisciplineObj.SelectedValue = 401 Or cboDisciplineObj.Text = "Kỷ luật cá nhân" Then ' Ca nhan
                        ctrlFindEmployeePopup.MultiSelect = True
                        chkDeductFromSalary.Enabled = False
                    ElseIf cboDisciplineObj.SelectedValue = 400 Then ' tap the
                        ctrlFindEmployeePopup.MultiSelect = True
                    End If
                Case 2
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                    phFindOrg.Controls.Add(ctrlFindOrgPopup)
                    ctrlFindOrgPopup.Show()
                Case 3
                    ctrlFindSigner = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
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
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Lay du lieu cho combobox cboStatus, cboDisciplineObj, cboDisciplineType, cboDisciplineLevel, cboDisciplineReason
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_DISCIPLINE_LEVEL = True
                ListComboData.GET_DISCIPLINE_STATUS = True
                ListComboData.GET_DISCIPLINE_OBJ = True
                ListComboData.GET_DISCIPLINE_TYPE = True
                ListComboData.GET_DISCIPLINE_REASON = True
                ListComboData.GET_LEVEL_DISCIPLINE = True
                rep.GetComboList(ListComboData)
                ListComboData.LIST_DISCIPLINE_TYPE = (From p In ListComboData.LIST_DISCIPLINE_TYPE Where p.ATTRIBUTE1 = 1).ToList
            End If

            'FillDropDownList(cboStatus, ListComboData.LIST_DISCIPLINE_STATUS, "NAME_VN", "ID", Common.Common.SystemLanguage, True)
            Dim dtData As New DataTable
            dtData = rep.GetOtherList(OtherTypes.DecisionStatus, True)
            FillRadCombobox(cboStatus, dtData, "NAME", "ID", True)
            FillDropDownList(cboDisciplineObj, ListComboData.LIST_DISCIPLINE_OBJ, "NAME_VN", "ID", Common.Common.SystemLanguage, False, cboDisciplineObj.SelectedValue)
            FillDropDownList(cboDisciplineType, ListComboData.LIST_DISCIPLINE_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, False, cboDisciplineType.SelectedValue)
            FillDropDownList(cboDisciplineLevel, ListComboData.LIST_DISCIPLINE_LEVEL, "NAME_VN", "ID", Common.Common.SystemLanguage, False, cboDisciplineLevel.SelectedValue)
            FillDropDownList(cboDisciplineReason, ListComboData.LIST_DISCIPLINE_REASON, "NAME_VN", "ID", Common.Common.SystemLanguage, False, cboDisciplineReason.SelectedValue)
            FillDropDownList(cbolevel, ListComboData.LIST_LEVEL_DISCIPLINE, "NAME_VN", "ID", Common.Common.SystemLanguage, False, cbolevel.SelectedValue)

            cboDisciplineObj.SelectedIndex = 0
            txtDecisionNo.ReadOnly = True
            cboDisciplineType.SelectedIndex = 0
            cboDisciplineLevel.SelectedIndex = 0
            cboDisciplineReason.SelectedIndex = 0
            cboStatus.SelectedIndex = 1
            cbolevel.SelectedIndex = 1

            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2018 To Date.Now.Year + 2
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year

            If cboDisciplineReason IsNot Nothing Then
                Dim comboBoxItem As New RadComboBoxItem()
                comboBoxItem.Text = ""
                comboBoxItem.Value = "0"
                cboDisciplineReason.Items.Add(comboBoxItem)
            End If
            rep.Dispose()
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
    ''' Phuong thuc lay params 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState Is Nothing Then
                If Request.Params("FormType") IsNot Nothing Then
                    FormType = Request.Params("FormType")
                End If
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = Decimal.Parse(Request.Params("ID"))
                    DesciplineID = IDSelect
                    hidID.Value = IDSelect
                End If
                If IDSelect IsNot Nothing Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Sub GetSigner()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim store As New ProfileStoreProcedure
        Try
            ClearControlValue(txtSignerName, txtSignerTitle)
            If IsDate(rdEffectDate.SelectedDate) AndAlso cboDisciplineObj.SelectedValue <> "" Then
                Dim signer = store.GET_SIGNER_BY_FUNC(Me.ViewName, rdEffectDate.SelectedDate, cboDisciplineObj.SelectedValue)
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

#End Region

    Private Sub cboDisciplineObj_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboDisciplineObj.SelectedIndexChanged
        GetSigner()
        If cboDisciplineObj.SelectedValue = 401 Or cboDisciplineObj.Text = "Kỷ luật cá nhân" Then

            txtDecisionNo.ReadOnly = True
            chkDeductFromSalary.Enabled = True
            EnableControlAll(True, rnAmountSalaryMonth, cboPeriod, rnAmountDeductedMonth, rnAmountInMonth)
            Employee_Discipline.Clear()
            rgEmployee.Rebind()
        Else
            txtDecisionNo.ReadOnly = False
            txtDecisionNo.Text = ""
            chkDeductFromSalary.Checked = False
            chkDeductFromSalary.Enabled = False

            chkAmountInMonth.Checked = False
            chkAmountInMonth.Enabled = False


            Employee_Discipline.Clear()
            rgEmployee.Rebind()

            EnableControlAll(False, rnAmountSalaryMonth, cboPeriod, rnAmountDeductedMonth, rnAmountInMonth, nmYear)
            ClearControlValue(rnAmountSalaryMonth, nmYear, cboPeriod, rnAmountInMonth, rnAmountDeductedMonth)
        End If
    End Sub

    Private Sub rdEffectDate_SelectedDateChanged(ByVal sender As Object, ByVal e As Calendar.SelectedDateChangedEventArgs) Handles rdEffectDate.SelectedDateChanged
        GetSigner()
        If IsDate(rdEffectDate.SelectedDate) And Employee_Discipline.Count > 0 Then
            txtDecisionNo.Text = hidEmpCode.Value + "/" + rdEffectDate.SelectedDate.Value.Month.ToString + "/" + rdEffectDate.SelectedDate.Value.Year.ToString + "/QĐKL-" + hidOrg_Name_2.Value

        End If
        If IsDate(rdEffectDate.SelectedDate) Then
            rdSignDate.SelectedDate = rdEffectDate.SelectedDate
        End If
    End Sub
End Class