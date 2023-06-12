Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlPortalFamily_Edit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Public Property EmployeeID As Decimal
    Property checkFK_Pkey As Decimal
        Get
            Return ViewState(Me.ID & "_checkFK_Pkey")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_checkFK_Pkey") = value
        End Set
    End Property
    Property FAMILY_DS As List(Of FamilyEditDTO)
        Get
            Return ViewState(Me.ID & "_FAMILY_DS")
        End Get
        Set(ByVal value As List(Of FamilyEditDTO))
            ViewState(Me.ID & "_FAMILY_DS") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            rgFamily.SetFilter()
            rgFamilyEdit.SetFilter()
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
        'If Not IsPostBack Then
        '    ViewConfig(RadPane2)
        '    GirdConfig(rgFamily)
        '    GirdConfig(rgFamilyEdit)
        'End If
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Seperator,
                         ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Seperator,
                         ToolbarItem.Submit)
            CType(Me.MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CommonConfig.ModuleID = CommonBusiness.SystemConfigModuleID.iPortal
                If HttpContext.Current.Session("ConfigDictionaryCache4") IsNot Nothing Then
                    Session.Remove("ConfigDictionaryCache4")
                End If
                CurrentState = CommonMessage.STATE_NORMAL
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim ComboBoxDataDTO As New ComboBoxDataDTO
            Dim dtData As DataTable
            Using rep As New ProfileRepository
                ComboBoxDataDTO.GET_RELATION = True
                ComboBoxDataDTO.GET_PROVINCE = True
                ComboBoxDataDTO.GET_DISTRICT = True
                ComboBoxDataDTO.GET_NATION = True
                ComboBoxDataDTO.GET_WARD = True
                rep.GetComboList(ComboBoxDataDTO)
                If ComboBoxDataDTO IsNot Nothing Then
                    FillDropDownList(cboRelationship, ComboBoxDataDTO.LIST_RELATION, "NAME", "ID", Common.Common.SystemLanguage, True, cboRelationship.SelectedValue)
                    FillDropDownList(cboNguyenQuan, ComboBoxDataDTO.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNguyenQuan.SelectedValue)
                    FillDropDownList(cbPROVINCE_ID, ComboBoxDataDTO.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cbPROVINCE_ID.SelectedValue)
                    FillDropDownList(cbTempPROVINCE_ID, ComboBoxDataDTO.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cbTempPROVINCE_ID.SelectedValue)
                    FillDropDownList(cbDISTRICT_ID, ComboBoxDataDTO.LIST_DISTRICT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cbDISTRICT_ID.SelectedValue)
                    FillDropDownList(cbTempDISTRICT_ID, ComboBoxDataDTO.LIST_DISTRICT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cbTempDISTRICT_ID.SelectedValue)
                    FillDropDownList(cbWARD_ID, ComboBoxDataDTO.LIST_WARD, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cbWARD_ID.SelectedValue)
                    FillDropDownList(cbTempWARD_ID, ComboBoxDataDTO.LIST_WARD, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cbTempWARD_ID.SelectedValue)
                    FillDropDownList(cbTempKtPROVINCE_ID, ComboBoxDataDTO.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cbTempKtPROVINCE_ID.SelectedValue)

                    FillDropDownList(cboNationlity, ComboBoxDataDTO.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNationlity.SelectedValue)
                    FillDropDownList(cboNATIONALITYFAMILY, ComboBoxDataDTO.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNATIONALITYFAMILY.SelectedValue)

                    cboNationlity.SelectedValue = 244
                    cboNATIONALITYFAMILY.SelectedValue = 244
                End If

                If Not IsPostBack Then
                    cboGender.DataSource = rep.GetOtherList("GENDER", True)
                    cboGender.DataTextField = "NAME"
                    cboGender.DataValueField = "ID"
                    cboGender.DataBind()
                End If

            End Using

            'Dim dic As New Dictionary(Of String, Control)
            'dic.Add("ADDRESS", txtAdress)
            'dic.Add("ID_NO", txtIDNO)
            'dic.Add("FULLNAME", txtFullName)
            'dic.Add("TAXTATION", txtTax)
            'dic.Add("REMARK", txtRemark)
            'dic.Add("BIRTH_DATE", rdBirthDate)
            'dic.Add("IS_DEDUCT", chkIsDeduct)
            'dic.Add("DEDUCT_REG", rdDeductReg)
            'dic.Add("DEDUCT_FROM", rdDeductFrom)
            'dic.Add("DEDUCT_TO", rdDeductTo)
            'dic.Add("RELATION_ID", cboRelationship)
            'dic.Add("ID", hidFamilyID)
            'dic.Add("CAREER", txtCareer)
            'dic.Add("TITLE_NAME", txtTitle)
            'dic.Add("PROVINCE_ID", cboNguyenQuan)

            'dic.Add("NATION_ID", cboNationlity)
            'dic.Add("ID_NO_DATE", rdIDDate)
            'dic.Add("ID_NO_PLACE_NAME", txtIDPlace)
            'dic.Add("PHONE", txtPhone)
            'dic.Add("TAXTATION_DATE", rdMSTDate)
            'dic.Add("TAXTATION_PLACE", txt_MSTPLACE)
            'dic.Add("BIRTH_CODE", txtBIRTH_CODE)
            'dic.Add("QUYEN", txtQuyen)
            'dic.Add("BIRTH_NATION_ID", cboNATIONALITYFAMILY)
            'dic.Add("BIRTH_PROVINCE_ID", cbTempKtPROVINCE_ID)
            'dic.Add("BIRTH_DISTRICT_ID", cbTempDISTRICT_ID)
            'dic.Add("BIRTH_WARD_ID", cbTempKtWARD_ID)

            'Utilities.OnClientRowSelectedChanged(rgFamily, dic)

            'Dim dic1 As New Dictionary(Of String, Control)
            'dic1.Add("ADDRESS", txtAdress)
            'dic1.Add("ID_NO", txtIDNO)
            'dic1.Add("FULLNAME", txtFullName)
            'dic1.Add("TAXTATION", txtTax)
            'dic1.Add("REMARK", txtRemark)
            'dic1.Add("BIRTH_DATE", rdBirthDate)
            'dic1.Add("IS_DEDUCT", chkIsDeduct)
            'dic1.Add("DEDUCT_REG", rdDeductReg)
            'dic1.Add("DEDUCT_FROM", rdDeductFrom)
            'dic1.Add("DEDUCT_TO", rdDeductTo)
            'dic1.Add("RELATION_ID", cboRelationship)
            'dic1.Add("ID", hidID)
            'dic1.Add("FK_PKEY", hidFamilyID)
            'dic1.Add("CAREER", txtCareer)
            'dic1.Add("TITLE_NAME", txtTitle)
            'dic1.Add("PROVINCE_ID", cboNguyenQuan)

            'dic1.Add("NATION_ID", cboNationlity)
            'dic1.Add("ID_NO_DATE", rdIDDate)
            'dic1.Add("ID_NO_PLACE_NAME", txtIDPlace)
            'dic1.Add("PHONE", txtPhone)
            'dic1.Add("TAXTATION_DATE", rdMSTDate)
            'dic1.Add("TAXTATION_PLACE", txt_MSTPLACE)
            'dic1.Add("BIRTH_CODE", txtBIRTH_CODE)
            'dic1.Add("QUYEN", txtQuyen)
            'dic1.Add("BIRTH_NATION_ID", cboNATIONALITYFAMILY)
            'dic1.Add("BIRTH_PROVINCE_ID", cbTempKtPROVINCE_ID)
            'dic1.Add("BIRTH_DISTRICT_ID", cbTempDISTRICT_ID)
            'dic1.Add("BIRTH_WARD_ID", cbTempKtWARD_ID)
            'Utilities.OnClientRowSelectedChanged(rgFamilyEdit, dic1)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try

            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    EnableControlAll(False, txtAdress, txtFullName, txtIDNO, txtRemark, txtTax, cboGender, rdIDDate, txtIDPlace, cboNationlity, txtPhone, rdMSTDate, txtBIRTH_CODE, cboNATIONALITYFAMILY, cbTempKtPROVINCE_ID, cbTempKtDISTRICT_ID, cbTempKtWARD_ID,
                                     rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo, txtAD_Village, txtQuyen, txt_MSTPLACE,
                                     chkIsDeduct, cboRelationship, cboNguyenQuan, txtCareer, txtTitle,
                                     chkIs_Owner, chkIs_Pass, txtHouseCertificate_Code, txtHouseCertificate_Num,
                                     cbPROVINCE_ID, cbDISTRICT_ID, cbWARD_ID, txtAdress, txtTempAdress, cbTempPROVINCE_ID, cbTempDISTRICT_ID, cbTempWARD_ID, chkis_same_company)

                    If Not chkIsDeduct.Checked Then
                        chkIsDeduct_CheckedChanged(Nothing, Nothing)
                    End If
                    chkIsDeduct.AutoPostBack = False
                    EnabledGridNotPostback(rgFamily, True)
                    EnabledGridNotPostback(rgFamilyEdit, True)
                Case CommonMessage.STATE_NEW
                    EnableControlAll(True, txtAdress, txtFullName, txtIDNO, txtRemark, txtTax, cboGender, rdIDDate, txtIDPlace, cboNationlity, txtPhone, rdMSTDate, txtBIRTH_CODE, cboNATIONALITYFAMILY, cbTempKtPROVINCE_ID, cbTempKtDISTRICT_ID, cbTempKtWARD_ID,
                                     rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo, txtAD_Village, txtQuyen,
                                     chkIsDeduct, cboRelationship, cboNguyenQuan, txtCareer, txtTitle,
                                     chkIs_Owner, chkIs_Pass, txt_MSTPLACE,
                                     cbPROVINCE_ID, cbDISTRICT_ID, cbWARD_ID, txtAdress, txtTempAdress, cbTempPROVINCE_ID, cbTempDISTRICT_ID, cbTempWARD_ID, chkis_same_company)

                    If Not chkIsDeduct.Checked Then
                        chkIsDeduct_CheckedChanged(Nothing, Nothing)
                    End If
                    chkIsDeduct.AutoPostBack = True
                    EnabledGridNotPostback(rgFamily, False)
                    EnabledGridNotPostback(rgFamilyEdit, False)
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, txtAdress, txtFullName, txtIDNO, txtRemark, txtTax, cboGender, rdIDDate, txtIDPlace, cboNationlity, txtPhone, rdMSTDate, txtBIRTH_CODE, cboNATIONALITYFAMILY, cbTempKtPROVINCE_ID, cbTempKtDISTRICT_ID, cbTempKtWARD_ID,
                                     rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo, txtAD_Village, txtQuyen,
                                     chkIsDeduct, cboRelationship, cboNguyenQuan, txtCareer, txtTitle, txt_MSTPLACE,
                                     chkIs_Owner, chkIs_Pass, txtHouseCertificate_Code, txtHouseCertificate_Num,
                                     cbPROVINCE_ID, cbDISTRICT_ID, cbWARD_ID, txtAdress, txtTempAdress, cbTempPROVINCE_ID, cbTempDISTRICT_ID, cbTempWARD_ID, chkis_same_company)

                    If Not chkIsDeduct.Checked Then
                        chkIsDeduct_CheckedChanged(Nothing, Nothing)
                    End If
                    chkIsDeduct.AutoPostBack = True
                    EnabledGridNotPostback(rgFamily, False)
                    EnabledGridNotPostback(rgFamilyEdit, False)
            End Select

            ChangeToolbarState()
            If Not CType(CommonConfig.dicConfig("PORTAL_ALLOW_CHANGE"), Boolean) Then
                CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                CType(MainToolBar.Items(2), RadToolBarButton).Enabled = False
                CType(MainToolBar.Items(5), RadToolBarButton).Enabled = False
            Else
                CType(MainToolBar.Items(0), RadToolBarButton).Enabled = True
                CType(MainToolBar.Items(2), RadToolBarButton).Enabled = True
                CType(MainToolBar.Items(5), RadToolBarButton).Enabled = True
            End If
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtAdress, txtTempAdress, txtAD_Village, txtHouseCertificate_Code, txtHouseCertificate_Num,
                                      chkIs_Owner, chkIs_Pass, txtFullName, txtIDNO, txtRemark, txtTax, txt_MSTPLACE,
                                      rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo, txtCareer, txtTitle,
                                      chkIsDeduct, hidFamilyID, hidID, cboRelationship, cboNguyenQuan,
                                      cbPROVINCE_ID, cbDISTRICT_ID, cbWARD_ID, cbTempPROVINCE_ID, cbTempDISTRICT_ID, cbTempWARD_ID, chkis_same_company)
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim isInsert As Boolean = True

                        Dim obj As New FamilyEditDTO
                        obj.EMPLOYEE_ID = EmployeeID
                        obj.BIRTH_DATE = rdBirthDate.SelectedDate
                        obj.DEDUCT_REG = rdDeductReg.SelectedDate
                        obj.ADDRESS = txtAdress.Text.Trim()
                        obj.TAXTATION = txtTax.Text
                        obj.DEDUCT_FROM = rdDeductFrom.SelectedDate()
                        obj.DEDUCT_TO = rdDeductTo.SelectedDate()
                        obj.FULLNAME = txtFullName.Text.Trim()
                        obj.ID_NO = txtIDNO.Text.Trim()
                        obj.IS_DEDUCT = chkIsDeduct.Checked
                        obj.ADDRESS_TT = txtTempAdress.Text.Trim
                        obj.CERTIFICATE_NUM = txtHouseCertificate_Num.Text.Trim
                        obj.CERTIFICATE_CODE = txtHouseCertificate_Code.Text.Trim
                        If cbPROVINCE_ID.SelectedValue <> "" Then
                            obj.AD_PROVINCE_ID = Decimal.Parse(cbPROVINCE_ID.SelectedValue)
                        End If
                        If cbDISTRICT_ID.SelectedValue <> "" Then
                            obj.AD_DISTRICT_ID = Decimal.Parse(cbDISTRICT_ID.SelectedValue)
                        End If
                        If cbWARD_ID.SelectedValue <> "" Then
                            obj.AD_WARD_ID = Decimal.Parse(cbWARD_ID.SelectedValue)
                        End If
                        If cbTempPROVINCE_ID.SelectedValue <> "" Then
                            obj.TT_PROVINCE_ID = Decimal.Parse(cbTempPROVINCE_ID.SelectedValue)
                        End If
                        If cbTempDISTRICT_ID.SelectedValue <> "" Then
                            obj.TT_DISTRICT_ID = Decimal.Parse(cbTempDISTRICT_ID.SelectedValue)
                        End If
                        If cbTempWARD_ID.SelectedValue <> "" Then
                            obj.TT_WARD_ID = Decimal.Parse(cbTempWARD_ID.SelectedValue)
                        End If
                        obj.AD_VILLAGE = txtAD_Village.Text.Trim
                        obj.IS_OWNER = chkIs_Owner.Checked
                        obj.IS_PASS = chkIs_Pass.Checked
                        obj.REMARK = txtRemark.Text.Trim
                        If cboRelationship.SelectedValue <> "" Then
                            obj.RELATION_ID = Decimal.Parse(cboRelationship.SelectedValue)
                        End If
                        obj.CAREER = txtCareer.Text.Trim()
                        obj.TITLE_NAME = txtTitle.Text.Trim()
                        If cboNguyenQuan.SelectedValue <> "" Then
                            obj.PROVINCE_ID = Decimal.Parse(cboNguyenQuan.SelectedValue)
                        End If

                        If cboGender.SelectedValue <> "" Then
                            obj.GENDER = cboGender.SelectedValue
                        End If

                        If cboNationlity.SelectedValue <> "" Then
                            obj.NATION_ID = cboNationlity.SelectedValue
                        End If

                        obj.ID_NO_DATE = rdIDDate.SelectedDate
                        obj.ID_NO_PLACE_NAME = txtIDPlace.Text
                        obj.PHONE = txtPhone.Text
                        obj.TAXTATION_DATE = rdMSTDate.SelectedDate
                        obj.TAXTATION_PLACE = txt_MSTPLACE.Text
                        obj.BIRTH_CODE = txtBIRTH_CODE.Text
                        obj.QUYEN = txtQuyen.Text
                        obj.STATUS = "0"
                        obj.IS_SAME_COMPANY = chkis_same_company.Checked
                        If cboNATIONALITYFAMILY.SelectedValue <> "" Then
                            obj.BIRTH_NATION_ID = cboNATIONALITYFAMILY.SelectedValue
                        End If

                        If cbTempKtPROVINCE_ID.SelectedValue <> "" Then
                            obj.BIRTH_PROVINCE_ID = cbTempKtPROVINCE_ID.SelectedValue
                        End If

                        If cbTempKtDISTRICT_ID.SelectedValue <> "" Then
                            obj.BIRTH_DISTRICT_ID = cbTempKtDISTRICT_ID.SelectedValue
                        End If

                        If cbTempKtWARD_ID.SelectedValue <> "" Then
                            obj.BIRTH_WARD_ID = cbTempKtWARD_ID.SelectedValue
                        End If

                        Using rep As New ProfileBusinessRepository
                            If hidFamilyID.Value <> "" Then
                                obj.FK_PKEY = hidFamilyID.Value
                                Dim bCheck = rep.CheckExistFamilyEdit(hidFamilyID.Value)
                                If bCheck IsNot Nothing Then
                                    Dim status = bCheck.STATUS
                                    Dim pkey = bCheck.ID
                                    ' Trạng thái chờ phê duyệt
                                    If status = 1 Then
                                        ShowMessage("Thông đang đang Chờ phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                                        Exit Sub
                                    End If
                                    isInsert = False
                                    obj.ID = pkey
                                End If
                            End If
                            'checkFK_Pkey dùng để kiểm  tra nếu chỉnh sửa 'thông tin chỉnh sửa' thì sẽ không phát sinh fk_pkey
                            If checkFK_Pkey = 1 Then
                                obj.FK_PKEY = Nothing
                            End If

                            If hidID.Value <> "" Then
                                isInsert = False
                            End If

                            If isInsert Then
                                rep.InsertEmployeeFamilyEdit(obj, 0)
                            Else
                                obj.ID = hidID.Value
                                rep.ModifyEmployeeFamilyEdit(obj, 0)
                            End If

                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgFamily.Rebind()
                            rgFamilyEdit.Rebind()
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                        End Using
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL

                    If rgFamilyEdit.SelectedItems.Count > 0 Then
                        Dim item = CType(rgFamilyEdit.SelectedItems(rgFamilyEdit.SelectedItems.Count - 1), GridDataItem)
                        hidFamilyID.Value = item.GetDataKeyValue("ID")
                        txtAdress.Text = item.GetDataKeyValue("ADDRESS")
                        txtIDNO.Text = item.GetDataKeyValue("ID_NO")
                        txtFullName.Text = item.GetDataKeyValue("FULLNAME")
                        txtTax.Text = item.GetDataKeyValue("TAXTATION")
                        txtRemark.Text = item.GetDataKeyValue("REMARK")
                        rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
                        chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
                        rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
                        rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
                        rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
                        cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
                        txtCareer.Text = item.GetDataKeyValue("CAREER")
                        txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
                        cboNguyenQuan.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
                        txtHouseCertificate_Code.Text = item.GetDataKeyValue("CERTIFICATE_CODE")
                        txtHouseCertificate_Num.Text = item.GetDataKeyValue("CERTIFICATE_NUM")
                        txtTempAdress.Text = item.GetDataKeyValue("ADDRESS_TT")
                        cbPROVINCE_ID.SelectedValue = item.GetDataKeyValue("AD_PROVINCE_ID")
                        cbDISTRICT_ID.SelectedValue = item.GetDataKeyValue("AD_DISTRICT_ID")
                        cbWARD_ID.SelectedValue = item.GetDataKeyValue("AD_WARD_ID")
                        txtAD_Village.Text = item.GetDataKeyValue("AD_VILLAGE")
                        cbTempPROVINCE_ID.SelectedValue = item.GetDataKeyValue("TT_PROVINCE_ID")
                        cbTempDISTRICT_ID.SelectedValue = item.GetDataKeyValue("TT_DISTRICT_ID")
                        cbTempWARD_ID.SelectedValue = item.GetDataKeyValue("TT_WARD_ID")
                        chkIs_Owner.Checked = item.GetDataKeyValue("IS_OWNER")
                        chkIs_Pass.Checked = item.GetDataKeyValue("IS_PASS")
                        If item.GetDataKeyValue("FK_PKEY") IsNot Nothing Then
                            hidFamilyID.Value = item.GetDataKeyValue("FK_PKEY")
                        End If
                        hidID.Value = item.GetDataKeyValue("ID")
                        chkIsDeduct_CheckedChanged(Nothing, Nothing)



                        cboNationlity.SelectedValue = item.GetDataKeyValue("NATION_ID")
                        cboGender.SelectedValue = item.GetDataKeyValue("GENDER")
                        rdIDDate.SelectedDate = item.GetDataKeyValue("ID_NO_DATE")
                        txtIDPlace.Text = item.GetDataKeyValue("ID_NO_PLACE_NAME")
                        txtPhone.Text = item.GetDataKeyValue("PHONE")
                        rdMSTDate.SelectedDate = item.GetDataKeyValue("TAXTATION_DATE")
                        txt_MSTPLACE.Text = item.GetDataKeyValue("TAXTATION_PLACE")
                        txtBIRTH_CODE.Text = item.GetDataKeyValue("BIRTH_CODE")
                        txtQuyen.Text = item.GetDataKeyValue("QUYEN")
                        cboNATIONALITYFAMILY.SelectedValue = item.GetDataKeyValue("BIRTH_NATION_ID")
                        cbTempKtPROVINCE_ID.SelectedValue = item.GetDataKeyValue("BIRTH_PROVINCE_ID")
                        cbTempDISTRICT_ID.SelectedValue = item.GetDataKeyValue("BIRTH_DISTRICT_ID")
                        cbTempKtWARD_ID.SelectedValue = item.GetDataKeyValue("BIRTH_WARD_ID")
                        chkis_same_company.Checked = item.GetDataKeyValue("IS_SAME_COMPANY")

                    Else
                        ClearControlValue(txtAdress, txtTempAdress, txtAD_Village, txtHouseCertificate_Code, txtHouseCertificate_Num, cboGender, rdIDDate, txtIDPlace, cboNationlity, txtPhone, rdMSTDate, txtBIRTH_CODE, cboNATIONALITYFAMILY, cbTempKtPROVINCE_ID, cbTempKtDISTRICT_ID, cbTempKtWARD_ID, txtQuyen,
                                      chkIs_Owner, chkIs_Pass, txtFullName, txtIDNO, txtRemark, txtTax, txtQuyen,
                                      rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo, txtCareer, txtTitle,
                                      chkIsDeduct, hidFamilyID, hidID, cboRelationship, cboNguyenQuan, txt_MSTPLACE,
                                      cbPROVINCE_ID, cbDISTRICT_ID, cbWARD_ID, cbTempPROVINCE_ID, cbTempDISTRICT_ID, cbTempWARD_ID, chkis_same_company)
                    End If


                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_SUBMIT
                    If rgFamilyEdit.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim status As String = ""
                    For Each item As GridDataItem In rgFamilyEdit.SelectedItems
                        status = item.GetDataKeyValue("STATUS")
                        If status = 1 Then
                            ShowMessage("Thông tin đang Chờ phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                            Exit Sub
                        End If
                        If status = 2 Then
                            ShowMessage("Thông tin đang phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                            Exit Sub
                        End If
                        If status = 3 Then
                            ShowMessage("Thông tin đang không phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate("Thông tin đã gửi duyệt sẽ không được chỉnh sửa. Bạn chắc chắn muốn gửi duyệt?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()


            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgFamily_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles rgFamily.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                Dim item = CType(e.Item, GridDataItem)
                If Not CType(CommonConfig.dicConfig("PORTAL_ALLOW_CHANGE"), Boolean) Then
                    CType(item("EDIT_ROW").Controls(0), LinkButton).Enabled = False
                Else
                    CType(item("EDIT_ROW").Controls(0), LinkButton).Enabled = True
                End If
            End If

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgFamily_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgFamily.NeedDataSource
        Try
            SetValueObjectByRadGrid(rgFamily, New FamilyDTO)

            Using rep As New ProfileBusinessRepository
                rgFamily.DataSource = rep.GetEmployeeFamily(New FamilyDTO With {.EMPLOYEE_ID = EmployeeID})
            End Using
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgFamilyEdit_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles rgFamilyEdit.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                Dim item = CType(e.Item, GridDataItem)
                If Not CType(CommonConfig.dicConfig("PORTAL_ALLOW_CHANGE"), Boolean) Then
                    CType(item("EDIT_ROW").Controls(0), LinkButton).Enabled = False
                Else
                    CType(item("EDIT_ROW").Controls(0), LinkButton).Enabled = True
                End If
            End If

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgFamilyEdit_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgFamilyEdit.NeedDataSource
        Try
            SetValueObjectByRadGrid(rgFamilyEdit, New FamilyEditDTO)
            Using rep As New ProfileBusinessRepository
                rgFamilyEdit.DataSource = rep.GetEmployeeFamilyEdit(New FamilyEditDTO With {.EMPLOYEE_ID = EmployeeID})
            End Using
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgFamilyEdit_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgFamilyEdit.ItemCommand
        Try
            If e.CommandName = "EditRow" Then
                Dim item = CType(e.Item, GridDataItem)
                Dim status As String = ""
                If item.GetDataKeyValue("STATUS") IsNot Nothing Then
                    status = item.GetDataKeyValue("STATUS")
                End If
                Select Case status
                    Case 1
                        ShowMessage("Bản ghi đang Chờ phê duyệt chỉ được xem thông tin", NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case 2
                        ShowMessage("Bản ghi đã Phê duyệt chỉ được xem thông tin", NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case 3
                        ShowMessage("Bản ghi đã Không phê duyệt chỉ được xem thông tin", NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case Else
                        CurrentState = CommonMessage.STATE_EDIT
                End Select
                ClearControlValue(txtAdress, txtTempAdress, txtAD_Village, txtHouseCertificate_Code, txtHouseCertificate_Num, cboGender, rdIDDate, txtIDPlace, cboNationlity, txtPhone, rdMSTDate, txtBIRTH_CODE, cboNATIONALITYFAMILY, cbTempKtPROVINCE_ID, cbTempKtDISTRICT_ID, cbTempKtWARD_ID, txtQuyen,
                                                    chkIs_Owner, chkIs_Pass, txtFullName, txtIDNO, txtRemark, txtTax, txtQuyen,
                                                    rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo, txtCareer, txtTitle,
                                                    chkIsDeduct, hidFamilyID, hidID, cboRelationship, cboNguyenQuan, txt_MSTPLACE,
                                                    cbPROVINCE_ID, cbDISTRICT_ID, cbWARD_ID, cbTempPROVINCE_ID, cbTempDISTRICT_ID, cbTempWARD_ID, chkis_same_company)
                hidFamilyID.Value = item.GetDataKeyValue("ID")
                txtAdress.Text = item.GetDataKeyValue("ADDRESS")
                txtIDNO.Text = item.GetDataKeyValue("ID_NO")
                txtFullName.Text = item.GetDataKeyValue("FULLNAME")
                txtTax.Text = item.GetDataKeyValue("TAXTATION")
                txtRemark.Text = item.GetDataKeyValue("REMARK")
                rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
                chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
                rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
                rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
                rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
                cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
                txtCareer.Text = item.GetDataKeyValue("CAREER")
                txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
                cboNguyenQuan.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
                txtHouseCertificate_Code.Text = item.GetDataKeyValue("CERTIFICATE_CODE")
                txtHouseCertificate_Num.Text = item.GetDataKeyValue("CERTIFICATE_NUM")
                txtTempAdress.Text = item.GetDataKeyValue("ADDRESS_TT")
                txtAD_Village.Text = item.GetDataKeyValue("AD_VILLAGE")
                If IsNumeric(item.GetDataKeyValue("AD_PROVINCE_ID")) Then
                    cbPROVINCE_ID.SelectedValue = item.GetDataKeyValue("AD_PROVINCE_ID")
                End If
                If IsNumeric(item.GetDataKeyValue("TT_PROVINCE_ID")) Then
                    cbTempPROVINCE_ID.SelectedValue = item.GetDataKeyValue("TT_PROVINCE_ID")
                End If

                Using rep As New ProfileRepository

                    If cbPROVINCE_ID.SelectedValue <> "" Then
                        Dim dt As DataTable = rep.GetDistrictList(cbPROVINCE_ID.SelectedValue, False)
                        FillRadCombobox(cbDISTRICT_ID, dt, "NAME", "ID")
                    End If
                    If IsNumeric(item.GetDataKeyValue("AD_DISTRICT_ID")) Then
                        cbDISTRICT_ID.SelectedValue = item.GetDataKeyValue("AD_DISTRICT_ID")
                    End If
                    If cbDISTRICT_ID.SelectedValue <> "" Then
                        Dim dt As DataTable = rep.GetWardList(cbDISTRICT_ID.SelectedValue, False)
                        FillRadCombobox(cbWARD_ID, dt, "NAME", "ID")
                    End If
                    If IsNumeric(item.GetDataKeyValue("AD_WARD_ID")) Then
                        cbWARD_ID.SelectedValue = item.GetDataKeyValue("AD_WARD_ID")
                    End If
                    If cbTempPROVINCE_ID.SelectedValue <> "" Then
                        Dim dt As DataTable = rep.GetDistrictList(cbTempPROVINCE_ID.SelectedValue, False)
                        FillRadCombobox(cbTempDISTRICT_ID, dt, "NAME", "ID")
                    End If
                    If IsNumeric(item.GetDataKeyValue("TT_DISTRICT_ID")) Then
                        cbTempDISTRICT_ID.SelectedValue = item.GetDataKeyValue("TT_DISTRICT_ID")
                    End If
                    If cbTempDISTRICT_ID.SelectedValue <> "" Then
                        Dim dt As DataTable = rep.GetWardList(cbTempDISTRICT_ID.SelectedValue, False)
                        FillRadCombobox(cbTempWARD_ID, dt, "NAME", "ID")
                    End If
                    If IsNumeric(item.GetDataKeyValue("TT_WARD_ID")) Then
                        cbTempWARD_ID.SelectedValue = item.GetDataKeyValue("TT_WARD_ID")
                    End If

                    If IsNumeric(item.GetDataKeyValue("NATION_ID")) Then
                        cboNationlity.SelectedValue = item.GetDataKeyValue("NATION_ID")
                    End If

                    If IsNumeric(item.GetDataKeyValue("BIRTH_NATION_ID")) Then
                        cboNATIONALITYFAMILY.SelectedValue = item.GetDataKeyValue("BIRTH_NATION_ID")
                    End If

                    If IsNumeric(item.GetDataKeyValue("BIRTH_PROVINCE_ID")) Then
                        cbTempKtPROVINCE_ID.SelectedValue = item.GetDataKeyValue("BIRTH_PROVINCE_ID")
                    End If

                    If IsNumeric(item.GetDataKeyValue("BIRTH_PROVINCE_ID")) Then
                        Dim dt As DataTable = rep.GetDistrictList(cbTempKtPROVINCE_ID.SelectedValue, False)
                        FillRadCombobox(cbTempKtDISTRICT_ID, dt, "NAME", "ID")
                    End If

                    If IsNumeric(item.GetDataKeyValue("BIRTH_DISTRICT_ID")) Then
                        cbTempKtDISTRICT_ID.SelectedValue = item.GetDataKeyValue("BIRTH_DISTRICT_ID")
                    End If

                    If IsNumeric(item.GetDataKeyValue("BIRTH_WARD_ID")) Then
                        Dim dt As DataTable = rep.GetWardList(cbTempKtDISTRICT_ID.SelectedValue, False)
                        FillRadCombobox(cbTempKtWARD_ID, dt, "NAME", "ID")
                        cbTempKtWARD_ID.SelectedValue = item.GetDataKeyValue("BIRTH_WARD_ID")
                    End If

                    cboGender.SelectedValue = item.GetDataKeyValue("GENDER")

                    rdIDDate.SelectedDate = item.GetDataKeyValue("ID_NO_DATE")
                    txtIDPlace.Text = item.GetDataKeyValue("ID_NO_PLACE_NAME")
                    txtPhone.Text = item.GetDataKeyValue("PHONE")
                    rdMSTDate.SelectedDate = item.GetDataKeyValue("TAXTATION_DATE")
                    txt_MSTPLACE.Text = item.GetDataKeyValue("TAXTATION_PLACE")
                    txtBIRTH_CODE.Text = item.GetDataKeyValue("BIRTH_CODE")
                    txtQuyen.Text = item.GetDataKeyValue("QUYEN")

                End Using
                chkIs_Owner.Checked = item.GetDataKeyValue("IS_OWNER")
                chkIs_Pass.Checked = item.GetDataKeyValue("IS_PASS")
                chkis_same_company.Checked = item.GetDataKeyValue("IS_SAME_COMPANY")
                If item.GetDataKeyValue("FK_PKEY") IsNot Nothing Then
                    hidFamilyID.Value = item.GetDataKeyValue("FK_PKEY")
                    checkFK_Pkey = 0
                Else
                    checkFK_Pkey = 1
                End If
                hidID.Value = item.GetDataKeyValue("ID")

                chkIsDeduct_CheckedChanged(Nothing, Nothing)
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgFamily_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgFamily.ItemCommand
        Try
            If e.CommandName = "EditRow" Then
                Using rep As New ProfileBusinessRepository
                    FAMILY_DS = rep.GetEmployeeFamilyEdit(New FamilyEditDTO With {.EMPLOYEE_ID = EmployeeID})
                End Using
                Dim item1 = CType(e.Item, GridDataItem)
                For Each LINE In FAMILY_DS
                    If LINE.FK_PKEY = item1.GetDataKeyValue("ID") And (LINE.STATUS = 0 Or LINE.STATUS = 1) Then
                        ShowMessage(Translate("Đã có thông tin ở mục chỉnh sửa."), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                CurrentState = CommonMessage.STATE_EDIT
                ClearControlValue(txtAdress, txtTempAdress, txtAD_Village, txtHouseCertificate_Code, txtHouseCertificate_Num, cboGender, rdIDDate, txtIDPlace, cboNationlity, txtPhone, rdMSTDate, txtBIRTH_CODE, cboNATIONALITYFAMILY, cbTempKtPROVINCE_ID, cbTempKtDISTRICT_ID, cbTempKtWARD_ID, txtQuyen,
                                    chkIs_Owner, chkIs_Pass, txtFullName, txtIDNO, txtRemark, txtTax, txtQuyen,
                                    rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo, txtCareer, txtTitle,
                                    chkIsDeduct, hidFamilyID, hidID, cboRelationship, cboNguyenQuan, txt_MSTPLACE,
                                    cbPROVINCE_ID, cbDISTRICT_ID, cbWARD_ID, cbTempPROVINCE_ID, cbTempDISTRICT_ID, cbTempWARD_ID, chkis_same_company)
                Dim item = CType(e.Item, GridDataItem)
                hidFamilyID.Value = item.GetDataKeyValue("ID")
                txtAdress.Text = item.GetDataKeyValue("ADDRESS")
                txtIDNO.Text = item.GetDataKeyValue("ID_NO")
                txtFullName.Text = item.GetDataKeyValue("FULLNAME")
                txtTax.Text = item.GetDataKeyValue("TAXTATION")
                txtRemark.Text = item.GetDataKeyValue("REMARK")
                rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
                chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
                rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
                rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
                rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
                cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
                txtCareer.Text = item.GetDataKeyValue("CAREER")
                txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
                cboNguyenQuan.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
                txtHouseCertificate_Code.Text = item.GetDataKeyValue("CERTIFICATE_CODE")
                txtHouseCertificate_Num.Text = item.GetDataKeyValue("CERTIFICATE_NUM")
                txtTempAdress.Text = item.GetDataKeyValue("ADDRESS_TT")
                txtAD_Village.Text = item.GetDataKeyValue("AD_VILLAGE")
                If IsNumeric(item.GetDataKeyValue("AD_PROVINCE_ID")) Then
                    cbPROVINCE_ID.SelectedValue = item.GetDataKeyValue("AD_PROVINCE_ID")
                End If
                If IsNumeric(item.GetDataKeyValue("TT_PROVINCE_ID")) Then
                    cbTempPROVINCE_ID.SelectedValue = item.GetDataKeyValue("TT_PROVINCE_ID")
                End If
                Using rep As New ProfileRepository
                    If cbPROVINCE_ID.SelectedValue <> "" Then
                        Dim dt As DataTable = rep.GetDistrictList(cbPROVINCE_ID.SelectedValue, False)
                        FillRadCombobox(cbDISTRICT_ID, dt, "NAME", "ID")
                    End If
                    If IsNumeric(item.GetDataKeyValue("AD_DISTRICT_ID")) Then
                        cbDISTRICT_ID.SelectedValue = item.GetDataKeyValue("AD_DISTRICT_ID")
                    End If
                    If cbDISTRICT_ID.SelectedValue <> "" Then
                        Dim dt As DataTable = rep.GetWardList(cbDISTRICT_ID.SelectedValue, False)
                        FillRadCombobox(cbWARD_ID, dt, "NAME", "ID")
                    End If
                    If IsNumeric(item.GetDataKeyValue("AD_WARD_ID")) Then
                        cbWARD_ID.SelectedValue = item.GetDataKeyValue("AD_WARD_ID")
                    End If
                    If cbTempPROVINCE_ID.SelectedValue <> "" Then
                        Dim dt As DataTable = rep.GetDistrictList(cbTempPROVINCE_ID.SelectedValue, False)
                        FillRadCombobox(cbTempDISTRICT_ID, dt, "NAME", "ID")
                    End If
                    If IsNumeric(item.GetDataKeyValue("TT_DISTRICT_ID")) Then
                        cbTempDISTRICT_ID.SelectedValue = item.GetDataKeyValue("TT_DISTRICT_ID")
                    End If
                    If cbTempDISTRICT_ID.SelectedValue <> "" Then
                        Dim dt As DataTable = rep.GetWardList(cbTempDISTRICT_ID.SelectedValue, False)
                        FillRadCombobox(cbTempWARD_ID, dt, "NAME", "ID")
                    End If

                    If IsNumeric(item.GetDataKeyValue("TT_WARD_ID")) Then
                        cbTempWARD_ID.SelectedValue = item.GetDataKeyValue("TT_WARD_ID")
                    End If

                    If IsNumeric(item.GetDataKeyValue("NATION_ID")) Then
                        cboNationlity.SelectedValue = item.GetDataKeyValue("NATION_ID")
                    End If

                    If IsNumeric(item.GetDataKeyValue("BIRTH_NATION_ID")) Then
                        cboNATIONALITYFAMILY.SelectedValue = item.GetDataKeyValue("BIRTH_NATION_ID")
                    End If

                    If IsNumeric(item.GetDataKeyValue("BIRTH_PROVINCE_ID")) Then
                        cbTempKtPROVINCE_ID.SelectedValue = item.GetDataKeyValue("BIRTH_PROVINCE_ID")
                    End If

                    If IsNumeric(item.GetDataKeyValue("BIRTH_PROVINCE_ID")) Then
                        Dim dt As DataTable = rep.GetDistrictList(cbTempKtPROVINCE_ID.SelectedValue, False)
                        FillRadCombobox(cbTempKtDISTRICT_ID, dt, "NAME", "ID")
                    End If

                    If IsNumeric(item.GetDataKeyValue("BIRTH_DISTRICT_ID")) Then
                        cbTempKtDISTRICT_ID.SelectedValue = item.GetDataKeyValue("BIRTH_DISTRICT_ID")
                    End If

                    If IsNumeric(item.GetDataKeyValue("BIRTH_WARD_ID")) Then
                        Dim dt As DataTable = rep.GetWardList(cbTempKtDISTRICT_ID.SelectedValue, False)
                        FillRadCombobox(cbTempKtWARD_ID, dt, "NAME", "ID")
                        cbTempKtWARD_ID.SelectedValue = item.GetDataKeyValue("BIRTH_WARD_ID")
                    End If

                    cboGender.SelectedValue = item.GetDataKeyValue("GENDER")

                    rdIDDate.SelectedDate = item.GetDataKeyValue("ID_NO_DATE")
                    txtIDPlace.Text = item.GetDataKeyValue("ID_NO_PLACE_NAME")
                    txtPhone.Text = item.GetDataKeyValue("PHONE")
                    rdMSTDate.SelectedDate = item.GetDataKeyValue("TAXTATION_DATE")
                    txt_MSTPLACE.Text = item.GetDataKeyValue("TAXTATION_PLACE")
                    txtBIRTH_CODE.Text = item.GetDataKeyValue("BIRTH_CODE")
                    txtQuyen.Text = item.GetDataKeyValue("QUYEN")

                End Using
                chkIs_Owner.Checked = item.GetDataKeyValue("IS_OWNER")
                chkIs_Pass.Checked = item.GetDataKeyValue("IS_PASS")
                hidFamilyID.Value = item.GetDataKeyValue("ID")
                hidID.Value = ""
                chkis_same_company.Checked = item.GetDataKeyValue("IS_SAME_COMPANY")
                chkIsDeduct_CheckedChanged(Nothing, Nothing)
                checkFK_Pkey = 0
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub chkIsDeduct_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkIsDeduct.CheckedChanged
        rdDeductFrom.Enabled = chkIsDeduct.Checked
        rdDeductTo.Enabled = chkIsDeduct.Checked
        rdDeductReg.Enabled = chkIsDeduct.Checked
        hid_Deduct.Visible = chkIsDeduct.Checked
    End Sub

    Protected Sub chkIsOwner_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkIs_Owner.CheckedChanged
        Try
            If CurrentState = CommonMessage.STATE_NEW Then
                If chkIs_Owner.Checked Then
                    txtHouseCertificate_Num.ReadOnly = False
                    txtHouseCertificate_Code.ReadOnly = False
                Else
                    txtHouseCertificate_Num.ReadOnly = True
                    txtHouseCertificate_Code.ReadOnly = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Protected Sub chkIs_Pass_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkIs_Pass.CheckedChanged
        If chkIs_Pass.Checked Then
            chkIsDeduct.Checked = False
            chkIsDeduct.Enabled = False
        Else
            chkIsDeduct.Enabled = True
        End If
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Using rep As New ProfileBusinessRepository
                    Dim lstID As New List(Of Decimal)
                    For Each item As GridDataItem In rgFamilyEdit.SelectedItems
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next
                    If lstID.Count > 0 Then
                        rep.SendEmployeeFamilyEdit(lstID)
                    End If
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgFamily.Rebind()
                    rgFamilyEdit.Rebind()
                End Using
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

#End Region

    Private Sub cboNationlity_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboNationlity.SelectedIndexChanged
        Dim dt As New DataTable
        Using repNS As New ProfileRepository
            If IsNumeric(cboNationlity.SelectedValue) Then
                dt = repNS.GetProvinceList1(cboNationlity.SelectedValue, False)
            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FillRadCombobox(cbDISTRICT_ID, dt, "NAME", "ID")
            End If
        End Using
    End Sub

    Private Sub cboNATIONALITYFAMILY_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboNATIONALITYFAMILY.SelectedIndexChanged
        Dim dt As New DataTable
        Using repNS As New ProfileRepository
            If IsNumeric(cboNATIONALITYFAMILY.SelectedValue) Then
                dt = repNS.GetProvinceList1(cboNATIONALITYFAMILY.SelectedValue, False)
            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FillRadCombobox(cbTempKtPROVINCE_ID, dt, "NAME", "ID")
            End If
        End Using
    End Sub


    Private Sub cbPROVINCE_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbPROVINCE_ID.SelectedIndexChanged
        Dim dt As New DataTable
        Using repNS As New ProfileRepository
            If IsNumeric(cbPROVINCE_ID.SelectedValue) Then
                dt = repNS.GetDistrictList(cbPROVINCE_ID.SelectedValue, False)
            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FillRadCombobox(cbDISTRICT_ID, dt, "NAME", "ID")
            End If
        End Using
    End Sub
    Private Sub cbDISTRICT_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbDISTRICT_ID.SelectedIndexChanged
        Dim dt As New DataTable
        Using repNS As New ProfileRepository
            If IsNumeric(cbDISTRICT_ID.SelectedValue) Then
                dt = repNS.GetWardList(cbDISTRICT_ID.SelectedValue, False)
            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FillRadCombobox(cbWARD_ID, dt, "NAME", "ID")
            End If
        End Using
    End Sub

    Private Sub cbTempPROVINCE_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbTempPROVINCE_ID.SelectedIndexChanged
        Dim dt As New DataTable
        Using repNS As New ProfileRepository
            If IsNumeric(cbTempPROVINCE_ID.SelectedValue) Then
                dt = repNS.GetDistrictList(cbTempPROVINCE_ID.SelectedValue, False)
            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FillRadCombobox(cbTempDISTRICT_ID, dt, "NAME", "ID")
            End If
        End Using
    End Sub
    Private Sub cbTempDISTRICT_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbTempDISTRICT_ID.SelectedIndexChanged
        Dim dt As New DataTable
        Using repNS As New ProfileRepository
            If IsNumeric(cbTempDISTRICT_ID.SelectedValue) Then
                dt = repNS.GetWardList(cbTempDISTRICT_ID.SelectedValue, False)
            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FillRadCombobox(cbTempWARD_ID, dt, "NAME", "ID")
            End If
        End Using
    End Sub



    Private Sub cbTempKtPROVINCE_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbTempKtPROVINCE_ID.SelectedIndexChanged
        Dim dt As New DataTable
        Using repNS As New ProfileRepository
            If IsNumeric(cbTempKtPROVINCE_ID.SelectedValue) Then
                dt = repNS.GetDistrictList(cbTempKtPROVINCE_ID.SelectedValue, False)
            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FillRadCombobox(cbTempKtDISTRICT_ID, dt, "NAME", "ID")
            End If
        End Using
    End Sub
    Private Sub cbTempKtDISTRICT_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbTempKtDISTRICT_ID.SelectedIndexChanged
        Dim dt As New DataTable
        Using repNS As New ProfileRepository
            If IsNumeric(cbTempKtDISTRICT_ID.SelectedValue) Then
                dt = repNS.GetWardList(cbTempKtDISTRICT_ID.SelectedValue, False)
            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FillRadCombobox(cbTempKtWARD_ID, dt, "NAME", "ID")
            End If
        End Using
    End Sub



    Protected Sub rgFamilyEdit_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgFamilyEdit.SelectedIndexChanged
        Try
            If rgFamilyEdit.SelectedItems.Count = 0 Then
                ClearControlValue(txtAdress, txtTempAdress, txtAD_Village, txtHouseCertificate_Code, txtHouseCertificate_Num,
                                        chkIs_Owner, chkIs_Pass, txtFullName, txtIDNO, txtRemark, txtTax, txt_MSTPLACE,
                                        rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo, txtCareer, txtTitle,
                                        chkIsDeduct, hidFamilyID, hidID, cboRelationship, cboNguyenQuan, cboGender, rdIDDate, txtIDPlace, cboNationlity, txtPhone, rdMSTDate, txtBIRTH_CODE, cboNATIONALITYFAMILY, cbTempKtPROVINCE_ID, cbTempKtDISTRICT_ID, cbTempKtWARD_ID, txtQuyen,
                                        cbPROVINCE_ID, cbDISTRICT_ID, cbWARD_ID, cbTempPROVINCE_ID, cbTempDISTRICT_ID, cbTempWARD_ID, chkis_same_company)
                Exit Sub
            End If
            ClearControlValue(txtAdress, txtTempAdress, txtAD_Village, txtHouseCertificate_Code, txtHouseCertificate_Num,
                                      chkIs_Owner, chkIs_Pass, txtFullName, txtIDNO, txtRemark, txtTax, txt_MSTPLACE,
                                      rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo, txtCareer, txtTitle,
                                      chkIsDeduct, hidFamilyID, hidID, cboRelationship, cboNguyenQuan, cboGender, rdIDDate, txtIDPlace, cboNationlity, txtPhone, rdMSTDate, txtBIRTH_CODE, cboNATIONALITYFAMILY, cbTempKtPROVINCE_ID, cbTempKtDISTRICT_ID, cbTempKtWARD_ID, txtQuyen,
                                      cbPROVINCE_ID, cbDISTRICT_ID, cbWARD_ID, cbTempPROVINCE_ID, cbTempDISTRICT_ID, cbTempWARD_ID, chkis_same_company)

            Dim item = CType(rgFamilyEdit.SelectedItems(rgFamilyEdit.SelectedItems.Count - 1), GridDataItem)
            CurrentState = CommonMessage.STATE_NORMAL
            hidFamilyID.Value = item.GetDataKeyValue("ID")
            txtAdress.Text = item.GetDataKeyValue("ADDRESS")
            txtIDNO.Text = item.GetDataKeyValue("ID_NO")
            txtFullName.Text = item.GetDataKeyValue("FULLNAME")
            txtTax.Text = item.GetDataKeyValue("TAXTATION")
            txtRemark.Text = item.GetDataKeyValue("REMARK")
            rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
            chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
            rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
            rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
            rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
            cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
            txtCareer.Text = item.GetDataKeyValue("CAREER")
            txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
            cboNguyenQuan.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
            txtHouseCertificate_Code.Text = item.GetDataKeyValue("CERTIFICATE_CODE")
            txtHouseCertificate_Num.Text = item.GetDataKeyValue("CERTIFICATE_NUM")
            txtTempAdress.Text = item.GetDataKeyValue("ADDRESS_TT")
            If IsNumeric(item.GetDataKeyValue("AD_PROVINCE_ID")) Then
                cbPROVINCE_ID.SelectedValue = item.GetDataKeyValue("AD_PROVINCE_ID")
            End If
            If IsNumeric(item.GetDataKeyValue("TT_PROVINCE_ID")) Then
                cbTempPROVINCE_ID.SelectedValue = item.GetDataKeyValue("TT_PROVINCE_ID")
            End If
            Using rep As New ProfileRepository
                If cbPROVINCE_ID.SelectedValue <> "" Then
                    Dim dt As DataTable = rep.GetDistrictList(cbPROVINCE_ID.SelectedValue, False)
                    FillRadCombobox(cbDISTRICT_ID, dt, "NAME", "ID")
                End If
                If IsNumeric(item.GetDataKeyValue("AD_DISTRICT_ID")) Then
                    cbDISTRICT_ID.SelectedValue = item.GetDataKeyValue("AD_DISTRICT_ID")
                End If
                If cbDISTRICT_ID.SelectedValue <> "" Then
                    Dim dt As DataTable = rep.GetWardList(cbDISTRICT_ID.SelectedValue, False)
                    FillRadCombobox(cbWARD_ID, dt, "NAME", "ID")
                End If
                If IsNumeric(item.GetDataKeyValue("AD_WARD_ID")) Then
                    cbWARD_ID.SelectedValue = item.GetDataKeyValue("AD_WARD_ID")
                End If
                If cbTempPROVINCE_ID.SelectedValue <> "" Then
                    Dim dt As DataTable = rep.GetDistrictList(cbTempPROVINCE_ID.SelectedValue, False)
                    FillRadCombobox(cbTempDISTRICT_ID, dt, "NAME", "ID")
                End If
                If IsNumeric(item.GetDataKeyValue("TT_DISTRICT_ID")) Then
                    cbTempDISTRICT_ID.SelectedValue = item.GetDataKeyValue("TT_DISTRICT_ID")
                End If
                If cbTempDISTRICT_ID.SelectedValue <> "" Then
                    Dim dt As DataTable = rep.GetWardList(cbTempDISTRICT_ID.SelectedValue, False)
                    FillRadCombobox(cbTempWARD_ID, dt, "NAME", "ID")
                End If
                If IsNumeric(item.GetDataKeyValue("TT_WARD_ID")) Then
                    cbTempWARD_ID.SelectedValue = item.GetDataKeyValue("TT_WARD_ID")
                End If

                If IsNumeric(item.GetDataKeyValue("NATION_ID")) Then
                    cboNationlity.SelectedValue = item.GetDataKeyValue("NATION_ID")
                End If

                If IsNumeric(item.GetDataKeyValue("BIRTH_NATION_ID")) Then
                    cboNATIONALITYFAMILY.SelectedValue = item.GetDataKeyValue("BIRTH_NATION_ID")
                End If

                If IsNumeric(item.GetDataKeyValue("BIRTH_PROVINCE_ID")) Then
                    cbTempKtPROVINCE_ID.SelectedValue = item.GetDataKeyValue("BIRTH_PROVINCE_ID")
                End If

                If IsNumeric(item.GetDataKeyValue("BIRTH_PROVINCE_ID")) Then
                    Dim dt As DataTable = rep.GetDistrictList(cbTempKtPROVINCE_ID.SelectedValue, False)
                    FillRadCombobox(cbTempKtDISTRICT_ID, dt, "NAME", "ID")
                End If

                If IsNumeric(item.GetDataKeyValue("BIRTH_DISTRICT_ID")) Then
                    cbTempKtDISTRICT_ID.SelectedValue = item.GetDataKeyValue("BIRTH_DISTRICT_ID")
                End If

                If IsNumeric(item.GetDataKeyValue("BIRTH_WARD_ID")) Then
                    Dim dt As DataTable = rep.GetWardList(cbTempKtDISTRICT_ID.SelectedValue, False)
                    FillRadCombobox(cbTempKtWARD_ID, dt, "NAME", "ID")
                    cbTempKtWARD_ID.SelectedValue = item.GetDataKeyValue("BIRTH_WARD_ID")
                End If

                cboGender.SelectedValue = item.GetDataKeyValue("GENDER")

                rdIDDate.SelectedDate = item.GetDataKeyValue("ID_NO_DATE")
                txtIDPlace.Text = item.GetDataKeyValue("ID_NO_PLACE_NAME")
                txtPhone.Text = item.GetDataKeyValue("PHONE")
                rdMSTDate.SelectedDate = item.GetDataKeyValue("TAXTATION_DATE")
                txt_MSTPLACE.Text = item.GetDataKeyValue("TAXTATION_PLACE")
                txtBIRTH_CODE.Text = item.GetDataKeyValue("BIRTH_CODE")
                txtQuyen.Text = item.GetDataKeyValue("QUYEN")

            End Using
            txtAD_Village.Text = item.GetDataKeyValue("AD_VILLAGE")
            chkIs_Owner.Checked = item.GetDataKeyValue("IS_OWNER")
            chkIs_Pass.Checked = item.GetDataKeyValue("IS_PASS")
            chkis_same_company.Checked = item.GetDataKeyValue("IS_SAME_COMPANY")
            If item.GetDataKeyValue("FK_PKEY") IsNot Nothing Then
                hidFamilyID.Value = item.GetDataKeyValue("FK_PKEY")
            End If
            hidID.Value = item.GetDataKeyValue("ID")
            chkIsDeduct_CheckedChanged(Nothing, Nothing)
            UpdateControlState()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rgFamily_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgFamily.SelectedIndexChanged
        Try
            If rgFamily.SelectedItems.Count = 0 Then
                ClearControlValue(txtAdress, txtTempAdress, txtAD_Village, txtHouseCertificate_Code, txtHouseCertificate_Num,
                                      chkIs_Owner, chkIs_Pass, txtFullName, txtIDNO, txtRemark, txtTax, cboGender, rdIDDate, txtIDPlace, cboNationlity, txtPhone, rdMSTDate, txtBIRTH_CODE, cboNATIONALITYFAMILY, cbTempKtPROVINCE_ID, cbTempKtDISTRICT_ID, cbTempKtWARD_ID,
                                      rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo, txtCareer, txtTitle, txtQuyen,
                                      chkIsDeduct, hidFamilyID, hidID, cboRelationship, cboNguyenQuan, txt_MSTPLACE,
                                      cbPROVINCE_ID, cbDISTRICT_ID, cbWARD_ID, cbTempPROVINCE_ID, cbTempDISTRICT_ID, cbTempWARD_ID, chkis_same_company)
                Exit Sub
            End If
            ClearControlValue(txtAdress, txtTempAdress, txtAD_Village, txtHouseCertificate_Code, txtHouseCertificate_Num,
                                      chkIs_Owner, chkIs_Pass, txtFullName, txtIDNO, txtRemark, txtTax, txtQuyen, cboGender, rdIDDate, txtIDPlace, cboNationlity, txtPhone, rdMSTDate, txtBIRTH_CODE, cboNATIONALITYFAMILY, cbTempKtPROVINCE_ID, cbTempKtDISTRICT_ID, cbTempKtWARD_ID,
                                      rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo, txtCareer, txtTitle,
                                      chkIsDeduct, hidFamilyID, hidID, cboRelationship, cboNguyenQuan, txt_MSTPLACE,
                                      cbPROVINCE_ID, cbDISTRICT_ID, cbWARD_ID, cbTempPROVINCE_ID, cbTempDISTRICT_ID, cbTempWARD_ID, chkis_same_company)
            CurrentState = CommonMessage.STATE_NORMAL
            Dim item = CType(rgFamily.SelectedItems(rgFamily.SelectedItems.Count - 1), GridDataItem)
            hidFamilyID.Value = item.GetDataKeyValue("ID")
            txtAdress.Text = item.GetDataKeyValue("ADDRESS")
            txtIDNO.Text = item.GetDataKeyValue("ID_NO")
            txtFullName.Text = item.GetDataKeyValue("FULLNAME")
            txtTax.Text = item.GetDataKeyValue("TAXTATION")
            txtRemark.Text = item.GetDataKeyValue("REMARK")
            rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
            chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
            rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
            rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
            rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
            cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
            txtCareer.Text = item.GetDataKeyValue("CAREER")
            txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
            cboNguyenQuan.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
            txtHouseCertificate_Code.Text = item.GetDataKeyValue("CERTIFICATE_CODE")
            txtHouseCertificate_Num.Text = item.GetDataKeyValue("CERTIFICATE_NUM")
            txtTempAdress.Text = item.GetDataKeyValue("ADDRESS_TT")
            txtAD_Village.Text = item.GetDataKeyValue("AD_VILLAGE")
            If IsNumeric(item.GetDataKeyValue("AD_PROVINCE_ID")) Then
                cbPROVINCE_ID.SelectedValue = item.GetDataKeyValue("AD_PROVINCE_ID")
            End If
            If IsNumeric(item.GetDataKeyValue("TT_PROVINCE_ID")) Then
                cbTempPROVINCE_ID.SelectedValue = item.GetDataKeyValue("TT_PROVINCE_ID")
            End If
            Using rep As New ProfileRepository
                If cbPROVINCE_ID.SelectedValue <> "" Then
                    Dim dt As DataTable = rep.GetDistrictList(cbPROVINCE_ID.SelectedValue, False)
                    FillRadCombobox(cbDISTRICT_ID, dt, "NAME", "ID")
                End If
                If IsNumeric(item.GetDataKeyValue("AD_DISTRICT_ID")) Then
                    cbDISTRICT_ID.SelectedValue = item.GetDataKeyValue("AD_DISTRICT_ID")
                End If
                If cbDISTRICT_ID.SelectedValue <> "" Then
                    Dim dt As DataTable = rep.GetWardList(cbDISTRICT_ID.SelectedValue, False)
                    FillRadCombobox(cbWARD_ID, dt, "NAME", "ID")
                End If
                If IsNumeric(item.GetDataKeyValue("AD_WARD_ID")) Then
                    cbWARD_ID.SelectedValue = item.GetDataKeyValue("AD_WARD_ID")
                End If
                If cbTempPROVINCE_ID.SelectedValue <> "" Then
                    Dim dt As DataTable = rep.GetDistrictList(cbTempPROVINCE_ID.SelectedValue, False)
                    FillRadCombobox(cbTempDISTRICT_ID, dt, "NAME", "ID")
                End If
                If IsNumeric(item.GetDataKeyValue("TT_DISTRICT_ID")) Then
                    cbTempDISTRICT_ID.SelectedValue = item.GetDataKeyValue("TT_DISTRICT_ID")
                End If
                If cbTempDISTRICT_ID.SelectedValue <> "" Then
                    Dim dt As DataTable = rep.GetWardList(cbTempDISTRICT_ID.SelectedValue, False)
                    FillRadCombobox(cbTempWARD_ID, dt, "NAME", "ID")
                End If
                If IsNumeric(item.GetDataKeyValue("TT_WARD_ID")) Then
                    cbTempWARD_ID.SelectedValue = item.GetDataKeyValue("TT_WARD_ID")
                End If

                If IsNumeric(item.GetDataKeyValue("NATION_ID")) Then
                    cboNationlity.SelectedValue = item.GetDataKeyValue("NATION_ID")
                End If

                If IsNumeric(item.GetDataKeyValue("BIRTH_NATION_ID")) Then
                    cboNATIONALITYFAMILY.SelectedValue = item.GetDataKeyValue("BIRTH_NATION_ID")
                End If

                If IsNumeric(item.GetDataKeyValue("BIRTH_PROVINCE_ID")) Then
                    cbTempKtPROVINCE_ID.SelectedValue = item.GetDataKeyValue("BIRTH_PROVINCE_ID")
                End If

                If IsNumeric(item.GetDataKeyValue("BIRTH_PROVINCE_ID")) Then
                    Dim dt As DataTable = rep.GetDistrictList(cbTempKtPROVINCE_ID.SelectedValue, False)
                    FillRadCombobox(cbTempKtDISTRICT_ID, dt, "NAME", "ID")
                End If

                If IsNumeric(item.GetDataKeyValue("BIRTH_DISTRICT_ID")) Then
                    cbTempKtDISTRICT_ID.SelectedValue = item.GetDataKeyValue("BIRTH_DISTRICT_ID")
                End If

                If IsNumeric(item.GetDataKeyValue("BIRTH_WARD_ID")) Then
                    Dim dt As DataTable = rep.GetWardList(cbTempKtDISTRICT_ID.SelectedValue, False)
                    FillRadCombobox(cbTempKtWARD_ID, dt, "NAME", "ID")
                    cbTempKtWARD_ID.SelectedValue = item.GetDataKeyValue("BIRTH_WARD_ID")
                End If

                cboGender.SelectedValue = item.GetDataKeyValue("GENDER")

                rdIDDate.SelectedDate = item.GetDataKeyValue("ID_NO_DATE")
                txtIDPlace.Text = item.GetDataKeyValue("ID_NO_PLACE_NAME")
                txtPhone.Text = item.GetDataKeyValue("PHONE")
                rdMSTDate.SelectedDate = item.GetDataKeyValue("TAXTATION_DATE")
                txt_MSTPLACE.Text = item.GetDataKeyValue("TAXTATION_PLACE")
                txtBIRTH_CODE.Text = item.GetDataKeyValue("BIRTH_CODE")
                txtQuyen.Text = item.GetDataKeyValue("QUYEN")
            End Using
            chkIs_Owner.Checked = item.GetDataKeyValue("IS_OWNER")
            chkIs_Pass.Checked = item.GetDataKeyValue("IS_PASS")
            chkis_same_company.Checked = item.GetDataKeyValue("IS_SAME_COMPANY")
            hidFamilyID.Value = item.GetDataKeyValue("ID")
            hidID.Value = ""
            chkIsDeduct_CheckedChanged(Nothing, Nothing)
            UpdateControlState()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub chkCopyEmployeeAddress_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCopyEmployeeAddress.CheckedChanged
        If chkCopyEmployeeAddress.Checked Then
            Using rep As New ProfileBusinessClient
                Dim Info As EmployeeCVDTO
                Info = rep.GetEmployeePerInfo(EmployeeID)
                txtAdress.Text = Info.PER_ADDRESS
                If Info.PER_PROVINCE IsNot Nothing Then
                    cbPROVINCE_ID.SelectedValue = Info.PER_PROVINCE
                End If
                If Info.PER_DISTRICT IsNot Nothing Then
                    cbDISTRICT_ID.SelectedValue = Info.PER_DISTRICT
                End If
                If Info.PER_WARD IsNot Nothing Then
                    cbWARD_ID.SelectedValue = Info.PER_WARD
                End If
                txtTempAdress.Text = Info.NAV_ADDRESS
                If Info.NAV_PROVINCE IsNot Nothing Then
                    cbTempPROVINCE_ID.SelectedValue = Info.NAV_PROVINCE
                End If
                If Info.NAV_DISTRICT IsNot Nothing Then
                    cbTempDISTRICT_ID.SelectedValue = Info.NAV_DISTRICT
                End If
                If Info.NAV_WARD IsNot Nothing Then
                    cbTempWARD_ID.SelectedValue = Info.NAV_WARD
                End If
            End Using
        End If
    End Sub
End Class
