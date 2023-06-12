Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI


Public Class ctrlInsRegimes
    Inherits Common.CommonView
    'Private WithEvents viewRegister As ctrlShiftPlanningRegister

    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlOrgSULPPopup As ctrlFindOrgPopup


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

#Region "Property & Variable"

    Private Property Status As Decimal
        Get
            Return ViewState(Me.ID & "_Status")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Status") = value
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

    Private Property RateCal As Decimal
        Get
            Return ViewState(Me.ID & "_RateCal")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_RateCal") = value
        End Set
    End Property

    Private Property LoaiCal As Decimal
        Get
            Return ViewState(Me.ID & "_LoaiCal")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_LoaiCal") = value
        End Set
    End Property

    'Private Property ListSign As List(Of SIGN_DTO)
    '    Get
    '        Return PageViewState(Me.ID & "_ListSign")
    '    End Get
    '    Set(ByVal value As List(Of SIGN_DTO))
    '        PageViewState(Me.ID & "_ListSign") = value
    '    End Set
    'End Property

    Public ReadOnly Property PageId As String
        Get
            Return Me.ID
        End Get
    End Property

    'Private Property ListPeriod As List(Of PERIOD_DTO)
    '    Get
    '        Return ViewState(Me.ID & "_ListPeriod")
    '    End Get
    '    Set(ByVal value As List(Of PERIOD_DTO))
    '        ViewState(Me.ID & "_ListPeriod") = value
    '    End Set
    'End Property

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

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar,
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

                    ddlREGIME_ID.Enabled = False
                    txtFROM_DATE.Enabled = False
                    txtTO_DATE.Enabled = False
                    txtDAY_CALCULATOR.Enabled = False
                    txtBORN_DATE.Enabled = False
                    txtNAME_CHILDREN.Enabled = False
                    txtCHILDREN_NO.Enabled = False
                    txtACCUMULATE_DAY.Enabled = False
                    txtSUBSIDY_SALARY.Enabled = False
                    txtSUBSIDY_MODIFY.Enabled = False

                    txtSUBSIDY.Enabled = False
                    'txtMONEY_ADVANCE.Enabled = False
                    txtPAYROLL_DATE.Enabled = False
                    txtDECLARE_DATE.Enabled = False
                    txtCONDITION.Enabled = False
                    txtINS_PAY_AMOUNT.Enabled = False
                    txtPAY_APPROVE_DATE.Enabled = False
                    txtAPPROV_DAY_NUM.Enabled = False
                    txtNOTE.Enabled = False
                    txtRegimes.Enabled = False

                    txtDoB.Enabled = False
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    btnSearchEmp.Enabled = True

                    ddlREGIME_ID.Enabled = True
                    txtFROM_DATE.Enabled = True
                    txtTO_DATE.Enabled = True
                    txtDAY_CALCULATOR.Enabled = True
                    txtBORN_DATE.Enabled = True
                    txtNAME_CHILDREN.Enabled = True
                    txtCHILDREN_NO.Enabled = True
                    txtACCUMULATE_DAY.Enabled = True
                    txtSUBSIDY_SALARY.Enabled = True

                    txtSUBSIDY_MODIFY.Enabled = True
                    'txtMONEY_ADVANCE.Enabled = True
                    txtSUBSIDY.Enabled = True
                    txtPAYROLL_DATE.Enabled = True
                    txtDECLARE_DATE.Enabled = True
                    txtCONDITION.Enabled = True
                    txtINS_PAY_AMOUNT.Enabled = True
                    txtPAY_APPROVE_DATE.Enabled = True
                    txtAPPROV_DAY_NUM.Enabled = True
                    txtNOTE.Enabled = True

                    txtDoB.Enabled = False
                    txtRegimes.Enabled = True
                Case CommonMessage.STATE_DELETE

                Case "Nothing"
            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub UpdateToolbarState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True

                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False

                    CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = True

                Case CommonMessage.STATE_NEW
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False

                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True

                    CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = False


                Case CommonMessage.STATE_EDIT
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False

                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True

                    CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = False

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
                    UpdateControlState(CommonMessage.STATE_NORMAL)
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

            txtEMPLOYEE_ID.Text = ""
            txtFULLNAME.Text = ""
            txtDEP.Text = ""
            txtDoB.SelectedDate = Nothing
            txtBirthPlace.Text = ""
            txtPOSITION.Text = ""
            txtEMPID.Text = ""

            txtEMPID.Text = "0"

            txtSOCIAL_NUMBER.Text = ""
            txtHEALTH_NUMBER.Text = ""

            ddlREGIME_ID.SelectedIndex = -1
            txtFROM_DATE.SelectedDate = Nothing
            txtTO_DATE.SelectedDate = Nothing
            txtDAY_CALCULATOR.Text = ""
            txtBORN_DATE.SelectedDate = Nothing
            txtNAME_CHILDREN.Text = ""
            txtCHILDREN_NO.Text = ""
            txtACCUMULATE_DAY.Value = Nothing
            txtCurrDate.Text = "0"
            txtSUBSIDY_SALARY.Text = "0"
            txtSUBSIDY_MODIFY.Text = "0"
            'txtMONEY_ADVANCE.Text = "0"
            txtSUBSIDY.Text = "0"
            txtPAYROLL_DATE.SelectedDate = Nothing
            txtDECLARE_DATE.SelectedDate = Nothing
            txtCONDITION.Text = ""
            txtINS_PAY_AMOUNT.Text = ""
            txtPAY_APPROVE_DATE.SelectedDate = Nothing
            txtAPPROV_DAY_NUM.Text = ""
            txtNOTE.Text = ""
            txtRegimes.Value = Nothing

        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    If rep.UpdateInsRegimes(Common.Common.GetUsername(), InsCommon.getNumber(txtID.Text) _
                                                                        , InsCommon.getNumber(txtEMPID.Text) _
                                                                        , InsCommon.getNumber(ddlREGIME_ID.SelectedValue) _
                                                                        , 0 _
                                                                        , (txtFROM_DATE.SelectedDate) _
                                                                        , (txtTO_DATE.SelectedDate) _
                                                                        , InsCommon.getNumber(txtDAY_CALCULATOR.Text) _
                                                                        , (txtBORN_DATE.SelectedDate) _
                                                                        , txtNAME_CHILDREN.Text _
                                                                        , InsCommon.getNumber(txtCHILDREN_NO.Text) _
                                                                        , InsCommon.getNumber(txtACCUMULATE_DAY.Text) _
                                                                        , InsCommon.getNumber(txtSUBSIDY_SALARY.Text) _
                                                                        , InsCommon.getNumber(txtSUBSIDY.Text) _
                                                                        , InsCommon.getNumber(0) _
                                                                        , (txtPAYROLL_DATE.SelectedDate) _
                                                                        , txtDECLARE_DATE.SelectedDate _
                                                                        , txtCONDITION.Text _
                                                                        , InsCommon.getNumber(txtINS_PAY_AMOUNT.Text) _
                                                                        , (txtPAY_APPROVE_DATE.SelectedDate) _
                                                                        , InsCommon.getNumber(txtAPPROV_DAY_NUM.Text) _
                                                                        , txtNOTE.Text _
                                                                        , InsCommon.getNumber(txtSUBSIDY_MODIFY.Text) _
                                                                        , InsCommon.getNumber(txtOFF_TOGETHER.Text) _
                                                                        , InsCommon.getNumber(txtOFF_IN_HOUSE.Text) _
                                                                        , InsCommon.getNumber(txtRegimes.Value), _
                                                                         InsCommon.getNumber(cboFormAllow.SelectedValue)) Then
                        Refresh("InsertView")
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
                Case CommonMessage.STATE_EDIT
                    If rep.UpdateInsRegimes(Common.Common.GetUsername(), InsCommon.getNumber(txtID.Text) _
                                                                        , InsCommon.getNumber(txtEMPID.Text) _
                                                                        , InsCommon.getNumber(ddlREGIME_ID.SelectedValue) _
                                                                        , 0 _
                                                                        , (txtFROM_DATE.SelectedDate) _
                                                                        , (txtTO_DATE.SelectedDate) _
                                                                        , InsCommon.getNumber(txtDAY_CALCULATOR.Text) _
                                                                        , (txtBORN_DATE.SelectedDate) _
                                                                        , txtNAME_CHILDREN.Text _
                                                                        , InsCommon.getNumber(txtCHILDREN_NO.Text) _
                                                                        , InsCommon.getNumber(txtACCUMULATE_DAY.Text) _
                                                                        , InsCommon.getNumber(txtSUBSIDY_SALARY.Text) _
                                                                        , InsCommon.getNumber(txtSUBSIDY.Text) _
                                                                        , InsCommon.getNumber(0) _
                                                                        , (txtPAYROLL_DATE.SelectedDate) _
                                                                        , txtDECLARE_DATE.SelectedDate _
                                                                        , txtCONDITION.Text _
                                                                        , InsCommon.getNumber(txtINS_PAY_AMOUNT.Text) _
                                                                        , (txtPAY_APPROVE_DATE.SelectedDate) _
                                                                        , InsCommon.getNumber(txtAPPROV_DAY_NUM.Text) _
                                                                        , txtNOTE.Text _
                                                                        , InsCommon.getNumber(txtSUBSIDY_MODIFY.Text) _
                                                                        , InsCommon.getNumber(txtOFF_TOGETHER.Text) _
                                                                        , InsCommon.getNumber(txtOFF_IN_HOUSE.Text) _
                                                                        , InsCommon.getNumber(txtRegimes.Value), _
                                                                         InsCommon.getNumber(cboFormAllow.SelectedValue)) Then
                        Refresh("UpdateView")
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetDataCombo()
        Dim rep As New InsuranceRepository
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsListEntitledType()
            FillRadCombobox(ddlREGIME_ID, lstSource, "NAME_VN", "ID", False)

            Dim dtData = rep.GetOtherList("HTNTC")
            FillRadCombobox(cboFormAllow, dtData, "NAME", "ID", False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtFROM_DATE_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtFROM_DATE.SelectedDateChanged
        Try
            LoadAutoData()
            ValidateDayCalculate()
            LoadSalaryRegimes()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtCHILDREN_NO_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCHILDREN_NO.TextChanged
        Try
            LoadSalaryRegimes()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtDECLARE_DATE_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtDECLARE_DATE.SelectedDateChanged
        Try
            LoadSalaryRegimes()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub LoadSalaryRegimes()
        If txtCHILDREN_NO.Text <> "" AndAlso txtDECLARE_DATE.SelectedDate IsNot Nothing Then
            Dim dt As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GET_MLTTC(txtDECLARE_DATE.SelectedDate)

            txtSUBSIDY.Value = Int(txtCHILDREN_NO.Value * Decimal.Parse(dt(0)("VALUE")) + txtRegimes.Value)
            txtSUBSIDY_MODIFY.Value = txtSUBSIDY.Value
        End If
    End Sub

    Private Sub txtTO_DATE_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtTO_DATE.SelectedDateChanged
        Try
            LoadAutoData()
            ValidateDayCalculate()
            LoadSalaryRegimes()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlREGIME_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlREGIME_ID.SelectedIndexChanged
        Try
            LoadAutoData()
            ValidateDayCalculate()
            LoadSalaryRegimes()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadAutoData(Optional ByVal dayCal As Decimal = -1)
        Try
            If CurrentState = CommonMessage.STATE_NEW Or CurrentState = CommonMessage.STATE_EDIT Then
                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsRegimes_cal(InsCommon.getNumber(txtEMPID.Text), InsCommon.getNumber(ddlREGIME_ID.SelectedValue), txtFROM_DATE.SelectedDate, txtTO_DATE.SelectedDate, dayCal)
                If Not (lstSource Is Nothing) AndAlso lstSource.Rows.Count > 0 Then
                    InsCommon.SetNumber(txtDAY_CALCULATOR, lstSource.Rows(0)("DAY_CALCULATOR")) 'Số ngày tính
                    If CurrentState = CommonMessage.STATE_EDIT Then
                        txtACCUMULATE_DAY.Text = lstSource.Rows(0)("ACCUMULATE_DAY") - InsCommon.getNumber2(txtCurrDate.Text) 'Số ngày lũy kế
                    Else
                        InsCommon.SetNumber(txtACCUMULATE_DAY, lstSource.Rows(0)("ACCUMULATE_DAY")) 'Số ngày lũy kế
                    End If
                    InsCommon.SetNumber(txtSUBSIDY_SALARY, lstSource.Rows(0)("SUBSIDY_SALARY")) 'Tiền lương trợ cấp
                    'InsCommon.SetNumber(txtSUBSIDY, lstSource.Rows(0)("SUBSIDY")) 'Tiền trợ cấp
                    'InsCommon.SetNumber(txtSUBSIDY_MODIFY, lstSource.Rows(0)("SUBSIDY")) 'Tiền trợ cấp chỉnh sửa

                    LoaiCal = Integer.Parse(lstSource.Rows(0)("LOAI_CAL"))
                    RateCal = Decimal.Parse(lstSource.Rows(0)("RATE_CAL"))
                    If LoaiCal = 1 Then
                        txtRegimes.Value = If(txtSUBSIDY_SALARY.Value Is Nothing, 0, txtSUBSIDY_SALARY.Value) * (RateCal / 100) * txtDAY_CALCULATOR.Value / 24
                    Else
                        Dim month As Decimal
                        Dim du As Double
                        If txtDAY_CALCULATOR.Value IsNot Nothing Then
                            Month = Int(txtDAY_CALCULATOR.Value / 30)
                            du = txtDAY_CALCULATOR.Value Mod 30.0
                        End If

                        txtRegimes.Value = If(txtSUBSIDY_SALARY.Value Is Nothing, 0, txtSUBSIDY_SALARY.Value) * (RateCal / 100) * month + If(txtSUBSIDY_SALARY.Value Is Nothing, 0, txtSUBSIDY_SALARY.Value) * (RateCal / 100) * du / 24
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillData(ByVal idSelected As Decimal)
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsRegimes(Common.Common.GetUsername(), InsCommon.getNumber(txtID.Text) _
                                                                                                                                    , 0 _
                                                                                                                                    , 0 _
                                                                                                                                    , Nothing _
                                                                                                                                    , 0 _
                                                                                                                                    , Nothing _
                                                                                                                                    , Nothing _
                                                                                                                                    , 0)

            If (lstSource IsNot Nothing AndAlso lstSource.Rows.Count > 0) Then
                txtEMPID.Text = InsCommon.getString(lstSource.Rows(0)("EMPID"))
                txtEMPLOYEE_ID.Text = InsCommon.getString(lstSource.Rows(0)("EMPLOYEE_CODE"))
                txtFULLNAME.Text = InsCommon.getString(lstSource.Rows(0)("FULL_NAME"))
                txtDEP.Text = InsCommon.getString(lstSource.Rows(0)("DEP_NAME"))

                txtSOCIAL_NUMBER.Text = InsCommon.getString(lstSource.Rows(0)("SOCIAL_NUMBER"))
                txtHEALTH_NUMBER.Text = InsCommon.getString(lstSource.Rows(0)("HEALTH_NUMBER"))
                'txtDEP.Text = InsCommon.getString(lstSource.Rows(0)("ID"))
                InsCommon.SetDate(txtDoB, lstSource.Rows(0)("BIRTH_DATE"))

                If IsNumeric(lstSource(0)("FORM_ALLOW")) AndAlso lstSource(0)("FORM_ALLOW") > 0 Then
                    cboFormAllow.SelectedValue = lstSource(0)("FORM_ALLOW")
                End If

                txtBirthPlace.Text = InsCommon.getString(lstSource.Rows(0)("PLACE_OF_BIRTH_NAME"))
                'txtPOSITION.Text = InsCommon.getString(lstSource.Rows(0)("ID"))

                txtPOSITION.Text = InsCommon.getString(lstSource.Rows(0)("POSITION_NAME"))

                ' dic.Add("SUBSIDY_AMOUNT", txtSUBSIDY)
                InsCommon.SetNumber(ddlREGIME_ID, lstSource.Rows(0)("REGIME_ID"))

                InsCommon.SetDate(txtFROM_DATE, lstSource.Rows(0)("FROM_DATE"))
                InsCommon.SetDate(txtTO_DATE, lstSource.Rows(0)("TO_DATE"))


                InsCommon.SetDate(txtBORN_DATE, lstSource.Rows(0)("BORN_DATE"))
                InsCommon.SetNumber(txtCHILDREN_NO, lstSource.Rows(0)("CHILDREN_NO"))

                txtNAME_CHILDREN.Text = InsCommon.getString(lstSource.Rows(0)("NAME_CHILDREN"))

                InsCommon.SetNumber(txtACCUMULATE_DAY, lstSource.Rows(0)("ACCUMULATE_DAY"))

                InsCommon.SetNumber(txtDAY_CALCULATOR, lstSource.Rows(0)("DAY_CALCULATOR"))
                InsCommon.SetNumber(txtCurrDate, lstSource.Rows(0)("DAY_CALCULATOR"))

                InsCommon.SetNumber(txtSUBSIDY_SALARY, lstSource.Rows(0)("SUBSIDY_SALARY"))
                InsCommon.SetNumber(txtSUBSIDY, lstSource.Rows(0)("SUBSIDY"))
                InsCommon.SetNumber(txtSUBSIDY_MODIFY, lstSource.Rows(0)("SUBSIDY_MODIFY"))

                InsCommon.SetNumber(txtRegimes, lstSource.Rows(0)("REGIMES_SAL"))


                InsCommon.SetDate(txtPAYROLL_DATE, lstSource.Rows(0)("PAYROLL_DATE"))
                InsCommon.SetDate(txtDECLARE_DATE, lstSource.Rows(0)("DECLARE_DATE"))

                txtCONDITION.Text = InsCommon.getString(lstSource.Rows(0)("CONDITION"))

                InsCommon.SetNumber(txtINS_PAY_AMOUNT, lstSource.Rows(0)("INS_PAY_AMOUNT"))

                InsCommon.SetDate(txtPAY_APPROVE_DATE, lstSource.Rows(0)("PAY_APPROVE_DATE"))
                InsCommon.SetNumber(txtAPPROV_DAY_NUM, lstSource.Rows(0)("APPROV_DAY_NUM"))

                txtNOTE.Text = InsCommon.getString(lstSource.Rows(0)("NOTE"))
                'InsCommon.SetNumber(txtMONEY_ADVANCE, lstSource.Rows(0)("MONEY_ADVANCE"))

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
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

    Private Sub cusDayCalculator_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusDayCalculator.ServerValidate
        Try
            ValidateDayCalculate()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ValidateDayCalculate()
        'check sổ ngày có vượt quá tổng số ngày được hưởng theo loại hưởng chế độ đó
        If txtDAY_CALCULATOR.Text.ToString() = "" OrElse ddlREGIME_ID.SelectedValue = "" Then
            cusDayCalculator.IsValid = True
            Exit Sub
        End If
        Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).CheckDayCalculate(ddlREGIME_ID.SelectedValue, txtDAY_CALCULATOR.Text)
        If lstSource.Rows(0)(0) = 1 Then
            cusDayCalculator.IsValid = True
        Else
            cusDayCalculator.IsValid = False
        End If
    End Sub

