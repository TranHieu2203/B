Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlInsListContractNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Insurance/Module/Insurance/List/" + Me.GetType().Name.ToString()

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

    Property _Value As Decimal?
        Get
            Return ViewState(Me.ID & "_Value")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_Value") = value
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

    Property lstAllow As List(Of INS_LIST_CONTRACT_DETAILDTO)
        Get
            Return ViewState(Me.ID & "_lstAllow")
        End Get
        Set(ByVal value As List(Of INS_LIST_CONTRACT_DETAILDTO))
            ViewState(Me.ID & "_lstAllow") = value
        End Set
    End Property
    Property dtAllowUpdate As DataTable
        Get
            Return ViewState(Me.ID & "_dtAllowUpdate")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtAllowUpdate") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
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
                    Dim obj As New INS_LIST_CONTRACTDTO

                    obj.ID = Decimal.Parse(Request.Params("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GetINS_LIST_CONTRACT_BY_ID(obj.ID)

                    If obj IsNot Nothing Then
                        hidID.Value = obj.ID
                        txtSoHD.Text = obj.CONTRACT_INS_NO
                        txtNam.Text = obj.YEAR.ToString
                        ddlINS_ORG_ID.SelectedValue = obj.ORG_INSURANCE
                        txtTuNgay.SelectedDate = obj.START_DATE
                        txtDenNgay.SelectedDate = obj.EXPIRE_DATE
                        rdGiaTriHD.Value = obj.VAL_CO
                        txtBuy_DATE.SelectedDate = obj.BUY_DATE
                        txtNote.Text = obj.NOTE
                        _Value = obj.ID
                        lstAllow = rep_client.GetINS_LIST_CONTRACT_DETAIL_BY_ID(_Value)
                        Dim lstDeletes As New List(Of Decimal)
                        lstDeletes.Add(Decimal.Parse(obj.ID))
                        If Not rep.CheckExistInDatabase(lstDeletes, InsuranceCommonTABLE_NAME.INS_LIST_CONTRACT) Then
                            ShowMessage("Số hợp đồng đã được sử dụng, không được chỉnh sửa", NotifyType.Warning)
                            EnableControlAll(False, txtSoHD, txtNam, ddlINS_ORG_ID, txtTuNgay, txtDenNgay, rdGiaTriHD, txtBuy_DATE, txtNote, ddlINS_PROGRAM, rntxtValue, rgAllow)
                            Return
                        Else
                            EnableControlAll(True, txtSoHD, txtNam, ddlINS_ORG_ID, txtTuNgay, txtDenNgay, rdGiaTriHD, txtBuy_DATE, txtNote, ddlINS_PROGRAM, rntxtValue, rgAllow)
                        End If
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

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button ctrFindPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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



    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objData As INS_LIST_CONTRACTDTO
        Dim rep As New InsuranceRepository
        Dim rep_client As New InsuranceBusinessClient
        Dim gID As Integer = 0
        Dim gstatus As Integer = 0
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Using re As New InsuranceRepository
                            Dim objData_contract As INS_LIST_CONTRACTDTO = New INS_LIST_CONTRACTDTO
                            If IsNumeric(hidID.Value) Then
                                objData_contract.ID = hidID.Value
                            End If
                            objData_contract.CONTRACT_INS_NO = txtSoHD.Text
                            If Not re.ValidateListContract(objData_contract) Then
                                ShowMessage("Số hợp đồng đã có", NotifyType.Error)
                                Exit Sub
                            End If
                        End Using
                        If _Value.HasValue Then
                            objData = New INS_LIST_CONTRACTDTO
                            objData.ID = hidID.Value
                            objData.CONTRACT_INS_NO = txtSoHD.Text
                            If IsNumeric(txtNam.Text) Then
                                objData.YEAR = Decimal.Parse(txtNam.Text)
                            End If
                            objData.ORG_INSURANCE = ddlINS_ORG_ID.SelectedValue
                            objData.START_DATE = txtTuNgay.SelectedDate
                            objData.EXPIRE_DATE = txtDenNgay.SelectedDate
                            objData.VAL_CO = rdGiaTriHD.Value
                            objData.BUY_DATE = txtBuy_DATE.SelectedDate
                            objData.NOTE = txtNote.Text.Trim
                            rep.ModifyINS_LIST_CONTRACT(objData, gID)
                            rep_client.DeleteINS_LIST_CONTRACT_DETAIL(_Value)
                        Else
                            objData = New INS_LIST_CONTRACTDTO
                            objData.CONTRACT_INS_NO = txtSoHD.Text
                            If IsNumeric(txtNam.Text) Then
                                objData.YEAR = Decimal.Parse(txtNam.Text)
                            End If
                            objData.ORG_INSURANCE = ddlINS_ORG_ID.SelectedValue
                            objData.START_DATE = txtTuNgay.SelectedDate
                            objData.EXPIRE_DATE = txtDenNgay.SelectedDate
                            objData.VAL_CO = rdGiaTriHD.Value
                            objData.BUY_DATE = txtBuy_DATE.SelectedDate
                            objData.NOTE = txtNote.Text.Trim
                            gID = Utilities.ObjToDecima(hidID.Value)
                            rep.InsertINS_LIST_CONTRACT(objData, gID)
                        End If
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        For Each dtGrid As GridDataItem In rgAllow.SelectedItems
                            Dim INS_PROGRAM_ID As Decimal = Decimal.Parse(dtGrid("INS_PROGRAM_ID").Text)
                            Dim MONEY_INS As Decimal = Decimal.Parse(dtGrid("MONEY_INS").Text)
                            rep_client.Insert_INS_LIST_CONTRACT_DETAIL(Utilities.ObjToDecima(gID), INS_PROGRAM_ID, MONEY_INS)
                        Next
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        'POPUPTOLINK()
                        'Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlInsListContract&group=List")

                        CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlInsListContract&group=List")
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
    ''' Load du lieu cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Try
            Dim rep As New InsuranceBusinessClient
            Dim lst_Program As DataTable = rep.GetINS_Program_Combobox(False) 'CHUONG TRINH BAO HIEM
            FillRadCombobox(ddlINS_PROGRAM, lst_Program, "NAME", "ID", False)

            'Dim lstSource As DataTable = rep.GET_INS_LIST_INSURANCE(True) 'Don vi bao hiem
            'FillRadCombobox(ddlINS_ORG_ID, lstSource, "NAME_VN", "ID", False)
            Dim lstSource As DataTable = rep.GetInsListInsuranceByUsername("ADMIN", False)
            FillRadCombobox(ddlINS_ORG_ID, lstSource, "NAME", "ID", True)
        Catch ex As Exception
            Throw ex
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
                lstAllow = New List(Of INS_LIST_CONTRACT_DETAILDTO)
                rgAllow.DataSource = New List(Of INS_LIST_CONTRACT_DETAILDTO)
            End If

        Catch ex As Exception

            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand

        Try
            If e.ActionName = "REMOVEALLOW" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                For Each selected As GridDataItem In rgAllow.SelectedItems
                    lstAllow.RemoveAll(Function(x) x.INS_PROGRAM_ID = selected.GetDataKeyValue("INS_PROGRAM_ID"))
                Next

                rgAllow.Rebind()
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub rgAllow_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgAllow.ItemCommand
        Try
            Select Case e.CommandName
                Case "InsertAllow"
                    Dim val_money As Decimal = 0
                    If ddlINS_PROGRAM.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn chương trình bảo hiểm"), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rntxtValue.Value = 0 Or rntxtValue Is Nothing Then
                        ShowMessage(Translate("Bạn phải nhập số tiền"), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstAllowList As New List(Of Decimal)
                    For Each item As GridDataItem In rgAllow.Items
                        lstAllowList.Add(item.GetDataKeyValue("INS_PROGRAM_ID"))
                    Next
                    If lstAllowList.Contains(ddlINS_PROGRAM.SelectedValue) Then
                        ShowMessage(Translate("Chương trình bảo hiểm đã tồn tại dưới lưới"), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim allow1 As INS_LIST_CONTRACT_DETAILDTO
                    allow1 = New INS_LIST_CONTRACT_DETAILDTO
                    allow1.ID = 0
                    allow1.INS_PROGRAM_ID = ddlINS_PROGRAM.SelectedValue
                    allow1.INS_PROGRAM_NAME = ddlINS_PROGRAM.Text.Trim
                    allow1.MONEY_INS = rntxtValue.Text

                    'If rntxtPercentINS.Text <> "" Then
                    '    allow1.PERCENT_INS = rntxtPercentINS.Value
                    'Else
                    '    allow1.PERCENT_INS = 0
                    'End If

                    lstAllow.Add(allow1)
                    rgAllow.Rebind()


                Case "DeleteAllow"
                    Dim str As String
                    If rgAllow.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = "REMOVEALLOW"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
        Catch ex As Exception
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
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                hidEmpID.Value = lstCommonEmployee(0).EMPLOYEE_ID
                'txtEMPLOYEE_CODE.Text = lstCommonEmployee(0).EMPLOYEE_CODE
                'txtORG_NAME.Text = lstCommonEmployee(0).ORG_NAME
                'txtTITLE_NAME.Text = lstCommonEmployee(0).TITLE_NAME
                'txtEMPLOYEE_NAME.Text = lstCommonEmployee(0).FULLNAME_VN
                'txtCapNS.Text = lstCommonEmployee(0).STAFF_RANK_NAME

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub txtNam_TextChanged(sender As Object, e As EventArgs) Handles txtNam.TextChanged
        Try
            ValidateYearDate()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub txtDenNgay_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs) Handles txtDenNgay.SelectedDateChanged, txtTuNgay.SelectedDateChanged
        Try
            ValidateYearDate()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ValidateYearDate()
        Try
            If txtTuNgay.SelectedDate > txtDenNgay.SelectedDate Then
                ShowMessage(Translate("Hợp đồng đến ngày phải lớn hơn hợp đồng từ ngày"), Utilities.NotifyType.Warning)
                Exit Sub
            End If
            If txtNam.Text <> "" Then
                If (txtTuNgay.SelectedDate IsNot Nothing And txtDenNgay.SelectedDate IsNot Nothing) AndAlso txtDenNgay.SelectedDate > txtTuNgay.SelectedDate Then
                    If txtTuNgay.SelectedDate.Value.Year <> Decimal.Parse(txtNam.Text) Then
                        ShowMessage("HĐ từ ngày phải trong năm", NotifyType.Warning)
                        txtTuNgay.SelectedDate = Nothing
                        txtTuNgay.Focus()
                        Exit Sub
                        'ElseIf txtDenNgay.SelectedDate.Value.Year <> Decimal.Parse(txtNam.Text) Then
                        '    ShowMessage("HĐ đến ngày phải trong năm", NotifyType.Warning)
                        '    txtDenNgay.SelectedDate = Nothing
                        '    txtDenNgay.Focus()
                        '    Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region


End Class

