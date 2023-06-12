Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlInsManagerHeathInsNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Insurance/Module/Insurance/Business/" + Me.GetType().Name.ToString()

#Region "Property"

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    Property isEdit As Boolean
        Get
            Return ViewState(Me.ID & "_isEdit")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isEdit") = value
        End Set
    End Property
    Property Employee_id As Integer
        Get
            Return ViewState(Me.ID & "_Employee_id")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_Employee_id") = value
        End Set
    End Property
    Property _Value As Decimal?
        Get
            Return ViewState(Me.ID & "_Value")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_Value") = value
        End Set
    End Property
    Property INSREMIGE As INS_REMIGE_MANAGER_DTO
        Get
            Return ViewState(Me.ID & "_INSREMIGES")
        End Get
        Set(ByVal value As INS_REMIGE_MANAGER_DTO)
            ViewState(Me.ID & "_INSREMIGES") = value
        End Set
    End Property
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    Property lstAllow As List(Of INS_CLAIME_INSURANCEDTO)
        Get
            Return ViewState(Me.ID & "_lstAllow")
        End Get
        Set(ByVal value As List(Of INS_CLAIME_INSURANCEDTO))
            ViewState(Me.ID & "_lstAllow") = value
        End Set
    End Property
    Property lst_detail As List(Of INS_LIST_CONTRACT_DETAILDTO)
        Get
            Return ViewState(Me.ID & "_lst_detail")
        End Get
        Set(ByVal value As List(Of INS_LIST_CONTRACT_DETAILDTO))
            ViewState(Me.ID & "_lst_detail") = value
        End Set
    End Property
    Property lst_Program As DataTable
        Get
            Return ViewState(Me.ID & "_lst_Program")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_lst_Program") = value
        End Set
    End Property
    Property lst_Family As DataTable
        Get
            Return ViewState(Me.ID & "_lst_Family")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_lst_Family") = value
        End Set
    End Property
#End Region

