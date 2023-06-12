Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI

Public Class ctrlInsArisingManual
    Inherits Common.CommonView
    'Private WithEvents viewRegister As ctrlShiftPlanningRegister

    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlOrgSULPPopup As ctrlFindOrgPopup

#Region "Property & Variable"

    Public Property popup As RadWindow
    Public Property popupId As String
    Public Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Private Property LastStartDate As Nullable(Of Date)
        Get
            Return PageViewState("LastStartDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            PageViewState("LastStartDate") = value
        End Set
    End Property

    Private Property LastEndDate As Nullable(Of Date)
        Get
            Return PageViewState("LastEndDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            PageViewState("LastEndDate") = value
        End Set
    End Property

    Private Property Status As Decimal
        Get
            Return ViewState(Me.ID & "_Status")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Status") = value
        End Set
    End Property

    Private Property Arising_Type As Decimal
        Get
            Return ViewState(Me.ID & "_Arising_Type")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Arising_Type") = value
        End Set
    End Property

    Public ReadOnly Property PageId As String
        Get
            Return Me.ID
        End Get
    End Property

    Public Property InsCompList As DataTable
        Get
            Return ViewState(Me.ID & "_InsCompList")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_InsCompList") = value
        End Set
    End Property

    Public Property SelectedItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_SelectedItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_SelectedItemList") = value
        End Set
    End Property

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar, _
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ShowPopupEmployee()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            IDSelect = 0 'Khoi tao cho IdSelect = 0
            GetDataCombo()
            GetParams() 'ThanhNT added 02/03/2016            

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub UpdateControlState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    btnSearchEmp.Enabled = False

                    ddlINS_ORG_ID.Enabled = False
                    ddlINS_ARISING_TYPE_ID.Enabled = False
                    txtSALARY_PRE_PERIOD.Enabled = False
                    txtSALARY_NOW_PERIOD.Enabled = False
                    txtEFFECTIVE_DATE.Enabled = False
                    txtEXPRIE_DATE.Enabled = False
                    txtDECLARE_DATE.Enabled = False
                    txtARISING_FROM_MONTH.Enabled = False
                    txtARISING_TO_MONTH.Enabled = False
                    txtNOTE.Enabled = False

                    txtHEALTH_RETURN_DATE.Enabled = False

                    chkSI.Enabled = False
                    chkHI.Enabled = False
                    chkUI.Enabled = False
                    chkTNLD_BNN.Enabled = False
                    txtDateIssue.Enabled = False
                    txtDoB.Enabled = False

                    txtR_FROM.Enabled = False
                    txtR_TO.Enabled = False
                    txtR_SI.Enabled = False
                    txtR_HI.Enabled = False
                    txtR_UI.Enabled = False

                    txtO_FROM.Enabled = False
                    txtO_TO.Enabled = False
                    txtO_HI_COM.Enabled = False
                    txtO_HI_EMP.Enabled = False
                    rdHI_DECLARED_DATE.Enabled = False
                    cboHI_RATE_ARREARS.Enabled = False
                    rdHI_DECLARED_DATE.Enabled = False

                    txtA_FROM.Enabled = False
                    txtA_TO.Enabled = False
                    txtA_SI.Enabled = False
                    txtA_HI.Enabled = False
                    txtA_UI.Enabled = False
                    chkIs_Foreign.Enabled = False


                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    btnSearchEmp.Enabled = True

                    ddlINS_ORG_ID.Enabled = True
                    ddlINS_ARISING_TYPE_ID.Enabled = True
                    txtSALARY_PRE_PERIOD.Enabled = True
                    txtSALARY_NOW_PERIOD.Enabled = True
                    txtEFFECTIVE_DATE.Enabled = True
                    txtEXPRIE_DATE.Enabled = True
                    txtDECLARE_DATE.Enabled = True
                    txtARISING_FROM_MONTH.Enabled = True
                    txtARISING_TO_MONTH.Enabled = True
                    txtNOTE.Enabled = True
                    chkSI.Enabled = True
                    chkHI.Enabled = True
                    chkUI.Enabled = True
                    chkTNLD_BNN.Enabled = True
                    chkIs_Foreign.Enabled = True
                    txtHEALTH_RETURN_DATE.Enabled = True

                    txtDateIssue.Enabled = False
                    txtDoB.Enabled = False

                    txtR_FROM.Enabled = True
                    txtR_TO.Enabled = True
                    txtR_SI.Enabled = True
                    txtR_HI.Enabled = True
                    txtR_UI.Enabled = True

                    txtO_FROM.Enabled = True
                    txtO_TO.Enabled = True
                    txtO_HI_COM.Enabled = True
                    txtO_HI_EMP.Enabled = True
                    rdHI_DECLARED_DATE.Enabled = True

                    txtA_FROM.Enabled = True
                    txtA_TO.Enabled = True
                    txtA_SI.Enabled = True
                    txtA_HI.Enabled = True
                    txtA_UI.Enabled = True

                Case CommonMessage.STATE_DELETE

                Case "Nothing"
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Dim str As String = "getRadWindow().close('1');"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                Case CommonMessage.TOOLBARITEM_SAVE
                    Call SaveData()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub txtEMPLOYEE_ID_TextChanged(sender As Object, e As EventArgs) Handles txtEMPLOYEE_ID.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If txtEMPLOYEE_ID.Text <> "" Then
                Reset_Find_Emp()
                Dim Count = 0
                Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                _filter.EMPLOYEE_CODE = txtEMPLOYEE_ID.Text
                EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                If Count <= 0 Then
                    ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                    txtEMPLOYEE_ID.Text = ""
                ElseIf Count = 1 Then
                    Dim item = EmployeeList(0)
                    GetValue_Find_Emp(item.EMPLOYEE_ID)
                    isLoadPopup = 0
                ElseIf Count > 1 Then
                    If FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        FindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                        'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                    End If
                    If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        ctrlFindEmployeePopup.MustHaveContract = False
                        ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEMPLOYEE_ID.Text
                        ctrlFindEmployeePopup.MultiSelect = False
                        FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.Show()
                        isLoadPopup = 1
                    End If
                End If
            Else
                ResetForm()
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Function & Sub"

    Private Sub ResetForm()
        Try
            txtID.Text = "0"
            txtEMPLOYEE_ID.Text = ""
            txtFULLNAME.Text = ""
            txtDEP.Text = ""
            txtEMPID.Text = "0"
            'ThanhNT added 05/01/2016
            txtPOSITION.Text = ""
            txtCMND.Text = ""
            txtDateIssue.SelectedDate = Nothing
            txtDoB.SelectedDate = Nothing
            'txtBirthPlace.Text = ""
            ddlINS_ORG_ID.Text = ""
            ddlINS_ARISING_TYPE_ID.Text = ""
            ddlINS_ARISING_TYPE_ID.SelectedIndex = -1
            'ThanhNT added 05/01/2016  --end
            ddlINS_ORG_ID.Text = ""
            ddlINS_ORG_ID.SelectedIndex = -1
            txtSALARY_PRE_PERIOD.Text = ""
            txtSALARY_NOW_PERIOD.Text = ""
            txtEFFECTIVE_DATE.SelectedDate = Nothing
            txtEXPRIE_DATE.SelectedDate = Nothing
            txtDECLARE_DATE.SelectedDate = Nothing
            txtARISING_FROM_MONTH.SelectedDate = Nothing
            txtARISING_TO_MONTH.SelectedDate = Nothing
            txtNOTE.Text = ""

            txtHEALTH_RETURN_DATE.SelectedDate = Nothing

            txtR_FROM.SelectedDate = Nothing
            txtR_TO.SelectedDate = Nothing
            txtR_SI.Text = "0"
            txtR_HI.Text = "0"
            txtR_UI.Text = "0"

            txtO_FROM.SelectedDate = Nothing
            txtO_TO.SelectedDate = Nothing
            txtO_HI_COM.Text = "0"
            txtO_HI_EMP.Text = "0"
            cboHI_RATE_ARREARS.Text = ""
            cboHI_RATE_ARREARS.SelectedIndex = -1
            rdHI_DECLARED_DATE.SelectedDate = Nothing

            txtA_FROM.SelectedDate = Nothing
            txtA_TO.SelectedDate = Nothing
            txtA_SI.Text = "0"
            txtA_HI.Text = "0"
            txtA_UI.Text = "0"
            txtSI_SAL_NEW.Text = ""
            txtSI_SAL_OLD.Text = ""
            txtHI_SAL_NEW.Text = ""
            txtHI_SAL_OLD.Text = ""
            txtUI_SAL_NEW.Text = ""
            txtUI_SAL_OLD.Text = ""
            rdBHTNLD_BNN_NEW.Text = ""
            rdBHTNLD_BNN_OLD.Text = ""
            rdA_BHTNLD_BNN.Text = "0"
            rdR_BHTNLD_BNN.Text = "0"
            'SOCIAL_NUMBER.Text = ""
            rdSALARY_HD.Text = ""
            rdSALARY_PC.Text = ""
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            Dim rep1 As New InsuranceRepository
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    'Nếu là điều chỉnh thì 2 mức lương phải khác nhau
                    If Arising_Type = 3 AndAlso InsCommon.getNumber(txtSALARY_PRE_PERIOD.Text) = InsCommon.getNumber(txtSALARY_NOW_PERIOD.Text) Then
                        ShowMessage("Vui lòng nhập mức lương kì này khác mức lương kì trước!", NotifyType.Warning)
                        UpdateControlState(CommonMessage.STATE_EDIT)
                        Exit Sub
                    End If
                    Dim pDate As String
                    Dim check As Integer = rep1.CHECK_INS_MANUAL(InsCommon.getNumber(txtEMPID.Text), ddlINS_ARISING_TYPE_ID.SelectedValue, txtARISING_FROM_MONTH.SelectedDate, pDate)

                    If check = 1 Then
                        ShowMessage("Biến động bảo hiểm đầu tiên phải là TĂNG MỚI", NotifyType.Warning)
                        Exit Sub
                    End If
                    If check = 2 Then
                        ShowMessage(String.Format("{0} {1}", "Tháng biến động mới phải lớn hơn tháng biến động tăng mới ", pDate), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rep.UpdateInsArisingManual(Common.Common.GetUsername(), InsCommon.getNumber(0) _
                                                    , InsCommon.getNumber(txtEMPID.Text) _
                                                    , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                                    , InsCommon.getNumber(ddlINS_ARISING_TYPE_ID.SelectedValue) _
                                                    , InsCommon.getNumber(txtSALARY_PRE_PERIOD.Text) _
                                                    , InsCommon.getNumber(txtSALARY_NOW_PERIOD.Text) _
                                                    , InsCommon.getNumber(0) _
                                                    , (txtEFFECTIVE_DATE.SelectedDate) _
                                                    , txtEXPRIE_DATE.SelectedDate _
                                                    , (txtDECLARE_DATE.SelectedDate) _
                                                    , (txtARISING_FROM_MONTH.SelectedDate) _
                                                    , (txtARISING_TO_MONTH.SelectedDate) _
                                                    , txtNOTE.Text _
                                                    , "" _
                                                    , "" _
                                                    , InsCommon.getNumber(0) _
                                                    , Nothing _
                                                    , Nothing _
                                                    , InsCommon.getNumber(0) _
                                                    , (txtR_FROM.SelectedDate) _
                                                    , "" _
                                                    , txtHEALTH_RETURN_DATE.SelectedDate _
                                                    , (txtR_FROM.SelectedDate) _
                                                    , (txtR_FROM.SelectedDate) _
                                                    , (txtR_FROM.SelectedDate) _
                                                    , (txtR_FROM.SelectedDate) _
                                                    , (txtO_FROM.SelectedDate) _
                                                    , (txtR_TO.SelectedDate) _
                                                    , (txtO_TO.SelectedDate) _
                                                    , InsCommon.getNumber(txtR_SI.Text) _
                                                    , InsCommon.getNumber(0) _
                                                    , InsCommon.getNumber(txtR_HI.Text) _
                                                    , InsCommon.getNumber(0) _
                                                    , InsCommon.getNumber(txtR_UI.Text) _
                                                    , InsCommon.getNumber(0) _
                                                    , (txtA_FROM.SelectedDate) _
                                                    , (txtA_TO.SelectedDate) _
                                                    , InsCommon.getNumber(txtA_SI.Text) _
                                                    , InsCommon.getNumber(txtA_HI.Text) _
                                                    , InsCommon.getNumber(txtA_UI.Text) _
                                                    , InsCommon.getNumber(IIf(chkSI.Checked, -1, 0)) _
                                                    , InsCommon.getNumber(IIf(chkHI.Checked, -1, 0)) _
                                                    , InsCommon.getNumber(IIf(chkUI.Checked, -1, 0)) _
                                                    , InsCommon.getNumber(IIf(chkTNLD_BNN.Checked, -1, 0)) _
                                                    , InsCommon.getNumber(txtSI_SAL_NEW.Text) _
                                                    , InsCommon.getNumber(txtHI_SAL_NEW.Text) _
                                                    , InsCommon.getNumber(txtUI_SAL_NEW.Text) _
                                                    , InsCommon.getNumber(rdBHTNLD_BNN_NEW.Text) _
                                                    , InsCommon.getNumber(txtSI_SAL_OLD.Text) _
                                                    , InsCommon.getNumber(txtHI_SAL_OLD.Text) _
                                                    , InsCommon.getNumber(txtUI_SAL_OLD.Text) _
                                                    , InsCommon.getNumber(rdBHTNLD_BNN_OLD.Text) _
                                                    , InsCommon.getNumber(rdA_BHTNLD_BNN.Text) _
                                                    , InsCommon.getNumber(rdR_BHTNLD_BNN.Text) _
                                                    , (rdHI_DECLARED_DATE.SelectedDate) _
                                                    , InsCommon.getNumber(txtO_HI_COM.Value) _
                                                    , InsCommon.getNumber(txtO_HI_EMP.Value) _
                                                    , InsCommon.getNumber(cboHI_RATE_ARREARS.SelectedValue) _
                                                    , InsCommon.getNumber(rdSALARY_HD.Text) _
                                                    , InsCommon.getNumber(rdSALARY_PC.Text) _
                                                    , InsCommon.getNumber(IIf(chkIs_Foreign.Checked, -1, 0))) Then

                        Refresh("InsertView")
                        'Common.Common.OrganizationLocationDataSession = Nothing
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)

                        rep.UPDATE_INS_INFORMATION(InsCommon.getNumber(txtEMPID.Text), txtARISING_FROM_MONTH.SelectedDate)

                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        UpdateControlState(CommonMessage.STATE_EDIT)
                    End If
                    UpdateControlState(CommonMessage.STATE_NORMAL)
                    'Common.Common.OrganizationLocationDataSession = Nothing
                Case CommonMessage.STATE_EDIT
                    'Nếu là điều chỉnh thì 2 mức lương phải khác nhau
                    If Arising_Type = 3 AndAlso InsCommon.getNumber(txtSALARY_PRE_PERIOD.Text) = InsCommon.getNumber(txtSALARY_NOW_PERIOD.Text) Then
                        ShowMessage("Vui lòng nhập mức lương kì này khác mức lương kì trước!", NotifyType.Warning)
                        UpdateControlState(CommonMessage.STATE_EDIT)
                        Exit Sub
                    End If
                    'objOrgFunction.ID = Decimal.Parse(hidID.Value)
                    If rep.UpdateInsArisingManual(Common.Common.GetUsername(), InsCommon.getNumber(txtID.Text) _
                                                                    , InsCommon.getNumber(txtEMPID.Text) _
                                                                    , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                                                    , InsCommon.getNumber(ddlINS_ARISING_TYPE_ID.SelectedValue) _
                                                                    , InsCommon.getNumber(txtSALARY_PRE_PERIOD.Text) _
                                                                    , InsCommon.getNumber(txtSALARY_NOW_PERIOD.Text) _
                                                                    , InsCommon.getNumber(0) _
                                                                    , (txtEFFECTIVE_DATE.SelectedDate) _
                                                                    , txtEXPRIE_DATE.SelectedDate _
                                                                    , (txtDECLARE_DATE.SelectedDate) _
                                                                    , (txtARISING_FROM_MONTH.SelectedDate) _
                                                                    , (txtARISING_TO_MONTH.SelectedDate) _
                                                                    , txtNOTE.Text _
                                                                    , "" _
                                                                    , "" _
                                                                    , InsCommon.getNumber(0) _
                                                                    , Nothing _
                                                                    , Nothing _
                                                                    , InsCommon.getNumber(0) _
                                                                    , (txtR_FROM.SelectedDate) _
                                                                    , "" _
                                                                    , txtHEALTH_RETURN_DATE.SelectedDate _
                                                                    , (txtR_FROM.SelectedDate) _
                                                                    , (txtR_FROM.SelectedDate) _
                                                                    , (txtR_FROM.SelectedDate) _
                                                                    , (txtR_FROM.SelectedDate) _
                                                                    , (txtO_FROM.SelectedDate) _
                                                                    , (txtR_TO.SelectedDate) _
                                                                    , (txtO_TO.SelectedDate) _
                                                                    , InsCommon.getNumber(txtR_SI.Text) _
                                                                    , InsCommon.getNumber(0) _
                                                                    , InsCommon.getNumber(txtR_HI.Text) _
                                                                    , InsCommon.getNumber(0) _
                                                                    , InsCommon.getNumber(txtR_UI.Text) _
                                                                    , InsCommon.getNumber(0) _
                                                                    , (txtA_FROM.SelectedDate) _
                                                                    , (txtA_TO.SelectedDate) _
                                                                    , InsCommon.getNumber(txtA_SI.Text) _
                                                                    , InsCommon.getNumber(txtA_HI.Text) _
                                                                    , InsCommon.getNumber(txtA_UI.Text) _
                                                                    , InsCommon.getNumber(IIf(chkSI.Checked, -1, 0)) _
                                                                    , InsCommon.getNumber(IIf(chkHI.Checked, -1, 0)) _
                                                                    , InsCommon.getNumber(IIf(chkUI.Checked, -1, 0)) _
                                                                    , InsCommon.getNumber(IIf(chkTNLD_BNN.Checked, -1, 0)) _
                                                                    , InsCommon.getNumber(txtSI_SAL_NEW.Text) _
                                                                    , InsCommon.getNumber(txtHI_SAL_NEW.Text) _
                                                                    , InsCommon.getNumber(txtUI_SAL_NEW.Text) _
                                                                    , InsCommon.getNumber(rdBHTNLD_BNN_NEW.Text) _
                                                                    , InsCommon.getNumber(txtSI_SAL_OLD.Text) _
                                                                    , InsCommon.getNumber(txtHI_SAL_OLD.Text) _
                                                                    , InsCommon.getNumber(txtUI_SAL_OLD.Text) _
                                                                    , InsCommon.getNumber(rdBHTNLD_BNN_OLD.Text) _
                                                                    , InsCommon.getNumber(rdA_BHTNLD_BNN.Text) _
                                                                    , InsCommon.getNumber(rdR_BHTNLD_BNN.Text) _
                                                                    , (rdHI_DECLARED_DATE.SelectedDate) _
                                                                    , InsCommon.getNumber(txtO_HI_COM.Value) _
                                                                    , InsCommon.getNumber(txtO_HI_EMP.Value) _
                                                                    , InsCommon.getNumber(cboHI_RATE_ARREARS.SelectedValue) _
                                                                    , InsCommon.getNumber(rdSALARY_HD.Text) _
                                                                    , InsCommon.getNumber(rdSALARY_PC.Text) _
                                                                    , InsCommon.getNumber(IIf(chkIs_Foreign.Checked, -1, 0))) Then
                        Refresh("UpdateView")
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        'UpdateControlState(CommonMessage.STATE_NORMAL)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        UpdateControlState(CommonMessage.STATE_EDIT)
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetDataCombo()
        Try
            Dim rep As New InsuranceBusinessClient
            Dim dtData = rep.GetInsListChangeType() 'Loai bien dong
            FillRadCombobox(ddlINS_ARISING_TYPE_ID, dtData, "ARISING_NAME", "ID", False)

            Dim lstSource As DataTable = rep.GetInsListInsuranceByUsername("ADMIN", False) 'Don vi bao hiem
            FillRadCombobox(ddlINS_ORG_ID, lstSource, "NAME", "ID", False)

            InsCompList = rep.RATE_ARREARS_LIST()

            FillRadCombobox(cboHI_RATE_ARREARS, InsCompList, "NAME_VN", "ID", False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtEXPRIE_DATE_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtEXPRIE_DATE.SelectedDateChanged
        Try
            If txtEXPRIE_DATE.SelectedDate.Value.Day <= 15 Then
                txtARISING_TO_MONTH.SelectedDate = txtEXPRIE_DATE.SelectedDate
            Else
                txtARISING_TO_MONTH.SelectedDate = txtEXPRIE_DATE.SelectedDate.Value.AddMonths(1)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtEFFECTIVE_DATE_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtEFFECTIVE_DATE.SelectedDateChanged
        Try
            If txtEFFECTIVE_DATE.SelectedDate IsNot Nothing Then
                If txtEFFECTIVE_DATE.SelectedDate.Value.Day <= 15 Then
                    txtARISING_FROM_MONTH.SelectedDate = txtEFFECTIVE_DATE.SelectedDate
                Else
                    txtARISING_FROM_MONTH.SelectedDate = txtEFFECTIVE_DATE.SelectedDate.Value.AddMonths(1)
                End If
                Call LoadArisingManual()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtDECLARE_DATE_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtDECLARE_DATE.SelectedDateChanged
        Try
            Call LoadArisingManual()
            If cboHI_RATE_ARREARS.SelectedValue <> "" AndAlso txtDECLARE_DATE.SelectedDate IsNot Nothing AndAlso New Date(txtDECLARE_DATE.SelectedDate.Value.Year, txtDECLARE_DATE.SelectedDate.Value.Month, 1) <= rdHI_DECLARED_DATE.SelectedDate Then
                txtO_FROM.SelectedDate = txtDECLARE_DATE.SelectedDate
                txtO_TO.SelectedDate = txtDECLARE_DATE.SelectedDate
                If txtHI_SAL_OLD.Value <> 0 AndAlso txtHI_SAL_OLD.Value IsNot Nothing Then
                    LoadHiMonneyBHYT()
                End If
            Else
                txtO_FROM.SelectedDate = Nothing
                txtO_TO.SelectedDate = Nothing
                txtO_HI_COM.Value = 0
                txtO_HI_EMP.Value = 0
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub rdHI_DECLARED_DATE_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdHI_DECLARED_DATE.SelectedDateChanged
        Try
            If cboHI_RATE_ARREARS.SelectedValue <> "" AndAlso txtDECLARE_DATE.SelectedDate IsNot Nothing AndAlso New Date(txtDECLARE_DATE.SelectedDate.Value.Year, txtDECLARE_DATE.SelectedDate.Value.Month, 1) <= rdHI_DECLARED_DATE.SelectedDate Then
                txtO_FROM.SelectedDate = txtDECLARE_DATE.SelectedDate
                txtO_TO.SelectedDate = txtDECLARE_DATE.SelectedDate
                If txtHI_SAL_OLD.Value <> 0 AndAlso txtHI_SAL_OLD.Value IsNot Nothing Then
                    LoadHiMonneyBHYT()
                End If
            Else
                txtO_FROM.SelectedDate = Nothing
                txtO_TO.SelectedDate = Nothing
                txtO_HI_COM.Value = 0
                txtO_HI_EMP.Value = 0
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtO_FROM_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtO_FROM.SelectedDateChanged, txtO_TO.SelectedDateChanged
        Try
            If cboHI_RATE_ARREARS.SelectedValue <> "" Then
                If txtHI_SAL_OLD.Value <> 0 AndAlso txtHI_SAL_OLD.Value IsNot Nothing Then
                    LoadHiMonneyBHYT()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtHEALTH_RETURN_DATE_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtHEALTH_RETURN_DATE.SelectedDateChanged
        Try
            Call LoadArisingManual()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkSI_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSI.CheckedChanged
        Try
            Call LoadArisingManual()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkHI_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkHI.CheckedChanged
        Try
            Call LoadArisingManual()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkUI_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkUI.CheckedChanged
        Try
            Call LoadArisingManual()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkTNLD_BNN_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTNLD_BNN.CheckedChanged
        Try
            Call LoadArisingManual()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub chkIs_Foreign_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIs_Foreign.CheckedChanged
        Try
            Call LoadArisingManual()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboHI_RATE_ARREARS_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboHI_RATE_ARREARS.SelectedIndexChanged
        If cboHI_RATE_ARREARS.SelectedValue <> "" AndAlso txtDECLARE_DATE.SelectedDate IsNot Nothing AndAlso New Date(txtDECLARE_DATE.SelectedDate.Value.Year, txtDECLARE_DATE.SelectedDate.Value.Month, 1) <= rdHI_DECLARED_DATE.SelectedDate Then
            If Not IsDate(txtO_FROM.SelectedDate) Then
                txtO_FROM.SelectedDate = txtDECLARE_DATE.SelectedDate
            End If

            If Not IsDate(txtO_TO.SelectedDate) Then
                txtO_TO.SelectedDate = txtDECLARE_DATE.SelectedDate
            End If

            If txtHI_SAL_OLD.Value <> 0 AndAlso txtHI_SAL_OLD.Value IsNot Nothing Then
                LoadHiMonneyBHYT()
            End If
        Else
            txtO_FROM.SelectedDate = Nothing
            txtO_TO.SelectedDate = Nothing
            txtO_HI_COM.Value = 0
            txtO_HI_EMP.Value = 0
        End If
    End Sub

    Private Sub ddlINS_ARISING_TYPE_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlINS_ARISING_TYPE_ID.SelectedIndexChanged
        Call LoadArisingManual()

        If (New InsuranceBusiness.InsuranceBusinessClient).CHECK_INS_ORG_ID(ddlINS_ARISING_TYPE_ID.SelectedValue) = False Then
            cboHI_RATE_ARREARS.Enabled = True
            rdHI_DECLARED_DATE.Enabled = True
        Else
            txtO_FROM.SelectedDate = Nothing
            txtO_TO.SelectedDate = Nothing
            txtO_HI_COM.Text = "0"
            txtO_HI_EMP.Text = "0"
            cboHI_RATE_ARREARS.Text = ""
            cboHI_RATE_ARREARS.SelectedIndex = -1
            cboHI_RATE_ARREARS.Enabled = False
            rdHI_DECLARED_DATE.Enabled = False
        End If
    End Sub

    Private Sub LoadHiMonneyBHYT()
        Dim dtData As DataTable
        Dim hiComPP As Decimal
        Dim hiEmpPP As Decimal
        Dim result As DataRow

        result = InsCompList.Select(String.Format("ID = {0}", cboHI_RATE_ARREARS.SelectedValue)).FirstOrDefault()

        dtData = (New InsuranceBusiness.InsuranceBusinessClient).GET_BHYT_PP(txtEFFECTIVE_DATE.SelectedDate)
        hiComPP = dtData.Rows(0)("HI_COM")
        hiEmpPP = dtData.Rows(0)("HI_EMP")

        If result("CODE") = "4_5NV" Then
            txtO_HI_COM.Value = 0
            txtO_HI_EMP.Value = ((hiComPP + hiEmpPP) / 100) * txtHI_SAL_OLD.Value * ((txtO_TO.SelectedDate.Value.Year - txtO_FROM.SelectedDate.Value.Year) * 12 + txtO_TO.SelectedDate.Value.Month - txtO_FROM.SelectedDate.Value.Month + 1)
        End If

        If result("CODE") = "4_5CTY" Then
            txtO_HI_COM.Value = ((hiComPP + hiEmpPP) / 100) * txtHI_SAL_OLD.Value * ((txtO_TO.SelectedDate.Value.Year - txtO_FROM.SelectedDate.Value.Year) * 12 + txtO_TO.SelectedDate.Value.Month - txtO_FROM.SelectedDate.Value.Month + 1)
            txtO_HI_EMP.Value = 0
        End If

        If result("CODE") = "1_5NV3CTY" Then
            txtO_HI_COM.Value = hiComPP * txtHI_SAL_OLD.Value * ((txtO_TO.SelectedDate.Value.Year - txtO_FROM.SelectedDate.Value.Year) * 12 + txtO_TO.SelectedDate.Value.Month - txtO_FROM.SelectedDate.Value.Month + 1) / 100
            txtO_HI_EMP.Value = hiEmpPP * txtHI_SAL_OLD.Value * ((txtO_TO.SelectedDate.Value.Year - txtO_FROM.SelectedDate.Value.Year) * 12 + txtO_TO.SelectedDate.Value.Month - txtO_FROM.SelectedDate.Value.Month + 1) / 100
        End If


    End Sub

    Private Sub LoadArisingManual()
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).InsAraisingAuto(Common.Common.GetUsername(), InsCommon.getNumber(txtEMPID.Text), InsCommon.getNumber(ddlINS_ARISING_TYPE_ID.SelectedValue), txtARISING_FROM_MONTH.SelectedDate, txtDECLARE_DATE.SelectedDate, txtEFFECTIVE_DATE.SelectedDate, InsCommon.getNumber(IIf(chkIs_Foreign.Checked, -1, 0)))
            Arising_Type = lstSource.Rows(0)("ARISING_TYPE")
            InsCommon.SetNumber(txtSALARY_PRE_PERIOD, lstSource.Rows(0)("SALARY_PRE_PERIOD"))
            InsCommon.SetNumber(txtSALARY_NOW_PERIOD, lstSource.Rows(0)("SALARY_NOW_PERIOD"))


            InsCommon.SetNumber(txtSI_SAL_OLD, lstSource.Rows(0)("SI_SAL_OLD"))
            InsCommon.SetNumber(txtSI_SAL_NEW, lstSource.Rows(0)("SI_SAL"))

            InsCommon.SetNumber(txtHI_SAL_OLD, lstSource.Rows(0)("HI_SAL_OLD"))
            InsCommon.SetNumber(txtHI_SAL_NEW, lstSource.Rows(0)("HI_SAL"))

            InsCommon.SetNumber(txtUI_SAL_OLD, lstSource.Rows(0)("UI_SAL_OLD"))
            InsCommon.SetNumber(txtUI_SAL_NEW, lstSource.Rows(0)("UI_SAL"))

            InsCommon.SetNumber(rdBHTNLD_BNN_OLD, lstSource.Rows(0)("TNLD_BNN_SAL_OLD"))
            InsCommon.SetNumber(rdBHTNLD_BNN_NEW, lstSource.Rows(0)("TNLD_BNN_SAL"))

            'InsCommon.SetNumber(rdSALARY_HD, lstSource.Rows(0)("SALARY_HD"))
            ' InsCommon.SetNumber(rdSALARY_PC, lstSource.Rows(0)("SALARY_PC"))

            'ThanhNT added 26/07/2016
            '[2:39:36 PM] Luyen Nguyen Pham Bao: giảm => không cho nhập mức lương kỳ này
            '[2:39:47 PM] Luyen Nguyen Pham Bao: Tăng => không cho nhập mức lương kỳ trước
            '[2:39:57 PM] Luyen Nguyen Pham Bao: điều chỉnh: 2 mức lương phải khác nhau
            If Arising_Type = 1 Then 'TANG
                txtSALARY_PRE_PERIOD.ReadOnly = True
                txtSALARY_NOW_PERIOD.ReadOnly = False

                txtSI_SAL_OLD.ReadOnly = True
                txtSI_SAL_NEW.ReadOnly = False
                txtHI_SAL_OLD.ReadOnly = True
                txtHI_SAL_NEW.ReadOnly = False
                txtUI_SAL_OLD.ReadOnly = True
                txtUI_SAL_NEW.ReadOnly = False
                rdBHTNLD_BNN_OLD.ReadOnly = True
                rdBHTNLD_BNN_NEW.ReadOnly = False


            ElseIf lstSource.Rows(0)("ARISING_TYPE") = 2 Then 'giam
                txtSALARY_NOW_PERIOD.ReadOnly = True
                txtSALARY_PRE_PERIOD.ReadOnly = False

                txtSI_SAL_OLD.ReadOnly = False
                txtSI_SAL_NEW.ReadOnly = True
                txtHI_SAL_OLD.ReadOnly = False
                txtHI_SAL_NEW.ReadOnly = True
                txtUI_SAL_OLD.ReadOnly = False
                txtUI_SAL_NEW.ReadOnly = True
                rdBHTNLD_BNN_OLD.ReadOnly = False
                rdBHTNLD_BNN_NEW.ReadOnly = True
            Else
                txtSALARY_PRE_PERIOD.ReadOnly = False
                txtSALARY_NOW_PERIOD.ReadOnly = False

                txtSI_SAL_OLD.ReadOnly = False
                txtSI_SAL_NEW.ReadOnly = False
                txtHI_SAL_OLD.ReadOnly = False
                txtHI_SAL_NEW.ReadOnly = False
                txtUI_SAL_OLD.ReadOnly = False
                txtUI_SAL_NEW.ReadOnly = False
                rdBHTNLD_BNN_OLD.ReadOnly = False
                rdBHTNLD_BNN_NEW.ReadOnly = False
            End If
            InsCommon.SetDate(txtA_FROM, lstSource.Rows(0)("A_FROM"))
            InsCommon.SetDate(txtA_TO, lstSource.Rows(0)("A_TO"))

            InsCommon.SetDate(txtR_FROM, lstSource.Rows(0)("R_FROM"))
            InsCommon.SetDate(txtR_TO, lstSource.Rows(0)("R_TO"))

            If chkSI.Checked Then
                InsCommon.SetNumber(txtA_SI, lstSource.Rows(0)("A_SI"))
                InsCommon.SetNumber(txtR_SI, lstSource.Rows(0)("R_SI"))
            Else
                txtA_SI.Value = Nothing
                txtR_SI.Value = Nothing

                txtSI_SAL_OLD.Value = Nothing
                txtSI_SAL_NEW.Value = Nothing

            End If

            If chkHI.Checked Then
                InsCommon.SetNumber(txtA_HI, lstSource.Rows(0)("A_HI"))
                InsCommon.SetNumber(txtR_HI, lstSource.Rows(0)("R_HI"))
            Else
                txtA_HI.Value = Nothing
                txtR_HI.Value = Nothing
                txtHI_SAL_OLD.Value = Nothing
                txtHI_SAL_NEW.Value = Nothing

            End If

            If chkUI.Checked Then
                InsCommon.SetNumber(txtA_UI, lstSource.Rows(0)("A_UI"))
                InsCommon.SetNumber(txtR_UI, lstSource.Rows(0)("R_UI"))
            Else
                txtA_UI.Value = Nothing
                txtR_UI.Value = Nothing

                txtUI_SAL_OLD.Value = Nothing
                txtUI_SAL_NEW.Value = Nothing

            End If

            If chkTNLD_BNN.Checked Then
                InsCommon.SetNumber(rdA_BHTNLD_BNN, lstSource.Rows(0)("A_TNLD_BNN"))
                InsCommon.SetNumber(rdR_BHTNLD_BNN, lstSource.Rows(0)("R_TNLD_BNN"))
            Else
                rdA_BHTNLD_BNN.Value = Nothing
                rdR_BHTNLD_BNN.Value = Nothing

                rdBHTNLD_BNN_OLD.Value = Nothing
                rdBHTNLD_BNN_NEW.Value = Nothing
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillData(ByVal idSelected As Decimal)
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsArisingManual(Common.Common.GetUsername(), InsCommon.getNumber(txtID.Text) _
                                                                                                                                        , 0, 0 _
                                                                                                                                        , Nothing _
                                                                                                                                        , Nothing _
                                                                                                                                        , 0, 0)

            If lstSource IsNot Nothing AndAlso lstSource.Rows.Count > 0 Then
                Arising_Type = lstSource.Rows(0)("ARISING_TYPE")
                If Arising_Type = 1 Then 'TANG
                    txtSALARY_PRE_PERIOD.ReadOnly = True
                    txtSALARY_NOW_PERIOD.ReadOnly = False
                ElseIf lstSource.Rows(0)("ARISING_TYPE") = 2 Then 'giam
                    txtSALARY_NOW_PERIOD.ReadOnly = True
                    txtSALARY_PRE_PERIOD.ReadOnly = False
                Else
                    txtSALARY_PRE_PERIOD.ReadOnly = False
                    txtSALARY_NOW_PERIOD.ReadOnly = False
                End If
                InsCommon.SetString(txtEMPID, lstSource.Rows(0)("EMPID"))

                InsCommon.SetString(txtEMPLOYEE_ID, lstSource.Rows(0)("EMPLOYEE_CODE"))
                InsCommon.SetString(txtFULLNAME, lstSource.Rows(0)("FULL_NAME"))
                InsCommon.SetString(txtDEP, lstSource.Rows(0)("DEP_NAME"))
                InsCommon.SetDate(txtDoB, lstSource.Rows(0)("BIRTH_DATE"))
                'InsCommon.SetString(txtBirthPlace, lstSource.Rows(0)("ID_PLACE_NAME"))
                InsCommon.SetString(BIRTH_PLACE, lstSource.Rows(0)("PLACE_OF_BIRTH_NAME"))
                InsCommon.SetString(txtCMND, lstSource.Rows(0)("ID_NO"))
                InsCommon.SetDate(txtDateIssue, lstSource.Rows(0)("ID_DATE"))
                InsCommon.SetString(txtPOSITION, lstSource.Rows(0)("POSITION_NAME"))

                'InsCommon.SetString(SOCIAL_NUMBER, lstSource.Rows(0)("SOCIAL_NUMBER"))

                InsCommon.SetNumber(chkSI, lstSource.Rows(0)("SI"))
                InsCommon.SetNumber(chkHI, lstSource.Rows(0)("HI"))
                InsCommon.SetNumber(chkUI, lstSource.Rows(0)("UI"))
                InsCommon.SetNumber(chkTNLD_BNN, lstSource.Rows(0)("BHTNLD_BNN"))
                InsCommon.SetNumber(chkIs_Foreign, lstSource.Rows(0)("FOREIGN"))

                InsCommon.SetNumber(ddlINS_ORG_ID, lstSource.Rows(0)("INS_ORG_ID"))
                InsCommon.SetNumber(ddlINS_ARISING_TYPE_ID, lstSource.Rows(0)("INS_ARISING_TYPE_ID"))
                InsCommon.SetNumber(txtSALARY_PRE_PERIOD, lstSource.Rows(0)("SALARY_PRE_PERIOD"))
                InsCommon.SetNumber(txtSALARY_NOW_PERIOD, lstSource.Rows(0)("SALARY_NOW_PERIOD"))


                InsCommon.SetDate(txtEFFECTIVE_DATE, lstSource.Rows(0)("EFFECTIVE_DATE"))
                InsCommon.SetDate(txtEXPRIE_DATE, lstSource.Rows(0)("EXPRIE_DATE"))
                InsCommon.SetDate(txtDECLARE_DATE, lstSource.Rows(0)("DECLARE_DATE"))
                InsCommon.SetDate(txtARISING_FROM_MONTH, lstSource.Rows(0)("ARISING_FROM_MONTH"))
                InsCommon.SetDate(txtARISING_TO_MONTH, lstSource.Rows(0)("ARISING_TO_MONTH"))
                InsCommon.SetDate(txtHEALTH_RETURN_DATE, lstSource.Rows(0)("HEALTH_RETURN_DATE"))

                InsCommon.SetString(txtNOTE, lstSource.Rows(0)("NOTE"))

                InsCommon.SetDate(txtR_FROM, lstSource.Rows(0)("R_FROM"))
                InsCommon.SetDate(txtR_TO, lstSource.Rows(0)("R_TO"))
                InsCommon.SetNumber(txtR_SI, lstSource.Rows(0)("R_SI"))
                InsCommon.SetNumber(txtR_HI, lstSource.Rows(0)("R_HI"))
                InsCommon.SetNumber(txtR_UI, lstSource.Rows(0)("R_UI"))

                InsCommon.SetDate(txtA_FROM, lstSource.Rows(0)("A_FROM"))
                InsCommon.SetDate(txtA_TO, lstSource.Rows(0)("A_TO"))
                InsCommon.SetNumber(txtA_SI, lstSource.Rows(0)("A_SI"))
                InsCommon.SetNumber(txtA_HI, lstSource.Rows(0)("A_HI"))
                InsCommon.SetNumber(txtA_UI, lstSource.Rows(0)("A_UI"))

                InsCommon.SetDate(txtO_FROM, lstSource.Rows(0)("O_FROM"))
                InsCommon.SetDate(txtO_TO, lstSource.Rows(0)("O_TO"))
                InsCommon.SetNumber(txtO_HI_COM, lstSource.Rows(0)("O_HI_COM"))
                InsCommon.SetNumber(txtO_HI_EMP, lstSource.Rows(0)("O_HI_EMP"))
                InsCommon.SetDate(rdHI_DECLARED_DATE, lstSource.Rows(0)("HI_DECLARED_DATE"))
                InsCommon.SetNumber(cboHI_RATE_ARREARS, lstSource.Rows(0)("HI_RATE_ARREARS"))

                If (New InsuranceBusiness.InsuranceBusinessClient).CHECK_INS_ORG_ID(ddlINS_ARISING_TYPE_ID.SelectedValue) Then
                    txtO_FROM.SelectedDate = Nothing
                    txtO_TO.SelectedDate = Nothing
                    txtO_HI_COM.Text = "0"
                    txtO_HI_EMP.Text = "0"
                    cboHI_RATE_ARREARS.Text = ""
                    cboHI_RATE_ARREARS.SelectedIndex = -1
                    cboHI_RATE_ARREARS.Enabled = False
                    rdHI_DECLARED_DATE.Enabled = False
                End If

                InsCommon.SetNumber(txtSI_SAL_OLD, lstSource.Rows(0)("SI_SAL_OLD"))
                InsCommon.SetNumber(txtSI_SAL_NEW, lstSource.Rows(0)("SI_SAL"))

                InsCommon.SetNumber(txtHI_SAL_OLD, lstSource.Rows(0)("HI_SAL_OLD"))
                InsCommon.SetNumber(txtHI_SAL_NEW, lstSource.Rows(0)("HI_SAL"))

                InsCommon.SetNumber(txtUI_SAL_OLD, lstSource.Rows(0)("UI_SAL_OLD"))
                InsCommon.SetNumber(txtUI_SAL_NEW, lstSource.Rows(0)("UI_SAL"))

                InsCommon.SetNumber(rdBHTNLD_BNN_OLD, lstSource.Rows(0)("BHTNLD_BNN_SAL_OLD"))
                InsCommon.SetNumber(rdBHTNLD_BNN_NEW, lstSource.Rows(0)("BHTNLD_BNN_SAL"))

                InsCommon.SetNumber(rdA_BHTNLD_BNN, lstSource.Rows(0)("A_TNLD_BNN"))

                InsCommon.SetNumber(rdR_BHTNLD_BNN, lstSource.Rows(0)("R_TNLD_BNN"))

                InsCommon.SetNumber(rdSALARY_HD, lstSource.Rows(0)("SALARY_HD"))
                InsCommon.SetNumber(rdSALARY_PC, lstSource.Rows(0)("SALARY_PC"))

            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub txtR_FROM_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtR_FROM.SelectedDateChanged
        Call LoadArisingManual2("0")
    End Sub

    Private Sub txtR_TO_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtR_TO.SelectedDateChanged
        Call LoadArisingManual2("0")
    End Sub

    Private Sub txtA_FROM_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtA_FROM.SelectedDateChanged
        Call LoadArisingManual2("1")
    End Sub

    Private Sub txtA_TO_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtA_TO.SelectedDateChanged
        Call LoadArisingManual2("1")
    End Sub

    'ThanhNT added 05/01/2016
    Private Function CheckArisingType(ByVal arisingTypeID As Decimal) As Boolean
        Try
            Dim rs = (New InsuranceBusiness.InsuranceBusinessClient).Check_Arising_Type(arisingTypeID)
            If rs Is Nothing Then
                ShowMessage("Đã xảy ra lỗi trong quá trình kiểm tra loại biến động", NotifyType.Warning)
                Exit Function
            Else
                If Decimal.Parse(rs(0)(0).ToString()) = 0 Then 'giam
                    Return True
                Else 'tang
                    Return False
                End If
            End If
        Catch ex As Exception

        End Try

    End Function

    Private Sub LoadArisingManual2(Optional ByVal truythu As String = "1")
        If CurrentState = CommonMessage.STATE_NEW Or CurrentState = CommonMessage.STATE_EDIT Then
            Try
                Dim lstSource As DataTable
                If truythu = "1" Then
                    lstSource = (New InsuranceBusiness.InsuranceBusinessClient).InsAraisingAuto2(Common.Common.GetUsername(), InsCommon.getNumber(txtEMPID.Text), InsCommon.getNumber(ddlINS_ARISING_TYPE_ID.SelectedValue), txtA_FROM.SelectedDate, txtA_TO.SelectedDate, txtHEALTH_RETURN_DATE.SelectedDate, truythu, InsCommon.getNumber(IIf(chkIs_Foreign.Checked, -1, 0)))
                    If chkSI.Checked Then
                        InsCommon.SetNumber(txtA_SI, lstSource.Rows(0)("A_SI"))
                        'InsCommon.SetNumber(txtR_SI, lstSource.Rows(0)("R_SI"))
                    Else
                        txtA_SI.Value = Nothing
                        'txtR_SI.Value = Nothing
                    End If

                    If chkHI.Checked Then
                        InsCommon.SetNumber(txtA_HI, lstSource.Rows(0)("A_HI"))
                        'InsCommon.SetNumber(txtR_HI, lstSource.Rows(0)("R_HI"))
                    Else
                        txtA_HI.Value = Nothing
                        'txtR_HI.Value = Nothing
                    End If

                    If chkUI.Checked Then
                        InsCommon.SetNumber(txtA_UI, lstSource.Rows(0)("A_UI"))
                        'InsCommon.SetNumber(txtR_UI, lstSource.Rows(0)("R_UI"))
                    Else
                        txtA_UI.Value = Nothing
                        'txtR_UI.Value = Nothing
                    End If

                Else
                    lstSource = (New InsuranceBusiness.InsuranceBusinessClient).InsAraisingAuto2(Common.Common.GetUsername(), InsCommon.getNumber(txtEMPID.Text), InsCommon.getNumber(ddlINS_ARISING_TYPE_ID.SelectedValue), txtR_FROM.SelectedDate, txtR_TO.SelectedDate, txtHEALTH_RETURN_DATE.SelectedDate, truythu, InsCommon.getNumber(IIf(chkIs_Foreign.Checked, -1, 0)))

                    If chkSI.Checked Then
                        'InsCommon.SetNumber(txtA_SI, lstSource.Rows(0)("A_SI"))
                        InsCommon.SetNumber(txtR_SI, lstSource.Rows(0)("R_SI"))
                    Else
                        'txtA_SI.Value = Nothing
                        txtR_SI.Value = Nothing
                    End If

                    If chkHI.Checked Then
                        'InsCommon.SetNumber(txtA_HI, lstSource.Rows(0)("A_HI"))
                        InsCommon.SetNumber(txtR_HI, lstSource.Rows(0)("R_HI"))
                    Else
                        'txtA_HI.Value = Nothing
                        txtR_HI.Value = Nothing
                    End If

                    If chkUI.Checked Then
                        'InsCommon.SetNumber(txtA_UI, lstSource.Rows(0)("A_UI"))
                        InsCommon.SetNumber(txtR_UI, lstSource.Rows(0)("R_UI"))
                    Else
                        'txtA_UI.Value = Nothing
                        txtR_UI.Value = Nothing
                    End If
                End If
            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then

                If Request.Params("Status") IsNot Nothing Then
                    Status = Decimal.Parse(Request.Params("Status"))
                    If Status = 1 Then 'Edit -> FillData
                        If Request.Params("IDSelect") IsNot Nothing Then
                            txtID.Text = Decimal.Parse(Request.Params("IDSelect"))
                            txtEMPID.Text = Decimal.Parse(Request.Params("EmployeeID"))
                            CurrentState = CommonMessage.STATE_EDIT
                            FillData(txtID.Text)
                        End If
                    ElseIf Status = 0 Then 'New
                        txtID.Text = "0"
                        txtEMPID.Text = "0"
                        CurrentState = CommonMessage.STATE_NEW
                        ResetForm()
                        If Request.Params("EmployeeID") IsNot Nothing Then
                            FillDataEmp(Decimal.Parse(Request.Params("EmployeeID")))
                        End If
                    Else 'View
                        If Request.Params("IDSelect") IsNot Nothing Then
                            txtID.Text = Decimal.Parse(Request.Params("IDSelect"))
                            txtEMPID.Text = Decimal.Parse(Request.Params("EmployeeID"))
                            CurrentState = CommonMessage.STATE_NORMAL
                            FillData(txtID.Text)
                        End If
                    End If
                End If
                UpdateControlState(CurrentState)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetValue_Find_Emp(ByVal Emp_ID As Decimal)
        Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetEmpInfo(Emp_ID, 0)
        If (Not (lstSource Is Nothing) AndAlso lstSource.Rows.Count > 0) Then
            If getSE_CASE_CONFIG("ctrlInsArisingManual_case1") > 0 Then

            Else
                If lstSource.Rows(0)("CHECK_EMP_INFOR").ToString() = "" Then 'nhân viên đó không tham gia bảo hiểm
                    ShowMessage("Nhân viên đã chọn không có thông tin bảo hiểm. Vui lòng chọn khác", NotifyType.Warning, 10)
                    Exit Sub
                End If
            End If

            txtEMPLOYEE_ID.Text = lstSource.Rows(0)("EMPLOYEE_ID")
            InsCommon.SetString(txtFULLNAME, lstSource.Rows(0)("VN_FULLNAME"))
            InsCommon.SetString(txtDEP, lstSource.Rows(0)("ORG_NAME"))
            InsCommon.SetDate(txtDoB, lstSource.Rows(0)("BIRTH_DATE"))

            'InsCommon.SetString(txtBirthPlace, lstSource.Rows(0)("ID_PLACE_NAME"))
            InsCommon.SetString(txtCMND, lstSource.Rows(0)("ID_NO"))

            InsCommon.SetDate(txtDateIssue, lstSource.Rows(0)("ID_DATE"))
            InsCommon.SetString(txtPOSITION, lstSource.Rows(0)("POSITION_NAME"))
            'txtINSORG.Text = lstSource.Rows(0)("INS_ORG_NAME")

            txtEMPID.Text = lstSource.Rows(0)("EMPID")
            'InsCommon.SetString(SOCIAL_NUMBER, lstSource.Rows(0)("SOCIAL_NUMBER"))
            InsCommon.SetString(BIRTH_PLACE, lstSource.Rows(0)("PLACE_OF_BIRTH_NAME"))

            InsCommon.SetNumber(ddlINS_ORG_ID, lstSource.Rows(0)("INS_ORG_ID"))

            chkSI.Checked = True
            chkHI.Checked = True
            chkUI.Checked = True
            chkTNLD_BNN.Checked = True
        End If
    End Sub
    Private Sub Reset_Find_Emp()


        InsCommon.SetString(txtFULLNAME, DBNull.Value)
        InsCommon.SetString(txtDEP, DBNull.Value)
        InsCommon.SetDate(txtDoB, DBNull.Value)

        'InsCommon.SetString(txtBirthPlace, DBNull.Value)
        InsCommon.SetString(txtCMND, DBNull.Value)

        InsCommon.SetDate(txtDateIssue, DBNull.Value)
        InsCommon.SetString(txtPOSITION, DBNull.Value)

        txtEMPID.Text = ""
        'InsCommon.SetString(SOCIAL_NUMBER, DBNull.Value)
        InsCommon.SetString(BIRTH_PLACE, DBNull.Value)

        InsCommon.SetNumber(ddlINS_ORG_ID, DBNull.Value)

        chkSI.Checked = False
        chkHI.Checked = False
        chkUI.Checked = False
        chkTNLD_BNN.Checked = False
    End Sub
#End Region

#Region "FindEmployeeButton"

    Private Sub ctrlFindEmployeePopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of Common.CommonBusiness.EmployeePopupFindDTO)
        Try
            Dim a As Object = ctrlFindEmployeePopup.SelectedEmployee
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of Common.CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetEmpInfo(item.EMPLOYEE_ID, 0)
                If (Not (lstSource Is Nothing) AndAlso lstSource.Rows.Count > 0) Then
                    If getSE_CASE_CONFIG("ctrlInsArisingManual_case1") > 0 Then

                    Else
                        If lstSource.Rows(0)("CHECK_EMP_INFOR").ToString() = "" Then 'nhân viên đó không tham gia bảo hiểm
                            btnSearchEmp.Focus()
                            ShowMessage("Nhân viên đã chọn không có thông tin bảo hiểm. Vui lòng chọn khác", NotifyType.Warning, 10)
                            Exit Sub
                        End If
                    End If

                    txtEMPLOYEE_ID.Text = lstSource.Rows(0)("EMPLOYEE_ID")
                    InsCommon.SetString(txtFULLNAME, lstSource.Rows(0)("VN_FULLNAME"))
                    InsCommon.SetString(txtDEP, lstSource.Rows(0)("ORG_NAME"))
                    InsCommon.SetDate(txtDoB, lstSource.Rows(0)("BIRTH_DATE"))

                    'InsCommon.SetString(txtBirthPlace, lstSource.Rows(0)("ID_PLACE_NAME"))
                    InsCommon.SetString(txtCMND, lstSource.Rows(0)("ID_NO"))

                    InsCommon.SetDate(txtDateIssue, lstSource.Rows(0)("ID_DATE"))
                    InsCommon.SetString(txtPOSITION, lstSource.Rows(0)("POSITION_NAME"))
                    'txtINSORG.Text = lstSource.Rows(0)("INS_ORG_NAME")

                    txtEMPID.Text = lstSource.Rows(0)("EMPID")
                    'InsCommon.SetString(SOCIAL_NUMBER, lstSource.Rows(0)("SOCIAL_NUMBER"))
                    InsCommon.SetString(BIRTH_PLACE, lstSource.Rows(0)("PLACE_OF_BIRTH_NAME"))

                    InsCommon.SetNumber(ddlINS_ORG_ID, lstSource.Rows(0)("INS_ORG_ID"))

                    chkSI.Checked = True
                    chkHI.Checked = True
                    chkUI.Checked = True
                    chkTNLD_BNN.Checked = True
                End If
            End If
            isLoadPopup = 0
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnSearchEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchEmp.Click
        Try
            isLoadPopup = 1

            ShowPopupEmployee()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ShowPopupEmployee()
        Try
            If FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                FindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If

            If isLoadPopup = 1 Then
                ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                'ctrlFindEmployeePopup.MustHaveTerminate = True
                ctrlFindEmployeePopup.MustHaveContract = False
                ctrlFindEmployeePopup.MultiSelect = False
                FindEmployee.Controls.Add(ctrlFindEmployeePopup)

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Sub FillDataEmp(ByVal empID As Decimal)
        Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetEmpInfo(empID, 0)
        If (Not (lstSource Is Nothing) AndAlso lstSource.Rows.Count > 0) Then
            If lstSource.Rows(0)("CHECK_EMP_INFOR").ToString() = "" Then 'nhân viên đó không tham gia bảo hiểm
                btnSearchEmp.Focus()
                ShowMessage("Nhân viên đã chọn không có thông tin bảo hiểm. Vui lòng chọn khác", NotifyType.Warning, 10)
                Exit Sub
            End If
            txtEMPLOYEE_ID.Text = lstSource.Rows(0)("EMPLOYEE_ID")
            InsCommon.SetString(txtFULLNAME, lstSource.Rows(0)("VN_FULLNAME"))
            InsCommon.SetString(txtDEP, lstSource.Rows(0)("ORG_NAME"))
            InsCommon.SetDate(txtDoB, lstSource.Rows(0)("BIRTH_DATE"))

            'InsCommon.SetString(txtBirthPlace, lstSource.Rows(0)("ID_PLACE_NAME"))
            InsCommon.SetString(txtCMND, lstSource.Rows(0)("ID_NO"))

            InsCommon.SetString(BIRTH_PLACE, lstSource.Rows(0)("PLACE_OF_BIRTH_NAME"))

            InsCommon.SetDate(txtDateIssue, lstSource.Rows(0)("ID_DATE"))
            InsCommon.SetString(txtPOSITION, lstSource.Rows(0)("POSITION_NAME"))
            'txtINSORG.Text = lstSource.Rows(0)("INS_ORG_NAME")

            txtEMPID.Text = lstSource.Rows(0)("EMPID")

            InsCommon.SetNumber(ddlINS_ORG_ID, lstSource.Rows(0)("INS_ORG_ID"))

            chkSI.Checked = True
            chkHI.Checked = True
            chkUI.Checked = True
            chkTNLD_BNN.Checked = True
        End If
    End Sub
#End Region

End Class