#End Region

#Region "FindEmployeeButton"

    Private Sub ctrlFindEmployeePopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of Common.CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New AttendanceRepository
        Try
            Dim a As Object = ctrlFindEmployeePopup.SelectedEmployee
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of Common.CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetEmpInfo(item.EMPLOYEE_ID, 0)
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
                    InsCommon.SetString(txtBirthPlace, lstSource.Rows(0)("PLACE_OF_BIRTH_NAME"))
                    'txtCMND.Text = lstSource.Rows(0)("ID_NO")
                    'txtDateIssue.Text = lstSource.Rows(0)("ID_DATE")
                    InsCommon.SetString(txtPOSITION, lstSource.Rows(0)("POSITION_NAME"))
                    'txtINSORG.Text = lstSource.Rows(0)("INS_ORG_NAME")
                    txtEMPID.Text = lstSource.Rows(0)("EMPID")

                    InsCommon.SetString(txtSOCIAL_NUMBER, lstSource.Rows(0)("SOCIAL_NUMBER"))
                    InsCommon.SetString(txtHEALTH_NUMBER, lstSource.Rows(0)("HEALTH_NUMBER"))

                End If

                'hidEmployeeID.Value = item.ID.ToString
                'txtEmplloyeeCode.Text = item.EMPLOYEE_ID
                'txtEmployeeName.Text = item.VN_FULLNAME
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
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
#End Region
    Private Sub GetValue_Find_Emp(ByVal Emp_ID As Decimal)
        Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetEmpInfo(Emp_ID, 0)
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
            InsCommon.SetString(txtBirthPlace, lstSource.Rows(0)("PLACE_OF_BIRTH_NAME"))
            'txtCMND.Text = lstSource.Rows(0)("ID_NO")
            'txtDateIssue.Text = lstSource.Rows(0)("ID_DATE")
            InsCommon.SetString(txtPOSITION, lstSource.Rows(0)("POSITION_NAME"))
            'txtINSORG.Text = lstSource.Rows(0)("INS_ORG_NAME")
            txtEMPID.Text = lstSource.Rows(0)("EMPID")

            InsCommon.SetString(txtSOCIAL_NUMBER, lstSource.Rows(0)("SOCIAL_NUMBER"))
            InsCommon.SetString(txtHEALTH_NUMBER, lstSource.Rows(0)("HEALTH_NUMBER"))
        End If
    End Sub
    Private Sub Reset_Find_Emp()


        InsCommon.SetString(txtFULLNAME, DBNull.Value)
        InsCommon.SetString(txtDEP, DBNull.Value)

        InsCommon.SetDate(txtDoB, DBNull.Value)
        InsCommon.SetString(txtBirthPlace, DBNull.Value)
        'txtCMND.Text = lstSource.Rows(0)("ID_NO")
        'txtDateIssue.Text = lstSource.Rows(0)("ID_DATE")
        InsCommon.SetString(txtPOSITION, DBNull.Value)
        'txtINSORG.Text = lstSource.Rows(0)("INS_ORG_NAME")
        txtEMPID.Text = ""

        InsCommon.SetString(txtSOCIAL_NUMBER, DBNull.Value)
        InsCommon.SetString(txtHEALTH_NUMBER, DBNull.Value)
    End Sub

    Private Sub txtDAY_CALCULATOR_TextChanged(sender As Object, e As System.EventArgs) Handles txtDAY_CALCULATOR.TextChanged
        LoadAutoData(txtDAY_CALCULATOR.Value)
        ValidateDayCalculate()
        LoadSalaryRegimes()
    End Sub
End Class