#Region "Page"


    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetYEARCombo()
            Refresh()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi taso toolbar voi cac button them moi, sua, xuat file, xoa, in hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Xet cac tuy chon gia tri cua Message la UpdateView, InsertView
    ''' Bind lai du lieu cho grid rgRegisterLeave
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New InsuranceRepository
        Dim rep_client As New InsuranceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Message = Request.Params("VIEW")
            Select Case Message
                Case "TRUE"
                    Dim ID As Decimal = Decimal.Parse(Request.Params("ID"))
                    Dim dt As DataTable = New DataTable
                    CurrentState = CommonMessage.STATE_EDIT
                    dt = rep_client.GET_INS_HEALTH_INSURANCE_BY_ID(ID)

                    If dt.Rows.Count > 0 Then
                        Dim r = dt.Rows(0)
                        If IsNumeric(r.Field(Of Decimal?)("EMPLOYEE_ID")) Then
                            hidEmpID.Value = r.Field(Of Decimal)("EMPLOYEE_ID")
                            GetDataCombo_by_FAMILY()
                        End If

                        If IsNumeric(r.Field(Of Decimal?)("ID")) Then
                            hidID.Value = r.Field(Of Decimal)("ID")
                        End If
                        If IsNumeric(r.Field(Of Decimal?)("ORG_ID")) Then
                            HidOrg_ID.Value = r.Field(Of Decimal)("ORG_ID")
                        End If

                        txtEMPLOYEE_CODE.Text = r.Field(Of String)("EMPLOYEE_CODE")
                        txtEMPLOYEE_NAME.Text = r.Field(Of String)("FULLNAME_VN")

                        txtNgaySinh.Text = r.Field(Of String)("BIRTH_DATE")

                        txtORG_NAME.Text = r.Field(Of String)("PHONG_BAN")
                        txtTITLE_NAME.Text = r.Field(Of String)("CHUCDANH")
                        txtCMND.Text = r.Field(Of String)("ID_NO")
                        cboYear.SelectedValue = r.Field(Of String)("YEARS")
                        GetDataCombo()
                        If IsNumeric(r.Field(Of Decimal?)("INS_CONTRACT_ID")) Then
                            cboHDbaohiem.SelectedValue = r.Field(Of Decimal)("INS_CONTRACT_ID")
                            GetDataCombo_by_IDContract()
                        End If
                        txtDVBaoHiem.Text = r.Field(Of String)("ORG_INSURANCE")
                        If IsDate(r.Field(Of Date?)("START_DATE")) Then
                            rdHDTuNgay.SelectedDate = r.Field(Of Date)("START_DATE")
                        End If
                        If IsDate(r.Field(Of Date?)("EXPIRE_DATE")) Then
                            rdHDDenNgay.SelectedDate = r.Field(Of Date)("EXPIRE_DATE")
                        End If
                        If IsNumeric(r.Field(Of Decimal?)("VAL_CO")) Then
                            txtGiaTri_HD.Value = r.Field(Of Decimal)("VAL_CO")
                        End If
                        If IsNumeric(r.Field(Of Decimal?)("INS_CONTRACT_DE_ID")) Then
                            cboCTBH.SelectedValue = r.Field(Of Decimal)("INS_CONTRACT_DE_ID")
                        End If
                        If IsNumeric(r.Field(Of Decimal?)("SOTIEN")) Then
                            txtSoTien.Value = r.Field(Of Decimal)("SOTIEN")
                        End If
                        If IsNumeric(r.Field(Of Decimal?)("CHECK_BHNT")) Then
                            'If r.Field(Of Decimal?)("CHECK_BHNT") = 0 Then
                            '    chkCheckBHNguoiThan.Checked = True
                            'Else
                            '    chkCheckBHNguoiThan.Checked = False
                            'End If
                            chkCheckBHNguoiThan.Checked = r.Field(Of Decimal)("CHECK_BHNT")

                            CheckBox_CheckChanged(Nothing, Nothing)
                        End If
                        If IsNumeric(r.Field(Of Decimal?)("DT_CHITRA")) Then
                            cboDTCT.SelectedValue = r.Field(Of Decimal)("DT_CHITRA")
                        End If
                        If IsNumeric(r.Field(Of Decimal?)("ID_NGUOITHAN")) Then
                            rcfamily_name.SelectedValue = r.Field(Of Decimal)("ID_NGUOITHAN")
                        End If
                        rcfamily_name.Text = r.Field(Of String)("HOTENNGUOITHAN")
                        txtMoiQuanHeTN.Text = r.Field(Of String)("MOIQUANHE")
                        If IsDate(r.Field(Of Date?)("NGAYSINHTN")) Then
                            rdNgaySinhTN.SelectedDate = r.Field(Of Date)("NGAYSINHTN")
                        End If
                        txtCMNDTN.Text = r.Field(Of String)("CMNDTN")
                        If IsDate(r.Field(Of Date?)("JOIN_DATE")) Then
                            rdNgaythamGia.SelectedDate = r.Field(Of Date)("JOIN_DATE")
                        End If
                        If IsDate(r.Field(Of Date?)("EFFECT_DATE")) Then
                            dpSTART_DATE.SelectedDate = r.Field(Of Date)("EFFECT_DATE")
                        End If
                        If IsNumeric(r.Field(Of Decimal?)("MONEY_INS")) Then
                            txtSoTienBaoHiem.Value = r.Field(Of Decimal)("MONEY_INS")
                        End If
                        If IsDate(r.Field(Of Date?)("REDUCE_DATE")) Then
                            dpEND_DATE.SelectedDate = r.Field(Of Date)("REDUCE_DATE")
                        End If
                        If IsNumeric(r.Field(Of Decimal?)("REFUND")) Then
                            nmCOST.Value = r.Field(Of Decimal)("REFUND")
                        End If
                        If IsDate(r.Field(Of Date?)("DATE_RECEIVE_MONEY")) Then
                            rdNgayNhanTien.SelectedDate = r.Field(Of Date)("DATE_RECEIVE_MONEY")
                        End If
                        txtNguoiNhanTien.Text = r.Field(Of String)("EMP_RECEIVE_MONEY")
                        txtNote.Text = r.Field(Of String)("NOTES")

                        _Value = ID
                        lstAllow = rep_client.GetINS_CLAIME_INSURANCE_BY_ID(_Value)
                    End If
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"


    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
            End Select

            UpdateControlState()
            Select Case sender.ID
                Case btnFindEmployee.ID
                    ctrlFindEmployeePopup.Show()
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objData As New INS_HEALTH_INSURANCE_DTO
        Dim rep As New InsuranceRepository
        Dim rep_client As New InsuranceBusinessClient
        Dim gID As Integer = 0
        Dim gstatus As Integer = 0
        Dim re As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then

                        'Dim obj_check As INS_HEALTH_INSURANCE_DTO = New INS_HEALTH_INSURANCE_DTO
                        If CurrentState = CommonMessage.STATE_EDIT Then
                            'obj_check.ID = hidID.Value
                            objData.ID = hidID.Value
                        End If
                        'obj_check.EMPLOYEE_ID = hidEmpID.Value
                        'obj_check.YEAR = cboYear.SelectedValue
                        'obj_check.INS_CONTRACT_ID = cboHDbaohiem.SelectedValue


                        objData.EMPLOYEE_ID = hidEmpID.Value
                        objData.ORG_ID = HidOrg_ID.Value
                        objData.YEAR = cboYear.SelectedValue
                        objData.INS_CONTRACT_ID = cboHDbaohiem.SelectedValue
                        objData.INS_CONTRACT_DE_ID = cboCTBH.SelectedValue
                        objData.CHECK_BHNT = chkCheckBHNguoiThan.Checked
                        'If chkCheckBHNguoiThan.Checked = True Then
                        '    objData.CHECK_BHNT = 0
                        'Else
                        '    objData.CHECK_BHNT = 1
                        'End If
                        If rcfamily_name.SelectedValue IsNot Nothing AndAlso rcfamily_name.SelectedValue <> "" Then
                            objData.FAMYLI_ID = rcfamily_name.SelectedValue
                        Else
                            objData.FAMYLI_ID = Nothing
                        End If
                        If Not re.ValidateHealth_Insurance(objData) Then
                            ShowMessage("Dữ liệu đã tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                        objData.DT_CHITRA = cboDTCT.SelectedValue
                        objData.JOIN_DATE = rdNgaythamGia.SelectedDate
                        objData.EFFECT_DATE = dpSTART_DATE.SelectedDate
                        objData.MONEY_INS = txtSoTienBaoHiem.Value
                        objData.REDUCE_DATE = dpEND_DATE.SelectedDate

                        objData.REFUND = nmCOST.Value
                        objData.DATE_RECEIVE_MONEY = rdNgayNhanTien.SelectedDate
                        objData.EMP_RECEIVE_MONEY = txtNguoiNhanTien.Text
                        objData.NOTES = txtNote.Text
                        If _Value.HasValue Then

                            gID = Utilities.ObjToDecima(hidID.Value)
                            rep.ModifyHEALTH_INSURANCE(objData, gID)
                            rep_client.DELETE_INS_CLAIME_INSURANCE(_Value)
                        Else
                            gID = Utilities.ObjToDecima(hidID.Value)
                            rep.InsertHEALTH_INSURANCE(objData, gID)
                        End If
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        Dim G_CLAIME_ID As Integer = 0
                        For Each dtGrid As GridDataItem In rgAllow.SelectedItems
                            Dim obj_Claim As INS_CLAIME_INSURANCEDTO
                            obj_Claim = New INS_CLAIME_INSURANCEDTO
                            obj_Claim.INS_HEALTH_ID = Utilities.ObjToDecima(gID)
                            obj_Claim.EXAMINE_DATE = Date.Parse(dtGrid("EXAMINE_DATE").Text)
                            obj_Claim.DISEASE_NAME = dtGrid("DISEASE_NAME").Text

                            obj_Claim.AMOUNT_OF_CLAIMS = Decimal.Parse(dtGrid("AMOUNT_OF_CLAIMS").Text)
                            obj_Claim.AMOUNT_OF_COMPENSATION = Decimal.Parse(dtGrid("AMOUNT_OF_COMPENSATION").Text)
                            obj_Claim.COMPENSATION_DATE = Date.Parse(dtGrid("COMPENSATION_DATE").Text)
                            obj_Claim.NOTE = dtGrid("NOTE").Text
                            rep.InsertCLAIME_INSURANCE(obj_Claim, G_CLAIME_ID)
                        Next
                        'rdThoiDiem.Enabled = False
                        dpSTART_DATE.Enabled = False
                        dpEND_DATE.Enabled = False
                        'cboLEVEL.Enabled = False
                        nmCOST.Enabled = False
                        'nmCOSTSAL.Enabled = False
                        'Dim str As String = "getRadWindow().close('1');"
                        'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        ''POPUPTOLINK()
                        'Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlInsManagerSunCare&group=Business")

                        CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlInsManagerSunCare&group=Business")
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgAllow_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgAllow.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            datarow.Selected = True
        End If
    End Sub
    Private Sub rgAllow_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgAllow.NeedDataSource
        Try
            If lstAllow IsNot Nothing AndAlso lstAllow.Count > 0 Then
                rgAllow.DataSource = lstAllow
            Else
                lstAllow = New List(Of INS_CLAIME_INSURANCEDTO)
                rgAllow.DataSource = New List(Of INS_CLAIME_INSURANCEDTO)
            End If

        Catch ex As Exception

            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rgAllow_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgAllow.ItemCommand
        Select Case e.CommandName
            Case "InsertAllow"
                If rdNgayKhamBenh.SelectedDate Is Nothing Then
                    ShowMessage(Translate("Bạn phải chọn ngày khám bệnh"), NotifyType.Warning)
                    Exit Sub
                End If

                If txtTenBenh.Text = "" Or txtTenBenh.Text Is Nothing Then
                    ShowMessage(Translate("Bạn phải nhập tên bệnh"), NotifyType.Warning)
                    Exit Sub
                End If
                Dim lstAllowList As New List(Of Date)
                For Each item As GridDataItem In rgAllow.Items
                    lstAllowList.Add(item.GetDataKeyValue("EXAMINE_DATE"))
                Next
                If lstAllowList.Contains(rdNgayKhamBenh.SelectedDate) Then
                    ShowMessage(Translate("Ngày khám bệnh đã tồn tại trên lưới danh sách"), NotifyType.Warning)
                    Exit Sub
                End If
                Dim allow1 As INS_CLAIME_INSURANCEDTO
                allow1 = New INS_CLAIME_INSURANCEDTO
                allow1.ID = 0
                allow1.INS_HEALTH_ID = _Value
                allow1.EXAMINE_DATE = rdNgayKhamBenh.SelectedDate
                allow1.DISEASE_NAME = txtTenBenh.Text

                allow1.AMOUNT_OF_CLAIMS = RnYCBoiThuong.Text
                allow1.AMOUNT_OF_COMPENSATION = rdDuocBoiThuong.Text
                allow1.COMPENSATION_DATE = rdNgayBoiThuong.SelectedDate
                allow1.NOTE = txtGhiChu.Text
                ClearControlValue(rdNgayKhamBenh, txtTenBenh, RnYCBoiThuong, rdDuocBoiThuong, txtGhiChu, rdNgayBoiThuong)
                lstAllow.Add(allow1)
                rgAllow.Rebind()
            Case "DeleteAllow"

                If rgAllow.SelectedItems.Count = 0 Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    Exit Sub
                End If

                ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                ctrlMessageBox.ActionName = "REMOVEALLOW"
                ctrlMessageBox.DataBind()
                ctrlMessageBox.Show()
        End Select

    End Sub
    Protected Sub dpSTART_DATE_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dpSTART_DATE.SelectedDateChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dpSTART_DATE.SelectedDate IsNot Nothing Then
                If cboHDbaohiem.SelectedValue = "" Or cboCTBH.SelectedValue = "" Then
                    ShowMessage(Translate("Chưa chọn HĐ và gói bảo hiểm"), Utilities.NotifyType.Warning)
                    dpSTART_DATE.Clear()
                    Return
                End If
                Dim dEffect As Date = dpSTART_DATE.SelectedDate
                If rdHDDenNgay.SelectedDate IsNot Nothing Then
                    Dim dExpire As Date = rdHDDenNgay.SelectedDate
                    Dim Songaysudung = DateDiff(DateInterval.Day, dEffect, dExpire) + 1
                    If Songaysudung >= 0 Then
                        Dim date1 = CDate(rdHDTuNgay.SelectedDate)
                        Dim date2 = CDate(rdHDDenNgay.SelectedDate)
                        Dim span = date2 - date1

                        Dim days As Double = Math.Round(span.TotalDays, 2) + 1
                        Dim SotienBHTang As Decimal = (If(txtSoTien.Value.HasValue, txtSoTien.Value, 0) / days * Songaysudung)
                        txtSoTienBaoHiem.Value = SotienBHTang
                    Else
                        txtSoTienBaoHiem.Value = 0
                    End If
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Protected Sub dpEND_DATE_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dpEND_DATE.SelectedDateChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dpEND_DATE.SelectedDate IsNot Nothing AndAlso rgAllow.Items.Count = 0 Then
                Dim dEnd_date As Date = dpEND_DATE.SelectedDate
                If dpSTART_DATE.SelectedDate IsNot Nothing Then
                    Dim dEffect As Date = dpSTART_DATE.SelectedDate
                    Dim SoNgaySuDung = DateDiff("d", dEffect, dEnd_date) + 1
                    Dim date1 = CDate(rdHDTuNgay.SelectedDate)
                    Dim date2 = CDate(rdHDDenNgay.SelectedDate)
                    Dim span = date2 - date1

                    Dim days As Double = Math.Round(span.TotalDays, 2) + 1
                    Dim SotienBHHoanLai As Decimal = If(txtSoTien.Value.HasValue, txtSoTien.Value, 0) - (If(txtSoTien.Value.HasValue, txtSoTien.Value, 0) / days * SoNgaySuDung)
                    nmCOST.Value = SotienBHHoanLai
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Public Sub CheckBox_CheckChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'If CurrentState = CommonMessage.STATE_EDIT Or  Then
            'Dim chk = DirectCast(sender, CheckBox)
            'Select Case chk.ID
            '    Case "chkCheckBHNguoiThan"
            If chkCheckBHNguoiThan.Checked = True Then
                Hide_1.Visible = True
                Hide_2.Visible = True
            Else
                Hide_1.Visible = False
                Hide_2.Visible = False
                ClearControlValue(rcfamily_name, txtMoiQuanHeTN, rdNgaySinhTN, txtCMNDTN)
                cboDTCT.SelectedValue = 1
            End If
            'End Select
            'End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand

        Try
            If e.ActionName = "REMOVEALLOW" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                For Each selected As GridDataItem In rgAllow.SelectedItems
                    lstAllow.RemoveAll(Function(x) x.EXAMINE_DATE = selected.GetDataKeyValue("EXAMINE_DATE"))
                Next
                rgAllow.Rebind()
                ClearControlValue(rdNgayKhamBenh, txtTenBenh, RnYCBoiThuong, rdDuocBoiThuong, txtGhiChu)
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(txtDVBaoHiem, rdHDTuNgay, rdHDDenNgay, txtGiaTri_HD, cboCTBH, cboHDbaohiem, txtSoTien)
            GetDataCombo()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cboHDbaohiem_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboHDbaohiem.SelectedIndexChanged
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(txtDVBaoHiem, rdHDTuNgay, rdHDDenNgay, txtGiaTri_HD, cboCTBH, txtSoTien)
            If cboHDbaohiem.SelectedValue <> "" Then
                Dim dtCost As New INS_LIST_CONTRACTDTO
                dtCost = rep.GetINS_LIST_CONTRACT_BY_ID(Utilities.ObjToInt(cboHDbaohiem.SelectedValue))
                If dtCost IsNot Nothing Then
                    txtDVBaoHiem.Text = dtCost.ORG_INSURANCE_NAME
                    rdHDTuNgay.SelectedDate = dtCost.START_DATE
                    rdHDDenNgay.SelectedDate = dtCost.EXPIRE_DATE
                    txtGiaTri_HD.Value = dtCost.VAL_CO
                    GetDataCombo_by_IDContract()
                End If
            Else
                FillRadCombobox(cboCTBH, New DataTable, "INS_PROGRAM_NAME", "ID", False)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cboCTBH_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCTBH.SelectedIndexChanged
        Dim rep As New InsuranceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(txtSoTien)
            If cboCTBH.Text <> "" Then
                Dim row As DataRow = lst_Program.Select("ID=" & cboCTBH.SelectedValue).FirstOrDefault()
                If Not row Is Nothing Then
                    If row.Item("MONEY_INS") IsNot Nothing Then
                        txtSoTien.Text = row.Item("MONEY_INS")
                    Else
                        txtSoTien.Text = 0
                    End If

                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rcfamily_name_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcfamily_name.SelectedIndexChanged
        Dim rep As New InsuranceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(txtMoiQuanHeTN, rdNgaySinhTN, txtCMNDTN, cboDTCT)
            If rcfamily_name.Text <> "" Then
                Dim row As DataRow = lst_Family.Select("ID=" & rcfamily_name.SelectedValue).FirstOrDefault()
                If Not row Is Nothing Then
                    txtMoiQuanHeTN.Text = row.Item("NAME_VN").ToString
                    rdNgaySinhTN.SelectedDate = row.Item("BIRTH_DATE").ToString
                    txtCMNDTN.Text = row.Item("ID_NO").ToString
                    If row.Item("CODE_QUANHE").ToString = "01" Or row.Item("CODE_QUANHE").ToString = "02" Then
                        cboDTCT.SelectedValue = 1
                    Else
                        cboDTCT.SelectedValue = 2
                    End If
                    'tinh tuoi than nhan neu >65 thì cảnh báo “Vượt quá giới hạn tuổi cho phép mua bảo hiểm”
                    Dim NgaySinhTN As Date = rdNgaySinhTN.SelectedDate
                    Dim months As Integer = DateDiff(DateInterval.Month, NgaySinhTN, Now)
                    Dim years As Integer = months / 12
                    If years > 65 Then
                        ShowMessage(Translate("Vượt quá giới hạn tuổi cho phép mua bảo hiểm"), Utilities.NotifyType.Warning)
                    End If
                End If
            End If
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
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Update trang thai cua cac control theo state
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
                        ctrlFindEmployeePopup.MultiSelect = False
                    End If
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien chon nhan vien cho ctrFindEmployee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New InsuranceRepository
        Dim rep_client As New InsuranceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                ClearControlValue(cboYear, cboHDbaohiem, txtDVBaoHiem, rdHDTuNgay, rdHDDenNgay, txtGiaTri_HD, cboCTBH, txtSoTien, cboDTCT, rcfamily_name, txtMoiQuanHeTN, rdNgaySinhTN, txtCMNDTN, rdNgaythamGia, dpSTART_DATE, txtSoTienBaoHiem, dpEND_DATE, nmCOST, rdNgayNhanTien, txtNguoiNhanTien, txtNote)
                hidEmpID.Value = lstCommonEmployee(0).EMPLOYEE_ID
                txtEMPLOYEE_CODE.Text = lstCommonEmployee(0).EMPLOYEE_CODE.ToString
                txtORG_NAME.Text = lstCommonEmployee(0).ORG_NAME.ToString
                If IsNumeric(lstCommonEmployee(0).ORG_ID) Then
                    HidOrg_ID.Value = lstCommonEmployee(0).ORG_ID
                End If
                txtTITLE_NAME.Text = lstCommonEmployee(0).TITLE_NAME.ToString
                txtEMPLOYEE_NAME.Text = lstCommonEmployee(0).FULLNAME_VN.ToString
                If IsDate(lstCommonEmployee(0).BIRTH_DATE) Then
                    txtNgaySinh.Text = lstCommonEmployee(0).BIRTH_DATE
                End If
                txtCMND.Text = lstCommonEmployee(0).ID_NO.ToString
                'txtCapNS.Text = lstCommonEmployee(0).STAFF_RANK_NAME
                GetDataCombo_by_FAMILY()
                cboDTCT.SelectedValue = 1
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub GetYEARCombo()
        Try
            Dim rep As New InsuranceBusinessClient

            Dim lst_Contract As DataTable = rep.GetINS_Contract_Combobox(False)
            Dim dt_year = lst_Contract.DefaultView.ToTable(True, "YEAR")
            dt_year.DefaultView.Sort = "YEAR ASC"

            FillRadCombobox(cboYear, dt_year.DefaultView.ToTable(), "YEAR", "YEAR", False)

            'Dim table As New DataTable
            'table.Columns.Add("YEAR", GetType(Integer))
            'table.Columns.Add("ID", GetType(Integer))
            'Dim row As DataRow
            'row = table.NewRow
            'row("ID") = DBNull.Value
            'row("YEAR") = DBNull.Value
            'table.Rows.Add(row)
            'For index = 2018 To Date.Now.Year + 2
            '    row = table.NewRow
            '    row("ID") = index
            '    row("YEAR") = index
            '    table.Rows.Add(row)
            'Next
            'FillRadCombobox(cboYear, table, "YEAR", "ID")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetDataCombo()
        Try
            Dim rep As New InsuranceBusinessClient
            If cboYear.SelectedValue <> "" Then
                Dim lst_Contract As DataTable = rep.GET_CBO_INS_LIST_CONTRACT(cboYear.SelectedValue) 'HOP DONG BAO HIEM
                FillRadCombobox(cboHDbaohiem, lst_Contract, "CONTRACT_INS_NO", "ID", False)
            Else
                FillRadCombobox(cboHDbaohiem, New DataTable, "CONTRACT_INS_NO", "ID", False)
                FillRadCombobox(cboCTBH, New DataTable, "INS_PROGRAM_NAME", "ID", False)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetDataCombo_by_IDContract()
        Try
            Dim rep As New InsuranceBusinessClient
            lst_Program = rep.GetINS_LIST_CONTRACT_DETAIL_BY_ID_COMBOBOX(cboHDbaohiem.SelectedValue) 'CHUONG TRINH BAO HIEM THEO ID CONTRACT INS
            FillRadCombobox(cboCTBH, lst_Program, "INS_PROGRAM_NAME", "ID", False)


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetDataCombo_by_FAMILY()
        Try
            Dim rep As New InsuranceBusinessClient
            lst_Family = rep.Get_LIST_FAMILY_BY_ID_EMP(Utilities.ObjToInt(hidEmpID.Value)) 'than nhan

            FillRadCombobox(rcfamily_name, lst_Family, "FULLNAME", "ID", False)


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub txtEMPLOYEE_CODE_TextChanged(sender As Object, e As EventArgs) Handles txtEMPLOYEE_CODE.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim store_list As New Store_Insurance_List()
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If txtEMPLOYEE_CODE.Text <> "" Then
                ClearControlValue(cboYear, cboHDbaohiem, txtDVBaoHiem, rdHDTuNgay, rdHDDenNgay, txtGiaTri_HD, cboCTBH, txtSoTien, cboDTCT, rcfamily_name, txtMoiQuanHeTN, rdNgaySinhTN, txtCMNDTN, rdNgaythamGia, dpSTART_DATE, txtSoTienBaoHiem, dpEND_DATE, nmCOST, rdNgayNhanTien, txtNguoiNhanTien, txtNote, txtEMPLOYEE_NAME, txtNgaySinh, txtORG_NAME, HidOrg_ID, txtTITLE_NAME, txtCMND)
                Dim Count = 0
                Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                _filter.EMPLOYEE_CODE = txtEMPLOYEE_CODE.Text
                EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                If Count <= 0 Then
                    ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                    txtEMPLOYEE_CODE.Text = ""
                ElseIf Count = 1 Then
                    Dim item = EmployeeList(0)


                    hidEmpID.Value = item.EMPLOYEE_ID
                    txtEMPLOYEE_CODE.Text = item.EMPLOYEE_CODE.ToString
                    txtORG_NAME.Text = item.ORG_NAME.ToString
                    If IsNumeric(item.ORG_ID) Then
                        HidOrg_ID.Value = item.ORG_ID
                    End If
                    txtTITLE_NAME.Text = item.TITLE_NAME.ToString
                    txtEMPLOYEE_NAME.Text = item.FULLNAME_VN.ToString
                    Dim lstSource As DataTable = store_list.GET_INS_EMPINFO(item.EMPLOYEE_ID, String.Empty)
                    If (Not (lstSource Is Nothing) AndAlso lstSource.Rows.Count > 0) Then
                        'If IsDate(item.BIRTH_DATE) Then
                        '    txtNgaySinh.Text = lstSource.Rows(0)("EMPLOYEE_ID")
                        'End If
                        InsCommon.SetString(txtNgaySinh, lstSource.Rows(0)("BIRTH_DATE"))
                        'txtCMND.Text = item.ID_NO.ToString
                        InsCommon.SetString(txtCMND, lstSource.Rows(0)("ID_NO"))
                    End If
                    'txtCapNS.Text = lstCommonEmployee(0).STAFF_RANK_NAME
                    GetDataCombo_by_FAMILY()
                    cboDTCT.SelectedValue = 1
                    isLoadPopup = 0
                ElseIf Count > 1 Then
                    If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                        'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                    End If
                    If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEMPLOYEE_CODE.Text
                        phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.Show()
                        isLoadPopup = 1
                    End If
                End If
            Else
                ClearControlValue(cboYear, cboHDbaohiem, txtDVBaoHiem, rdHDTuNgay, rdHDDenNgay, txtGiaTri_HD, cboCTBH, txtSoTien, cboDTCT, rcfamily_name, txtMoiQuanHeTN, rdNgaySinhTN, txtCMNDTN, rdNgaythamGia, dpSTART_DATE, txtSoTienBaoHiem, dpEND_DATE, nmCOST, rdNgayNhanTien, txtNguoiNhanTien, txtNote, txtEMPLOYEE_NAME, txtNgaySinh, txtORG_NAME, HidOrg_ID, txtTITLE_NAME, txtCMND)
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

    Private Sub cusdpSTART_DATE_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cusdpSTART_DATE.ServerValidate
        Try
            If rdHDTuNgay.SelectedDate Is Nothing Or rdHDDenNgay.SelectedDate Is Nothing Then
                dpSTART_DATE.Clear()
                args.IsValid = False
            End If
            If dpSTART_DATE.SelectedDate < rdHDTuNgay.SelectedDate Or dpSTART_DATE.SelectedDate > rdHDDenNgay.SelectedDate Then
                dpSTART_DATE.Clear()
                dpSTART_DATE.Focus()
                args.IsValid = False
            End If
        Catch ex As Exception
            args.IsValid = True
        End Try

    End Sub
End Class

